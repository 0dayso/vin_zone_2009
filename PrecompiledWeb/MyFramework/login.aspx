<%@ page language="C#" autoeventwireup="true" inherits="login, App_Web_x03uetc4" enableeventvalidation="false" viewstateencryptionmode="never" enableviewstatemac="false" %>

<html xmlns="http://www.w3.org/1999/xhtml" >
<head runat="server">
<meta http-equiv="Content-Type" content="text/html; charset=gb2312">
<LINK href="img/system.css" type=text/css rel=stylesheet>
 <TITLE>用户登录-金色世纪后台管理系统 CRM 7.0</TITLE>
</head>
<script language=javascript>
    function ValidataPage()
    {
        if(document.getElementById("txtUserName").value =="")
        {
            alert("请输入用户名！");
            document.getElementById("txtUserName").focus();
            return false;
        }
        if(document.getElementById("txtPassword").value =="")
        {
            alert("请输入密码！");
            document.getElementById("txtPassword").focus();
            return false;
        }
        return true;
    }
    function login_onload()
    {
      document.all.txtUserName.focus();
      if (window.parent!=null && window.parent.location.href!=window.location.href)
      {        
        window.parent.location.href=window.location.href;
      }
    }
</script>
<body leftmargin="0" topmargin="0" marginwidth="0" marginheight="0" background="Image/LoginImage/main_bak.gif" onload="login_onload();">
<form runat="server" >
    <table width="98%" border="0" align="center" cellpadding="0" cellspacing="0">
        <tr>
            <td>
                &nbsp;</td>
        </tr>
        <tr>
            <td>
                &nbsp;</td>
        </tr>
      
        <tr>
            <td style="color: #000000; height: 18px; font-weight: bold; font-size: medium;" align="center">
                &nbsp;</td>
        </tr>
        <tr>
            <td>
                <table width="427" border="0" align="center" cellpadding="0" cellspacing="0">
                    <tr>
                        <td>
                            <div align="center">
                                <img src="Image/LoginImage/crm_login_03.gif" width="427" height="30"></div>
                        </td>
                    </tr>
                    <tr>
                        <td>
                            &nbsp;</td>
                    </tr>
                    <tr>
                        <td>
                            <img src="Image/LoginImage/crm_login_06.gif" width="427" height="44"></td>
                    </tr>
                    <tr>
                        <td background="Image/LoginImage/crm_login_07.gif">
                            &nbsp;</td>
                    </tr>
                    <tr>
                        <td height="177" background="Image/LoginImage/crm_login_09.gif">
                            <table width="75%" border="0" align="right" cellpadding="0" cellspacing="0">
                                <tr>
                                    <td height="35">
                                        <div align="right">
                                            <img src="Image/LoginImage/crm_login_id.gif" width="24" height="24" align="absmiddle">&nbsp;用户名&nbsp;</div>
                                    </td>
                                    <td>
                                        <asp:TextBox ID="txtUserName" Style="border-right: #62ACC8 1px solid; border-top: #62ACC8 1px solid;
                                            font-size: 12px; border-left: #62ACC8 1px solid; color: #333333; border-bottom: #62ACC8 1px solid;
                                            font-family: 'Arial', 'Helvetica', 'sans-serif'; background-color: #ffffff" runat="server"></asp:TextBox></td>
                                </tr>
                                <tr>
                                    <td>
                                        <div align="right">
                                            <img src="Image/LoginImage/crm_login_pw.gif" width="24" height="24" align="absmiddle">&nbsp;密码&nbsp;</div>
                                    </td>
                                    <td>
                                        <asp:TextBox ID="txtPassword" runat="server" TextMode="password" Style="border-right: #62ACC8 1px solid;
                                            border-top: #62ACC8 1px solid; font-size: 12px; border-left: #62ACC8 1px solid;
                                            color: #333333; border-bottom: #62ACC8 1px solid; font-family: 'Arial', 'Helvetica', 'sans-serif';
                                            background-color: #ffffff"></asp:TextBox></td>
                                </tr>
                                <tr>
                                    <td>
                                        &nbsp;</td>
                                    <td>
                                        &nbsp;</td>
                                </tr>
                                <tr>
                                    <td colspan="2" style="height: 28px">
                                        <div align="center">
                                            <asp:ImageButton ID="btnLogIn" runat="server" src="Image/LoginImage/crm_login_an.gif"
                                                Width="90" Height="28" OnClick="btnLogIn_Click" OnClientClick="return ValidataPage();" /></div>
                                    </td>
                                </tr>
                                <tr>
                                    <td colspan="2">
                                        &nbsp;</td>
                                </tr>
                            </table>
                        </td>
                    </tr>
                </table>
            </td>
        </tr>
          <tr>
            <td>
                &nbsp;</td>
        </tr>
        <tr>
            <td align="center" style="height:20px; font-weight: bold; font-size: medium;">
            请点击<a href="DownLoad/Setup.msi">下载</a>客户端工具</td>
        </tr>
         <tr>
            <td align="center" style="height:20px; font-weight: bold; font-size: medium;">
            各地办事处请点击<a href="DownLoad/USE_Client3.5.rar">下载</a></td>
        </tr>
        <tr>
            <td align="center">
                <div id="divMessage" runat=server style="width:420px;height:50px; font-size: 14px; color: #ff0033; font-style: normal; font-family: Arial;" align=left></div></td>
        </tr>
    </table>
</form>
</body>
</html>

