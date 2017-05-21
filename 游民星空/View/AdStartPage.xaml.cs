using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.IO;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.Storage;
using Windows.Storage.Pickers;
using Windows.Storage.Provider;
using Windows.Storage.Streams;
using Windows.UI.Popups;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Media.Imaging;
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
    public sealed partial class AdStartPage : Page , INotifyPropertyChanged
    {
        public AdStartPage()
        {
            this.InitializeComponent();

            apiService = new ApiService();

            AdStarts = new ObservableCollection<AdStart>();
            LoadData();

            AppTheme = DataShareManager.Current.AppTheme;
            DataShareManager.Current.ShareDataChanged += Current_ShareDataChanged;
        }

        #region 九幽的数据统计
        protected override void OnNavigatedFrom(NavigationEventArgs e)
        {
            JYHelper.TracePageEnd("AdStartPage");
        }

        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            JYHelper.TracePageStart("AdStartPage");
        }
        #endregion

        private void Current_ShareDataChanged()
        {
            AppTheme = DataShareManager.Current.AppTheme;
        }

        private ElementTheme appTheme;
        public ElementTheme AppTheme
        {
            get
            {
                return appTheme;
            }
            set
            {
                appTheme = value;
                OnPropertyChanged();
            }
        }

        private ApiService apiService;

        #region INotifyPropertyChanged Member

        public event PropertyChangedEventHandler PropertyChanged;

        private void OnPropertyChanged([CallerMemberName]string propertyName = "")
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        } 

        #endregion

        private ObservableCollection<AdStart> adStarts;
        /// <summary>
        /// 启动图
        /// </summary>
        public ObservableCollection<AdStart> AdStarts
        {
            get
            {
                return adStarts;
            }
            set
            {
                adStarts = value;
                OnPropertyChanged();
            }
        }

        public async void LoadData()
        {
            List<AdStart> adStart = await apiService.GetStartImage();
            if (adStart != null)
            {
                foreach (var item in adStart)
                {
                    AdStarts.Add(item);
                }
            }
        }

        private void Image_Holding(object sender, HoldingRoutedEventArgs e)
        {
            var img = sender as FrameworkElement;
            if (img != null)
            {
                MenuFlyout menuFlyout = FlyoutBase.GetAttachedFlyout(sender as FrameworkElement) as MenuFlyout;
                menuFlyout.ShowAt(img,e.GetPosition(img));
            }
        }

        private async void MenuFlyoutItem_Click(object sender, RoutedEventArgs e)
        {
            MenuFlyoutItem item = sender as MenuFlyoutItem;

            if (item != null)
            {
                AdStart adStart = item.DataContext as AdStart;

                

                FileSavePicker savePicker = new FileSavePicker();
                savePicker.SuggestedFileName = "游民壁纸_" + DateTime.Now.Month+DateTime.Now.Day;
                savePicker.DefaultFileExtension = ".jpg";
                savePicker.FileTypeChoices.Add("Picture", new List<string>() { ".jpg", ".png" });
                savePicker.SuggestedStartLocation = PickerLocationId.PicturesLibrary;
                savePicker.ContinuationData["Op"] = "ImgSave";
                 
             
                StorageFile file = await savePicker.PickSaveFileAsync();
                if(file!= null)
                {
                    
                    CachedFileManager.DeferUpdates(file);

                    try
                    {
                        using (Stream stream = await file.OpenStreamForWriteAsync())
                        {
                            IBuffer buffer = await HttpBaseService.SendGetRequestAsBytes(adStart.picAdress);
                            stream.Write(buffer.ToArray(), 0, (int)buffer.Length);
                            await stream.FlushAsync();
                        }
                        FileUpdateStatus updateStatus = await CachedFileManager.CompleteUpdatesAsync(file);
                        if (updateStatus == FileUpdateStatus.Complete)
                        {
                            await new MessageDialog("图片已保存").ShowAsync();
                        }
                    }catch(Exception ex)
                    {
                        JYHelper.TraceError("AdStartPage.xaml"+ex.Message);
    
                    }
                }
            }
        }
    }
}
