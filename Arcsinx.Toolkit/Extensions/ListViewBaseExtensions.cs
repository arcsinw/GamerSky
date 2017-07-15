﻿using Arcsinx.Toolkit.Helper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Media;

namespace Arcsinx.Toolkit.Extensions
{
    public static class ListViewBaseExtensions
    {
        public static void ScrollUp(this ListViewBase listView)
        {
            var scrollViewer = listView.GetScrollViewer();

        }

        public static void ScrollDown(this ListViewBase listView)
        {
            var scrollViewer = listView.GetScrollViewer();

        }

        /// <summary>
        /// 平滑滚动到某一项
        /// </summary>
        /// <param name="listViewBase"></param>
        /// <param name="item"></param>
        public static void ScrollIntoViewSmoothly(this ListViewBase listViewBase, object item)
        {
            ScrollIntoViewSmoothly(listViewBase, item, ScrollIntoViewAlignment.Default);
        }

        public static void ScrollIntoViewSmoothly(this ListViewBase listViewBase,object item,ScrollIntoViewAlignment alignment)
        {
            if(listViewBase == null)
            {
                throw new ArgumentException(nameof(ListViewBase));
            }

            // GetFirstDescendantOfType 是 WinRTXamlToolkit 中的扩展方法，
            // 寻找该控件在可视树上第一个符合类型的子元素。
            ScrollViewer scrollViewer = FindChildOfType<ScrollViewer>(listViewBase);

            // 由于 ScrollViewer 肯定有，因此不做 null 检查判断了。

            // 记录初始位置，用于 ScrollIntoView 检测目标位置后复原。
            double originHorizontalOffset = scrollViewer.HorizontalOffset;
            double originVerticalOffset = scrollViewer.VerticalOffset;

            EventHandler<object> layoutUpdatedHandler = null;
            layoutUpdatedHandler = delegate
            {
                listViewBase.LayoutUpdated -= layoutUpdatedHandler;

                // 获取目标位置。
                double targetHorizontalOffset = scrollViewer.HorizontalOffset;
                double targetVerticalOffset = scrollViewer.VerticalOffset;

                EventHandler<ScrollViewerViewChangedEventArgs> scrollHandler = null;
                scrollHandler = delegate
                {
                    scrollViewer.ViewChanged -= scrollHandler;

                    // 最终目的，带平滑滚动效果滚动到 item
                    scrollViewer.ChangeView(targetHorizontalOffset, targetVerticalOffset, null);
                };
                scrollViewer.ViewChanged += scrollHandler;

                // 复原位置，且不需要使用动画效果
                scrollViewer.ChangeView(originHorizontalOffset, originVerticalOffset, null, false);
            };
            listViewBase.LayoutUpdated += layoutUpdatedHandler;

            // 跑腿
            listViewBase.ScrollIntoView(item, alignment);
        }

        /// <summary>
        /// 在UI对象中查找一个子元素
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="root"></param>
        /// <returns></returns>
        public static T FindChildOfType<T>(DependencyObject root) where T : class
        {
            //创建一个队列结构来存放可视化树的对象
            var queue = new Queue<DependencyObject>();
            queue.Enqueue(root);
            //循环查找类型
            while (queue.Count > 0)
            {
                DependencyObject current = queue.Dequeue();
                //查找子节点的对象类型
                for (int i = VisualTreeHelper.GetChildrenCount(current) - 1; 0 <= i; i--)
                {
                    var child = VisualTreeHelper.GetChild(current, i);
                    var typedChild = child as T;
                    if (typedChild != null)
                    {
                        return typedChild;
                    }
                    queue.Enqueue(child);
                }
            }
            return null;
        }
    } 
}