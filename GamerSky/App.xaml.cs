using GamerSky.Helper;
using GamerSky.View;
using Microsoft.Azure.Mobile;
using Microsoft.Azure.Mobile.Analytics;
using System;
using Windows.ApplicationModel;
using Windows.ApplicationModel.Activation;
using Windows.System;
using Windows.UI.Popups;
using Windows.UI.StartScreen;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Navigation;

namespace GamerSky
{
    /// <summary>
    /// 提供特定于应用程序的行为，以补充默认的应用程序类。
    /// </summary>
    sealed partial class App : Application
    {
        /// <summary>
        /// 初始化单一实例应用程序对象。这是执行的创作代码的第一行，
        /// 已执行，逻辑上等同于 main() 或 WinMain()。
        /// </summary>
        public App()
        {
            this.InitializeComponent();
            this.Suspending += OnSuspending;
            
            UnhandledException += OnUnhandledException;
            
        }

        #region Handle unhandled exception

        private async void OnUnhandledException(object sender, Windows.UI.Xaml.UnhandledExceptionEventArgs e)
        {
            e.Handled = true;
            await new MessageDialog("Application Unhandled Exception:\r\n" + GetExceptionDetailMessage(e.Exception), "(╯‵□′)╯︵┻━┻").ShowAsync();
        }

        /// <summary>
        /// Should be called from OnActivated and OnLaunched
        /// </summary>
        private void RegisterExceptionHandlingSynchronizationContext()
        {
            ExceptionHandlingSynchronizationContext
                .Register()
                .UnhandledException += SynchronizationContext_UnhandledException;
        }

        private async void SynchronizationContext_UnhandledException(object sender, Helper.UnhandledExceptionEventArgs e)
        {
            e.Handled = true;
            MessageDialog dialog = new MessageDialog(GetExceptionDetailMessage(e.Exception), GlobalStringLoader.GetString("USendEmail"));
            
            dialog.Commands.Add(new UICommand("OK", async (a) =>
            {
                string subject = "GamerSky.x's Exception";
                string body = GetExceptionDetailMessage(e.Exception);
                string address = "wangx86@live.com";
                var mailto = new Uri($"mailto:{address}?subject={subject}&body={body}");
                await Launcher.LaunchUriAsync(mailto);
            }));

            dialog.Commands.Add(new UICommand("Cancle"));
             
            await dialog.ShowAsync(); 
        }
         

        // https://github.com/ljw1004/async-exception-stacktrace
        private string GetExceptionDetailMessage(Exception ex)
        {
            return $"{ex.Message}\r\n{ex.StackTraceEx()}";
        }

        #endregion

        public async void CreateJumpList()
        {
            JumpList jumpList = await JumpList.LoadCurrentAsync();
            jumpList.Items.Add(JumpListItem.CreateWithArguments("HotNews", "News"));
            await jumpList.SaveAsync();
        }

        /// <summary>
        /// 在应用程序由最终用户正常启动时进行调用。
        /// 将在启动应用程序以打开特定文件等情况下使用。
        /// </summary>
        /// <param name="e">有关启动请求和过程的详细信息。</param>
        protected override void OnLaunched(LaunchActivatedEventArgs e)
        {
            Frame rootFrame = Window.Current.Content as Frame;

            // 不要在窗口已包含内容时重复应用程序初始化，
            // 只需确保窗口处于活动状态
            if (rootFrame == null)
            {
                // 创建要充当导航上下文的框架，并导航到第一页
                rootFrame = new Frame();

                rootFrame.NavigationFailed += OnNavigationFailed;

                if (e.PreviousExecutionState == ApplicationExecutionState.Terminated)
                {
                    //TODO: 从之前挂起的应用程序加载状态
                }

                // 将框架放在当前窗口中
                Window.Current.Content = rootFrame;
            }

            if (rootFrame.Content == null)
            {
                // 当导航堆栈尚未还原时，导航到第一页，
                // 并通过将所需信息作为导航参数传入来配置
                // 参数
                rootFrame.Navigate(typeof(MasterDetailPage), e.Arguments);
            }
            // 确保当前窗口处于活动状态
            Window.Current.Activate();

            MobileCenter.Start("5dd1c060-7a76-4dc2-b261-4476dee882b0", typeof(Analytics));
            RegisterExceptionHandlingSynchronizationContext();

        }

        /// <summary>
        /// 导航到特定页失败时调用
        /// </summary>
        ///<param name="sender">导航失败的框架</param>
        ///<param name="e">有关导航失败的详细信息</param>
        void OnNavigationFailed(object sender, NavigationFailedEventArgs e)
        {
            throw new Exception("Failed to load Page " + e.SourcePageType.FullName);
        }

        /// <summary>
        /// 在将要挂起应用程序执行时调用。  在不知道应用程序
        /// 无需知道应用程序会被终止还是会恢复，
        /// 并让内存内容保持不变。
        /// </summary>
        /// <param name="sender">挂起的请求的源。</param>
        /// <param name="e">有关挂起请求的详细信息。</param>
        private void OnSuspending(object sender, SuspendingEventArgs e)
        {
            var deferral = e.SuspendingOperation.GetDeferral();
            //TODO: 保存应用程序状态并停止任何后台活动
            deferral.Complete();
        }

        protected override void OnActivated(IActivatedEventArgs args)
        {
            RegisterExceptionHandlingSynchronizationContext();
        }
    }
}
