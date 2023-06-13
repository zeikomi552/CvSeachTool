using CvSeachTool.Common.Enums;
using CvSeachTool.Models.CvsModel;
using MVVMCore.BaseClass;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace CvSeachTool.Models.CvsImage
{
    public class CvsImageM : ModelBase
    {
        #region GET Model Endpoint[Endpoint]プロパティ
        /// <summary>
        /// GET Model Endpoint[Endpoint]プロパティ用変数
        /// </summary>
        public const string Endpoint = "https://civitai.com/api/v1/images";
        #endregion
        public class CvsItems : ModelBase
        {

            #region The id of the image[Id]プロパティ
            /// <summary>
            /// The id of the image[Id]プロパティ用変数
            /// </summary>
            int _Id = 0;
            /// <summary>
            /// The id of the image[Id]プロパティ
            /// </summary>
            [JsonPropertyName("id")]
            public int Id
            {
                get
                {
                    return _Id;
                }
                set
                {
                    if (!_Id.Equals(value))
                    {
                        _Id = value;
                        NotifyPropertyChanged("Id");
                    }
                }
            }
            #endregion
           
            #region The url of the image at it's source resolution[Url]プロパティ
            /// <summary>
            /// The url of the image at it's source resolution[Url]プロパティ用変数
            /// </summary>
            string _Url = string.Empty;
            /// <summary>
            /// The url of the image at it's source resolution[Url]プロパティ
            /// </summary>
            [JsonPropertyName("url")]
            public string Url
            {
                get
                {
                    return _Url;
                }
                set
                {
                    if (_Url == null || !_Url.Equals(value))
                    {
                        _Url = value;
                        NotifyPropertyChanged("Url");
                    }
                }
            }
            #endregion

            #region The blurhash of the image[Hash]プロパティ
            /// <summary>
            /// The blurhash of the image[Hash]プロパティ用変数
            /// </summary>
            string _Hash = string.Empty;
            /// <summary>
            /// The blurhash of the image[Hash]プロパティ
            /// </summary>
            [JsonPropertyName("hash")]
            public string Hash
            {
                get
                {
                    return _Hash;
                }
                set
                {
                    if (_Hash == null || !_Hash.Equals(value))
                    {
                        _Hash = value;
                        NotifyPropertyChanged("Hash");
                    }
                }
            }
            #endregion

            #region The blurhash of the image[Width]プロパティ
            /// <summary>
            /// The blurhash of the image[Width]プロパティ用変数
            /// </summary>
            int _Width = 0;
            /// <summary>
            /// The blurhash of the image[Width]プロパティ
            /// </summary>
            [JsonPropertyName("width")]
            public int Width
            {
                get
                {
                    return _Width;
                }
                set
                {
                    if (!_Width.Equals(value))
                    {
                        _Width = value;
                        NotifyPropertyChanged("Width");
                    }
                }
            }
            #endregion

            #region  The height of the image[Height]プロパティ
            /// <summary>
            ///  The height of the image[Height]プロパティ用変数
            /// </summary>
            int _Height = 0;
            /// <summary>
            ///  The height of the image[Height]プロパティ
            /// </summary>
            [JsonPropertyName("height")]
            public int Height
            {
                get
                {
                    return _Height;
                }
                set
                {
                    if (!_Height.Equals(value))
                    {
                        _Height = value;
                        NotifyPropertyChanged("Height");
                    }
                }
            }
            #endregion

            #region If the image has any mature content labels[Nsfw]プロパティ
            /// <summary>
            /// If the image has any mature content labels[Nsfw]プロパティ用変数
            /// </summary>
            bool _Nsfw = false;
            /// <summary>
            /// If the image has any mature content labels[Nsfw]プロパティ
            /// </summary>
            [JsonPropertyName("nsfw")]
            public bool Nsfw
            {
                get
                {
                    return _Nsfw;
                }
                set
                {
                    if (!_Nsfw.Equals(value))
                    {
                        _Nsfw = value;
                        NotifyPropertyChanged("Nsfw");
                    }
                }
            }
            #endregion

            #region The NSFW level of the image[NsfwLevel]プロパティ
            /// <summary>
            /// The NSFW level of the image[NsfwLevel]プロパティ用変数
            /// </summary>
            string _NsfwLevel = string.Empty;
            /// <summary>
            /// The NSFW level of the image[NsfwLevel]プロパティ
            /// </summary>
            [JsonPropertyName("nsfwLevel")]
            public string NsfwLevel
            {
                get
                {
                    return _NsfwLevel;
                }
                set
                {
                    if (_NsfwLevel == null || !_NsfwLevel.Equals(value))
                    {
                        _NsfwLevel = value;
                        NotifyPropertyChanged("NsfwLevel");
                    }
                }
            }
            #endregion

            #region The date the image was posted[CreatedAt]プロパティ
            /// <summary>
            /// The date the image was posted[CreatedAt]プロパティ用変数
            /// </summary>
            DateTime _CreatedAt = DateTime.MinValue;
            /// <summary>
            /// The date the image was posted[CreatedAt]プロパティ
            /// </summary>
            [JsonPropertyName("createdAt")]
            public DateTime CreatedAt
            {
                get
                {
                    return _CreatedAt;
                }
                set
                {
                    if (!_CreatedAt.Equals(value))
                    {
                        _CreatedAt = value;
                        NotifyPropertyChanged("CreatedAt");
                    }
                }
            }
            #endregion

            #region The ID of the post the image belongs to[PostId]プロパティ
            /// <summary>
            /// The ID of the post the image belongs to[PostId]プロパティ用変数
            /// </summary>
            int _PostId = 0;
            /// <summary>
            /// The ID of the post the image belongs to[PostId]プロパティ
            /// </summary>
            [JsonPropertyName("postId")]
            public int PostId
            {
                get
                {
                    return _PostId;
                }
                set
                {
                    if (!_PostId.Equals(value))
                    {
                        _PostId = value;
                        NotifyPropertyChanged("PostId");
                    }
                }
            }
            #endregion

            //#region The ID of the post the image belongs to[Meta]プロパティ
            ///// <summary>
            ///// The ID of the post the image belongs to[Meta]プロパティ用変数
            ///// </summary>
            //object _Meta = new object();
            ///// <summary>
            ///// The ID of the post the image belongs to[Meta]プロパティ
            ///// </summary>
            //[JsonPropertyName("meta")]
            //public object Meta
            //{
            //    get
            //    {
            //        return _Meta;
            //    }
            //    set
            //    {
            //        if (_Meta == null || !_Meta.Equals(value))
            //        {
            //            _Meta = value;
            //            NotifyPropertyChanged("Meta");
            //        }
            //    }
            //}
            //#endregion

            #region The username of the creator[Username]プロパティ
            /// <summary>
            /// The username of the creator[Username]プロパティ用変数
            /// </summary>
            string _Username = string.Empty;
            /// <summary>
            /// The username of the creator[Username]プロパティ
            /// </summary>
            [JsonPropertyName("username")]
            public string Username
            {
                get
                {
                    return _Username;
                }
                set
                {
                    if (_Username == null || !_Username.Equals(value))
                    {
                        _Username = value;
                        NotifyPropertyChanged("Username");
                    }
                }
            }
            #endregion
        }

        #region Element of Image Items[Items]プロパティ
        /// <summary>
        /// Element of Image Items[Items]プロパティ用変数
        /// </summary>
        ObservableCollection<CvsItems> _Items = new ObservableCollection<CvsItems>();
        /// <summary>
        /// Element of Image Items[Items]プロパティ
        /// </summary>
        [JsonPropertyName("items")]
        public ObservableCollection<CvsItems> Items
        {
            get
            {
                return _Items;
            }
            set
            {
                if (_Items == null || !_Items.Equals(value))
                {
                    _Items = value;
                    NotifyPropertyChanged("Items");
                }
            }
        }
        #endregion


        #region Metadata[Metadata]プロパティ
        /// <summary>
        /// Metadata[Metadata]プロパティ用変数
        /// </summary>
        CvsMetadataM _Metadata = new CvsMetadataM();
        /// <summary>
        /// Metadata[Metadata]プロパティ
        /// </summary>
        [JsonPropertyName("metadata")]
        public CvsMetadataM Metadata
        {
            get
            {
                return _Metadata;
            }
            set
            {
                if (_Metadata == null || !_Metadata.Equals(value))
                {
                    _Metadata = value;
                    NotifyPropertyChanged("Metadata");
                }
            }
        }
        #endregion
    }
}
