using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using MyFramework.BusinessLogic.Common;
using System.Collections;

public partial class Mydemo_同一用户最新登录踢掉历史登录_Default : BasePage
{
    protected void Page_Load(object sender, EventArgs e)
    {

    }

    protected void Button1_Click(object sender, EventArgs e)
    {
        string userId = HttpContext.Current.User.Identity.Name;
        if (this.isForce)
        {
            this.LitLogin.Text = HttpContext.Current.User.Identity.Name + " has logged in!";
        }
        else
        {
            this.LitLogin.Text = "welcome " + HttpContext.Current.User.Identity.Name + "!";
            SaveLoginUser(userId);
        }
        Button1.Enabled = false;
    }

    void SaveLoginUser(string userId)
    {
        Hashtable hOnline = (Hashtable)Application["Online"];
        hOnline[userId] = Session.SessionID;
        if (hOnline != null)
        {
            //hOnline.con
            IDictionaryEnumerator idE = hOnline.GetEnumerator();
            string strKey = "";
            while (idE.MoveNext())
            {
                if (idE.Value != null && idE.Value.ToString().Equals(userId))
                {
                    //already login
                    strKey = idE.Key.ToString();
                    hOnline[strKey] = "XXXXXX";
                    break;
                }
            }
        }
        else
        {
            hOnline = new Hashtable();
        }

        hOnline[Session.SessionID] = userId;
        Application.Lock();
        Application["Online"] = hOnline;
        Application.UnLock();
    }

    protected void Button2_Click(object sender, EventArgs e)
    {

    }
}