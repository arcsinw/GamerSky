using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.Foundation;
using Windows.Security.Cryptography;
using Windows.Storage;
using Windows.Storage.Streams;
using Windows.Web;

namespace GamerSky.Core.Helper
{
    public class HtmlStreamUriResolver : IUriToStreamResolver
    {
        public IAsyncOperation<IInputStream> UriToStreamAsync(Uri uri)
        {
            return GetContent(uri).AsAsyncOperation();
        }

        public async Task<string> LoadStringFromPackageFileAsync(string name)
        {
            Debug.WriteLine("LoadStringFromPackageFileAsync : " + name);
            // Using the storage classes to read the content from a file as a string.
            StorageFile f = await StorageFile.GetFileFromApplicationUriAsync(new Uri($"ms-appx:///Html/{name}"));
            return await FileIO.ReadTextAsync(f);
        }

        // 根据 uri 返回对应的内容流
        private async Task<IInputStream> GetContent(Uri uri)
        {
            string path = uri.AbsolutePath;
            string content = string.Empty;

            switch (path)
            {
                case "/Content.html":
                    content = await LoadStringFromPackageFileAsync("Content.html");
                    break;
                case "/gs.js":
                    content = await LoadStringFromPackageFileAsync("gs.js");
                    break;
                case "/gsAppHTMLTemplate_Video.js":
                    content = await LoadStringFromPackageFileAsync("gsAppHTMLTemplate_Video.js");
                    break;
                case "/gs.css":
                    content = await LoadStringFromPackageFileAsync("gs.css");
                    break;
                //case "/gsAppHTMLTemplate.css":
                //    content = await LoadStringFromPackageFileAsync("gsAppHTMLTemplate.css");
                //    break;
                case "/gsVideo.js":
                    content = await LoadStringFromPackageFileAsync("gsVideo.js");
                    break;
            }

            // Convert the string to a stream.
            IBuffer buffer = CryptographicBuffer.ConvertStringToBinary(content, BinaryStringEncoding.Utf8);
            var stream = new InMemoryRandomAccessStream();
            await stream.WriteAsync(buffer);
            return stream.GetInputStreamAt(0);
        }
    }
}
