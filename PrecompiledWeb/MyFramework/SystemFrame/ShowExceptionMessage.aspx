<%@ page language="C#" autoeventwireup="true" inherits="ShowExceptionMessage, App_Web_1o1tgpfx" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml" >
 <HEAD>
		<title>Show Exception Message</title>
  </HEAD>
	<body>
		<form id="frmShowExceptionMessage" method="post" runat="server">
			<table width="100%">
				<tr>
					<td height="15"></td>
				</tr>
				<tr>
					<td align="center">
                        <asp:GridView  id="grdMessage" runat="server" AutoGenerateColumns="False" Width="450px" ShowHeader="False"
							GridLines="Horizontal" OnSelectedIndexChanged="grdMessage_SelectedIndexChanged">
                            <Columns>                             
                                <asp:ButtonField Text="&gt;" />
                                <asp:BoundField DataField="Message"></asp:BoundField>
                            </Columns>
                        </asp:GridView>
					</td>
				</tr>
			</table>
			<table cellSpacing="0" cellPadding="0" id="ButtonTable" runat="server" align="center">
				<tr>
					<td class="bmsButtonAreaPosition"></td>
				</tr>
				<tr>
					<td id="tdBack" runat="server"><table cellpadding="0" cellspacing="0">
							<tr>
								<td></td>
								<td class="bmsButtonSeperation">
									<input type="button" id="btnBack" runat="server" name="btnBack" value="返回" onclick="window.history.go(-2);" class=mnfCommandButton60 onserverclick="btnBack_ServerClick">
									<input type="button" id="btnClose" runat="server" name="btnClose" value="关闭" onclick="window.close();" class=mnfCommandButton60></td>
							</tr>
						</table>
					</td>
					
				</tr>			
			</table>

		</form>
	</body>
</HTML>
