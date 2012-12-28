<%@ page language="C#" autoeventwireup="true" inherits="自定义控件_Default, App_Web_hit2x1dh" %>

<%@ Register Assembly="MyFramework.Component" Namespace="MyFramework.Component" TagPrefix="cc1" %>

 


<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>无标题页</title>
</head>
<body>
    <form id="form1" autocomplete="off" runat="server">
        <div>
            <br />
            <br />
            
            <table width="98%"  cellpadding="3" cellspacing="1"  class="bmsEditTable" >
                <tr>
                    <td class="bmsInputContent" style="width: 773px">
                        &nbsp;</td>
                    <td class="bmsInputContent">
                    </td>
                    
                </tr>
                <tr>
                    <td class="bmsInputContent" style="width: 773px">
                        <cc1:webtextbox id="WebTextBox1" runat="server" hasdropdownlist="True"><Value>
</Value>
</cc1:webtextbox>
                        文本框 可选择,各种验证.</td>
                    <td class="bmsInputContent">
                    </td>
                    
                </tr>
            
                <tr>
                    <td colspan="2" class="bmsInputContent"    style="text-align: center;" >
                        <asp:Button ID="Button1"   runat="server" CssClass="mnfCommandButton60" OnClick="Button1_Click"
                            Text="Button" /></td>
                 
                </tr>
            </table>
        </div>
    </form>
</body>
</html>
