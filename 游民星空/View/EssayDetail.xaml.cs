using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.IO;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.System;
using Windows.UI.Popups;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;
using 游民星空.Core.Helper;
using 游民星空.Core.Http;
using 游民星空.Core.Model;
using 游民星空.Core.ViewModel;

// “空白页”项模板在 http://go.microsoft.com/fwlink/?LinkId=234238 上提供

namespace 游民星空.View
{
    /// <summary>
    /// 可用于自身或导航至 Frame 内部的空白页。
    /// </summary>
    public sealed partial class EssayDetail : Page
    {
        private EssayDetailViewModel viewModel;
        public EssayDetail()
        {
            this.InitializeComponent();
            NavigationCacheMode = NavigationCacheMode.Disabled;

            webView.NewWindowRequested += WebView_NewWindowRequested;

          
        }

        /// <summary>
        /// 处理WebView中的新请求
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="args"></param>
        private void WebView_NewWindowRequested(WebView sender, WebViewNewWindowRequestedEventArgs args)
        {
            //args.Handled = true;
            if(args.Uri.Query.EndsWith(".jpg",StringComparison.CurrentCultureIgnoreCase))
            {
               
            }
            else
            {
                webView.Navigate(args.Uri);
            }

        }

        protected override async void OnNavigatedTo(NavigationEventArgs e)
        {
            progress.IsActive = true;
            essayResult = e.Parameter as Essay;
            if (essayResult == null) return;
           if(!essayResult.contentId.Equals("0"))
            {
                this.DataContext = viewModel = new EssayDetailViewModel(essayResult);
                await viewModel.GenerateHtmlString();
            }
            else
            {
                webView.Navigate(new Uri(essayResult.contentURL));
            }
            progress.IsActive = false;
        }
        Essay essayResult;
        private async void Edge(object sender, RoutedEventArgs e)
        {
            await Launcher.LaunchUriAsync(new Uri(viewModel.OriginUri));
        }

        private async void webView_DOMContentLoaded(WebView sender, WebViewDOMContentLoadedEventArgs args)
        {
            //动态加载js
            //var js = @"var myScript = document.createElement(""script"");
            //                myScript.type = ""text/javascript"";
            //                myScript.src=""ms-appx-web:///Assets/gsAppHTMLTemplate_js/gsAppHTMLTemplate.js"";
            //                document.body.appendChild(myScript);";
            //await sender.InvokeScriptAsync("eval", new[] { js });

            //js = @"var myScript = document.createElement(""script"");
            //                myScript.type = ""text/javascript"";
            //                myScript.src=""ms-appx-web:///Assets/gsAppHTMLTemplate_js/gsAppHTMLTemplate_Video.js"";
            //                document.body.appendChild(myScript);";
            //await sender.InvokeScriptAsync("eval", new[] { js });

            //加载v1 css
            var js = @"var myCss = document.createElement(""link"");
                    myCss.rel = ""stylesheet"";
                    myCss.type = ""text/css"";
                    myCss.href = ""ms-appx-web:///Assets/gsAppHTMLTemplate_css/base.css"";
                    document.body.appendChild(myCss)";
            await sender.InvokeScriptAsync("eval", new[] { js });

            //3、调用js执行自定义代码（为图片添加点击事件，并通知）
            // js = @"var imgs = document.getElementsByTagName(""img"");
            //for (var i = 0, len = imgs.length; i < len; i++) {
            //    imgs[i].onclick = function (e) {
            //        var jsonObj = { type: 'image', content1: this.src };
            //        window.external.notify(JSON.stringify(jsonObj));
            //    };
            //}";
            // await sender.InvokeScriptAsync("eval", new[] { js });

            //4、动态加载手势
            //js = @"var myScript = document.createElement(""script"");
            //    myScript.type = ""text/javascript"";
            //    myScript.src = ""ms-appx-web:///Assets/gsAppHTMLTemplate_js/gesture.js"";
            //    document.body.appendChild(myScript);
            //    window.external.notify(myScript.src+"""");";
            //await sender.InvokeScriptAsync("eval", new[] { js });

            //为body添加手势监听
           // js = @"var target = document.getElementsByTagName(""body"")[0];
           //prepareTarget(target, eventListener);";
           // await sender.InvokeScriptAsync("eval", new[] { js });

            //iframe自适应
            js = @"var iframeTags = document.getElementsByTagName(""iframe"");
            for (var iframeTagIndex = 0;
                iframeTagIndex < iframeTags.length;
                iframeTagIndex++)
                    {
                        var iframeTag = iframeTags[iframeTagIndex];
                        iframeTag.removeAttribute(""style"");
                        iframeTag.height = document.body.clientWidth * (9 / 16);
                        iframeTag.width = document.body.clientWidth;
                    }

                    var embedTags = document.getElementsByTagName(""embed"");
                    for (var embedTagIndex = 0;
                         embedTagIndex < embedTags.length;
                         embedTagIndex++)
                    {
                        var embedTag = embedTags[embedTagIndex];
                        embedTag.removeAttribute(""style"");
                        embedTag.height = document.body.clientWidth * (9 / 16);
                        embedTag.width = document.body.clientWidth;
                    }";
            await sender.InvokeScriptAsync("eval", new[] { js });
        }

        private void webView_ScriptNotify(object sender, NotifyEventArgs e)
        {
            var model = Functions.Deserlialize<JSParameter>(e.Value);
            if (model == null) return;
            switch (model.type)
            {
                case "image":
                   
                    break;
                case "swiperight":
                    //右滑
                    if (Frame.CanGoBack)
                    {
                        Frame.GoBack();
                    }
                    break;
                case "swipeleft":
                    //左滑
                  
                    break;
                case "text":
                   
                    break;
            }
        }

        /// <summary>
        /// 后退
        /// </summary>
        public async void Back()
        {
            await webView.InvokeScriptAsync("eval", new[] { "history.go(-1)" });
        }

        public async void Refresh()
        {
            await webView.InvokeScriptAsync("eval", new[] { "history.go()" });
        }

        public async void Forward()
        {
            await webView.InvokeScriptAsync("eval", new[] { "history.go(1)" });
        }

        private void webView_Holding(object sender, HoldingRoutedEventArgs e)
        {

        }

        private void commentWebView_Loaded(object sender, RoutedEventArgs e)
        {
            string html = @"<html>
                                        <head>
                                        <meta charset=""utf-8"">
                                        <title></title>
                                        </head>
                                        <body>
                                        <div id=""SOHUCS"" sid="""+viewModel.essayResult.contentId+ @"""></div>
                                        <script id=""changyan_mobile_js"" charset=""utf-8"" type=""text/javascript""
                                            scr=""http://changyan.sohu.com/upload/mobile/v2/wap-js/src/adapter-version.js?1456929528669-0.5822440742160904"">
                                        </script>
                                        <script type=""text/javascript"">
                                            window.changyan.api.config({
                                                appid:'cyqQwkOU4',
                                                conf:'C6F9522019500001516610E01850CD20'});
                                        </script></body></html>";
            commentWebView.NavigateToString(html);
        }

        private async void refreshButton_Click(object sender, RoutedEventArgs e)
        {
            await viewModel.GenerateHtmlString();
        }
    }
}
