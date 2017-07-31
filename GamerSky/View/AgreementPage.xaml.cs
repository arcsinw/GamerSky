using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.IO;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices.WindowsRuntime;
using System.Threading.Tasks;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.Storage;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;
using GamerSky.ViewModel;

namespace GamerSky.View
{
    /// <summary>
    /// 可用于自身或导航至 Frame 内部的空白页。
    /// </summary>
    public sealed partial class AgreementPage : Page
    {
        public AgreementPage()
        {
            this.InitializeComponent(); 
        }
         

        public void Back()
        {
            if(this.Frame.CanGoBack)
            {
                this.Frame.GoBack();
            }
        }

        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            var parameter = e.Parameter as string;
            if(parameter== null)
            {
                titleTextBlock.Text = "协议";
                GetAgreement();
            }
            else if (parameter.Equals("Version"))
            {
                titleTextBlock.Text = "更新历史";
                GetVersionHistory();
            }
        }

        /// <summary>
        /// 获取协议
        /// </summary>
        public async void GetAgreement()
        {
            string text = await GetTextFromFile(new Uri ("ms-appx:///Data/Agreement.txt"));
            textBlock.Text = text;
        }

        /// <summary>
        /// 获取更新历史
        /// </summary>
        public async void GetVersionHistory()
        {
            string text = await GetTextFromFile(new Uri("ms-appx:///Data/VersionHistory.txt"));
            textBlock.Text = text;
        }

        /// <summary>
        /// 从文件中获取文本
        /// </summary>
        /// <param name="uri"></param>
        /// <returns></returns>
        private async Task<string> GetTextFromFile(Uri uri)
        {
            StorageFile file = await StorageFile.GetFileFromApplicationUriAsync(uri);
            string text = await FileIO.ReadTextAsync(file);
            return text;
        }
         
         
    }
}
