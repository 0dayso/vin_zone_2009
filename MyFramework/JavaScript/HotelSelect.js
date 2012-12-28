/**
 * ��ַģ������
 * Author: Toby Lee
 * Email : tobylee@jsj.com.cn
 *
 * ʹ�÷���������csSetData(CityList)����������Դ����
 *           ����csInit('srcid', 'destid')����Ҫʹ�õĿؼ�
 *
 */
var _csOptionDiv = null;
var _csCityList = null;
var _csStyleOfDiv = ['#000', '#fff', '#333'];
var _csMovekey = 1;
var _csArrList = [];
var _csHint = "�������ƴ/ȫƴ/����";
var _csLeftCorrect = 0;
var _csTopCorrect = 0;
 var myHotelListShadow=document.createElement("DIV");
 // ���ò���������
 myHotelListShadow.style.visibility="hidden";
 myHotelListShadow.style.position="absolute";
 myHotelListShadow.zIndex=99999;
 myHotelListShadow.style.backgroundColor="white";
 // ����iframe
 var myHotelListShadowFrame=document.createElement("IFRAME");
 myHotelListShadowFrame.name="myHotelListIFrame";
 myHotelListShadow.appendChild(myHotelListShadowFrame);
 document.body.appendChild(myHotelListShadow);
 // ������
 var myHotelListData=document.createElement("DIV");
 myHotelListData.style.visibility="hidden";
 myHotelListData.zIndex=99999;
 myHotelListData.style.position="absolute";
 
 document.body.appendChild(myHotelListData);
/**
 * ��ʼ��������������ʼ��Ҫʹ�õ�ַģ������������Ҫ��������
 * �����һ�����û���ʾ��һ��������Ҫ�Ķ�Ӧֵ��
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
 * ��������������ʱִ��
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
 * �������뿪�����ִ��
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
 * ��������¼���������Ӧ�ĺ������
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
 * ���ñ������ݵ�����
 * Ŀǰֻ����һ�����͵���ά���� [['k1', 'k2', 'k3', 'v'],...]
 *
 */
function csSetData(tmp) {
	_csCityList = tmp;
}
/**
 * ����ָ�����������Ӧ�ı�����ֵ�Ŀؼ�
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
 * ������ʾ�����ֵ����ؼ��Ķ��ձ�
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
 * ����������ֵ��ƥ��Ķ�Ӧ��¼������ʾ��
 *
 */
function csSearchVal() {
	if(_csMovekey) return;
	if(_csCityList==null) {
		alert("������������Դ����");
	}
	objTmp = window.event.srcElement;
	try{
		objTarget = document.getElementById(csGetTarget(objTmp.id));
	}catch(e) {
		alert("Ŀ��Ԫ�ز�����");
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
		//_tmpBody += "<li name='NONE'>��ƥ����</li>";
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
 * ������ʾ�������������м�¼�Ĳ㣨DIV��
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
 * ���ݼ��̶�����װ���㶨λ����Ӧ��¼��
 * keyCodeֵ13�ǻس���38Ϊ�ϣ�40Ϊ�¡�
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
		alert("Ŀ��Ԫ�ز�����");
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
 * ����������ʾ�㣬����������ݡ�
 *
 */
function csHideDiv() {
	//_csMovekey = 0;
	_csOptionDiv.innerHTML = "";
	_csOptionDiv.style.display = "none";
}
/**
 * ��ȡ��Ӧ���ݼ��л�ý���ļ�¼������
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
 * ������궯����װ�����Ӧ����Ӧ��¼��
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
 * ������߽�
 *
 */
function csCorrectLeft(t) {
	_csLeftCorrect = t;
}
/**
 * �����ϱ߽�
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
