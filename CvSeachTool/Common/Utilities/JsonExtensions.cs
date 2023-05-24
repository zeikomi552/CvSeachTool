using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

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
                // デシリアライズオブジェクト関数に読み込んだデータを渡して、
                // 指定されたデータ用のクラス型で値を返す。
                return JsonSerializer.Deserialize<T>(new MemoryStream(Encoding.UTF8.GetBytes(jsontext)));

            }
            catch (Exception ex)
            {
                Debug.WriteLine($"failed:{ex.Message}");
                return default;
            }
        }
    }
}
