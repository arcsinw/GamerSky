using GamerSky.Utils;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using System.Reactive.Linq;
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


namespace GamerSky.Views
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class SearchPage : Page
    {
        public SearchPage()
        {
            this.InitializeComponent();
        }

        public ObservableCollection<string> SearchHistories { get; set; } = new ObservableCollection<string>();

        public void InitSearch()
        {
            var input = Observable.FromEventPattern(searchBox, "TextChanged")
                .Select(_ => searchBox.Text)
                .Throttle(TimeSpan.FromSeconds(2))
                .DistinctUntilChanged(new TrimStringComparer())
                .Subscribe(x => Window.Current.Dispatcher.RunAsync(Windows.UI.Core.CoreDispatcherPriority.Low, () =>
                      {
                          SearchHistories.Add(x);
                      }));
        }
    }
}
