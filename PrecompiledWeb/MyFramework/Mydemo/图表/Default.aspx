<%@ page language="C#" autoeventwireup="true" inherits="Mydemo_图表_Default, App_Web_fa42qlev" %>

<%@ Register assembly="MyFramework.Component" namespace="MyFramework.Component" tagprefix="cc1" %>

<%@ Register assembly="DundasWebChart" namespace="Dundas.Charting.WebControl" tagprefix="DCWC" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
</head>
<body>
    <form id="form1" runat="server">
    <div>
    
    </div>
    <DCWC:Chart ID="Chart1" runat="server">
        <series>
            <DCWC:Series Name="Default_AutoRunWizard">
                <SmartLabels Enabled="True" />
            </DCWC:Series>
              <DCWC:Series Name="aa">
                <SmartLabels Enabled="True" />
            </DCWC:Series>
            <DCWC:Series Name="bb">
                <SmartLabels Enabled="True" />
            </DCWC:Series>
         
          
        </series>
        <chartareas>
            <DCWC:ChartArea Name="Default">
            </DCWC:ChartArea>
        </chartareas>
<Legends>
<DCWC:Legend Name="Default"></DCWC:Legend>
</Legends>
    </DCWC:Chart>
    
    </form>
</body>
</html>
