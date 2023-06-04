﻿using CvSeachTool.Common;
using CvSeachTool.Common.Enums;
using CvSeachTool.Common.Utilities;
using CvSeachTool.Models;
using CvSeachTool.Models.Condition;
using CvSeachTool.Views;
using Microsoft.Win32;
using Microsoft.WindowsAPICodePack.Dialogs;
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
using System.Windows.Markup;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using static CvSeachTool.Models.CvsModelM;
using static CvSeachTool.Models.CvsModelM.CvsModelVersions;

namespace CvSeachTool.ViewModels
{
    public class MainWindowVM : ViewModelBase
    {
        #region イメージフィルタ用[ImageFilter]プロパティ
        /// <summary>
        /// イメージフィルタ用[ImageFilter]プロパティ
        /// </summary>
        public ImageNsfwEnum ImageFilter
        {
            get
            {
                return GblValues.Instance.ImageFilter;
            }
            set
            {
                if (!GblValues.Instance.ImageFilter.Equals(value))
                {
                    GblValues.Instance.ImageFilter = value;
                    NotifyPropertyChanged("ImageFilter");
                }
            }
        }
        #endregion

        #region イメージリスト[ImageList]プロパティ
        /// <summary>
        /// イメージリスト[ImageList]プロパティ用変数
        /// </summary>
        DisplayImageM _ImageList = new DisplayImageM();
        /// <summary>
        /// イメージリスト[ImageList]プロパティ
        /// </summary>
        public DisplayImageM ImageList
        {
            get
            {
                return _ImageList;
            }
            set
            {
                if (_ImageList == null || !_ImageList.Equals(value))
                {
                    _ImageList = value;
                    NotifyPropertyChanged("ImageList");
                }
            }
        }
        #endregion

        #region API実行中フラグ(true:実行中 false:実行中でない)[ExecuteGetAPI]プロパティ
        /// <summary>
        /// API実行中フラグ(true:実行中 false:実行中でない)[ExecuteGetAPI]プロパティ用変数
        /// </summary>
        bool _ExecuteGetAPI = false;
        /// <summary>
        /// API実行中フラグ(true:実行中 false:実行中でない)[ExecuteGetAPI]プロパティ
        /// </summary>
        public bool ExecuteGetAPI
        {
            get
            {
                return _ExecuteGetAPI;
            }
            set
            {
                if (!_ExecuteGetAPI.Equals(value))
                {
                    _ExecuteGetAPI = value;
                    NotifyPropertyChanged("ExecuteGetAPI");
                }
            }
        }
        #endregion

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

        #region ブックマーク[BookmarkConf]プロパティ
        /// <summary>
        /// ブックマーク[BookmarkConf]プロパティ
        /// </summary>
        public ConfigManager<ModelList<CvsItems>>? BookmarkConf
        {
            get
            {
                return GblValues.Instance.BookmarkConf;
            }
            set
            {
                if (GblValues.Instance.BookmarkConf == null || !GblValues.Instance.BookmarkConf.Equals(value))
                {
                    GblValues.Instance.BookmarkConf = value;
                    NotifyPropertyChanged("BookmarkConf");
                }
            }
        }
        #endregion

        #region Configファイルオブジェクト[Config]プロパティ
        /// <summary>
        /// Configファイルオブジェクト[Config]プロパティ
        /// </summary>
        public ConfigManager<ConfigM>? Config
        {
            get
            {
                return GblValues.Instance.Config;
            }
            set
            {
                if (GblValues.Instance.Config == null || !GblValues.Instance.Config.Equals(value))
                {
                    GblValues.Instance.Config = value;
                    NotifyPropertyChanged("Config");
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

        #region コンストラクタ
        /// <summary>
        /// コンストラクタ
        /// </summary>
        public MainWindowVM()
        {
        }
        #endregion

        #region マークダウンの出力処理
        /// <summary>
        /// マークダウンの出力処理
        /// </summary>
        public void Output()
        {
            try
            {
                // nullチェック
                if (this.CvsModel != null)
                {
                    // マークダウン出力1
                    this.CvsModel.MarkdownOutput1();
                }
            }
            catch (Exception ex)
            {
                ShowMessage.ShowErrorOK(ex.Message, "Error");
            }
        }
        #endregion

        #region マークダウンの出力処理
        /// <summary>
        /// マークダウンの出力処理
        /// </summary>
        public void Output2()
        {
            try
            {
                // nullチェック
                if (this.CvsModel != null)
                {
                    // マークダウン出力2
                    this.CvsModel.MarkdownOutput2();
                }
            }
            catch (Exception ex)
            {
                ShowMessage.ShowErrorOK(ex.Message, "Error");
            }
        }
        #endregion

        public void Output3()
        {
            try
            {
                // nullチェック
                if (this.CvsModel != null)
                {

                    using (var cofd = new CommonOpenFileDialog()
                    {
                        Title = "フォルダを選択してください",
                        // フォルダ選択モードにする
                        IsFolderPicker = true,
                    })
                    {
                        if (cofd.ShowDialog() == CommonFileDialogResult.Ok)
                        {
                            this.CvsModel.OutputMarkdown3(cofd.FileName);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                ShowMessage.ShowErrorOK(ex.Message, "Error");
            }
        }

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
                this.ExecuteGetAPI = true;
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
                var request_model = JSONUtil.DeserializeFromText<CvsModelM>(request = await tmp.Request(url));

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

                    // ブックマーク登録されている場合はブックマーク情報をセットする
                    if (this.BookmarkConf != null && this.BookmarkConf.Item != null && this.BookmarkConf.Item.Items != null)
                    {
                        // モデル全数分回す
                        foreach (var cvitem in this.CvsModel.Items)
                        {
                            // ブックマークに登録されているIDならセット
                            cvitem.IsBookmark = (from x in this.BookmarkConf.Item.Items
                                                 where x.Id.Equals(cvitem.Id)
                                                 select x).Any();
                        }
                    }

                }
            }
            catch (Exception e)
            {
                ShowMessage.ShowErrorOK(e.Message, "Error");
            }
            finally
            {
                this.ExecuteGetAPI = false;
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

                    // イメージをセットする
                    this.ImageList.SetImages(new ObservableCollection<CvsImages>(tmp_img));

                    // 最初の行を選択する
                    this.ImageList.SetFirst();

                    // ImageリストのListViewを先頭へスクロールさせる
                    ListViewTopRow(sender);
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
                    this.ImageList.SetImages(this.CvsModel.Items.SelectedItem.SelectedModelVersion.Images);

                    // 最初の行を選択する
                    this.ImageList.SetFirst();

                    // ImageリストのListViewを先頭へスクロールさせる
                    ListViewTopRow(sender);
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
                    // 次のページが最終ページより前である場合
                    if (this.CvsModel.Metadata.CurrentPage + 1 <= CvsModel.Metadata.TotalPages)
                    {
                        // Execute GET Query
                        GETQuery(sender, this.CvsModel.Metadata.NextPage, false);
                    }
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
                    // 前のページが1より大きい場合
                    if (this.CvsModel.Metadata.CurrentPage - 1 >= 1)
                    {
                        // Execute GET Query
                        GETQuery(sender, this.CvsModel.Metadata.PrevPage, false);
                    }
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

        #region ファイルのプロンプト情報抜き出し
        /// <summary>
        /// ファイルのプロンプト情報抜き出し
        /// </summary>
        public void ImageFileRead()
        {
            
            try
            {
                // ダイアログのインスタンスを生成
                var dialog = new OpenFileDialog();

                // ファイルの種類を設定
                dialog.Filter = "画像ファイル (*.png)|*.png|全てのファイル (*.*)|*.*";

                string message = string.Empty;
                // ダイアログを表示する
                if (dialog.ShowDialog() == true)
                {

                    using (var reader = new BinaryReader(File.Open(dialog.FileName, FileMode.Open, FileAccess.Read)))
                    {
                        if (PngReader.ReadPngSignature(reader))
                        {
                            var ihdrchunk = PngReader.ReadChunk(reader);
                            var itextchunk = PngReader.ReadChunk(reader);

                            // データがutf - 8の場合
                            message = System.Text.Encoding.UTF8.GetString(itextchunk.ChunkData).Replace("\0", ":");
                        }
                    }

                }

                if (!string.IsNullOrEmpty(message))
                {
                    ShowMessage.ShowNoticeOK(message, "Notice");
                }
            }
            catch (Exception ex)
            {
                ShowMessage.ShowErrorOK(ex.Message, "Error");
            }
        }
        #endregion

        #region ブックマーク画面を開く処理
        /// <summary>
        /// ブックマーク画面を開く処理
        /// </summary>
        public void OpenBookmarkV()
        {
            try
            {
                var wnd = new BookmarkV();
                var vm = wnd.DataContext as BookmarkVM;

                // ブックマーク画面の表示
                wnd.ShowDialog();

                // コンフィグファイルの読み込み直し
                GblValues.Instance.ConfigInit();

                // モデルのリストの初期化処理
                InitModelList();

            }
            catch (Exception ex)
            {
                ShowMessage.ShowErrorOK(ex.Message, "Error");
            }
        }
        #endregion

        #region ブックマークへ追加
        /// <summary>
        /// ブックマークへ追加
        /// </summary>
        public void AddBookmark()
        {
            try
            {
                // nullチェック
                if (this.CvsModel != null && this.CvsModel.Items != null && this.CvsModel.Items.SelectedItem != null
                    && this.BookmarkConf != null && this.BookmarkConf.Item != null && this.BookmarkConf.Item.Items != null)
                {
                    var check = (from x in this.BookmarkConf.Item.Items
                                 where x.Id.Equals(this.CvsModel.Items.SelectedItem.Id)
                                 select x);

                    // Bookmarkをセット
                    this.CvsModel.Items.SelectedItem.IsBookmark = !this.CvsModel.Items.SelectedItem.IsBookmark;

                    // 存在しなければ追加
                    if (!check.Any())
                    {
                        // ブックマークの追加
                        this.BookmarkConf.Item.Items.Add(this.CvsModel.Items.SelectedItem);
                    }
                    else
                    {
                        // ブックマークの追加
                        this.BookmarkConf.Item.Items.Remove(check.First());
                    }
                    // JSON形式で保存
                    this.BookmarkConf!.SaveJSON();
                }
            }
            catch (Exception ex)
            {
                ShowMessage.ShowErrorOK(ex.Message, "Error");
            }
        }
        #endregion

        #region 画面初期化処理
        /// <summary>
        /// 画面初期化処理
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        public override void Init(object sender, EventArgs e)
        {
            try
            {
                // モデルのリストの初期化処理
                InitModelList();
            }
            catch (Exception ex)
            {
                ShowMessage.ShowErrorOK(ex.Message, "Error");
            }
        }
        #endregion

        #region モデルのリストの初期化処理
        /// <summary>
        /// モデルのリストの初期化処理
        /// </summary>
        private void InitModelList()
        {
            // オブジェクトの作成
            this.CvsModel = new CvsModelExM(new CvsModelM());

            // Bookmarkを初期リストに追加
            this.CvsModel.Items = this.BookmarkConf!.Item;

            foreach (var item in this.CvsModel.Items)
            {
                item.IsBookmark = true;
            }
        }
        #endregion

        #region クローズ処理
        /// <summary>
        /// クローズ処理
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        public override void Close(object sender, EventArgs e)
        {

        }
        #endregion

    }
}
