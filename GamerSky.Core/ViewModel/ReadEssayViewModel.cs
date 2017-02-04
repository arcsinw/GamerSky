using GamerSky.Core.Http;
using GamerSky.Core.IncrementalLoadingCollection;
using GamerSky.Core.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.ApplicationModel;
using Windows.Storage;
using Windows.UI.Xaml;

namespace GamerSky.Core.ViewModel
{
    public class ReadEssayViewModel : ViewModelBase
    {
        private StringBuilder tempHtml  = new StringBuilder();
        public string _originUri;

        public ReadEssayViewModel()
        {
            if (DesignMode.DesignModeEnabled)
            {
                Comments = new EssayCommentsCollection("836890");
            }

            AppTheme = DataShareManager.Current.AppTheme;
            IsNoImgMode = DataShareManager.Current.IsNoImage;
            EssayFontSize = DataShareManager.Current.FontSize;
            //DataShareManager.Current.ShareDataChanged += Current_ShareDataChanged;
        }

        private void Current_ShareDataChanged()
        {
            AppTheme = DataShareManager.Current.AppTheme;
            IsNoImgMode = DataShareManager.Current.IsNoImage;
            EssayFontSize = DataShareManager.Current.FontSize;
        }

        public async void SetCurrentEssay(Essay essay, int currentIndex)
        {
            CurrentEssay = essay;

            switch (currentIndex)
            {
                case 0:
                    await GenerateHtmlString();
                    break;
                case 1:
                    GenerateComments();
                    break;
            }
        }

        #region Properties
        private string _contentHtml;

        public string ContentHtml
        {
            get { return _contentHtml; }
            set { _contentHtml = value; }
        }

        private ElementTheme appTheme;
        public ElementTheme AppTheme
        {
            get
            {
                return appTheme;
            }
            set
            {
                appTheme = value;
                OnPropertyChanged();
            }
        }

        /// <summary>
        /// ProgressRing IsActive
        /// </summary>
        private bool isActive;
        public bool IsActive
        {
            get
            {
                return isActive;
            }
            set
            {
                isActive = value;
                OnPropertyChanged();
            }
        }

        private Essay _currentEssay;

        public Essay CurrentEssay
        {
            get { return _currentEssay; }
            set
            {
                _currentEssay = value;
            }
        }

        private bool _isNoImgMode;

        public bool IsNoImgMode
        {
            get { return _isNoImgMode; }
            set { _isNoImgMode = value; OnPropertyChanged(); }
        }

        private int _essayFontSize;

        public int EssayFontSize
        {
            get { return _essayFontSize; }
            set { _essayFontSize = value; DataShareManager.Current.UpdateFontSize(value); OnPropertyChanged(); }
        }

        public EssayCommentsCollection Comments { get; set; }
        #endregion

        #region Generate Content

        /// <summary>
        /// 生成新闻内容网页
        /// </summary>
        public async Task GenerateHtmlString()
        {
            IsActive = true;
            News news = await ApiService.Instance.ReadEssay(CurrentEssay.ContentId);
            if (news != null)
            {
                _originUri = news.OriginURL;

                string mainBody = news.MainBody;

                string head = "<meta http-equiv=\"Content-Type\" content=\"text/html; charset = utf-8\" />"
                            + "<meta name=\"viewport\" content=\"width= device-width, user-scalable = no\" />"
                            + "<meta name=\"format-detection\" content=\"telephone=no,email=no\">" //忽略电话号码和邮箱
                            + "<meta name=\"msapplication-tap-highlight\" content=\"no\">" //wp点击无高光;
                            + "<link type=\"text/css\" rel=\"stylesheet\" href=\"ms-appx-web:///Assets/Css/gs.css\"/>"
                            //+ "<link type=\"text/css\" rel=\"stylesheet\" href=\"ms-appx-web:///Assets/Css/gsAppHTMLTemplate.css\"/>"
                            + "<script type=\"text/javascript\" src=\"ms-appx-web:///Assets/Js/gsAppHTMLTemplate.js\"></script>"
                            + "<script type=\"text/javascript\" src=\"ms-appx-web:///Assets/Js/jquery.min.js\"></script>"
                            + "<script type=\"text/javascript\" src=\"ms-appx-web:///Assets/Js/jquery.lazyload.js\"></script>"
                            + "<script type=\"text/javascript\" src=\"ms-appx-web:///Assets/Js/gs.js\"></script>";
                //+ "<script type=\"text/javascript\" src=\"ms-appx-web:///Assets/Js/gsVideo.js\"></script>";
                //+ "<script src=\"http://j.gamersky.com/g/gsVideo.js\"></script>";

                string title = news.Title;
                string subTitle = news.SubTitle;

                #region 相关阅读
                List<RelatedReadings> relatedReadings = await ApiService.Instance.GetRelatedReadings(CurrentEssay.ContentId, CurrentEssay.ContentType);

                string relatedReadingsHtml =
                    @"<div class=""list"" id=""gsTemplateContent_RelatedReading"">
                    <div class=""tit yellow"" style=""border-left:5px solid #FFC600"">相关阅读</div>
                           <div class=""txtlist"" id=""gsTemplateContent_RelatedReadingContent"">";
                if (relatedReadings != null && relatedReadings.Count == 0)
                {
                    relatedReadingsHtml += "";
                }
                if (relatedReadings != null)
                {
                    foreach (var item in relatedReadings)
                    {
                        relatedReadingsHtml += "<a class=\"Row\" href=\"javascript:void(0)\" onclick=\"SendNotify(\"" + item.contentId + "\")\"><div>" + item.title + "</div></a>";
                    }
                    relatedReadingsHtml += "</div></div>";
                }
                #endregion

                tempHtml.Clear();
                tempHtml.Append("<!DOCTYPE html>" +
                    "<html>" +
                        "<head>" + head + "</head>" +
                        "<body quick-markup_injected=\"true\" onload=\"onLoad()\">" +
                            "<GSAppHTMLTemplate version=\"1.4.6\"/>" +
                             //"<div id=\"ScrollToTop\"><a href=\"#top\">#</a></div>" +
                             //"<div id=\"ScrollToTop\"><a href=\"javascript:scroller(body,100);\">#</a></div>" +
                             "<div id=\"body\" class=\"fontsizetwo\">" +
                                  "<h1 class=\"heading\" id=\"gsTemplateContent_Title\">" + title + "</h1>" +
                                  "<span class=\"info\" id=\"gsTemplateContent_Subtitle\">" + subTitle + "</span>" +
                                  "<div class=\"bar\"></div>" +
                                  "<div class=\"content\" id=\"gsTemplateContent_MainBody\">" + mainBody + "</div>" +
                                  "<div id=\"gsTemplateContent_AD1\"></div>" +
                                  "<div class=\"list\" id=\"gsTemplateContent_RelatedTopic\">" +
                                        "<div class=\"tit red\" style=\"border-left:5px solid #f22f09;\">相关专题</div>" +
                                            "<div id=\"gsTemplateContent_RelatedTopicContent\">" +
                                            "</div>" +
                                        "</div>" +
                                  "</div>" +
                                         relatedReadingsHtml +
                             "</div>" +
                        "</body>" +
                        "<script type=\"text/javascript\">" +
                        @"function resizeVideo(){
                         var winWidth = document.body.clientWidth;
			            var iframes = document.getElementsByTagName('iframe');
			            if (iframes != null) {
				            for (var i = 0; i < iframes.length; i++) {
					            iframes[i].removeAttribute('style');
					            iframes[i].width = winWidth;
					            iframes[i].height = winWidth * (9 / 16);
				            }
			            }
			            var embeds = document.getElementsByTagName('embed');
			            if (embeds != null) {
				            for (var j = 0; j < embeds.length; j++) {
					            var embedTag = embedTags[j];
					
					            embedTag.removeAttribute('style');
					            embedTag.height = winWidth * (9 / 16);
					            embedTag.width = winWidth;
				            }
			            }
                        //div
			            var player = document.getElementById('youkuplayer_0');
			            if (player != null) {
				            player.removeAttribute('style');
				            player.style.width = winWidth + 'px';
				            player.style.height = winWidth * (9 / 16) + 'px';
			            }}" +
                        "var body = document.getElementsByTagName('body')[0]; " +
                       @"document.onreadystatechange = function () { resizeVideo(); }
                        window.onresize = function(){
                            resizeVideo();
                    };
                    </script>" +
                "</html>");

                ContentHtml = tempHtml.ToString();
                OnPropertyChanged("ContentHtml");
            }
            IsActive = false;
        }
         
        public void GenerateComments()
        {
            Comments = new EssayCommentsCollection(CurrentEssay.ContentId);
            OnPropertyChanged("Comments");
        }
        #endregion

        public void RefreshContent()
        {
          
        }
    }
}
