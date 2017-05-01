String.prototype.replaceAll = function(key, value)
{
    var regExp = new RegExp(key, "gm");//g,全局;m,多次;i,大小写不敏感
    return this.replace(regExp, value);
}
String.prototype.indexOfWithIgnorecase = function(key)
{
    var regExp = eval("/"+ key +"/i");
    var result = this.match(regExp);
    if (result!=null)
    {
        return result.index;
    }
    return -1;
}
var kOS = "android";
var kAppVersion = window.handler.getAppVersion();
var kDefaultPageFontSize = window.handler.getFontSize();
var kDefaultPageColorMode = window.handler.getColorMode();
var kDefaultNullImageMode = window.handler.getPicMode();
var kDeviceAPSToken = window.handler.getDeviceToken();
var kNetworkType = window.handler.getNetworkType();
var _kGSHadDocumentReady = false;
var _kGSOSBuffer = 0;// 0 未知, 1 iOS, 2 Androi
var _kGSAppVersionNumberCode = parseInt(kAppVersion.replaceAll("\\.", ""));
var _kGSIsNightColorModeBuffer = typeof(kDefaultPageColorMode)!="undefined" && kDefaultPageColorMode=="night";
var _kGSIsNullImageModeBuffer = typeof(kDefaultNullImageMode)!="undefined" && kDefaultNullImageMode=="yes";
if (typeof(kDefaultPageColorMode)!="undefined")
{
    gsSetPageParam({name:"pageColorMode", value:kDefaultPageColorMode});
}
if (typeof(kDefaultPageFontSize)!="undefined")
{
    gsSetPageParam({name:"pageFontSize", value:kDefaultPageFontSize});
}

function gsOnPageLoad()
{
    try
    {
        gsOnDocumentReadystatechange();
    }
    catch (exception)
    {
    }
}

function gsOnDocumentReadystatechange()
{
    if (_kGSHadDocumentReady)
    {
        return;
    }
    _kGSHadDocumentReady = true;

    var mainBody = document.getElementById("gsTemplateContent_MainBody");

    // 首先卸载不需要的资源
    var unloadGSElementsIDsJSON = "";


    if (unloadGSElementsIDsJSON != null
        && unloadGSElementsIDsJSON.length > 0)
    {
        unloadGSElementsIDsJSON = unloadGSElementsIDsJSON.substring(0, unloadGSElementsIDsJSON.length - 1);
        unloadGSElementsIDsJSON = "[" + unloadGSElementsIDsJSON + "]";
        var msg = "unloadGSElements:{\"unloadElementIDs\":" + unloadGSElementsIDsJSON + "}";
        gsSendMessageToSuperWebView(msg);
    }

    var aTags = mainBody.getElementsByTagName("a");
    try
    {
        if (aTags != null
            && aTags.length > 0)
        {
            for (var aTagIndex = aTags.length - 1;
                 aTagIndex > -1;
                 aTagIndex--)
            {
                var aTag = aTags[aTagIndex];
                var aTagHref = aTag.getAttribute("href");

                var urlCheckResult = gsCheckTheURL(aTagHref, aTag);

                if (urlCheckResult.imgTag != null
                    && urlCheckResult.imgHDURL != null)
                {
                    urlCheckResult.imgTag.setAttribute("hdImageURL", urlCheckResult.imgHDURL);
                }

                if (urlCheckResult.valid == false
                    && aTag.parentNode != null)
                {
                    var aTagParentNode = aTag.parentNode;
                    var aTagChildNodes = aTag.childNodes;

                    if (aTag.textContent != "查看作者更多文章"
                        && aTagChildNodes != null
                        && aTagChildNodes.length > 0)
                    {
                        for (var aTagChildNodeIndex = 0;
                             aTagChildNodeIndex < aTagChildNodes.length;
                             aTagChildNodeIndex++)
                        {
                            aTagParentNode.insertBefore(aTagChildNodes[aTagChildNodeIndex], aTag);
                        }
                    }

                    aTagParentNode.removeChild(aTag);
                }else{
                    aTag.removeAttribute("target");
                }
            }
        }
    }
    catch (err)
    {
    }

    var images = mainBody.getElementsByTagName("img");
    var screenHeight = window.screen.height;
    for(var i=0;i<images.length;i++)
    {
          images[i].style.width = "100%";
          images[i].style.height = "auto";
          var src = images[i].src;
          images[i].setAttribute("originsrc",src);
          images[i].setAttribute("gsElementID",i);
          images[i].onclick=gsImageOnClick;
          if(images[i].offsetTop > 2 * screenHeight){
            images[i].setAttribute("src","");
          }else{
            if(_kGSIsNullImageModeBuffer){
                images[i].setAttribute("src","");
            }
          }
    }

    var iframeStyle = document.createElement("style");
    iframeStyle.setAttribute("type","text/css");
    var css = document.createTextNode(" .content iframe{margin:0px 0px 0px 0px;width:100%;}");
    iframeStyle.appendChild(css);
    var head = document.getElementsByTagName("head")[0];
    head.appendChild(iframeStyle);
    window.onscroll = gsOnScroll;

    var version = document.getElementsByTagName("GSAppHTMLTemplate")[0].getAttribute("version");
    window.handler.checkTemplates(version);
//    handler.print(document.body.innerHTML);
//    setTimeout(gsOnScroll, 100);
}

function gsOnScroll()
{
    if(_kGSIsNullImageModeBuffer){
        return;
    }
    var images = document.getElementById("gsTemplateContent_MainBody").getElementsByTagName("img");
    var currentTop = window.scrollY;
    var screenHeight = window.screen.height;
    for(var i = 0; i < images.length; i++){
        var offsetTop = images[i].offsetTop;
        var currentSrc = images[i].src;
        if(offsetTop < (5 * screenHeight + currentTop) && currentSrc != "")
        {
            images[i].setAttribute("src",images[i].getAttribute("originsrc"));
        }

    }
}
function gsSetPageParam(param)
{
    if (typeof(param.name)=="undefined")
    {
        param = eval("(" + param + ")");
    }

    switch (param.name)
    {
        case "theme":
        case "pageColorMode":
        {
            var htmlTag = document.getElementsByTagName("html")[0];
            switch (param.value)
            {
                case "day":
                {
                    gsSetElementClass(htmlTag, "PageColorMode_Night", "PageColorMode_Day");
                    if (typeof(kDefaultPageColorMode)!="undefined")
                    {
                        kDefaultPageColorMode = "day";
                        _kGSIsNightColorModeBuffer = false;
                    }
                }break;
                case "night":
                {
                    gsSetElementClass(htmlTag, "PageColorMode_Day", "PageColorMode_Night");
                    if (typeof(kDefaultPageColorMode)!="undefined")
                    {
                        kDefaultPageColorMode = "night";
                        _kGSIsNightColorModeBuffer = true;
                    }
                }break;
            }


        }break;
        case "pageFontSize":
        {
            var htmlTag = document.getElementsByTagName("html")[0];
            if (typeof(kDeviceIsBigScreen)!="undefined"
                && kDeviceIsBigScreen=="yes")
            {
                switch (param.value)
                {
                    case "min":
                    {
                        gsSetElementClass(htmlTag, "PageFontSize_MiddleForBigScreen", "");
                        gsSetElementClass(htmlTag, "PageFontSize_MaxForBigScreen", "PageFontSize_MinForBigScreen");
                    }break;
                    case "middle":
                    {
                        gsSetElementClass(htmlTag, "PageFontSize_MinForBigScreen", "");
                        gsSetElementClass(htmlTag, "PageFontSize_MaxForBigScreen", "PageFontSize_MiddleForBigScreen");
                    }break;
                    case "max":
                    {
                        gsSetElementClass(htmlTag, "PageFontSize_MinForBigScreen", "");
                        gsSetElementClass(htmlTag, "PageFontSize_MiddleForBigScreen", "PageFontSize_MaxForBigScreen");
                    }break;
                }
            }
            else
            {
                switch (param.value)
                {
                    case "min":
                    {
                        gsSetElementClass(htmlTag, "PageFontSize_Middle", "");
                        gsSetElementClass(htmlTag, "PageFontSize_Max", "PageFontSize_Min");
                    }break;
                    case "middle":
                    {
                        gsSetElementClass(htmlTag, "PageFontSize_Min", "");
                        gsSetElementClass(htmlTag, "PageFontSize_Max", "PageFontSize_Middle");
                    }break;
                    case "max":
                    {
                        gsSetElementClass(htmlTag, "PageFontSize_Min", "");
                        gsSetElementClass(htmlTag, "PageFontSize_Middle", "PageFontSize_Max");
                    }break;
                }
            }
        }break;
        case "nullImageMode":
        {
            var images = document.getElementsByTagName("img");
            if (param.value=="yes")
            {
                if (typeof(kDefaultNullImageMode)!="undefined")
                {
                    kDefaultNullImageMode = "yes";
                    _kGSIsNullImageModeBuffer = true;
                }

                for (var imgIndex=0;
                     imgIndex<images.length;
                     imgIndex++)
                {
                    var img = images[imgIndex];
//                    img.style.visibility = "hidden";
                    img.setAttribute("src","");

                    // if (gsIsElementClass(img.parentNode, "ImgBoxer"))
                    // {
                    //     var imgBoxer = img.parentNode;
                    //     gsSetTheImgBoxerBgType(imgBoxer, "nullImageMode", null);
                    // }
                }
            }
            else
            {
                if (typeof(kDefaultNullImageMode)!="undefined")
                {
                    kDefaultNullImageMode = "no";
                    _kGSIsNullImageModeBuffer = false;
                }

                for (var imgIndex=0;
                     imgIndex<images.length;
                     imgIndex++)
                {
                    var img = images[imgIndex];
//                        img.style.visibility = "visible";
                    img.setAttribute("src",img.getAttribute("originsrc"));
                    // if (gsIsElementClass(img.parentNode, "ImgBoxer"))
                    // {
                    //     var imgBoxer = img.parentNode;
                    //     gsSetTheImgBoxerBgType(imgBoxer, "none", null);
                    // }
                    // if (img.nextSibling!=null
                    //     && gsIsElementClass(img.nextSibling, "ImgBoxerStatusBar"))
                    // {
                    //     img.nextSibling.style.visibility = "visible";
                    // }
                }

                // 请求当前图片
                // gsOnWindowScroll();
            }
        }break;
        case "gsTemplateContent_RelatedTopic":
        {
            var relatedTopicsHTML = "";
            var relatedTopics = param.value;
            if (relatedTopics!=null
                && relatedTopics.length!=null
                && relatedTopics.length>0)
            {
                var relatedTopicsCount = relatedTopics.length;
                for (var relatedTopicIndex=0;
                     relatedTopicIndex<relatedTopicsCount;
                     relatedTopicIndex++)
                {
                    var relatedTopic = relatedTopics[relatedTopicIndex];
                    var relatedTopicId = relatedTopic.topicId;
                    if (relatedTopicId==null
                        || relatedTopicId.length==null)
                    {
                        relatedTopicId = "";
                    }

                    var openTopicURL
                        = "gsOpenTheTopicWithId('" + relatedTopicId +"')";

                    var subscribeTopicURL = "";
                    var buttonHTML = "";

                    if (relatedTopic.subscribed)
                    {
                        subscribeTopicURL = "gsCancelSubscribeTheTopicWithId('" + relatedTopicId +"')";
                        buttonHTML = "<div class=\"Button Readed\">已订阅</div>";
                    }
                    else
                    {
                        subscribeTopicURL = "gsSubscribeTheTopicWithId('" + relatedTopicId +"')";
                        buttonHTML = "<div class=\"Button\">订阅</div>";
                    }

                    var relatedTopicHTML
                        = "<table width=\"100%\" border=\"0\" cellspacing=\"0\" cellpadding=\"0\">"
                        + "<tr>"
                        + "<td class=\"Content\" align=\"left\" valign=\"middle\" onclick=\"" + openTopicURL + "\">"
                        + "<img src=\"" + relatedTopic.iconURL + "\">"
                        + "<span class=\"Title\">" + relatedTopic.title + "</span>"
                            /*
                             + "<br />"
                             + "<span class=\"Subtitle\">" + relatedTopic.subscribersCount + "人订阅</span>"
                             */
                        + "</td>"
                        + "<td class=\"ButtonArea\" align=\"center\" valign=\"middle\" onclick=\"" + subscribeTopicURL + "\">"
                        + buttonHTML
                        + "</td>"
                        + "</tr>"
                        + "</table>";

                    relatedTopicsHTML += relatedTopicHTML;
                }
            }

            var relatedTopicElement
                = document.getElementById("gsTemplateContent_RelatedTopic");
            if (relatedTopicElement!=null)
            {
                if (relatedTopicsHTML.length>0)
                {
                    var relatedTopicElementContent
                        = document.getElementById("gsTemplateContent_RelatedTopicContent");
                    if (relatedTopicElementContent!=null)
                    {
                        relatedTopicElementContent.innerHTML = relatedTopicsHTML;
                    }

                    // 显示
                    relatedTopicElement.style.display = "block";

                    // 关联操作
                    var relatedTopicContents = relatedTopicElement.getElementsByClassName("Content");
                    var relatedTopicButtons = relatedTopicElement.getElementsByClassName("Button");
                    try
                    {
                        for (var relatedTopicContentIndex in relatedTopicContents)
                        {
                            var relatedTopicContent = relatedTopicContents[relatedTopicContentIndex];
                            gsAddTouchEventListenerToObject(relatedTopicContent);

                            var relatedTopicButton = relatedTopicButtons[relatedTopicContentIndex];
                            gsAddTouchEventListenerToObject(relatedTopicButton);
                        }
                    }
                    catch(err)
                    {}
                }
                else
                {
                    relatedTopicElement.style.display = "none";
                }
            }
        }break;
        case "gsTemplateContent_RelatedReading":
        {
            var relatedReadingsHTML = "";
            var relatedReadings = param.value;
            if (relatedReadings!=null
                && relatedReadings.length!=null
                && relatedReadings.length>0)
            {
                var relatedTopicId = null;
                var relatedTopicName = null;
                for (var relatedReadingIndex in relatedReadings)
                {
                    var relatedReading = relatedReadings[relatedReadingIndex];
                    var relatedReadingPageID = relatedReading.pageID;
                    if (relatedReadingPageID==null
                        || relatedReadingPageID.length==null)
                    {
                        relatedReadingPageID = "";
                    }
                    var relatedReadingPageURL = relatedReading.pageURL;
                    if (relatedReadingPageURL==null
                        || relatedReadingPageURL.length==null)
                    {
                        relatedReadingPageURL = "";
                    }

                    if (relatedTopicId==null)
                    {
                        relatedTopicId = relatedReading.topicId;
                        relatedTopicName = relatedReading.topicName;
                    }

                    var relatedReadingURL
                        = "openPage:{\"pageID\":\"" + relatedReadingPageID +"\","
                        + "\"pageURL\":\"" + relatedReadingPageURL + "\","
                        + "\"openMethod\":\"default\"}";
                    relatedReadingURL = encodeURI(relatedReadingURL);

                    var imageHTML = "";
                    if (relatedReading.thumbnailURL!=null
                    && typeof(relatedReading.thumbnailURL)!="undefined"
                    && relatedReading.thumbnailURL.length>0)
                    {
                        imageHTML = "<div class=\"Thumbnail\"><img src=\"" + relatedReading.thumbnailURL + "\"></div>";
                    }

                    var titleHTML = "";
                    if (relatedReading.title!=null
                    && typeof(relatedReading.title)!="undefined"
                    && relatedReading.title.length>0)
                    {
                        titleHTML = "<div>" + relatedReading.title + "</div>";
                    }

                    var relatedReadingHTML
                        = "<a href=\"" + relatedReadingURL + "\"><div class=\"Row\"><div>" + imageHTML + titleHTML + "</div></div></a>"

                    relatedReadingsHTML += relatedReadingHTML;

                    if (relatedReadingIndex>=2)
                    {
                        break;
                    }
                }
                if (relatedReadingsHTML!=null
                    && relatedReadingsHTML.length>0
                    && relatedTopicId!=null
                    && relatedTopicName!=null)
                {
                    relatedReadingsHTML
                        += "<div class=\"Row Button\" onclick=\"gsOpenTheTopicWithId(\""
                        + relatedTopicId
                        + "\")\">点击查看更多["
                        + relatedTopicName
                        + "]新闻</div>";
                }
            }

            var relatedReadingElement
                = document.getElementById("gsTemplateContent_RelatedReading");
            if (relatedReadingElement!=null)
            {
                if (relatedReadingsHTML.length>0)
                {
                    var relatedReadingElementContent
                        = document.getElementById("gsTemplateContent_RelatedReadingContent");
                    if (relatedReadingElementContent!=null)
                    {
                        relatedReadingElementContent.innerHTML = relatedReadingsHTML;
                    }

                    // 显示
                    relatedReadingElement.style.display = "block";

                    // 关联操作
                    var rows = relatedReadingElement.getElementsByClassName("Row");
                    try
                    {
                        for (var rowIndex in rows)
                        {
                            var row = rows[rowIndex];
                            gsAddTouchEventListenerToObject(row);
                        }
                    }
                    catch(err)
                    {}

                    var buttons = relatedReadingElement.getElementsByClassName("Button");
                    try
                    {
                        for (var buttonIndex in buttons)
                        {
                            var button = buttons[buttonIndex];
                            gsAddTouchEventListenerToObject(button);
                        }
                    }
                    catch(err)
                    {}
                }
                else
                {
                    relatedReadingElement.style.display = "none";
                }
            }
        }break;
        case "gsTemplateContent_HotComments":
        case "gsTemplateContent_AllComments":
        case "gsTemplateContent_Comments":
        {
            var allCommentsType = null;
            if (param.name=="gsTemplateContent_HotComments")
            {
                allCommentsType = "hot";
            }
            else if (param.name=="gsTemplateContent_AllComments")
            {
                allCommentsType = "lastest";
            }
            else
            {
                allCommentsType = null;
            }

            var comments = param.value;
            if (comments!=null
                && comments.length!=null
                && comments.length>0)
            {
                var lastCommetnsType = null;
                var commentsHTML = "";

                var commentsCount = comments.length;
                for (var commentIndex=0;
                     commentIndex<commentsCount;
                     commentIndex++)
                {
                    var comment = comments[commentIndex];
                    var commentType = comment.type;
                    if (commentType==null
                        && allCommentsType!=null)
                    {
                        commentType = allCommentsType;
                    }

                    if (lastCommetnsType!=commentType)
                    {
                        lastCommetnsType = commentType;

                        if (commentsHTML.length>0)
                        {
                            commentsHTML += "</ul>";
                        }
                        switch (commentType)
                        {
                            case "hot":
                            {
                                commentsHTML
                                    += "<div class=\"tit blue\">热门评论</div>"
                                    + "<ul class=\"Comment\" id=\"hotComments\">";
                            }break;
                            case "lastest":
                            default:
                            {
                                commentsHTML
                                    += "<div class=\"tit blue\">最新评论</div>"
                                    + "<ul class=\"Comment\" id=\"lastestComments\">";
                            }break;
                        }
                    }

                    var commentHTML = gsCreateTheCommentHTML(comment);
                    commentsHTML += commentHTML;
                }
                if (commentsHTML.length>0)
                {
                    commentsHTML += "</ul><div class=\"Button More\">点击查看更多评论</div>";
                }
                var commentsElement
                    = document.getElementById("gsTemplateContent_Comments");
                if (commentsElement!=null)
                {
                    commentsElement.innerHTML = commentsHTML;
                    commentsElement.style.display = "block";

                    var buttons = commentsElement.getElementsByClassName("Button");
                    try
                    {
                        for (var buttonIndex in buttons)
                        {
                            var button = buttons[buttonIndex];
                            gsAddTouchEventListenerToObject(button);
                        }
                    }
                    catch(err)
                    {}
                }

                // 关联操作
//                if(commentsElement != null){}
//
//                }
            }
        }break;
        case "gsTemplateContent_NewComment":
        {
            var comment = param.value;
            var lastestComments = document.getElementById("lastestComments");
            if (lastestComments==null)
            {
                gsSetPageParam({"name":"gsTemplateContent_AllComments", "value":[comment]});
            }
            else
            {
                var commentHTML = gsCreateTheCommentHTML(comment);
                if (commentHTML!=null
                    && commentHTML.length>0)
                {
                    lastestComments.innerHTML
                        = commentHTML
                        + lastestComments.innerHTML;

                    // 关联操作
                    var buttons = lastestComments.getElementsByClassName("Button");
                    try
                    {
                        for (var buttonIndex in buttons)
                        {
                            var button = buttons[buttonIndex];
                            gsAddTouchEventListenerToObject(button);
                        }
                    }
                    catch(err)
                    {}
                }
            }
        }break;
        case "gsTemplateContent_AD1":
        {
            try
            {
                var ad = param.value;

                if (typeof(ad.contentType)=="undefined")
                {
                    // 1, 使用 contentURL
                    // 2, 使用 contentID
                    ad.contentType = 1;
                }
                if (typeof(ad.url)!="undefined")
                {
                    ad.contentURL = ad.url;
                }

                var openMethod = "withContentID";
                if (ad.contentType=="1")
                {
                    openMethod = "withContentURL";
                }
                else if (ad.contentType=="2")
                {
                    openMethod = "withContentID";
                }
                else
                {
                    openMethod = "default";
                }

                var adId = "adId";
                if (gsIsCurrentOSAndroid())
                {
                    adId = "adID";
                }

                var adHTML
                    = "<a href=\"openPage:{&quot;pageID&quot;:&quot;" + ad.contentID
                    +"&quot;,&quot;pageURL&quot;:&quot;" + ad.contentURL
                    + "&quot;, &quot;openMethod&quot;:&quot;" + openMethod
                    + "&quot;,&quot;" + adId + "&quot;:&quot;" + ad.id + "&quot;}\">"
                    + "<table width=\"100%\" border=\"0\" cellspacing=\"0\" cellpadding=\"0\">"
                    + "<tr>"
                    + "<td rowspan=\"2\" class=\"Icon\">"
                    + "<img src=\"" + ad.iconURL + "\" width=\"58\" height=\"58\">"
                    + "</td>"
                    + "<td class=\"Title\">" + ad.title + "</td>"
                    + "<td class=\"Badge\">"
                    + "<div>推广</div>"
                    + "</td>"
                    + "</tr>"
                    + "<tr>"
                    + "<td colspan=\"2\" class=\"Subtitle\">" + ad.subtitle + "</td>"
                    + "</tr>"
                    + "</tbody></table>"
                    + "</a>";
                window.handler.print(adHTML);
                var ad1 = document.getElementById("gsTemplateContent_AD1");
                if (ad1!=null)
                {
                    ad1.innerHTML = adHTML;
                    ad1.style.display = "block";
                }
            }
            catch (err)
            {
            }
        }break;
        case "gsTemplateContent_AD2":
        {
            var ad = param.value;

            if (typeof(ad.contentType)=="undefined")
            {
                // 1, 使用 contentURL
                // 2, 使用 contentID
                ad.contentType = 1;
            }
            if (typeof(ad.url)!="undefined")
            {
                ad.contentURL = ad.url;
            }

            var openMethod = "withContentID";
            if (ad.contentType=="1")
            {
                openMethod = "withContentURL";
            }
            else if (ad.contentType=="2")
            {
                openMethod = "withContentID";
            }
            else
            {
                openMethod = "default";
            }

            var adWidth = "100%";
            if (gsIsCurrentOSAndroid())
            {
                adWidth= document.body.clientWidth - 22;
            }

            var adId = "adId";
            if (gsIsCurrentOSAndroid())
            {
                adId = "adID";
            }

            var adHTML
                = "<a href=\"openPage:{&quot;pageID&quot;:&quot;" + ad.contentID
                + "&quot;,&quot;pageURL&quot;:&quot;" + ad.contentURL
                + "&quot;, &quot;openMethod&quot;:&quot;" + openMethod
                + "&quot;,&quot;" + adId + "&quot;:&quot;" + ad.id + "&quot;}\">"
                + "<table width=\"100%\" border=\"0\" cellspacing=\"0\" cellpadding=\"0\">"
                + "<tr>"
                + "<td class=\"Icon\" colspan=\"2\">"
                + "<img src=\"" + ad.iconURL + "\">"
                + "</td>"
                + "</tr>"
                + "<tr>"
                + "<td class=\"Badge\" valign=\"top\">"
                + "<div>推广</div>"
                + "</td>"
                + "<td class=\"Title\">"
                + ad.title
                + "</td>"
                + "</tr>"
                + "</table>"
                + "</a>";
            window.handler.print(adHTML);
            var ad2 = document.getElementById("gsTemplateContent_AD2");
            if (ad2!=null)
            {
                ad2.innerHTML = adHTML;
                ad2.style.display = "block";
            }
        }break;
    }
}

function scrollToCurImage(curImageUrl){
    var images = document.getElementById("gsTemplateContent_MainBody").getElementsByTagName("img");
    for(var i=0; i<images.length; i++){
        var src = images[i].getAttribute("src");
        if(src == curImageUrl){
            var offset = images[i].offsetTop - 50;
            window.scrollTo(0,offset);
            break;
        }
    }
}

function scrollToTop(){
    window.scrollTo(0,0);
}

////////////////////////////////////////////////
/* App和主站公用的JS代码 */
////////////////////////////////////////////////

function gsVideoInApp(videoType, videoContent, videoParam, videoWidth, videoHeight)
{
    switch (videoType)
    {
        case "优酷":
        case "土豆":
        {
            videoContent = videoContent;
            var videoDefaultWidth = (document.body.clientWidth - 100.0) / 2;
            var videoDefaultHeight = videoDefaultWidth * 1080.0/1920.0;

            var wstart = videoContent.indexOf("width=\"") + 7;
            var temp = videoContent.substring(wstart);
            var wend = temp.indexOf("\"");
            var width = temp.substring(0,wend);
            videoContent = videoContent.replace(width,videoDefaultWidth);

            var hstart = videoContent.indexOf("height=\"") + 8;
            temp = videoContent.substring(hstart);
            var hend = temp.indexOf("\"");
            var height = temp.substring(0,hend);
            videoContent = videoContent.replace(height,videoDefaultHeight);
            window.handler.print(videoContent);
        }break;
        case "乐视":
        {
            videoContent
                = gsCreateLeTvVideoContent(videoContent, videoParam);
        }break;
    }

    var videoHTML = videoContent;

    document.write(videoHTML);
}


function gsCreateLeTvVideoContent(videoId, videoParam)
{
    var bodyWidth = document.body.clientWidth-20.0;
    var height = bodyWidth*9.0/16.0;
    var width = bodyWidth;
    var leTv_videosupport_js = "http:\/\/j.gamersky.com\/wap\/js\/leTv_videosupport_2.0_v1.m.js";

    var html
        = "<div id=\"leshitvauto\" style=\"margin-left:auto;margin-right:auto; width:" + width + "; height:" + height + ";\">"

        + "<script type=\"text\/javascript\">"

        + "var domainname = \"http:\/\/yuntv.letv.com\/\";"

        + "<\/script>"
        + "<script type=\"text\/javascript\" src=\"http:\/\/yuntv.letv.com\/swfobj_1.3.m.js\"><\/script>"

        + "<script type=\"text\/javascript\" src=\"" + leTv_videosupport_js  + "\"><\/script>"

        + "<script type=\"text\/javascript\" src=\"http:\/\/yuntv.letv.com\/user_defined.js?v2\"><\/script>"

        + "<script type=\"text\/javascript\" src=\"http:\/\/yuntv.letv.com\/js\/player_v2.3.js\" data=\"{uu:'" + videoId + "',vu:'" + videoParam + "',auto_play:'0',gpcflag:'1',width:'" + width + "',height:'" + height + "'}\"><\/script>"

        + "<\/div>";

    return html;
}













//////////////////////////////////////////////////
function gsCreateTheCommentReplyCommentsHTML(comment)
{
    if (comment==null
        || comment.replyComments==null
        || comment.replyComments.length<1)
    {
        return "";
    }

    var replyCommentsHTML = "";
    var replyComments = comment.replyComments;
    var replyCommentsCount = replyComments.length;
    for (var replyCommentIndex=0;
         replyCommentIndex<replyCommentsCount;
         replyCommentIndex++)
    {
        var replyComment = replyComments[replyCommentIndex];
        if (replyComment!=null)
        {
            var replyCommentContentClass
                = "ReplyCommentContent";
            var replyCommentContentOperateBarClass
                = "ReplyCommentContentOperateBar";
            if (replyCommentIndex+1==replyCommentsCount)
            {
                replyCommentContentClass = "LastReplyCommentContent";
                replyCommentContentOperateBarClass = "ReplyCommentContentOperateBar LastReplyCommentContentOperateBar";
            }

            var originCommentContent
                = replyComment.content;
            var finalCommentContent
                = originCommentContent;
            if (originCommentContent.length>60)
            {
                finalCommentContent
                    = originCommentContent.substring(0, 50)
                    + "..."
                    + "<a class=\"ShowOriginContentButton\" onclick=\"gsCommentShowCurrentCommentOriginContent(this);event.stopPropagation();return false;\">显示全部</a>";
            }

            var moreFloorsButtonHTML = "";

            var replyCommentCSS = "";
            if (replyCommentsCount>3)
            {
                if (replyCommentIndex==2)
                {
                    moreFloorsButtonHTML
                        = "<div class=\"ReplyComment ReplyCommentContent ShowMoreFloorsButton\" onclick=\"gsCommentShowHiddenFloors(this);event.stopPropagation();\">显示隐藏楼层</div>";
                }

                if (replyCommentIndex>1
                    && replyCommentIndex<replyCommentsCount-1)
                {
                    replyCommentCSS = "display:none;";
                }
            }
            replyCommentsHTML += moreFloorsButtonHTML;


            var replyCommentHTML
                = "<div class=\"ReplyComment\" style=\"" + replyCommentCSS + ";border:10px red;\">"
                + "<div class=\"ReplyCommentTitle\">" + replyComment.userNickName + "<span class=\"ReplyCommentFloorIndex\">" + (replyCommentIndex+1) + "</span></div>"
                + "<div class=\"" + replyCommentContentClass + "\" origincommentcontent=\"" + originCommentContent + "\">" + finalCommentContent + "</div>"
                + "<div class=\"" + replyCommentContentOperateBarClass + "\">"
                + "<div class=\"PraiseButton\" onclick=\"gsCommentPraiseTheComment('" + replyComment.id.toString() + "', this);event.stopPropagation();\">"
                + "<div class=\"Icon\"></div>"
                + "<div class=\"Label\">" + replyComment.praisesCount + "</div>"
                + "</div>"
                + "<div class=\"ReplyButton\" onclick=\"gsCommentReplyTheComment('" + replyComment.id.toString() + "', '" + replyComment.userNickName + "' ,this);event.stopPropagation();\">回复</div>"
                + "</div>"
                + "</div>";

            replyCommentsHTML += replyCommentHTML;
        }
    }
    if(replyCommentsHTML!=null
        && replyCommentsHTML.length>0)
    {
        replyCommentsHTML
            = "<div class=\"ReplyComments\" >"
            + replyCommentsHTML
            + "</div>";
    }

    return replyCommentsHTML;
}

function gsCreateTheCommentHTML(comment)
{
    if (comment==null)
    {
        return "";
    }

    var userHeadImageHTML = "";
    if (comment.userHeadImageURL!=null
        && comment.userHeadImageURL.length>0)
    {
        userHeadImageHTML = "<img width=\"100%\" height=\"100%\" src=\"" +comment.userHeadImageURL+ "\" />";
    }

    var userSubtitle = "";
    /*
     var userIPLocation = comment.userIPLocation;
     if (userIPLocation!=null
     && userIPLocation.length>0)
     {
     var indexOfQu = userIPLocation.indexOf("区");
     if (indexOfQu>-1
     && indexOfQu<(userIPLocation.length-1))
     {
     userIPLocation = userIPLocation.substring(indexOfQu+1);
     }

     userSubtitle = userIPLocation + " " + comment.dateTime;
     }
     else
     */
    {
        userSubtitle = comment.dateTime;
    }

    var replyCommentsHTML = gsCreateTheCommentReplyCommentsHTML(comment);

    var originCommentContent
        = comment.content;
    var finalCommentContent
        = originCommentContent;

    if (originCommentContent.length>60)
    {
        finalCommentContent
            = originCommentContent.substring(0, 50)
            + "..."
            + "<a class=\"ShowOriginContentButton\" onclick=\"gsCommentShowCurrentCommentOriginContent(this);event.stopPropagation();return false;\">显示全部</a>";
    }

    return "<li class=\"li\">"
        + "<div class=\"comment-header\">"
        + "<div class=\"header-wrap\">"
        + "<div class=\"box-pic\">" + userHeadImageHTML + "</div>"
        + "<div class=\"box-msg\">"
        + "<div class=\"msg-name\">" + comment.userNickName + "</div>"
        + "<div class=\"msg-time\">" + userSubtitle + "</div>"
        + "</div>"
        + "</div>"
        + "<div class=\"header-reply\" onclick=\"gsCommentReplyTheComment('" + comment.id.toString() + "','" + comment.userNickName + "' ,this);event.stopPropagation();\">回复</div>"
        + "<div class=\"header-praise\" onclick=\"gsCommentPraiseTheComment('" + comment.id.toString() + "', this);event.stopPropagation();\">"
        + "<div class=\"Icon\"></div>"
        + "<div class=\"Label\">" + comment.praisesCount + "</div>"
        + "</div>"
        + "</div>"
        + "<div class=\"comment-cont\">"
        + replyCommentsHTML
        + "<div originCommentContent=\""
        + originCommentContent
        + "\">" + finalCommentContent + "</div>"
        + "</div>"
        + "</li>";
}

function gsCommentShowCurrentCommentOriginContent(showButtonInContent)
{
    if (showButtonInContent==null)
    {
        return;
    }
    var contentOwner = showButtonInContent.parentNode;
    if (contentOwner==null)
    {
        return;
    }
    var originCommentContent = contentOwner.getAttribute("originCommentContent");
    if (originCommentContent==null
        || originCommentContent.length<1)
    {
        return;
    }
    contentOwner.innerHTML
        = originCommentContent;
}

function gsCommentPraiseTheComment(commentID, praiseContentOwner)
{
    if (commentID==null
        || commentID.length<1)
    {
        return;
    }

    var praiseIconContents
        = praiseContentOwner.getElementsByClassName("Icon");
    if (praiseIconContents==null
        || praiseIconContents.length<1)
    {
        return;
    }
    var praiseIconContent = praiseIconContents[0];

    var praiseNumberContent = null;
    var praiseNumberContents = praiseContentOwner.getElementsByClassName("Label");
    if (praiseNumberContents!=null
        && praiseNumberContents.length>0)
    {
        praiseNumberContent = praiseNumberContents[0];
    }

    if (praiseIconContent.className.indexOf("IconSelected")>-1)
    {
        gsSendMessageToSuperWebView("openPage:{\"openMethod\":\"withContentURL\", \"pageID\":\""
            + commentID
            + "\", \"pageURL\":\"treadTheComment.app.gamersky\"}");

        praiseIconContent.className = "Icon";
        if (praiseNumberContent!=null)
        {
            praiseNumberContent.innerHTML
                = (parseInt(praiseNumberContent.textContent)-1);
        }
    }
    else
    {
        gsSendMessageToSuperWebView("openPage:{\"openMethod\":\"withContentURL\", \"pageID\":\""
            + commentID
            + "\", \"pageURL\":\"praiseTheComment.app.gamersky\"}");

        praiseIconContent.className = "Icon IconSelected";
        if (praiseNumberContent!=null)
        {
            praiseNumberContent.innerHTML
                = (parseInt(praiseNumberContent.textContent)+1);
        }
    }
}

function gsCommentTreadTheComment(commentID, praiseContentOwner)
{
    if (commentID==null
        || commentID.length<1)
    {
        return;
    }

    gsSendMessageToSuperWebView("openPage:{\"openMethod\":\"withContentURL\", \"pageID\":\""
        + commentID
        + "\", \"pageURL\":\"praiseTheComment.app.gamersky\"}");

    if (praiseContentOwner!=null)
    {
        var praiseNumberContents
            = praiseContentOwner.getElementsByClassName("Label");
        var praiseNumberContentsCount
            = praiseNumberContents.length;
        for (var praiseNumberContentIndex=0;
             praiseNumberContentIndex<praiseNumberContentsCount;
             praiseNumberContentIndex++)
        {
            var praiseNumberContent
                = praiseNumberContents[praiseNumberContentIndex];
            praiseNumberContent.innerHTML
                = (parseInt(praiseNumberContent.textContent)+1);
        }

        var praiseIconContents
            = praiseContentOwner.getElementsByClassName("Icon");
        var praiseIconContentsCount
            = praiseIconContents.length;
        for (var praiseIconContentIndex=0;
             praiseIconContentIndex<praiseIconContentsCount;
             praiseIconContentIndex++)
        {
            var praiseIconContent
                = praiseIconContents[praiseIconContentIndex];
            praiseIconContent.className = "IconSelected";
        }

        // praiseContentOwner.onclick = null;
    }
}

function gsCommentReplyTheComment(commentID,userNickName, praiseContentOwner)
{
    if (commentID==null
        || commentID.length<1)
    {
        return;
    }

    {
        gsSendMessageToSuperWebView("openPage:{\"openMethod\":\"withContentURL\", \"pageID\":\""
            + commentID
            + "\", \"userNickName\":\""+userNickName+"\",\"pageURL\":\"replyTheComment.app.gamersky\"}");

        if (praiseContentOwner!=null)
        {
            var praiseNumberContents
                = praiseContentOwner.getElementsByClassName("Label");
            var praiseNumberContentsCount
                = praiseNumberContents.length;
            for (var praiseNumberContentIndex=0;
                 praiseNumberContentIndex<praiseNumberContentsCount;
                 praiseNumberContentIndex++)
            {
                var praiseNumberContent
                    = praiseNumberContents[praiseNumberContentIndex];
                praiseNumberContent.innerHTML
                    = (parseInt(praiseNumberContent.textContent)+1);
            }

            var praiseIconContents
                = praiseContentOwner.getElementsByClassName("Icon");
            var praiseIconContentsCount
                = praiseIconContents.length;
            for (var praiseIconContentIndex=0;
                 praiseIconContentIndex<praiseIconContentsCount;
                 praiseIconContentIndex++)
            {
                var praiseIconContent
                    = praiseIconContents[praiseIconContentIndex];
                praiseIconContent.className = "IconSelected";
            }

            // praiseContentOwner.onclick = null;
        }
    }
}

function gsCommentShowHiddenFloors(showButton)
{
    if (showButton==null)
    {
        return;
    }
    var floorsOwner = showButton.parentNode;
    if (floorsOwner==null)
    {
        return;
    }
    var replyCommentFloors
        = document.getElementsByClassName("ReplyComment");
    if (replyCommentFloors==null
        || replyCommentFloors.length<1)
    {
        return;
    }

    var replyCommentFloorsCount = replyCommentFloors.length;
    for (var replyCommentFloorIndex=0;
         replyCommentFloorIndex<replyCommentFloorsCount;
         replyCommentFloorIndex++)
    {
        var replyCommentFloor = replyCommentFloors[replyCommentFloorIndex];
        replyCommentFloor.style.display = "block";
    }
    showButton.style.display = "none";
}

function gsAddTouchEventListenerToObject(obj)
{
    if (obj==null
        || obj==undefined
        || obj.addEventListener==undefined)
    {
        return;
    }

    obj.addEventListener("touchstart", function(event)
    {
        gsSetElementClass(this, "", "TouchStart");
    }, false);
    obj.addEventListener("touchmove", function(event)
    {
        gsSetElementClass(this, "TouchStart", "");
    }, false);

    obj.addEventListener("touchmove", function(event)
    {
        gsSetElementClass(this, "TouchStart", "");
    }, true);
    obj.addEventListener("touchend", function(event)
    {
        gsSetElementClass(this, "TouchStart", "");
    }, false);

    obj.addEventListener("touchend", function(event)
    {
        gsSetElementClass(this, "TouchStart", "");
    }, true);

    obj.addEventListener("touchcancel", function(event)
    {
        gsSetElementClass(this, "TouchStart", "");
    }, false);
    obj.addEventListener("touchcancel", function(event)
    {
        gsSetElementClass(this, "TouchStart", "");
    }, true);
}

function gsImageOnClick()
{
    var gsImageUrls = new Array();
    var gsImageHdUrls = new Array();
    var gsImageDes = new Array();
    var index = this.getAttribute("gsElementID");
    var mainBody = document.getElementById("gsTemplateContent_MainBody");
    var images = mainBody.getElementsByTagName("img");
    for(var i=0;i<images.length;i++)
    {
        gsImageUrls.push(images[i].getAttribute("originsrc"));
        var additionalInfo = gsGetTheImageAdditionalInform(images[i]);
    	var v2 = eval("(" + additionalInfo + ")");
        gsImageHdUrls.push(v2.hdImageURL);
        gsImageDes.push(v2.caption);
    }

    window.handler.openImage2(gsImageUrls,gsImageHdUrls,gsImageDes,index);

}

function gsCheckTheURL(url, superATag)
{
    var result = new Object();
    {
        result.valid = false;
        result.imgTag = null;
        result.imgHDURL = "";
    }

    if (url!=null
        && typeof (url)!="undefined"
        && url.length>0)
    {
        result.valid = true;

        if (superATag != null)
        {
            var aTagChildNodes = superATag.childNodes;
            if (aTagChildNodes != null
                && aTagChildNodes.length == 1)
            {
                var aTagChildNode = aTagChildNodes[0];
                if (aTagChildNode != null
                    && aTagChildNode.tagName != null
                    && aTagChildNode.tagName == "IMG")
                {
                    result.valid = false;
                    result.imgTag = aTagChildNode;
                    var urlExfileName
                        = url.substring(url.length - 4, url.length);
                    if (urlExfileName == ".jpg"
                        || urlExfileName == ".gif"
                        || urlExfileName == "jpeg"
                        || urlExfileName == ".png"
                        || urlExfileName == ".bmp")
                    {
                        var urlSections = url.split("?");
                        if (urlSections != null
                            && urlSections.length > 1)
                        {
                            result.imgHDURL = urlSections[1];
                        }
                    }
                }
            }
        }
    }

    return result;

}
function gsGetTheImgSrc(img)
{
    return img.getAttribute("src");
}
function gsSetTheImgSrc(img, src)
{
    img.setAttribute("src", src);
    return;

}
function gsSetElementClass(element, classNameNeedRemoved, classNameNeedAdded)
{
    if (element==null)
    {
        return;
    }

    if (classNameNeedAdded==null
        || classNameNeedAdded.length<1)
    {
        if (classNameNeedRemoved!=null
            && classNameNeedRemoved.length>0
            && element.className!=null
            && element.className.length>0)
        {
            element.className = element.className.replaceAll(classNameNeedRemoved, "");
        }
    }
    else if (element.className==null
        || element.className.length<1)
    {
        element.className = classNameNeedAdded;
    }
    else if (classNameNeedRemoved!=null
        && classNameNeedRemoved.length>0
        && element.className.indexOf(classNameNeedRemoved)!=-1)
    {
        element.className = element.className.replaceAll(classNameNeedRemoved, classNameNeedAdded);
    }
    else if (element.className.indexOf(classNameNeedAdded)==-1)
    {
        element.className += " "+classNameNeedAdded;
    }
}
function gsIsElementClass(element, className)
{
    if (element==null
        || element.className==null
        || className==null)
    {
        return false;
    }
    return element.className.match(new RegExp('(\\s|^)'+className+'(\\s|$)'));
}
function gsGetObjectParentWithTag(obj, tagName)
{
    if (obj!=null)
    {
        if (tagName==null
            || tagName.length<1)
        {
            return obj.parentNode;
        }

        for (var parentNode=obj.parentNode;
             parentNode!=null;
             parentNode=parentNode.parentNode)
        {
            if (parentNode.tagName==tagName)
            {
                return parentNode;
            }
        }
    }
    return null;
}
function gsGetObjectParentWithClass(obj, className)
{
    if (obj!=null)
    {
        if (className==null
            || className.length<1)
        {
            return obj.parentNode;
        }

        for (var parentNode=obj.parentNode;
             parentNode!=null;
             parentNode=parentNode.parentNode)
        {
            if (gsIsElementClass(parentNode, className))
            {
                return parentNode;
            }
        }
    }
    return null;
}
function gsGetObjectParentUnderBody(obj)
{
    if (obj!=null)
    {
        var lastParentNode = obj;
        for (var parentNode=obj.parentNode;
             parentNode!=null;
             parentNode=parentNode.parentNode)
        {
            if (parentNode.tagName=="BODY")
            {
                return lastParentNode;
            }
            lastParentNode = parentNode;
        }
    }
    return null;
}
// 由于网页元素情况极为复杂
// 所以这里指针对游民内容进行判断
function gsIsObjectsContinuous(fromObj, toObj)
{
    if (fromObj==null
        || toObj==null)
    {
        return false;
    }

    // 1. 新闻内连续图片,例: 高清视频截图
    var fromParentPTag = gsGetObjectParentWithTag(fromObj, "P");
    var toParentPTag = gsGetObjectParentWithTag(toObj, "P");
    if (fromParentPTag==null
        || toParentPTag==null)
    {
        return false;
    }

    var objectsContinuous = true;
    for (var parentNextSibling=fromParentPTag.nextSibling;
         parentNextSibling!=null && parentNextSibling!=toParentPTag;
         parentNextSibling=parentNextSibling.nextSibling)
    {
        if (parentNextSibling.clientHeight>0)
        {
            if (parentNextSibling.childNodes==null
                || parentNextSibling.childNodes.length<1)
            {
                var finalText = parentNextSibling.innerText;
                if (finalText!=null)
                {
                    finalText = finalText.replaceAll(" ", "");
                }

                if (finalText!=null
                    && finalText.length>0)
                {
                    objectsContinuous = false;
                    break;
                }
            }
            else
            {
                objectsContinuous = false;
                break;
            }
        }
    }

    return objectsContinuous;
}
function gsSendMessageToSuperWebView(msg)
{

    document.location = msg;
//    if (gsIsCurrentOSAndroid())
//    {
//
//    }
//    else
//    {
//
//    }
//    var aTag = document.createElement("a");
//            aTag.href = msg;
//            aTag.click();
}
function gsOpenTheArticelWithId(articelId)
{
    var openTopicURL
        = "openPage:{\"pageID\":\"" + articelId +"\","
        + "\"pageURL\":\"\","
        + "\"openMethod\":\"withContentID\"}";
    gsSendMessageToSuperWebView(openTopicURL);
}
function gsOpenTheArticelWithURL(articelURL)
{
    var openTopicURL
        = "openPage:{\"pageID\":\"\","
        + "\"pageURL\":\"" + articelURL + "\","
        + "\"openMethod\":\"withContentURL\"}";
    gsSendMessageToSuperWebView(openTopicURL);
}
function gsOpenTheTopicWithId(topicId)
{
    var openTopicURL
        = "openPage:{\"pageID\":\"" + topicId +"\","
        + "\"pageURL\":\"openTheSubscribeTopic.app.gamersky\","
        + "\"openMethod\":\"withContentURL\"}";
    gsSendMessageToSuperWebView(openTopicURL);
}
function gsSubscribeTheTopicWithId(topicId)
{
    var subscribeTopicURL
        = "openPage:{\"pageID\":\"" + topicId +"\","
        + "\"pageURL\":\"subscribeTheTopic.app.gamersky\","
        + "\"openMethod\":\"withContentID\"}";

    gsSendMessageToSuperWebView(subscribeTopicURL);

    event.currentTarget.onclick
        = function()
    {
        gsCancelSubscribeTheTopicWithId(topicId);
    };
    var buttons = event.currentTarget.getElementsByClassName("Button");
    if (buttons!=null && buttons.length>0)
    {
        for (var buttonIndex in buttons)
        {
            var button = buttons[buttonIndex];
            button.className = "Button Readed";
            button.innerHTML = "已订阅";
        }
    }
}
function gsCancelSubscribeTheTopicWithId(topicId)
{
    var subscribeTopicURL
        = "openPage:{\"pageID\":\"" + topicId +"\","
        + "\"pageURL\":\"cancelSubscribeTheTopic.app.gamersky\","
        + "\"openMethod\":\"withContentID\"}";

    gsSendMessageToSuperWebView(subscribeTopicURL);

    event.currentTarget.onclick
        = function()
    {
        gsSubscribeTheTopicWithId(topicId);
    };
    var buttons = event.currentTarget.getElementsByClassName("Button");
    if (buttons!=null && buttons.length>0)
    {
        for (var buttonIndex in buttons)
        {
            var button = buttons[buttonIndex];
            button.className = "Button";
            button.innerHTML = "订阅";
        }
    }
}

function gsGetTheImageAdditionalInform(img)
{
    if (img==null || img.tagName.toLowerCase()!="img")
    {
        return "{\"caption\":\"\", \"hdImageURL\":\"\"}";
    }

    var imgCaption = "";


    if (img!=null)
    {
        var nextBrTag = img.nextElementSibling;
        if (nextBrTag!=null
            && nextBrTag.tagName=="BR")
        {
            for (var nextSibling=nextBrTag.nextSibling;
                 nextSibling!=null;
                 nextSibling=nextSibling.nextSibling)
            {
                if (nextSibling.nodeType==3)
                {
                    imgCaption = nextSibling.data;
                    if (imgCaption!=null
                        && imgCaption.length>0)
                    {
                        break;
                    }
                }
                else if (nextSibling.tagName!="SPAN")
                {
                    break;
                }
            }
        }
        if (imgCaption==null
            || imgCaption.length<1)
        {
            var parentPTag = gsGetObjectParentWithTag(img, "P");
            if (parentPTag!=null
                && parentPTag.nextElementSibling!=null
                && parentPTag.nextElementSibling.tagName=="P")
            {
                var nextPTag = parentPTag.nextElementSibling;
                imgCaption = nextPTag.textContent;
            }
        }
    }

    if (imgCaption.length>0)
    {
        if (gsIsCurrentOSAndroid())
        {
            imgCaption = imgCaption.replace(/[\r\n]/g, "");
            imgCaption = imgCaption.replace(/["]/g, "\\\"");
        }
        else
        {
            imgCaption = imgCaption.replaceAll(" ", "");
            imgCaption = imgCaption.replaceAll("\r\n", "");
            imgCaption = imgCaption.replaceAll("\r", "");
            imgCaption = imgCaption.replaceAll("\n", "");
            imgCaption = imgCaption.replaceAll("\"", "\\\"");
        }
    }

    var imgHDImageURL = img.getAttribute("hdImageURL");
    if (imgHDImageURL==null)
    {
        imgHDImageURL = "";
    }
    if (imgHDImageURL.length>0)
    {
        imgHDImageURL = imgHDImageURL.replaceAll(" ", "");
        imgHDImageURL = imgHDImageURL.replaceAll("\r\n", "");
        imgHDImageURL = imgHDImageURL.replaceAll("\r", "");
        imgHDImageURL = imgHDImageURL.replaceAll("\n", "");
        imgHDImageURL = imgHDImageURL.replace(/[\r\n]/g, "");
    }

    return "{\"caption\":\"" + imgCaption + "\", \"hdImageURL\":\"" + imgHDImageURL + "\"}";
}
function gsIsCurrentOSIOS()
{
    if (_kGSOSBuffer==1)
    {
        return true;
    }
    else if (_kGSOSBuffer==0)
    {
        if (typeof(kOS)!="undefined")
        {
            if (kOS=="android")
            {
                _kGSOSBuffer = 2;
            }
            else if (kOS=="ios")
            {
                _kGSOSBuffer = 1;
                return true;
            }
        }
        return false;
    }
    return false;
}
function gsIsCurrentOSAndroid()
{
    if (_kGSOSBuffer==2)
    {
        return true;
    }
    else if (_kGSOSBuffer==0)
    {
        if (typeof(kOS)!="undefined")
        {
            if (kOS=="android")
            {
                _kGSOSBuffer = 2;
                return true;
            }
            else if (kOS=="ios")
            {
                _kGSOSBuffer = 1;
            }
        }
        return false;
    }
    return false;
}