using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

using iis_webapp;

public partial class Mydemo_WCF_Default2 : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        iis_webapp.WebAppServiceType webapp = new WebAppServiceType();

        bool a, b;
        webapp.Login("aa", "bb",out a,out b );

        if (a)
        {
            Response.Write("登陆成功！");
        }
    }
}