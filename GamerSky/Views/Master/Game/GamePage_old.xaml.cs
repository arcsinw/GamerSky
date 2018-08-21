﻿using System;
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

namespace GamerSky.Views
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class GamePage : Page
    {
        public GamePage()
        {
            this.InitializeComponent();
        }

        public void Show()
        {
            MainPage.Current.ShowModuleGrid();
        }

        public void Hide()
        {
            MainPage.Current.HideModuleGrid();
        }

        private void Page_SizeChanged(object sender, SizeChangedEventArgs e)
        {
            flipView.Height = e.NewSize.Width * 9 / 16;
        }


        

    }

   
}
