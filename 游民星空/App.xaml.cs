using JYAnalyticsUniversal;
using System;
using Windows.ApplicationModel;
using Windows.ApplicationModel.Activation;
using Windows.ApplicationModel.Background;
using Windows.ApplicationModel.Store;
using Windows.Foundation.Metadata;
using Windows.Graphics.Display;
using Windows.Phone.UI.Input;
using Windows.UI.Core;
using Windows.UI.Notifications;
using Windows.UI.Popups;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Navigation;
using 游民星空.BackgroundTask;
using 游民星空.Core.Helper;
using 游民星空.Helper;
using 游民星空.View;

namespace 游民星空
{
    /// <summary>
    /// Provides application-specific behavior to supplement the default Application class.
    /// </summary>
    sealed partial class App : Application
    {
        /// <summary>
        /// Initializes the singleton application object.  This is the first line of authored code
        /// executed, and as such is the logical equivalent of main() or WinMain().
        /// </summary>
        public App()
        {
            this.InitializeComponent();
            this.Suspending += OnSuspending;
            this.Resuming += OnResuming;

           
//#if DEBUG
//            licenseInformation = CurrentAppSimulator.LicenseInformation;
//#else
//            licenseInformation = CurrentApp.LicenseInformation;
//#endif
            this.UnhandledException += OnUnhandledException;
            
        }

        

        //#region IAPs
        //        LicenseInformation licenseInformation;

        //        /// <summary>
        //        /// 某个应用内产品是否有效
        //        /// </summary>
        //        /// <param name="id"></param>
        //        /// <returns></returns>
        //        public bool IsFeatureActive(string id)
        //        {
        //            return licenseInformation.ProductLicenses[id].IsActive;
        //        }

        //        public async void RemoveAd()
        //        {
        //            if(!IsFeatureActive(IAPHelper.Remove_Ad))
        //            {
        //                try
        //                {
        //                    await CurrentAppSimulator.RequestProductPurchaseAsync(IAPHelper.Remove_Ad,false);
        //                }
        //                catch(Exception )
        //                {

        //                }
        //            }
        //            else  //已购买
        //            {

        //            }
        //        }

        //#endregion

        private void OnResuming(object sender, object e)
        {
            JYHelper.StartTraceAsync();
        }

        
        private async void OnUnhandledException(object sender, Windows.UI.Xaml.UnhandledExceptionEventArgs e)
        {
            e.Handled = true;
            await new MessageDialog("Application Unhandled Exception:\r\n" + e.Exception.Message).ShowAsync();
        }

#region 处理异步方法里的异常
        /// <summary>
        /// Should be called from OnActivated and OnLaunched
        /// </summary>
        private void RegisterExceptionHandlingSynchronizationContext()
        {
            ExceptionHandlingSynchronizationContext
                .Register()
                .UnhandledException += SynchronizationContext_UnhandledException;
        }

        private async void SynchronizationContext_UnhandledException(object sender, Core.Helper.UnhandledExceptionEventArgs e)
        {
            e.Handled = true;
            await new MessageDialog("Synchronization Context Unhandled Exception:\r\n" + GetExceptionDetailMessage(e.Exception), "这真是令人尴尬,,ԾㅂԾ,,")
                .ShowAsync();
        }
#endregion

#region 获取简短的异常堆栈信息
        private string GetExceptionDetailMessage(Exception e)
        {
            return $"{e.Message}\r\n{e.StackTraceEx()}";
        }
#endregion

        /// <summary>
        /// Invoked when the application is launched normally by the end user.  Other entry points
        /// will be used such as when the application is launched to open a specific file.
        /// </summary>
        /// <param name="e">Details about the launch request and process.</param>
        protected override void OnLaunched(LaunchActivatedEventArgs e)
        {
            ///全局异常处理
            RegisterExceptionHandlingSynchronizationContext();

            

#if DEBUG
            if (System.Diagnostics.Debugger.IsAttached)
            {
                this.DebugSettings.EnableFrameRateCounter = true;
                this.DebugSettings.IsTextPerformanceVisualizationEnabled = true;
            }
#endif

            Frame rootFrame = Window.Current.Content as Frame;

            // 不要在窗口已包含内容时重复应用程序初始化，
            // 只需确保窗口处于活动状态
            if (rootFrame == null)
            {
                // 创建要充当导航上下文的框架，并导航到第一页
                rootFrame = new Frame();

                rootFrame.NavigationFailed += OnNavigationFailed;
                rootFrame.Navigated += OnNavigated;
                if (e.PreviousExecutionState == ApplicationExecutionState.Terminated)
                {
                    //TODO: 从之前挂起的应用程序加载状态
                }

                // 将框架放在当前窗口中
                Window.Current.Content = rootFrame;

                SystemNavigationManager.GetForCurrentView().BackRequested += OnBackRequested;

                if (ApiInformation.IsTypePresent("Windows.Phone.UI.HardwareButtons"))
                {
                    HardwareButtons.BackPressed += OnBackPressed;
                }
                UpdateBackButtonVisibility();
            }

            if (rootFrame.Content == null)
            {
                // 当导航堆栈尚未还原时，导航到第一页，
                // 并通过将所需信息作为导航参数传入来配置
                // 参数
                rootFrame.Navigate(typeof(HomePage), e.Arguments);
            }

            //注册动态磁贴任务
            //LiveTileHelper.RegisterLiveTileTask();
          
            // 确保当前窗口处于活动状态
            Window.Current.Activate();
            JYHelper.StartTraceAsync();
        }
         
        /// <summary>
        /// handle hardware back button press 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void OnBackPressed(object sender, BackPressedEventArgs e)
        {
            var frame = (Frame)Window.Current.Content;
            if (frame.CanGoBack) {
                e.Handled = true;
                frame.GoBack();
            }
        }

        /// <summary>
        /// handle software back button press 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void OnBackRequested(object sender, BackRequestedEventArgs e)
        {
            var frame = (Frame)Window.Current.Content;
            if (frame.CanGoBack) {
                e.Handled = true;
                frame.GoBack();
            }
        }

        private void OnNavigated(object sender, NavigationEventArgs e)
        {
            UpdateBackButtonVisibility();
        }

        /// <summary>
        /// Invoked when Navigation to a certain page fails
        /// </summary>
        /// <param name="sender">The Frame which failed navigation</param>
        /// <param name="e">Details about the navigation failure</param>
        private void OnNavigationFailed(object sender, NavigationFailedEventArgs e)
        {
            throw new Exception("Failed to load Page " + e.SourcePageType.FullName);
        }

        /// <summary>
        /// Invoked when application execution is being suspended.  Application state is saved
        /// without knowing whether the application will be terminated or resumed with the contents
        /// of memory still intact.
        /// </summary>
        /// <param name="sender">The source of the suspend request.</param>
        /// <param name="e">Details about the suspend request.</param>
        private void OnSuspending(object sender, SuspendingEventArgs e)
        {
            var deferral = e.SuspendingOperation.GetDeferral();
            //TODO: Save application state and stop any background activity
            JYHelper.EndTraceAsync();
            deferral.Complete();
        }

        /// <summary>
        /// 更新标题栏上后退键显示状态
        /// </summary>
        private void UpdateBackButtonVisibility()
        {
            var frame = (Frame)Window.Current.Content;

            var visibility = AppViewBackButtonVisibility.Collapsed;
            if (frame.CanGoBack) {
                visibility = AppViewBackButtonVisibility.Visible;
            }

            SystemNavigationManager.GetForCurrentView().AppViewBackButtonVisibility = visibility;
        }

        protected override void OnActivated(IActivatedEventArgs args)
        {
            RegisterExceptionHandlingSynchronizationContext();

            base.OnActivated(args);
            JYHelper.StartTraceAsync();
        }
         
    }
}
