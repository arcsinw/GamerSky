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
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Media.Imaging;
using Windows.UI.Xaml.Navigation;
using 游民星空.Core.Http;
using 游民星空.Core.Model;

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

            foreach (var item in adStart)
            {
                AdStarts.Add(item);
            }
        }

        private void Image_Holding(object sender, HoldingRoutedEventArgs e)
        {
            FlyoutBase.ShowAttachedFlyout((FrameworkElement)sender);
            //var bitmap = e.OriginalSource as BitmapImage;
        }

        private void MenuFlyoutItem_Click(object sender, RoutedEventArgs e)
        {
            MenuFlyoutItem item = sender as MenuFlyoutItem;

            if (item != null)
            {
                Image img = item.DataContext as Image;

               
            }
        }
    }
}
