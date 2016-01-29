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
                string bodycss = @"body
                                        {
                                            overflow-x:hidden;
                                            margin:0;
                                            padding:8px 0px 0px 0px;
                                            background-color:#f7f7f7;
                                            color:#333;	
                                            text-align:justify;
	                                        word-break:break-all;
                                            -webkit-tap-highlight-color:transparent;
	                                        /*禁止选中内容, 禁止弹出内容菜单*/
                                            -webkit-touch-callout:none;
	                                        /*
	                                        -webkit-user-select:none;
	                                        */
                                        }";
                string basecss = @"/*------------------------------------------------ -x68
	                                                基础元素
                                                ------------------------------------------------ -x68*/
                                                img {border:0;}
                                                a {color:#3871c8;text-decoration:none;}
                                                a:focus{outline:none;}
	                                                .PageColorMode_Day a{}
	                                                .PageColorMode_Night a{color:#3e6bb6;}
                                                div,ul,li {overflow:hidden;}
                                                ul {margin:0;padding:0;list-style-type:none;}
                                                li {list-style-type:none;vertical-align:middle;}
                                                h1{font-size:24px;}
                                                .heading {margin:0 10px; padding:4px; top:22px; line-height:28px; color:#333;}
	                                                .PageColorMode_Day .heading {color:#333;}
	                                                .PageColorMode_Night .heading {color:#966122;}
                                                .bar {margin:10px 10px 0; width:auto; height:1px;}
	                                                .PageColorMode_Day .bar {background-color:#eaeaea;}
	                                                .PageColorMode_Night .bar {background-color:#2a2e38;}
                                                .adnone {display:none;}
                                                .info{margin:0 10px; padding:0px 4px; line-height:22px; color:gray; font-size:14px; color:#848484;}
	                                                .PageColorMode_Day .info {color:#848484;}
	                                                .PageColorMode_Night .info {color:#464950;}";
                string css = @".content {margin:0 10px; width:auto; height:auto; line-height:150%;}
                                    /*针对移动端的优化*/
                                    .Mid2L_down {width:100%; height:auto; padding:0px;}
                                    .Mid2L_down .img {float:left; margin:0px; width:auto; height:auto; padding:0px;}
                                    .Mid2L_down .con {float:left; margin-left:10px; width:auto; height:auto; padding:0px;}
                                    .Mid2L_down .con .tit {width:100%; height:30px; padding:0px; line-height:30px;}
                                    .Mid2L_down .con .txt {width:100%;}
                                    .Mid2L_down .dow {display:none;}
                                    ";
                string js = "";
                string title = news.result.title;
                string subTitle = news.result.subTitle;



                List<RelatedReadingsResult> relateReadings = await apiService.GetRelatedReadings(essayResult.contentId, essayResult.contentType);


                HtmlString = "<!DOCTYPE html><html><head><style>"+bodycss+css+basecss+"</style></head>" +
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

        }

    }
}
