using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using MyFramework.BusinessLogic.Common;

public partial class Mydemo_同一用户最新登录踢掉历史登录_Online : BasePage
{
    
    protected void Page_Load(object sender, EventArgs e)
    {
        Dictionary<string, string>
            userlist = Application["OnlineUserList"] as Dictionary<string, string>;
        this.LitCookie.Text = "登录时间 :" + userlist[Convert.ToString(Session["onlineUserID"])].Split(',')[0];

    }

    protected void LinkButton1_Click(object sender, EventArgs e)
    {
        string m_strUserOnlineID = DateTime.Now.ToString() + "," + Session.SessionID;// Guid.NewGuid().ToString();

        Dictionary<string, string>
            userlist = Application["OnlineUserList"] as Dictionary<string, string>;

        userlist[Convert.ToString(Session["onlineUserID"])] = m_strUserOnlineID;

        Application["OnlineUserList"] = userlist;
        HttpCookie onlineID = new HttpCookie(Convert.ToString(Session["onlineUserID"]));
        onlineID.Value = m_strUserOnlineID;
        Response.Cookies.Add(onlineID);

        Response.Redirect("Cookie.aspx");
    }
}