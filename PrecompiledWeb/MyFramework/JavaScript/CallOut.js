/*
*
*
*add by yanwei 2011-3-21
*主要用于外呼
*
*用法;在需要外呼的电话号节点上添加　　　 isCallOut='yes'　属性
*
*例如：<input type='text' value='13241995173' isCallOut='yes'/>
*
*如果要保存数据库记录请实现这个无参数的方法（AddCallOutInfo）
*
*
*/
var CallOut={
    createDiv: function (id,width,height,left,top,str){
            var div=document.createElement("div");
             div.id=id;
             div.style.position        = "absolute";
             div.style.width            = width; 
             div.style.height            = height; 
             div.style.borderWidth        ="1px";
             div.style.borderStyle        = "solid";
             div.style.borderColor        = "gray";
             div.style.fontSize        = "9pt";
             div.style.backgroundColor    = "#ffffff";
             div.style.color            = "#349045";
             div.style.filter            = "progid:DXImageTransform.Microsoft.Shadow(color=#999999,direction=135,strength=3)";
             div.style.padding            = "5px 8px 3px 8px";
             div.style.left = left+"px" ;
             div.style.top = top+"px";
             div.innerHTML=str;
             return div;
    },
    callOut: function (phone){
        if(phone){ 
            if(IsMobile(phone)){
                phone="90"+phone
            }else{
                if(phone.substring(0,3)=="010"){
                     phone="0"+phone.substring(3)
                }else
                     phone="9"+phone;  
            } 
            topWin=CallOut.getData(window);
            topWin.open('call:'+phone+':call'); 
            try{
                if(AddCallOutInfo)
                   AddCallOutInfo();
            }catch(e){}
         }	
    },
    getData:function (p) {
               var i=0; 
                while(p.opener!=null){    
                    p = p.opener; 
                }
                 return p; 
    },
    showTips:function(ele){
          var showTips=document.getElementById("showCallOutMessge"); 
          var left=wgetObjectLeft(ele);
          var top=wgetObjectTop(ele);
          if(showTips){
             showTips.style.left=(left+180)+"px";
             showTips.style.top=(top)+"px";
             showTips.style.display="block";
             showTips.innerHTML=ele.value
          }else{ 
             showTips=CallOut.createDiv("showCallOutMessge",220,20,left+180,top,ele.value);
             document.body.appendChild(showTips);
          }  
         if(!showTips.onmouseover){
              showTips.onmouseover=function(){ this.style.display="block";}
              showTips.onmouseout=function() { this.style.display="none"; }
          }  
    },
    show:function(ele){
          var a=document.createElement("A");
          a.innerText="呼出"; 
          a.href="javascript:void(0);";   
          if(ele.tagName == "SELECT"){ 
             ele.onchange=function(){ CallOut.showTips(this); } 
          }     
          a.onclick=function(){
            var ele=this.previousSibling;   
            if(ele.tagName == "SELECT"){
               phone=ele.options[ele.selectedIndex].text.replace(/[^0-9]/,"");
            }else{
                phone=(ele.value||ele.innerText).replace(/[^0-9]/,"");  
            } 
            if(phone==""||phone=="-1")  
                alert("请输入正确的电话号码！"); 
            else
                CallOut.callOut(phone); 
         } 
         ele.insertAdjacentElement("afterEnd",a);   
    },
    bind:function(){ 
        var elements = document.all?document.all:document.getElementsByTagName("*"); 
        for(var i=0;i<elements.length;i++){
	        if (elements[i].getAttribute("isCallOut")=="yes"){ 
	           CallOut.show(elements[i]);
            } 
        }	 
     }
}; 
window.attachEvent("onload",CallOut.bind);