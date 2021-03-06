﻿// ******************************************************************
// Copyright (c) Microsoft. All rights reserved.
// This code is licensed under the MIT License (MIT).
// THE CODE IS PROVIDED “AS IS”, WITHOUT WARRANTY OF ANY KIND, EXPRESS OR IMPLIED,
// INCLUDING BUT NOT LIMITED TO THE WARRANTIES OF MERCHANTABILITY,
// FITNESS FOR A PARTICULAR PURPOSE AND NONINFRINGEMENT.
// IN NO EVENT SHALL THE AUTHORS OR COPYRIGHT HOLDERS BE LIABLE FOR ANY CLAIM,
// DAMAGES OR OTHER LIABILITY, WHETHER IN AN ACTION OF CONTRACT,
// TORT OR OTHERWISE, ARISING FROM, OUT OF OR IN CONNECTION WITH
// THE CODE OR THE USE OR OTHER DEALINGS IN THE CODE.
// ******************************************************************

using System;
using Windows.Media.Casting;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Media.Imaging;

namespace GamerSky.Controls
{
    /// <summary>
    /// The ImageEx control extends the default Image platform control improving the performance and responsiveness of your Apps.
    /// Source images are downloaded asynchronously showing a load indicator while in progress.
    /// Once downloaded, the source image is stored in the App local cache to preserve resources and load time next time the image needs to be displayed.
    /// </summary>
    public partial class ImageEx
    {
        /// <summary>
        /// Identifies the <see cref="NineGrid"/> dependency property.
        /// </summary>
        public static readonly DependencyProperty NineGridProperty = DependencyProperty.Register(nameof(NineGrid), typeof(Thickness), typeof(ImageEx), new PropertyMetadata(default(Thickness)));

        /// <summary>
        /// Identifies the <see cref="Stretch"/> dependency property.
        /// </summary>
        public static readonly DependencyProperty StretchProperty = DependencyProperty.Register(nameof(Stretch), typeof(Stretch), typeof(ImageEx), new PropertyMetadata(Stretch.Uniform));

        /// <summary>
        /// Identifies the <see cref="DecodePixelHeight"/> dependency property.
        /// </summary>
        public static readonly DependencyProperty DecodePixelHeightProperty = DependencyProperty.Register(nameof(DecodePixelHeight), typeof(int), typeof(ImageEx), new PropertyMetadata(0));

        /// <summary>
        /// Identifies the <see cref="DecodePixelType"/> dependency property.
        /// </summary>
        public static readonly DependencyProperty DecodePixelTypeProperty = DependencyProperty.Register(nameof(DecodePixelType), typeof(int), typeof(ImageEx), new PropertyMetadata(DecodePixelType.Physical));

        /// <summary>
        /// Identifies the <see cref="DecodePixelWidth"/> dependency property.
        /// </summary>
        public static readonly DependencyProperty DecodePixelWidthProperty = DependencyProperty.Register(nameof(DecodePixelWidth), typeof(int), typeof(ImageEx), new PropertyMetadata(0));

        /// <summary>
        /// Event raised if the image failed loading.
        /// </summary>
        [Obsolete("This event is obsolete; use ImageExFailed event instead")]
        public event ExceptionRoutedEventHandler ImageFailed;

        /// <summary>
        /// Event raised when the image is successfully loaded and opened.
        /// </summary>
        [Obsolete("This event is obsolete; use ImageExOpened event instead")]
        public event RoutedEventHandler ImageOpened;

        /// <summary>
        /// Event raised if the image failed loading.
        /// </summary>
        public event ImageExFailedEventHandler ImageExFailed;

        /// <summary>
        /// Event raised when the image is successfully loaded and opened.
        /// </summary>
        public event ImageExOpenedEventHandler ImageExOpened;

        /// <summary>
        /// Gets or sets DecodePixelHeight for underlying bitmap
        /// </summary>
        public int DecodePixelHeight
        {
            get { return (int)GetValue(DecodePixelHeightProperty); }
            set { SetValue(DecodePixelHeightProperty, value); }
        }

        /// <summary>
        /// Gets or sets DecodePixelType for underlying bitmap
        /// </summary>
        public DecodePixelType DecodePixelType
        {
            get { return (DecodePixelType)GetValue(DecodePixelTypeProperty); }
            set { SetValue(DecodePixelTypeProperty, value); }
        }

        /// <summary>
        /// Gets or sets DecodePixelWidth for underlying bitmap
        /// </summary>
        public int DecodePixelWidth
        {
            get { return (int)GetValue(DecodePixelWidthProperty); }
            set { SetValue(DecodePixelWidthProperty, value); }
        }

        /// <summary>
        /// Gets or sets the nine-grid used by the image.
        /// </summary>
        public Thickness NineGrid
        {
            get { return (Thickness)GetValue(NineGridProperty); }
            set { SetValue(NineGridProperty, value); }
        }

        /// <summary>
        /// Gets or sets the stretch of the image.
        /// </summary>
        public Stretch Stretch
        {
            get { return (Stretch)GetValue(StretchProperty); }
            set { SetValue(StretchProperty, value); }
        }

        /// <summary>
        /// Returns the image as a <see cref="CastingSource"/>.
        /// </summary>
        /// <returns>The image as a <see cref="CastingSource"/>.</returns>
        public CastingSource GetAsCastingSource()
        {
            return _isInitialized ? _image.GetAsCastingSource() : null;
        }

        /// <summary>
        /// Gets or sets a value indicating whether gets or sets cache state
        /// </summary>
        public bool IsCacheEnabled
        {
            get; set;
        }
    }
}
