using GamerSky.Core.Helper;
using GamerSky.Core.Http;
using GamerSky.Core.Model;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;

// The Blank Page item template is documented at http://go.microsoft.com/fwlink/?LinkId=234238

namespace GamerSky.View
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class ReadEssayPage : Page
    {
        public ReadEssayPage()
        {
            this.InitializeComponent();
        }

        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            Essay essay = e.Parameter as Essay;

            Get(essay);
        }

        private async void Get(Essay essay)
        {
            Uri uri = new WebView().BuildLocalStreamUri("id", "/Content.html");
            HtmlStreamUriResolver resolver = new HtmlStreamUriResolver();
            webView.NavigateToLocalStreamUri(uri, resolver);

            News news = await ApiService.Instance.ReadEssay(essay.ContentId);
            if (news != null)
            {
                try
                {
                    await webView.InvokeScriptAsync("setTitle", new string[] { news.Title });
                    await webView.InvokeScriptAsync("setMainTitle", new string[] { news.Title });
                    await webView.InvokeScriptAsync("setSubTitle", new string[] { news.SubTitle });
                    await webView.InvokeScriptAsync("setBody", new string[] { news.MainBody });
                }
                catch (Exception ex)
                {
                    string errorText = null;
                    switch (ex.HResult)
                    {
                        case unchecked((int)0x80020006):
                            errorText = "There is no function called doSomething";
                            break;
                        case unchecked((int)0x80020101):
                            errorText = "A JavaScript error or exception occured while executing the function doSomething";
                            break;

                        case unchecked((int)0x800a138a):
                            errorText = "doSomething is not a function";
                            break;
                        default:
                            // Some other error occurred.
                            errorText = ex.Message;
                            break;
                    }
                    Debug.WriteLine(errorText);
                }
            }
        }
    }
}
