
function bsGCComboxContain() {
    //新建一个DIV元素
	var GCComboxBackDiv=document.createElement("DIV");
	GCComboxBackDiv.id="bsGCComboxBackDiv";
	GCComboxBackDiv.style.visibility="hidden";
	GCComboxBackDiv.style.position="absolute";
	GCComboxBackDiv.zIndex=999999;
	GCComboxBackDiv.style.backgroundColor="white";
    //新建一个IFAME,放在DIV中
	var GCComboxBackFrame=document.createElement("IFRAME");
	GCComboxBackFrame.style.position="absolute";
	GCComboxBackFrame.id="bsGCComboxBackFrame";
	GCComboxBackDiv.appendChild(GCComboxBackFrame);
    
	document.body.appendChild(GCComboxBackDiv);
     
	GCComboxDiv=document.createElement("DIV");
	GCComboxDiv.id="bsGCComboxDiv";
	GCComboxDiv.style.visibility="hidden";
	GCComboxDiv.style.position="absolute";
	GCComboxDiv.style.borderRight="black 1px solid";
	GCComboxDiv.style.borderLeft="black 1px solid";
	GCComboxDiv.style.borderTop="black 1px solid";
	GCComboxDiv.style.borderBottom="black 1px solid";
	GCComboxDiv.style.overflowY="auto";
	GCComboxDiv.style.overflowX="hidden";
	GCComboxDiv.zIndex=999998;
	GCComboxDiv.style.backgroundColor="white";
 	
	document.body.appendChild(GCComboxDiv);
}
function bsGCCombox(ParamObj){
    this.gccomboxID = ParamObj.gccomboxID || "";
    this.userInputObj = document.getElementById(this.gccomboxID);
    this.hiddenInputObj = document.getElementById(this.gccomboxID+"_ComboxSelect");
    
    this.postUrl=ParamObj.gcpostUrl;
    
    this.bindObj=ParamObj.gcrelationObj;
    
    this.bindEvent=ParamObj.gcevent;
    
    this.forceSelect=ParamObj.gcforceSelect;
    
    this.forbiddenEvent=false;
    
    if (!this.userInputObj) return;
    
    this.userInputObj.setAttribute("autocomplete", "off");
    
    this.gcXML=null;
    this.gcList=null;
    
    this.lastSelCell=null;
    	
    this.gcComboxTable=null;
	this.gcComboxTBody=null;
	
	this.gcComboxWidth=this.userInputObj.offsetWidth;
	
	this.getTimer=null;
				    
    var GCComboxFocusHandler = function(obj){
			return function(){
				obj.showGCCombox();
			}
		}
    this.userInputObj.onfocus = GCComboxFocusHandler(this);
    
    var GCComboxBlurHandler = function(obj){
			return function(){
				obj.blurInputObj();
			}
		}
    this.userInputObj.onblur = GCComboxBlurHandler(this);
    
    var GCComboxChangeHandler = function(obj){
			return function(){
				obj.changeInputObj();
			}
		}
    this.userInputObj.onpropertychange = GCComboxChangeHandler(this);
    
    if (this.userInputObj.value!="") 
    {
        this.gcXML=this.getGCXML();
        this.gcList=this.gcXML.getElementsByTagName("resultlist")[0].childNodes;
        if(this.gcList[0]!=null)
            this.hiddenInputObj.value=this.gcList[0].getAttribute('id');
    }
    
    /*
    var GCComboxChangeHandler = function(obj){
		return function(){
		    if (window.event.propertyName!="value") {
		        window.clearTimeout(obj.getTimer);
		    }else{
			    if (obj.getTimer!=null) window.clearTimeout(obj.getTimer);
			    obj.getTimer=window.setTimeout( function watchGetList() {
			        obj.changeInputObj();
			        obj.getTimer=null;
			        },300);
			}
		}
	}
    
    this.userInputObj.onkeyup=GCComboxChangeHandler(this);
    this.userInputObj.onpropertychange = GCComboxChangeHandler(this);
    */
}
bsGCCombox.prototype.getGCXML = function() {
	var xmlFile=this.postUrl;
	
    var urlvalue=this.userInputObj.value;
    urlvalue=urlvalue.replace("+","%2B");
	xmlFile=xmlFile.replace("@0",urlvalue);
	
	
	if (this.bindObj!=null) {
		var paraObjIDs=this.bindObj.split(",");
		
		
		var paraObj=null;
		var paraValue=null;
		var paraFlow=1;
		
		for (var i=0;i<paraObjIDs.length;i++) {
			paraObj=document.getElementById(paraObjIDs[i]);
			if (typeof(paraObj)=="object") {
				 if(paraObj.tagName.toUpperCase()=="INPUT") paraValue=paraObj.value;
				 else if (paraObj.tagName.toUpperCase()=="SELECT") paraValue=paraObj.options[paraObj.selectedIndex].value;
				 
				 xmlFile=xmlFile.replace("@"+paraFlow,paraValue);
			}
			paraFlow++;
		}
	}
	
	var xmlDoc;
	try {
		if(window.ActiveXObject) {
			xmlDoc=new ActiveXObject('Microsoft.XMLDOM');
			xmlDoc.async=false;
			xmlDoc.load(xmlFile);
		}else if (document.implementation&&document.implementation.createDocument){
			xmlDoc=document.implementation.createDocument('', '', null);
			xmlDoc.load(xmlFile);
		}else{
			return null;
		}
		
		if (xmlDoc.parseError.errorCode != 0) return null;
		
		xmlDoc.setProperty("SelectionLanguage", "XPath");
	
		return xmlDoc;
	}catch(ee){
		return null;
	}
}

bsGCCombox.prototype.createGCCombox=function() {
	var ComboxDiv=document.getElementById("bsGCComboxDiv");
	
	ComboxDiv.innerHTML="";
	//ComboxDiv.style.width=600;
	ComboxDiv.style.height=0;
	
	/*
	if (this.userInputObj.value.length<2) {
		hideGCCombox();
		return false;
	}
	*/
	if (this.userInputObj.value=="") {
	    this.gcXML=null;
	    this.gcList=null;
	} else {	
	    this.gcXML=this.getGCXML();
	}
	
	if (!this.gcXML) {
	    this.gcList=null;
	    this.hiddenInputObj.value="";
		hideGCCombox();
		return false;
	}
	
	this.gcList=this.gcXML.getElementsByTagName("resultlist")[0].childNodes;
	
	
	if (this.gcList.length==0) {
	    this.gcList=null;
	    this.hiddenInputObj.value="";
		hideGCCombox();
		return false;
	}
	
	this.gcComboxTable=document.createElement("TABLE");
	
 	this.gcComboxTable.id=this.gccomboxID + "TABLE";
 
 	//this.gcComboxTable.width="100%";
 	
 	ComboxDiv.appendChild(this.gcComboxTable);
 	this.gcComboxTable.style.fontSize="9pt";
	
	this.gcComboxTBody=document.createElement("TBODY");
  this.gcComboxTBody.id=this.gccomboxID + "TBODY";
  this.gcComboxTable.appendChild(this.gcComboxTBody);
  
	var eventClickWrapper = function(obj){
		return function(){
			obj.selectGCByMouse();
	  }
	}
	
	var eventMouseOverWrapper = function(obj) {
		return function() {
			obj.highlightGC();
		}
	}
	
	var eventMouseOutWrapper = function(obj) {
		return function() {
			obj.lowlightGC();
		}
	}
	
	
				
	for (var i=0;i<this.gcList.length;i++) {
		var comboxTableRow=document.createElement("TR");
		var comboxTableCell=document.createElement("TD");
		comboxTableRow.appendChild(comboxTableCell);
		
		comboxTableCell.id=this.gccomboxID + "GCSCELL" + i;
		comboxTableCell.style.whiteSpace="nowrap";
	    comboxTableCell.innerHTML=this.gcList[i].getAttribute('name');
	    		
		comboxTableCell.setAttribute("gcid",this.gcList[i].getAttribute('id'));
		comboxTableCell.setAttribute("gcname",this.gcList[i].getAttribute('name'));
		comboxTableCell.style.cursor="hand";	
		//this.gcList[i].getAttribute('name')
		comboxTableCell.onclick=eventClickWrapper(this);
		comboxTableCell.onmouseover=eventMouseOverWrapper(this);
		comboxTableCell.onmouseout=eventMouseOutWrapper(this);
	    this.gcComboxTBody.appendChild(comboxTableRow);
	  
		//comboxTableRow.style.position="absolute";
       
  }
  
  ComboxDiv.style.height=this.gcComboxTable.offsetHeight+5;
  ComboxDiv.style.width=this.gcComboxTable.offsetWidth+2;
  
  return true;
}
bsGCCombox.prototype.showGCCombox = function() {
	if (!this.createGCCombox()) return;
	
	var curEle=document.getElementById("bsGCComboxDiv");
	var curBackEle=document.getElementById("bsGCComboxBackDiv");
	var curBackFrameEle=document.getElementById("bsGCComboxBackFrame");
	
	var curInputEle=this.userInputObj;
	
	curEle.style.width = this.gcComboxTable.offsetWidth+5;
	if (curEle.offsetWidth<this.userInputObj.offsetWidth) curEle.style.width=this.userInputObj.offsetWidth+2;
	if (curEle.offsetWidth<100) curEle.style.width=100;
	
	if (curEle.offsetHeight>300) curEle.style.height=300;
	
	//curEle.style.width=this.userInputObj.offsetWidth;
		
    var MoveTimeTop		= this.userInputObj.offsetTop + this.userInputObj.offsetHeight;
	var MoveTimeLeft	= this.userInputObj.offsetLeft;
	while ( curInputEle = curInputEle.offsetParent ) {
		if( this.userInputObj.tagName.toLowerCase() == "fieldset" ) {
	        MoveTimeTop  += 8;
	        MoveTimeLeft += 10;
	   }
	
		MoveTimeTop		+= curInputEle.offsetTop; 
		MoveTimeLeft	+= curInputEle.offsetLeft;
	}
	
	curEle.style.top		= MoveTimeTop + 1;
	curEle.style.left		= MoveTimeLeft;
  if( parseInt( curEle.style.top ) + parseInt( curEle.offsetHeight ) 
  		> parseInt( document.body.clientHeight ) + parseInt( document.body.scrollTop ) )
  {
     curEle.style.top = parseInt( curEle.style.top ) - parseInt( curEle.offsetHeight ) - 15;
  }	
  
  if( parseInt( curEle.style.left ) + parseInt( curEle.offsetWidth ) 
  	> parseInt( document.body.clientWidth ) + parseInt( document.body.scrollLeft ) )
  {
      curEle.style.left = parseInt( curEle.style.left ) - parseInt( curEle.offsetWidth ) + 75;
  }

  curBackEle.style.top = curEle.style.top;
  curBackEle.style.left = curEle.style.left;
  curBackEle.style.width = curEle.offsetWidth;
  
  curBackEle.style.height = curEle.offsetHeight;
    
  curBackFrameEle.style.width=curBackEle.offsetWidth-2;
  
  curBackFrameEle.style.height=curBackEle.offsetHeight;
  
  curEle.style.visibility="visible";
  curBackEle.style.visibility="visible";
     
}
bsGCCombox.prototype.changeInputObj=function() {
	if (event.propertyName=="value" && !this.forbiddenEvent) {
		this.showGCCombox();
		//this.hiddenInputObj.value="";
		//if (this.bindEvent!=null) eval(this.bindEvent);
	}else{
	    return;
	}
}
bsGCCombox.prototype.blurInputObj=function() {
	var tmpEle = document.activeElement;
	var isGCComboxEvent=false;
	while (tmpEle.tagName.toUpperCase()!="HTML") {
		if (tmpEle.id=="bsGCComboxDiv") {
			isGCComboxEvent=true;
			break;
		}
		tmpEle=tmpEle.parentElement;
	}
    
    window.event.cancelBubble=true;
    
	if (!isGCComboxEvent) {
	    this.forbiddenEvent=true;	    
	    if (this.forceSelect==1 && (this.hiddenInputObj.value==null || this.hiddenInputObj.value=="")) {	        
	    	if (this.gcList!=null && this.gcList.length>0) {
	    	    this.hiddenInputObj.value=this.gcList[0].getAttribute('id');
	      	    this.userInputObj.value=this.gcList[0].getAttribute('name');		    	
		    }else{
		    	this.userInputObj.value="";
		    	this.hiddenInputObj.value="";
		    }
		}
				
	    hideGCCombox();
	}
	this.forbiddenEvent=false;
}
bsGCCombox.prototype.selectGCByMouse=function() {
	var eSrc = window.event.srcElement;
	window.event.cancelBubble=true;
	
	this.hiddenInputObj.value=eSrc.getAttribute("gcid");
	this.userInputObj.value=eSrc.innerText;
	
	hideGCCombox();
	if (this.bindEvent!=null) eval(this.bindEvent);
	return;
}
bsGCCombox.prototype.highlightGC=function() {
	var eSrc = window.event.srcElement;
	
	var gcList=this.gcComboxTBody.childNodes;
	
	for (var i=0;i<gcList.length;i++) {
		if (gcList[i].childNodes[0].style.backgroundColor=="highlight") {
			gcList[i].childNodes[0].style.backgroundColor="white";
			gcList[i].childNodes[0].style.color="black";
			break;
		}
	}
	
	eSrc.style.color="white";
	eSrc.style.backgroundColor="highlight";
	this.lastSelCell=eSrc;
	return;
}
bsGCCombox.prototype.lowlightGC=function() {
	var eSrc = window.event.srcElement;
	eSrc.style.color="black";
	eSrc.style.backgroundColor="white";
	return;
}
selectGCByKey=function() {
	var ComboxDiv=document.getElementById("bsGCComboxDiv");
	if (ComboxDiv.style.visibility=="hidden") return;
	
	var inputObjID=ComboxDiv.childNodes[0].id.substring(0,ComboxDiv.childNodes[0].id.indexOf("TABLE"));
	var inputObj=document.getElementById(inputObjID);
	var hiddenInputObj=document.getElementById(inputObjID+"_ComboxSelect");
	
	var gcList = ComboxDiv.childNodes[0].childNodes[0].childNodes;
	var lastSelCell=null;
	var lastSelRow=null;
	var lastIdx=0;
	
	var targetSelCell=null;
	var targetSelRow=null;
	var targetIdx=0;
	for (var i=0;i<gcList.length;i++) {
		if (gcList[i].childNodes[0].style.backgroundColor=="highlight") {
			lastSelRow=gcList[i];
			lastSelCell=gcList[i].childNodes[0];
			lastIdx=i;
			
			break;
		}
	}
	
	if (window.event.keyCode==13) {
			if (lastSelCell == null){
				lastSelCell=gcList[0].childNodes[0];
			}
			
			hiddenInputObj.value=lastSelCell.getAttribute("gcid");
			inputObj.value=lastSelCell.innerText;
			
			hideGCCombox();
			
			var bindParam=inputObj.getAttribute("bindParam");
			
			var bindParamArr=bindParam.split("|");
				
			if (bindParamArr.length>2 && bindParamArr[2]!="") {
				var bindEvent=bindParamArr[2];						
				if (bindEvent!=null) eval(bindEvent);
			}
						
			window.event.keyCode=0;
			window.event.cancelBubble=true;
	}
	return;
}
lightGCByKey=function() {
	var ComboxDiv=document.getElementById("bsGCComboxDiv");
	if (ComboxDiv.style.visibility=="hidden") return;
	
	var inputObjID=ComboxDiv.childNodes[0].id.substring(0,ComboxDiv.childNodes[0].id.indexOf("TABLE"));
	var inputObj=document.getElementById(inputObjID);
	
	var gcTable=ComboxDiv.childNodes[0];
	var gcList = ComboxDiv.childNodes[0].childNodes[0].childNodes;
	var lastSelCell=null;
	var lastSelRow=null;
	var lastIdx=0;
	
	var targetSelCell=null;
	var targetSelRow=null;
	var targetIdx=0;
	for (var i=0;i<gcList.length;i++) {
		if (gcList[i].childNodes[0].style.backgroundColor=="highlight") {
			lastSelRow=gcList[i];
			lastSelCell=gcList[i].childNodes[0];
			lastIdx=i;
			
			break;
		}
	}
		
	switch(window.event.keyCode) {
		case 38:
			if (lastSelCell == null) {
				targetIdx=gcList.length-1;
			}else{
				targetIdx=lastIdx-1;
				lastSelCell.style.color="black";
				lastSelCell.style.backgroundColor="white";				
			}
			if (targetIdx<0) targetIdx=gcList.length-1;
			var targetCell=gcList[targetIdx].childNodes[0];
			var targetRow=gcList[targetIdx];
			
			targetCell.style.color="white";
			targetCell.style.backgroundColor="highlight";
			targetCell.scrollIntoView(false);
			break;
			
		case 40:
			
			if (lastSelCell == null) {
				targetIdx=0;
			}else{
				targetIdx=lastIdx+1;
				if (targetIdx==gcList.length) targetIdx=0;
				
				lastSelCell.style.color="black";
				lastSelCell.style.backgroundColor="white";				
			}
			
			var targetCell=gcList[targetIdx].childNodes[0];
			var targetRow=gcList[targetIdx];
						
			targetCell.style.color="white";
			targetCell.style.backgroundColor="highlight";
			
			if ((targetCell.offsetHeight+2)*targetIdx-ComboxDiv.scrollTop>=260)	targetCell.scrollIntoView(false);
			break;
	}
	return;
}
function hideGCCombox(){
	bsGCComboxDiv.style.visibility="hidden";	
	bsGCComboxBackDiv.style.visibility="hidden";
}
function bindGCComboxObject() {
	var inputObjs = document.getElementsByTagName("input");
	bsGCComboxContain();
	
	for(var i=0;i<inputObjs.length;i++){
		if (inputObjs[i].type == "text" && inputObjs[i].getAttribute("isGCCombox")=="yes"){
			var tmpEle = inputObjs[i];
			var formObj=null;
			var formObjReadOnly=0;
			
			while (tmpEle.tagName.toUpperCase()!="HTML") {
				if (tmpEle.tagName.toUpperCase()=="FORM") {
					formObj=tmpEle;
					break;
				}
				tmpEle=tmpEle.parentElement;
			}
			
			var bindParam=inputObjs[i].getAttribute("bindParam");
			
			var bindParamArr=bindParam.split("|");
			
			var bindPostUrl=bindParamArr[0];
			
			if (bindParamArr.length>1 && bindParamArr[1]!="") var bindRelationObj=bindParamArr[1];
			else var bindRelationObj=null;
				
			if (bindParamArr.length>2 && bindParamArr[2]!="") var bindEvent=bindParamArr[2];
			else var bindEvent=null;
				
			if (formObj!=null) {
				var hideInputObj=document.createElement("INPUT");
				hideInputObj.type="hidden";			
				hideInputObj.id=inputObjs[i].id+"_ComboxSelect";
				hideInputObj.name=inputObjs[i].name+"_ComboxSelect";
				
				formObj.appendChild(hideInputObj);
				/*
				if (inputObjs[i].getAttribute("defaultSelect")!=null && inputObjs[i].getAttribute("defaultSelect")!="") {
					hideInputObj.value=inputObjs[i].getAttribute("defaultSelect");
					if (bindEvent!=null) eval(bindEvent);
				}
				*/
				
				if (inputObjs[i].getAttribute("forceSelect")=="1") {
				    formObjReadOnly=1;
				}
				
			}
						
			new bsGCCombox({gccomboxID: inputObjs[i].id,gcpostUrl: bindPostUrl,gcrelationObj: bindRelationObj,gcevent:bindEvent,gcforceSelect:formObjReadOnly});
			
			
		}
	}
}
window.attachEvent("onload",bindGCComboxObject);
document.attachEvent("onkeypress",selectGCByKey);
document.attachEvent("onkeyup",lightGCByKey);
