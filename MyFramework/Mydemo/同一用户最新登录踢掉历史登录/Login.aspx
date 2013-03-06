<%@ Page Language="C#"  AutoEventWireup="true" CodeFile="Login.aspx.cs" Inherits="Mydemo_同一用户最新登录踢掉历史登录_Login" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
</head>
<body>
    <form id="form1" runat="server">
    <div>
        <asp:Button ID="BtnLogin" runat="server" Text="Login" 
            onclick="BtnLogin_Click" />
        <asp:TextBox ID="TxtUserId" runat="server"></asp:TextBox>
    </div>
    </form>
</body>
</html>
