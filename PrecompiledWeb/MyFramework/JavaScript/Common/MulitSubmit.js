// JScript 文件
                                                             
function CreateShowDiv(indentyId) 
{
    var spanContent = document.createElement("SPAN");
    spanContent.id = "spanContent"+indentyId;
    spanContent.style.display = "none";
    spanContent.style.position="absolute";
    spanContent.style.color = "red";
    
    var img = document.createElement("IMG");
    img.src = "/MyFramework/Image/loading_little.gif";
    img.onmouseover = function()
    {
        this.src='/MyFramework/Image/loading_mid.gif';
    }
    
    var textContent = document.createTextNode("处理中...，请稍候!");
    
    spanContent.appendChild(img);
    spanContent.appendChild(textContent);
    
	document.body.appendChild(spanContent);
}

function ShowMask(paramSearch)
{
    var validFunction = paramSearch.getAttribute("ValidFunction");
    
    if(validFunction != null && validFunction !="undefined")
    {
       var bResult = eval(validFunction+"()");
       
       if(!bResult)
         return false;
    }
    
    paramSearch.style.visibility = "hidden";
    
    var objSpan = document.getElementById('spanContent'+paramSearch.id);
    
    objSpan.style.display=''; 
    
    objSpan.style.top  = paramSearch.offsetTop;
    
	objSpan.style.left = paramSearch.offsetLeft;
     
}    
      
function LoadImage()
{
    var inputObjs = document.getElementsByTagName("input");
    
	for(var i=0;i<inputObjs.length;i++){
	
		if (inputObjs[i].type == "submit" &&  inputObjs[i].getAttribute("IsForbidden") != null &&  inputObjs[i].getAttribute("IsForbidden").toLowerCase()=="true")
		{ 
			CreateShowDiv(inputObjs[i].id);
			
			inputObjs[i].onclick = 	function()
			{
			    return ShowMask(this);
			}
		}
	}
}
window.attachEvent("onload",LoadImage);
