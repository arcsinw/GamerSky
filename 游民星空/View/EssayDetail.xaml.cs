using System;
using System.Collections.Generic;
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
        public EssayDetail()
        {
            this.InitializeComponent();
            NavigationCacheMode = NavigationCacheMode.Required;
        }
        protected override async void OnNavigatedTo(NavigationEventArgs e)
        {
            string contentId = e.Parameter as string;
            if (string.IsNullOrEmpty(contentId)) return;
            AllChannelListPostData postData = new AllChannelListPostData();
            postData.request = new request { contentId = contentId };
            News news = await new ApiService().ReadEssay(postData);
            if (news != null)
            {
                webView.NavigateToString(news.result.mainBody);
            }
        }

  
    }
}
