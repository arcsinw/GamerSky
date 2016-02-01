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
using 游民星空.Core.ViewModel;

// “空白页”项模板在 http://go.microsoft.com/fwlink/?LinkId=234238 上提供

namespace 游民星空.View
{
    /// <summary>
    /// 可用于自身或导航至 Frame 内部的空白页。
    /// </summary>
    public sealed partial class StrategyPage : Page
    {
        public StrategyPage()
        {
            this.InitializeComponent();
        }


        private void Pivot_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if(pivot.SelectedIndex==0)
            {
                ViewModel.LoadFocusStrategys();
            }
            else
            {
                ViewModel.LoadAllStrategys();
            }
        }

        /// <summary>
        /// 下拉刷新
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="args"></param>
        private void PullToRefreshBox_RefreshInvoked(DependencyObject sender, object args)
        {
            if(pivot.SelectedIndex==0)
            {
                ViewModel.RefreshFocusStrategy();
            }
            else
            {
                ViewModel.RefreshAllStrategy();
            }
        }
    }
}
