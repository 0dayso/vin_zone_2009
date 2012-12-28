function RoundFloat(tnValue, tnPrecise){
	if(tnPrecise==null) tnPrecise= 6;
	var lnNumber = Math.pow(10,tnPrecise)
	return Math.round(tnValue*lnNumber)/lnNumber;
}
function CTrim(tsValue)
{
    return tsValue.replace(/(^\s*)|(\s*$)/g, ""); 
}
function CDate(tsDateString,tsSeparator)// yyyy-MM-dd OR yyyy-MM-dd :hh:mm:ss
{
    if(typeof(tsSeparator) == "undefined" || tsSeparator==null)tsSeparator="-";
    var lsDateString = null;
    var lsTimeString  = null;
    var loDateTimes = tsDateString.split(" ");
    if(loDateTimes!=null && loDateTimes.length>1)
    {
        lsDateString =loDateTimes[0];
        lsTimeString = loDateTimes[1]; 
    }else
        lsDateString = tsDateString;
    var loDates = lsDateString.split(tsSeparator);
    if(loDates!=null&& loDates.length>2)
    {
        var date  = new Date();
        date.setFullYear(loDates[0],loDates[1]-1,loDates[2]);
        if(lsTimeString!=null){
            lsTimes = lsTimeString.split(":");
            if(lsTimes!=null && lsTimes.length>1)
                date.setHours(lsTimes[0],lsTimes[1],0,0);  
            else
                date.setHours(0,0,0,0);
        }else
            date.setHours(0,0,0,0);
        return date;
    }else{
        return null;
    }
}
function DateFormat(tdDate,tsFormat){
    if(typeof(tsFormat) == "undefined" || tsFormat == null)tsFormat="yyyy-MM-dd";
    if(typeof(tdDate) =="string") tdDate = CDate(tdDate);
    Date.prototype.format = function(format) 
    { 
        var o = { 
            "M+" : this.getMonth()+1, //month 
            "d+" : this.getDate(), //day 
            "h+" : this.getHours(), //hour 
            "m+" : this.getMinutes(), //minute 
            "s+" : this.getSeconds(), //second 
            "q+" : Math.floor((this.getMonth()+3)/3), //quarter 
            "S" : this.getMilliseconds() //millisecond 
            } 
            if(/(y+)/.test(format)) format=format.replace(RegExp.$1, 
            (this.getFullYear()+"").substr(4 - RegExp.$1.length)); 
            for(var k in o)if(new RegExp("("+ k +")").test(format)) 
            format = format.replace(RegExp.$1, 
            RegExp.$1.length==1 ? o[k] : 
            ("00"+ o[k]).substr((""+ o[k]).length)); 
            return format; 
        } 
    return (tdDate.format(tsFormat));
} 
function DateFormatYMDHMNT(tdDate){
    return DateFormat(tdDate,"yyyy-MM-dd hh:mm");   
}   
function DateFormatYMDHMS(tdDate){
    return DateFormat(tdDate,"yyyy-MM-dd hh:mm:ss");   
}   
function DateFormatYMDHM(tdDate,tsTime){
    return DateFormat(tdDate,"yyyy-MM-dd") +" "+tsTime;   
} 
function GetWeekOfDay(tdDate){    
    var lsDay = "";
    if(typeof(tdDate)!="object") tdDate = CDate(tdDate,null);
    if(tdDate ==null) return null;
    if (tdDate.getDay()==0) lsDay = "日"; 
    if (tdDate.getDay()==1) lsDay = "一"; 
    if (tdDate.getDay()==2) lsDay = "二"; 
    if (tdDate.getDay()==3) lsDay = "三"; 
    if (tdDate.getDay()==4) lsDay = "四"; 
    if (tdDate.getDay()==5) lsDay = "五"; 
    if (tdDate.getDay()==6) lsDay = "六"; 
    return lsDay;
}
function DateDiff(tdDate1, tdDate2){ 
    var lnDays;
    if(typeof(tdDate1)!="object") tdDate1 = CDate(tdDate1);
    if(typeof(tdDate2)!="object") tdDate2 = CDate(tdDate2);
    if(tdDate1==null || tdDate2 == null) return null;
    lnDays = parseInt((tdDate1 - tdDate2) / 1000 / 60 / 60 /24);//把相差的毫秒数转换为天数 
    return lnDays;
}
function DateAdd(interval,number,date){ // date 可以是时间对象也可以是字符串，如果是后者，形式必须为: yyyy-mm-dd hh:mm:ss 其中分隔符不定。"2006年12月29日 16点01分23秒" 也是合法的
    number = parseInt(number);
    if (typeof(date)=="string"){
        loDate = date.split(/\D/);
        eval("var loDate = new Date("+loDate.join(",")+")");
        loDate.setMonth(loDate.getMonth()-1);
    }
    if (typeof(date)=="object"){
        var loDate = new Date(date);
    }
    switch(interval){
        case "y": loDate.setFullYear(loDate.getFullYear()+number); break;
        case "m": loDate.setMonth(loDate.getMonth()+number); break;
        case "d": loDate.setDate(loDate.getDate()+number); break;
        case "w": loDate.setDate(loDate.getDate()+7*number); break;
        case "h": loDate.setHours(loDate.getHour()+number); break;
        case "n": loDate.setMinutes(loDate.getMinutes()+number); break;
        case "s": loDate.setSeconds(loDate.getSeconds()+number); break;
        case "l": loDate.setMilliseconds(loDate.getMilliseconds()+number); break;
    } 
    return loDate;
}