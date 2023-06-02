using MVVMCore.BaseClass;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CvSeachTool.Models
{
    public class BookmarkM : ModelBase
    {
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
                return Path.GetFileName(this.BookmarkFilePath);
            }

        }
        #endregion
    }
}
