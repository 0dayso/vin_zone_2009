using System;
using System.Data;
using System.Configuration;
using System.Collections;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;

using doxml;
using System.Xml;

public partial class showXML : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        doxml.Service ser = new Service();
        XmlNode xml = ser.sendXml();      
        HttpContext.Current.Response.Clear();
        HttpContext.Current.Response.ContentType = "text/xml";
        HttpContext.Current.Response.Charset = "UTF-8 ";
        XmlTextWriter writer = new XmlTextWriter(HttpContext.Current.Response.OutputStream, System.Text.Encoding.UTF8);
        writer.Formatting = Formatting.Indented;
        xml.WriteTo(writer);        
        writer.Flush();
        HttpContext.Current.Response.End();
    }
}
