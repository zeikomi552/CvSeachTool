using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Security.Policy;
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
        /// <param name="parameter">パラメータ</param>
        /// <param name="add_endpoint">true:エンドポイントを自動で付与する false:付与しない</param>
        /// <returns>Task</returns>
        public async Task<string> Request(string parameter, bool add_endpoint = true)
        {
            using (var client = new HttpClient())
            {
                if (add_endpoint)
                {
                    // エンドポイント + パラメータ
                    string url = CvsModelM.Endpoint + parameter;

                    // レスポンス待ち(非同期)
                    var response = await client.GetAsync(url);

                    // レスポンスを返却
                    return await response.Content.ReadAsStringAsync();
                }
                else
                {
                    // 上から来たクエリをそのまま実行
                    var response = await client.GetAsync(parameter);

                    // レスポンスを返却
                    return await response.Content.ReadAsStringAsync();
                }
            }
        }
        #endregion
    }
}
