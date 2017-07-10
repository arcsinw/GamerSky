﻿using System;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Media;

namespace Arcsinx.Toolkit.Extensions
{
    public static class Extensions
    {
        public static ScrollViewer GetScrollViewer(this DependencyObject element)
        {
            if (element is ScrollViewer scrollViewer)
            {
                return scrollViewer;
            }

            for (var i = 0; i < VisualTreeHelper.GetChildrenCount(element); i++)
            {
                var child = VisualTreeHelper.GetChild(element, i);

                var result = GetScrollViewer(child);
                if (result == null) continue;

                return result;
            }

            return null;
        }

        public static bool AlmostEqual(this float x, float y, float tolerance = 0.01f) =>
            Math.Abs(x - y) < tolerance;
    }
}
