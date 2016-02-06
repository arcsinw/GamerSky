//视频播放代码
function gsVideoInApp(videoType, videoContent, videoParam, videoWidth, videoHeight) {
    switch (videoType) {
        case "优酷":
        case "土豆":
            {
                videoContent = videoContent;
            } break;
        case "乐视":
            {
                videoContent
                    = gsCreateLeTvVideoContent(videoContent, videoParam);
            } break;
    }

    var videoHTML = videoContent;

    if ((gsIsCurrentOSIOS() && _kGSAppVersionNumberCode >= 201)
    ||
        gsIsCurrentOSAndroid()) {
        var videoIndex = gsSaveAnVideoOriginContent(videoContent);

        if (videoIndex > -1) {
            var videoDefaultWidth
                = document.body.clientWidth - 20.0;
            var videoDefaultHeight
                = videoDefaultWidth
                * 1080.0 / 1920.0;

            videoHTML
                = "<table class=\"GSTemplateContent_VideoBoxer\" id=\"videoBoxer_" + videoIndex + "\" style=\"\" border=\"0\" width=\"" + videoDefaultWidth + "\" height=\"" + videoDefaultHeight + "\" cellspacing=\"0\" cellpadding=\"0\">"
                + "<tr>"
                + "<td valign=\"middle\" align=\"center\">"
                + "<div class=\"PlayButtonBackground\"></div>"
                + "<div class=\"PlayButton\" onclick=\"gsPlayVideoWithVideoIndex(" + videoIndex + ")\"></div>"
                + "</td>"
                + "</tr>"
                + "</table>";
        }
    }
    document.write(videoHTML);
}