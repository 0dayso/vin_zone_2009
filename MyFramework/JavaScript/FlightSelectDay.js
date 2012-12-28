/*
  Ahui, ahui_net@163.net   
*/
	document.write("<div id=\"AhuiSelectDay\" style=\"position:absolute;z-index:999999;display:none\"></div>");
	document.write("<div id=\"AhuiSelectYearMonth\" style=\"position:absolute;z-index:999999;display:none\"></div>");
				
	
	var ToDayDate		= new Date();
	var ToDayYear		= ToDayDate.getFullYear();
	var ToDayMonth		= ToDayDate.getMonth() + 1;
	var ToDayDay		= ToDayDate.getDate();
	var ToDay			= ToDayYear +"-"+ ( ToDayMonth < 10 ? "0"+ToDayMonth : ToDayMonth ) +"-"+ ( ToDayDay < 10 ? "0"+ToDayDay : ToDayDay );
	
	var ThisAhuiYear	= ToDayYear;
	var ThisAhuiMonth	= ToDayMonth;
	var ThisAhuiObj		= null;
	
	var MouseEvent		= " onmouseover=\"this.style.backgroundColor='E4EB1D';\" onmouseout=\"this.style.backgroundColor='';\" ";
				
	// 由此开始运行
	function AhuiDateBeginRun(e)
	{
		SetDefaltDate(e.value);
		ThisAhuiObj									= e;
		AhuiTimeWriteHtml();

		var MoveTimeTop		= e.offsetTop + e.offsetHeight;
		var MoveTimeLeft	= e.offsetLeft;
		
		while ( e = e.offsetParent )
		{
			if( e.tagName.toLowerCase() == "fieldset" )
		    {
		        MoveTimeTop  += 8;
		        MoveTimeLeft += 10;
		    }
		
			MoveTimeTop		+= e.offsetTop; 
			MoveTimeLeft	+= e.offsetLeft;
		}
		document.all.AhuiSelectDay.style.top		= MoveTimeTop + 2;
		document.all.AhuiSelectDay.style.left		= MoveTimeLeft;
		document.all.AhuiSelectDay.style.display	= "";
		
        if( parseInt( document.all.AhuiSelectDay.style.top ) + parseInt( document.all.AhuiSelectDay.offsetHeight ) > parseInt( document.body.clientHeight ) + parseInt( document.body.scrollTop ) )
        {
            document.all.AhuiSelectDay.style.top = parseInt( document.all.AhuiSelectDay.style.top ) - parseInt( document.all.AhuiSelectDay.offsetHeight ) - 15;
        }	
        
        if( parseInt( document.all.AhuiSelectDay.style.left ) + parseInt( document.all.AhuiSelectDay.offsetWidth ) > parseInt( document.body.clientWidth ) + parseInt( document.body.scrollLeft ) )
        {
            document.all.AhuiSelectDay.style.left = parseInt( document.all.AhuiSelectDay.style.left ) - parseInt( document.all.AhuiSelectDay.offsetWidth ) + 75;
        }	       		
	}
	
		
	// 设置默认值
	function SetDefaltDate( Value )
	{
		var NowDate	= new Date();
		if( Value != "" )
		{
		    var ThisYear    = Value.split('-')[0];
		    var ThisMonth   = Value.split('-')[1] - 1;
		    var ThisDay     = Value.split('-')[2].split(' ')[0];
		    
			NowDate	= new Date( ThisYear, ThisMonth, ThisDay );
	
		}
		
	    
		ThisAhuiYear	= NowDate.getFullYear();
		ThisAhuiMonth	= NowDate.getMonth() + 1;
	
	}
	
	// 关闭事件
	function document.onclick()
	{
		if( document.all.AhuiSelectDay.style.display == "none" ) return;
		var ThisTag	= document.activeElement;
		if( AhuiSelectDay.contains( ThisTag ) || ThisTag == ThisAhuiObj ) return;
		document.all.AhuiSelectDay.style.display = "none";
	}
	
	function MonthChange(e)
	{
		ThisAhuiMonth = parseInt( e.value );
		AhuiTimeWriteHtml();
	}
	
	function YearChange(e)
	{
		ThisAhuiYear = parseInt( e.value );
		AhuiTimeWriteHtml();
	}
	
	// 选择年和月	
	function MonthSelectHtml()
	{
		var ReturnValue	= "<select onchange=\"MonthChange(this)\" onclick=\"window.event.cancelBubble=true\" style=\"height:10;font-size:12px;\" >";		
		for( var i=1; i<=12; i++ )
		{
			ReturnValue += "<option value=\""+ i +"\" "+ ( ( ThisAhuiMonth == i ) ? "selected" : "" ) +" >"+ ( ( i < 10 ) ? "0"+ i : i ) +"</option>";
		}		
		ReturnValue		+= "<select>";		
		return ReturnValue;
	}	
	function YearSelectHtml()
	{
		var ReturnValue	= "<select onchange=\"YearChange(this)\" onclick=\"window.event.cancelBubble=true\" style=\"height:10;font-size:12px;\" >";		
		for( var i=1900; i<=2099; i++ )
		{
			ReturnValue += "<option value=\""+ i +"\" "+ ( ( ThisAhuiYear == i ) ? "selected" : "" ) +" >"+ i +"</option>";
		}		
		ReturnValue		+= "<select>";
		return ReturnValue;
	}	
	
	function AhuiTimeWriteHtml()
	{
		document.all.AhuiSelectDay.innerHTML = _GetMonthHtml();
	}
	
				
		
	function AddMonth()
	{
		if( ThisAhuiYear == 2050 && ThisAhuiMonth == 12 ) return;
	
		if( ThisAhuiMonth == 12 )
		{
			ThisAhuiYear ++;
			ThisAhuiMonth = 1;
		}
		else
		{
			ThisAhuiMonth ++;
		}
		AhuiTimeWriteHtml();
	}
	
	function DecMonth()
	{
		if( ThisAhuiYear == 1950 && ThisAhuiMonth == 1 ) return;
		
		if( ThisAhuiMonth == 1 )
		{
			ThisAhuiYear --;
			ThisAhuiMonth = 12;
		}
		else
		{
			ThisAhuiMonth --;
		}
		AhuiTimeWriteHtml();
	}
	
	
	// 获得一个月的日历
	function _GetMonthHtml()
	{
		var MaxDay		= _GetAllDay( ThisAhuiYear, ThisAhuiMonth );
		var BeginWeek	= _GetBeginWeek( ThisAhuiYear, ThisAhuiMonth );
	
		ThisYD			= "<nobr>"+ YearSelectHtml() + MonthSelectHtml() +"</nobr>";
	
		// 头文件
		var MonthHtml	= ""+
			"<table cellspacing=\"0\" bordercolordark=\"#ffffff\" cellpadding=\"2\" rules=\"all\" bordercolorlight=\"#aaaaaa\" border=\"1\" style=\"filter:progid:DXImageTransform.Microsoft.Shadow(color=#999999,direction=135,strength=7)\">"+
				"<tr style=\"background-color: #E2E5D7\" align=\"center\">"+
					"<td width=\"13\" onclick=\"DecMonth();\" style=\"cursor:hand\" "+ MouseEvent +" >&lt;</td>"+
					"<td colspan=\"5\">"+ ThisYD +"</td>"+
					"<td width=\"13\" onclick=\"AddMonth();\" style=\"cursor:hand\" "+ MouseEvent +" >&gt;</td>"+
				"</tr>"+
				"<tr style=\"background-color: #E3EBDF;cursor:defalut;\" align=\"center\">"+
					"<td width=\"13\">日</td>"+
					"<td width=\"13\">一</td>"+
					"<td width=\"13\">二</td>"+
					"<td width=\"13\">三</td>"+
					"<td width=\"13\">四</td>"+
					"<td width=\"13\">五</td>"+
					"<td width=\"13\">六</td>"+
				"</tr>";							
			
		var AddDay = 1;
		
		// 开始
		while( AddDay <= MaxDay )
		{
			MonthHtml += "<tr style=\"background-color: #ECEAF2\" align=\"center\">";
			
			for( var i=0; i<7; i++ )
			{
			
				if( ( BeginWeek == i || AddDay != 1 ) && AddDay <= MaxDay )
				{
					var FontColor = "";
					if( ThisAhuiYear == ToDayYear && ThisAhuiMonth == ToDayMonth && ToDayDay == AddDay ) FontColor = "color:red;"
					MonthHtml += "<td onclick=\"Go(this)\" style=\"cursor:hand;"+ FontColor +"\" "+ MouseEvent +" >"+ AddDay +"</td>";
					AddDay ++;
				}
				else
				{
					MonthHtml += "<td>&nbsp;</td>";
				}
				
			}
			MonthHtml += "</tr>";
		}
		
		MonthHtml	+=	"<tr style=\"background-color: #ECEAF2;cursor:defalut;\" align=\"center\">"+
							"<td colspan=\"5\" style=\"cursor:hand;\" onclick=\"Go2('"+ ToDay +"')\" "+ MouseEvent +">今日: "+ ToDay +"</td>"+
							"<td colspan=\"2\" style=\"cursor:hand;\" onclick=\"Go2('')\" "+ MouseEvent +" >清空</td>"+
						"</tr>";
		
		// 结束标志
		MonthHtml += "</table>";
		
		return MonthHtml;
	}
	
	// 向输入框写入
	function Go(e)
	{
		var ThisDay	= parseInt( e.innerText );
		
		var SelectedDay = ( ThisAhuiYear +"-"+ ( ( ThisAhuiMonth < 10 ) ? "0"+ ThisAhuiMonth : ThisAhuiMonth ) +"-"+ ( ( ThisDay < 10 ) ? "0"+ ThisDay : ThisDay ) );
		
		Go2( SelectedDay )
		hidShow();
	}	
	function Go2( SelectedDay )
	{
		if( ThisAhuiObj != undefined )
		{
			ThisAhuiObj.value = SelectedDay;			
		}
		document.all.AhuiSelectDay.style.display	= "none";
	}
	
	
	
	
	// 获得一个月有多少天
	function _GetAllDay( )
	{
		var February	= 28;
		if( ( ThisAhuiYear % 400 == 0 ) || ( ThisAhuiYear % 4 == 0 && ThisAhuiYear % 100 != 0 ) ) February = 29;
		var MonthList	= new Array( 31, February, 31, 30, 31, 30, 31, 31, 30, 31, 30, 31 );
		
		return MonthList[ ThisAhuiMonth - 1 ];
	}		
	
	
	// 获得一个月第一天是星期几				
	function _GetBeginWeek()
	{				
		var NowDate = new Date( ThisAhuiYear, ThisAhuiMonth - 1, 1 );
		return NowDate.getDay();
	}	

var CalendarData=new Array(20);
var madd=new Array(12);
var tgString="甲乙丙丁戊己庚辛壬癸";
var dzString="子丑寅卯辰巳午未申酉戌亥";
var numString="一二三四五六七八九十";
var monString="正二三四五六七八九十冬腊";
var weekString="日一二三四五六";
var sx="鼠牛虎兔龙蛇马羊猴鸡狗猪";
var cYear;
var cMonth;
var cDay;
var cHour;
var cDateString;
var DateString;
var Browser=navigator.appName;
function init()
{
    CalendarData[0]=0x41A95;
    CalendarData[1]=0xD4A;
    CalendarData[2]=0xDA5;
    CalendarData[3]=0x20B55;
    CalendarData[4]=0x56A;
    CalendarData[5]=0x7155B;
    CalendarData[6]=0x25D;
    CalendarData[7]=0x92D;
    CalendarData[8]=0x5192B;
    CalendarData[9]=0xA95;
    CalendarData[10]=0xB4A;
    CalendarData[11]=0x416AA;
    CalendarData[12]=0xAD5;
    CalendarData[13]=0x90AB5;
    CalendarData[14]=0x4BA;
    CalendarData[15]=0xA5B;
    CalendarData[16]=0x60A57;
    CalendarData[17]=0x52B;
    CalendarData[18]=0xA93;
    CalendarData[19]=0x40E95;
    madd[0]=0;
    madd[1]=31;
    madd[2]=59;
    madd[3]=90;
    madd[4]=120;
    madd[5]=151;
    madd[6]=181;
    madd[7]=212;
    madd[8]=243;
    madd[9]=273;
    madd[10]=304;
    madd[11]=334;
}
function GetBit(m,n)
{
    return (m>>n)&1;
}
function e2c(TheDate)
{
    var total,m,n,k;
    var isEnd=false;
    var tmp=TheDate.getYear();
    if (tmp<1900) tmp+=1900;
    
    total=(tmp-2001)*365
    +Math.floor((tmp-2001)/4)
    +madd[TheDate.getMonth()]
    +TheDate.getDate()
    -23;
    if (TheDate.getYear()%4==0&&TheDate.getMonth()>1)
        total++;
    for(m=0;;m++)
    {
        k=(CalendarData[m]<0xfff)?11:12;
        for(n=k;n>=0;n--)
        {
            if(total<=29+GetBit(CalendarData[m],n))
            {
                isEnd=true;
                break;
            }
            total=total-29-GetBit(CalendarData[m],n);
        }
        if(isEnd)break;
    }
    cYear=2001 + m;
    cMonth=k-n+1;
    cDay=total;
    if(k==12)
    {
        if(cMonth==Math.floor(CalendarData[m]/0x10000)+1)
        cMonth=1-cMonth;
        if(cMonth>Math.floor(CalendarData[m]/0x10000)+1)
        cMonth--;
    }
    cHour=Math.floor((TheDate.getHours()+3)/2);
}
function GetcDateString()
{   
    var tmp="";
    tmp+=tgString.charAt((cYear-4)%10); //年干
    tmp+=dzString.charAt((cYear-4)%12); //年支
    tmp+="年(";
    tmp+=sx.charAt((cYear-4)%12);
    tmp+=")";
    if(cMonth<1)
    {
        tmp+="闰";
        tmp+=monString.charAt(-cMonth-1);
    }
    else
        tmp+=monString.charAt(cMonth-1);
        
    tmp+="月";
    tmp+=(cDay<11)?"初":((cDay<20)?"十":((cDay<30)?"廿":"卅"));
    if(cDay%10!=0||cDay==10)
        tmp+=numString.charAt((cDay-1)%10);
    if(cHour==13)tmp+="夜";
        tmp+=dzString.charAt((cHour-1)%12);
    tmp+="时";
    cDateString=tmp;
    return tmp;
}
function GetYinData(TheDate)
{
    init();
    e2c(TheDate);
   // return "123" ;
    return "阴历：" + cYear + "年" + cMonth + "月" + cDay + "号  " ;
}