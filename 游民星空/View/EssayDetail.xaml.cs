using System;
using Windows.System;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Navigation;
using 游民星空.Core.Helper;
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
             
            SizeChanged += EssayDetail_SizeChanged;
        }

        private void EssayDetail_SizeChanged(object sender, SizeChangedEventArgs e)
        {
            ReSizeVideo();
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
            if (!essayResult.contentId.Equals("0"))
            {
                this.DataContext = viewModel = new EssayDetailViewModel(essayResult);
                await viewModel.GenerateHtmlString();
            }
            else
            {
                webView.Navigate(new Uri(essayResult.contentURL));
            }
            progress.IsActive = false;
            JYHelper.TraceRead();
        }

        Essay essayResult;
        private async void Edge(object sender, RoutedEventArgs e)
        {
            await Launcher.LaunchUriAsync(new Uri(viewModel.OriginUri));
        }

        private async void webView_DOMContentLoaded(WebView sender, WebViewDOMContentLoadedEventArgs args)
        {  
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

            //为<a></a>添加点击事件，并通知
            //js = @" var links = document.getElementsByTagName('a');
            //    for(var i=0;i<links.length;i++)
            //    {
            //        if(links[i].id='RelatedReadings')
            //        {
            //            links[i].onclick = function()
            //            {
            //                     var jsonObj = { PageId: this.href, PageUrl:' ', OpenMethod:'OpenArticleWithId'};
            //                    window.external.notify(JSON.stringify(jsonObj));
            //            };
            //        }
            //    }";
           
            //await sender.InvokeScriptAsync("eval", new[] { js });


            //动态加载手势
            js = @"var myScript = document.createElement(""script"");
                myScript.type = ""text/javascript"";
                myScript.src = ""ms-appx-web:///Assets/gsAppHTMLTemplate_js/gesture.js"";
                document.body.appendChild(myScript);
                window.external.notify(myScript.src+"""");";
            //await sender.InvokeScriptAsync("eval", new[] { js });

            //为body添加手势监听
             js = @"var target = document.getElementsByTagName(""body"")[0];
            prepareTarget(target, eventListener);";
            //await sender.InvokeScriptAsync("eval", new[] { js });

           

            //iframe自适应
            js = @"var iframeTags = document.getElementsByTagName('iframe');
            for (var iframeTagIndex = 0;
                iframeTagIndex < iframeTags.length;
                iframeTagIndex++)
                    {
                        var iframeTag = iframeTags[iframeTagIndex];
                        iframeTag.removeAttribute('style');
                        iframeTag.height = document.body.clientWidth * (9 / 16);
                        iframeTag.width = document.body.clientWidth;
                    }

                    var embedTags = document.getElementsByTagName('embed');
                    for (var embedTagIndex = 0;
                         embedTagIndex < embedTags.length;
                         embedTagIndex++)
                    {
                        var embedTag = embedTags[embedTagIndex];
                        embedTag.removeAttribute('style');
                        embedTag.height = document.body.clientWidth * (9 / 16);
                        embedTag.width = document.body.clientWidth;
                    }";
            //await sender.InvokeScriptAsync("eval", new[] { js });
        }

        /// <summary>
        /// 翻译网页
        /// </summary>
        private async void Translate()
        {
            var js = "javascript:(function(){var s = document.createElement('script'); s.type = 'text/javascript'; s.src = 'http://labs.microsofttranslator.com/bookmarklet/default.aspx?f=js&to=en'; document.body.insertBefore(s, document.body.firstChild);})()";
            switch (pivot.SelectedIndex)
            {
                case 0:
                    await webView.InvokeScriptAsync("eval", new[] { js });
                    break;
                case 1:
                    await commentWebView.InvokeScriptAsync("eval", new[] { js });
                    break;
            }
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
        /// 自适应视频窗口大小
        /// </summary>
        public async void ReSizeVideo()
        {
            //iframe自适应
            var js = @"var iframeTags = document.getElementsByTagName('iframe');
             
            for (var iframeTagIndex = 0;
                iframeTagIndex <iframeTags.length;
                iframeTagIndex++)
                    {
                        var iframeTag = iframeTags[iframeTagIndex];
                        iframeTag.removeAttribute('style');
                        iframeTag.height = document.body.clientWidth * (9 / 16);
                        iframeTag.width = " + DeviceInformationHelper.GetScreenWidth()+@"
                    }

                    var embedTags = document.getElementsByTagName('embed');
                    for (var embedTagIndex = 0;
                         embedTagIndex < embedTags.length;
                         embedTagIndex++)
                    {
                        var embedTag = embedTags[embedTagIndex];
                        embedTag.removeAttribute('style');
                        embedTag.height = document.body.clientWidth * (9 / 16);
                        embedTag.width = document.body.clientWidth;
                    }";
            await webView.InvokeScriptAsync("eval", new[] { js });

            js = @"var iframeTags = document.getElementsByTagName('iframe');
                        var iframeTag = iframeTags[0];
                        iframeTag.removeAttribute('style');
                        iframeTag.height = document.body.clientWidth * (9 / 16);
                        iframeTag.width = document.body.clientWidth;";
            //await webView.InvokeScriptAsync("eval", new[] { js });
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

        /// <summary>
        /// 设置夜间模式
        /// </summary>
        public async void NightMode()
        {
            var js = @"var htmlTag= document.getElementsByTagName(""html"")[0];
            gsSetElementClass(htmlTag, ""PageColorMode_Day"", ""PageColorMode_Night"");
                   ";
            await webView.InvokeScriptAsync("eval", new[] { js});
            //await webView.InvokeScriptAsync("eval", new[] { "document.body.style.backgroundColor='#000000';" });
        }

        public async void DayMode()
        {
            await webView.InvokeScriptAsync("eval", new[] { "document.body.style.backgroundColor='#FFFFFF';" });
        }
 
         
        private async void refreshButton_Click(object sender, RoutedEventArgs e)
        {
            await viewModel.GenerateHtmlString();
        }

        private async void pivot_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (viewModel != null)
            {
                switch ( pivot.SelectedIndex)
                {
                    case 0:
                        await viewModel.GenerateHtmlString();
                        break;
                    case 1:
                        viewModel.GenerateCommentString();
                        break;
                }
            }
        }

        /// <summary>
        /// 夜间模式
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void nightCheckBox_Checked(object sender, RoutedEventArgs e)
        {
            NightMode();
        }

        private void nightCheckBox_Unchecked(object sender, RoutedEventArgs e)
        {
            DayMode();
        }



        private void webView_FrameContentLoading(WebView sender, WebViewContentLoadingEventArgs args)
        {
            var uri = args.Uri;
        }

        private void webView_FrameDOMContentLoaded(WebView sender, WebViewDOMContentLoadedEventArgs args)
        {
            viewModel.IsActive = false;
        }

        /// <summary>
        /// 翻译成英文
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void translateBtn_Click(object sender, RoutedEventArgs e)
        {
            switch(pivot.SelectedIndex)
            {
                case 0:
                    Translate();

                    break;
                case 1:
                    break;
            }
        }

        /// <summary>
        /// 收藏文章
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void likeBtn_Click(object sender, RoutedEventArgs e)
        {
            var vm = this.DataContext as EssayDetailViewModel;
            if (vm != null)
            {
                DataShareManager.Current.UpdateFavoriteEssayList(vm.essayResult);
            }
        }
    }
}
