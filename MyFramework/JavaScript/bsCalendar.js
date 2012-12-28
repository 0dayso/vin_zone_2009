function bsCalendarContain() {
	var CalendarBackDiv=document.createElement("DIV");
	CalendarBackDiv.id="bsCalendarBackDiv";
	CalendarBackDiv.style.visibility="hidden";
	CalendarBackDiv.style.position="absolute";
	CalendarBackDiv.zIndex=9998;
	CalendarBackDiv.style.backgroundColor="white";
     
	var CalendarBackFrame=document.createElement("IFRAME");
	CalendarBackFrame.style.position="absolute";
	CalendarBackFrame.id="bsCalendarBackFrame";
	CalendarBackDiv.appendChild(CalendarBackFrame);
    
	document.body.appendChild(CalendarBackDiv);
     
     
	CalendarDiv=document.createElement("DIV");
	CalendarDiv.id="bsCalendarDiv";
	CalendarDiv.style.visibility="hidden";
	CalendarDiv.style.position="absolute";
	CalendarDiv.zIndex=9999;
	CalendarDiv.style.backgroundColor="white";
    
	document.body.appendChild(CalendarDiv);
}
function bsCanlendar(ParamObj){
    this.calendarID = ParamObj.calendarID || "";
    this.userInputObj = document.getElementById(this.calendarID);
        
    if (!this.userInputObj) return;
    
    this.userInputObj.setAttribute("autocomplete", "off");
    this.userInputObj.readOnly=true;
        
    var today=new Date();
    this.currentDay=today.getDate();
    this.currentMonth=today.getMonth() + 1;
    this.currentYear=today.getFullYear();
    this.currentDate=this.currentYear +"-"+ ( this.currentMonth < 10 ? "0"+this.currentMonth : this.currentMonth ) +"-"+ ( this.currentDay < 10 ? "0"+this.currentDay : this.currentDay );
    
    this.calendarDay=this.currentDay;
    this.calendarYear=this.currentYear;
    this.calendarMonth=this.currentMonth;
    this.calendarDate=this.currentDate;
    
    this.lastCalendarDate=this.calendarDate;
    
		var CalendarDiv=document.getElementById("bsCalendarDiv");
		var CalendarBackDiv=document.getElementById("bsCalendarBackDiv");
		var CalendarBackFrame=document.getElementById("bsCalendarBackFrame");
		
		this.calendarTable=null;
		this.calendarTableBody=null;
		this.calendarYearSelect=null;
		this.calendarMonthSelect=null;
				    
    var CalendarFocusHandler = function(obj){
			return function(){
				obj.showCalendar();
			}
		}
    this.userInputObj.onfocus = CalendarFocusHandler(this);
}
bsCanlendar.prototype.setDefaltDate=function(defaultDate) {
	if (defaultDate!="") {
		var tempDate=new Date(defaultDate.split('-')[0], defaultDate.split('-')[1] - 1, defaultDate.split('-')[2].split(' ')[0]);
	}else{
		var tempDate	= new Date();
	}
		
	this.calendarYear		= tempDate.getFullYear();
	this.calendarMonth	= tempDate.getMonth() + 1;
	this.calendarDay		= tempDate.getDate();
	
	this.setCalendarDate();
	this.lastCalendarDate=this.calendarDate;
}
bsCanlendar.prototype.setCalendarDate=function() {
	this.calendarMonthDay=this.getMonthDay();
	this.calendarBeginWeekday=this.getBeginWeek();
	if (this.calendarDay > this.calendarMonthDay ) this.calendarDay = this.calendarMonthDay ;
	this.calendarDate=this.calendarYear +"-"+ ( this.calendarMonth < 10 ? "0"+this.calendarMonth : this.calendarMonth ) +"-"+ ( this.calendarDay < 10 ? "0"+this.calendarDay : this.calendarDay );
}
bsCanlendar.prototype.createCalendar=function() {		
		CalendarDiv.innerHTML="";
		
  	this.calendarTable=document.createElement("TABLE");
  	this.calendarTable.id=this.calendarID + "TABLE";
  	this.calendarTable.cellSpacing="0";
  	this.calendarTable.cellPadding="2";
  	this.calendarTable.rules="all";
  	this.calendarTable.borderColorLight="#aaaaaa";
  	this.calendarTable.borderColorDark="#ffffff";     	
  	this.calendarTable.style.border="1";
  	this.calendarTable.style.height="10";
  	this.calendarTable.style.fontSize="12px";
  	this.calendarTable.style.filter ="progid:DXImageTransform.Microsoft.Shadow(color=#999999,direction=135,strength=7)";
  	CalendarDiv.appendChild(this.calendarTable);
    this.calendarTableBody=document.createElement("TBODY");
    this.calendarTableBody.id=this.calendarID + "TBODY";
    this.calendarTable.appendChild(this.calendarTableBody);
	  var calendarTableRows=document.createElement("TR");
	  calendarTableRows.id=this.calendarID + "HEAD";
	  this.calendarTableBody.appendChild(calendarTableRows);
	  calendarTableRows.style.backgroundColor="#E2E5D7";
	  calendarTableRows.align="center";
	  
	  var calendarTableCells=document.createElement("TD");
	  calendarTableCells.id=this.calendarID + "HEAD_DECMONTH";
	  calendarTableRows.appendChild(calendarTableCells);
	  calendarTableCells.innerText="<";
	  calendarTableCells.style.width="13";
	  calendarTableCells.style.cursor="hand";
	  
	  var eventWrapper = function(obj){
			return function(){
	       obj.decCalendarMonth();
	    }
	  }
	  
	  calendarTableCells.onclick=eventWrapper(this);
	  
	  var calendarTableCells=document.createElement("TD");
	  calendarTableCells.id=this.calendarID + "HEAD_SELECTYEARMONTH";
	  calendarTableRows.appendChild(calendarTableCells);
	  calendarTableCells.colSpan=5;
	  
	  this.calendarYearSelect=document.createElement("SELECT");
	  this.calendarYearSelect.id=this.calendarID + "HEAD_SELECTYEAR";
	  calendarTableCells.appendChild(this.calendarYearSelect); 
	  this.calendarYearSelect.style.height=10;
	  this.calendarYearSelect.style.fontSize="12px";
	  
	  for( var i=1950; i<=2050; i++ ) {
	  	 var calendarYearOption=document.createElement("OPTION");
			 this.calendarYearSelect.appendChild(calendarYearOption);
			 calendarYearOption.value=i;
			 calendarYearOption.text=i;
			 if (i==this.calendarYear) calendarYearOption.selected=true;
	  }
	  
	  var eventWrapper = function(obj){
			return function(){
	       obj.setCalendarYear();
	    }
	  }
	  
	  this.calendarYearSelect.onchange=eventWrapper(this);
	  
	  this.calendarMonthSelect=document.createElement("SELECT");
	  this.calendarMonthSelect.id=this.calendarID + "HEAD_SELECTMONTH";
	  calendarTableCells.appendChild(this.calendarMonthSelect); 
	  this.calendarMonthSelect.style.height=10;
	  this.calendarMonthSelect.style.fontSize="12px";
	  
	  for( var i=1; i<=12; i++ ) {
	  	 var calendarMonthOption=document.createElement("OPTION");
			 this.calendarMonthSelect.appendChild(calendarMonthOption);
			 calendarMonthOption.value=i;
			 if (i<10) calendarMonthOption.text="0"+i;
			 else calendarMonthOption.text=i;
			 if (i==this.calendarMonth) calendarMonthOption.selected=true;
	  }
	  
	  var eventWrapper = function(obj){
			return function(){
	       obj.setCalendarMonth();
	    }
	  }
	  
	  this.calendarMonthSelect.onchange=eventWrapper(this);
	  
	  var calendarTableCells=document.createElement("TD"); 
	  calendarTableCells.id=this.calendarID + "HEAD_ADDMONTH";
	  calendarTableRows.appendChild(calendarTableCells);
	  calendarTableCells.innerText=">";
	  calendarTableCells.style.width="13";
	  calendarTableCells.style.cursor="hand";
	  
	  var eventWrapper = function(obj){
			return function(){
	       obj.addCalendarMonth();
	    }
	  }
	  
	  calendarTableCells.onclick=eventWrapper(this);
	  
	  var calendarTableRows=document.createElement("TR");
	  calendarTableRows.id=this.calendarID + "HEAD2";
	  this.calendarTableBody.appendChild(calendarTableRows);
	  calendarTableRows.style.backgroundColor="#E3EBDF";
	  calendarTableRows.align="center";
	  
	  var calendarWeekDesc=["日","一","二","三","四","五","六"];
	  
	  for (var i=0;i<7;i++) {
		 	var calendarTableCells=document.createElement("TD");
		 	calendarTableRows.appendChild(calendarTableCells);
		 	calendarTableCells.innerText=calendarWeekDesc[i];
		 	calendarTableCells.style.width="13";
	  }
}
bsCanlendar.prototype.fillCalendar = function() {    
	var curEle=document.getElementById(this.calendarTable.id);
	
	while (curEle.rows.length>2) {
		curEle.deleteRow(2);
	}
	this.calendarYearSelect.selectedIndex=this.calendarYear-1950;
	this.calendarMonthSelect.selectedIndex=this.calendarMonth-1;
	
	var eventWrapper = function(obj){
		return function(){
			obj.setCalendarDay();
	  }
	}	
	var tempDay=1;
	var tempRowIdx=0;
		
	while (tempDay <=this.calendarMonthDay ) {
		var calendarTableRows=document.createElement("TR");
		this.calendarTableBody.appendChild(calendarTableRows);
		calendarTableRows.style.backgroundColor="#ECEAF2";
		calendarTableRows.align="center";
		
  	for( var i=0; i<7; i++ ) {
	 		var calendarTableCells=document.createElement("TD");
			calendarTableRows.appendChild(calendarTableCells);
			calendarTableCells.align="center";
			
  		if ((this.calendarBeginWeekday==i || tempDay !=1) && tempDay <= this.calendarMonthDay) {
  			calendarTableCells.id=this.calendarID + "DAYSCELL" + tempDay ;
  			calendarTableCells.innerText=tempDay;
  			calendarTableCells.style.cursor="hand";
  			if( this.calendarYear == this.currentYear && this.calendarMonth == this.currentMonth && this.currentDay == tempDay ) {
  				calendarTableCells.style.color="red";
  			}else {
  				var listDate=this.calendarYear ;
  				listDate += "-" + ( this.calendarMonth < 10 ? "0" + this.calendarMonth : this.calendarMonth )	
  				listDate += "-" + ( tempDay < 10 ? "0" + tempDay : tempDay );
  				if (listDate==this.lastCalendarDate) calendarTableCells.style.color="blue";
  			}
		   	calendarTableCells.onclick=eventWrapper(this);
	   			
  			tempDay++;
  		}else{
  			calendarTableCells.innerText=" ";
  		}
  	}
  	tempRowIdx++;
  }
  
  var calendarTableRows=document.createElement("TR");
  calendarTableRows.id=this.calendarID + "FOOTER";
 	this.calendarTableBody.appendChild(calendarTableRows);
 	calendarTableRows.style.backgroundColor="#ECEAF2";
 	calendarTableRows.align="center";
  var calendarTableCells=document.createElement("TD");
  calendarTableCells.id=this.calendarID + "FOOTER_TODAY";
  calendarTableRows.appendChild(calendarTableCells);
  calendarTableCells.colSpan=5;
  calendarTableCells.style.cursor="hand";
  calendarTableCells.innerText="今天"+this.currentDate;
  
 	var eventWrapper = function(obj){
		return function(){
       obj.setCalendarToday();
    }
  }
 	
 	calendarTableCells.onclick=eventWrapper(this);
   
  var calendarTableCells=document.createElement("TD");
  calendarTableCells.id=this.calendarID + "FOOTER_CLEAR";
  calendarTableRows.appendChild(calendarTableCells);
  calendarTableCells.colSpan=2;
  calendarTableCells.style.cursor="hand";
  calendarTableCells.innerText="清空";
 	var eventWrapper = function(obj){
		return function(){
       obj.setCalendarBlank();
    }
  }
 	
 	calendarTableCells.onclick=eventWrapper(this);
 	
 	var curEle=document.getElementById("bsCalendarDiv");
	var curBackEle=document.getElementById("bsCalendarBackDiv");
	var curBackFrameEle=document.getElementById("bsCalendarBackFrame");
	
	var curInputEle=this.userInputObj;
 	
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
          
     curBackFrameEle.width=curBackEle.offsetWidth-2;
     curBackFrameEle.height=curBackEle.offsetHeight;
     
     curEle.style.visibility="visible";
  	 curBackEle.style.visibility="visible";    
 	
}
bsCanlendar.prototype.showCalendar = function() {
	this.setDefaltDate(this.userInputObj.value);
	
	this.createCalendar();
	this.fillCalendar();     
}
bsCanlendar.prototype.setCalendarYear=function() {
	var eSrc = window.event.srcElement;
	this.calendarYear=eSrc.options[eSrc.selectedIndex].value;
	this.setCalendarDate();
	this.fillCalendar();
}
bsCanlendar.prototype.setCalendarMonth=function() {
	var eSrc = window.event.srcElement;
	this.calendarMonth=eSrc.options[eSrc.selectedIndex].value;
	this.setCalendarDate();
	this.fillCalendar();
}
bsCanlendar.prototype.addCalendarMonth=function() {	
	if( this.calendarYear == 2050 && this.calendarMonth == 12 ) return;
	if( this.calendarMonth == 12 ){
		this.calendarYear ++;
		this.calendarMonth = 1;
	}else{
		this.calendarMonth ++;
	}
	
	this.setCalendarDate();
	this.fillCalendar();
}
bsCanlendar.prototype.decCalendarMonth=function() {
	if( this.calendarYear == 1950 && this.calendarMonth == 1 ) return;
	if( this.calendarMonth == 1 )	{
		this.calendarYear --;
		this.calendarMonth = 12;
	}else{
		this.calendarMonth --;
	}
		
	this.setCalendarDate();
	this.fillCalendar();	
}
bsCanlendar.prototype.setCalendarDay=function() {
	var eSrc = window.event.srcElement;
	this.calendarDay=eSrc.innerText;
	this.setCalendarDate();
	this.userInputObj.value=this.calendarDate;
	
	hideCalendar();
		
	if (this.calendarDate!=this.lastCalendarDate) this.userInputObj.fireEvent("onchange");	
  this.lastCalendarDate=this.calendarDate;
}
bsCanlendar.prototype.setCalendarToday=function() {
	this.calendarDay=this.currentDay;
	this.calendarYear=this.currentYear;
	this.calendarMonth=this.currentMonth;
	this.calendarMonthDay=this.getMonthDay();
	this.calendarBeginWeekday=this.getBeginWeek();
	this.calendarDate=this.currentDate;
	
	this.userInputObj.value=this.calendarDate;
	
	hideCalendar();
	
	if (this.calendarDate!=this.lastCalendarDate) this.userInputObj.fireEvent("onchange");	
  this.lastCalendarDate=this.calendarDate;  
}
bsCanlendar.prototype.setCalendarBlank=function() {
	this.userInputObj.value="";
	
	hideCalendar();
	if (this.lastCalendarDate!="") this.userInputObj.fireEvent("onchange");	
  this.lastCalendarDate="";  
}
bsCanlendar.prototype.getMonthDay=function(){
		var February	= 28;
		if( ( this.calendarYear % 400 == 0 ) || ( this.calendarYear % 4 == 0 && this.calendarYear % 100 != 0 ) ) February = 29;
		var MonthList	= new Array( 31, February, 31, 30, 31, 30, 31, 31, 30, 31, 30, 31 );		
		return MonthList[ this.calendarMonth - 1 ];
}
	
bsCanlendar.prototype.getBeginWeek=function(){				
	var NowDate = new Date( this.calendarYear, this.calendarMonth - 1, 1 );
	return NowDate.getDay();
}
function hideCalendar(){
	bsCalendarDiv.style.visibility="hidden";	
	bsCalendarBackDiv.style.visibility="hidden";
}
function listenDocumentClick() {
	var tmpEle	= window.event.srcElement;
	
	if (tmpEle.getAttribute("isCalendar")!="yes") {		
		var isCalendarEvent=false;
		while (tmpEle.parentElement.tagName.toUpperCase()!="HTML") {
			if (tmpEle.parentElement.id=="bsCalendarDiv") {
				isCalendarEvent=true;
				break;
			}
			tmpEle=tmpEle.parentElement;
		}
		
		if (!isCalendarEvent) hideCalendar();
	}
}
function bindCalendarObject() {
	var inputObjs = document.getElementsByTagName("input");
	bsCalendarContain();
	
	for(var i=0;i<inputObjs.length;i++){
		if (inputObjs[i].type == "text" && inputObjs[i].getAttribute("isCalendar")=="yes"){
			new bsCanlendar({calendarID: inputObjs[i].id});
		}
	}	
	document.attachEvent("onclick",listenDocumentClick); 
}
window.attachEvent("onload",bindCalendarObject);