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

using System.Net;
using System.IO;
public partial class 新文件夹1_Default : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {

    }

    protected void Button1_Click(object sender, EventArgs e)
    {
        string myuri = "http://localhost:2533/MyFramework/showXML/showXML.aspx";
        WebRequest webr = WebRequest.Create(myuri);    
        DataSet ds = new DataSet();
        Stream str = webr.GetResponse().GetResponseStream();
        System.Xml.XmlDocument menuds = new System.Xml.XmlDocument();               
        ds.ReadXml(str);
        this.GridView1.DataSource = ds;
        this.GridView1.DataBind();
    }
}
