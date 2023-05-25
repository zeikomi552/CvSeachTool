using MVVMCore.BaseClass;
using MVVMCore.Common.Utilities;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;
using static CvSeachTool.Models.CvsModelM;

namespace CvSeachTool.Models
{
    public class CvsModelExM : ModelBase
    {
        public CvsModelExM(CvsModelM model)
        {
            // set request items
            this.Items = new ModelList<CvsItems>(model.Items);

            // set request metadata
            this.Metadata = model.Metadata;
        }

        #region json result of items[Items]プロパティ
        /// <summary>
        /// json result of items[Items]プロパティ用変数
        /// </summary>
        ModelList<CvsItems> _Items = new ModelList<CvsItems>();
        /// <summary>
        /// json result of items[Items]プロパティ
        /// </summary>
        [JsonPropertyName("items")]
        public ModelList<CvsItems> Items
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

        #region json row data[Rowdata]プロパティ
        /// <summary>
        /// json row data[Rowdata]プロパティ用変数
        /// </summary>
        string _Rowdata = string.Empty;
        /// <summary>
        /// json row data[Rowdata]プロパティ
        /// </summary>
        public string Rowdata
        {
            get
            {
                return _Rowdata;
            }
            set
            {
                if (_Rowdata == null || !_Rowdata.Equals(value))
                {
                    _Rowdata = value;
                    NotifyPropertyChanged("Rowdata");
                }
            }
        }
        #endregion

        #region request url[RequestURL]プロパティ
        /// <summary>
        /// request url[RequestURL]プロパティ用変数
        /// </summary>
        string _RequestURL = string.Empty;
        /// <summary>
        /// request url[RequestURL]プロパティ
        /// </summary>
        public string RequestURL
        {
            get
            {
                return _RequestURL;
            }
            set
            {
                if (_RequestURL == null || !_RequestURL.Equals(value))
                {
                    _RequestURL = value;
                    NotifyPropertyChanged("RequestURL");
                }
            }
        }
        #endregion


    }
}
