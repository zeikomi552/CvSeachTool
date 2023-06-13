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

namespace CvSeachTool.ViewModels
{
    public class UcSearchImageVM : ViewModelBase
    {
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

                    // 1つ以上要素が存在する場合
                    if (this.CvsImage.Items.Count > 0)
                    {
                        // 1つ目の要素をセットする
                        this.CvsImage.Items.SelectedItem = this.CvsImage.Items.ElementAt(0);

                        // DataGridを先頭へスクロールさせる
                        //DataGridTopRow(sender);
                    }

                    //// ブックマーク登録されている場合はブックマーク情報をセットする
                    //if (this.BookmarkConf != null && this.BookmarkConf.Item != null && this.BookmarkConf.Item.Items != null)
                    //{
                    //    // モデル全数分回す
                    //    foreach (var cvitem in this.CvsModel.Items)
                    //    {
                    //        // ブックマークに登録されているIDならセット
                    //        cvitem.IsBookmark = (from x in this.BookmarkConf.Item.Items
                    //                             where x.Id.Equals(cvitem.Id)
                    //                             select x).Any();
                    //    }
                    //}

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
    }
}
