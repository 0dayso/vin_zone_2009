<%@ page language="C#" autoeventwireup="true" inherits="JavaScriptDemo_Default, App_Web_xhkccaw2" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>无标题页</title>

    <script language="javascript" type="text/javascript" src="../JavaScript/Common/bsCalendarNew.js"></script>

    <script language="javascript" type="text/javascript">         
    function rep()
    {  
        //clipboardData.getData('text')获取剪切板中的数据。
        //clipboardData.setData('text','222')设置剪贴版的数据。
        clipboardData.setData('text',clipboardData.getData('text').replace(/[^\d]/g,''));
    } 
    </script>

</head>
<body>
    <form id="form1" runat="server">
        <div>
            <input id="Text1" type="text" onbeforepaste="rep();" />
            <asp:TextBox ID="TextBox1" runat="server" iscalendar="yes"></asp:TextBox>
        </div>
    </form>
</body>
</html>
