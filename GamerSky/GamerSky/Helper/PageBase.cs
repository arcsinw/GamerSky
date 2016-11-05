using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.Foundation;
using Windows.Foundation.Metadata;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Media.Animation;
using Windows.UI.Xaml.Navigation;

namespace GamerSky.Helper
{
    /// <summary>
    /// 提供滑动返回操作
    /// </summary>
    public class PageBase : Page
    { 
        private TranslateTransform translateTransform;
        public PageBase()
        {
            this.ManipulationMode = ManipulationModes.TranslateX;
            this.ManipulationCompleted += PageBase_ManipulationCompleted;
            this.ManipulationDelta += PageBase_ManipulationDelta;
            translateTransform = this.RenderTransform as TranslateTransform;
            if(translateTransform== null)
            {
                this.RenderTransform = translateTransform = new TranslateTransform();
            }
        }
      
        
        /// <summary>
        /// 手势操作时
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void PageBase_ManipulationDelta(object sender, ManipulationDeltaRoutedEventArgs e)
        {
            if(translateTransform.X + e.Delta.Translation.X<0)
            {
                translateTransform.X = 0;
                return;
            }
            translateTransform.X += e.Delta.Translation.X;
        }

        /// <summary>
        /// 手势操作结束
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void PageBase_ManipulationCompleted(object sender, ManipulationCompletedRoutedEventArgs e)
        {
            double abs_delta = Math.Abs(e.Cumulative.Translation.X);
            double speed = Math.Abs(e.Velocities.Linear.X);
            double delta = e.Cumulative.Translation.X;
            double to = 0;
            if(abs_delta < this.ActualWidth / 3 && speed<0.3) //力度不够，不导航到目标页面
            {
                translateTransform.X = 0;
                return;
            }

            action = 0; //后退

            if(delta>0)
            {
                to = this.ActualWidth;
            }
            else if(delta < 0)
            {
                return;
            }

            var storyBoard = new Storyboard();
            var doubleAnimation = new DoubleAnimation()
            {
                Duration = new Windows.UI.Xaml.Duration(TimeSpan.FromMilliseconds(120)),
                From = translateTransform.X,
                To = to
            };
            doubleAnimation.Completed += DoubleAnimation_Completed;
            Storyboard.SetTarget(doubleAnimation, translateTransform);
            Storyboard.SetTargetProperty(doubleAnimation, "X");
            storyBoard.Children.Add(doubleAnimation);
            storyBoard.Begin();
        }

        int action = 0;

        /// <summary>
        /// 动画结束
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void DoubleAnimation_Completed(object sender, object e)
        {
            if (action == 0)
            {
                var frame = (Window.Current.Content as Frame);
                if (frame == null) return;
                if (frame.CanGoBack)
                {
                    frame?.GoBack();
                }
            }
            translateTransform = this.RenderTransform as TranslateTransform;
            if (translateTransform == null)
            {
                this.RenderTransform = translateTransform = new TranslateTransform();
            }
            translateTransform.X = 0;

        }
    }
}
