using GamerSky.Core.Helper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;


namespace GamerSky.Extensions
{
    /// <summary>
    /// 扩展
    /// </summary>
    public class ExtensionHelper
    {

        // "HtmlString" attached property for a WebView
        public static readonly DependencyProperty HtmlStringProperty =
           DependencyProperty.RegisterAttached("HtmlString", typeof(string), typeof(ExtensionHelper), new PropertyMetadata("", OnHtmlStringChanged));

        // Getter and Setter
        public static string GetHtmlString(DependencyObject obj) { return (string)obj.GetValue(HtmlStringProperty); }
        public static void SetHtmlString(DependencyObject obj, string value) { obj.SetValue(HtmlStringProperty, value); }

        // Handler for property changes in the DataContext : set the WebView
        private static void OnHtmlStringChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            WebView wv = d as WebView;
            if (wv != null)
            {
                wv.NavigateToString((string)e.NewValue);
            }
        }




        //// Getter and Setter
        //public static Uri GetLocalStreamUri(DependencyObject obj) { return (Uri)obj.GetValue(LocalStreamUriProperty); }
        //public static void SetLocalStreamUri(DependencyObject obj, string value) { obj.SetValue(LocalStreamUriProperty, value); }

        //// Using a DependencyProperty as the backing store for LocalStreamUri.  This enables animation, styling, binding, etc...
        //public static readonly DependencyProperty LocalStreamUriProperty =
        //    DependencyProperty.Register("LocalStreamUri", typeof(Uri), typeof(WebView), new PropertyMetadata(new Uri(""), OnLocalStreamUriChanged));

        //private static void OnLocalStreamUriChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        //{
        //    WebView wv = d as WebView;
        //    if(wv != null)
        //    {
        //        Uri uri = wv.BuildLocalStreamUri("gamersky",(string) e.NewValue);
        //        wv.NavigateToLocalStreamUri(uri, new HtmlStreamUriResolver());
        //    }
        //}


    }
}
