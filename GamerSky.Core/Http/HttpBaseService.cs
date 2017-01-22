using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.Storage.Streams;
using Windows.Web.Http;

namespace GamerSky.Core.Http
{
    /// <summary>
    /// provide basic http function
    /// </summary>
    public class HttpBaseService
    {
        /// <summary>
        /// 向服务器发送get请求  返回服务器回复数据(string)
        /// </summary>
        /// <param name="url"></param>
        /// <returns></returns>
        public async static Task<string> SendGetRequest(string uri)
        {
            try
            {
                HttpResponseMessage response = await new HttpClient().GetAsync(new Uri(uri));
                response.EnsureSuccessStatusCode();
                return await response.Content.ReadAsStringAsync();
            }
            catch(Exception e)
            {
                Debug.WriteLine("HttpBaseService SendGetRequest:" + e.Message);
                return null;
            }
        }

        /// <summary>
        /// 向服务器发送post请求 返回服务器回复数据(string)
        /// </summary>
        /// <param name="url"></param>
        /// <param name="body"></param>
        /// <returns></returns>
        public async static Task<string> SendPostRequest(string uri, string body)
        {
            try
            {
                HttpRequestMessage request = new HttpRequestMessage(HttpMethod.Post, new Uri(uri));
                request.Content = new HttpStringContent(body, Windows.Storage.Streams.UnicodeEncoding.Utf8, "application/json; charset=utf-8");
                HttpResponseMessage response = await new HttpClient().SendRequestAsync(request);
                response.EnsureSuccessStatusCode();
                return await response.Content.ReadAsStringAsync();
            }
            catch(Exception e)
            {
                Debug.Write(e.Message);
                return null;
            }
        }

        /// <summary>
        /// 向服务器发送get请求  返回服务器回复数据(bytes)
        /// </summary>
        /// <param name="url"></param>
        /// <returns></returns>
        public async static Task<IBuffer> SendGetRequestAsBytes(string uri)
        {
            try
            {
                HttpResponseMessage response = await new HttpClient().GetAsync(new Uri(uri));
                response.EnsureSuccessStatusCode();
                return await response.Content.ReadAsBufferAsync();
            }
            catch(Exception e)
            {
                Debug.WriteLine("HttpBaseService SendGetRequestAsBytes:" + e.Message);
                return null;
            }
        }
    }
}
