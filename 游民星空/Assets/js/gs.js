String.prototype.replaceAll = function (key, value) {
    var regExp = new RegExp(key, "gm");//g,全局;m,多次;i,大小写不敏感
    return this.replace(regExp, value);
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
function SendNotify(notifyString)
{
    window.external.notify(notifyString);
}

/* 手势操作 */
var gesture;
//手势操作开始坐标
var gestureStartX;

//触发Id,防止重复触发，触发Id与手势Id
var gestureId = 1;
var lastGestureId = 0;

//速度触发
var gestureVector = 1.5;

var translateX;
//手势操作初始化
function InitGesture(body)
{
    //var body = document.getElementsByTagName('body')[0];
    AddEvent(body,gestureListener);
	 
}

//为targetId指定的element添加手势事件
function AddEvent(target,eventListener)
{
    target.addEventListener("MSGestureStart", eventListener, false);
    target.addEventListener("MSGestureEnd", eventListener, false);
    target.addEventListener("MSGestureChange", eventListener, false);
    target.addEventListener("MSInertiaStart", eventListener, false);
    //target.addEventListener("MSGestureTap", eventListener, false);
    //target.addEventListener("MSGestureHold", eventListener, false);
    target.addEventListener("pointerdown", onPointDown, false);
    target.addEventListener("pointerup", onPointUp, false);

    gesture = new MSGesture();
    gesture.target = target;
}

function onPointUp(e) {
    //把触发时间参数传到gesture
    gesture.addPointer(e.pointerId);
}

function onPointDown(e) {
    //把触发时间参数传到gesture
    gesture.addPointer(e.pointerId);
}

//事件处理
function gestureListener(event) {
    var myGesture = event.gestureObject;
   
    if (event.type == 'MSGestureStart') {
        gestureStartX = event.clientX;
    }
    else if(event.type == "MSInertialStart")
    {
        translateX = event.clientX - gestureStartX;
        if(event.velocityX > 50)
        {
            SendNotify("gestures : goforward");
        }
    }
    else if (event.type == 'MSGestureChange') {
        if (gestureStartX == 'undefined') {
            //gestureStartX = event.clientX;
            return;
        }
        translateX = event.clientX - gestureStartX;
        if (translateX < -60) {
            SendNotify('gestures : goforward');
            gestureStartX = event.clientX;
            lastGestureId = gestureId;
            myGesture.stop();
        }
        else if (translateX > 60) {
            SendNotify('gestures : goback');
            gestureStartX = event.clientX;
            lastGestureId = gestureId;
            myGesture.stop();
        }
    }
    else if (event.type == 'MSGestureEnd') {
        gestureStartX = event.clientX;
        gestureId++;
        myGesture.reset();
    }
}
