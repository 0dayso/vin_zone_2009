// JScript 文件
// 作者 佘邵镔 
if(!document.attachEvent)
{
    document.attachEvent = function(){document.addEventListener(arguments[0].substr(2),arguments[1],arguments[2])}
}

document.attachEvent("onmouseover",function(e)
{

	if (document.readyState!="complete") return ; 
    var tip = "";

    if(typeof(event)=="undefined"){
        tip = e.target.getAttribute("tips")
    }else{
        e    = event;
        tip = e.srcElement.tips;
    }

    
    if(typeof(tip)!="undefined"&&tip.length>0)
    {
        var _tips = document.getElementById("myTip");
        
        if(typeof(_tips)=="undefined"||_tips == null)
        {
            _tips        = document.createElement("div");
            _tips.id    = "myTip";
            _tips.style.position        = "absolute";//relative absolute
            _tips.style.width            = "200px";//宽度
            _tips.style.borderWidth        = "1px";
            _tips.style.borderStyle        = "solid";
            _tips.style.borderColor        = "gray";
            _tips.style.fontSize        = "9pt";
            _tips.style.backgroundColor    = "#ffffff";
            _tips.style.color            = "#349045";

            
            _tips.style.filter            = "progid:DXImageTransform.Microsoft.Shadow(color=#999999,direction=135,strength=3)";
            _tips.style.padding            = "5px 8px 3px 8px";
            


            document.body.appendChild(_tips);            
            _tips.style.display            = "none";
        }

            _tips.style.display    = "";//不显示
           
           
             _tips.innerHTML        = tip;
		   
          // _tips.innerHTML+="<br>e.clientY+tip.height"+e.clientY+"+"+_tips.length;
          //_tips.innerHTML+="<br>document.documentElement.scrollHeight="+document.documentElement.scrollHeight;
            if (document.body.scrollLeft+e.clientX+210 >document.body.scrollWidth )
            {
				 _tips.style.left =document.body.scrollLeft+e.clientX-210; 
            }
            else
            {
				_tips.style.left =document.body.scrollLeft+e.clientX+10; 
           
            }
             
             if (e.clientY+tip.length/4>document.body.scrollHeight/2)
            {
				 _tips.style.top = document.body.scrollTop+e.clientY - 40; 
            }
            else
            {
				_tips.style.top = document.body.scrollTop+e.clientY; 
            }
            
            var _iframestr="<iframe frameborder=0 scrolling='NO' style='position:absolute; visibility:inherit; top:0px; left:0px; z-index:-1;' width=100% height=100%></iframe>";

            _tips.innerHTML+=_iframestr;
            
    }
}
);

document.attachEvent('onmouseout',function(e)
{   
    var _tips = document.getElementById("myTip");
    if(_tips!=null)
    {
        _tips.style.display="none";//鼠标移走
    }
}
)


    