using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using 游民星空.Core.Http;
using 游民星空.Core.Model;

namespace 游民星空.Core.ViewModel
{
    public class EssayDetailViewModel : ViewModelBase
    {
        private ApiService apiService;
        private EssayResult essayResult;

        public EssayDetailViewModel(EssayResult essay)
        {
            IsActive = true;
            apiService = new ApiService();
            this.essayResult = essay;

            //GenerateHtmlString();
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

        private ObservableCollection<RelatedReadingsResult> relatedReadings;
        /// <summary>
        /// 相关阅读
        /// </summary>
        public ObservableCollection<RelatedReadingsResult> RelatedReadings
        {
            get
            {
                return relatedReadings;
            }
            set
            {
                relatedReadings = value;
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
        /// 生成网页
        /// </summary>
        public async Task GenerateHtmlString()
        {
            News news = await apiService.ReadEssay(essayResult.contentId);
            if (news != null)
            {
                OriginUri = news.result.originURL;

                string body = news.result.mainBody;
                string css = "<style>"
                       + "html{-ms-content-zooming:none;font-family:微软雅黑;}"
                       + ".author{font-weight:bold;} .bio{color:gray;}"
                       + "body{padding:8px;word-break:break-all;} p{margin:10px auto;} a{color:skyblue;}"
                       + "body{line-height:120%; font:normal 100% Helvetica, Arial, sans-serif;}"
                       + "img{height:auto;width:auto;width:100%}"
                       + "h1{ text-align:left; font-size:1em;}" //标题栏
                       +".bar { display:block; border-top: 1px solid #bbb;width:auto;height:auto;fontsize:19px;}" 
                       +".heading {margin: 0; padding: 0; top: 22px; line-height:28px; color:#333;}"
	                   + ".PageColorMode_Day.heading {color:#333;}"
    	               + ".PageColorMode_Night.heading {color:#966122;}"
                       + ".PageColorMode_Day .bar {background-color:#eaeaea;}"
	                   + ".PageColorMode_Night .bar {background-color:#2a2e38;}"
                       + "</style>";   //基础css
                string baseCss = "<style>"+
                    "img {border:0;}"+
                    "a {color:#3871c8;text-decoration:none;}"+
                    "a: focus{ outline: none;}"+
	                ".PageColorMode_Day a{}"+
	                ".PageColorMode_Night a{color:#3e6bb6;}"+
                    "div,ul,li { overflow: hidden;}"+
                    "ul { margin: 0; padding: 0; list-style-type:none;}"+
                    "li { list-style-type:none; vertical align:middle;}"+
	                ".PageColorMode_Day.heading {color:#333;}"+
	                ".PageColorMode_Night.heading {color:#966122;}"+
	                ".PageColorMode_Day.bar {background-color:#eaeaea;}"+
	                ".PageColorMode_Night.bar {background-color:#2a2e38;}"+
                    ".adnone { display: none;}"+
                    ".info{margin: 0 10px; padding: 0px 4px; line-height:22px; color: gray; font-size:14px; color:#848484;}"+
	                ".PageColorMode_Day.info {color:#848484;}"+
	                ".PageColorMode_Night.info {color:#464950;}"+
                    "</style>";
                string videoCss= "<style>"+
                    ".GSTemplateContent_VideoBoxer{max-width: 100% ; width:100%;height:100%;display:block;}"+
                    ".GSTemplateContent_VideoBoxer {background-position:center center; background-repeat:no-repeat; background-size: 177px 49px; background-image:url(../Html/gsAppHTMLTemplate_image/gsAppHTMLTemplate_Video_Background@2x.png);}" +
                    ".PageColorMode_Day.GSTemplateContent_VideoBoxer {background-color: #eeeeee;}"+
	                ".PageColorMode_Night.GSTemplateContent_VideoBoxer {background-color: #1B1A1F;}"+
                    ".GSTemplateContent_VideoBoxer.PlayButtonBackground{"+
                    "float:none;border-radius:40px;width: 80px;height: 80px;background-position:center center;background-repeat:no-repeat;-webkit-filter:blur(4px);}"+
                    ".GSTemplateContent_VideoBoxer.PlayButton{"+
                    "float:none;margin-top:-80px;border-radius:40px;width: 80px;height: 80px;background-repeat:no-repeat;"+
                    "background-position:center center;"+
                    "background-size:80px 80px;"+
                    "background-image:url(../Html/gsAppHTMLTemplate_image/gsAppHTMLTemplate_Video_PlayButton@2x.png);"+
                    "-webkit-filter:hue-rotate(0deg);}"+
                    "</style>";

                string relatedCss = @"#gsTemplateContent_RelatedTopic {display:none;-webkit-user-select:none;}
                    # gsTemplateContent_RelatedTopicContent {display:block; width:100%; height:auto; padding:0px;}
                    # gsTemplateContent_RelatedTopicContent table {margin-bottom:10px; border:1.0px solid #d3d3d3; width:100%; height:74px;}
                        .PageColorMode_Day #gsTemplateContent_RelatedTopicContent table {border:1.0px solid #d3d3d3;}
	                    .PageColorMode_Night #gsTemplateContent_RelatedTopicContent table {border:1.0px solid #222b38;}
                    #gsTemplateContent_RelatedTopicContent .Content img {float:left; margin:0px 6px 0px 6px; width:94px; height:54px;}
                    #gsTemplateContent_RelatedTopicContent .Content .Title {float:none; width:auto; height:34px; line-height:54px; font-size:16px;}
	                    .PageColorMode_Day #gsTemplateContent_RelatedTopicContent .Content .Title {color:#333;}
	                    .PageColorMode_Night #gsTemplateContent_RelatedTopicContent .Content .Title {color:#576476;}
                    #gsTemplateContent_RelatedTopicContent .Content .Subtitle {float:none; width:auto; height:20px; line-height:20px; font-size:14px;}
	                    .PageColorMode_Day #gsTemplateContent_RelatedTopicContent .Content .Subtitle {color:#a5a5a5;}
	                    .PageColorMode_Night #gsTemplateContent_RelatedTopicContent .Content .Subtitle {color:#464950;}
                    #gsTemplateContent_RelatedTopicContent .ButtonArea {width:74px;}
                    #gsTemplateContent_RelatedTopicContent .Button {border-radius:2px; width:54px; height:30px; line-height:30px; text-align:center; font-size:12px;}
	                    .PageColorMode_Day #gsTemplateContent_RelatedTopicContent .Button {background:#ee5449; color:#f7f7f7;}
	                    .PageColorMode_Night #gsTemplateContent_RelatedTopicContent .Button {background:#812c25; color:#c6a09d;}
                    #gsTemplateContent_RelatedTopicContent .Readed {}
	                    .PageColorMode_Day #gsTemplateContent_RelatedTopicContent .Readed {background:#6699ff;}
	                    .PageColorMode_Night #gsTemplateContent_RelatedTopicContent .Readed {background:#336699;}
                    ";
                string head = "<meta name=\"viewport\" content=\"width= device-width, user-scalable = no\" />"
                            + "<meta name=\"format-detection\" content=\"telephone=no,email=no\">" //忽略电话号码和邮箱
                            + "<meta name=\"msapplication-tap-highlight\" content=\"no\">"; //wp点击无高光;

                string videoJs = "<script src=\"../Html/gsAppHTMLTemplate_js/gsAppHTMLTemplate.js\"></script>" +
                  "<script src=\"../Html/gsAppHTMLTemplate_js/gsAppHTMLTemplate_Video.js\"></script>" +
                  "<script src=\"../Html/gsAppHTMLTemplate_js/gsVideo.js\"></script>"+
                  "<link href=\"../Html/gsAppHTMLTemplate_css/gsAppHTMLTemplate.css\" rel=\"stylesheet\" type=\"text/css\"/>";

                string title = news.result.title;
                string subTitle = news.result.subTitle;
                
                List<RelatedReadingsResult> relateReadings = await apiService.GetRelatedReadings(essayResult.contentId, essayResult.contentType);


                HtmlString = "<!DOCTYPE html><html><head>"+head+videoJs+css+videoCss+"</head>" +
                    "<body>" +
                        "<div id=\"body\" class=\"fontsizetwo\">"+
                                  "<h1 class=\"heading\" id=\"gsTemplateContent_Title\">" + title + "</h1>" +
                                  "<span class=\"info\" id=\"gsTemplateContent_Subtitle\">" + subTitle + "</span>" +
                                  "<div class=\"bar\"></div>" +
                                  "<div class=\"content\" id=\"gsTemplateContent_MainBody\">" + body + "</div>" +
                                  "<div class=\"list\" id=\"gsTemplateContent_RelatedReading\">" +
                                        "<div class=\"tit yellow\">相关阅读</div>" +//相关阅读
                                        "<div class=\"txtlist\" id=\"gsTemplateContent_RelatedReadingContent\">" +"</div>"+
                                  "</div>" +
                        "</div>" +
                   "</body>" +
                   "</html>";

            }
            //IsActive = false;

        }

    }
}
