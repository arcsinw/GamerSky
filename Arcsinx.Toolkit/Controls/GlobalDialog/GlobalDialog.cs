using Arcsinx.Toolkit.Interfaces;
using System;
using System.Numerics;
using System.Threading.Tasks;
using Windows.ApplicationModel;
using Windows.UI.Composition;
using Windows.UI.Core;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Hosting;
using Windows.UI.Xaml.Input;


namespace Arcsinx.Toolkit.Controls
{
    /// <summary>
    /// GlobalDialog
    /// </summary>
    public class GlobalDialog : ContentControl
    {
        /// <summary>
        /// Listener
        /// </summary>
        private static IBackKeyPressManager Listener { get; set; }

        /// <summary>
        /// CustomTitlebar
        /// </summary>
        private static ICustomTitleBar CustomTitlebar { get; set; }

        private static Panel Container { get; set; }

        /// <summary>
        /// CanNotClose
        /// </summary>
        public static bool CanNotClose { get; set; } = false;


        private Compositor _compositor;
        private Visual _rootGridVisual;
        private Visual _maskBorderVisual;
        private Visual _dialogVisual;

        private Grid _rootGrid;
        private Border _maskBorder;
        private ContentPresenter _dialogPresenter;

        // Provide the method to solve getting Storyboard before OnApplyTemplate() execute problem.
        private TaskCompletionSource<int> _loadedTcs;
        private TaskCompletionSource<int> _showTcs;

        /// <summary>
        /// HideCompleted
        /// </summary>
        public event EventHandler HideCompleted;

        /// <summary>
        /// ClickHide
        /// </summary>
        public bool ClickHide { get; set; } = true;

        /// <summary>
        /// Initializes a new instance of the <see cref="GlobalDialog"/> class.
        /// </summary>
        public GlobalDialog()
        {
            this.DefaultStyleKey = typeof(GlobalDialog);
            if (!DesignMode.DesignModeEnabled)
            {
                if (Container == null)
                {
                    throw new NullReferenceException("Please Intialize GlobalDialog First!");
                }

                Container.Children.Add(this);
                _loadedTcs = new TaskCompletionSource<int>();
                
                this.SizeChanged += GlobalDialog_SizeChanged;
            }
        }

        private void GlobalDialog_SizeChanged(object sender, SizeChangedEventArgs e)
        {
            if (e.PreviousSize.Height == 0 && e.NewSize.Height != 0) 
            {
                _loadedTcs.SetResult(0);
            }
        }

        /// <summary>
        /// OnApplyTemplate
        /// </summary>
        protected override void OnApplyTemplate()
        {
            base.OnApplyTemplate();
            _rootGrid = GetTemplateChild<Grid>("RootGrid");
            _maskBorder = GetTemplateChild<Border>("MaskBorder");
            _dialogPresenter = GetTemplateChild<ContentPresenter>("DialogPresenter");
            if (!DesignMode.DesignModeEnabled)
            {
                InitializeComposition();
            }

            _rootGrid.Tapped += _rootGrid_Tapped;
        }


        #region Private Method

        private void InitializeComposition()
        {
            _compositor = ElementCompositionPreview.GetElementVisual(this).Compositor;
            _rootGridVisual = ElementCompositionPreview.GetElementVisual(_rootGrid);
            _maskBorderVisual = ElementCompositionPreview.GetElementVisual(_maskBorder);
            _dialogVisual = ElementCompositionPreview.GetElementVisual(_dialogPresenter);

            _rootGridVisual.IsVisible = false;
            _dialogVisual.Scale = new Vector3(1.3f, 1.3f, 0f);
            _maskBorderVisual.Opacity = 0;
        }

        #endregion

        #region Event

        private void _rootGrid_Tapped(object sender, TappedRoutedEventArgs e)
        {
            if (ClickHide)
            {
                this.Hide();
            }
        }


        #endregion

        #region Public Method

        /// <summary>
        /// Show
        /// </summary>
        public async void Show()
        {
            await _loadedTcs.Task;

            if (CustomTitlebar != null)
            {
                CustomTitlebar.BackRequested += CustomTitlebar_BackRequested;
            }

            // 自定义TitleBar在平板模式下仍然需要这个事件
            SystemNavigationManager.GetForCurrentView().BackRequested += View_BackRequested;
            //await Task.Delay(500);

            if (Listener != null)
            {
                Listener.UnRegisterBackKeyPress();
            }

            _rootGridVisual.IsVisible = true;

            var scaleAnimation = _compositor.CreateVector3KeyFrameAnimation();
            scaleAnimation.InsertKeyFrame(0f, new Vector3(1.3f, 1.3f, 0f));
            scaleAnimation.InsertKeyFrame(1f, new Vector3(1f, 1f, 0f));
            scaleAnimation.Duration = TimeSpan.FromMilliseconds(400);
            _dialogVisual.CenterPoint = new Vector3((float)_dialogPresenter.ActualWidth / 2, (float)_dialogPresenter.ActualHeight / 2, 0f);
            _dialogVisual.StartAnimation("Scale", scaleAnimation);


            var opacityAnimation = _compositor.CreateScalarKeyFrameAnimation();
            opacityAnimation.InsertKeyFrame(0f, 0f, _compositor.CreateLinearEasingFunction());
            opacityAnimation.InsertKeyFrame(1f, 1f);
            opacityAnimation.Duration = TimeSpan.FromMilliseconds(300);
            _maskBorderVisual.StartAnimation("Opacity", opacityAnimation);
        }

        /// <summary>
        /// ShowAsync
        /// </summary>
        /// <returns>Task</returns>
        public async Task ShowAsync()
        {
            await _loadedTcs.Task;

            _showTcs = new TaskCompletionSource<int>();


            if (CustomTitlebar != null)
            {
                CustomTitlebar.BackRequested += CustomTitlebar_BackRequested;
            }

            // 自定义TitleBar在平板模式下仍然需要这个事件
            SystemNavigationManager.GetForCurrentView().BackRequested += View_BackRequested;
            if (Listener != null)
            {
                Listener.UnRegisterBackKeyPress();
            }

            _rootGridVisual.IsVisible = true;

            var scaleAnimation = _compositor.CreateVector3KeyFrameAnimation();
            scaleAnimation.InsertKeyFrame(0f, new Vector3(1.3f, 1.3f, 0f));
            scaleAnimation.InsertKeyFrame(1f, new Vector3(1f, 1f, 0f));
            scaleAnimation.Duration = TimeSpan.FromMilliseconds(400);
            _dialogVisual.CenterPoint = new Vector3((float)_dialogPresenter.ActualWidth / 2, (float)_dialogPresenter.ActualHeight / 2, 0f);
            _dialogVisual.StartAnimation("Scale", scaleAnimation);


            var opacityAnimation = _compositor.CreateScalarKeyFrameAnimation();
            opacityAnimation.InsertKeyFrame(0f, 0f, _compositor.CreateLinearEasingFunction());
            opacityAnimation.InsertKeyFrame(1f, 1f);
            opacityAnimation.Duration = TimeSpan.FromMilliseconds(300);
            _maskBorderVisual.StartAnimation("Opacity", opacityAnimation);

            await _showTcs.Task;
        }


        private void CustomTitlebar_BackRequested(object sender, BackKeyPressEventArgs e)
        {
            if (CanNotClose)
            {
                return;
            }

            e.Handled = true;
            this.Hide();
        }


        private void View_BackRequested(object sender, BackRequestedEventArgs e)
        {
            if (CanNotClose)
            {
                return;
            }

            e.Handled = true;
            this.Hide();
        }

        /// <summary>
        /// Hide
        /// </summary>
        public void Hide()
        {
            if (CustomTitlebar != null)
            {
                CustomTitlebar.BackRequested -= CustomTitlebar_BackRequested;
            }

            // 自定义TitleBar在平板模式下仍然需要这个事件
            SystemNavigationManager.GetForCurrentView().BackRequested -= View_BackRequested;
            Listener?.RegisterBackKeyPress();
            
            HideCompleted?.Invoke(this, null);
            _showTcs?.SetResult(0);

            var scaleAnimation = _compositor.CreateVector3KeyFrameAnimation();
            scaleAnimation.InsertKeyFrame(0f, new Vector3(1f, 1f, 0f));
            scaleAnimation.InsertKeyFrame(1f, new Vector3(1.3f, 1.3f, 0f));
            scaleAnimation.Duration = TimeSpan.FromMilliseconds(600);
            _dialogVisual.CenterPoint = new Vector3((float)_dialogPresenter.ActualWidth / 2, (float)_dialogPresenter.ActualHeight / 2, 0f);
            _dialogVisual.StartAnimation("Scale", scaleAnimation);

            var opacityAnimation = _compositor.CreateScalarKeyFrameAnimation();
            opacityAnimation.InsertKeyFrame(0f, 1f);
            opacityAnimation.InsertKeyFrame(1f, 0f);
            opacityAnimation.Duration = TimeSpan.FromMilliseconds(600);
            var batch = _compositor.CreateScopedBatch(CompositionBatchTypes.Animation);

            _rootGridVisual.StartAnimation("Opacity", opacityAnimation);

            batch.Completed += (s, e) =>
            {
                _rootGridVisual.IsVisible = false;
                Container.Children.Remove(this);
            };
            batch.End();
        }

        #endregion

        #region Initialize GlobalDialog

        /// <summary>
        /// 如果实现了IBackKeyPressManager，请在对应的页面初始化
        /// 如果没有实现，建议在根页面初始化
        /// </summary>
        /// <param name="panel">panel</param>
        /// <param name="listener">listener</param>
        /// /// <param name="titleBar">titleBar</param>
        public static void InitializeDialog(Panel panel, IBackKeyPressManager listener, ICustomTitleBar titleBar = null)
        {
            Container = panel;
            Listener = listener;
            CustomTitlebar = titleBar;
        }

        #endregion


        private T GetTemplateChild<T>(string childName) where T : FrameworkElement
        {
            return GetTemplateChild(childName) as T;
        }
    }
}
