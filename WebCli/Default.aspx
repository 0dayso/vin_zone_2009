<%@ Page Language="C#" AutoEventWireup="true"  CodeFile="Default.aspx.cs" Inherits="_Default" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml" >
<head runat="server">
    <title>无标题页</title>
</head>
<body>
    <form id="form1" runat="server">
    <div>
        &nbsp;<asp:FileUpload ID="fulXML" runat="server" />
        <asp:Label ID="lblMessage" runat="server"></asp:Label><br />
        <asp:Button ID="btnUpload" runat="server" OnClick="btnUpload_Click" Text="上传" />
    
    </div>
    </form>
</body>
</html>
