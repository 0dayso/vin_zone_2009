<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Default.aspx.cs" Inherits="Mydemo_缓存_Default" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
</head>
<body>
    <form id="form1" runat="server">
    <div>
        显示数据，无缓存显示输入框内容，有则显示缓存数据。<br />
        &nbsp;<asp:Button ID="Button1" runat="server" Text="test" OnClick="Button1_Click" />
        <asp:TextBox ID="TextBox1" Text="test" runat="server"></asp:TextBox>
        <asp:Literal ID="LitMessage" runat="server"></asp:Literal>
        <br />
        <asp:Button ID="Button2" runat="server" Text="remove all cache" OnClick="Button2_Click" />
        <br />
        <br />
        <br />
        <br />
        <br />
        <br />
        <br />
        <br />
        <br />
        <br />
        <br />
        <br />
        <br />
        <br />
    </div>
    </form>
</body>
</html>
