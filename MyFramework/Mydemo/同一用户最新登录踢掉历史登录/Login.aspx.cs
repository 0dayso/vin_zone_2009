using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using MyFramework.BusinessLogic.Common;

public partial class Mydemo_同一用户最新登录踢掉历史登录_Login : MyFramework.BusinessLogic.Common.BasePage
{

    protected void Page_Load(object sender, EventArgs e)
    {

    }

    protected void BtnLogin_Click(object sender, EventArgs e)
    {
        Session["onlineUserID"] = this.TxtUserId.Text.Trim() == "" ? HttpContext.Current.User.Identity.Name : this.TxtUserId.Text.Trim();
        SaveLoginUser(Convert.ToString(Session["onlineUserID"]));
        
        Response.Redirect("Cookie.aspx");
    }

    void SaveLoginUser(string userId)
    {
        /* 登录用户存入全局Application对象中,
          *  如果已存在,则修改系统之前分配的Guid标识
         */
        bool isNewApp = false;
        string m_strUserOnlineID = DateTime.Now.ToString() + "," + Session.SessionID;// Guid.NewGuid().ToString();
        HttpCookie onlineID = null;
        Dictionary<string, string> userlist;

        if (Application["OnlineUserList"] == null)
        {
            userlist = new Dictionary<string, string>();
            isNewApp = true;
        }
        else
        {
            userlist = Application["OnlineUserList"] as Dictionary<string, string>;
        }

        if (userlist.ContainsKey(userId))
        {
            //m_strUserOnlineID = Guid.NewGuid().ToString();
            //onlineID = new HttpCookie(HttpContext.Current.User.Identity.Name);
            //onlineID.Value = m_strUserOnlineID;
            //Response.Cookies.Add(onlineID);
            Response.Redirect("Online.aspx");
        }
        else
        {
            userlist.Add(userId, m_strUserOnlineID);
        }

        if (isNewApp)
        {
            Application.Add("OnlineUserList", userlist);
        }
        else
        {
            //更新
            Application["OnlineUserList"] = userlist;
        }
        onlineID = new HttpCookie(Convert.ToString(Session["onlineUserID"]));
        onlineID.Value = m_strUserOnlineID;
        Response.Cookies.Add(onlineID);
        //WebGlobal.SetCookie("UserOnlineID", m_strUserOnlineID);


    }
}