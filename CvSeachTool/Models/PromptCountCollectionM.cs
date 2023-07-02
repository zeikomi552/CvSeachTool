using CvSeachTool.Models.CvsImage;
using MVVMCore.BaseClass;
using MVVMCore.Common.Utilities;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CvSeachTool.Models
{
    public class PromptCountCollectionM : ModelBase
    {
        public PromptCountCollectionM()
        {
            
        }

        #region プロンプト要素[PromptItems]プロパティ
        /// <summary>
        /// プロンプト要素[PromptItems]プロパティ用変数
        /// </summary>
        ModelList<PromptCountM> _PromptItems = new ModelList<PromptCountM>();
        /// <summary>
        /// プロンプト要素[PromptItems]プロパティ
        /// </summary>
        public ModelList<PromptCountM> PromptItems
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

        public void CreatePromptItems(CvsImageExM cvsimg)
        {
            Dictionary<string, int> prompt_dic = new Dictionary<string, int>();
            foreach (var item in cvsimg.Items)
            {
                if (item.Meta == null)
                    continue;

                string prompt = item.Meta.Prompt.Replace(">", ">,");
                string[] prompt_list = prompt.Split(",");

                foreach (var pitem in prompt_list)
                {
                    string key = pitem.Trim().Replace("(", "").Replace(")","").ToLower();
                    if (string.IsNullOrEmpty(key))
                    {
                        continue;
                    }

                    if (prompt_dic.ContainsKey(key))
                    {
                        prompt_dic[key]++;
                    }
                    else
                    {
                        prompt_dic.Add(key, 1);
                    }
                }
            }
            var tmp = (from x in prompt_dic
                       select new PromptCountM { Prompt = x.Key, Count = x.Value }).OrderByDescending(x => x.Count).ThenBy(x=>x.Prompt).ToList();

            this.PromptItems.Items = new ObservableCollection<PromptCountM>(tmp);
        }
    }
}
