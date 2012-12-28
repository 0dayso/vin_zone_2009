<%@ Page Language="C#" AutoEventWireup="true" CodeFile="JQuery_First.aspx.cs" Inherits="_Default" %>

<%@ Register Assembly="Pager" Namespace="FeliControls" TagPrefix="Feli" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
    <script src="Scripts/jquery-1.4.1-vsdoc.js" type="text/javascript"></script>
    <style type="text/css">
        .postBody p, .postCon p
        {
            text-indent: 0px;
        }
        
        
        
        .postBody p, .postCon p
        {
            margin: 5px auto 5px auto;
        }
        
        .postBody p, .postCon p
        {
            margin: 0 auto 1em auto;
        }
        p
        {
            margin: 5px auto 5px auto;
            text-indent: 0px;
        }
        
        *
        {
            margin: 0;
            padding: 0;
        }
    </style>
    <script type="text/javascript" language="javascript">
        function JujeSel()
        {
            // var s = document.all.DropDownList1.value;
            var s = $("#DropDownList1").val();
            if (s == 2 || s == 3)
            {
                alert("s is " + s);
            }
         

        }
    </script>
</head>
<body>
    <form id="form1" runat="server">
    <div>
    </div>
    <asp:GridView ID="GridView1" runat="server" OnPageIndexChanging="GridView1_PageIndexChanging">
    </asp:GridView>
    <asp:DropDownList ID="DropDownList1" runat="server">
        <asp:ListItem Value="1">11</asp:ListItem>
        <asp:ListItem Value="2">22</asp:ListItem>
        <asp:ListItem Value="3">33</asp:ListItem>
        <asp:ListItem Value="4">44</asp:ListItem>
    </asp:DropDownList>
    <input id="Button1" type="button" onclick="JujeSel()" value="button" />
    <Feli:Pager ID="Pager1" runat="server" OnPageIndexChanged="Pager1_PageIndexChanged" />
    </form>
</body>
</html>
