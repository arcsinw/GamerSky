using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.IO;
using System.Linq;
using System.Runtime.CompilerServices;
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
using 游民星空.Core.Http;
using 游民星空.Core.Model;

// “空白页”项模板在 http://go.microsoft.com/fwlink/?LinkId=234238 上提供

namespace 游民星空.View
{
    /// <summary>
    /// 可用于自身或导航至 Frame 内部的空白页。
    /// </summary>
    public sealed partial class EssayDetail : Page
    {
        private ApiService apiService;
        public EssayDetail()
        {
            this.InitializeComponent();
            NavigationCacheMode = NavigationCacheMode.Required;
            apiService = new ApiService();
        }

        /// <summary>
        /// 页面是否分析完成
        /// </summary>
        private bool isDomLoaded = false;

        private EssayResult essayResult;

        #region OnPropertyChanged
        public event PropertyChangedEventHandler PropertyChanged;

        public void OnPropertyChanged([CallerMemberName]string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
        #endregion

        private bool isActive = true;
        public bool IsActive
        {
            get
            {
                return isActive;
            }
            set
            {
                isActive = value;
                OnPropertyChanged();
            }
        }

        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            essayResult = e.Parameter as EssayResult;
           
        }

        private async void webView_DOMContentLoaded(WebView sender, WebViewDOMContentLoadedEventArgs args)
        {
            isDomLoaded = true;

            if (essayResult != null)
            {
                News news = await apiService.ReadEssay(essayResult.contentId);
                if (news != null)
                {
                    //webView.Source = new Uri("ms-appx-web:///Html/gsAppHTMLTemplate_News.html");

                    if (isDomLoaded)
                    {
                        await webView.InvokeScriptAsync("setContent", new[] { news.result.mainBody });
                        await webView.InvokeScriptAsync("setTitle", new[] { news.result.title });
                        await webView.InvokeScriptAsync("setSubTitle", new[] { news.result.subTitle });
                    }
                }
                List<RelatedReadingsResult> relateReadings = await apiService.GetRelatedReadings(essayResult.contentId, essayResult.contentType);
            }

            IsActive = false;
        }

        protected override async void OnNavigatedFrom(NavigationEventArgs e)
        {
            try
            {
                if (isDomLoaded)
                {
                    await webView.InvokeScriptAsync("clearContent", new[] { "" });
                    await webView.InvokeScriptAsync("clearTitle", new[] { "" });
                    await webView.InvokeScriptAsync("clearSubTitle", new[] { "" });
                }
            }
            catch
            {

            }
        }
    }
}
