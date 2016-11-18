using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.UI;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Documents;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;


namespace GamerSky.Controls
{
    internal class ArticlePresenter : UserControl
    { 
        private Grid GridContainer { get; set; }
        private WebView WebView { get; set; }

        public ArticlePresenter()
        {
            InitializeUserControl();
            Loaded += ArticlePresenter_Loaded;
            Unloaded += ArticlePresenter_Unloaded;
        }


        public static readonly DependencyProperty HtmlProperty = DependencyProperty.Register("Html", typeof(string), typeof(ArticlePresenter), new PropertyMetadata(default(string), HtmlChangedCallback));

        public string Html
        {
            get { return (string)GetValue(HtmlProperty); }
            set { SetValue(HtmlProperty, value); }
        }

        private static void HtmlChangedCallback(DependencyObject dependencyObject, DependencyPropertyChangedEventArgs eventArgs)
        {
            var articlePresenter = (ArticlePresenter)dependencyObject;
            AssignHtml(eventArgs.NewValue, articlePresenter);
        }

        private void InitializeUserControl()
        {
            GridContainer = InitializeLayout();
            WebView = InitializeWebView();

            Grid.SetRow(WebView, 0);
            GridContainer.Children.Add(WebView);

            Content = GridContainer;
        }

        private WebView InitializeWebView()
        {
            var webView = new WebView(WebViewExecutionMode.SameThread) { DefaultBackgroundColor = Colors.Transparent };
            webView.NavigationStarting += WebView_NavigationStarting;
            webView.ScriptNotify += WebView_ScriptNotify;
            webView.UnsupportedUriSchemeIdentified += WebView_UnsupportedUriSchemeIdentified;
            // webView.NavigationCompleted += ((sender, e) => ReadingWebViewNavigationCompleted?.Invoke(this, new ReadingWebViewNavigationCompletedEventArgs(e)));

            return webView;
        }


        private void WebView_UnsupportedUriSchemeIdentified(WebView sender, WebViewUnsupportedUriSchemeIdentifiedEventArgs args)
        {
            Debug.WriteLine("Unsupported Uri Clicked");
        }

        private void WebView_ScriptNotify(object sender, NotifyEventArgs e)
        {
            Debug.WriteLine("Script Fired");
        }

        private void WebView_NavigationStarting(WebView sender, WebViewNavigationStartingEventArgs args)
        {
            Debug.WriteLine("Webpage Navigating.");
        }

        private Grid InitializeLayout()
        {
            var grid = new Grid();
            grid.RowDefinitions.Add(new RowDefinition { Height = new GridLength(1, GridUnitType.Star) });
            return grid;
        }

        private void ArticlePresenter_Unloaded(object sender, RoutedEventArgs e)
        {
            WebView = null;
            GridContainer.Children.Clear();
        }

        private void ArticlePresenter_Loaded(object sender, RoutedEventArgs e)
        {
            AssignHtml(Html, this);
        }

        private static void AssignHtml(object newValue, ArticlePresenter articlePresenter)
        {
            if (newValue != null)
            {
                articlePresenter.WebView?.NavigateToString(newValue.ToString());
            }
        }
    }
}