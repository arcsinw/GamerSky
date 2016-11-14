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

// 转换为数字
function intval(v) {
    v = parseInt(v);
    return isNaN(v) ? 0 : v;
}

// 获取元素信息
function getPos(e) {
    var l = 0;
    var t = 0;
    var w = intval(e.style.width);
    var h = intval(e.style.height);
    var wb = e.offsetWidth;
    var hb = e.offsetHeight;
    while (e.offsetParent) {
        l += e.offsetLeft + (e.currentStyle ? intval(e.currentStyle.borderLeftWidth) : 0);
        t += e.offsetTop + (e.currentStyle ? intval(e.currentStyle.borderTopWidth) : 0);
        e = e.offsetParent;
    }
    l += e.offsetLeft + (e.currentStyle ? intval(e.currentStyle.borderLeftWidth) : 0);
    t += e.offsetTop + (e.currentStyle ? intval(e.currentStyle.borderTopWidth) : 0);
    return { x: l, y: t, w: w, h: h, wb: wb, hb: hb };
}

// 获取滚动条信息
function getScroll() {
    var t, l, w, h;

    if (document.documentElement && document.documentElement.scrollTop) {
        t = document.documentElement.scrollTop;
        l = document.documentElement.scrollLeft;
        w = document.documentElement.scrollWidth;
        h = document.documentElement.scrollHeight;
    } else if (document.body) {
        t = document.body.scrollTop;
        l = document.body.scrollLeft;
        w = document.body.scrollWidth;
        h = document.body.scrollHeight;
    }
    return { t: t, l: l, w: w, h: h };
}

// 锚点(Anchor)间平滑跳转
function scroller(el, duration) {
    if (typeof el != 'object') { el = document.getElementById(el); }

    if (!el) return;

    var z = this;
    z.el = el;
    z.p = getPos(el);
    z.s = getScroll();
    z.clear = function () { window.clearInterval(z.timer); z.timer = null };
    z.t = (new Date).getTime();

    z.step = function () {
        var t = (new Date).getTime();
        var p = (t - z.t) / duration;
        if (t >= duration + z.t) {
            z.clear();
            window.setTimeout(function () { z.scroll(z.p.y, z.p.x) }, 13);
        } else {
            st = ((-Math.cos(p * Math.PI) / 2) + 0.5) * (z.p.y - z.s.t) + z.s.t;
            sl = ((-Math.cos(p * Math.PI) / 2) + 0.5) * (z.p.x - z.s.l) + z.s.l;
            z.scroll(st, sl);
        }
    };
    z.scroll = function (t, l) { window.scrollTo(l, t) };
    z.timer = window.setInterval(function () { z.step(); }, 13);
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
function InitGesture(body) {
    //var body = document.getElementsByTagName('body')[0];
    AddEvent(body, eventListener);

}

//为targetId指定的element添加手势事件
function AddEvent(target, eventListener) {
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
function eventListener(event) {
    var myGesture = event.gestureObject;

    if (event.type == 'MSGestureStart') {
        gestureStartX = event.clientX;
    }
        //else if(event.type == "MSInertialStart")
        //{
        //    SendNotify("MSInertialStart");
        //    translateX = event.clientX - gestureStartX;
        //    SendNotify("clientX " + clientX + "gestureStartX " + gestureStartX + "translateX "+translateX);
        //    if(event.velocityX > 50)
        //    {
        //        SendNotify("gestures : goforward");
        //    }
        //}
    else if (event.type == 'MSGestureChange') {
        if (gestureStartX == 'undefined') {
            gestureStartX = event.clientX;
            return;
        }
        //SendNotify("clientX " + event.clientX + " gestureStartX " + gestureStartX + " translateX " + translateX);
        translateX = event.clientX - gestureStartX;
        if (translateX < -100) {
            SendNotify('gestures : goforward');
            gestureStartX = event.clientX;
            lastGestureId = gestureId;
            myGesture.stop();
        }
        else if (translateX > 100) {
            //SendNotify('gestures : goback');
            gestureStartX = event.clientX;
            lastGestureId = gestureId;
            myGesture.stop();
        }
    }
    else if (event.type == 'MSGestureEnd') {
        //SendNotify("clientX " + clientX + "gestureStartX " + gestureStartX + "translateX " + translateX);
        gestureStartX = event.clientX;
        gestureId++;
        myGesture.reset();
    }
}
