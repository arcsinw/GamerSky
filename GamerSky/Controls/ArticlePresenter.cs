using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Threading.Tasks;
using Windows.Foundation;
using Windows.UI;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Navigation;

namespace GamerSky.Controls
{
    public class ArticlePresenter : UserControl
    {
        #region Events
        public event TypedEventHandler<WebView, System.Object> ContainsFullScreenElementChanged;
        public event TypedEventHandler<WebView, WebViewContentLoadingEventArgs> ContentLoading;
        public event TypedEventHandler<WebView, WebViewDOMContentLoadedEventArgs> DOMContentLoaded;
        public event TypedEventHandler<WebView, WebViewContentLoadingEventArgs> FrameContentLoading;
        public event TypedEventHandler<WebView, WebViewDOMContentLoadedEventArgs> FrameDOMContentLoaded;
        public event TypedEventHandler<WebView, WebViewNavigationCompletedEventArgs> FrameNavigationCompleted;
        public event TypedEventHandler<WebView, WebViewNavigationStartingEventArgs> FrameNavigationStarting;
        public event LoadCompletedEventHandler LoadCompleted;
        public event TypedEventHandler<WebView, WebViewLongRunningScriptDetectedEventArgs> LongRunningScriptDetected;
        public event TypedEventHandler<WebView, WebViewNavigationCompletedEventArgs> NavigationCompleted;
        public event WebViewNavigationFailedEventHandler NavigationFailed;
        public event TypedEventHandler<WebView, WebViewNavigationStartingEventArgs> NavigationStarting;
        public event TypedEventHandler<WebView, WebViewNewWindowRequestedEventArgs> NewWindowRequested;
        public event TypedEventHandler<WebView, WebViewPermissionRequestedEventArgs> PermissionRequested;
        public event NotifyEventHandler ScriptNotify;
        public event TypedEventHandler<WebView, System.Object> UnsafeContentWarningDisplaying;
        public event TypedEventHandler<WebView, WebViewUnsupportedUriSchemeIdentifiedEventArgs> UnsupportedUriSchemeIdentified;
        public event TypedEventHandler<WebView, WebViewUnviewableContentIdentifiedEventArgs> UnviewableContentIdentified;
        #endregion

        #region Properties
        public static readonly DependencyProperty HtmlProperty = DependencyProperty.Register("Html", typeof(string), typeof(ArticlePresenter), new PropertyMetadata(default(string), HtmlChangedCallback));

        public string Html
        {
            get { return (string)GetValue(HtmlProperty); }
            set { SetValue(HtmlProperty, value); }
        }

        private static void HtmlChangedCallback(DependencyObject dependencyObject, DependencyPropertyChangedEventArgs eventArgs)
        {
            var articlePresenter = (ArticlePresenter)dependencyObject;
            if (!articlePresenter.Html.Equals(eventArgs.NewValue))
            {
                AssignHtml(eventArgs.NewValue, articlePresenter);
            }
        }


        public NavigateTypeEnum NavigationType { get; set; } = NavigateTypeEnum.String;


        #endregion

        #region Deal with WebView
        private WebView InitializeWebView()
        {
            var webView = new WebView(WebViewExecutionMode.SameThread) { DefaultBackgroundColor = Colors.Transparent };
            webView.NavigationStarting += WebView_NavigationStarting;
            webView.NavigationCompleted += WebView_NavigationCompleted;
            webView.ScriptNotify += WebView_ScriptNotify;
            webView.UnsupportedUriSchemeIdentified += WebView_UnsupportedUriSchemeIdentified;
            webView.NewWindowRequested += WebView_NewWindowRequested;

            // webView.NavigationCompleted += ((sender, e) => ReadingWebViewNavigationCompleted?.Invoke(this, new ReadingWebViewNavigationCompletedEventArgs(e)));

            return webView;
        }

        private void WebView_NavigationCompleted(WebView sender, WebViewNavigationCompletedEventArgs args)
        {
            NavigationCompleted?.Invoke(sender, args);
            Debug.WriteLine("NavigationCompleted");
        }

        private void WebView_NewWindowRequested(WebView sender, WebViewNewWindowRequestedEventArgs args)
        {
            NewWindowRequested?.Invoke(sender, args);
            Debug.WriteLine("NewWindowRequest : " + args.Uri);
        }

        private void WebView_UnsupportedUriSchemeIdentified(WebView sender, WebViewUnsupportedUriSchemeIdentifiedEventArgs args)
        {
            Debug.WriteLine($"Unsupported Uri Clicked.  {args.Uri.ToString()}");

            if (args.Uri.ToString().StartsWith("{gsapp") || args.Uri.ToString().StartsWith("gsapp"))
            {
                UnsupportedUriSchemeIdentified?.Invoke(sender, args);
            }
            else
            {
                args.Handled = true;
            }
        }

        private void WebView_ScriptNotify(object sender, NotifyEventArgs e)
        {
            ScriptNotify?.Invoke(sender, e);
            Debug.WriteLine("Script Fired");
        }

        private void WebView_NavigationStarting(WebView sender, WebViewNavigationStartingEventArgs args)
        {
            NavigationStarting?.Invoke(sender, args);
            Debug.WriteLine("Webpage Navigating.");
        }
        #endregion

        #region Public methods

        /// <summary>
        /// Invoke js
        /// </summary>
        /// <param name="funcName">function name of js</param>
        /// <param name="args">arguments of js </param>
        public async Task<string> InvokeScriptAsync(string funcName, IEnumerable<string> args)
        {
            try
            {
                return await WebView?.InvokeScriptAsync(funcName, args);
            }
            catch (Exception ex)
            {
                string errorText = string.Empty;
                switch (ex.HResult)
                {
                    case unchecked((int)0x80020006):
                        errorText = $"There is no function called {funcName}";
                        break;
                    case unchecked((int)0x80020101):
                        errorText = $"A JavaScript error or exception occured while executing the function {funcName}";
                        break;
                    case unchecked((int)0x800a138a):
                        errorText = $"{funcName} is not a function";
                        break;
                    default:
                        // Some other error occurred.
                        errorText = funcName + ex.Message;
                        break;
                }
                Debug.WriteLine(errorText);
                return string.Empty;
            }
        }


        #endregion 

        //public NavigateTypeEnum NavigateType
        //{
        //    get { return (NavigateTypeEnum)GetValue(NavigateTypeProperty); }
        //    set { SetValue(NavigateTypeProperty, value); }
        //}

        //// Using a DependencyProperty as the backing store for NavigateType.  This enables animation, styling, binding, etc...
        //public static readonly DependencyProperty NavigateTypeProperty =
        //    DependencyProperty.Register("NavigateType", typeof(NavigateTypeEnum), typeof(ArticlePresenter), new PropertyMetadata(NavigateTypeEnum.String));


        private Grid GridContainer { get; set; }
        private WebView WebView { get; set; }

        public ArticlePresenter()
        {
            InitializeUserControl();
            Loaded += ArticlePresenter_Loaded;
            Unloaded += ArticlePresenter_Unloaded;
        }

        private void InitializeUserControl()
        {
            GridContainer = InitializeLayout();
            WebView = InitializeWebView();

            Grid.SetRow(WebView, 0);
            GridContainer.Children.Add(WebView);

            Content = GridContainer;
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
            if (newValue != null && !string.IsNullOrEmpty((string)newValue))
            {
                switch (articlePresenter.NavigationType)
                {
                    case NavigateTypeEnum.Uri:
                        articlePresenter.WebView?.Navigate(new Uri(newValue.ToString()));
                        break;
                    case NavigateTypeEnum.String:
                    default:
                        articlePresenter.WebView?.NavigateToString(newValue.ToString());
                        break;
                }
            }
        }
    }

    public enum NavigateTypeEnum
    {
        String,
        Uri,
    }
}