// JavaScript Document
// JavaScript Document
function leTvVideo(width, height, uu, vu) {

    var html = null;
    var leTv_videosupport_js = "http://yuntv.letv.com/videosupport_2.0_v1.m.js";

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

        var bodyWidth = document.body.clientWidth - 20;

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
}
function gsVideo(videoType, videoContent, videoWidth, videoHeight) {
  
    if (location.protocol == "file:") {
        gsVideoInApp(videoType, videoContent, null, videoWidth, videoHeight);
    }
    else {
        switch (videoType) {
            case "优酷":
            case "土豆":
                {
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
                            " width=\"650\" ");
                    }

                    document.write(videoContent);
                }
                break;
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