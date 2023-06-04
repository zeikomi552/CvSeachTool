using CvSeachTool.Common.Utilities;
using Microsoft.Win32;
using Microsoft.WindowsAPICodePack.Dialogs;
using MVVMCore.BaseClass;
using MVVMCore.Common.Utilities;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;
using System.Windows.Documents;
using System.Windows.Interop;
using System.Windows.Media.Animation;
using System.Windows.Media.Media3D;
using static CvSeachTool.Models.CvsModelM;
using static System.Net.WebRequestMethods;
using File = System.IO.File;

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


        #region マークダウンの出力処理
        /// <summary>
        /// マークダウンの出力処理
        /// </summary>
        public void MarkdownOutput1()
        {
            // ダイアログのインスタンスを生成
            var dialog = new SaveFileDialog();

            // ファイルの種類を設定
            dialog.Filter = "マークダウン (*.md)|*.md";

            // ダイアログを表示する
            if (dialog.ShowDialog() == true)
            {
                StringBuilder sb = new StringBuilder();

                int rank = 1;
                foreach (var item in this.Items)
                {
                    sb.AppendLine($"## {rank++}位 {item.Id} {item.Name}");
                    sb.AppendLine($"");
                    sb.AppendLine($"- Creator : {item.Creator.Username}");
                    sb.AppendLine($"- AllowCommercialUse : {item.AllowCommercialUse}");
                    sb.AppendLine($"- AllowNoCredit : {item.AllowNoCredit}");
                    sb.AppendLine($"- Nsfw : {item.Nsfw}");
                    sb.AppendLine($"- URL : https://civitai.com/models/{item.Id}");
                    //sb.AppendLine($"- DownloadCount : {item.Stats.DownloadCount}");
                    //sb.AppendLine($"- CommentCount:{item.Stats.CommentCount}");
                    //sb.AppendLine($"- FavoriteCount:{item.Stats.FavoriteCount}");
                    //sb.AppendLine($"- RatingCount:{item.Stats.RatingCount}");
                    //sb.AppendLine($"- Rating:{item.Stats.Rating}");
                    sb.AppendLine($"");
                    foreach (var modelver in item.ModelVersions)
                    {
                        sb.AppendLine($"### ver : {modelver.Name}");
                        sb.AppendLine($"");
                        sb.AppendLine($"- Create At {modelver.CreatedAt}");
                        sb.AppendLine($"- ModelVersionURL https://civitai.com/models/{item.Id}?modelVersionId={modelver.Id}");
                        sb.AppendLine($"- [Model Download]({modelver.DownloadUrl})");
                        sb.AppendLine($"");
                        int count = 0;
                        foreach (var image in modelver.Images)
                        {
                            sb.AppendLine($"");
                            //sb.AppendLine($"{image.Nsfw}");

                            if (image.Meta != null && (image.Nsfw.Equals("None") || image.Nsfw.Equals("Soft")))
                            {
                                sb.AppendLine($"```");
                                sb.AppendLine($"Prompt : {image.Meta.Prompt}");
                                sb.AppendLine($"");
                                sb.AppendLine($"Negative Prompt : {image.Meta.NegativPrompt}");
                                sb.AppendLine($"```");
                                sb.AppendLine($"");
                                sb.AppendLine($"");
                                sb.AppendLine($"<img alt=\"{image.Url}\" src=\"{image.Url}\" width=\"20%\">");
                                sb.AppendLine($"");
                                if (count++ > 2) break;
                                //break;
                            }
                        }
                        sb.AppendLine($"");
                        break;
                    }
                    sb.AppendLine($"");
                }

                // ファイル出力処理
                File.WriteAllText(dialog.FileName, sb.ToString());
            }
        }
        #endregion

        #region マークダウンの出力処理
        /// <summary>
        /// マークダウンの出力処理
        /// </summary>
        public void MarkdownOutput2()
        {
            // ダイアログのインスタンスを生成
            var dialog = new SaveFileDialog();

            // ファイルの種類を設定
            dialog.Filter = "マークダウン (*.md)|*.md";

            // ダイアログを表示する
            if (dialog.ShowDialog() == true)
            {
                StringBuilder sb = new StringBuilder();
                sb.AppendLine($"## CIVITAIモデルダウンロードランキング");
                sb.AppendLine($"データ取得日 : {DateTime.Today.ToString("yyyy/MM/dd")}");
                sb.AppendLine($"※各リンクはCIVITAIへのログインが必要です");
                sb.AppendLine($"");
                sb.AppendLine($"CIVITAI");
                sb.AppendLine($"https://civitai.com/");
                sb.AppendLine($"");
                sb.AppendLine($"REST API");
                sb.AppendLine($"{this.RequestURL}");
                sb.AppendLine($"");


                sb.AppendLine($"|<center>順位</center><center>(DL数)</center>|モデルID / 作者名<br>モデル名|モデルタイプ<br>NSFW<br>商用利用|");
                sb.AppendLine($"|---|---|---|");


                int rank = 1;
                foreach (var item in this.Items)
                {
                    sb.AppendLine($"|<center>{rank++}位</center><center>({item.Stats.DownloadCount})</center>" +
                        $"|{item.Id} / [{item.Creator.Username}](https://civitai.com/user/{item.Creator.Username}/models)<br>[{item.Name.Replace("|", "\\|")}](https://civitai.com/models/{item.Id})" +
                        $"| {item.Type}<br>{(item.Nsfw ? "NSFW" : "-")}<br>{item.AllowCommercialUse}|");
                }

                // ファイル出力処理
                File.WriteAllText(dialog.FileName, sb.ToString());
            }
        }
        #endregion

        /// <summary>
        /// ファイルの並列コピー
        /// </summary>
        /// <param name="srcPath">コピー元ディレクトリ</param>
        /// <param name="dstPath">コピー先ディレクトリ</param>
        /// <param name="filter">フィルター</param>
        /// <param name="additional_file_name">ファイル名に追加する文字列(重複を避けるため)</param>
        static void CopyFilesParallel(string srcPath, string dstPath, string filter, string additional_file_name)
        {
            // コピー元ファイルの一覧（FileInfoの配列）を作る
            System.IO.DirectoryInfo dir = new System.IO.DirectoryInfo(srcPath);
            System.IO.FileInfo[] files =
                dir.GetFiles(filter, System.IO.SearchOption.AllDirectories);

            // マルチスレッドでコピー
            Parallel.ForEach(files, file =>
            {
                string dst = dstPath + "\\" + additional_file_name + "_" + file.Name;
                System.IO.File.Copy(file.FullName, dst, true);
            });
        }

        public string OutputMarkdown3(string dir)
        {

            // ダイアログのインスタンスを生成
            var dialog = new SaveFileDialog();

            // ファイルの種類を設定
            dialog.Filter = "マークダウン (*.md)|*.md";

            string img_dir = string.Empty;
            string mk_filename = string.Empty;
            // ダイアログを表示する
            if (dialog.ShowDialog() == true)
            {
                // 拡張子なしファイル名取得
                mk_filename = Path.GetFileNameWithoutExtension(dialog.FileName);

                // ファイル名をディレクトリ名として使用する
                img_dir = Path.Combine(PathManager.GetCurrentDirectory(dialog.FileName), mk_filename);

                // ファイル移動先ディレクトリの作成
                PathManager.CreateDirectory(img_dir);

                // ファイルコピー
                CopyFilesParallel(dir, img_dir, "*.png", mk_filename);
            }
            else
            {
                return string.Empty;
            }

            List<FileInfoM> list = new List<FileInfoM>();

            // フォルダ内のファイル一覧を取得
            var fileArray = Directory.GetFiles(dir);
            foreach (string file in fileArray)
            {
                using (var reader = new BinaryReader(File.Open(file, FileMode.Open, FileAccess.Read)))
                {
                    if (PngReader.ReadPngSignature(reader))
                    {
                        var ihdrchunk = PngReader.ReadChunk(reader);
                        var itextchunk = PngReader.ReadChunk(reader);

                        // データがutf - 8の場合
                        var msg = System.Text.Encoding.UTF8.GetString(itextchunk.ChunkData).Replace("\0", ":");

                        var msg_list = msg.Split("\n");
                        var prompt = msg_list.ElementAt(0).Replace("parameters:", "");

                        var file_info = new FileInfoM() { FilePath = file, ImageText = msg, Prompt = prompt, BasePrompt = prompt.Split(",").Last().Trim()};
                        list.Add(file_info);
                    }
                }

            }
            var list_sort = (from x in list
                             orderby x.BasePrompt, x.FilePath
                             select x).ToList<FileInfoM>();

            StringBuilder sb = new StringBuilder();

            string base_prompt = string.Empty;
            int no = 1;
            foreach (var tmp in list_sort)
            {
                if (!base_prompt.Equals(tmp.BasePrompt))
                {
                    base_prompt = tmp.BasePrompt;
                    sb.AppendLine($"## No.{no ++} {tmp.BasePrompt}");
                }

                sb.AppendLine($"### {tmp.Prompt}");
                string img_file_soutai = Path.Combine(Path.GetFileName(img_dir), mk_filename + "_" + Path.GetFileName(tmp.FilePath));
                sb.AppendLine($"![]({img_file_soutai})");
                sb.AppendLine($"");
                sb.AppendLine($"```");
                sb.AppendLine($"{tmp.ImageText.Replace("parameters:", "prompt: ")}");
                sb.AppendLine($"```");
            }

            // ファイル出力処理
            File.WriteAllText(dialog.FileName, sb.ToString());

            return sb.ToString();
        }
    }
}
