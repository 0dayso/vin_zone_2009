<%@ page language="C#" autoeventwireup="true" inherits="MainPage, App_Web_x03uetc4" %>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>酒店预订 机票预订 旅游度假 - 金色世纪网</title>

   <%-- <script language="javascript" type="text/javascript">
        // 窗口是否存在
        var HiddenMsgId = null;
        
        //建立一个弹出窗口
        var oPopup = window.createPopup();

        //得到这个弹出窗口的body
        var oPopupBody = oPopup.document.body;

        //开始显示的坐标（默认是最右下脚）
        flyMove.expand = 0;
        flyMove.flyY = 0;
        flyMove.flyX = 0;

        //渐进显示的定时器
        var g_idFlyPopup = -1;

        //显示弹出窗口的定时器
        var TimeoutFlag=-1;


        window.onblur = function(){ 
        HiddenMsgId = null; 
        } 
        
        //显示弹出窗口的方法
        function richDialog()
        {
            if (document.getElementById("MessageCheck").checked)
            {
//                alter("aa");
                var dt = MainPage.GetMessage().value;
    //            var dt = null
    

                if (null != dt )
                {
                    window.focus();
                     if (HiddenMsgId != dt.Rows[0]["MESSAGE_ID"])
                     {
                         //在弹出窗口中写入文字和数据
                         oPopup.document.body.innerHTML = oDialog.innerHTML; 

                         oPopupBody.style.fontSize = document.body.currentStyle.fontSize;

                         oPopupBody.style.cursor="pointer";
                         oPopupBody.style.color = "infotext";
                         oPopupBody.style.borderWidth='3px';
                         oPopupBody.style.borderStyle='window-inset';
                         
                         //oPopupBody.style.borderColor='activeborder';
                         //下面代码会立即显示弹出窗口
                //            oPopup.show(100, 50, 400, 300);
                        if (null == dt.Rows[0]["MSG_TITLE"])
                        {
                            oPopup.document.getElementById("MSG_TITLE").innerText = "";
                        }else{
                            oPopup.document.getElementById("MSG_TITLE").innerText = dt.Rows[0]["MSG_TITLE"];
                        }
                        if (null == dt.Rows[0]["MSG_CONTENT"])
                        {
                            oPopup.document.getElementById("MSG_CONTENT").innerText = "";
                        }else{
                            oPopup.document.getElementById("MSG_CONTENT").innerText = dt.Rows[0]["MSG_CONTENT"];
                        }
                        oPopup.document.getElementById("Message_Id").innerText = dt.Rows[0]["MESSAGE_ID"];
                        
//                        document.getElementById("HiddenMsgId").value = dt.Rows[0]["MESSAGE_ID"];
                        
                        oPopup.document.getElementById("MSG_Count").innerText = "您有" + dt.Rows.length +"条未读新消息"
                        flyInit();
                        g_idFlyPopup = window.setInterval("flyMove()",10);
                        
                        HiddenMsgId = dt.Rows[0]["MESSAGE_ID"];
                     }
                }
            }


        }

        function flyMove()
        {
         flyMove.expand += 2;
         flyMove.flyY -= 2;
         oPopup.show(flyMove.flyX-flyMove.expand, flyMove.flyY, flyMove.expand, flyMove.expand);
         var oPopupBody = oPopup.document.body;
         if (oPopupBody.clientWidth >= oPopupBody.scrollWidth && oPopupBody.clientHeight >= oPopupBody.scrollHeight)
         {
          //清除渐进显示的定时器
          window.clearInterval(g_idFlyPopup);
          g_idFlyPopup = -1;

          //清除调用弹出窗口的定时器
          window.clearTimeout(TimeoutFlag);
          TimeoutFlag=-1;

          //注册5秒后关闭弹出窗口的定时器
          //window.setTimeout( 'closePopup()', 50000 );

         }
        }

        //关闭弹出窗口
        function closePopup()
        {
         if( null != oPopup )
         {
          oPopup.hide();
         }
        }

        //初始化弹出窗口的坐标，将其定位到最右下角
        function flyInit()
        {
         flyMove.expand = 0;
         flyMove.flyY = window.screen.height;
         flyMove.flyX = window.screen.width;
        }

        function closeMessage()
        {
            if( null != oPopup )
            {
                MainPage.DeleteMessage(oPopup.document.getElementById('Message_Id').innerText);
                oPopup.hide();
                HiddenMsgId = null;
            }

        }
    
    function iframeLoad()
    {
        if(ifrmMain.document.body.scrollHeight>430)
        {
          top.document.all['ifrmMain'].height="610px";
          if(ifrmMain.document.body.scrollHeight>top.document.all['ifrmMain'].height)
                top.document.all['ifrmMain'].height=ifrmMain.document.body.scrollHeight;
         
        }
        document.title ="酒店预订 机票预订 旅游度假 - 金色世纪网";
    }
   
    function topMenuonClick(tsmenuid,tsmenuurl)
    {
        top.document.all['ifrmMain'].src=tsmenuurl;
      //  var loToptableDt=document.getElementById("toptable");
//        for(var lnIndex=1;lnIndex<loToptableDt.rows[0].cells.length-1;lnIndex++)
//        {
//            loToptableDt.rows[0].cells[lnIndex].children[0].rows[0].cells[0].background="Image/GTopImage/TopMenu_bg1.gif";
//        }  
       // var objList = window.event.srcElement;  
       // objList.parentElement.background="Image/GTopImage/TopMenu_bg2.gif";
    
    }
    function RefReshWeb()
    {
        top.document.all['ifrmFresh'].src="/MyFramework/ReConnectPage.aspx?rdom="+ new Date();
       
    }
    
    function Ischecked(checked)
    {
        if (checked)
        {
            HiddenMsgId = null;
            document.getElementById('MessageCheck').checked = true;
        }
    }
    
    
    function DepartmentTask(){ 
      ifrmMain.location.href="/MyFramework/PublicCase/PublicCaseSystemTask.aspx?TopMenuID=6&LeftMenuID=614";
    }
     
    function Main_onLoad()
    {
        window.setInterval('RefReshWeb()',180000);

        richDialog();
        window.setInterval("richDialog()", 10000 );
    }
    
    
    </script>--%>

    <link rel="Stylesheet" href="CSS/SystemFrame.css" type="text/css" />
</head>
<body topmargin="0" leftmargin="0" rightmargin="0" bottommargin="0" >
   
    <form id="form1" runat="server">
     
     <div id="oDialog" style="display: none;">
            <div id="myid" style="position: absolute; top: 0; left: 0; width: 200px; height: 100%;
                border: 1px solid black; border-top: 1px solid white; border-left: 1px solid white;
                font: normal 10pt tahoma; color: blue;">
                <div style="display: none;">
                    <embed src="Sound/type.wma" loop="false">
                </div>
                <div style="font-family: tahoma; background-color: #4490AE; margin: 0px; width: 200px; height:100%;" >
                    <table border="0" cellpadding="3" cellspacing="1"
                        width="250px" id="showtable" height="250px" >
                        <tr>
                            <td style="padding-left: 4px; font-weight: normal; font-size: 12px; font-family: Arial;
                                background-color: #E8F3F7; color: Black;" align="center" colspan="2" >
                                <font color="red" id="MSG_Count"></font>
                            </td>
                        </tr>
                        <tr>
                            <td style="background-color: break-word; padding-left: 4px; font-weight: normal;
                                font-size: 12px; font-family: Arial; background-color: #E8F3F7; color: Black;width:250px"
                                id="MSG_TITLE" colspan="2" align="center">
                            </td>
                        </tr>
                        <tr>
                            <td style="padding-left: 4px; font-weight: normal; font-size: 12px; font-family: Arial;
                                background-color: #E8F3F7; color: Black; width: 70px">
                                你的详细消息:
                            </td>
                            <td style="background-color: White; word-wrap: break-word; padding-left: 4px; font-weight: normal;
                                font-size: 12px; font-family: Arial; background-color: #E8F3F7; color: Black;width:180px; height:130px"
                                id="MSG_CONTENT">
                            </td>
                        </tr>
                        <tr>
                            <td style="padding-left: 3px; font-weight: normal; font-size: 12px; font-family: Arial;
                                background-color: #E8F3F7; color: Black;" colspan="2" align="center">
                                <button class="style1" style="border: 1px solid black; border- left: 1px solid white;
                                    border- top: 1px solid white; background: #efefef" tabindex="-1" onclick="window.parent.closeMessage();">
                                    删除消息</button>&nbsp
                                <button class="style1" style="border: 1px solid black; border- left: 1px solid white;
                                    border- top: 1px solid white; background: #efefef" tabindex="-1" onclick="window.parent.oPopup.hide();window.parent.document.getElementById('MessageCheck').checked = false;isOpened=false;">
                                    关闭提示</button>&nbsp
                               <button style="border: 1px solid black; border- left: 1px solid white;border- top: 1px solid white; width:60px;
                                 background: #efefef; " tabindex="-1" onclick="window.parent.DepartmentTask();"  > 部门任务</button>
                            </td>
                        </tr>
                    </table>
                </div>
                <div id="Message_Id" style="font-size: 12px; display: none">
                </div>
            </div>
        </div>
        <table height="100%" width="100%" cellspacing="0" cellpadding="0" align="center"
            border="0">
            <tr>
                <td width="100%"  height="66">
                    <table width="100%" border="0" align="center" cellpadding="0" cellspacing="0">
                        <tr>
                            <td height="66" valign="bottom" background="Image/GTopImage/Top_bg.gif">
                                <table width="100%" border="0" cellspacing="0" cellpadding="0">
                                    <tr>
                                        <td colspan="3" >
                                            <table width="100%" border="0" cellpadding="0" cellspacing="0" height="40">
                                               <tr>
                                                 <td width="126"><img src="Image/GTopImage/crm_top_01.gif" width="263" height="56"></td>
                                                    <td background="Image/GTopImage/crm_top_02.gif">&nbsp;</td>
                                                    <td width="22" valign="bottom" background="Image/GTopImage/crm_top_03-01.gif"></td>
                                                     <td width="500" valign="bottom" background="Image/GTopImage/crm_top_04.gif"></td>
                                                </tr>
                                            </table>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td height="2" colspan="3" background="Image/GTopImage/crm_top_05.gif">
                                        </td>
                                    </tr>
                                    <tr>
                                        <td background="Image/GTopImage/crm_topdh_01.gif" style="height: 28px">
                                            <%this.Response.Write(lsTopHtml);%>
                                        </td>
                                        <td style="height: 28px" background="Image/GTopImage/crm_topdh_01.gif">
                                            &nbsp
                                        </td>
                                        <td style="height: 28px" background="Image/GTopImage/crm_topdh_01.gif">
                                            <span id="spanKnowledge" runat="server"><a href='http://jp.jsj.com.cn/KnowledgeBase.aspx' target='_blank'>知识库</a></span>
                                            <nobr>
                                                提示窗<input id="MessageCheck" type="checkbox" value="" checked="checked" onclick="Ischecked(this.checked);" />&nbsp;
                                            </nobr>
                                        </td>
                                    </tr>
                                </table>
                            </td>
                        </tr>
                    </table>
                </td>
            </tr>
            <tr height="100%">
                <td style="height: 104%">
                    <iframe id="ifrmMain" name="ifrmMain" src='<%=lsTopDefUrl%>' "  
                        scrolling="auto" frameborder="0" width="100%" height="100%" >
                    </iframe>
                </td>
            </tr>
        </table>
       <iframe id="ifrmFresh" name="ifrmFresh" src=''scrolling="no" frameborder="0" width="0" height="0" style="display:none"></iframe>
    </form>
</body>
</html>
