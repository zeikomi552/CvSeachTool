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
        public async Task<string> Request(int limit)
        {
            using (var client = new HttpClient())
            {
                var response = await client.GetAsync(CvsModelM.Endpoint + $"?limit={limit}");
                return await response.Content.ReadAsStringAsync();
            }
        }
        #endregion
    }
}
