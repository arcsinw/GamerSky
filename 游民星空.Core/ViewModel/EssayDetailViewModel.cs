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
            apiService = new ApiService();
            this.essayResult = essay;

            GenerateHtmlString();
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

        /// <summary>
        /// 生成网页
        /// </summary>
        public async void GenerateHtmlString()
        {
            News news = await apiService.ReadEssay(essayResult.contentId);
            if (news != null)
            {
                string body = news.result.mainBody;
                string css = "<style>"
                       + "html{-ms-content-zooming:none;font-family:微软雅黑;}"
                       + ".author{font-weight:bold;} .bio{color:gray;}"
                       + "body{padding:8px;word-break:break-all;} p{margin:30px auto;} a{color:skyblue;}"
                       + "body{line-height:120%; font:normal 100% Helvetica, Arial, sans-serif;}"
                       + "img{height:auto;width:auto;width:100%}"
                       + "h1{ text-align:left; font-size:1em;}" //标题栏
                       + ".heading {margin: 0; padding: 0; top: 22px; line - height:28px; color:#333;}"
	                   + ".PageColorMode_Day.heading {color:#333;}"
    	               + ".PageColorMode_Night.heading {color:#966122;}"
                       + ".bar { margin: 10px 10px 0; width: auto; height: 1px;}"
                       + ".PageColorMode_Day.bar {background-color:#eaeaea;}"
	                   + ".PageColorMode_Night.bar {background-color:#2a2e38;}"
                       + "</style>";   //基础css

                string head = "<meta name=\"viewport\" content=\"width-width, initial-scale=1\"/>"
                    + "<meta name=\"format-detection\" content=\"telephone=no,email=no\">" //忽略电话号码和邮箱
                    + "<meta name=\"msapplication-tap-highlight\" content=\"no\">"; //wp点击无高光;

                string js = "<script>" +
                    @"function gsVideo(videoType, videoContent, videoParam, videoWidth, videoHeight)
                    {
                    switch (videoType)
                    {
                        case ""优酷"":
                        case ""土豆"":
                            {
                                videoContent = videoContent;
                            }
                            break;
                        case ""乐视"":
                            {
                                videoContent
                                    = gsCreateLeTvVideoContent(videoContent, videoParam);
                            }
                            break;
                    }

                    var videoHTML = videoContent;

                    if ((gsIsCurrentOSIOS() && _kGSAppVersionNumberCode >= 201)
                    ||
                        gsIsCurrentOSAndroid())
                    {
                        var videoIndex = gsSaveAnVideoOriginContent(videoContent);

                        if (videoIndex > -1)
                        {
                            var videoDefaultWidth
                                = document.body.clientWidth - 20.0;
                            var videoDefaultHeight
                                = videoDefaultWidth
                                * 1080.0 / 1920.0;"
                                
                                +"videoHTML = <table class=\"GSTemplateContent_VideoBoxer\" id=\"videoBoxer_\" + videoIndex + \" style=\"\" border=\"0\" width=\"\" + videoDefaultWidth + \"\" height=\"\" + videoDefaultHeight + \"\" cellspacing=\"0\" cellpadding=\"0\">"
                                + "<tr>"
                                + "<td valign=\"middle\" align=\"center\">"
                                + "<div class=\"PlayButtonBackground\"></div>"
                                + "<div class=\"PlayButton\" onclick=\"gsPlayVideoWithVideoIndex(\" + videoIndex + \")\"></div>"
                                + "</td>"
                                + "</tr>"
                                + "</table>"
                + "}"
            + "}"
            + "document.write(videoHTML)"
            + "}"
            + "</script>";
                string title = news.result.title;
                string subTitle = news.result.subTitle;



                List<RelatedReadingsResult> relateReadings = await apiService.GetRelatedReadings(essayResult.contentId, essayResult.contentType);


                HtmlString = "<!DOCTYPE html><html><head>"+head+"</head>"+css+"</head>" +
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
                   "</body>" +js+
                   "</html>";

            }

        }

    }
}
