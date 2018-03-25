using GalaSoft.MvvmLight.Views;
using GamerSky.Interfaces;
using GamerSky.Views;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;

namespace GamerSky.Utils
{
    public class MasterDetailNavigationService : IMasterDetailNavigationService
    {
        public const string RootPageKey = "-- ROOT --";
     
        public const string UnknownPageKey = "-- UNKNOWN --";

        private Dictionary<string, Type> dic;

        public MasterDetailNavigationService()
        {
            dic = new Dictionary<string, Type>();
        }

        public Frame CurrentFrame { get; set; }
        
        public bool CanGoBack { get; }
        
        public bool CanGoForward { get; }
        
        public string CurrentPageKey { get; }
        
        public void Configure(string key, Type pageType)
        {
            if (string.IsNullOrEmpty(key) || (pageType == null))
            {
                return;
            }

            if (!dic.ContainsKey(key))
            {
                dic.Add(key, pageType);
            }
        }
        
        public string GetKeyForPage(Type page)
        {
            if (page == null)
            {
                return string.Empty;
            }

            var item = dic.First(d => d.Value == page);
            return item.Key;
        }
         
        
        public void GoForward()
        {
            if (MasterDetailPage.Current.DetailFrame.CanGoForward)
            {
                MasterDetailPage.Current.DetailFrame.GoForward();
            }
            else if (MasterDetailPage.Current.MasterFrame.CanGoForward)
            {
                MasterDetailPage.Current.MasterFrame.GoForward();
            }
        }
        
        public void NavigateTo(string pageKey)
        {

        }
        
        public virtual void NavigateTo(string pageKey, object parameter)
        {

        } 

        public void MasterNavigateTo(string pageKey, object parameter)
        {
            if (!dic.ContainsKey(pageKey))
            {
                return;
            }

            Type sourcePage = dic[pageKey];
            MasterDetailPage.Current.MasterFrame.Navigate(sourcePage, parameter);
        }

        public void MasterNavigateTo(string pageKey)
        {
            if (!dic.ContainsKey(pageKey))
            {
                return;
            }

            Type sourcePage = dic[pageKey];
            MasterDetailPage.Current.MasterFrame.Navigate(sourcePage);
        }

        public void DetailNavigateTo(string pageKey)
        {
            if (!dic.ContainsKey(pageKey))
            {
                return;
            }

            Type sourcePage = dic[pageKey];
            MasterDetailPage.Current.DetailFrame.Navigate(sourcePage);
        }

        public void DetailNavigateTo(string pageKey, object parameter)
        {
            if (!dic.ContainsKey(pageKey))
            {
                return;
            }

            Type sourcePage = dic[pageKey];
            MasterDetailPage.Current.DetailFrame.Navigate(sourcePage, parameter);
        }
         
        public void GoBack()
        {
            if (MasterDetailPage.Current.DetailFrame.CanGoBack)
            {
                MasterDetailPage.Current.DetailFrame.GoBack();
            }
            else if (MasterDetailPage.Current.MasterFrame.CanGoBack)
            {
                MasterDetailPage.Current.MasterFrame.GoBack();
            }
            else
            {
                Application.Current.Exit();
            }
        }
         
    }
}
