<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Online.aspx.cs" Inherits="Mydemo_同一用户最新登录踢掉历史登录_Online" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
</head>
<body>
    <form id="form1" runat="server">
    <div>
        <span style="color: Red; margin-right: 10px;">当前用户已在其他浏览器或其他地方登录！</span><span style="margin-right: 10px;"><asp:Literal
            ID="LitCookie" runat="server"></asp:Literal></span>
        <asp:LinkButton ID="LinkButton1" runat="server" OnClick="LinkButton1_Click">强制下线</asp:LinkButton>
    </div>
    </form>
</body>
</html>
