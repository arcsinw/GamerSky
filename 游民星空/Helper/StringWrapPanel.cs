using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.Foundation;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;

namespace 游民星空.Helper
{
    public class StringWrapPanel : Panel
    {
        /// <summary>
        /// 计算控件大小
        /// </summary>
        /// <param name="availableSize">可用大小</param>
        /// <returns></returns>
        protected override Size MeasureOverride(Size availableSize)
        { 
            //可用空间大小
            Size usefulSize = new Size(availableSize.Width, double.PositiveInfinity);

            //控件高度
            double y = 0;
            double x = 0;

            foreach (UIElement item in Children)
            {
                item.Measure(usefulSize);

                Size itemSize = item.DesiredSize;
                double itemWidth = itemSize.Width;

                y = (itemSize.Height) > y ? itemSize.Height : y;

                //加入该子控件后一行满了
                if (x + itemSize.Width > availableSize.Width)
                {
                    x = 0;
                    y += itemSize.Height;
                }
                x += itemSize.Width;
            }

            return new Size(availableSize.Width, y);
        }

        /// <summary>
        /// 为每个子控件布局
        /// </summary>
        /// <param name="finalSize"></param>
        /// <returns></returns>
        protected override Size ArrangeOverride(Size finalSize)
        { 
            //记录横坐标
            double x = 0.0;
            double y = 0.0;

            foreach (UIElement item in Children)
            {
                Size itemSize = item.DesiredSize;
                double itemWidth = itemSize.Width;

                //加入该控件后一行满了
                if (x + itemSize.Width > finalSize.Width)
                {
                    x = 0;
                    y += itemSize.Height;
                }
                //控件的坐标
                Point pt = new Point(x, y);

                //控件布局
                item.Arrange(new Rect(pt, itemSize));
                x += itemSize.Width;
            }

            return finalSize;
        }
    }
}
