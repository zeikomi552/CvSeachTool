using CvSeachTool.Common.Utilities;
using CvSeachTool.Models;
using MVVMCore.BaseClass;
using MVVMCore.Common.Utilities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Http.Json;
using System.Text;
using System.Threading.Tasks;

namespace CvSeachTool.ViewModels
{
    public class MainWindowVM : ViewModelBase
    {
        #region stablediffusion model object[CvsModel]プロパティ
        /// <summary>
        /// stablediffusion model object[CvsModel]プロパティ用変数
        /// </summary>
        CvsModelM? _CvsModel = new CvsModelM();
        /// <summary>
        /// stablediffusion model object[CvsModel]プロパティ
        /// </summary>
        public CvsModelM? CvsModel
        {
            get
            {
                return _CvsModel;
            }
            set
            {
                if (_CvsModel == null || !_CvsModel.Equals(value))
                {
                    _CvsModel = value;
                    NotifyPropertyChanged("CvsModel");
                }
            }
        }
        #endregion

        /// <summary>
        /// 画面初期化処理
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        public override async void Init(object sender, EventArgs e)
        {
            GetModelReqestM tmp = new GetModelReqestM();
            string request = string.Empty;
            this.CvsModel = JsonExtensions.DeserializeFromFile<CvsModelM>(request = await tmp.Request(100));
            this.CvsModel!.Rowdata = request;
        }

        public override void Close(object sender, EventArgs e)
        {

        }


    }
}
