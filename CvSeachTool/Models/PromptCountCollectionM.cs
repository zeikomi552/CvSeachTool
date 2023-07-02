using CvSeachTool.Models.CvsImage;
using CvSeachTool.Models.CvsModel;
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

        #region ネガティブプロンプト情報[NegativePromptItems]プロパティ
        /// <summary>
        /// ネガティブプロンプト情報[NegativePromptItems]プロパティ用変数
        /// </summary>
        ModelList<PromptCountM> _NegativePromptItems = new ModelList<PromptCountM>();
        /// <summary>
        /// ネガティブプロンプト情報[NegativePromptItems]プロパティ
        /// </summary>
        public ModelList<PromptCountM> NegativePromptItems
        {
            get
            {
                return _NegativePromptItems;
            }
            set
            {
                if (_NegativePromptItems == null || !_NegativePromptItems.Equals(value))
                {
                    _NegativePromptItems = value;
                    NotifyPropertyChanged("NegativePromptItems");
                }
            }
        }
        #endregion

        #region プロンプトリストの初期化
        /// <summary>
        /// プロンプトリストの初期化
        /// </summary>
        /// <param name="cvsimg">CIVITAI用イメージ要素</param>
        public void InitItems(CvsImageExM cvsimg)
        {
            // プロンプトリストの初期化
            CreatePromptItems(cvsimg, false);

            // ネガティブプロンプトリストの初期化
            CreatePromptItems(cvsimg, true);
        }
        #endregion

        #region プロンプトリストの初期化
        /// <summary>
        /// プロンプトリストの初期化
        /// </summary>
        /// <param name="cvsimg">CIVITAI用イメージ要素</param>
        public void InitItems(CvsImage.DisplayImageM cvsimg)
        {
            // プロンプトリストの初期化
            CreatePromptItems(cvsimg, false);

            // ネガティブプロンプトリストの初期化
            CreatePromptItems(cvsimg, true);

        }
        #endregion

        #region プロンプトリストの初期化
        /// <summary>
        /// プロンプトリストの初期化
        /// </summary>
        /// <param name="cvsimg">CIVITAI用イメージ要素</param>
        public void InitItems(CvsModelExM cvsimg)
        {
            // プロンプトリストの初期化
            CreatePromptItems(cvsimg, false);

            // ネガティブプロンプトリストの初期化
            CreatePromptItems(cvsimg, true);
        }
        #endregion

        #region Promptリストの初期化
        /// <summary>
        /// Promptリストの初期化
        /// </summary>
        /// <param name="cvsimg">Civitai用イメージ</param>
        /// <param name="negative_prompt_f">false:Promptのセット true:ネガティブプロンプトのセット</param>
        public void CreatePromptItems(CvsImageExM cvsimg, bool negative_prompt_f = false)
        {
            // ディクショナリで管理
            Dictionary<string, int> prompt_dic = new Dictionary<string, int>();

            // イメージを一通り回す
            foreach (var item in cvsimg.Items)
            {
                // メタ情報のnullチェック
                if (item.Meta == null)
                    continue;

                // >の後は何故か,で区切られてないことが多いので,を追加
                string prompt = negative_prompt_f ? item.Meta.NegativePrompt.Replace(">", ">,") : item.Meta.Prompt.Replace(">", ">,");

                // 分割
                string[] prompt_list = prompt.Split(",");

                // プロンプトリストを回す
                foreach (var pitem in prompt_list)
                {
                    // (や)を外す
                    string key = pitem.Trim().Replace("(", "").Replace(")","").ToLower();

                    // プロンプトが登録されていない場合無視
                    if (string.IsNullOrEmpty(key))
                    {
                        continue;
                    }

                    // 既に登録済みかを確認
                    if (prompt_dic.ContainsKey(key))
                    {
                        prompt_dic[key]++;  // カウントアップ
                    }
                    else
                    {
                        prompt_dic.Add(key, 1); // 初期登録
                    }
                }
            }

            // ソート
            var tmp = (from x in prompt_dic
                       select new PromptCountM { Prompt = x.Key, Count = x.Value }).OrderByDescending(x => x.Count).ThenBy(x=>x.Prompt).ToList();

            if (negative_prompt_f)
            {
                // 要素にセット
                this.NegativePromptItems.Items = new ObservableCollection<PromptCountM>(tmp);
            }
            else
            {
                // 要素にセット
                this.PromptItems.Items = new ObservableCollection<PromptCountM>(tmp);
            }
        }
        #endregion

        #region Promptリストの初期化
        /// <summary>
        /// Promptリストの初期化
        /// </summary>
        /// <param name="cvsimg">Civitai用イメージ</param>
        /// <param name="negative_prompt_f">false:Promptのセット true:ネガティブプロンプトのセット</param>
        public void CreatePromptItems(CvsImage.DisplayImageM cvsimg, bool negative_prompt_f = false)
        {
            // ディクショナリで管理
            Dictionary<string, int> prompt_dic = new Dictionary<string, int>();

            // イメージを一通り回す
            foreach (var item in cvsimg.FilteredImages)
            {
                // メタ情報のnullチェック
                if (item.Meta == null)
                    continue;

                // >の後は何故か,で区切られてないことが多いので,を追加
                string prompt = negative_prompt_f ? item.Meta.NegativePrompt.Replace(">", ">,") : item.Meta.Prompt.Replace(">", ">,");

                // 分割
                string[] prompt_list = prompt.Split(",");

                // プロンプトリストを回す
                foreach (var pitem in prompt_list)
                {
                    // (や)を外す
                    string key = pitem.Trim().Replace("(", "").Replace(")", "").ToLower();

                    // プロンプトが登録されていない場合無視
                    if (string.IsNullOrEmpty(key))
                    {
                        continue;
                    }

                    // 既に登録済みかを確認
                    if (prompt_dic.ContainsKey(key))
                    {
                        prompt_dic[key]++;  // カウントアップ
                    }
                    else
                    {
                        prompt_dic.Add(key, 1); // 初期登録
                    }
                }
            }

            // ソート
            var tmp = (from x in prompt_dic
                       select new PromptCountM { Prompt = x.Key, Count = x.Value }).OrderByDescending(x => x.Count).ThenBy(x => x.Prompt).ToList();

            if (negative_prompt_f)
            {
                // 要素にセット
                this.NegativePromptItems.Items = new ObservableCollection<PromptCountM>(tmp);
            }
            else
            {
                // 要素にセット
                this.PromptItems.Items = new ObservableCollection<PromptCountM>(tmp);
            }
        }
        #endregion


        #region Promptリストの初期化
        /// <summary>
        /// Promptリストの初期化
        /// </summary>
        /// <param name="cvsimg">Civitai用イメージ</param>
        /// <param name="negative_prompt_f">false:Promptのセット true:ネガティブプロンプトのセット</param>
        public void CreatePromptItems(CvsModelExM cvsmodel, bool negative_prompt_f = false)
        {
            // ディクショナリで管理
            Dictionary<string, int> prompt_dic = new Dictionary<string, int>();

            // イメージを一通り回す
            foreach (var model in cvsmodel.Items.Items)
            {
                // モデルバージョン情報チェック
                if (model.ModelVersions == null)
                    continue;

                foreach (var model_ver in model.ModelVersions)
                {
                    // モデルバージョンのnullチェック
                    if (model_ver.Images == null)
                        continue;

                    foreach (var item in model_ver.Images)
                    {
                        // メタ情報のnullチェック
                        if(item.Meta == null)
                            continue;

                        // >の後は何故か,で区切られてないことが多いので,を追加
                        string prompt = negative_prompt_f ? item.Meta.NegativPrompt.Replace(">", ">,") : item.Meta.Prompt.Replace(">", ">,");

                        // 分割
                        string[] prompt_list = prompt.Split(",");

                        // プロンプトリストを回す
                        foreach (var pitem in prompt_list)
                        {
                            // (や)を外す
                            string key = pitem.Trim().Replace("(", "").Replace(")", "").ToLower();

                            // プロンプトが登録されていない場合無視
                            if (string.IsNullOrEmpty(key))
                            {
                                continue;
                            }

                            // 既に登録済みかを確認
                            if (prompt_dic.ContainsKey(key))
                            {
                                prompt_dic[key]++;  // カウントアップ
                            }
                            else
                            {
                                prompt_dic.Add(key, 1); // 初期登録
                            }
                        }
                    }
                }
            }

            // ソート
            var tmp = (from x in prompt_dic
                       select new PromptCountM { Prompt = x.Key, Count = x.Value }).OrderByDescending(x => x.Count).ThenBy(x => x.Prompt).ToList();

            if (negative_prompt_f)
            {
                // 要素にセット
                this.NegativePromptItems.Items = new ObservableCollection<PromptCountM>(tmp);
            }
            else
            {
                // 要素にセット
                this.PromptItems.Items = new ObservableCollection<PromptCountM>(tmp);
            }
        }
        #endregion

    }
}
