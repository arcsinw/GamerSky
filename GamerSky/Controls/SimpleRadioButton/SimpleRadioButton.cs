using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Documents;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
 
namespace GamerSky.Controls
{
    public sealed class SimpleRadioButton : RadioButton
    {
        public SimpleRadioButton()
        {
            this.DefaultStyleKey = typeof(SimpleRadioButton);
        }



        #region Properties
        public string Source
        {
            get { return (string)GetValue(SourceProperty); }
            set { SetValue(SourceProperty, value); }
        }

        // Using a DependencyProperty as the backing store for Icon.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty SourceProperty =
            DependencyProperty.Register("Source", typeof(string), typeof(SimpleRadioButton), new PropertyMetadata(null));
         

        public string Title
        {
            get { return (string)GetValue(TitleProperty); }
            set { SetValue(TitleProperty, value); }
        }

        // Using a DependencyProperty as the backing store for Title.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty TitleProperty =
            DependencyProperty.Register("Title", typeof(string), typeof(SimpleRadioButton), new PropertyMetadata(0));

        public string SelectedSource
        {
            get { return (string)GetValue(SelectedSourceProperty); }
            set { SetValue(SelectedSourceProperty, value); }
        }

        // Using a DependencyProperty as the backing store for IconSelected.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty SelectedSourceProperty =
            DependencyProperty.Register("SelectedSource", typeof(string), typeof(SimpleRadioButton), new PropertyMetadata(null));

        #endregion

    }
}
