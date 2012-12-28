<%@ page language="C#" autoeventwireup="true" inherits="GridViewPager, App_Web_0pu2bb2o" %>
<%@ Register Assembly="Pager" Namespace="FeliControls" TagPrefix="Feli" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml" >
<head runat="server">
    <title>无标题页</title>
</head>
<body>
    <form id="form1" runat="server">
    <div id="DIV1">
        <asp:GridView ID="GridView1" runat="server" OnPageIndexChanged="GridView1_PageIndexChanged" OnPageIndexChanging="GridView1_PageIndexChanging">
        </asp:GridView>
        &nbsp;<asp:Button ID="Button1" runat="server" OnClick="Button1_Click" Text="Button" /><br />
        &nbsp;<Feli:Pager ID="Pager1" runat="server" OnPageIndexChanged="Pager1_PageIndexChanged" Width="452px" />
    </div>
    </form>
</body>
</html>
