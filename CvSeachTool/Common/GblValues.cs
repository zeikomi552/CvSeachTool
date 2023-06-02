using CvSeachTool.Common.Enums;
using CvSeachTool.Models;
using MVVMCore.Common.Utilities;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static CvSeachTool.Models.CvsModelM;

namespace CvSeachTool.Common
{
    public sealed class GblValues
    {
        private static GblValues _Instance = new GblValues();

        private GblValues()
        {
        }

        public static GblValues Instance
        {
            get
            {
                return _Instance;
            }
        }

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

        #region コンフィグファイルの初期化処理
        /// <summary>
        /// コンフィグファイルの初期化処理
        /// </summary>
        public void InitConfig()
        {
            // コンフィグファイルの作成
            this.Config = new ConfigManager<ConfigM>("conf", "CvSearchTool.conf", new ConfigM());

            // コンフィグファイルの読み込み
            this.Config.LoadXML();

            if (File.Exists(this.Config.Item.BookmarkFile))
            {
                // ブックマーク情報の作成
                this.BookmarkConf = new ConfigManager<ModelList<CvsItems>>(this.Config.Item.BookmarkDir, this.Config.Item.BookmarkFile, new ModelList<CvsItems>());
            }
            else
            {
                // ブックマーク情報の作成
                this.BookmarkConf = new ConfigManager<ModelList<CvsItems>>(@"conf\bookmark", @"bookmark.conf", new ModelList<CvsItems>());
            }

            // ブックマーク情報をJSON形式で読み込み
            this.BookmarkConf!.LoadJSON();
        }
        #endregion
    }
}
