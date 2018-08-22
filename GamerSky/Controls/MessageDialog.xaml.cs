﻿using Arcsinx.Toolkit.Controls;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;

namespace GamerSky.Controls
{
    public sealed partial class MessageDialog : GlobalDialog
    {
        public MessageDialog()
        {
            this.InitializeComponent();
        }

        public MessageDialog(string message)
        {
            this.InitializeComponent();
            this.Text = message;
        }

        public string Text { get; set; }
    }
}