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

                //基础样式
                string baseCss = "<style>"
                       + "html{-ms-content-zooming:none;font-family:微软雅黑;}"
                       + ".author{font-weight:bold;} .bio{color:gray;}"
                       + "body{padding:0px;word-break:break-all;} p{margin:10px auto;} a{color:skyblue;}"
                       + "body{line-height:120%; font:normal 100% Helvetica, Arial, sans-serif;}"
                       + "img{height:auto;width:auto;width:100%}"
                       + "h1{ text-align:left; font-size:1em;}" //标题栏
                       +".bar { display:block; border-top: 1px solid #bbb;width:auto;height:auto;fontsize:19px;}" 
                       +".heading {margin: 0; padding: 0; top: 22px; line-height:28px; color:#333;}"
	                   + ".PageColorMode_Day.heading {color:#333;}"
    	               + ".PageColorMode_Night.heading {color:#966122;}"
                       + ".PageColorMode_Day .bar {background-color:#eaeaea;}"
	                   + ".PageColorMode_Night .bar {background-color:#2a2e38;}"+
                        "img {border:0;}"+
                        "a {color:#3871c8;text-decoration:none;}"+
                        "a:focus{ outline: none;}"+
	                    ".PageColorMode_Day a{}"+
	                    ".PageColorMode_Night a{color:#3e6bb6;}"+
                        "div,ul,li { overflow: hidden;}"+
                        "ul { margin: 0; padding: 0; list-style-type:none;}"+
                        "li { list-style-type:none; vertical-align:middle;}"+
	                    ".PageColorMode_Day.heading {color:#333;}"+
	                    ".PageColorMode_Night.heading {color:#966122;}"+
	                    ".PageColorMode_Day.bar {background-color:#eaeaea;}"+
	                    ".PageColorMode_Night.bar {background-color:#2a2e38;}"+
                        ".adnone { display: none;}"+
                        ".info{margin: 0 0px; padding: 0px 4px; line-height:22px; color: gray; font-size:14px; color:#848484;}"+
	                    ".PageColorMode_Day.info {color:#848484;}"+
	                    ".PageColorMode_Night.info {color:#464950;}"+
                        "</style>";

                //相关阅读
                string relatedReadingsCss = "<style>" +
                    ".list {display: block; width: auto; height: auto; border-radius:2px; border: 1.0px solid #e0e0e0; border-top:7px solid #ddd; border-bottom:1px solid #bbb; padding:10px 10px 0 10px;  background:#f7f7f7;}"+
	                ".PageColorMode_Day.list {border: 1.0px solid #e0e0e0; border-top:7px solid #ddd; border-bottom:1px solid #bbb; background:#f7f7f7;}"+
	                ".PageColorMode_Night.list {border: 1.0px solid #1d1c21; border-top:7px solid #0e0e0e; border-bottom:1px solid #040404; background:#17181a;}"+
                    ".list.tit { margin: 0 0 10px - 10px; padding-left:5px; width: auto; height: 20px; line-height:20px; font-weight:bold;}"+
	                ".PageColorMode_Day.list.tit {color:#333;}"+
	                ".PageColorMode_Night.list.tit {color:#576476;}"+
                    ".list.txtlist { display: block; width: auto; height: auto;}"+
                    ".list.txtlist a { display: block;}"+
                    ".list.txtlist.Row { display: block; width: 100%; height: 40px; line-height:40px; font-size:14px; white-space:nowrap; text-overflow:ellipsis;}"+
                    ".PageColorMode_Day.list.txtlist.Row {border-top:1.0px solid #e0e0e0; color:#333;}"+
                    ".PageColorMode_Night.list.txtlist.Row {border-top:1.0px solid #2a2e38; color:#576476;}"+
                    ".list.more {display: block; border-top:1.0px solid #2a2e38; width:100%; font-size:14px; height:40px; line-height:40px; text-align:center; color:#5f5f69;}"+
                    ".PageColorMode_Day.list.more {border-top:1.0px solid #e0e0e0; color:#333;}"+
                    ".PageColorMode_Night.list.more {border-top:1.0px solid #2a2e38; color:#5f5f69;}"+
                    ".list.more a { display: block;}"+
                    ".PageColorMode_Day.list.tit.red {border-left:5px solid #f22f09;}"+
                    ".PageColorMode_Night.list.tit.red {border-left:5px solid #812c25;}"+
                    ".PageColorMode_Day.list.tit.yellow {border-left:5px solid #ffc600;}"+
                    ".PageColorMode_Night.list.tit.yellow {border-left:5px solid #867025;}"+
                    ".PageColorMode_Day.list.tit.blue {border-left:5px solid #3fa5ee;}"+
                    ".PageColorMode_Night.list.tit.blue {border-left:5px solid #477399;}"+
                    "</style>";

                //#region CSS
                ////相关专题
                //string relatedTopicCss = "<style>" +
                //    "#gsTemplateContent_RelatedTopic {display:none;-webkit-user-select:none;}"+
                //    "#gsTemplateContent_RelatedTopicContent {display:block; width:100%; height:auto; padding:0px;}"+
                //    "#gsTemplateContent_RelatedTopicContent table {margin-bottom:10px; border:1.0px solid #d3d3d3; width:100%; height:74px;}"+
	               // ".PageColorMode_Day #gsTemplateContent_RelatedTopicContent table {border:1.0px solid #d3d3d3;}"+
	               // ".PageColorMode_Night #gsTemplateContent_RelatedTopicContent table {border:1.0px solid #222b38;}"+
                //    "#gsTemplateContent_RelatedTopicContent .Content img {float:left; margin:0px 6px 0px 6px; width:94px; height:54px;}"+
                //    "#gsTemplateContent_RelatedTopicContent .Content .Title {float:none; width:auto; height:34px; line-height:54px; font-size:16px;}"+
	               // ".PageColorMode_Day #gsTemplateContent_RelatedTopicContent .Content .Title {color:#333;}"+
	               // ".PageColorMode_Night #gsTemplateContent_RelatedTopicContent .Content .Title {color:#576476;}"+
                //    "#gsTemplateContent_RelatedTopicContent .Content .Subtitle {float:none; width:auto; height:20px; line-height:20px; font-size:14px;}"+
	               // ".PageColorMode_Day #gsTemplateContent_RelatedTopicContent .Content .Subtitle {color:#a5a5a5;}"+
	               // ".PageColorMode_Night #gsTemplateContent_RelatedTopicContent .Content .Subtitle {color:#464950;}"+
                //    "#gsTemplateContent_RelatedTopicContent .ButtonArea {width:74px;}"+
                //    "#gsTemplateContent_RelatedTopicContent .Button {border-radius:2px; width:54px; height:30px; line-height:30px; text-align:center; font-size:12px;}"+
	               // ".PageColorMode_Day #gsTemplateContent_RelatedTopicContent .Button {background:#ee5449; color:#f7f7f7;}"+
	               // ".PageColorMode_Night #gsTemplateContent_RelatedTopicContent .Button {background:#812c25; color:#c6a09d;}"+
                //    "#gsTemplateContent_RelatedTopicContent .Readed {}"+
	               // ".PageColorMode_Day #gsTemplateContent_RelatedTopicContent .Readed {background:#6699ff;}"+
	               // ".PageColorMode_Night #gsTemplateContent_RelatedTopicContent .Readed {background:#336699;}"+
                //    "</style>";

                ////视频css
                //string videoCss= "<style>"+
                //    ".GSTemplateContent_VideoBoxer{max-width: 100% ; width:100%;height:100%;display:block;}"+
                //    ".GSTemplateContent_VideoBoxer {background-position:center center; background-repeat:no-repeat; background-size: 177px 49px; background-image:url(../Html/gsAppHTMLTemplate_image/gsAppHTMLTemplate_Video_Background@2x.png);}" +
                //    ".PageColorMode_Day.GSTemplateContent_VideoBoxer {background-color: #eeeeee;}"+
	               // ".PageColorMode_Night.GSTemplateContent_VideoBoxer {background-color: #1B1A1F;}"+
                //    ".GSTemplateContent_VideoBoxer.PlayButtonBackground{"+
                //    "float:none;border-radius:40px;width: 80px;height: 80px;background-position:center center;background-repeat:no-repeat;-webkit-filter:blur(4px);}"+
                //    ".GSTemplateContent_VideoBoxer.PlayButton{"+
                //    "float:none;margin-top:-80px;border-radius:40px;width: 80px;height: 80px;background-repeat:no-repeat;"+
                //    "background-position:center center;"+
                //    "background-size:80px 80px;"+
                //    "background-image:url(../Html/gsAppHTMLTemplate_image/gsAppHTMLTemplate_Video_PlayButton@2x.png);"+
                //    "-webkit-filter:hue-rotate(0deg);}"+
                //    "</style>";

                ////表格样式
                //string listcss = "<style>" +
                //     "table {color:#ccc;}" +
                //     ".table2 {margin: 0 auto; width: 98%; border: 1.0px solid #555; border-collapse:collapse; background-color:#333; cursor:default;}" +
                //     ".table2 td {padding: 4px; border: 1.0px solid #555;}" +
                //     ".tr2 {text-align:center; background-color:#000;}" +
                //     "</style>";

                //string imgcss="<style>"+
                //    ".ImgBoxer { display: inline-block; float:none; text-align:center;}"+
                //    ".ImgBoxer img { float:left;}"+
                //    ".ImgBoxerStatusBar{display: inline;visibility: hidden;float:left;margin: -44px 0px 0px 0px;width: 100 %;height: 44px;line-height:40px;text-align:right;}"+
                //    ".ImgBoxerStatusBar.ImgSetLogo{float:right;margin: 0px;width: 100 %;height: 44px;"+
                //    "background-image: url(../gsAppHTMLTemplate_image/imgSet_BottomBarBackground@2x.png);"+
                //    "background-size: 350px 44px;background-repeat: no-repeat;"+
                //    "background-position: center top;padding: 0px;line-height: 44px;text-align: center;font-size: 18px;font - weight: bold;color: #fff;}"+
                //    ".ImgBoxerStatusBar.ImgSetLogo.Icon{"+
                //    "background-image: url(../gsAppHTMLTemplate_image/imgSet_ExpandIcon@2x.png);"+
                //    "background-size: 25px 20px;background-position: center center;background-repeat: no-repeat;}"+
                //    ".ImgBoxerStatusBar.ImgSetLogo.Caption{font-size: 16px;color: #ffffff;}"+
                //    ".ImgBoxerStatusBar.GIFLogo{float:right;margin: 7px 0px 0px 0px;width: 60px;"+
                //    "height: 37px;background-image:url(../gsAppHTMLTemplate_image/img_Badge_GIF@2x.png);"+
                //    "background-size:100 % 100 %;}"+
                //    ".ImgSetBoxer {margin: 0px 10px 10px 2px; box-shadow:0px 0px 1px #000;}"+
                //    ".ImgSetBoxer img{ width: 100 %;}"+
                //    ".ImgSetWhitePage {margin: 0px 10px 0px 0px; box-shadow:0px 0px 1px #000;}"+
	               // ".PageColorMode_Day.ImgSetWhitePage {background-color:#eee}"+
	               // ".PageColorMode_Night.ImgSetWhitePage {background-color:#2c2c2c}"+
                //    ".GSTemplateImageLoader { display: none;}"+
                //    "#gsTemplateContent_MainBody {line-height:150%;}"+
                //    "#gsTemplateContent_MainBody p {display:block;}"+
                //    "#gsTemplateContent_MainBody img {display:none; visibility:hidden;}"+
                //    "#gsTemplateContent_MainBody #youkuplayer0 img {display:inherit; visibility:visible;}"+
                //    "#gsTemplateContent_RelatedReading {display:none;}"+
                //    "#gsTemplateContent_RelatedReadingContent {margin-left:-10px; margin-right:-10px;}"+
                //    "#gsTemplateContent_RelatedReadingContent .Row {padding:0px;}"+
                //    "#gsTemplateContent_RelatedReadingContent .Row div {margin:0px 10px 0px 10px; white-space:nowrap; text-overflow:ellipsis;}"+
                //    "#gsTemplateContent_RelatedReadingContent .Button {text-align:center; color:#888;}"+
                //    ".PageColorMode_Day #gsTemplateContent_RelatedReadingContent .Button {color:#888;}"+
                //    ".PageColorMode_Night #gsTemplateContent_RelatedReadingContent .Button {color:#5a5a60;}"+
                //"</style>";
                //#endregion

                string head = "<meta http-equiv=\"Content-Type\" content=\"text/html; charset = utf-8\" />"
                            + "<meta name=\"viewport\" content=\"width= device-width, user-scalable = no\" />"
                            + "<meta name=\"format-detection\" content=\"telephone=no,email=no\">" //忽略电话号码和邮箱
                            + "<meta name=\"msapplication-tap-highlight\" content=\"no\">" //wp点击无高光;
                            + "<link type=\"text/css\" rel=\"stylesheet\" href=\"ms-appx-web:///Assets/gsAppHTMLTemplate_css/base.css\"/>"
                            + "<script type=\"text/javascript\" src=\"ms-appx-web:///Assets/gsAppHTMLTemplate_js/gsAppHTMLTemplate.js\"></script>";

                string videoJs = "<script src=\"ms-appx-web:///Assets/gsAppHTMLTemplate_js/gsAppHTMLTemplate.js\"></script>" +
                  "<script src=\"ms-appx-web:///Assets/gsAppHTMLTemplate_js/gsAppHTMLTemplate_Video.js\"></script>" +
                  "<link href=\"ms-appx-web:///Assets/gsAppHTMLTemplate_css/base.css\" rel=\"stylesheet\" type=\"text/css\"/>" +
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
                        "<head>" + head +baseCss + relatedReadingsCss + videoJs+ "</head>" +
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
