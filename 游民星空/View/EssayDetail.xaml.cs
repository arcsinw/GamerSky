using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Phone.UI.Input;
using Windows.Storage;
using Windows.Storage.Pickers;
using Windows.Storage.Provider;
using Windows.Storage.Streams;
using Windows.System;
using Windows.UI.Popups;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Navigation;
using 游民星空.Core.Helper;
using 游民星空.Core.Http;
using 游民星空.Core.Model;
using 游民星空.Core.ViewModel;
using 游民星空.Helper;

// “空白页”项模板在 http://go.microsoft.com/fwlink/?LinkId=234238 上提供

namespace 游民星空.View
{
    /// <summary>
    /// 可用于自身或导航至 Frame 内部的空白页。
    /// </summary>
    public sealed partial class EssayDetail : Page
    {
        private EssayDetailViewModel viewModel;
        private Essay essayResult;
        private bool isEssayLoaded = false;


        private bool isDOMLoadCompleted = false;
        public EssayDetail()
        {
            this.InitializeComponent();
            NavigationCacheMode = NavigationCacheMode.Disabled;

            webView.NewWindowRequested += WebView_NewWindowRequested;
             
        }
    
        public void CloseImageFlipView()
        {
            if (imageFlipView.Visibility == Visibility.Visible)
            {
                imageFlipView.Visibility = Visibility.Collapsed;
                appBar.Visibility = Visibility.Collapsed;
            }
            else
            {
                if (Frame.CanGoBack)
                {
                    this.Frame.GoBack();
                }
            }
     
        }
 
        /// <summary>
        /// 处理WebView中的新请求
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="args"></param>
        private void WebView_NewWindowRequested(WebView sender, WebViewNewWindowRequestedEventArgs args)
        {
            args.Handled = true;
            if(args.Uri.Query.EndsWith(".jpg",StringComparison.CurrentCultureIgnoreCase))
            {
                GetAllPictures();
                imageFlipView.Visibility = Visibility.Visible;
                appBar.Visibility = Visibility.Visible;
            }
            else
            {
                webView.Navigate(args.Uri);
            }

        }

        private async void GetAllPictures()
        {
            await webView.InvokeScriptAsync("GetAllPictures", new[] {"" });
        }

        protected override void OnNavigatedTo(NavigationEventArgs e)
        { 
            progress.IsActive = true;
            essayResult = e.Parameter as Essay;
            if (essayResult == null) return;
            if (!essayResult.contentId.Equals("0"))
            {
                this.DataContext = viewModel = new EssayDetailViewModel(essayResult);
                //await viewModel.GenerateHtmlString();
            }
            else
            {
                webView.Navigate(new Uri(essayResult.contentURL));
            }
            progress.IsActive = false;
            JYHelper.TraceRead();
        }

    
        private async void Edge(object sender, RoutedEventArgs e)
        {
            await Launcher.LaunchUriAsync(new Uri(viewModel.OriginUri));
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
            Debug.WriteLine(e.Value);
            
            List<JsImage> imgs = Functions.Deserlialize<List<JsImage>>(e.Value);

            imageFlipView.ItemsSource = imgs;

            //if(e.Value.Contains("forward"))
            //{
            //    if (pivot.Items != null)
            //    {
            //        if (pivot.SelectedIndex > 0)
            //        {
            //            pivot.SelectedIndex--;
            //        }
            //        else
            //        {
            //            pivot.SelectedIndex = pivot.Items.Count - 1;
            //        }
            //    }
            //}
            //else
            if(e.Value.Contains("back"))
            {
                if(Frame.CanGoBack)
                {
                    Frame.GoBack();
                }
            }
            //var model = Functions.Deserlialize<JSParameter>(e.Value);
            //if (model == null) return;
            //switch (model.type)
            //{
            //    case "image":
                   
            //        break;
            //    case "swiperight":
            //        //右滑
            //        if (Frame.CanGoBack)
            //        {
            //            Frame.GoBack();
            //        }
            //        break;
            //    case "swipeleft":
            //        //左滑
                   
            //        break;
            //    case "text":
                   
            //        break;
            //}
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
            await viewModel.GenerateHtmlString();
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
            if (isDOMLoadCompleted)
            {
                await webView.InvokeScriptAsync("NightMode", new[] { "" });
            }
        }

        public async void DayMode()
        {
            if (isDOMLoadCompleted)
            {
                await webView.InvokeScriptAsync("DayMode", new[] { "" });
            }
        }
 
         
        private async void refreshButton_Click(object sender, RoutedEventArgs e)
        {
            progress.IsActive = true;
            
            await viewModel.GenerateHtmlString();
            progress.IsActive = false;
        }

        
        private async void pivot_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (viewModel != null)
            {
                switch ( pivot.SelectedIndex)
                {
                    case 0:
                        if (!isEssayLoaded)
                        {
                            await viewModel.GenerateHtmlString();
                            isEssayLoaded = true;
                        }
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

         
        /// <summary>
        /// 翻译成英文
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void translateBtn_Click(object sender, RoutedEventArgs e)
        { 
            Translate();
            //ExperimentHelper.LogTranslateClick();
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

        private void webView_DOMContentLoaded(WebView sender, WebViewDOMContentLoadedEventArgs args)
        {
            isDOMLoadCompleted = true;
        }

        private async void saveAppBar_Click(object sender, RoutedEventArgs e)
        {
            var folder = KnownFolders.SavedPictures;

            FileSavePicker savePicker = new FileSavePicker();
            savePicker.SuggestedFileName = "游民壁纸_" + DateTime.Now.Month + DateTime.Now.Day;
            savePicker.DefaultFileExtension = ".jpg";
            savePicker.FileTypeChoices.Add("Picture", new List<string>() { ".jpg", ".png" });
            savePicker.SuggestedStartLocation = PickerLocationId.PicturesLibrary;
            savePicker.ContinuationData["Op"] = "ImgSave";
             
            StorageFile file = await savePicker.PickSaveFileAsync();
            if (file != null)
            {
                CachedFileManager.DeferUpdates(file);

                try
                {
                    using (Stream stream = await file.OpenStreamForWriteAsync())
                    {
                        IBuffer buffer = await HttpBaseService.SendGetRequestAsBytes((imageFlipView.SelectedItem as JsImage).src);
                        stream.Write(buffer.ToArray(), 0, (int)buffer.Length);
                        await stream.FlushAsync();
                    }
                    FileUpdateStatus updateStatus = await CachedFileManager.CompleteUpdatesAsync(file);
                    if (updateStatus == FileUpdateStatus.Complete)
                    {
                        UIHelper.ShowToast("图片已保存");
                    }
                }
                catch (Exception ex)
                {
                    JYHelper.TraceError("AdStartPage.xaml" + ex.Message);

                }
            }
        }
    }
}
