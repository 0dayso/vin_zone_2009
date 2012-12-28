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

public partial class SystemFrame_ShowMessage :BasePage
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!this.IsPostBack)
        {
            this.PageCaption = "提示信息";

            if (this.CallerSharedInfo != null)
            {
                this.lblMessage.Text = (string)this.CallerSharedInfo["TransferMessage"];
            }
            else
            {
                this.lblMessage.Text = this.Request["Message"];
            }
            this.lblMessage.ForeColor = System.Drawing.Color.Green;

        }
    }
}
