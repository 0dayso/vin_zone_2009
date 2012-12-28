<%@ Page Language="C#" AutoEventWireup="true" CodeFile="DialogFrame.aspx.cs" Inherits="SystemFrame_DialogFrame" %>
<html xmlns="http://www.w3.org/1999/xhtml" >
<head runat="server">
    <title>对话窗口</title>
		<script language="javascript">	
        function GenControlScript(toWindow, tsControlNames){
            var lsControlNames = tsControlNames.split(",");
            var lsScript = "";
            var loControl = null;
            for(var lnIndex=0; lnIndex<lsControlNames.length; lnIndex++){
	            loControl = toWindow.document.all[lsControlNames[lnIndex]];
	            if(loControl!=null){
		            if(loControl.name==lsControlNames[lnIndex]){
			            lsScript+=loControl.outerHTML;
		            }else{
			            if(loControl.length!=null){
				            for(var lnControlIndex = 0; lnControlIndex<loControl.length; lnControlIndex++){
					            lsScript+=loControl[lnControlIndex].outerHTML;
				            }
			            }
		            }
	            }
            }
            return lsScript;
        }

        function LoadPage(){
            var loFrame = document.all["DialogContent"];
            window.opener = window.dialogArguments.Opener;
            if(window.dialogArguments.InputParameters==null||window.dialogArguments.InputParameters==""){
	            loFrame.src = window.dialogArguments.Url;
            }else{
	            var loDocument = loFrame.contentWindow.document;
        		
	            var lsScript = "";
	            lsScript += "<html><bo"+"dy>";
	            lsScript += "<form action=\""+window.dialogArguments.Url+"\" method=post style=\"VISIBILITY: hidden\">";
	            lsScript += GenControlScript(window.opener, window.dialogArguments.InputParameters);
	            lsScript += "</form>";
	            lsScript += "<scrip"+"t language=javascrip"+"t>";
	            lsScript += "function SubmitForm(){ document.forms[0].submit();}";
	            lsScript += "</scrip"+"t>";
	            lsScript += "</bo"+"dy></html>";
	            loDocument.write(lsScript);
	            loFrame.contentWindow.SubmitForm();	
            }
        }	

		</script>
	</HEAD>
	<body onload="LoadPage();">
		<form id="Form1" method="post" runat="server" >
			<iframe id="DialogContent" width="100%" height="100%" frameborder=0  ></iframe>
		</form>
	</body>
</HTML>