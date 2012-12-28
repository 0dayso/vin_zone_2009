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
using MyFramework.BusinessLogic.Common;
using System.Net;
public partial class login : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {

    }

    protected void btnLogIn_Click(object sender, ImageClickEventArgs e)
    {
        UserSession loUserSession = new UserSession(this.Session);
        this.Session.Clear();
        string Userhash = "lt";
        bool lsBool;
        string strUrl = Request.Url.AbsoluteUri.ToUpper().ToString();
        //if (strUrl.IndexOf("GC.JSJ.COM.CN/MyFramework/") != -1)
        //    lsBool = false;
        //else if (strUrl.IndexOf("219.238.247.212/MyFramework/") != -1)
        //    lsBool = false;
        //else
            lsBool = true;
            string lsMessage = loUserSession.Login(this.txtUserName.Text.Trim(), this.txtPassword.Text.Trim(), ref Userhash, lsBool);
        
        if (lsMessage != "")
        {
            this.divMessage.InnerText = lsMessage;
           InsertLoginLog(0);
        }
        else
        {
            InsertLoginLog(1);
            HttpCookie usercookie = new HttpCookie("User", Userhash);
            this.Response.Cookies.Add(usercookie);
            Response.Redirect("MainPage.aspx?TopMenuId=1");
        }
       
    }
    protected void InsertLoginLog(int Status)
    {
        try
        {
            string lsSQL = @"insert into T_S_CRM_LOGIN (CRM_LOG_ID,CRM_LOG_TIME,CRM_LOGIN_NAME,CRM_LOG_IP,CRM_LOG_STATUS,CRM_SERVER_IP)
                           values(" + Convert.ToInt32(CommonDBFunction.GenerateIdentity("T_S_CRM_LOGIN")) +
                                        " , to_date('" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "','yyyy-MM-DD HH24:MI:SS')" + ",'" +
                                        this.txtUserName.Text.Trim() + "','" +
                                        IP().ToString() + "'," +
                                        Status + ",'" +
                                        GetServerIp() + "')";

        MyFramework.DAL.DBA.ExecuteNonQuery(lsSQL);
        }
        catch
        { }
    }
    protected string GetServerIp()
    {
        string serverIP;
        HttpRequest Request = HttpContext.Current.Request;
        if (Request.Url.Host != "" && Request.Url.Host != null)
        {
            serverIP = Request.Url.Host.ToString();
        }
        else
        {
           serverIP= "0.0.0";
        }
        return serverIP;
    }

    protected string IP()
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
