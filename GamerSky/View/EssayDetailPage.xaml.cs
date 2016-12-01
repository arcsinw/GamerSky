using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Storage;
using Windows.Storage.Pickers;
using Windows.Storage.Provider;
using Windows.Storage.Streams;
using Windows.System;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Navigation;
using GamerSky.Core.Helper;
using GamerSky.Core.Http;
using GamerSky.Core.Model;
using GamerSky.Core.ViewModel;
using GamerSky.Helper;
using GamerSky.Core.IncrementalLoadingCollection;

// “空白页”项模板在 http://go.microsoft.com/fwlink/?LinkId=234238 上提供

namespace GamerSky.View
{
    /// <summary>
    /// 可用于自身或导航至 Frame 内部的空白页。
    /// </summary>
    public sealed partial class EssayDetailPage : Page
    {
        #region Properties
        public Essay essayResult { get; set; }

        public EssayDetailViewModel viewModel { get; set; }
          
        private ObservableCollection<JsImage> Images { get; set; } = new ObservableCollection<JsImage>();
        /// <summary>
        /// 当前点击的图片url
        /// </summary>
        private string currentImageUrl { get; set; }

        #endregion

        public EssayCommentsCollection CommentsCollection { get; set; } = new EssayCommentsCollection("838261");
        
        public EssayDetailPage()
        {
            this.InitializeComponent();

            webView.NewWindowRequested += WebView_NewWindowRequested;
            DataShareManager.Current.ShareDataChanged += Current_ShareDataChanged;
        }

        private void Current_ShareDataChanged()
        {
            if (viewModel.AppTheme == ElementTheme.Dark)
            {
                NightMode();
            }
            else
            {
                DayMode();
            }
        }

        #region Js Methods
        /// <summary>
        /// 使用js获取所有图片
        /// </summary>
        private async void GetAllPictures()
        {
            try
            {
                await webView.InvokeScriptAsync("GetAllPictures", new[] { "" });
            }
            catch
            {

            }
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

        /// <summary>
        /// 后退
        /// </summary>
        public async void Back()
        {
            await webView.InvokeScriptAsync("eval", new[] { "history.go(-1)" });
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
            try
            {
                await webView.InvokeScriptAsync("NightMode", new[] { "" });
            }
            catch
            {

            }
        }

        /// <summary>
        /// 日间模式
        /// </summary>
        public async void DayMode()
        {
            try
            {
                await webView.InvokeScriptAsync("DayMode", new[] { "" });
            }
            catch
            {

            }
        }

        #endregion


        #region User input handler
        private void refreshButton_Click(object sender, RoutedEventArgs e)
        {
            Refresh();
        }


        private void pivot_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (essayResult != null)
            {
                Refresh();
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
        }

        /// <summary>
        /// 收藏文章
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void likeBtn_Click(object sender, RoutedEventArgs e)
        {
            if (essayResult != null)
            {
                DataShareManager.Current.UpdateFavoriteEssayList(essayResult);
            }
        }

        private async void saveAppBar_Click(object sender, RoutedEventArgs e)
        {
            UIHelper.ShowToast("保存中……");
            var folder = KnownFolders.SavedPictures;
            StorageFile file;
            if (Functions.IsMobile())
            {
                file = await folder.CreateFileAsync("游民壁纸_" + Functions.GetUnixTimeStamp().ToString() + ".jpg");
            }
            else
            {
                FileSavePicker savePicker = new FileSavePicker();
                savePicker.SuggestedFileName = "游民壁纸_" + Functions.GetUnixTimeStamp().ToString(); ;
                savePicker.DefaultFileExtension = ".jpg";
                savePicker.FileTypeChoices.Add("Picture", new List<string>() { ".jpg", ".png" });
                savePicker.SuggestedStartLocation = PickerLocationId.PicturesLibrary;
                savePicker.ContinuationData["Op"] = "ImgSave"; 
                file = await savePicker.PickSaveFileAsync();
            }
            if (file != null)
            {
                CachedFileManager.DeferUpdates(file);
                try
                {
                    var selectImg = imageFlipView.SelectedItem as JsImage;
                    if (selectImg != null)
                    {
                        string url = selectImg.hdsrc.Replace("http://www.gamersky.com/showimage/id_gamersky.shtml?", "");
                        url = string.IsNullOrEmpty(url) ? selectImg.src : url;
                        using (Stream stream = await file.OpenStreamForWriteAsync())
                        {
                            IBuffer buffer = await HttpBaseService.SendGetRequestAsBytes(url);
                            stream.Write(buffer.ToArray(), 0, (int)buffer.Length);
                            await stream.FlushAsync();
                        }
                        FileUpdateStatus updateStatus = await CachedFileManager.CompleteUpdatesAsync(file);
                        if (updateStatus == FileUpdateStatus.Complete)
                        {
                            UIHelper.ShowToast("图片已保存");
                        }
                    }

                }
                catch (Exception ex)
                {
                    JYHelper.TraceError("AdStartPage.xaml" + ex.Message);

                }
            }
        }

        /// <summary>
        /// 查看高清图片
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void hdAppBar_Click(object sender, RoutedEventArgs e)
        {

        }

        /// <summary>
        /// 使用Edge打开网页
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private async void edgeListViewItem_Tapped(object sender, TappedRoutedEventArgs e)
        {
            await Launcher.LaunchUriAsync(new Uri(viewModel.OriginUri));
        }
        #endregion
         
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

        public async void Refresh()
        {
            switch (pivot.SelectedIndex)
            {
                case 0:
                    await viewModel.GenerateHtmlString(); 
                    break;
                case 1:
                    viewModel.RefreshComments(); 
                    break;
            }
        }
         
        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            this.essayResult = e.Parameter as Essay;

            if (essayResult != null)
            {
                DataContext = viewModel = new EssayDetailViewModel(essayResult);  
                if (essayResult.ContentId.Equals("0"))
                {
                    webView.Navigate(new Uri(essayResult.ContentURL));
                }
                else
                {
                    Refresh();
                }
            }
        }


        #region Webview's event
        /// <summary>
        /// 处理WebView中的新请求
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="args"></param>
        private void WebView_NewWindowRequested(WebView sender, WebViewNewWindowRequestedEventArgs args)
        {
            args.Handled = true;

            Debug.WriteLine(args.Uri);

            if (args.Uri.Query.EndsWith(".jpg", StringComparison.CurrentCultureIgnoreCase) ||
                args.Uri.Query.EndsWith(".png", StringComparison.CurrentCultureIgnoreCase) ||
                args.Uri.Query.EndsWith(".gif", StringComparison.CurrentCultureIgnoreCase))
            {
                currentImageUrl = args.Uri.ToString();
                Debug.WriteLine("ClickImageUrl：" + currentImageUrl);
                GetAllPictures();
                
                imageFlipView.Visibility = Visibility.Visible;
                appBar.Visibility = Visibility.Visible;

                if (Images != null)
                {
                    for (int i = 0; i < Images.Count; i++)
                    {
                        if(Images[i].hdsrc!=null && currentImageUrl.Contains(Images[i].hdsrc))
                        {
                            imageFlipView.SelectedIndex = i;
                        }
                        else if (Images[i].src!=null && currentImageUrl.Contains(Images[i].src) )
                        {
                            imageFlipView.SelectedIndex = i;
                        }
                    }
                }
            }
            else
            {
                webView.Navigate(args.Uri);
            }

        }
         
        private void webView_DOMContentLoaded(WebView sender, WebViewDOMContentLoadedEventArgs args)
        { 
            Images.Clear();
            GetAllPictures();
        }
          
        private void webView_ScriptNotify(object sender, NotifyEventArgs e)
        {
            Debug.WriteLine(e.Value);

            var imgs = JsonHelper.Deserlialize<List<JsImage>>(e.Value);
            if (imgs != null && Images.Count == 0)
            {
                foreach (var item in imgs)
                {
                    Images.Add(item);
                }

                countRun.Text = Images.Count.ToString();

            }
            if (e.Value.Contains("back"))
            {
                if (Frame.CanGoBack)
                {
                    Frame.GoBack();
                }
            }

        }

        private void webView_NavigationCompleted(WebView sender, WebViewNavigationCompletedEventArgs args)
        {
            if (DataShareManager.Current.AppTheme == ElementTheme.Dark)
            {
                nightCheckBox.IsChecked = true;
                NightMode();
            }
        }
        #endregion
    }
}
