Charset: UTF - 8
//////////////////////////////////////////////////////////////////////////////
// 手势识别
var myGesture;
var myADGesture;
var myRelatedGesture;
var myElement;
var myADElement;
var myRelatedElement;
var gestureStartX;

var isselecting = false;
document.onselectionchange = function () {
    if (document.getSelection().toString().length != 0) {
        isselecting = true;
    }
    else {
        isselecting = false;
    }
}

function gestureInit() {
    gesturePrepareTarget('body', gestureListener);
    //gesturePrepareTarget('adcontent', gestureListener);
    //gesturePrepareTarget('related', gestureListener);

    myGesture = new MSGesture();
    myElement = document.getElementById('body');
    myGesture.target = myElement;

    //myRelatedGesture = new MSGesture();
    //myRelatedElement = document.getElementById('related');
    //myRelatedGesture.target = myRelatedElement;
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