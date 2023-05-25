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
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Http.Json;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;
using System.Windows;
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

                foreach (var item in this.CvsModel!.Items)
                {
                    sb.AppendLine($"## {item.Id} {item.Name}");
                    sb.AppendLine($"");
                    sb.AppendLine($"Creator:{item.Creator.Username}");
                    sb.AppendLine($"AllowCommercialUse:{item.AllowCommercialUse}");
                    sb.AppendLine($"AllowNoCredit:{item.AllowNoCredit}");
                    sb.AppendLine($"Nsfw:{item.Nsfw}");
                    sb.AppendLine($"URL:https://civitai.com/models/{item.Id}");
                    sb.AppendLine($"DownloadCount:{item.Stats.DownloadCount}");
                    sb.AppendLine($"CommentCount:{item.Stats.CommentCount}");
                    sb.AppendLine($"FavoriteCount:{item.Stats.FavoriteCount}");
                    sb.AppendLine($"RatingCount:{item.Stats.RatingCount}");
                    sb.AppendLine($"Rating:{item.Stats.Rating}");
                    sb.AppendLine($"");
                    foreach (var modelver in item.ModelVersions)
                    {
                        sb.AppendLine($"### {modelver.Name}");
                        sb.AppendLine($"");
                        sb.AppendLine($"Create At {modelver.CreatedAt}");
                        sb.AppendLine($"");
                        foreach (var image in modelver.Images)
                        {
                            sb.AppendLine($"");
                            sb.AppendLine($"```");
                            sb.AppendLine($"Meta:{image.Meta}");
                            sb.AppendLine($"```");
                            sb.AppendLine($"<img alt=\"{image.Url}\" src=\"{image.Url}\" width=\"20%\">");
                            sb.AppendLine($"");
                        }
                        sb.AppendLine($"");
                    }
                    sb.AppendLine($"");
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
        public void GETQuery()
        {
            try
            {
                // GET クエリの実行
                GETQuery(this.GetCondition.GetConditionQuery);
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
        private async void GETQuery(string query, bool add_endpoint = true)
        {
            try
            {
                GetModelReqestM tmp = new GetModelReqestM();
                string request = string.Empty;

                // 実行してJSON形式をデシリアライズ
                var request_model = JsonExtensions.DeserializeFromFile<CvsModelM>(request = await tmp.Request(query, add_endpoint));

                // Nullチェック
                if (request_model != null)
                {
                    this.CvsModel = new CvsModelExM(request_model); // ModelListへ変換
                    this.CvsModel!.Rowdata = request;               // 生データの保持
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
        public void ModelSelectionChanged()
        {
            try
            {
                if (this.CvsModel != null && this.CvsModel.Items != null && this.CvsModel.Items.SelectedItem != null)
                {
                    List<CvsImages> tmp_img = new List<CvsImages>();

                    foreach (var modelver in this.CvsModel.Items.SelectedItem.ModelVersions)
                    {
                        tmp_img.AddRange(modelver.Images);
                    }

                    this.Images = new ObservableCollection<CvsImages>(tmp_img);
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
        public void ModelVersionSelectionChanged()
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
        public void MoveNext()
        {
            try
            {
                // null check
                if (this.CvsModel != null)
                {
                    // Execute GET Query
                    GETQuery(this.CvsModel.Metadata.NextPage, false);
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
        public void MovePrev()
        {
            try
            {
                // null check
                if (this.CvsModel != null)
                {
                    // Execute GET Query
                    GETQuery(this.CvsModel.Metadata.PrevPage, false);
                }
            }
            catch (Exception ex)
            {
                ShowMessage.ShowErrorOK(ex.Message, "Error");
            }
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
