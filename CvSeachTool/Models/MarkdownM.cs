using CvSeachTool.Common.Utilities;
using Microsoft.Win32;
using Microsoft.WindowsAPICodePack.Dialogs;
using MVVMCore.BaseClass;
using MVVMCore.Common.Utilities;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CvSeachTool.Models
{
    public class MarkdownM : ModelBase
    {
        


        /// <summary>
        /// マークダウンの出力処理
        /// </summary>
        /// <param name="dir"></param>
        /// <returns></returns>
        public static string OutputMarkdown(string dir)
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

                        var file_info = new FileInfoM() { FilePath = file, ImageText = msg, Prompt = prompt, BasePrompt = prompt.Split(",").Last().Trim() };
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
                    sb.AppendLine($"## No.{no++} {tmp.BasePrompt}");
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
    }
}
