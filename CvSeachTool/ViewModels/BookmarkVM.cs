using CvSeachTool.Common;
using CvSeachTool.Models;
using CvSeachTool.Views;
using MVVMCore.BaseClass;
using MVVMCore.Common.Utilities;
using MVVMCore.Common.Wrapper;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static CvSeachTool.Models.CvsModelM;
using static CvSeachTool.Models.CvsModelM.CvsModelVersions;

namespace CvSeachTool.ViewModels
{
    public class BookmarkVM : ViewModelBase
    {

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

        #region ブックマークリスト[BookmarkList]プロパティ
        /// <summary>
        /// ブックマークリスト[BookmarkList]プロパティ用変数
        /// </summary>
        ModelList<BookmarkM> _BookmarkList = new ModelList<BookmarkM>();
        /// <summary>
        /// ブックマークリスト[BookmarkList]プロパティ
        /// </summary>
        public ModelList<BookmarkM> BookmarkList
        {
            get
            {
                return _BookmarkList;
            }
            set
            {
                if (_BookmarkList == null || !_BookmarkList.Equals(value))
                {
                    _BookmarkList = value;
                    NotifyPropertyChanged("BookmarkList");
                }
            }
        }
        #endregion

        #region 初期化処理
        /// <summary>
        /// 初期化処理
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="ev"></param>
        public override void Init(object sender, EventArgs ev)
        {
            try
            {
                InitBookmarkList();
            }
            catch (Exception e)
            {
                ShowMessage.ShowErrorOK(e.Message, "Error");
            }
        }
        #endregion

        #region ブックマークリストの初期化処理
        /// <summary>
        /// ブックマークリストの初期化処理
        /// </summary>
        private void InitBookmarkList()
        {
            // フォルダのパス
            var dirPath = Path.Combine(PathManager.GetApplicationFolder(), this.Config!.Item.BookmarkDir);

            List<BookmarkM> list = new List<BookmarkM>();
            // フォルダ内のファイル一覧を取得
            var fileArray = Directory.GetFiles(dirPath);
            foreach (string file in fileArray)
            {
                list.Add(new BookmarkM() { BookmarkFilePath = file });
            }

            // ブックマークリストのセット
            this.BookmarkList.Items = new ObservableCollection<BookmarkM>(list);

            // 最初の要素を選択
            this.BookmarkList.SelectedFirst();
        }
        #endregion

        #region ブックマークの選択変更
        /// <summary>
        /// ブックマークの選択変更
        /// </summary>
        public void BookmarkSelectionChanged()
        {
            try
            {

            }
            catch (Exception e)
            {
                ShowMessage.ShowErrorOK(e.Message, "Error");
            }
        }
        #endregion

        #region クローズ処理
        /// <summary>
        /// クローズ処理
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="ev"></param>
        public override void Close(object sender, EventArgs ev)
        {
            try
            {
                this.BookmarkConf!.SaveJSON();
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
                if (this.BookmarkConf != null && this.BookmarkConf.Item != null && this.BookmarkConf.Item.SelectedItem != null)
                {

                    List<CvsImages> tmp_img = new List<CvsImages>();

                    // モデルバージョン分イメージをリストにセット
                    foreach (var modelver in this.BookmarkConf.Item.SelectedItem.ModelVersions)
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

        #region ListViewを先頭へスクロールさせる処理
        /// <summary>
        /// DataGridを先頭へスクロールさせる処理
        /// </summary>
        /// <param name="sender">画面内のコントロールオブジェクト</param>
        private void ListViewTopRow(object sender)
        {
            // ウィンドウの取得
            var wnd = (BookmarkV)VisualTreeHelperWrapper.GetWindow<BookmarkV>(sender);

            // イメージのListViewのスクロールバーを先頭へ移動
            ScrollbarTopRow.TopRow4ListView(wnd.lvImages);
        }
        #endregion

        #region Bookmarkの削除
        /// <summary>
        /// Bookmarkの削除
        /// </summary>
        public void DeleteBookmark()
        {
            try
            {
                // 選択行の削除処理
                this.BookmarkConf!.Item.SelectedItemDelete();
            }
            catch (Exception ex)
            {
                ShowMessage.ShowErrorOK(ex.Message, "Error");
            }
        }
        #endregion
    }
}
