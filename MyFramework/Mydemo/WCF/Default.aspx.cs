using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

using MyFramework.WebAppService;



public partial class Mydemo_WCF_Default : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        WebAppServiceType webService = new WebAppServiceType();
        if (webService.Login("aa", "bb"))
        {
            Response.Write("登陆成功！");
        }
    }
}