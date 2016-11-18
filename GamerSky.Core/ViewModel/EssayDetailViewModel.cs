﻿using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.Storage;
using Windows.UI.Xaml;
using GamerSky.Core.Helper;
using GamerSky.Core.Http;
using GamerSky.Core.Model;

namespace GamerSky.Core.ViewModel
{
    public class EssayDetailViewModel : ViewModelBase
    {
        private ApiService apiService;
        private StringBuilder tempHtml = new StringBuilder();

        private Essay essayResult = new Essay();
        public Essay EssayResult
        {
            get
            {
                return essayResult;
            }
            set
            {
                essayResult = value;
                OnPropertyChanged();
            }
        }

        public ObservableCollection<string> HtmlCollection { get; set; } = new ObservableCollection<string>();

        public EssayDetailViewModel()
        {
            apiService = new ApiService();
            EssayResult = new Essay();
            AppTheme = DataShareManager.Current.AppTheme;
            DataShareManager.Current.ShareDataChanged += Current_ShareDataChanged;
        }

        private void Current_ShareDataChanged()
        {
            AppTheme = DataShareManager.Current.AppTheme;
        }
         
         
        #region Properties
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

        private string htmlString;
        /// <summary>
        /// HTML正文
        /// </summary>
        public string HtmlString
        {
            get
            {
                return htmlString;
            }
            set
            {
                htmlString = value;
                OnPropertyChanged(); 
            }
        }

        private string commentString;
        /// <summary>
        /// 评论Html
        /// </summary>
        public string CommentString
        {
            get
            {
                return commentString;
            }
            set
            {
                commentString = value;
                OnPropertyChanged(); 
            }
        }

        private string originUri;
        public string OriginUri
        {
            get
            {
                return originUri;
            }
            set
            {
                originUri = value;
                OnPropertyChanged();
            }
        }

        private string body;
        public string Body
        {
            get
            {
                return body;
            }
            set
            {
                body = value;
                OnPropertyChanged();
            }
        }

        private string title;
        public string Title
        {
            get
            {
                return title;
            }
            set
            {
                title = value;
                OnPropertyChanged();
            }
        }

        private string subTitle;
        public string SubTitle
        {
            get
            {
                return subTitle;
            }
            set
            {
                subTitle = value;
                OnPropertyChanged();
            }
        }
         
        /// <summary>
        /// 相关阅读
        /// </summary>
        public ObservableCollection<RelatedReadings> RelatedReadings { get; set; }

        #endregion


        #region GenerateContent

        /// <summary>
        /// 生成新闻内容网页
        /// </summary>
        public async Task GenerateHtmlString(Essay essay)
        {
            IsActive = true;
            News news = await apiService.ReadEssay(essay.ContentId);
            if (news != null)
            {
                EssayResult = essay;
                OriginUri = news.OriginURL;

                string mainBody = news.MainBody;

                string head = "<meta http-equiv=\"Content-Type\" content=\"text/html; charset = utf-8\" />"
                            + "<meta name=\"viewport\" content=\"width= device-width, user-scalable = no\" />"
                            + "<meta name=\"format-detection\" content=\"telephone=no,email=no\">" //忽略电话号码和邮箱
                            + "<meta name=\"msapplication-tap-highlight\" content=\"no\">" //wp点击无高光;
                             + "<link type=\"text/css\" rel=\"stylesheet\" href=\"ms-appx-web:///Assets/Css/gs.css\"/>"
                            + "<script src=\"ms-appx-web:///Assets/js/gs.js\"></script>"
                            + "<script src=\"ms-appx-web:///Assets/js/gsVideo.js\"></script>";

                string title = news.Title;
                string subTitle = news.SubTitle;

                #region 相关阅读
                List<RelatedReadings> relatedReadings = await apiService.GetRelatedReadings(essay.ContentId, essay.ContentType);

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
                        relatedReadingsHtml += "<a class=\"Row\" href=\"openPageWithContentId:" + item.contentId + "\"><div>" + item.title + "</div></a>";
                    }
                    relatedReadingsHtml += "</div></div>";
                }
                #endregion

                tempHtml.Clear();
                tempHtml.Append("<!DOCTYPE html>" +
                    "<html>" +
                        "<head>" + head + "</head>" +
                        "<body quick-markup_injected=\"true\">" +
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
                        "var body = document.getElementsByTagName('body')[0]; window.onload = InitGesture(body);" +
                       @"document.onreadystatechange = function () { resizeVideo(); }
                        window.onresize = function(){
                            resizeVideo();
                    };
                    </script>" +
                "</html>");

                HtmlString = tempHtml.ToString() ; 
            }
            IsActive = false;
        }

        /// <summary>
        /// 生成评论网页
        /// </summary>
        /// <param name="essay"></param>
        public async void GenerateCommentString(Essay essay)
        {
            IsActive = true;
            EssayResult = essay;
            var file = await StorageFile.GetFileFromApplicationUriAsync(new Uri("ms-appx:///Assets/Html/Comment.html"));
            CommentString = await FileIO.ReadTextAsync(file);
            CommentString = CommentString.Replace("{0}", essay.Title).Replace("{1}", essay.ContentId);

            IsActive = false;
        } 
        #endregion
    }
}
