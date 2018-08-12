using System;
using System.Numerics;
using Windows.ApplicationModel;
using Windows.Foundation;
using Windows.UI;
using Windows.UI.Composition;
using Windows.UI.ViewManagement;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Hosting;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Shapes;

namespace GamerSky.Controls
{
    public sealed class IndicatorPivot : Pivot
    {
        #region Field

        private ScrollViewer _scrollViewer;
        private Line _tipLine;
        private TranslateTransform _tipLineTranslateTransform;

        private Compositor _compositor;
        private Visual _lineVisual;
        private CompositionPropertySet _scrollProperties;

        private int _ignoreCount = 0;
        private double _previsousOffset;

        #endregion

        #region Dependency Property

        public double HeaderWidth
        {
            get { return (double)GetValue(HeaderWidthProperty); }
            set { SetValue(HeaderWidthProperty, value); }
        }

        // Using a DependencyProperty as the backing store for HeaderWidth.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty HeaderWidthProperty =
            DependencyProperty.Register("HeaderWidth", typeof(double), typeof(IndicatorPivot), new PropertyMetadata(80.0));



        public Brush HeaderBackground
        {
            get { return (Brush)GetValue(HeaderBackgroundProperty); }
            set { SetValue(HeaderBackgroundProperty, value); }
        }

        // Using a DependencyProperty as the backing store for HeaderBackground.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty HeaderBackgroundProperty =
            DependencyProperty.Register("HeaderBackground", typeof(Brush), typeof(IndicatorPivot), new PropertyMetadata(new SolidColorBrush(Colors.Transparent)));



        public double BackgroundLineStokeThickness
        {
            get { return (double)GetValue(BackgroundLineStokeThicknessProperty); }
            set { SetValue(BackgroundLineStokeThicknessProperty, value); }
        }

        // Using a DependencyProperty as the backing store for BackgroundLineStokeThickness.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty BackgroundLineStokeThicknessProperty =
            DependencyProperty.Register("BackgroundLineStokeThickness", typeof(double), typeof(IndicatorPivot), new PropertyMetadata(2.0));


        public Brush BackgroundLineStoke
        {
            get { return (Brush)GetValue(BackgroundLineStokeProperty); }
            set { SetValue(BackgroundLineStokeProperty, value); }
        }

        // Using a DependencyProperty as the backing store for BackgroundLineStoke.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty BackgroundLineStokeProperty =
            DependencyProperty.Register("BackgroundLineStoke", typeof(Brush), typeof(IndicatorPivot), new PropertyMetadata(new SolidColorBrush(Colors.LightGray)));


        public double IndicatorLineStokeThickness
        {
            get { return (double)GetValue(IndicatorLineStokeThicknessProperty); }
            set { SetValue(IndicatorLineStokeThicknessProperty, value); }
        }

        // Using a DependencyProperty as the backing store for ForegroundLineStokeThickness.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty IndicatorLineStokeThicknessProperty =
            DependencyProperty.Register(nameof(IndicatorLineStokeThickness), typeof(double), typeof(IndicatorPivot), new PropertyMetadata(2.0));



        public Brush IndicatorLineStroke
        {
            get { return (Brush)GetValue(IndicatorLineStrokeProperty); }
            set { SetValue(IndicatorLineStrokeProperty, value); }
        }

        // Using a DependencyProperty as the backing store for ForegroundLineStroke.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty IndicatorLineStrokeProperty =
            DependencyProperty.Register(nameof(IndicatorLineStroke), typeof(Brush), typeof(IndicatorPivot), new PropertyMetadata(new SolidColorBrush(Colors.Red)));


        #endregion

        public IndicatorPivot()
        {
            this.DefaultStyleKey = typeof(IndicatorPivot);

            if (!DesignMode.DesignModeEnabled)
            {
                this.SelectionChanged += IndicatorPivot_SelectionChanged;
                this.Loaded += IndicatorPivot_Loaded;
            }
        }

        private void IndicatorPivot_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            // Stop Expression Animation
            _lineVisual.StopAnimation(nameof(Visual.Offset));

            _ignoreCount++;

            var animation = _compositor.CreateVector3KeyFrameAnimation();
            animation.InsertKeyFrame(1f, new Vector3((float)(SelectedIndex * HeaderWidth), 0f, 0f));
            animation.Duration = TimeSpan.FromMilliseconds(330);

            var batch = _compositor.CreateScopedBatch(CompositionBatchTypes.Animation);

            _lineVisual.StartAnimation(nameof(Visual.Offset), animation);
            batch.Completed += (s, args) =>
            {
                _ignoreCount--;
            };
            batch.End();
        }

        private void IndicatorPivot_Loaded(object sender, RoutedEventArgs e)
        {
            if (Items.Count > 1)
            {
                var res = this.ActualWidth / Items.Count;
                if (DeviceUtils.IsMobile)
                {
                    HeaderWidth = res;
                }
                _tipLine.X2 = HeaderWidth;
            }
        }

        protected override void OnApplyTemplate()
        {
            base.OnApplyTemplate();

            var itemsPresenterTranslateTransform = GetTemplateChild<TranslateTransform>("ItemsPresenterTranslateTransform");
            if (itemsPresenterTranslateTransform != null && !DesignMode.DesignModeEnabled)
            {
                itemsPresenterTranslateTransform.RegisterPropertyChangedCallback(TranslateTransform.XProperty, Callback);
            }
            _scrollViewer = GetTemplateChild<ScrollViewer>("ScrollViewer");
            if (_scrollViewer != null && !DesignMode.DesignModeEnabled)
            {
                _scrollViewer.RegisterPropertyChangedCallback(ScrollViewer.HorizontalOffsetProperty, HorizontalOffsetCallback);
            }
            _tipLine = GetTemplateChild<Line>("TipLine");
            _tipLineTranslateTransform = GetTemplateChild<TranslateTransform>("TipLineTranslateTransform");

            InitializeComposition();
        }

        private void InitializeComposition()
        {
            _compositor = ElementCompositionPreview.GetElementVisual(this).Compositor;
            _lineVisual = ElementCompositionPreview.GetElementVisual(_tipLine);
            _scrollProperties = ElementCompositionPreview.GetScrollViewerManipulationPropertySet(_scrollViewer);
        }

        private void HorizontalOffsetCallback(DependencyObject sender, DependencyProperty dp)
        {
            //Vector3 v3;
            //_scrollProperties = ElementCompositionPreview.GetScrollViewerManipulationPropertySet(_scrollViewer);
            //var res = _scrollProperties.TryGetVector3("Translation", out v3);
            //if (res == CompositionGetValueStatus.Succeeded)
            //{
            //    Debug.WriteLine($"X:{v3.X}");
            //    Debug.WriteLine($"Y:{v3.Y}");
            //    Debug.WriteLine($"Z:{v3.Z}");
            //}



            //if (_tcs != null && !_tcs.Task.IsCompleted)
            //    return;

            if (_ignoreCount != 0)
            {
                return;
            }

            if (_previsousOffset != 0)
            {
                var x = (double)sender.GetValue(dp);
                var right = x > _previsousOffset;

                if (right)
                {
                    // 非边界
                    if (SelectedIndex + 1 != Items.Count)
                    {
                        var newX = ((x - _previsousOffset) / Items.Count) + (SelectedIndex * HeaderWidth);
                        var max = (SelectedIndex + 1) * HeaderWidth;
                        _lineVisual.Offset = new Vector3((float)(newX < max ? newX : max), 0f, 0f);
                        //_tipLineTranslateTransform.X = newX < max ? newX : max;
                    }
                    else
                    {
                        _lineVisual.Offset = new Vector3((float)((SelectedIndex * HeaderWidth) - (x - _previsousOffset)), 0f, 0f);
                        //_tipLineTranslateTransform.X = (SelectedIndex * HeaderWidth) - (x - _previsousOffset);
                    }
                }
                else
                {
                    // 非边界
                    if (SelectedIndex != 0)
                    {
                        var newX = ((x - _previsousOffset) / Items.Count) + (SelectedIndex * HeaderWidth);
                        var max = (SelectedIndex + 1) * HeaderWidth;
                        _lineVisual.Offset = new Vector3((float)(newX < max ? newX : max), 0f, 0f);
                        //_tipLineTranslateTransform.X = newX < max ? newX : max;
                    }
                    else
                    {
                        _lineVisual.Offset = new Vector3((float)(_previsousOffset - x), 0f, 0f);
                        //_tipLineTranslateTransform.X = _previsousOffset - x;
                    }
                }
            }
        }

        private void Callback(DependencyObject sender, DependencyProperty dp)
        {
            _previsousOffset = (double)sender.GetValue(dp);
            //_tipLineTranslateTransform.X = (SelectedIndex * HeaderWidth);
            //var animation = _compositor.CreateVector3KeyFrameAnimation();
            //animation.InsertKeyFrame(0f, _lineVisual.Offset);
            //animation.InsertKeyFrame(1f, new Vector3((float)(SelectedIndex * HeaderWidth), 0f, 0f));
            //animation.Duration = TimeSpan.FromMilliseconds(500);

            //var batch = _compositor.CreateScopedBatch(CompositionBatchTypes.Animation);

            //_lineVisual.StartAnimation("Offset", animation);
            //batch.Completed += (s, e) => 
            //{
            //    _needIgnore = false;
            //};
            //batch.End();
        }

        private T GetTemplateChild<T>(string name) where T : DependencyObject => GetTemplateChild(name) as T;
    }


    public static class DeviceUtils
    {
        public static bool IsMobile => Windows.Foundation.Metadata.ApiInformation.IsTypePresent("Windows.Phone.UI.Input.HardwareButtons") ? true : false;

        public static Size ScreenSize()
        {
            var bounds = ApplicationView.GetForCurrentView().VisibleBounds;
            return new Size(bounds.Width, bounds.Height);
        }
    }
}
