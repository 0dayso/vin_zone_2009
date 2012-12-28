<%@ Page Language="C#" AutoEventWireup="true" CodeFile="AjaxPro.aspx.cs" Inherits="_Default" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>无标题页</title>

    <script language="javascript" type="text/javascript">
     function aa() 
     {
      
var s=_Default.GetTime().value;//AjaxTest是类名，GetTime是方法 
  //alert(s);
var a= document.getElementById("Text1");
a.value=s;
 
 
}


   
    
    </script>

</head>
<body>
    <form id="form1" runat="server">
        <div>
            <br />
            <span style="color: #ff9900">
            AjaxPro</span><br />
            <input id="Text1" onclick="aa()" type="text" />
            &nbsp; 单击文本框时更新时间(无刷新)<br  />
            <hr />
        </div>
        <asp:TextBox ID="TextBox1" runat="server"></asp:TextBox>
        <asp:Button ID="btnGetIP" runat="server" OnClick="Button1_Click" Text="获取ip" />
    </form>
</body>
</html>
