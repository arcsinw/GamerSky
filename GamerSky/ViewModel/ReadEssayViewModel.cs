using GamerSky.Http;
using GamerSky.IncrementalLoadingCollection;
using GamerSky.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.ApplicationModel;
using Windows.Storage;
using Windows.UI.Xaml;

namespace GamerSky.ViewModel
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

        public async void SetCurrentContentId(string contentId,int currentIndex)
        {
            ContentId = contentId;
            Comments?.Clear();
            switch (currentIndex)
            {
                case 0:
                    await GenerateHtmlString(contentId);
                    break;
                case 1:
                    GenerateComments(ContentId);
                    break;
            }
        }

        #region Properties
        private string contentHtml;

        public string ContentHtml
        {
            get { return contentHtml; }
            set { contentHtml = value; OnPropertyChanged(); }
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

        private Essay currentEssay;

        public Essay CurrentEssay
        {
            get { return currentEssay; }
            set
            {
                currentEssay = value;
                OnPropertyChanged();
            }
        }

        private string contentId;

        public string ContentId
        {
            get { return contentId; }
            set { contentId = value; }
        }


        private bool isNoImgMode;

        public bool IsNoImgMode
        {
            get { return isNoImgMode; }
            set { isNoImgMode = value; OnPropertyChanged(); }
        }

        private int essayFontSize = 12;

        public int EssayFontSize
        {
            get { return essayFontSize; }
            set { essayFontSize = value;  OnPropertyChanged(); DataShareManager.Current.UpdateFontSize(value); }
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
                            //+ "<script type=\"text/javascript\" src=\"ms-appx-web:///Assets/Js/gsAppHTMLTemplate.js\"></script>"
                            + "<script type=\"text/javascript\" src=\"ms-appx-web:///Assets/Js/gesture.js\"></script>"
                            + "<script type=\"text/javascript\" src=\"ms-appx-web:///Assets/Js/jquery.min.js\"></script>"
                            + "<script type=\"text/javascript\" src=\"ms-appx-web:///Assets/Js/jquery.lazyload.js\"></script>"
                            + "<script type=\"text/javascript\" src=\"ms-appx-web:///Assets/Js/gs.js\"></script>"
                            + "<script type=\"text/javascript\" src=\"ms-appx-web:///Assets/Js/gsAppHTMLTemplate.js\"></script>"
                            + "<script type=\"text/javascript\" src=\"ms-appx-web:///Assets/Js/gsAppHTMLTemplate_Video.js\"></script>";

                string title = news.Title;
                string subTitle = news.SubTitle;

                #region 相关阅读
                List<RelatedReadings> relatedReadings = await ApiService.Instance.GetRelatedReadings(CurrentEssay.ContentId, CurrentEssay.ContentType);

                StringBuilder relatedReadingsHtml = new StringBuilder(string.Empty);
                 
                if (relatedReadings != null)
                {
                    foreach (var item in relatedReadings)
                    {
                        StringBuilder tmp = new StringBuilder();
                        //tmp.Append($"<div class=\"Thumbnail\"><img src=\"{item.thumbnailUrl}\"></div>");
                        tmp.Append($"<div>{item.title}</div>");

                        relatedReadingsHtml.Append($@"<a href=""javascript:void(0)"" onclick=""OpenEssayById({item.contentId})""><div class=""Row""><div>{tmp}</div></div></a>");

                        tmp.Clear();    
                    }
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
                                        "<div class=\"tit red\" >相关专题</div>" + //style=\"border-left:5px solid #f22f09;\"
                                            "<div id=\"gsTemplateContent_RelatedTopicContent\">" +
                                            "</div>" +
                                        "</div>" +
                                  "</div>" +
                                  $@"<div class=""list"" id=""gsTemplateContent_RelatedReading"">
	                                        <div class=""tit yellow"">相关阅读</div>
	                                            <div class=""txtlist"" id=""gsTemplateContent_RelatedReadingContent"">
    	                                            {relatedReadingsHtml}
	                                            </div>
                                        </div>"+
                                         //relatedReadingsHtml +
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

        public async Task GenerateHtmlString(string contentId)
        {
            IsActive = true;
            News news = await ApiService.Instance.ReadEssay(contentId);
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
                            //+ "<script type=\"text/javascript\" src=\"ms-appx-web:///Assets/Js/gsAppHTMLTemplate.js\"></script>"
                            + "<script type=\"text/javascript\" src=\"ms-appx-web:///Assets/Js/gesture.js\"></script>"
                            + "<script type=\"text/javascript\" src=\"ms-appx-web:///Assets/Js/jquery.min.js\"></script>"
                            + "<script type=\"text/javascript\" src=\"ms-appx-web:///Assets/Js/jquery.lazyload.js\"></script>"
                            + "<script type=\"text/javascript\" src=\"ms-appx-web:///Assets/Js/gs.js\"></script>";

                string title = news.Title;
                string subTitle = news.SubTitle;

                #region 相关阅读
                List<RelatedReadings> relatedReadings = await ApiService.Instance.GetRelatedReadings(contentId);

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
                        relatedReadingsHtml += $@"<a class=""Row"" href=""javascript:void(0)"" onclick=""OpenEssayById({item.contentId})""><div>" + item.title + "</div></a>";
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

        public void GenerateComments(string contentId)
        {
            Comments = new EssayCommentsCollection(contentId);
            OnPropertyChanged("Comments");
        }

        /// <summary>
        /// 生成评论网页
        /// </summary>
        /// <param name="essay"></param>
        public async void GenerateCommentString()
        {
            IsActive = true;
            var file = await StorageFile.GetFileFromApplicationUriAsync(new Uri("ms-appx:///Assets/Html/Comment.html"));
            CommentString = await FileIO.ReadTextAsync(file);

            if (CurrentEssay != null)
            {
                CommentString = CommentString.Replace("{0}", CurrentEssay.Title).Replace("{1}", CurrentEssay.ContentId);
            }
            else
            {
                CommentString = CommentString.Replace("{0}", "评论").Replace("{1}", ContentId);
            }

            IsActive = false;
        }
        #endregion

        public async void RefreshContent()
        {
            ContentHtml = string.Empty;
            await GenerateHtmlString();
        }

        public void RefreshComments()
        {
            //Comments?.Clear();
            //Comments = new EssayCommentsCollection(ContentId,1);

            GenerateCommentString();
            OnPropertyChanged("Comments");
        }
    }
}
