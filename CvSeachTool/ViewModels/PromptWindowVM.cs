using CvSeachTool.Models;
using MVVMCore.BaseClass;
using MVVMCore.Common.Utilities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CvSeachTool.ViewModels
{
    public class PromptWindowVM : ViewModelBase
    {
        #region プロンプト情報[PromptItems]プロパティ
        /// <summary>
        /// プロンプト情報[PromptItems]プロパティ用変数
        /// </summary>
        PromptCountCollectionM? _PromptItems;
        /// <summary>
        /// プロンプト情報[PromptItems]プロパティ
        /// </summary>
        public PromptCountCollectionM? PromptItems
        {
            get
            {
                return _PromptItems;
            }
            set
            {
                if (_PromptItems == null || !_PromptItems.Equals(value))
                {
                    _PromptItems = value;
                    NotifyPropertyChanged("PromptItems");
                }
            }
        }
        #endregion



        public override void Init(object sender, EventArgs e)
        {
            try
            {
                this.PromptItems = new PromptCountCollectionM();
            }
            catch (Exception ex)
            {
                ShowMessage.ShowErrorOK(ex.Message, "Error");
            }
        }

        public override void Close(object sender, EventArgs e)
        {
            try
            {
            }
            catch (Exception ex)
            {
                ShowMessage.ShowErrorOK(ex.Message, "Error");
            }
        }
    }
}
