﻿using Arcsinx.Toolkit.Extensions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.UI.Composition;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Hosting;

namespace Arcsinx.Toolkit.Controls
{
    public class ParallaxService
    {
        /// <summary>
        /// Identifies the ParallaxService.VerticalMultiplier XAML attached property.
        /// </summary>
        public static readonly DependencyProperty VerticalMultiplierProperty = DependencyProperty.RegisterAttached("VerticalMultiplier", typeof(double), typeof(ParallaxService), new PropertyMetadata(0d, OnMultiplierChanged));

        /// <summary>
        /// Identifies the ParallaxService.HorizontalMultiplier attached property.
        /// </summary>
        public static readonly DependencyProperty HorizontalMultiplierProperty = DependencyProperty.RegisterAttached("HorizontalMultiplier", typeof(double), typeof(ParallaxService), new PropertyMetadata(0d, OnMultiplierChanged));

        /// <summary>
        /// Gets the ParallaxService.VerticalMultiplier attached property value for the specified target element.
        /// </summary>
        /// <param name="element">The target element for the attached property value..</param>
        /// <returns>A value for how fast the parallax effect should scroll vertically.</returns>
        public static double GetVerticalMultiplier(UIElement element)
        {
            return (double)element.GetValue(VerticalMultiplierProperty);
        }

        /// <summary>
        /// Sets the ParallaxService.VerticalMultiplier attached property value for the specified target element.
        /// </summary>
        /// <param name="element">The target element for the attached property value.</param>
        /// <param name="value">The value for how fast the parallax effect should scroll vertically.</param>
        public static void SetVerticalMultiplier(UIElement element, double value)
        {
            element.SetValue(VerticalMultiplierProperty, value);
        }

        /// <summary>
        /// Gets the ParallaxService.HorizontalMultiplier attached property value for the specified target element.
        /// </summary>
        /// <param name="element">The target element for the attached property value..</param>
        /// <returns>A value for how fast the parallax effect should scroll vertically.</returns>
        public static double GetHorizontalMultiplier(UIElement element)
        {
            return (double)element.GetValue(HorizontalMultiplierProperty);
        }

        /// <summary>
        /// Sets the ParallaxService.HorizontalMultiplier attached property value for the specified target element.
        /// </summary>
        /// <param name="element">The target element for the attached property value.</param>
        /// <param name="value">The value for how fast the parallax effect should scroll horizontally.</param>
        public static void SetHorizontalMultiplier(UIElement element, double value)
        {
            element.SetValue(HorizontalMultiplierProperty, value);
        }

        /// <summary>
        /// Identifies the ParallaxService.ScrollingElement XAML attached property.
        /// </summary>
        private static readonly DependencyProperty ScrollingElementProperty = DependencyProperty.RegisterAttached("ScrollingElement", typeof(ScrollViewer), typeof(ParallaxService), new PropertyMetadata(null));

        /// <summary>
        /// Gets the ParallaxService.ScrollingElementattached property value for the specified target element.
        /// </summary>
        /// <param name="element">The target element for the attached property value..</param>
        /// <returns>A <see cref="FrameworkElement"/> that is, or contains a ScrollViewer.</returns>
        private static ScrollViewer GetScrollingElement(UIElement element)
        {
            return (ScrollViewer)element.GetValue(ScrollingElementProperty);
        }

        /// <summary>
        /// Sets the ParallaxService.ScrollingElementattached property value for the specified target element.
        /// </summary>
        /// <param name="element">The target element for the attached property value.</param>
        /// <param name="value">The element that is, or contains a ScrollViewer.</param>
        private static void SetScrollingElement(UIElement element, ScrollViewer value)
        {
            element.SetValue(ScrollingElementProperty, value);
        }

        private static void OnMultiplierChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            var uiElement = (UIElement)d;
            var scrollViewer = GetScrollingElement(uiElement);
            if (scrollViewer == null)
            {
                var element = d as FrameworkElement;
                if (element != null)
                {
                    scrollViewer = element.FindVisualAscendant<ScrollViewer>();
                    if (scrollViewer == null)
                    {
                        element.Loaded += OnElementLoaded;
                        return;
                    }

                    SetScrollingElement(uiElement, scrollViewer);
                }
            }

            CreateParallax(uiElement, scrollViewer, (double)d.GetValue(HorizontalMultiplierProperty), (double)d.GetValue(VerticalMultiplierProperty));
        }

        private static void OnElementLoaded(object sender, RoutedEventArgs e)
        {
            var element = (FrameworkElement)sender;
            element.Loaded -= OnElementLoaded;

            var scrollViewer = element.FindVisualAscendant<ScrollViewer>();
            SetScrollingElement(element, scrollViewer);

            CreateParallax(element, scrollViewer, (double)element.GetValue(HorizontalMultiplierProperty), (double)element.GetValue(VerticalMultiplierProperty));
        }

        private static void CreateParallax(UIElement parallaxElement, ScrollViewer scroller, double horizontalMultiplier, double verticalMultiplier)
        {
            if ((parallaxElement == null) || (scroller == null))
            {
                return;
            }

            CompositionPropertySet scrollerViewerManipulation = ElementCompositionPreview.GetScrollViewerManipulationPropertySet(scroller);

            Compositor compositor = scrollerViewerManipulation.Compositor;

            ExpressionAnimation expression = compositor.CreateExpressionAnimation(
                "Matrix4x4.CreateFromTranslation(Vector3(HorizontalMultiplier * scroller.Translation.X, VerticalMultiplier * scroller.Translation.Y, 0.0f))");
            expression.SetReferenceParameter("scroller", scrollerViewerManipulation);
            expression.SetScalarParameter("HorizontalMultiplier", (float)horizontalMultiplier);
            expression.SetScalarParameter("VerticalMultiplier", (float)verticalMultiplier);

            Visual visual = ElementCompositionPreview.GetElementVisual(parallaxElement);
            visual.StartAnimation("TransformMatrix", expression);
        }
    }
}