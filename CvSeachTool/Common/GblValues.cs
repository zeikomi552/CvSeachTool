using CvSeachTool.Common.Enums;
using CvSeachTool.Models;
using MVVMCore.Common.Utilities;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static CvSeachTool.Models.CvsModel.CvsModelM;

namespace CvSeachTool.Common
{
    public sealed class GblValues
    {
        private static GblValues _Instance = new GblValues();

        #region コンストラクタ
        /// <summary>
        /// コンストラクタ
        /// </summary>
        private GblValues()
        {
            // コンフィグファイルの初期化処理
            ConfigInit();
        }
        #endregion

        #region インスタンス
        /// <summary>
        /// インスタンス
        /// </summary>
        public static GblValues Instance
        {
            get
            {
                return _Instance;
            }
        }
        #endregion

                #region お気に入りを保存しているフォルダ
        /// <summary>
        /// お気に入りを保存しているフォルダ
        /// </summary>
        public string BookmarkDir
        {
            get
            {
                return Path.Combine(PathManager.GetApplicationFolder(), this.Config!.Item.BookmarkDir);
            }
        }
        #endregion

        #region イメージフィルタ用[ImageFilter]プロパティ
        /// <summary>
        /// イメージフィルタ用[ImageFilter]プロパティ用変数
        /// </summary>
        ImageNsfwEnum _ImageFilter = ImageNsfwEnum.None;
        /// <summary>
        /// イメージフィルタ用[ImageFilter]プロパティ
        /// </summary>
        public ImageNsfwEnum ImageFilter
        {
            get
            {
                return _ImageFilter;
            }
            set
            {
                if (!_ImageFilter.Equals(value))
                {
                    _ImageFilter = value;
                }
            }
        }
        #endregion

        #region ブックマーク[BookmarkConf]プロパティ
        /// <summary>
        /// ブックマーク[BookmarkConf]プロパティ用変数
        /// </summary>
        ConfigManager<ModelList<CvsItems>>? _BookmarkConf;
        /// <summary>
        /// ブックマーク[BookmarkConf]プロパティ
        /// </summary>
        public ConfigManager<ModelList<CvsItems>>? BookmarkConf
        {
            get
            {
                return _BookmarkConf;
            }
            set
            {
                if (_BookmarkConf == null || !_BookmarkConf.Equals(value))
                {
                    _BookmarkConf = value;
                }
            }
        }
        #endregion

        #region Configファイルオブジェクト[Config]プロパティ
        /// <summary>
        /// Configファイルオブジェクト[Config]プロパティ用変数
        /// </summary>
        ConfigManager<ConfigM>? _Config;
        /// <summary>
        /// Configファイルオブジェクト[Config]プロパティ
        /// </summary>
        public ConfigManager<ConfigM>? Config
        {
            get
            {
                return _Config;
            }
            set
            {
                if (_Config == null || !_Config.Equals(value))
                {
                    _Config = value;
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
                }
            }
        }
        #endregion

        #region コンフィグファイルの初期化処理
        /// <summary>
        /// コンフィグファイルの初期化処理
        /// </summary>
        public void ConfigInit()
        {
            // コンフィグファイルの作成
            this.Config = new ConfigManager<ConfigM>("conf", "CvSearchTool.conf", new ConfigM());

            // コンフィグファイルの読み込み
            this.Config.LoadXML();

            // ブックマークファイルの存在確認
            if (File.Exists(Path.Combine(PathManager.GetApplicationFolder(), this.Config.Item.BookmarkDir, this.Config.Item.BookmarkFile)))
            {
                // ブックマーク情報の作成
                this.BookmarkConf = new ConfigManager<ModelList<CvsItems>>(this.Config.Item.BookmarkDir, this.Config.Item.BookmarkFile, new ModelList<CvsItems>());
            }
            else
            {
                // ブックマーク情報の作成
                this.BookmarkConf = new ConfigManager<ModelList<CvsItems>>(@"conf\bookmark", @"bookmark.conf", new ModelList<CvsItems>());
            }

            // ブックマークファイルの読み込み
            this.BookmarkConf.LoadJSON();
        }
        #endregion


    }
}
