using CvSeachTool.Common.Utilities;
using CvSeachTool.Models;
using CvSeachTool.Models.Condition;
using Microsoft.Win32;
using MVVMCore.BaseClass;
using MVVMCore.Common.Utilities;
using MVVMCore.Common.Wrapper;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Data.Common;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Http.Json;
using System.Reflection;
using System.Reflection.Metadata;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using static CvSeachTool.Models.CvsModelM;
using static CvSeachTool.Models.CvsModelM.CvsModelVersions;

namespace CvSeachTool.ViewModels
{
    public class MainWindowVM : ViewModelBase
    {
        #region stablediffusion model object[CvsModel]プロパティ
        /// <summary>
        /// stablediffusion model object[CvsModel]プロパティ用変数
        /// </summary>
        CvsModelExM? _CvsModel = null;
        /// <summary>
        /// stablediffusion model object[CvsModel]プロパティ
        /// </summary>
        public CvsModelExM? CvsModel
        {
            get
            {
                return _CvsModel;
            }
            set
            {
                if (_CvsModel == null || !_CvsModel.Equals(value))
                {
                    _CvsModel = value;
                    NotifyPropertyChanged("CvsModel");
                }
            }
        }
        #endregion

        #region GET Query Condtion[GetCondition]プロパティ
        /// <summary>
        /// GET Query Condtion[GetCondition]プロパティ用変数
        /// </summary>
        CvsModelGetConditionM _GetCondition = new CvsModelGetConditionM();
        /// <summary>
        /// GET Query Condtion[GetCondition]プロパティ
        /// </summary>
        public CvsModelGetConditionM GetCondition
        {
            get
            {
                return _GetCondition;
            }
            set
            {
                if (_GetCondition == null || !_GetCondition.Equals(value))
                {
                    _GetCondition = value;
                    NotifyPropertyChanged("GetCondition");
                }
            }
        }
        #endregion

        #region Current page[CurrentPage]プロパティ
        /// <summary>
        /// Current page[CurrentPage]プロパティ用変数
        /// </summary>
        int _CurrentPage = 0;
        /// <summary>
        /// Current page[CurrentPage]プロパティ
        /// </summary>
        public int CurrentPage
        {
            get
            {
                return _CurrentPage;
            }
            set
            {
                if (!_CurrentPage.Equals(value))
                {
                    _CurrentPage = value;
                    NotifyPropertyChanged("CurrentPage");
                }
            }
        }
        #endregion

        #region This model images[Images]プロパティ
        /// <summary>
        /// This model images[Images]プロパティ用変数
        /// </summary>
        ObservableCollection<CvsImages> _Images = new ObservableCollection<CvsImages>();
        /// <summary>
        /// This model images[Images]プロパティ
        /// </summary>
        [JsonPropertyName("images")]
        public ObservableCollection<CvsImages> Images
        {
            get
            {
                return _Images;
            }
            set
            {
                if (_Images == null || !_Images.Equals(value))
                {
                    _Images = value;
                    NotifyPropertyChanged("Images");
                }
            }
        }
        #endregion

        #region Selected image[SelectedImage]プロパティ
        /// <summary>
        /// Selected image[SelectedImage]プロパティ用変数
        /// </summary>
        CvsImages _SelectedImage = new CvsImages();
        /// <summary>
        /// Selected image[SelectedImage]プロパティ
        /// </summary>
        public CvsImages SelectedImage
        {
            get
            {
                return _SelectedImage;
            }
            set
            {
                if (_SelectedImage == null || !_SelectedImage.Equals(value))
                {
                    _SelectedImage = value;
                    NotifyPropertyChanged("SelectedImage");
                }
            }
        }
        #endregion



        #region マークダウンの出力処理
        /// <summary>
        /// マークダウンの出力処理
        /// </summary>
        public void Output()
        {
            // ダイアログのインスタンスを生成
            var dialog = new SaveFileDialog();

            // ファイルの種類を設定
            dialog.Filter = "マークダウン (*.md)|*.md";

            // ダイアログを表示する
            if (dialog.ShowDialog() == true)
            {
                StringBuilder sb = new StringBuilder();

                int rank = 1;
                foreach (var item in this.CvsModel!.Items)
                {
                    sb.AppendLine($"## {rank++}位 {item.Id} {item.Name}");
                    sb.AppendLine($"");
                    sb.AppendLine($"- Creator : {item.Creator.Username}");
                    sb.AppendLine($"- AllowCommercialUse : {item.AllowCommercialUse}");
                    sb.AppendLine($"- AllowNoCredit : {item.AllowNoCredit}");
                    sb.AppendLine($"- Nsfw : {item.Nsfw}");
                    sb.AppendLine($"- URL : https://civitai.com/models/{item.Id}");
                    //sb.AppendLine($"- DownloadCount : {item.Stats.DownloadCount}");
                    //sb.AppendLine($"- CommentCount:{item.Stats.CommentCount}");
                    //sb.AppendLine($"- FavoriteCount:{item.Stats.FavoriteCount}");
                    //sb.AppendLine($"- RatingCount:{item.Stats.RatingCount}");
                    //sb.AppendLine($"- Rating:{item.Stats.Rating}");
                    sb.AppendLine($"");
                    foreach (var modelver in item.ModelVersions)
                    {
                        sb.AppendLine($"### ver : {modelver.Name}");
                        sb.AppendLine($"");
                        sb.AppendLine($"- Create At {modelver.CreatedAt}");
                        sb.AppendLine($"- ModelVersionURL https://civitai.com/models/{item.Id}?modelVersionId={modelver.Id}");
                        sb.AppendLine($"- [Model Download]({modelver.DownloadUrl})");
                        sb.AppendLine($"");
                        int count = 0;
                        foreach (var image in modelver.Images)
                        {
                            sb.AppendLine($"");
                            //sb.AppendLine($"{image.Nsfw}");

                            if (image.Meta != null && (image.Nsfw.Equals("None") || image.Nsfw.Equals("Soft")))
                            {
                                sb.AppendLine($"```");
                                sb.AppendLine($"Prompt : {image.Meta.Prompt}");
                                sb.AppendLine($"");
                                sb.AppendLine($"Negative Prompt : {image.Meta.NegativPrompt}");
                                sb.AppendLine($"```");
                                sb.AppendLine($"");
                                sb.AppendLine($"");
                                sb.AppendLine($"<img alt=\"{image.Url}\" src=\"{image.Url}\" width=\"20%\">");
                                sb.AppendLine($"");
                                if (count++ > 2) break;
                                //break;
                            }
                        }
                        sb.AppendLine($"");
                        break;
                    }
                    sb.AppendLine($"");
                }

                // ファイル出力処理
                File.WriteAllText(dialog.FileName, sb.ToString());
            }
        }
        #endregion

        #region マークダウンの出力処理
        /// <summary>
        /// マークダウンの出力処理
        /// </summary>
        public void Output2()
        {
            // ダイアログのインスタンスを生成
            var dialog = new SaveFileDialog();

            // ファイルの種類を設定
            dialog.Filter = "マークダウン (*.md)|*.md";

            // ダイアログを表示する
            if (dialog.ShowDialog() == true)
            {
                StringBuilder sb = new StringBuilder();
                sb.AppendLine($"## CIVITAIモデルダウンロードランキング");
                sb.AppendLine($"データ取得日 : {DateTime.Today.ToString("yyyy/MM/dd")}");
                sb.AppendLine($"※各リンクはCIVITAIへのログインが必要です");
                sb.AppendLine($"");
                sb.AppendLine($"CIVITAI");
                sb.AppendLine($"https://civitai.com/");
                sb.AppendLine($"");
                sb.AppendLine($"REST API");
                sb.AppendLine($"{CvsModel!.RequestURL}");
                sb.AppendLine($"");


                sb.AppendLine($"|<center>順位</center><center>(DL数)</center>|モデルID / 作者名<br>モデル名|モデルタイプ<br>NSFW<br>商用利用|");
                sb.AppendLine($"|---|---|---|");


                int rank = 1;
                foreach (var item in this.CvsModel!.Items)
                {
                    sb.AppendLine($"|<center>{rank++}位</center><center>({item.Stats.DownloadCount})</center>" +
                        $"|{item.Id} / [{item.Creator.Username}](https://civitai.com/user/{item.Creator.Username}/models)<br>[{item.Name.Replace("|", "\\|")}](https://civitai.com/models/{item.Id})" +
                        $"| {item.Type}<br>{(item.Nsfw ? "NSFW" : "-")}<br>{item.AllowCommercialUse}|");
                }

                // ファイル出力処理
                File.WriteAllText(dialog.FileName, sb.ToString());
            }
        }
        #endregion

        #region Execute GET REST API
        /// <summary>
        /// Execute GET REST API
        /// </summary>
        public void GETQuery(object sender, EventArgs ev)
        {
            try
            {
                // GET クエリの実行
                GETQuery(sender, this.GetCondition.GetConditionQuery);
            }
            catch(Exception e)
            {
                ShowMessage.ShowErrorOK(e.Message, "Error");
            }
        }
        #endregion

        #region GETクエリの実行処理
        /// <summary>
        /// GETクエリの実行処理
        /// </summary>
        /// <param name="query"></param>
        /// <param name="add_endpoint"></param>
        private async void GETQuery(object sender, string query, bool add_endpoint = true)
        {
            try
            {
                GetModelReqestM tmp = new GetModelReqestM();
                string request = string.Empty;

                // エンドポイント + パラメータ
                string url = string.Empty;
                if (add_endpoint)
                {
                    // エンドポイント + パラメータ
                    url = CvsModelM.Endpoint + query;
                }
                else
                {
                    // エンドポイント + パラメータ
                    url = query;
                }

                // 実行してJSON形式をデシリアライズ
                var request_model = JsonExtensions.DeserializeFromFile<CvsModelM>(request = await tmp.Request(url));

                // Nullチェック
                if (request_model != null)
                {
                    this.CvsModel = new CvsModelExM(request_model); // ModelListへ変換
                    this.CvsModel!.Rowdata = request;               // 生データの保持
                    this.CvsModel!.RequestURL = url;               // 生データの保持

                    // 1つ以上要素が存在する場合
                    if (this.CvsModel.Items.Count > 0)
                    {
                        // 1つ目の要素をセットする
                        this.CvsModel.Items.SelectedItem = this.CvsModel.Items.ElementAt(0);

                        // DataGridを先頭へスクロールさせる
                        DataGridTopRow(sender);
                    }

                }
            }
            catch (Exception e)
            {
                ShowMessage.ShowErrorOK(e.Message, "Error");
            }
        }
        #endregion

        #region モデルの選択が変更された場合の処理
        /// <summary>
        /// モデルの選択が変更された場合の処理
        /// </summary>
        public void ModelSelectionChanged(object sender, EventArgs ev)
        {
            try
            {
                // nullチェック
                if (this.CvsModel != null && this.CvsModel.Items != null && this.CvsModel.Items.SelectedItem != null)
                {

                    List<CvsImages> tmp_img = new List<CvsImages>();

                    // モデルバージョン分イメージをリストにセット
                    foreach (var modelver in this.CvsModel.Items.SelectedItem.ModelVersions)
                    {
                        // イメージをリストにセット
                        tmp_img.AddRange(modelver.Images);
                    }

                    this.Images = new ObservableCollection<CvsImages>(tmp_img);

                    // イメージが存在する場合
                    if (this.Images.Any())
                    {
                        // 先頭のイメージを選択する
                        this.SelectedImage = this.Images.ElementAt(0);

                        // ImageリストのListViewを先頭へスクロールさせる
                        ListViewTopRow(sender);
                    }
                }
            }
            catch (Exception ex)
            {
                ShowMessage.ShowErrorOK(ex.Message, "Error");
            }
        }
        #endregion


        #region モデルの選択が変更された場合の処理
        /// <summary>
        /// モデルの選択が変更された場合の処理
        /// </summary>
        public void ModelVersionSelectionChanged(object sender, EventArgs ev)
        {
            try
            {
                // nullチェック
                if (this.CvsModel != null && this.CvsModel.Items != null 
                    && this.CvsModel.Items.SelectedItem != null 
                    && this.CvsModel.Items.SelectedItem.SelectedModelVersion != null)
                {
                    List<CvsImages> tmp_img = new List<CvsImages>();

                    // 対象行をセット
                    this.Images = new ObservableCollection<CvsImages>(this.CvsModel.Items.SelectedItem.SelectedModelVersion.Images);

                    // イメージが存在する場合
                    if (this.Images.Any())
                    {
                        // 先頭のイメージを選択する
                        this.SelectedImage = this.Images.ElementAt(0);

                        // ImageリストのListViewを先頭へスクロールさせる
                        ListViewTopRow(sender);
                    }
                }
            }
            catch (Exception ex)
            {
                ShowMessage.ShowErrorOK(ex.Message, "Error");
            }
        }
        #endregion

        #region ListViewを先頭へスクロールさせる処理
        /// <summary>
        /// DataGridを先頭へスクロールさせる処理
        /// </summary>
        /// <param name="sender">画面内のコントロールオブジェクト</param>
        private void ListViewTopRow(object sender)
        {
            // ウィンドウの取得
            var wnd = (MainWindow)VisualTreeHelperWrapper.GetWindow<MainWindow>(sender);

            // イメージのListViewのスクロールバーを先頭へ移動
            ScrollbarTopRow.TopRow4ListView(wnd.lvImages);
        }
        #endregion


        #region フレーズのダブルクリック
        /// <summary>
        /// フレーズのダブルクリック
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        public void OpenURL(object sender, EventArgs e)
        {
            try
            {
                // ウィンドウを取得
                var wnd = VisualTreeHelperWrapper.GetWindow<MainWindow>(sender) as MainWindow;

                // nullチェック
                if (wnd != null && this.CvsModel != null && this.CvsModel.Items != null && this.CvsModel.Items.SelectedItem != null)
                {
                    var startInfo = new System.Diagnostics.ProcessStartInfo($"https://civitai.com/models/{this.CvsModel!.Items.SelectedItem.Id}");
                    startInfo.UseShellExecute = true;
                    System.Diagnostics.Process.Start(startInfo);
                }
            }
            catch (Exception ex)
            {
                ShowMessage.ShowErrorOK(ex.Message, "Error");
            }
        }
        #endregion

        #region フレーズのダブルクリック
        /// <summary>
        /// フレーズのダブルクリック
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        public void OpenURLModelVersion(object sender, EventArgs e)
        {
            try
            {
                // ウィンドウを取得
                var wnd = VisualTreeHelperWrapper.GetWindow<MainWindow>(sender) as MainWindow;

                // nullチェック
                if (wnd != null && this.CvsModel != null && this.CvsModel.Items != null && this.CvsModel.Items.SelectedItem != null && this.CvsModel!.Items.SelectedItem.SelectedModelVersion != null)
                {
                    var startInfo = new System.Diagnostics.ProcessStartInfo($"https://civitai.com/models/{this.CvsModel!.Items.SelectedItem.Id}" + $"?modelVersionId={this.CvsModel!.Items.SelectedItem.SelectedModelVersion.Id}");
                    startInfo.UseShellExecute = true;
                    System.Diagnostics.Process.Start(startInfo);
                }
            }
            catch (Exception ex)
            {
                ShowMessage.ShowErrorOK(ex.Message, "Error");
            }
        }
        #endregion

        #region 次のページへ移動
        /// <summary>
        /// 次のページへ移動
        /// </summary>
        public void MoveNext(object sender, EventArgs ev)
        {
            try
            {
                // null check
                if (this.CvsModel != null)
                {
                    // Execute GET Query
                    GETQuery(sender, this.CvsModel.Metadata.NextPage, false);


                }
            }
            catch (Exception ex)
            {
                ShowMessage.ShowErrorOK(ex.Message, "Error");
            }
        }
        #endregion

        #region 前のページへ移動
        /// <summary>
        /// 前のページへ移動
        /// </summary>
        public void MovePrev(object sender, EventArgs ev)
        {
            try
            {
                // null check
                if (this.CvsModel != null)
                {
                    // Execute GET Query
                    GETQuery(sender, this.CvsModel.Metadata.PrevPage, false);
                }
            }
            catch (Exception ex)
            {
                ShowMessage.ShowErrorOK(ex.Message, "Error");
            }
        }
        #endregion

        #region DataGridを先頭へスクロールさせる処理
        /// <summary>
        /// DataGridを先頭へスクロールさせる処理
        /// </summary>
        /// <param name="sender">画面内のコントロールオブジェクト</param>
        private void DataGridTopRow(object sender)
        {
            // ウィンドウの取得
            var wnd = (MainWindow)VisualTreeHelperWrapper.GetWindow<MainWindow>(sender);

            // モデルのDataGridのスクロールバーを先頭へ移動
            ScrollbarTopRow.TopRow4DataGrid(wnd.dgModel);

            // モデルバージョンのDataGridのスクロールバーを先頭へ移動
            ScrollbarTopRow.TopRow4DataGrid(wnd.dgModelVersions);
        }
        #endregion

        /// <summary>
        /// 画面初期化処理
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        public override void Init(object sender, EventArgs e)
        {
        }

        public override void Close(object sender, EventArgs e)
        {

        }


    }
}
