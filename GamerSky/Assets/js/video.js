
function video_GetFirstVideo()
{
    var firstVideo = null;
	var videos = document.getElementsByTagName("video");
	if (videos!=null && videos.length>0)
	{
		var videosCount = videos.length;
		for (var videoIndex=0; videoIndex<videosCount; videoIndex++)
		{
			firstVideo = videos[videoIndex];
			if (firstVideo!=null)
			{
				break;
			}
		}
	}
	if (firstVideo==null)
	{
		var iFrames = document.getElementsByTagName("iframe");
		if (iFrames!=null && iFrames.length>0)
		{
			var iFramesCount = iFrames.length;

            for (var iFrameIndex=0; iFrameIndex<iFramesCount; iFrameIndex++)
			{
				var iFrame = iFrames[iFrameIndex];
				
				var videos = iFrame.contentWindow.document.getElementsByTagName("video");

				if (videos!=null && videos.length>0)
				{
					var videosCount = videos.length;
					for (var videoIndex=0; videoIndex<videosCount; videoIndex++)
					{
						firstVideo = videos[videoIndex];
						if (firstVideo!=null)
						{
							break;
						}
					}
				}
				
				if (firstVideo!=null)
				{
					break;
				}
			}
		}
	}
    return firstVideo;
}

function video_PlayFirstVideo()
{
	var firstVideo = document.getElementsByTagName("iframe")[0];
	window.handler.print(window.handler.getScreenWidth());
	window.handler.print(firstVideo.src);
    window.handler.print("liyongfu 9823493204");
    firstVideo.setAttribute("height",window.handler.getScreenWidth());
    /*
    if (firstVideo!=null)
	{
		firstVideo.play();
	}
    */
    var innerVideo = video_GetFirstVideo();
    innerVideo.play();
    
}
function video_PauseFirstVideo()
{
    var firstVideo = video_GetFirstVideo();
    if (firstVideo!=null)
    {
        firstVideo.pause();
    }
}
function video_IsFirstVideoPlaying()
{
    var firstVideo = video_GetFirstVideo();
    if (firstVideo!=null)
    {
        if (!firstVideo.paused())
        {
            return "yes";
        }
    }
    
    return "no";
}
