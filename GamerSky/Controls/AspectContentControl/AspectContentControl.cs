using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Foundation;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Documents;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;


namespace GamerSky.Controls
{
    public class AspectContentControl : ContentControl
    {
        public AspectContentControl()
        {
            this.DefaultStyleKey = typeof(AspectContentControl);
        }

        /// <summary>
        /// 高宽比
        /// </summary>
        public double AspectRatio
        {
            get { return (double)GetValue(AspectRatioProperty); }
            set { SetValue(AspectRatioProperty, value); }
        }

        // Using a DependencyProperty as the backing store for AspectRatio.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty AspectRatioProperty =
            DependencyProperty.Register("AspectRatio", typeof(double), typeof(AspectContentControl), new PropertyMetadata(1.0));



        protected override Size MeasureOverride(Size availableSize)
        {
            return new Size(availableSize.Width, availableSize.Width * AspectRatio);
        }
    }
}
