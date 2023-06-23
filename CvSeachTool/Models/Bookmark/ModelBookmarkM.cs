using CvSeachTool.Common.Utilities;
using Microsoft.Win32;
using MVVMCore.BaseClass;
using MVVMCore.Common.Utilities;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static CvSeachTool.Models.CvsModel.CvsModelM;

namespace CvSeachTool.Models.Bookmark
{
    public class ModelBookmarkM : BookmarkBaseM
    {
        public static string DefaultBookmarkFile { get; set; } = "bookmark.conf";

        #region モデル用ブックマークのフォルダパス[ModelBookmarkDir]プロパティ
        /// <summary>
        /// モデル用ックマークのフォルダパス[ModelBookmarkDir]プロパティ
        /// </summary>
        public string ModelBookmarkDir
        {
            get
            {
                return BookmarkDir;
            }
            set
            {
                if (BookmarkDir == null || !BookmarkDir.Equals(value))
                {
                    BookmarkDir = value;
                    NotifyPropertyChanged("ModelBookmarkDir");
                }
            }
        }
        #endregion

        #region モデル用ブックマークのファイルパス[ModelBookmarkFile]プロパティ
        /// <summary>
        /// モデル用ブックマークのファイルパス[ModelBookmarkFile]プロパティ用変数
        /// </summary>
        string _ModelBookmarkFile = DefaultBookmarkFile;
        /// <summary>
        /// モデル用ブックマークのファイルパス[ModelBookmarkFile]プロパティ
        /// </summary>
        public string ModelBookmarkFile
        {
            get
            {
                return _ModelBookmarkFile;
            }
            set
            {
                if (_ModelBookmarkFile == null || !_ModelBookmarkFile.Equals(value))
                {
                    _ModelBookmarkFile = value;
                    NotifyPropertyChanged("ModelBookmarkFile");
                }
            }
        }
        #endregion

        #region ブックマーク[ModelBookmarkConf]プロパティ
        /// <summary>
        /// ブックマーク[ModelBookmarkConf]プロパティ用変数
        /// </summary>
        ConfigManager<ModelList<CvsItem>>? _ModelBookmarkConf;
        /// <summary>
        /// ブックマーク[ModelBookmarkConf]プロパティ
        /// </summary>
        public ConfigManager<ModelList<CvsItem>>? ModelBookmarkConf
        {
            get
            {
                return _ModelBookmarkConf;
            }
            set
            {
                if (_ModelBookmarkConf == null || !_ModelBookmarkConf.Equals(value))
                {
                    _ModelBookmarkConf = value;
                }
            }
        }
        #endregion


        #region ブックマークの初期化処理
        /// <summary>
        /// ブックマークの初期化処理
        /// 存在する場合は読み込み、存在しない場合は空のファイルを作成する
        /// </summary>
        public void InitBookmark()
        {

            // ブックマークファイルの存在確認
            if (File.Exists(Path.Combine(PathManager.GetApplicationFolder(), this.ModelBookmarkDir, this.ModelBookmarkFile)))
            {
                // ブックマーク情報の作成
                this.ModelBookmarkConf = new ConfigManager<ModelList<CvsItem>>(this.ModelBookmarkDir, this.ModelBookmarkFile, new ModelList<CvsItem>());
            }
            else
            {
                // ブックマーク情報の作成
                this.ModelBookmarkConf = new ConfigManager<ModelList<CvsItem>>(ModelBookmarkM.BookmarkDir,
                    ModelBookmarkM.DefaultBookmarkFile, new ModelList<CvsItem>());
            }

            // ブックマークファイルの読み込み
            this.ModelBookmarkConf.LoadJSON();
        }
        #endregion
    }
}
