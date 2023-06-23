using CvSeachTool.Models.Config;
using MVVMCore.BaseClass;
using MVVMCore.Common.Utilities;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CvSeachTool.Models.Bookmark
{
    public class BookmarkBaseM : ModelBase
    {
        /// <summary>
        /// ブックマーク用保存用ディレクトリ
        /// </summary>
        public static string BookmarkDir { get; set; } = string.Format(@"{0}\bookmark", ConfigM.CurrDir);

        #region お気に入りを保存しているフォルダ
        /// <summary>
        /// お気に入りを保存しているフォルダ
        /// </summary>
        public string BookmarkDirFullPath
        {
            get
            {
                return Path.Combine(PathManager.GetApplicationFolder(), BookmarkDir);
            }
        }
        #endregion

        #region ブックマークのファイルパス[BookmarkFilePath]プロパティ
        /// <summary>
        /// ブックマークのファイルパス[BookmarkFilePath]プロパティ用変数
        /// </summary>
        string _BookmarkFilePath = string.Empty;
        /// <summary>
        /// ブックマークのファイルパス[BookmarkFilePath]プロパティ
        /// </summary>
        public string BookmarkFilePath
        {
            get
            {
                return _BookmarkFilePath;
            }
            set
            {
                if (_BookmarkFilePath == null || !_BookmarkFilePath.Equals(value))
                {
                    _BookmarkFilePath = value;
                    NotifyPropertyChanged("BookmarkFilePath");
                    NotifyPropertyChanged("BookmarkFile");
                }
            }
        }
        #endregion

        #region ブックマークファイル名[BookmarkFile]プロパティ
        /// <summary>
        /// ブックマークファイル名[BookmarkFile]プロパティ
        /// </summary>
        public string BookmarkFile
        {
            get
            {
                return Path.GetFileName(BookmarkFilePath);
            }
        }
        #endregion

    }
}
