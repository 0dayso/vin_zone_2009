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

public partial class _Default : System.Web.UI.Page
{
    
    protected void Page_Load(object sender, EventArgs e)
    {
        AjaxPro.Utility.RegisterTypeForAjax(typeof(_Default));//AjaxTest是类名
    }  
    [AjaxPro.AjaxMethod]
    public string GetTime()
    {
        return DateTime.Now.ToString();
    }

    //
    protected void Button1_Click(object sender, EventArgs e)
    {
        this.TextBox1.Text = GetServerIp();
    }

    protected string GetServerIp()
    {
        string userIP;

        HttpRequest Request = HttpContext.Current.Request;
        // 如果使用代理，获取真实IP
        if (Request.ServerVariables["HTTP_X_FORWARDED_FOR"] == "")
            userIP = Request.ServerVariables["REMOTE_ADDR"];
        else
            userIP = Request.ServerVariables["HTTP_X_FORWARDED_FOR"];
        if (userIP == null || userIP == "")
            userIP = Request.UserHostAddress;
        return userIP;
    }
}
