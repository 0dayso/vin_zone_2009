<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Session.aspx.cs" Inherits="Mydemo_同一用户最新登录踢掉历史登录_Default" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
</head>
<body>
    <form id="form1" runat="server">
    <div>
    
        <asp:Button ID="Button1" runat="server" Text="BtnLogin" 
            onclick="Button1_Click" />
        <asp:Literal ID="LitLogin" runat="server"></asp:Literal>
    
        <br />
        <asp:Button ID="Button2" runat="server" onclick="Button2_Click" Text="Button test" />
    
    </div>
    </form>
</body>
</html>
