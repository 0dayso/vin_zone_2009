using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Mydemo_同一用户最新登录踢掉历史登录_LoginOut : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        Session.RemoveAll();
        foreach (string cookieName in Request.Cookies.AllKeys)
        {
            HttpCookie cookie = Request.Cookies[cookieName];
            cookie.Expires = DateTime.Today.AddDays(-5);
            Response.Cookies.Add(cookie);
            Response.Redirect("login.aspx");
        }        
    }
}