using CommonServiceLocator;
using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Ioc;
using GalaSoft.MvvmLight.Views;
using GamerSky.Interfaces;
using GamerSky.Utils;
using GamerSky.Views;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GamerSky.ViewModels
{
    public class ViewModelLocator 
    {
        #region Keys
        public const string NewsPageKey = "NewsPage";
        public const string WebViewPageKey = "WebViewPage";
        public const string MasterDetailPageKey = "MasterDetailPage";
        public const string MainPageKey = "MainPage";
        public const string SearchPageKey = "SearchPage";
        public const string GamePageKey = "GamePage";
        #endregion

        public ViewModelLocator()
        {
            ServiceLocator.SetLocatorProvider(() => SimpleIoc.Default);
            if (ViewModelBase.IsInDesignModeStatic)
            {

            }
            else
            {

            }

            Configure();
        }

        private void Configure()
        {
            var nav = new MasterDetailNavigationService();
            nav.Configure(NewsPageKey, typeof(NewsPage));
            nav.Configure(WebViewPageKey, typeof(WebViewPage));
            nav.Configure(MasterDetailPageKey, typeof(MasterDetailPage));
            nav.Configure(MainPageKey, typeof(MainPage));
            nav.Configure(SearchPageKey, typeof(SearchPage));
            nav.Configure(GamePageKey, typeof(GamePage));

            SimpleIoc.Default.Register<IMasterDetailNavigationService>(() => nav);
            SimpleIoc.Default.Register<NewsPageViewModel>();
            SimpleIoc.Default.Register<WebViewPageViewModel>();
            SimpleIoc.Default.Register<MasterDetailPageViewModel>();
            SimpleIoc.Default.Register<MainPageViewModel>();
            SimpleIoc.Default.Register<SearchPageViewModel>();
            SimpleIoc.Default.Register<GamePageViewModel>();
        }

        #region ViewModels' instances
        public NewsPageViewModel NewsPageInstance
        {
            get
            {
                return ServiceLocator.Current.GetInstance<NewsPageViewModel>();
            }
        }

        public WebViewPageViewModel WebViewPageInstance
        {
            get
            {
                return ServiceLocator.Current.GetInstance<WebViewPageViewModel>();
            }
        } 
         
        public MasterDetailPageViewModel MasterDetailPageInstance
        {
            get
            {
                return ServiceLocator.Current.GetInstance<MasterDetailPageViewModel>();
            }
        }

        public MainPageViewModel MainPageInstance
        {
            get
            {
                return ServiceLocator.Current.GetInstance<MainPageViewModel>();
            }
        }

        public GamePageViewModel GamePageInstance
        {
            get
            {
                return ServiceLocator.Current.GetInstance<GamePageViewModel>();
            }
        }

        public SearchPageViewModel SearchPageInstance
        {
            get
            {
                return ServiceLocator.Current.GetInstance<SearchPageViewModel>();
            }
        }
        #endregion

        public static void Cleanup()
        {

        }
    }
}
