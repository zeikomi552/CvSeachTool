using CvSeachTool.Common;
using MVVMCore.BaseClass;
using MVVMCore.Common.Utilities;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static CvSeachTool.Models.CvsModelM;

namespace CvSeachTool.Models
{
    public class ConfigM : ModelBase
    {
        #region ブックマークのフォルダパス[BookmarkDir]プロパティ
        /// <summary>
        /// ブックマークのフォルダパス[BookmarkDir]プロパティ用変数
        /// </summary>
        string _BookmarkDir = @"Conf\Bookmark";
        /// <summary>
        /// ブックマークのフォルダパス[BookmarkDir]プロパティ
        /// </summary>
        public string BookmarkDir
        {
            get
            {
                return _BookmarkDir;
            }
            set
            {
                if (_BookmarkDir == null || !_BookmarkDir.Equals(value))
                {
                    _BookmarkDir = value;
                    NotifyPropertyChanged("BookmarkDir");
                }
            }
        }
        #endregion

        #region ブックマークのファイルパス[BookmarkFile]プロパティ
        /// <summary>
        /// ブックマークのファイルパス[BookmarkFile]プロパティ用変数
        /// </summary>
        string _BookmarkFile = "bookmark.conf";
        /// <summary>
        /// ブックマークのファイルパス[BookmarkFile]プロパティ
        /// </summary>
        public string BookmarkFile
        {
            get
            {
                return _BookmarkFile;
            }
            set
            {
                if (_BookmarkFile == null || !_BookmarkFile.Equals(value))
                {
                    _BookmarkFile = value;
                    NotifyPropertyChanged("BookmarkFile");
                }
            }
        }
        #endregion


        #region コンフィグファイルの初期化処理
        /// <summary>
        /// コンフィグファイルの初期化処理
        /// </summary>
        public void InitConfig()
        {
            // コンフィグファイルの読み込み
            GblValues.Instance.Config!.LoadXML();

            // ブックマーク情報をJSON形式で読み込み
            GblValues.Instance.BookmarkConf!.LoadJSON();
        }
        #endregion
    }
}
