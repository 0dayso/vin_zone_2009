<%@ Control Language="C#"  AutoEventWireup="true" CodeFile="PageBegin.ascx.cs" Inherits="SystemFrame_PageBegin" %>
<%@ Register Src="PageHeader.ascx" TagName="PageHeader" TagPrefix="uc2" %>
<%@ Register Src="PageLeft.ascx" TagName="PageLeft" TagPrefix="uc1" %>
<%@ Register Assembly="MyFramework.BusinessLogic" Namespace="MyFramework.BusinessLogic.Common.SystemFrame" TagPrefix="cc1" %>
<script language=javascript>
//得到当前对象的left和Top 值
function wgetObjectLeft(toObject){  
    var lnLeftValue = toObject.offsetLeft;   
    while(toObject = toObject.offsetParent){   
        lnLeftValue += toObject.offsetLeft;   
    }   
    return lnLeftValue; 
}   
function wgetObjectTop(toObject){   
    var  lnTopValue = toObject.offsetTop;   
    while(toObject=toObject.offsetParent){   
        lnTopValue += toObject.offsetTop;   
    }   
    return lnTopValue;
}   
function displayControlPanel(isShow,toObject){
     var loIframe = document.getElementById("ifrmConverPanel");
     var toPanelObject = document.getElementById("DivCtl");
        if(isShow ){     
            var lnLeft = wgetObjectLeft(toObject) ;
            var lnTop = wgetObjectTop(toObject);                         
                         
            toPanelObject.style.display = "block";
            loIframe.style.display ="block";
                
//            if(document.body.clientWidth - lnLeft <toPanelObject.offsetWidth){
//                lnLeft = lnLeft + toObject.offsetWidth - toPanelObject.offsetWidth;  
//            }             
            toPanelObject.style.left = lnLeft-toObject.offsetWidth-20;
            toPanelObject.style.top = lnTop-15;  
                    
            loIframe.style.height = toPanelObject.offsetHeight;
            loIframe.style.width = toPanelObject.offsetWidth;
            loIframe.style.left = toPanelObject.style.left;
            loIframe.style.top = toPanelObject.style.top;
            
        }else{
            toPanelObject.style.display = "none";
            loIframe.style.display ="none";
        }

}

</script>
<iframe id="ifrmConverPanel"   style="z-index:10000; position:absolute;display:none; left: 18px; width: 101px; top: 2px; height: 25px;"></iframe>
        <div id="DivCtl" style="z-index:10001;position:absolute;display:none; display: none"   onmouseleave="displayControlPanel(false,document.getElementById('ctlPanel'))">
            <table align="center" border="0" cellpadding="0" cellspacing="0" width="120">
                <tr>
                    <td>
                        <img height="4" src="/MyFramework/Image/CtlPanel/crm_window_01.gif" width="123" /></td>
                </tr>
                <tr>
                    <td background="/MyFramework/Image/CtlPanel/crm_window_02.gif">
                        <table align="center" border="0" cellpadding="0" cellspacing="0" width="80%">
                            <tr>
                                <td height="6">
                                </td>
                                <td>
                                </td>
                            </tr>
                            <tr>
                                <td>
                                    <div align="center">
                                        <img align="absMiddle" height="18" src="/MyFramework/Image/CtlPanel/crm_kzmb_pw.gif" width="18" /></div>
                                </td>
                                <td>
                                    <div align="center">
                                        <img align="absMiddle" height="18" src="/MyFramework/Image/CtlPanel/crm_kzmb_dx.gif" width="18" /></div>
                                </td>
                            </tr>
                            <tr>
                                <td>
                                    <div align="center">
                                        <a href="/MyFramework/ChangePassword.aspx">密码</a></div>
                                </td>
                                <td>
                                    <div align="center">
                                        <a href="#">短信</a></div>
                                </td>
                            </tr>
                            <tr>
                                <td height="6">
                                </td>
                                <td>
                                </td>
                            </tr>
                            <tr>
                                <td>
                                    <div align="center">
                                        <img align="absMiddle" height="18" src="/MyFramework/Image/CtlPanel/crm_kzmb_new.gif" width="18" /></div>
                                </td>
                                <td>
                                    <div align="center">
                                        <img align="absMiddle" height="18" src="/MyFramework/Image/CtlPanel/crm_kzmb_gd.gif" width="18" /></div>
                                </td>
                            </tr>
                            <tr>
                                <td>
                                    <div align="center">
                                        <a href="#">消息</a></div>
                                </td>
                                <td>
                                    <div align="center">
                                        <a href="#">工单</a></div>
                                </td>
                            </tr>
                        </table>
                    </td>
                </tr>
                <tr>
                    <td>
                        <img height="8" src="/MyFramework/Image/CtlPanel/crm_window_03.gif" width="123" /></td>
                </tr>
            </table>
    
    </div>


<table height="100%" cellSpacing="0" cellPadding="0" width="100%" align="center" border="0">
    <tr height="100%" width="100%">
      	<table height="100%" cellspacing="0" cellpadding="0" width="100%" border="0" bgcolor="#F7FBFF">
					<tr>
					    <% if (this.IsShowPageHeader)
                        { %>
						<td valign="top"  height="100%" bgcolor="#D4E7EF">
							     <table id="tbcrmleftmenu" style="width:128px" cellpadding=0 cellspacing=0 border="0"  bgcolor="#D4E7EF"  align="right">
                                        <tr>
                                           <td height="20px"></td>
                                         </tr>
                                         <tr>
                                         <td  width="126px" align="right">
                                         <input type="hidden" id="OldleftId" value="" />
                                            <uc1:PageLeft ID="moPageLeft" runat="server"/>
                                        </td>
                                    </tr>
                                </table>
						</td>
						<td width="8" valign="top" class="HideBar"  background="/MyFramework/Image/GLeftImage/crm_dh_01.gif"> 
                         <img id="igHideBarImpage" src='/MyFramework/Image/GLeftImage/framehide.gif' border="0" alt="隐藏/显示模块栏" onClick="javascript:hidebar();" WIDTH="9" HEIGHT="31" >
                       </td>
					    <%}else{%>
	                    <td valign="top"  height="100%" width="10px" >&nbsp;
	                    </td>	
	                   <%} %>
						<td align="left" width="100%" valign="top" cellspacing="0" cellpadding="0"   >
							<table height="100%" cellspacing="0" cellpadding="0" width="100%" border="0" align="center">
								<tr>
									<td id="mainright" valign="top" align="center">
									<table height="100%" cellspacing="0" cellpadding="0" width="100%" border="0">
										<tr>
											<td valign="top" height="100%" align="center">
											    <table width="98%" border="0" cellpadding="0" cellspacing="0">
                                                  <% if (this.IsShowTabHeader)
                                                             { %>
                                                 <tr>
                                                 
                                                    <td height="45" valign="bottom">
                                                       
                                                             <table width="100%" border="0" cellspacing="0" cellpadding="0" border="0"  height="100%">
                                                          <tr>
                                                             <td width="20">&nbsp;</td>
                                                             
                                                            <td  valign="bottom">
                                                              <table width="230" border="0" cellspacing="0" cellpadding="0">
                                                                  <tr>
                                                                    <td width="7"><img src="/MyFramework/Image/GLeftImage/crm_dh_03.gif" width="7" height="28"></td>
                                                                    <td background="/MyFramework/Image/GLeftImage/crm_dh_04.gif"><div align="center"><img src="/MyFramework/Image/GLeftImage/dot-dot.jpg" width="13" height="13">&nbsp;<span class="big_14"><%=this.PageCaption%></span></div></td>
                                                                    <td width="7"><img src="/MyFramework/Image/GLeftImage/crm_dh_05.gif" width="12" height="28"></td>
                                                                  </tr>
                                                              </table>
                                                              </td>
                                                               <td valign="bottom">
                                                              <span  style="font-size:16px; color:Red"><%=this.SysDateTime%></span>
                                                              </td>
                                                              <% if (this.IsShowPageHeader)
                                                             { %>
                                                              <td valign="bottom"><table width="150" border="0" align="right" cellpadding="0" cellspacing="0">
                                                                  <tr>
                                                                    <td valign="middle" width="60" ></td>
                                                                    <td valign="middle" width="60" ><a href="<%=this.Toolhref%>" target="_blank"><%=this.ToolBar%></a></td>
                                                                    
                                                                  </tr>
                                                              </table></td>
                                                              <%} %>
                                                            </tr>
                                                        </table>
                                                        </td>
                                             	   </tr>
                                             	    
												    <tr>
                                                        <td height="1" bgcolor="4C7686"></td>
                                                     </tr>
                                                     <%} %>
													<tr height="100%">
														<td bgcolor="#ffffff" align="left" valign="top" height="100%" colspan=2>
															<table cellpadding=4 cellspacing=2 border=0 width="100%" height="100%">
																<tr>
																	<td width="100%" valign="top" align=Left height="100%">
        
						<!-- Begin Page -->
                       