﻿using CvSeachTool.Common.Enums;
using CvSeachTool.Common;
using CvSeachTool.Models;
using CvSeachTool.Models.Condition;
using CvSeachTool.Models.CvsImage;
using CvSeachTool.Models.CvsModel;
using Microsoft.Win32;
using MVVMCore.BaseClass;
using MVVMCore.Common.Utilities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DisplayImageM = CvSeachTool.Models.CvsImage.DisplayImageM;
using static CvSeachTool.Models.CvsModel.CvsModelM.CvsModelVersions;
using System.Collections.ObjectModel;
using static CvSeachTool.Models.CvsImage.CvsImageM;
using System.Windows;

namespace CvSeachTool.ViewModels
{
    public class UcSearchImageVM : ViewModelBase
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

        #region 検索条件[SearchCondition]プロパティ
        /// <summary>
        /// 検索条件[SearchCondition]プロパティ用変数
        /// </summary>
        CvsImageGetConditionM _SearchCondition = new CvsImageGetConditionM();
        /// <summary>
        /// 検索条件[SearchCondition]プロパティ
        /// </summary>
        public CvsImageGetConditionM SearchCondition
        {
            get
            {
                return _SearchCondition;
            }
            set
            {
                if (_SearchCondition == null || !_SearchCondition.Equals(value))
                {
                    _SearchCondition = value;
                    NotifyPropertyChanged("SearchCondition");
                }
            }
        }
        #endregion

        #region イメージモデル[CvsImage]プロパティ
        /// <summary>
        /// イメージモデル[CvsImage]プロパティ用変数
        /// </summary>
        CvsImageExM? _CvsImage = null;
        /// <summary>
        /// イメージモデル[CvsImage]プロパティ
        /// </summary>
        public CvsImageExM? CvsImage
        {
            get
            {
                return _CvsImage;
            }
            set
            {
                if (_CvsImage == null || !_CvsImage.Equals(value))
                {
                    _CvsImage = value;
                    NotifyPropertyChanged("CvsImage");
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
        public override void Init(object sender, EventArgs e)
        {
            try
            {

            }
            catch (Exception ex)
            {
                ShowMessage.ShowErrorOK(ex.Message, "Error");
            }
        }

        public override void Close(object sender, EventArgs e)
        {
            try
            {

            }
            catch (Exception ex)
            {
                ShowMessage.ShowErrorOK(ex.Message, "Error");
            }
        }

        public void Search()
        {
            try
            {

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
        public void Search(object sender, EventArgs ev)
        {
            try
            {
                // GET クエリの実行
                GETQuery(sender, this.SearchCondition.GetConditionQuery);
            }
            catch (Exception e)
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
                    url = CvsImageM.Endpoint + query;
                }
                else
                {
                    // エンドポイント + パラメータ
                    url = query;
                }

                // 実行してJSON形式をデシリアライズ
                var request_model = JSONUtil.DeserializeFromText<CvsImageM>(request = await tmp.Request(url));

                // Nullチェック
                if (request_model != null)
                {
                    this.CvsImage = new CvsImageExM(request_model); // ModelListへ変換
                    this.CvsImage!.Rowdata = request;               // 生データの保持
                    this.CvsImage!.RequestURL = url;               // 生データの保持

                    //var tmp = from x in request_model.Items where x.NsfwLevel
                    //this.FilteredCvsImage = new CvsImageExM();


                    // 1つ以上要素が存在する場合
                    if (this.CvsImage.Items.Count > 0)
                    {
                        // 1つ目の要素をセットする
                        this.CvsImage.Items.SelectedItem = this.CvsImage.Items.ElementAt(0);
                    }

                    ImageChanged();
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

        #region 次のページへ移動
        /// <summary>
        /// 次のページへ移動
        /// </summary>
        public void MoveNext(object sender, EventArgs ev)
        {
            try
            {
                // null check
                if (this.CvsImage != null)
                {
                    // 次のページが最終ページより前である場合
                    if (this.CvsImage.Metadata.CurrentPage + 1 <= CvsImage.Metadata.TotalPages)
                    {
                        // Execute GET Query
                        GETQuery(sender, this.CvsImage.Metadata.NextPage, false);
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
                if (this.CvsImage != null)
                {
                    // 前のページが1より大きい場合
                    if (this.CvsImage.Metadata.CurrentPage - 1 >= 1)
                    {
                        // Execute GET Query
                        GETQuery(sender, this.CvsImage.Metadata.PrevPage, false);
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
        public void ImageChanged()
        {
            try
            {
                // nullチェック
                if (this.CvsImage != null && this.CvsImage.Items != null && this.CvsImage.Items.SelectedItem != null)
                {
                    // イメージをセットする
                    this.ImageList.SetImages(new ObservableCollection<CvsItem>(this.CvsImage.Items.Items));

                    // 最初の行を選択する
                    this.ImageList.SetFirst();

                    // ImageリストのListViewを先頭へスクロールさせる
                    //ListViewTopRow(sender);
                }
            }
            catch (Exception ex)
            {
                ShowMessage.ShowErrorOK(ex.Message, "Error");
            }
        }
        #endregion


        #region JSONリクエストのコピー
        /// <summary>
        /// JSONリクエストのコピー
        /// </summary>
        public void CopyRequest()
        {
            try
            {
                // nullチェック
                if (this.CvsImage != null)
                {
                    // クリップボードにコピー
                    Clipboard.SetText(this.CvsImage.RequestURL);
                }
            }
            catch (Exception ex)
            {
                ShowMessage.ShowErrorOK(ex.Message, "Error");
            }
        }
        #endregion
    }
}