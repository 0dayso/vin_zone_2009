function GetFormControl(tsControlName) {
    var loForm = GetMainForm();
    if (loForm != null) return loForm[tsControlName];
    else return null;
}

function GetMainForm() {
    if (document.forms.length > 0) {
        return document.forms[0];
    } else {
        return null;
    }
}

function BoundHyperlink_onclick(toHyperlink, tsPostAction) {
    if (toHyperlink.PostBack == "1") {
        var loForm = GetMainForm();
        var lsOldPostAction = "";
        if (tsPostAction != null) {
            lsOldPostAction = loForm["MnfPostAction"].value;
            loForm["MnfPostAction"].value = tsPostAction;
        }
        loForm[toHyperlink.KeyQueryName].value = toHyperlink.KeyValue;
        NetPostBack("", "");
        if (tsPostAction != null) loForm["MnfPostAction"].value = lsOldPostAction;
    } else
        SubmitUrl(toHyperlink.href, tsPostAction);
    return false;
}

function SubmitUrl(tsUrl, tsPostAction) {
    var loForm = GetMainForm();
    var lsOldAction = loForm.action;
    var lsOldPostAction = "";
    if (tsPostAction != null) {
        lsOldPostAction = loForm["MnfPostAction"].value;
        loForm["MnfPostAction"].value = tsPostAction;
    }
    loForm.action = tsUrl;
    if (loForm.__VIEWSTATE != null) loForm.__VIEWSTATE.name = "___VIEWSTATE";
    if (loForm.__EVENTTARGET != null) loForm.__EVENTTARGET.name = "___EVENTTARGET";
    if (loForm.__EVENTARGUMENT != null) loForm.__EVENTARGUMENT.name = "___EVENTARGUMENT";
    loForm.submit();
    loForm.action = lsOldAction;
    if (tsPostAction != null) loForm["MnfPostAction"].value = lsOldPostAction;
    return true;
}

function PostBackForm(tsTarget, tsPostAction) {
    var loForm = GetMainForm();
    var lsOldTarget = loForm.target;
    var lsOldPostAction = "";
    if (tsPostAction != null) {
        lsOldPostAction = loForm["MnfPostAction"].value;
        loForm["MnfPostAction"].value = tsPostAction;
    }
    if (tsTarget != null && tsTarget != "") loForm.target = tsTarget;
    if (typeof (MnfOnSubmit) != "undefined") MnfOnSubmit();
    loForm.submit();
    loForm.target = lsOldTarget;
    if (tsPostAction != null) loForm["MnfPostAction"].value = lsOldPostAction;
}

function PostBackToParent() {
    var loParent = GetDialogParent();
    var lsOldParentName = loParent.name;
    loParent.name = "ParentWindow";
    PostBackForm("ParentWindow");
    loParent.name = lsOldParentName;
}


function PostBackSortAction(tsSortFields) {
    var loForm = GetMainForm();
    loForm.VicDataGrid_SortFields.value = tsSortFields;
    NetPostBack('', '');
}

function NetPostBack(eventTarget, eventArgument) {
    var theform;
    if (window.navigator.appName.toLowerCase().indexOf("microsoft") > -1) {
        theform = document.Form1;
    }
    else {
        theform = document.forms["Form1"];
    }

    if (theform == null) theform = GetMainForm();

    if (theform.__EVENTTARGET != null) theform.__EVENTTARGET.value = eventTarget.split("$").join(":");
    if (theform.__EVENTARGUMENT != null) theform.__EVENTARGUMENT.value = eventArgument;
    theform.submit();
}

function AppendQueryString(tsURL, tsQueryString) {
    var lsURL = new String(tsURL);
    var lnIndex = lsURL.indexOf("?");
    if (tsQueryString == "") return tsURL;
    if (lnIndex >= 0) {
        return tsURL + "&" + tsQueryString;
    } else {
        return tsURL + "?" + tsQueryString;
    }
}

function GetAppPath() {
    var loForm = GetMainForm();
    if (loForm.MNF_AppPath != null) return "/" + loForm.MNF_AppPath.value;
    return "";
}

function AppendAppPath(tsUrl) {
    if (tsUrl.substring(0, 1) == "/")
        return GetAppPath() + tsUrl;
    else
        return tsUrl;
}

function GetDialogParent() {
    return window.parent.opener;
}

function DialogArgu() {
    this.Action = "";
}

function OpenDialog(tsURL, toArgu, tsInputParameters, tnWidth, tnHeight, tnTop, tnLeft, tsStyle) {
    var lsStyle = "resizable:yes;status=no;dialogHeight:" + tnHeight + "px;dialogWidth:" + tnWidth + "px;";
    lsStyle = lsStyle + ";" + tsStyle;
    var loArgus = new Object();
    if (tsURL.indexOf("?") != -1)
        tsURL += "&RIDD=" + Math.random()
    else
        tsURL += "?RIDD=" + Math.random()
    loArgus.Url = tsURL;
    loArgus.Opener = window;
    loArgus.Argu = toArgu;
    loArgus.InputParameters = tsInputParameters;
    window.showModalDialog("/MyFramework/SystemFrame/DialogFrame.aspx", loArgus, lsStyle);
    return true;
}
function OpenWindow(tsURL, tsWindowName, tnWidth, tnHeight) {
    var sW = screen.width;
    var sH = screen.height;
    var wL = (sW - tnWidth) / 2;
    var wH = (sH - tnHeight) / 2;
    var Win = window.open(tsURL, tsWindowName, 'width=' + tnWidth + ',height=' + tnHeight + ',resizable=yes,scrollbars=yes,menubar=no,status=yes,left=' + wL + ',top=' + wH);
}
function CallHandlers(toHandlers) {
    for (var lnIndex = 0; lnIndex < toHandlers.length; lnIndex++) {
        if (lnIndex > 0) toHandlers[lnIndex]();
    }
    if (toHandlers.length > 0 && toHandlers[0] != null) toHandlers[0]();
}

function showsubmenu(sid) {

    var lsOldMenuId = document.getElementById("OldleftId");
    whichEl = eval("submenu" + sid);
    whichEl1 = eval("menuTitle" + sid);
    if (whichEl.style.display == "none") {
        var lsOldMenuId = document.getElementById("OldleftId");
        if (lsOldMenuId.value != "") {
            eval("submenu" + lsOldMenuId.value + ".style.display=\"none\";");
            eval("menuTitle" + lsOldMenuId.value + ".background=\"/MyFramework/Image/GLeftImage/crm_left.gif\";");

        }
        lsOldMenuId.value = sid;
        eval("submenu" + sid + ".style.display=\"\";");
        whichEl1.background = "/MyFramework/Image/GLeftImage/crm_left1.gif";
    }
    else {
        eval("submenu" + sid + ".style.display=\"none\";");
        whichEl1.background = "/MyFramework/Image/GLeftImage/crm_left.gif";
    }

}
function mouseover(sid) {
    whichEl = eval("menuTitle" + sid);
    whichEl.background = "/MyFramework/Image/TopImage/anniu_left_01.gif";

}
function mouseout(sid) {
    whichEl = eval("submenu" + sid);
    whichEl1 = eval("menuTitle" + sid);
    if (whichEl.style.display == "none") {
        whichEl1.background = "/MyFramework/Image/GLeftImage/crm_left.gif";
    }
    else
        whichEl1.background = "/MyFramework/Image/GLeftImage/crm_left1.gif";
}

//清空页面输入信息 
function InputBlank() {
    var InputObj = document.all.tags("input");
    var TextAreaObj = document.all.tags("textarea");

    for (var i = 0; i < InputObj.length; i++) {
        if (InputObj[i].type.toLowerCase() == "text") {
            InputObj[i].value = "";
        }
    }

    for (var j = 0; j < TextAreaObj.length; j++) {
        TextAreaObj[j].value = "";
    }

}


//得到当前对象的left和Top 值
function getObjectLeft(toObject) {
    var lnLeftValue = toObject.offsetLeft;
    while (toObject = toObject.offsetParent) {
        lnLeftValue += toObject.offsetLeft;
    }
    return lnLeftValue;
}
function getObjectTop(toObject) {
    var lnTopValue = toObject.offsetTop;
    while (toObject = toObject.offsetParent) {
        lnTopValue += toObject.offsetTop;
    }
    return lnTopValue;
}
//清除所有选项
function clearOptions(toList) {
    for (var lnIndex = toList.options.length - 1; lnIndex >= 0; lnIndex--) {
        toList.options[lnIndex] = null;
    }
}
//得到选择的选项
function getSelectedOption(toList) {
    for (var lnIndex = 0; lnIndex < toList.options.length; lnIndex++) {
        if (toList.options[lnIndex].selected) {
            return toList.options[lnIndex];
        }
    }
    return null;
}
function hidebar() {
    lsLeftMenuTableName = "";
    if (document.all["tbcrmleftmenu"] != undefined)
        lsLeftMenuTableName = "tbcrmleftmenu";
    else
        return false;
    if (document.all[lsLeftMenuTableName].style.display == "none") {
        document.all["igHideBarImpage"].src = '/MyFramework/Image/GLeftImage/framehide.gif';
        document.all[lsLeftMenuTableName].style.display = "block";
    } else {
        document.all[lsLeftMenuTableName].style.display = "none";
        document.all["igHideBarImpage"].src = '/MyFramework/Image/GLeftImage/frameshow.gif';
    }
    return true;
}

//window.history.forward(1);// 使点击落工具条上后退与前进按钮无效 

document.onkeydown = function ShieldBack() {
    if ((event.altKey) && ((event.keyCode == 37) || //屏蔽Alt+ 方向键  ←

        (event.keyCode == 39))) { //屏蔽Alt+ 方向键  →

        //alert("不准你使用ALT+方向键前进或后退网页！"); 

        event.returnValue = false;
    }

    if ((event.keyCode == 8) && (event.srcElement.type != "text" && event.srcElement.type != "textarea" && event.srcElement.type != "password")) {

        //屏蔽BackSpace键
        event.keyCode = 0;

        event.cancelBubble = true;

        event.returnValue = false;

    }
}

/* 四舍五入
* ForRound(Dight,How):数值格式化函数，Dight要 
* 格式化的数字，How要保留的小数位数。       
*/
function ForRound(Dight, How) {
    Dight = Math.round(Dight * Math.pow(10, How)) / Math.pow(10, How);
    return Dight;
}

// 处理window.onload 添加多个 函数
function addLoadEvent(func) {
    var oldonload = window.onload;
    if (typeof window.onload != "function") {
        window.onload = func;
    } else {
        window.onload = function () {
            oldonload();
            func();
        }
    }
}

// 设置窗口
function setPageHeight() {
    var winHeight = document.documentElement.clientHeight;
    var bodyHeight = document.body.clientHeight;
    var tableContent = document.getElementById("tableContent");
    tableContent.style.minHeight = bodyHeight + "px";
}

addLoadEvent(setPageHeight);