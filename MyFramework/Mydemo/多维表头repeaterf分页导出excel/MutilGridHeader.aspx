<%@ Page Language="C#" AutoEventWireup="true" CodeFile="MutilGridHeader.aspx.cs"
    Inherits="多维表头_MutilGridHeader" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>无标题页</title>
    <link href="../CSS/SystemFrame.css" rel="stylesheet" type="text/css" />
</head>
<body>
    <form id="form1" runat="server">
        <div id="DIV1">
            <asp:GridView ID="GridView1" runat="server" AllowPaging="True" OnPageIndexChanging="GridView1_PageIndexChanging"
                PageSize="7">
            </asp:GridView>
        </div>
        <div>
            <asp:Repeater ID="Repeater1" runat="server">
                <HeaderTemplate>
                    <table border="1">
                        <tr>
                            <td colspan="2">
                                id and name</td>
                            <td colspan="2">
                                info age</td>
                        </tr>
                        <tr>
                            <td>
                                id</td>
                            <td>
                                name</td>
                            <td>
                                sex</td>
                            <td>
                                age</td>
                        </tr>
                </HeaderTemplate>
                <ItemTemplate>
                    <tr>
                        <td>
                            <%# Eval("id") %>
                        </td>
                        <td>
                            <%# Eval("name") %>
                        </td>
                        <td>
                            <%# Eval("sex") %>
                        </td>
                        <td>
                            <%# Eval("age") %>
                        </td>
                    </tr>
                </ItemTemplate>
                <FooterTemplate>
                    </table>
                </FooterTemplate>
            </asp:Repeater>
            <table>
                <tr>
                    <td colspan="4">
                        <asp:HyperLink ID="lnkUp" runat="server">上一页</asp:HyperLink><asp:HyperLink ID="lnkDown"
                            runat="server">下一页</asp:HyperLink><asp:Label ID="lbl_info" runat="server" Text="当前第x页,共x页"></asp:Label>
                    </td>
                </tr>
            </table>
        </div>
        <asp:Button ID="btnToExcel" runat="server" Text="导出excel" OnClick="btnToExcel_Click" />
        repeater多维表头,repeater实现分页.</form>
</body>
</html>
