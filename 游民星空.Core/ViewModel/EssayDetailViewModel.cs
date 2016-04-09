using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.Storage;
using Windows.UI.Xaml;
using 游民星空.Core.Helper;
using 游民星空.Core.Http;
using 游民星空.Core.Model;

namespace 游民星空.Core.ViewModel
{
    public class EssayDetailViewModel : ViewModelBase
    {
        private ApiService apiService;
        public Essay essayResult { get; set; }

        public EssayDetailViewModel(Essay essay)
        {
            IsActive = true;
            apiService = new ApiService();
            this.essayResult = essay;

            AppTheme = DataShareManager.Current.AppTheme;
            DataShareManager.Current.ShareDataChanged += Current_ShareDataChanged;
             
        }

        private void Current_ShareDataChanged()
        {
            AppTheme = DataShareManager.Current.AppTheme;
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

       /// <summary>
       /// 翻译按钮显示
       /// </summary>
        public bool IsTranslateVisible
        {
            get
            {
                return ExperimentHelper.GetBool(ExperimentHelper.TranslateButtonVisibility,false);
            }
        }
        //private ObservableCollection<RelatedReadings> relatedReadings;
        ///// <summary>
        ///// 相关阅读
        ///// </summary>
        //public ObservableCollection<RelatedReadings> RelatedReadings
        //{
        //    get
        //    {
        //        return relatedReadings;
        //    }
        //    set
        //    {
        //        relatedReadings = value;
        //        OnPropertyChanged();
        //    }
        //}

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
        /// 生成新闻内容网页
        /// </summary>
        public async Task GenerateHtmlString()
        {
            IsActive = true;
            News news = await apiService.ReadEssay(essayResult.contentId);
            if (news != null)
            {
                OriginUri = news.originURL;

                string mainBody = news.mainBody;

                string head = "<meta http-equiv=\"Content-Type\" content=\"text/html; charset = utf-8\" />"
                            + "<meta name=\"viewport\" content=\"width= device-width, user-scalable = no\" />"
                            + "<meta name=\"format-detection\" content=\"telephone=no,email=no\">" //忽略电话号码和邮箱
                            + "<meta name=\"msapplication-tap-highlight\" content=\"no\">" //wp点击无高光;
                            + "<link type=\"text/css\" rel=\"stylesheet\" href=\"ms-appx-web:///Assets/css/gs.css\"/>"
                            + "<script src=\"ms-appx-web:///Assets/js/gs.js\"></script>";

                string videoJs = "<script src=\"ms-appx-web:///Assets/gsAppHTMLTemplate_js/gsAppHTMLTemplate.js\"></script>" +
                  "<script src=\"ms-appx-web:///Assets/gsAppHTMLTemplate_js/gsAppHTMLTemplate_Video.js\"></script>" +
                 "<script src=\"ms-appx-web:///Assets/gsAppHTMLTemplate_js/gsVideo.js\"></script>";

                string title = news.title;
                string subTitle = news.subTitle;

                #region 相关阅读
                //List<RelatedReadings> relatedReadings = await apiService.GetRelatedReadings(essayResult.contentId, essayResult.contentType);

                //string relatedReadingsHtml = 
                //    "<div class=\"list\" id=\"gsTemplateContent_RelatedReading\">" +
                //    "<div class=\"tit yellow\">相关阅读</div>" +//相关阅读
                //           "<div class=\"txtlist\" id=\"gsTemplateContent_RelatedReadingContent\">";
                //if (relatedReadings != null && relatedReadings.Count == 0)
                //{
                //    relatedReadingsHtml = "";
                //}
                //if (relatedReadings != null)
                //{          
                //    foreach (var item in relatedReadings)
                //    {
                //        relatedReadingsHtml += "<a id=\"RelatedReadings\" href=\"" + item.contentId + "\"><div class=\"Row\"><div>" + item.title + "</div></div></a>";
                //    }
                //    relatedReadingsHtml += "</div></div>";
                //}
                #endregion
                 
                HtmlString = "<!DOCTYPE html>" +
                    "<html>" +
                        "<head>" + head + "</head>" +
                        "<body quick-markup_injected=\"true\">" +
                            "<GSAppHTMLTemplate version=\"1.4.6\"/>" +
                             "<div id=\"body\" class=\"fontsizetwo\">" +
                                  "<h1 class=\"heading\" id=\"gsTemplateContent_Title\">" + title + "</h1>" +
                                  "<span class=\"info\" id=\"gsTemplateContent_Subtitle\">" + subTitle + "</span>" +
                                  "<div class=\"bar\"></div>" +
                                  "<div class=\"content\" id=\"gsTemplateContent_MainBody\">" + mainBody + "</div>" +
                                  "<div id=\"gsTemplateContent_AD1\"></div>" +
                                  "<div class=\"list\" id=\"gsTemplateContent_RelatedTopic\">" +
                                        "<div class=\"tit red\">相关专题</div>" +
                                            "<div id=\"gsTemplateContent_RelatedTopicContent\">" +
                                            "</div>" +
                                        "</div>" +
                                  "</div>" +
                                         //relatedReadingsHtml +
                             "</div>"+
                        "</body>"+
                        "<script type=\"text/javascript\">"+
                       @"window.onresize = function(){
                        var winWidth = document.body.clientWidth;
		                var iframes = document.getElementsByTagName('iframe');
		                if(iframes!=null)
		                {
			                for(var i=0;i<iframes.length;i++)
			                {
                                iframes[i].removeAttribute('style');
				                iframes[i].width = winWidth;
				                iframes[i].height = winWidth * (3/4);
			                }
		                }
		
	                    var embeds = document.getElementsByTagName('embed');
	                    if(embeds!=null)
	                    {
	                        for (var j = 0;j < embeds.length;j++)
	                        {
	                            var embedTag = embedTags[j];
	                            embedTag.removeAttribute('style');
	                            embedTag.height = winWidth * (3/4);
	                            embedTag.width = winWidth;
	                        }
		                }
	    
		                var player = document.getElementById('youkuplayer_0');
		                if(player!=null)
		                {
                                player.removeAttribute('style');
				                player.width = winWidth;
				                player.height = winWidth *(3/4);
		                }
                    };
                    </ script >" +
                "</html>";
            }
            IsActive = false;
        }

       public void GenerateCommentString()
        {
            IsActive = true;
            CommentString ="<!DOCTYPE html><html><body><div id=\"SOHUCS\" sid=\"" + essayResult.contentId + "\"></div>" +
                            //"<script charset=\"utf-8\" type=\"text/javascript\" src=\"http://changyan.sohu.com/upload/changyan.js\"></script>" +
                            "<script type=\"text/javascript\">" +
                        @"(function() {
                                var expire_time = parseInt((new Date()).getTime() / (5 * 60 * 1000));
                                var head = document.head || document.getElementsByTagName(""head"")[0] || document.documentElement;
                                var script_version = document.createElement(""script""), script_cyan = document.createElement(""script"");
                                script_version.type = script_cyan.type = ""text/javascript"";
                                script_version.charset = script_cyan.charset = ""utf-8"";
                                script_version.onload = function() {
                                    script_cyan.id = 'changyan_mobile_js';
                                    script_cyan.src = 'http://changyan.itc.cn/upload/mobile/wap-js/changyan_mobile.js?client_id=cyqQwkOU4&'
                                                    + 'conf=prod_9627c45df543c816a3ddf2d8ea686a99&version=' + cyan_resource_version;
                                    head.insertBefore(script_cyan, head.firstChild);
                                };
                                script_version.src = 'http://changyan.sohu.com/upload/mobile/wap-js/version.js?_=' + expire_time;
                                head.insertBefore(script_version, head.firstChild);
                        })();"+
                "</script></body></html>";
            IsActive = false;
        }
    }
}
