using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using System.Windows.Markup;
using System.Xml.Serialization;

namespace CvSeachTool.Common.Utilities
{
    public static class JsonExtensions
    {
        /// <summary>
        /// JSONファイルからの読み込み
        /// </summary>
        /// <typeparam name="T">データ型</typeparam>
        /// <param name="path">ファイルパス</param>
        /// <returns></returns>
        public static T? DeserializeFromFile<T>(string jsontext)
        {
            try
            {
                using (StreamReader sr = new StreamReader("bookmark.conf", Encoding.UTF8))
                {
                    string str = sr.ReadToEnd();
                    // デシリアライズオブジェクト関数に読み込んだデータを渡して、
                    // 指定されたデータ用のクラス型で値を返す。
                    return JsonSerializer.Deserialize<T>(new MemoryStream(Encoding.UTF8.GetBytes(str)));
                }
            }
            catch 
            {
                throw;
            }
        }

        public static void SerializeFromFile<T>(T elem, string filename)
        {
            try
            {
                // ファイルを作成
                using (var stream = new FileStream(filename, FileMode.Create))
                {
                    JsonSerializer.Serialize<T>(stream, elem);
                }


            }
            catch
            {
                throw;
            }
        }
    }
}
