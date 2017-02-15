function setTitle(title)
{
    document.title = title;
}

function setMainTitle(title)
{
    document.getElementById("gsTemplateContent_Title").innerText = title;
}

function setSubTitle(title)
{
    document.getElementById("gsTemplateContent_Subtitle").innerText = title;
}

function setBody(body)
{
    document.getElementById("gsTemplateContent_MainBody").innerHTML = body;
}
 
function SetElementClass(element, classNameNeedRemoved, classNameNeedAdded) {
    if (element == null) {
        return;
    }

    if (classNameNeedAdded == null
        || classNameNeedAdded.length < 1) {
        if (classNameNeedRemoved != null
            && classNameNeedRemoved.length > 0
            && element.className != null
            && element.className.length > 0) {
            element.className = element.className.replaceAll(classNameNeedRemoved, "");
        }
    }
    else if (element.className == null
        || element.className.length < 1) {
        element.className = classNameNeedAdded;
    }
    else if (classNameNeedRemoved != null
        && classNameNeedRemoved.length > 0
        && element.className.indexOf(classNameNeedRemoved) != -1) {
        element.className = element.className.replaceAll(classNameNeedRemoved, classNameNeedAdded);
    }
    else if (element.className.indexOf(classNameNeedAdded) == -1) {
        element.className += " " + classNameNeedAdded;
    }
}

function DayMode() {
    var htmlTag = document.getElementsByTagName("html")[0];
    SetElementClass(htmlTag, "PageColorMode_Night", "PageColorMode_Day");

    var imgs = document.getElementsByTagName('img');
    for (i = 0; i < imgs.length; i++) {
        imgs[i].style.opacity = 1.0;
    }
}

function NightMode() {
    var htmlTag = document.getElementsByTagName("html")[0];
    SetElementClass(htmlTag, "PageColorMode_Day", "PageColorMode_Night");

    var imgs = document.getElementsByTagName('img');
    for (var i = 0; i < imgs.length; i++) {
        imgs[i].style.opacity = 0.6;
    }
}

//向webview发出通知
function SendNotify(notifyString) {
    window.external.notify(notifyString);
} 
 

//通过Id打开新闻
function OpenEssayById(id)
{ 
    SendNotify(id.toString()); 
}

//获取所有的图片
function GetAllPictures() {
    var imgs = document.getElementsByTagName('img');
    var imgArray = new Array();

    for (var i = 0; i < imgs.length; i++) {
        var img = new Object();
        img.src = imgs[i].src;
        img.hdsrc = imgs[i].parentNode.href;
        img.index = i;
        imgArray.push(img); 
    }
    return JSON.stringify(imgArray);
    //SendNotify(JSON.stringify(imgArray));
}

//无图模式
function NoImageMode() {
    var imgs = document.getElementsByTagName("img");
    for (var index = 0; i < imgs.length; i++) {
        imgs[index].setAttribute("src", "");
    }
    $("img").attr("src", "");
}
 

function ChangeFontSize(para) {
    switch (para) {
        case "min":
            {
                gsSetElementClass(htmlTag, "PageFontSize_Middle", "");
                gsSetElementClass(htmlTag, "PageFontSize_Max", "PageFontSize_Min");
            }
            break;
        case "middle":
            {
                gsSetElementClass(htmlTag, "PageFontSize_Min", "");
                gsSetElementClass(htmlTag, "PageFontSize_Max", "PageFontSize_Middle");
            }
            break;
        case "max":
            {
                gsSetElementClass(htmlTag, "PageFontSize_Min", "");
                gsSetElementClass(htmlTag, "PageFontSize_Middle", "PageFontSize_Max");
            }
            break;
    }
}

function SetFontSize(fontSize)
{
    var a = document.getElementsByClassName("content")[0];
    a.style.fontSize = fontSize + 'pt';
}

function translatePage()
{
    $.getScript("http://www.microsofttranslator.com/ajax/v2/toolkit.ashx?mode=manual&toolbar=thin", function() {
		$("#translatorbtn").click(function(){
			translatePage();
		});
	});
	function translatePage(){Microsoft.Translator.translate(document.body,"zh-CHS", "en");}
}
 

 