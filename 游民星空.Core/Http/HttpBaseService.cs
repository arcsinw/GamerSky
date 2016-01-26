using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.Storage.Streams;
using Windows.Web.Http;

namespace 游民星空.Core.Http
{
    /// <summary>
    /// provide basic http function
    /// </summary>
    public class HttpBaseService
    {
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
                return null;
            }
        }

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
                return null;
            }
        }
        
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
                return null;
            }
        }
    }
}
