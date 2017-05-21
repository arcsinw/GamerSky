
﻿function setTitle(title)
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
 

//获取所有的图片
function GetAllPictures() {
    var imgs = document.getElementsByTagName('img');
    var imgArray = new Array();

    for (var i = 0; i < imgs.length; i++) {
        var img = new Object();
        img.src = imgs[i].src;
        img.hdsrc = imgs[i].parentNode.href;
        img.index = i + 1;
        imgArray.push(img);

    }
    SendNotify(JSON.stringify(imgArray));
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
 

window.onload = function ()
{
    gestureInit();
    $("img").lazyload();
}


//////////////////////////////////////////////////////////////////////////////
// 手势识别
var myGesture;
var myADGesture;
var myRelatedGesture;
var myElement;
var myADElement;
var myRelatedElement;
var gestureStartX;

function gestureInit() {
    gesturePrepareTarget('paragraph', gestureListener);
    //gesturePrepareTarget('adcontent', gestureListener);
    gesturePrepareTarget('related', gestureListener);

    myGesture = new MSGesture();
    myElement = document.getElementById('paragraph');
    myGesture.target = myElement;

    //myADGesture = new MSGesture();
    //myADElement = document.getElementById('adcontent');
    //myADGesture.target = myADElement;

    myRelatedGesture = new MSGesture();
    myRelatedElement = document.getElementById('related');
    myRelatedGesture.target = myRelatedElement;
}

function onLoad() {
    gestureInit()
}

function gesturePrepareTarget(targetId, eventListener) {
    var target = document.getElementById(targetId);
    target.addEventListener('MSGestureStart', eventListener, false);
    target.addEventListener('MSGestureEnd', eventListener, false);
    target.addEventListener('MSGestureChange', eventListener, false);
    target.addEventListener('MSInertiaStart', eventListener, false);
    target.addEventListener('MSGestureTap', eventListener, false);
    target.addEventListener('MSGestureHold', eventListener, false);
    target.addEventListener('pointerdown', eventListener, false);
}

function getstureReset() {
    myGesture.reset();
    gestureStartX = 0;
    gestureStartY = 0;
}

function gestureListener(evt) {
    if (isselecting) return;
    if (evt.type == 'pointerdown') {
        myGesture.addPointer(evt.pointerId);
        gestureStartX = evt.clientX;
        gestureStartY = evt.clientY;
    }
    else if (evt.type == 'MSGestureStart') {
        gestureStartX = evt.clientX;
        gestureStartY = evt.clientY;
    }
    else if (evt.type == 'MSGestureTap') {
    }
    else if (evt.type == 'MSGestureChange') {
        var translateY = evt.clientY - gestureStartY;
        if (translateY < -20 || translateY > 20) {
            return;
        }
        var translateX = evt.clientX - gestureStartX;
        if (translateX < -90) {
            gestureStartX = evt.clientX;
            window.external.notify('gestures:goforward');
            myGesture.stop();
        }
        else if (translateX > 90) {
            gestureStartX = evt.clientX;
            window.external.notify('gestures:goback');
            myGesture.stop();
        }
    }
    else if (evt.type == 'MSGestureEnd') {
        gestureStartX = evt.clientX;
        myGesture.reset();
    }
}