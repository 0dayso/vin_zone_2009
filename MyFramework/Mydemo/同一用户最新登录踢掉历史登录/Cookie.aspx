<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Cookie.aspx.cs" Inherits="Mydemo_同一用户最新登录踢掉历史登录_Cookie" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
</head>
<body>
    <form id="form1" runat="server">
    <div>
        <asp:Button ID="BtnLogin" runat="server" Text="test" 
            onclick="BtnLogin_Click" />
        <br />
        <asp:Literal ID="LitLoginMessage" runat="server"></asp:Literal>
        <br />
        -----------------------------<br />
        application 所有内容：<br />
        <asp:Literal ID="LitApplication" runat="server"></asp:Literal>
    </div>
    </form>
</body>
</html>
