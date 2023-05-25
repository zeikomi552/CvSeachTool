using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace CvSeachTool.Models
{
    internal class GetModelReqestM
    {
        #region 接続用クライアントの作成
        /// <summary>
        /// 接続用クライアントの作成
        /// </summary>
        /// <param name="url">URL</param>
        /// <returns>Task</returns>
        public async Task<string> Request(string parameter)
        {
            using (var client = new HttpClient())
            {
                string url = CvsModelM.Endpoint + parameter;
                var response = await client.GetAsync(url);
                return await response.Content.ReadAsStringAsync();
            }
        }
        #endregion
    }
}
