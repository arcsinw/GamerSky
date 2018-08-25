﻿using CommonServiceLocator;
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
        public const string GroupPageKey = "GroupPage";
        public const string OriginPageKey = "OriginPage";
        public const string GameDetailPageKey = "GameDetailPage";
        public const string SubjectContentPageKey = "SubjectContentPage";
        public const string LoginPageKey = "LoginPage";
        public const string SpecialSubjectPageKey = "SpecialSubjectPage";
        public const string SpecialSubjectContentPageKey = "SpecialSubjectContentPage";
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

        private static void Configure()
        {
            var nav = new MasterDetailNavigationService();
            nav.Configure(NewsPageKey, typeof(NewsPage));
            nav.Configure(WebViewPageKey, typeof(WebViewPage));
            nav.Configure(MasterDetailPageKey, typeof(MasterDetailPage));
            nav.Configure(MainPageKey, typeof(MainPage));
            nav.Configure(SearchPageKey, typeof(SearchPage));
            nav.Configure(GamePageKey, typeof(GamePage));
            nav.Configure(GroupPageKey, typeof(GroupPage));
            nav.Configure(OriginPageKey, typeof(OriginalPage));
            nav.Configure(GameDetailPageKey, typeof(GameDetailPage));
            nav.Configure(SubjectContentPageKey, typeof(SubjectContentPage));
            nav.Configure(LoginPageKey, typeof(LoginPage));
            nav.Configure(SpecialSubjectPageKey, typeof(SpecialSubjectPage));
            nav.Configure(SpecialSubjectContentPageKey, typeof(SpecialSubjectContentPage));

            SimpleIoc.Default.Register<IMasterDetailNavigationService>(() => nav);
            SimpleIoc.Default.Register<NewsPageViewModel>();
            SimpleIoc.Default.Register<WebViewPageViewModel>();
            SimpleIoc.Default.Register<MasterDetailPageViewModel>();
            SimpleIoc.Default.Register<MainPageViewModel>();
            SimpleIoc.Default.Register<SearchPageViewModel>();
            SimpleIoc.Default.Register<GamePageViewModel>();
            SimpleIoc.Default.Register<GroupPageViewModel>();
            SimpleIoc.Default.Register<OriginPageViewModel>();
            SimpleIoc.Default.Register<GameDetailPageViewModel>();
            SimpleIoc.Default.Register<SubjectContentPageViewModel>();
            SimpleIoc.Default.Register<LoginPageViewModel>();
            SimpleIoc.Default.Register<SpecialSubjectPageViewModel>();
            SimpleIoc.Default.Register<SpecialSubjectContentPageViewModel>();
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

        public GroupPageViewModel GroupPageInstance
        {
            get
            {
                return ServiceLocator.Current.GetInstance<GroupPageViewModel>();
            }
        }

        public OriginPageViewModel OriginPageInstance
        {
            get
            {
                return ServiceLocator.Current.GetInstance<OriginPageViewModel>();
            }
        }

        public GameDetailPageViewModel GameDetailPageInstance
        {
            get
            {
                return ServiceLocator.Current.GetInstance<GameDetailPageViewModel>();
            }
        }

        public SubjectContentPageViewModel SubjectContentPageInstance
        {
            get
            {
                return ServiceLocator.Current.GetInstance<SubjectContentPageViewModel>();
            }
        }

        public LoginPageViewModel LoginPageInstance
        {
            get
            {
                return ServiceLocator.Current.GetInstance<LoginPageViewModel>();
            }
        }

        public SpecialSubjectPageViewModel SpecialSubjectPageInstance
        {
            get
            {
                return ServiceLocator.Current.GetInstance<SpecialSubjectPageViewModel>();
            }
        }

        public SpecialSubjectContentPageViewModel SpecialSubjectContentPageInstance
        {
            get
            {
                return ServiceLocator.Current.GetInstance<SpecialSubjectContentPageViewModel>();
            }
        }
        #endregion

        public static void Cleanup()
        {
            SimpleIoc.Default.Reset();

            Configure();
        }
    }
}
