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
using GamerSky.Helper;
using GamerSky.Http;
using GamerSky.Model;
using GamerSky.ViewModel; 
using GamerSky.IncrementalLoadingCollection;
using GamerSky.PostDataModel;
using System.Threading.Tasks;

// “空白页”项模板在 http://go.microsoft.com/fwlink/?LinkId=234238 上提供

namespace GamerSky.View
{
    /// <summary>
    /// 可用于自身或导航至 Frame 内部的空白页。
    /// </summary>
    public sealed partial class EssayDetailPage : Page
    {
        #region Properties
        public Essay _essayResult;
        public Essay EssayResult
        {
            get
            {
                return _essayResult;
            }
            set
            {
                _essayResult = value;
            }
        }

        //public EssayDetailViewModel ViewModel { get; set; }
          
        private ObservableCollection<JsImage> Images { get; set; } = new ObservableCollection<JsImage>();
        /// <summary>
        /// 当前点击的图片url
        /// </summary>
        private string CurrentImageUrl { get; set; } 

        /// <summary>
        /// 新闻是否已加载
        /// </summary>
        public bool IsEssayLoaded { get; set; } = false;

        /// <summary>
        /// 评论是否已加载
        /// </summary>
        public bool IsCommentLoaded { get; set; } = false;

        #endregion

        public EssayCommentsCollection CommentsCollection { get; set; }

        public EssayDetailPage()
        {
            this.InitializeComponent();
             
            DataShareManager.Current.ShareDataChanged += Current_ShareDataChanged;
        }

        private void Current_ShareDataChanged()
        {
            if (ViewModel.AppTheme == ElementTheme.Dark)
            { 
                DayMode();
            }
            else
            {
                NightMode();
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
            
        }


        private async void pivot_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (EssayResult != null)
            {
                switch (pivot.SelectedIndex)
                {
                    case 0:
                        if (!IsEssayLoaded)
                        {
                            await ViewModel.GenerateHtmlString();
                            //await GenerateEssayHtml();
                        } 
                        IsEssayLoaded = true;
                        break;
                    case 1:
                        if (!IsCommentLoaded)
                        {
                            ViewModel.RefreshComments();
                            ViewModel.GenerateCommentString();
                            CommentsCollection = new EssayCommentsCollection(EssayResult.ContentId);
                        }
                        IsCommentLoaded = true;
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
        }

        /// <summary>
        /// 收藏文章
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void likeBtn_Click(object sender, RoutedEventArgs e)
        {
            if (EssayResult != null)
            {
                DataShareManager.Current.UpdateFavoriteEssayList(EssayResult);
            }
        }

        private async void saveAppBar_Click(object sender, RoutedEventArgs e)
        {
            UIHelper.ShowToast(GlobalStringLoader.GetString("Saving"));
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
                savePicker.FileTypeChoices.Add("Picture", new List<string>() { ".jpg", ".png", ".gif" });
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
                            UIHelper.ShowToast(GlobalStringLoader.GetString("PictureSaved"));
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
            await Launcher.LaunchUriAsync(new Uri(ViewModel.OriginUri));
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

         
         
        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            EssayResult = e.Parameter as Essay;

            if (EssayResult != null)
            {
                IsEssayLoaded = false;
                IsCommentLoaded = false;

                ViewModel.Essay = EssayResult;
                //DataContext = ViewModel = new EssayDetailViewModel(EssayResult);  
                if (EssayResult.ContentId.Equals("0"))
                {
                    webView.Navigate(new Uri(EssayResult.ContentURL));
                }
                //pivot_SelectionChanged(pivot, null);
                //else
                //{
                //    Refresh();
                //}
            }
        }

        protected override void OnNavigatedFrom(NavigationEventArgs e)
        {
            base.OnNavigatedFrom(e);
            DataContext = null;
        }
         
        public async Task GenerateEssayHtml()
        {
            Uri uri = new WebView().BuildLocalStreamUri("id", "/Content.html");
            HtmlStreamUriResolver resolver = new HtmlStreamUriResolver();
            webView.NavigateToLocalStreamUri(uri, resolver);

            News news = await ApiService.Instance.ReadEssay(EssayResult.ContentId);
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
                CurrentImageUrl = args.Uri.ToString();
                Debug.WriteLine("ClickImageUrl：" + CurrentImageUrl);
                GetAllPictures();
                
                imageFlipView.Visibility = Visibility.Visible;
                appBar.Visibility = Visibility.Visible;

                if (Images != null)
                {
                    for (int i = 0; i < Images.Count; i++)
                    {
                        if(Images[i].hdsrc!=null && CurrentImageUrl.Contains(Images[i].hdsrc))
                        {
                            imageFlipView.SelectedIndex = i;
                        }
                        else if (Images[i].src!=null && CurrentImageUrl.Contains(Images[i].src) )
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

        #region Comment Webview
        private void commentWebView_NavigationStarting(WebView sender, WebViewNavigationStartingEventArgs args)
        {
            progress.IsActive = true;
        }

        private void commentWebView_NavigationCompleted(WebView sender, WebViewNavigationCompletedEventArgs args)
        {
            progress.IsActive = false;
        }
        #endregion
         
    }
}
