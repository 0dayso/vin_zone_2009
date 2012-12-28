/**
 * 地址模糊输入
 * Author: Toby Lee
 * Email : tobylee@jsj.com.cn
 *
 * 使用方法：先用csSetData(CityList)设置数据来源数组
 *           再用csInit('srcid', 'destid')设置要使用的控件
 *
 */
var _csOptionDiv = null;
var _csCityList = null;
var _csStyleOfDiv = ['#000', '#fff', '#333'];
var _csMovekey = 1;
var _csArrList = [];
var _csHint = "请输入简拼/全拼/汉字";
var _csLeftCorrect = 0;
var _csTopCorrect = 0;
 var myHotelListShadow=document.createElement("DIV");
 // 设置层的相关属性
 myHotelListShadow.style.visibility="hidden";
 myHotelListShadow.style.position="absolute";
 myHotelListShadow.zIndex=99999;
 myHotelListShadow.style.backgroundColor="white";
 // 生成iframe
 var myHotelListShadowFrame=document.createElement("IFRAME");
 myHotelListShadowFrame.name="myHotelListIFrame";
 myHotelListShadow.appendChild(myHotelListShadowFrame);
 document.body.appendChild(myHotelListShadow);
 // 创建层
 var myHotelListData=document.createElement("DIV");
 myHotelListData.style.visibility="hidden";
 myHotelListData.zIndex=99999;
 myHotelListData.style.position="absolute";
 
 document.body.appendChild(myHotelListData);
/**
 * 初始化函数，用来初始化要使用地址模糊输入的输入框，要求有两个
 * 输入框，一个向用户显示，一个保存需要的对应值。
 *
 */
function HotelComboxInit(eSrc) {
	try {
		var tmpTargetName = eSrc.name;
		var _hcHiddenInput=document.createElement("INPUT");
		_hcHiddenInput.type="hidden";
		_hcHiddenInput.name=tmpTargetName+"_ID";
		
		while (true) {
			var tmpParentObj = eSrc.parentElement;
			if (tmpParentObj.tagName.toUpperCase() == "FORM") break;
			if (tmpParentObj.tagName.toUpperCase() == "BODY") {
				throw 'error';
				break;
			}
			eSrc = tmpParentObj;
		}
		
		tmpParentForm.appendChild(_hcHiddenInput);
	}catch (e) {
		alert(e.message);
	}
}
/**
 * 当焦点进入输入框时执行
 *
 */
function HotelComboxRun(eSrc) {
	try {
		var tmpSourceName = eSrc.name;
		var tmpTargetName = tmpSourceName+"__ID";
		
		var tmpTargetObj=document.getElementById(tmpTargetName);
		
		if(tmpTargetObj == null || typeof(tmpTargetObj)!="object") {
			
			var _hcHiddenInput=document.createElement("INPUT");
			_hcHiddenInput.type="hidden";
			_hcHiddenInput.name=tmpTargetName+"_ID";
			
			while (true) {
				var tmpParentObj = eSrc.parentElement;
				if (tmpParentObj.tagName.toUpperCase() == "FORM") break;
				if (tmpParentObj.tagName.toUpperCase() == "BODY") {
					throw 'error';
					break;
				}
				eSrc = tmpParentObj;
			}
			
			tmpParentForm.appendChild(_hcHiddenInput);
		}
	}catch (e) {
		alert(e.message);
	}
}
/**
 * 当焦点离开输入框执行
 *
 */
function csOnBlur() {
	var tmp = window.event.srcElement;
	_csMovekey = 1;
	if(tmp.value=="") {
		tmp.value = _csHint;
	}else {
		csMoveFocus(13);
	}
	_csMovekey = 0;
	csHideDiv();
}
/**
 * 处理键盘事件，并向相应的函数输出
 *
 */
function keyValue() {
	switch(window.event.keyCode) {
		case 13:
		case 38:
		case 40:
			_csMovekey = 1;
			csMoveFocus(window.event.keyCode);
			break;
		default:
			_csMovekey = 0;
			break;
	}
	//alert(window.event.keyCode);
	return false;
}
/**
 * 设置保存数据的数组
 * 目前只处理一种类型的两维数组 [['k1', 'k2', 'k3', 'v'],...]
 *
 */
function csSetData(tmp) {
	_csCityList = tmp;
}
/**
 * 查找指定输入框所对应的保存其值的控件
 *
 */
function csGetTarget(id) {
	for(var i = 0; i < _csArrList.length; i++) {
		if(_csArrList[i][0]==id) {
			return _csArrList[i][1];
		}
	}
}
/**
 * 加入显示输入框及值保存控件的对照表
 *
 */
function csSetTarget(id, targetid) {
	for(var i = 0; i < _csArrList.length; i++) {
		if(_csArrList[i][0]==id) {
			return false;
		}
	}
	_csArrList.push([id, targetid]);
}
/**
 * 搜索与输入值相匹配的对应记录，并显示。
 *
 */
function csSearchVal() {
	if(_csMovekey) return;
	if(_csCityList==null) {
		alert("请设置数据来源数组");
	}
	objTmp = window.event.srcElement;
	try{
		objTarget = document.getElementById(csGetTarget(objTmp.id));
	}catch(e) {
		alert("目标元素不存在");
		return ;
	}
	if(objTarget==null) return;
	if(objTmp.value=="") {
		objTarget.value = "";
		_csOptionDiv.innerHTML = '';
		_csOptionDiv.style.display = "none";
		return;
	}
	_tmpBody = "<ul name='for"+objTmp.id+"'>";
	t = 0;
	for( var i = 0; i < _csCityList.length; i++ ) {
		var lastIdx = _csCityList[i].length - 1;
		for(var j = 0; j < lastIdx; j++) {
			if(objTmp.value.toUpperCase()==_csCityList[i][j].substr(0, objTmp.value.length).toUpperCase()) {
		/**
		if(objTmp.value==_csCityList[i][0].substr(0, objTmp.value.length) ||
				objTmp.value.toUpperCase()==_csCityList[i][1].substr(0, objTmp.value.length).toUpperCase() ||
				objTmp.value.toUpperCase()==_csCityList[i][2].substr(0, objTmp.value.length).toUpperCase()) {
		*/
				if(t==0) {
					objTarget.value = _csCityList[i][lastIdx];
					clsName = "csHover";
				}else {
					clsName = "csListStyle";
				}
				t++;
				_tmpBody += "<li class='"+clsName+"' name='"+_csCityList[i][lastIdx]+"' onmouseover='csOnMouseMove();'>"+_csCityList[i][0]+"</li>";
				break;
			}
		}
	}
	if(t==0) {
		//_tmpBody += "<li name='NONE'>无匹配项</li>";
	}
	_tmpBody += "</ul>";
	//_tmpBody += "<iframe frameborder=1 style='position:absolute; top:0px; left:0px; visibility:inherit;z-index:-1;background-color:#eeeeee' width=100% height=100%></iframe>";
	_csOptionDiv.innerHTML = _tmpBody;
	_csOptionDiv.style.width = objTmp.clientWidth+3;
	_csOptionDiv.style.left = csGetSrcLeft(objTmp) + _csLeftCorrect;
	_csOptionDiv.style.top = csGetSrcTop(objTmp)+objTmp.clientHeight + _csTopCorrect;
	if(t!=0) {
		_csOptionDiv.style.display = "";
	}
}
/**
 * 创建显示所搜索到的所有记录的层（DIV）
 *
 */
function csCreateDiv() {
	var div = document.createElement("div");
	div.id = "_csOptionDiv";
	div.style.position = "absolute";
	div.style.display = "none";
	div.style.left = "0px";
	div.style.top = "0px";
	div.style.backgroundColor = _csStyleOfDiv[1];
	div.style.color = _csStyleOfDiv[0];
	div.style.border = _csStyleOfDiv[2] + " 1px solid";
	_csOptionDiv = div;
}
/**
 * 根据键盘动作，装焦点定位到相应记录。
 * keyCode值13是回车，38为上，40为下。
 *
 */
function csMoveFocus(t) {
	if(_csOptionDiv.style.display=="none") {
		return ;
	}
	var lists = _csOptionDiv.childNodes[0].childNodes;
	var idx = csGetFocusOption(lists);
	objTmp = window.event.srcElement;
	try{
		objTarget = document.getElementById(csGetTarget(objTmp.id));
	}catch(e) {
		alert("目标元素不存在");
		return ;
	}
	for(var i = 0; i < lists.length; i++) {
		lists[i].className = "";
	}
	switch(t) {
		case 13:
			if(lists.length==1&&lists[0].name=="NONE") {
				objTarget.value=="";
			}else {
				if(idx==-1) {
					newidx = 0;
				}
				newidx = idx;
				objTarget.value = lists[newidx].name;
				objTmp.value = lists[newidx].innerText;
			}
			csHideDiv();
			break;
		case 38:
			if(lists.length==1&&lists[0].name=="NONE") {
				return;
			}else {
				if(idx==-1) {
					newidx = lists.length-1;
				}else {
					if(idx==0) {
						newidx = lists.length - 1;
					}else {
						newidx = idx - 1;
					}
				}
				objTarget.value = lists[newidx].name;
				objTmp.value = lists[newidx].innerText;
				lists[newidx].className = "csHover";
			}
			break;
		case 40:
			if(lists.length==1&&lists[0].name=="NONE") {
				return;
			}else {
				if(idx==-1) {
					newidx = 0;
				}else {
					if(idx==lists.length-1) {
						newidx = 0;
					}else {
						newidx = idx+1;
					}
				}
				objTarget.value = lists[newidx].name;
				objTmp.value = lists[newidx].innerText;
				lists[newidx].className = "csHover";
			}
			break;
	}
}
/**
 * 隐藏数据显示层，并清空其内容。
 *
 */
function csHideDiv() {
	//_csMovekey = 0;
	_csOptionDiv.innerHTML = "";
	_csOptionDiv.style.display = "none";
}
/**
 * 获取对应数据集中获得焦点的记录的索引
 *
 */
function csGetFocusOption(lists) {
	for(var i = 0; i < lists.length; i++) {
		if(lists[i].className=="csHover") {
			return i;
		}
	}
	return -1;
}
/**
 * 根据鼠标动作，装焦点对应到相应记录。
 *
 */
function csOnMouseMove() {
	_csMovekey = 1;
	var objList = window.event.srcElement;
	var objLists = window.event.srcElement.parentElement;
	var objTmp = document.getElementById(objLists.name.substr(3));
	var objTarget = document.getElementById(csGetTarget(objTmp.id));
	var lists = objLists.childNodes;
	for(var i = 0; i < lists.length; i++) {
		lists[i].className = "";
	}
	objList.className = "csHover";
	objTarget.value = objList.name;
	objTmp.value = objList.innerText;
	return;
}
/**
function csGetSrcLeft(obj) {
	var ileft = 0;
	while(obj) {
		ileft += obj.offsetLeft;
		obj = obj.parentElement;
	}
	return ileft;
}
function csGetSrcTop(obj) {
	var itop = 0;
	while(obj) {
		itop += obj.offsetTop;
		obj = obj.parentElement;
	}
	return itop;
}
*/
function csGetSrcLeft(obj)
{
	var l=obj.offsetLeft;
	while(obj=obj.offsetParent){
		l+=obj.offsetLeft;
	}
	return l;
}
function csGetSrcTop(obj)
{
	var t=obj.offsetTop;
	while(obj=obj.offsetParent){
		t+=obj.offsetTop;
	}
	return t;
}
/**
 * 修正左边界
 *
 */
function csCorrectLeft(t) {
	_csLeftCorrect = t;
}
/**
 * 修正上边界
 *
 */
function csCorrectTop(t) {
	_csTopCorrect = t;
}
function csSetHint(tmp) {
	if(tmp!="") {
		_csHint = tmp;
	}
}
