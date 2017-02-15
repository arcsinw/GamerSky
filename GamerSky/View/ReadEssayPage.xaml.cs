﻿using GamerSky.Core.Helper;
using GamerSky.Core.Http;
using GamerSky.Core.Model;
using GamerSky.Core.ViewModel;
using GamerSky.Helper;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.Storage;
using Windows.Storage.Pickers;
using Windows.Storage.Provider;
using Windows.Storage.Streams;
using Windows.System;
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
        private bool _isContentLoaded = false;
        private bool _isCommentLoaded = false;

        private Essay _currentEssay;

        public ObservableCollection<JsImage> Images { get; set; } = new ObservableCollection<JsImage>();

        public ReadEssayPage()
        {
            InitializeComponent();

            DataShareManager.Current.ShareDataChanged += Current_ShareDataChanged;
        }

        private void Current_ShareDataChanged()
        {
            if (ViewModel.AppTheme == ElementTheme.Dark)
            {
                NightMode();
            }
            else
            {
                DayMode(); 
            }
            if(ViewModel.IsNoImgMode)
            {
                NoImageMode();
            }
            SetFontSize(ViewModel.EssayFontSize.ToString());
        }
        
        protected override void OnNavigatedFrom(NavigationEventArgs e)
        {
            ViewModel.ContentHtml = string.Empty;
            ViewModel.Comments?.Clear();
        }

        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            Essay essay = (Essay)e.Parameter;
            this._currentEssay = essay;
            //ViewModel.SetCurrentEssay(essay,flipView.SelectedIndex);
            _isCommentLoaded = false;
            _isContentLoaded = false;
            ViewModel.SetCurrentContentId(essay.ContentId, flipView.SelectedIndex);
        }

        #region Js invoke  
        /// <summary>
        /// 设置夜间模式
        /// </summary>
        public async void NightMode()
        {
            await articlePresenter.InvokeScriptAsync("NightMode", new[] { "" });
            UIHelper.ShowStatusBar();
        }

        /// <summary>
        /// 日间模式
        /// </summary>
        public async void DayMode()
        {
            await articlePresenter.InvokeScriptAsync("DayMode", new[] { "" });
            UIHelper.ShowStatusBar();
        }

        public async void GetAllPictures()
        {
            string result = await articlePresenter.InvokeScriptAsync("GetAllPictures", new[] { "" });
            var imgs = JsonHelper.Deserlialize<List<JsImage>>(result);
            if (imgs != null)
            {
                imgs.ForEach(x => Images.Add(x));
            }
        }

        public async void NoImageMode()
        {
            await articlePresenter.InvokeScriptAsync("NoImageMode", new[] { "" });
        }

        public async void SetFontSize(string fontSize)
        {
            await articlePresenter.InvokeScriptAsync("SetFontSize", new[] { fontSize });
        }
        #endregion
         
        #region Flyout's operation
        private void nightCheckBox_Checked(object sender, RoutedEventArgs e)
        {
            //DataShareManager.Current.UpdateAPPTheme(true);
            NightMode();
        }

        private void nightCheckBox_Unchecked(object sender, RoutedEventArgs e)
        { 
            DayMode();
            //DataShareManager.Current.UpdateAPPTheme(false);
        }

        private async void edgeListViewItem_Tapped(object sender, TappedRoutedEventArgs e)
        {
            if (!string.IsNullOrEmpty(ViewModel._originUri))
            {
                await Launcher.LaunchUriAsync(new Uri(ViewModel._originUri));
            }
        }

        private void refreshButton_Click(object sender, RoutedEventArgs e)
        {
            ViewModel.RefreshContent();
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
                    Debug.WriteLine(ex.Message);
                }
            }
        }
         
        

        private void imageFlipView_Tapped(object sender, TappedRoutedEventArgs e)
        {
            imageFlipView.Visibility = Visibility.Collapsed;
            commandBar.Visibility = Visibility.Collapsed;
        }

        private void likeBtn_Click(object sender, RoutedEventArgs e)
        {
            if (_currentEssay != null)
            {
                DataShareManager.Current.UpdateFavoriteEssayList(_currentEssay);
            }
        }
          
        private void noImg_Checked(object sender, RoutedEventArgs e)
        {
            DataShareManager.Current.UpdateNoImagesMode(true);
            NoImageMode();
        }

        private void noImg_Unchecked(object sender, RoutedEventArgs e)
        {
            DataShareManager.Current.UpdateNoImagesMode(false);
        }

        private void articlePresenter_NavigationStarting(WebView sender, WebViewNavigationStartingEventArgs args)
        {
            if(ViewModel.IsNoImgMode)
            {
                NoImageMode();
            }
        }
          
        private void fontSlider_ValueChanged(object sender, RangeBaseValueChangedEventArgs e)
        {
            SetFontSize(e.NewValue.ToString());
        }
        #endregion

        private async void flipView_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            //if (ViewModel.CurrentEssay == null) return;
            if (string.IsNullOrEmpty(ViewModel.ContentId)) return;
            switch (flipView.SelectedIndex)
            {
                case 0:
                    if (!_isContentLoaded)
                    {
                        await ViewModel.GenerateHtmlString(ViewModel.ContentId);
                    }
                    _isContentLoaded = true;
                    break;
                case 1:
                    if (!_isCommentLoaded)
                    {
                        ViewModel.GenerateComments(ViewModel.ContentId);
                    }
                    _isCommentLoaded = true;
                    break;
            }
        }

        private void articlePresenter_NavigationCompleted(WebView sender, WebViewNavigationCompletedEventArgs args)
        {
            if (ViewModel.AppTheme == ElementTheme.Dark)
            {
                NightMode();
            }
            else
            {
                DayMode();
            }
            GetAllPictures();
        }

        private void articlePresenter_NewWindowRequested(WebView sender, WebViewNewWindowRequestedEventArgs args)
        {
            args.Handled = true;
            string tmp = args.Uri.OriginalString;
            if (tmp.EndsWith(".jpg") ||
                tmp.EndsWith(".png") ||
                tmp.EndsWith(".bmp") ||
                tmp.EndsWith(".gif"))
            {
                var result = Images.Where(x => x.hdsrc.Equals(tmp));

                if (result != null)
                {
                    imageFlipView.SelectedIndex = result.First().index;
                    imageFlipView.Visibility = Visibility.Visible;
                    commandBar.Visibility = Visibility.Visible;
                }
            }
        }

        private void articlePresenter_ScriptNotify(object sender, NotifyEventArgs e)
        {
            Debug.WriteLine(e.Value);
            if (e.Value.Equals("gestures:goback"))
            {
                NavigationHelper.GoBack();
            }

            if (!string.IsNullOrEmpty(e.Value))
            {
                _isCommentLoaded = false;
                _isContentLoaded = false;
                ViewModel.SetCurrentContentId(e.Value, flipView.SelectedIndex);
            }
        }
    }
}
