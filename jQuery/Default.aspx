<%@ Page Language="C#" AutoEventWireup="true"  CodeFile="Default.aspx.cs" Inherits="_Default" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml" >
<head runat="server">
    <title>jQuery</title>
    <script src="Scripts/jquery-1.5.min.js" type="text/javascript"></script>
        <script type="text/javascript" >
    jQuery(function() {
        $('#btnClick').click(function(){
            jQuery.ajax({
                type : "GET",
                url : "Data.ashx",
                data : "MethodName=GetData",
                success : function(data){
                    $('#display').html("<h1> Hi, " + data.FirstName + " " + data.LastName + " your Blog Address is http://" + data.Blog + "</h1>");
                   }
        }); 
        });
    });
</script>
</head>
<body>
    <form id="form1" runat="server">
    <input id="btnClick" type="button" value="Ajax Call" />
    <div id="display">
    </div>
        
    </form>
</body>
</html>
