using CvSeachTool.Common.Utilities;
using CvSeachTool.Models;
using Microsoft.Win32;
using MVVMCore.BaseClass;
using MVVMCore.Common.Utilities;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Http.Json;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

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


        public void Output()
        {
            // ダイアログのインスタンスを生成
            var dialog = new SaveFileDialog();

            // ファイルの種類を設定
            dialog.Filter = "マークダウン (*.md)|*.md";

            // ダイアログを表示する
            if (dialog.ShowDialog() == true)
            {
                StringBuilder sb = new StringBuilder();

                foreach (var item in this.CvsModel!.Items)
                {
                    sb.AppendLine($"## {item.Id} {item.Name}");
                    sb.AppendLine($"");
                    sb.AppendLine($"Creator:{item.Creator.Username}");
                    sb.AppendLine($"AllowCommercialUse:{item.AllowCommercialUse}");
                    sb.AppendLine($"AllowNoCredit:{item.AllowNoCredit}");
                    sb.AppendLine($"Nsfw:{item.Nsfw}");
                    sb.AppendLine($"URL:https://civitai.com/models/{item.Id}");
                    sb.AppendLine($"DownloadCount:{item.Stats.DownloadCount}");
                    sb.AppendLine($"CommentCount:{item.Stats.CommentCount}");
                    sb.AppendLine($"FavoriteCount:{item.Stats.FavoriteCount}");
                    sb.AppendLine($"RatingCount:{item.Stats.RatingCount}");
                    sb.AppendLine($"Rating:{item.Stats.Rating}");
                    sb.AppendLine($"");
                    foreach (var modelver in item.ModelVersions)
                    {
                        sb.AppendLine($"### {modelver.Name}");
                        sb.AppendLine($"");
                        sb.AppendLine($"Create At {modelver.CreatedAt}");
                        sb.AppendLine($"");
                        foreach (var image in modelver.Images)
                        {
                            sb.AppendLine($"");
                            sb.AppendLine($"```");
                            sb.AppendLine($"Meta:{image.Meta}");
                            sb.AppendLine($"```");
                            sb.AppendLine($"<img alt=\"{image.Url}\" src=\"{image.Url}\" width=\"20%\">");
                            sb.AppendLine($"");
                        }
                        sb.AppendLine($"");
                    }
                    sb.AppendLine($"");
                }

                // ファイル出力処理
                File.WriteAllText(dialog.FileName, sb.ToString());
            }
        }
        /// <summary>
        /// 画面初期化処理
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        public override async void Init(object sender, EventArgs e)
        {
            GetModelReqestM tmp = new GetModelReqestM();
            string request = string.Empty;
            this.CvsModel = JsonExtensions.DeserializeFromFile<CvsModelM>(request = await tmp.Request(100, "Image", "Most Downloaded", "Checkpoint", "AllTime"));
            this.CvsModel!.Rowdata = request;
        }

        public override void Close(object sender, EventArgs e)
        {

        }


    }
}
