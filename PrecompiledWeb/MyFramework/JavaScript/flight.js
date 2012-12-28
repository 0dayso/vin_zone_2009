// JScript 文件
//机票查询用到的函数

//日期前后天
function S_onclick(bid)
{
 var e = window.event.srcElement; 
 var f=e.value;
 
 var IsAdd=0;
 if (f=="<")
 {
    IsAdd=-1
 }
 if (f==">")
 {
    IsAdd=1
 }
   var d= document.getElementById(bid);
   var d1=d.value;
   var darray=d1.split("-");
   var d2=darray[1]+"/"+darray[2]+"/"+darray[0];
   var now=new Date(d2);
   now.setDate(now.getDate()+IsAdd);
   var nformat = now.getFullYear()+"-"+(now.getMonth()+1)+"-"+now.getDate();
   nformat = nformat.replace(/\b(\w)\b/g,"0$1");//给小于10的数字前面加0
   d.value=nformat;
}
function GetToday()
{
    var today = new Date();
    var nformat = today.getFullYear()+"-"+(today.getMonth()+1)+"-"+today.getDate();
   nformat = nformat.replace(/\b(\w)\b/g,"0$1");//给小于10的数字前面加0
   document.getElementById("tFlydate").value=nformat;
}
function GetTomorrow()
{
    var today = new Date();
    today.setDate(today.getDate()+1);
    var nformat = today.getFullYear()+"-"+(today.getMonth()+1)+"-"+ today.getDate() ;
   nformat = nformat.replace(/\b(\w)\b/g,"0$1");//给小于10的数字前面加0
   document.getElementById("tFlydate").value=nformat;
}
function GetAfterTomorrow()
{
    var today = new Date();
    today.setDate(today.getDate()+2);
    var nformat = today.getFullYear()+"-"+(today.getMonth()+1)+"-"+ today.getDate() ;
   nformat = nformat.replace(/\b(\w)\b/g,"0$1");//给小于10的数字前面加0
   document.getElementById("tFlydate").value=nformat;
}
//城市输入
function SetTarget() {
 var e = window.event.srcElement; 
 var f=e.id;
 var temp=e.value.toUpperCase();
 var  t;
   
	if (f=="tSrc")
	{
	  t= document.getElementById("ddlSrc");	
		
		
		 for (var i=0;i<t.options.length;i++) {
			 
				if (t.options[i].text.substring(0,temp.length)==temp || t.options[i].value==temp)
				{
					t.selectedIndex=i;
					break;
				}
			}
		
	}
	
	if (f=="tDest")
	{
	   t= document.getElementById("ddlDest");
	   for (var i=0;i<t.options.length;i++) {
			 
				if (t.options[i].text.substring(0,temp.length)==temp || t.options[i].value==temp)
				{
					t.selectedIndex=i;
					break;
				}
			}
		
	}
	
	if (f=="tZcity")
	{
	   t= document.getElementById("ddlZcity");	
	   for (var i=0;i<t.options.length;i++) {
				if (t.options[i].text.substring(0,temp.length)==temp || t.options[i].value==temp)
				{
					t.selectedIndex=i;
					break;
				}
			}
	}
	
		
	
}
//城市输入
function MWSetTarget(inputValue,tarElement) 
{
    var temp=inputValue.toUpperCase();
    for (var i=0;i<tarElement.options.length;i++) 
    {
    			 
	    if (tarElement.options[i].text.substring(0,temp.length)==temp || tarElement.options[i].value==temp)
	    {
		    tarElement.selectedIndex=i;
		    break;
	    }
    }
}
function MGetToday()
{
    var today = new Date();
    var nformat = today.getFullYear()+"-"+(today.getMonth()+1)+"-"+today.getDate();
   nformat = nformat.replace(/\b(\w)\b/g,"0$1");//给小于10的数字前面加0
   document.getElementById("txtDeparDate").value=nformat;
   document.getElementById("txtTransDate").value=nformat;   
}
function MGetTomorrow()
{
    var today = new Date();
    today.setDate(today.getDate()+1);
    var nformat = today.getFullYear()+"-"+(today.getMonth()+1)+"-"+ today.getDate() ;
   nformat = nformat.replace(/\b(\w)\b/g,"0$1");//给小于10的数字前面加0
   document.getElementById("txtDeparDate").value=nformat;
   document.getElementById("txtTransDate").value=nformat;  
}
function MGetAfterTomorrow()
{
    var today = new Date();
    today.setDate(today.getDate()+2);
    var nformat = today.getFullYear()+"-"+(today.getMonth()+1)+"-"+ today.getDate() ;
   nformat = nformat.replace(/\b(\w)\b/g,"0$1");//给小于10的数字前面加0
   document.getElementById("txtDeparDate").value=nformat;
   document.getElementById("txtTransDate").value=nformat;  
}
function MGetNext()
{
 var bid = "txtDeparDate" ;
 var e = window.event.srcElement; 
 var f=e.value;
 
 var IsAdd=0;
 if (f=="<")
 {
    IsAdd=-1
 }
 if (f==">")
 {
    IsAdd=1
 }
   var d= document.getElementById(bid);
   var d1=d.value;
   var darray=d1.split("-");
   var d2=darray[1]+"/"+darray[2]+"/"+darray[0];
   var now=new Date(d2);
   now.setDate(now.getDate()+IsAdd);
   var nformat = now.getFullYear()+"-"+(now.getMonth()+1)+"-"+now.getDate();
   nformat = nformat.replace(/\b(\w)\b/g,"0$1");//给小于10的数字前面加0
   document.getElementById("txtDeparDate").value=nformat;
   document.getElementById("txtTransDate").value=nformat;  
}