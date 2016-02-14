﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.IO;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.System;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;
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
           if(essayResult!= null)
            {
                this.DataContext = viewModel = new EssayDetailViewModel(essayResult);
            }
            await viewModel.GenerateHtmlString();
            progress.IsActive = false;
        }
        Essay essayResult;
        private async void AppBarButton_Click(object sender, RoutedEventArgs e)
        {
            await Launcher.LaunchUriAsync(new Uri(viewModel.OriginUri));
        }

        //private void webView_Loaded(object sender, RoutedEventArgs e)
        //{
            
        //}

        //private async void webView_DOMContentLoaded(WebView sender, WebViewDOMContentLoadedEventArgs args)
        //{
        //    News news = await new ApiService().ReadEssay(essayResult.contentId);
        //    if (news != null)
        //    {
        //        await webView.InvokeScriptAsync("setContent", new[] { news.result.mainBody });
        //        await webView.InvokeScriptAsync("setTitle", new[] { news.result.title });
        //        await webView.InvokeScriptAsync("setSubTitle", new[] { news.result.subTitle });
        //    }
        //}
    }
}
