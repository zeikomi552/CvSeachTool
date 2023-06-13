using CvSeachTool.Common.Converters;
using MVVMCore.BaseClass;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using static CvSeachTool.Models.CvsModel.CvsModelM.CvsModelVersions;

namespace CvSeachTool.Models
{
    public class DisplayImageM : ModelBase
    {
        #region This model images[Images]プロパティ
        /// <summary>
        /// This model images[Images]プロパティ用変数
        /// </summary>
        ObservableCollection<CvsImages> _Images = new ObservableCollection<CvsImages>();
        /// <summary>
        /// This model images[Images]プロパティ
        /// </summary>
        private ObservableCollection<CvsImages> Images
        {
            get
            {
                return _Images;
            }
            set
            {
                if (_Images == null || !_Images.Equals(value))
                {
                    _Images = value;
                    NotifyPropertyChanged("Images");
                }
            }
        }
        #endregion

        #region This model Filtered images[FilteredImages]プロパティ
        /// <summary>
        /// This model Filtered images[FilteredImages]プロパティ用変数
        /// </summary>
        ObservableCollection<CvsImages> _FilteredImages = new ObservableCollection<CvsImages>();
        /// <summary>
        /// This model Filtered images[FilteredImages]プロパティ
        /// </summary>
        public ObservableCollection<CvsImages> FilteredImages
        {
            get
            {
                return _FilteredImages;
            }
            set
            {
                if (_FilteredImages == null || !_FilteredImages.Equals(value))
                {
                    _FilteredImages = value;
                    NotifyPropertyChanged("FilteredImages");
                }
            }
        }
        #endregion

        #region Selected image[SelectedImage]プロパティ
        /// <summary>
        /// Selected image[SelectedImage]プロパティ用変数
        /// </summary>
        CvsImages _SelectedImage = new CvsImages();
        /// <summary>
        /// Selected image[SelectedImage]プロパティ
        /// </summary>
        public CvsImages SelectedImage
        {
            get
            {
                return _SelectedImage;
            }
            set
            {
                if (_SelectedImage == null || !_SelectedImage.Equals(value))
                {
                    _SelectedImage = value;
                    NotifyPropertyChanged("SelectedImage");
                }
            }
        }
        #endregion

        #region イメージのリストをセットする
        /// <summary>
        /// イメージのリストをセットする
        /// </summary>
        /// <param name="images">イメージリスト</param>
        public void SetImages(ObservableCollection<CvsImages> images)
        {
            this.Images = images;   // イメージのセット

            // フィルタのリフレッシュ
            RefreshFilter();
        }
        #endregion

        #region イメージフィルターのリフレッシュ
        /// <summary>
        /// イメージフィルターのリフレッシュ
        /// </summary>
        public void RefreshFilter()
        {
            var tmp = (from x in this.Images
                       where ImageNsfwEnumToVisibilityConverter.Convert(x.Nsfw)
                       select x).ToList<CvsImages>();

            this.FilteredImages = new ObservableCollection<CvsImages>(tmp);

        }
        #endregion

        #region 1つ以上あるかどうかを判定する
        /// <summary>
        /// 1つ以上あるかどうかを判定する
        /// </summary>
        public bool Any
        {
            get
            {
                return this.FilteredImages.Any();
            }
        }
        #endregion

        #region 最初のアイテムを選択する
        /// <summary>
        /// 最初のアイテムを選択する
        /// </summary>
        public void SetFirst()
        {
            if (this.FilteredImages.Count > 0)
            {
                this.SelectedImage = this.FilteredImages.ElementAt(0);
            }
        }
        #endregion
    }
}
