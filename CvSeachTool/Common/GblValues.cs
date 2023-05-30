using CvSeachTool.Common.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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


    }
}
