// JavaScript Document
// JavaScript Document
function leTvVideo(width, height, uu, vu) {

    var html = null;
    var leTv_videosupport_js = "http://yuntv.letv.com/videosupport_2.0_v1.m.js";

    // 2016年07月22日
    // 增加了wap旋转改变尺寸的js gsvideoWinRi()
    // 同时减去的宽度由 20.0 变成 28.0

    // 2016年06月23日
    // 本次更内容:
    // 乐视更新了JS,跟进同步内容
    // 1. 优酷分享代码，新增了参数“newPlayer: false, show_related: false”，如果不加此参数，视频播放完毕后一定几率跳出优酷页面。

    // 2015年08月24日
    // 本次更内容:
    // 乐视更新了JS,跟进同步内容
    // 1. 更新了"http://j.gamersky.com/wap/js/leTv_videosupport_2.0_v1.m.js"
    //		乐视原文件进行了更新,文件名没有更改
    //		本次更新在最新的乐视原文件上注释掉了自动加载视频内容的代码,可在文件内搜索@更改查看
    // 2. "http://yuntv.letv.com/bcloud.js" 中更新 "player_v2.1.js" 至 "player_v2.3.js"
    //		当前我们将".../bcloud.js" 整合进了当前文件(leTV.js),本次更新将更新 "player_v2.1.js" 至 "player_v2.3.js"

    // 2015年06月11日
    // 本次更内容:
    // 由于 IE8下 "http://yuntv.letv.com/bcloud.js" 在 document.write 中的 document.write 会在当前脚本断之外执行, 导致IE8里视频上方多出一个空白div框架
    // 所以本次更改将  "http://yuntv.letv.com/bcloud.js" 中的内容直接拿来写入,避免出现 document.write 中的 document.write 从而修正该问题
    // 2015年06月01日
    // 如果是wap或app端, 使用如下代码

    // 区别为:

    // 1. 提取了wap端所需的JS,

    // 2. 修改了乐视的一个JS文件"leTv_videosupport_2.0_v1.m.js"
    // 原文件地址为 "http://yuntv.letv.com/videosupport_2.0_v1.m.js"

    // 在文件中只修改一处

    // 可在本地文件"leTv_videosupport_2.0_v1.m.js"中搜索 "@更改" 查看修改内容(只是注释掉了预载代码)

    // 该js文件需要放在服务端上,如果相对路径改变,需要修改以下代码中的相应路径
    if (location.host == "wap.gamersky.com" || location.protocol == "file:") {

        // 移动端自适应宽度

        var bodyWidth = document.body.clientWidth - 28;

        height = bodyWidth * 9.0 / 16.0;

        width = bodyWidth;

        leTv_videosupport_js = "http:\/\/j.gamersky.com\/wap\/js\/leTv_videosupport_2.0_v1.m.js";
    }


    html = "<div id=\"leshitvauto\" style=\"margin-left:auto;margin-right:auto; width:" + width + "; height:" + height + ";\">"

		+ "<script type=\"text\/javascript\">"

		+ "var domainname = \"http:\/\/yuntv.letv.com\/\";"

		+ "<\/script>"
		+ "<script type=\"text\/javascript\" src=\"http:\/\/yuntv.letv.com\/swfobj_1.3.m.js\"><\/script>"

		+ "<script type=\"text\/javascript\" src=\"" + leTv_videosupport_js + "\"><\/script>"

		+ "<script type=\"text\/javascript\" src=\"http:\/\/yuntv.letv.com\/user_defined.js?v2\"><\/script>"

		+ "<script type=\"text\/javascript\" src=\"http:\/\/yuntv.letv.com\/js\/player_v2.3.js\" data=\"{uu:'" + uu + "',vu:'" + vu + "',auto_play:'0',gpcflag:'1',width:'" + width + "',height:'" + height + "'}\"><\/script>"

		+ "<\/div>";

    document.write(html);
    gsvideoWinRi("#leshitvauto");
}

// 函数 gsGetWidthFromVideoContent
// 从原始视频内容中获取指定的视频宽度，不包括宽度单位。
function gsGetWidthFromVideoContent(videoContent) {
    if (videoContent != null
    && videoContent.length > 0) {
        var videoWidth = null;

        var regForWidth = /\s\1*width=([\'"]?)([^\'" ]+)\1/g;
        videoWidth = videoContent.match(regForWidth);
        if (videoWidth != null) {
            videoWidth = videoWidth.toString();
        }
        else {
            var regForStyleWidth = /\1*width:([\'\s]?)([^\';]+)[\';]\1/g;
            videoWidth = videoContent.match(regForStyleWidth);
            if (videoWidth != null) {
                videoWidth = videoWidth.toString();
            }
        }
        if (videoWidth != null
        && videoWidth.length > 0) {
            var regForNumber = /[0-9]*/g;
            videoWidth = videoWidth.match(regForNumber);
            if (videoWidth != null) {
                videoWidth = videoWidth.toString().replace(/,*/g, "");
            }
        }

        if (videoWidth != null
        && videoWidth.length > 0) {
            return videoWidth + "px";
        }
    }
    return 0;
}

// 函数 gsGetHeightFromVideoContent
// 从原始视频内容中获取指定的视频宽度，不包括宽度单位。
function gsGetHeightFromVideoContent(videoContent) {
    if (videoContent != null
        && videoContent.length > 0) {
        var videoHeight = null;

        var regForHeight = /\s\1*height=([\'"]?)([^\'" ]+)\1/g;
        videoHeight = videoContent.match(regForHeight);
        if (videoHeight != null) {
            videoHeight = videoHeight.toString();
        }
        else {
            var regForStyleHeight = /\1*height:([\'\s]?)([^\';]+)[\';]\1/g;
            videoHeight = videoContent.match(regForStyleHeight);
            if (videoHeight != null) {
                videoHeight = videoHeight.toString();
            }
        }
        if (videoHeight != null
            && videoHeight.length > 0) {
            var regForNumber = /[0-9]*/g;
            videoHeight = videoHeight.match(regForNumber);
            if (videoHeight != null) {
                videoHeight = videoHeight.toString().replace(/,*/g, "");
            }
        }

        if (videoHeight != null
            && videoHeight.length > 0) {
            return videoHeight + "px";
        }
    }
    return 0;
}

// 函数 gsVideo:
// 2016年10月10日
// 本次更新内容：
// 不在页面播放第三方视频，变为跳转至第三方页面。
// 本次更新设计文件: "gsVideo.js"，"leTV.js"。
// 查看新增代码可搜索"@视频页面跳转改动"。
// 所有视频跳转界面都在Class为“VideoJumper”的“div”中。
// 2016年08月18日
// 本次更内容:
// 1，新版优酷代码使用单引号表示视频Id,双引号表示视频尺寸，导致之前根据双引号获取视频Id的方法失败，
// 本次修改会同时判断双引号和单引号，取两者中正确的值去获取视频Id，相关代码可搜索: youkuVideoIdEndCharIndexWithDoubleQuotes 。
// 函数 gsVideo:
// 2016年03月25日
// 本次更内容:
// 1.优酷视频使用了新的合作方式代码,无论原始视频代码是视频URL,iFrame代码,或JS代码,
//   函数gsVideo都会从中提取"优酷视频Id",然后拼写成合作代码进行播放.
function gsVideo(videoType, videoContent, videoWidth, videoHeight) {
    var isWap = location.host == "wap.gamersky.com";
    window.external.notify(isWap);
    if (location.protocol == "file:") {
        gsVideoInApp(videoType, videoContent, null, videoWidth, videoHeight);
    }
    else {
        switch (videoType) {
            // 优酷视频代码使用了新的合作形式.
            case "优酷":
                {
                    // 尝试获取优酷视频Id.
                    var youkuVideoId = null;
                    if (videoContent != null
                    && videoContent.length > -1) {
                        // 保存原始视频内容.
                        var originVideoContent = videoContent;
                        // 去除空格.
                        videoContent = videoContent.replace(new RegExp(" ", "gm"), "");
                        // 保存去掉空格后的原始视频内容.
                        var originVideoContentWithoutBlank = videoContent;
                        // 小写化.
                        videoContent = videoContent.toLowerCase();

                        // 尝试从URL格式的视频代码中获取视频Id.
                        var urlPlayerSign
                            = "http://v.youku.com/v_show/id_";
                        var indexOfURLPlayerSign
                            = videoContent.indexOf(urlPlayerSign);
                        if (indexOfURLPlayerSign > -1) {
                            var youkuVideoIdBeginCharIndex
                                = indexOfURLPlayerSign + urlPlayerSign.length;
                            var youkuVideoIdEndCharIndex
                                = videoContent.indexOf(
                                "==",
                                youkuVideoIdBeginCharIndex)
                                + 2;
                            youkuVideoId
                                = originVideoContentWithoutBlank.substring(youkuVideoIdBeginCharIndex, youkuVideoIdEndCharIndex);
                        }
                        else {
                            // 尝试从iFrame格式的视频代码中获取视频Id.
                            var iFramePlayerSign
                                = "http://player.youku.com/embed/";
                            var indexOfiFramePlayerSign
                                = videoContent.indexOf(iFramePlayerSign);
                            if (indexOfiFramePlayerSign > -1) {
                                var youkuVideoIdBeginCharIndex
                                    = indexOfiFramePlayerSign + iFramePlayerSign.length;

                                var youkuVideoIdEndCharIndexWithDoubleQuotes
                                    = videoContent.indexOf(
                                    "\"",
                                    youkuVideoIdBeginCharIndex);
                                var youkuVideoIdEndCharIndexWithSingleQuote
                                    = videoContent.indexOf(
                                    "\'",
                                    youkuVideoIdBeginCharIndex);

                                var youkuVideoIdEndCharIndex = youkuVideoIdBeginCharIndex;
                                if (youkuVideoIdEndCharIndexWithDoubleQuotes != -1
                                && youkuVideoIdEndCharIndexWithSingleQuote != -1) {
                                    if (youkuVideoIdEndCharIndexWithDoubleQuotes < youkuVideoIdEndCharIndexWithSingleQuote) {
                                        youkuVideoIdEndCharIndex = youkuVideoIdEndCharIndexWithDoubleQuotes;
                                    }
                                    else if (youkuVideoIdEndCharIndexWithSingleQuote != -1) {
                                        youkuVideoIdEndCharIndex = youkuVideoIdEndCharIndexWithSingleQuote;
                                    }
                                }
                                else if (youkuVideoIdEndCharIndexWithDoubleQuotes != -1
                                         && youkuVideoIdEndCharIndexWithSingleQuote == -1) {
                                    youkuVideoIdEndCharIndex = youkuVideoIdEndCharIndexWithDoubleQuotes;
                                }
                                else if (youkuVideoIdEndCharIndexWithDoubleQuotes == -1
                                         && youkuVideoIdEndCharIndexWithSingleQuote != -1) {
                                    youkuVideoIdEndCharIndex = youkuVideoIdEndCharIndexWithSingleQuote;
                                }


                                youkuVideoId
                                    = originVideoContentWithoutBlank.substring(youkuVideoIdBeginCharIndex, youkuVideoIdEndCharIndex);
                            }
                            else {
                                // 尝试从JS格式的视频代码中获取视频Id.
                                var jsPlayerSign
                                    = "vid:'";
                                var indexOfJSPlayerSign
                                    = videoContent.indexOf(jsPlayerSign);
                                if (indexOfJSPlayerSign > -1) {
                                    var youkuVideoIdBeginCharIndex
                                        = indexOfJSPlayerSign + jsPlayerSign.length;
                                    var youkuVideoIdEndCharIndex
                                        = videoContent.indexOf(
                                        "'",
                                        youkuVideoIdBeginCharIndex);
                                    youkuVideoId
                                        = originVideoContentWithoutBlank.substring(youkuVideoIdBeginCharIndex,
                                        youkuVideoIdEndCharIndex);
                                }
                            }
                        }
                    }

                    if (youkuVideoId != null
                    && youkuVideoId.length > 0) {
                        // 指定宽度
                        if (typeof (videoWidth) == "undefined") {
                            if (isWap) {
                                // 移动端自适应宽度
                                videoWidth = document.body.clientWidth - 28.0;
                                videoHeight = (videoWidth * 9.0 / 16.0) + "px";
                                videoWidth = videoWidth + "px";
                            }
                            else {
                                videoWidth
                                    = gsGetWidthFromVideoContent(originVideoContent);
                                if (videoWidth == null || videoWidth.length < 1) {
                                    videoWidth = "550px";
                                    videoHeight = "350px";
                                }
                                else {
                                    videoHeight
                                        = gsGetHeightFromVideoContent(originVideoContent);
                                    if (videoHeight == null || videoHeight.length < 1) {
                                        videoHeight = (parseFloat(videoWidth) * 9.0 / 16.0) + "px";
                                    }
                                }
                            }
                        }
                        else if (typeof (videoHeight) == "undefined") {
                            videoHeight = (parseFloat(videoWidth) * 9.0 / 16.0) + "px";
                        }

                        var youkuPlayerId = 0;
                        if (typeof (window.kGS_YouKuPlayersCount) == "undefined") {
                            window.kGS_YouKuPlayersCount = 0;
                        }
                        youkuPlayerId = window.kGS_YouKuPlayersCount;
                        window.kGS_YouKuPlayersCount++;

                        var gsYouKuClientId = "f7d81b29f4146ce2";
                        var youkuPlayerParams = "{styleid:'0', client_id:'" +
                                                gsYouKuClientId +
                                                "', vid:'" +
                                                youkuVideoId +
                                                "', newPlayer:false, show_related:false}";

                        var youkuPlayerIdParam = "youkuplayer_" + youkuPlayerId;
                        var youkuPlayerHTML = "";
                        // @视频页面跳转改动
                        if (!isWap) {
                            youkuPlayerHTML
                                = "<div class=\"VideoJumper\" style=\"width: 100%; height: auto; text-align: center;\">"
                                  + "<a href='http://v.youku.com/v_show/id_" + youkuVideoId + ".html?from=s1.8-3-1.1&spm=a2h0k.8191407.0.0' target='_blank'>"
                                  + "<img src=\"http://image.gamersky.com/webimg15/videoplaceholder.jpg\">"
                                  + "</a>"
                                  + "</div>";
                        }
                        else {
                            if (youkuPlayerId == 0) {
                                youkuPlayerHTML = "<div id=\"" +
                                                  youkuPlayerIdParam +
                                                  "\" style=\"width:" +
                                                  videoWidth +
                                                  ";height:" +
                                                  videoHeight +
                                                  "; margin:0px auto;\"></div>"
                                                  +
                                                  "<script type=\"text/javascript\" src=\"http://player.youku.com/jsapi\">"
                                                  + "window.kGS_"
                                                  + youkuPlayerIdParam
                                                  + " = new YKU.Player('" +
                                                  youkuPlayerIdParam +
                                                  "', " +
                                                  youkuPlayerParams +
                                                  ");"
                                                  +
                                                  "</script>";
                            }
                            else {
                                // 需要多个视频播放时，后续视频使用iframe格式。
                                /*
                                 youkuPlayerHTML = "<div id=\"" + youkuPlayerIdParam + "\" style=\"width:" + videoWidth + ";height:" + videoHeight + "; margin:0px auto;\"></div>"
                                 + "<script type=\"text/javascript\">"
                                 + "window.kGS_" + youkuPlayerIdParam + " = new YKU.Player('" + youkuPlayerIdParam + "', " + youkuPlayerParams + ");"
                                 + "</script>";
                                 */
                                youkuPlayerHTML
                                    = "<iframe width=\"" +
                                      videoWidth +
                                      "\" height=\"" +
                                      videoHeight
                                      +
                                      "\" src=\"http://player.youku.com/embed/" +
                                      youkuVideoId +
                                      "?client_id=" +
                                      gsYouKuClientId +
                                      "\" frameborder=0 allowfullscreen></iframe>";
                            }
                        }
                        document.write(youkuPlayerHTML);
                        gsvideoWinRi("#" + youkuPlayerIdParam);
                    }
                        // 获取视频Id失败,则按旧版本代码执行.
                    else {
                        gsVideo("优酷_旧版本", videoContent, videoWidth, videoHeight)
                    }
                } break;
            case "优酷_旧版本":
                {
                    if (!isWap) {
                        videoContent
                            = "<div class=\"VideoJumper\" style=\"width: 100%; height: auto; text-align: center;\">"
                              + "暂时无法播放视频"
                              + "</div>";
                        document.write(videoContent);
                    }
                } break;
            case "土豆":
                {
                    // @视频页面跳转改动
                    if (!isWap) {
                        var tudouVideoCode = "";
                        var tudouVideoCodeBeginSign = "&code=";
                        var tudouVideoCodeBeginSignIndex = videoContent.indexOf(tudouVideoCodeBeginSign);
                        if (tudouVideoCodeBeginSignIndex > -1) {
                            var tudouVideoCodeEndSignIndex
                                = videoContent.indexOf("&", tudouVideoCodeBeginSignIndex + 1);
                            if (tudouVideoCodeEndSignIndex == -1) {
                                tudouVideoCodeEndSignIndex = videoContent.length;
                            }
                            tudouVideoCode
                                = videoContent.substring(
                                tudouVideoCodeBeginSignIndex + tudouVideoCodeBeginSign.length,
                                tudouVideoCodeEndSignIndex);
                            if (tudouVideoCode == null) {
                                tudouVideoCode = "";
                            }
                        }

                        var tudouVideoLCode = "";
                        var tudouVideoLCodeBeginSign = "&lcode=";
                        var tudouVideoLCodeBeginSignIndex = videoContent.indexOf(tudouVideoLCodeBeginSign);
                        if (tudouVideoLCodeBeginSignIndex > -1) {
                            var tudouVideoLCodeEndSignIndex
                                = videoContent.indexOf("&", tudouVideoLCodeBeginSignIndex + 1);
                            if (tudouVideoLCodeEndSignIndex == -1) {
                                tudouVideoLCodeEndSignIndex = videoContent.length;
                            }
                            tudouVideoLCode
                                = videoContent.substring(
                                tudouVideoLCodeBeginSignIndex + tudouVideoLCodeBeginSign.length,
                                tudouVideoLCodeEndSignIndex);
                            if (tudouVideoLCode == null) {
                                tudouVideoLCode = "";
                            }
                        }

                        videoContent = "暂时无法播放视频";
                        if (tudouVideoCode != null
                        && tudouVideoCode.length > 0) {
                            if (tudouVideoLCode != null
                                && tudouVideoLCode.length > 0) {
                                videoContent
                                    = "<a href='http://www.tudou.com/albumplay/"
                                      + tudouVideoLCode + "/"
                                      + tudouVideoCode + ".html"
                                      + "/?spm=a2h0k.8191414.XnHXIkxXL6E.A' target='_blank'>"
                                      + "<img src=\"http://image.gamersky.com/webimg15/videoplaceholder.jpg\">"
                                      + "</a>";
                            }
                            else {
                                videoContent
                                    = "<a href='http://www.tudou.com/programs/view/"
                                      + tudouVideoCode
                                      + "/?spm=a2h0k.8191414.XnHXIkxXL6E.A' target='_blank'>"
                                      + "<img src=\"http://image.gamersky.com/webimg15/videoplaceholder.jpg\">"
                                      + "</a>";
                            }
                        }
                        videoContent
                            = "<div class=\"VideoJumper\" style=\"width: 100%; height: auto; text-align: center;\">"
                              + videoContent
                              + "</div>";
                    }
                    else {
                        var regForHeight = /\s\1*height=([\'"]?)([^\'" ]+)\1/g;
                        var regForStyleHeight = /\1*height:([\'\s]?)([^\';]+)[\';]\1/g;
                        if (typeof (videoHeight) != "undefined") {
                            videoContent = videoContent.replace(regForHeight, "");
                            videoContent = videoContent.replace(regForStyleHeight, "");

                            videoContent
                                = videoContent.replace(
                                " ",
                                " height=\"" + videoHeight + "\" ");
                        }
                        else if (videoContent.match(regForHeight) == null
                                 && videoContent.match(regForStyleHeight) == null) {
                            videoContent
                                = videoContent.replace(
                                " ",
                                " height=\"350\" ");
                        }

                        var regForWidth = /\s\1*width=([\'"]?)([^\'" ]+)\1/g;
                        var regForStyleWidth = /\1*width:([\'\s]?)([^\';]+)[\';]\1/g;
                        if (typeof (videoWidth) != "undefined") {
                            videoContent = videoContent.replace(regForWidth, "");
                            videoContent = videoContent.replace(regForStyleWidth, "");

                            videoContent
                                = videoContent.replace(
                                " ",
                                " width=\"" + videoWidth + "\" ");
                        }
                        else if (videoContent.match(regForWidth) == null
                                 && videoContent.match(regForStyleWidth) == null) {
                            videoContent
                                = videoContent.replace(
                                " ",
                                " width=\"550\" ");
                        }
                    }
                    document.write(videoContent);
                } break;
                /*
                case "乐视":
                {
                    if (typeof(videoWidth)=="undefined")
                    {
                        videoWidth = 550;
                    }
                    if (typeof(videoHeight)=="undefined")
                    {
                        videoHeight = 345;
                    }
    
                    leTvVideo(videoWidth, videoHeight, videoContent, videoParam);
                }
                    break;
                    */
        }
    }
}
function gsvideoWinRi(th) {
    if (location.host == "wap.gamersky.com") {
        $(window).resize(function () {
            var bWidth = document.body.clientWidth - 28.0;
            gh = bWidth * 9.0 / 16.0;
            gw = bWidth;
            $(th).css({ 'width': gw + 'px', 'height': gh + 'px' });
        });
    } else {
        return false;
    }
}