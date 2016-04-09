
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
}

function NightMode() {
    var htmlTag = document.getElementsByTagName("html")[0];
    SetElementClass(htmlTag, "PageColorMode_Day", "PageColorMode_Night");
}
