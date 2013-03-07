using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using MyFramework.BusinessLogic.Common;

public partial class Mydemo_同一用户最新登录踢掉历史登录_Cookie : BasePage
{
    protected void Page_Load(object sender, EventArgs e)
    {
        this.LitLoginMessage.Text = "welcome : " + Convert.ToString(Session["onlineUserID"]);
        var strCookie = this.Page.Request.Cookies[Convert.ToString(Session["onlineUserID"])] == null ? "无 cookie" : this.Page.Request.Cookies[Convert.ToString(Session["onlineUserID"])].Value.Split(',')[0];
        this.LitLoginMessage.Text += "<br/> cookie :" + strCookie;
        if (isForce)
        {
            MessageBox("你的帐号已在别处登陆，你被强迫下线！", this.Page, "LoginOut.aspx");
            this.LitLoginMessage.Text = "你的帐号已在别处登陆";
        }
        Dictionary<string, string> userList = new Dictionary<string, string>();
        userList = Application["OnlineUserList"] as Dictionary<string, string>;
        //foreach (string key in userList.Keys)
        //{
        //    this.LitApplication.Text = "<br/>" + key + ":" + userList[key];
        //}
        if (!IsPostBack)
        {
            //遍历字典
            foreach (KeyValuePair<string, string> kvp in userList)
            {
                this.LitApplication.Text += "<br/>" + kvp.Key + ":" + kvp.Value;
            }
        }
    }

    protected void BtnLogin_Click(object sender, EventArgs e)
    {
        // SaveLoginUser(HttpContext.Current.User.Identity.Name);
    }
}