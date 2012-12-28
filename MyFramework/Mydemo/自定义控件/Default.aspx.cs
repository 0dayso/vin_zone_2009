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


public partial class 自定义控件_Default :BasePage
{
    protected void Page_Load(object sender, EventArgs e)
    {
        this.WebTextBox1.drpList.Items.Add(new ListItem("万恶介绍费", "1"));
        this.WebTextBox1.drpList.Items.Add(new ListItem("米己饥己溺随碟附送", "2"));
    }
    protected void Button1_Click(object sender, EventArgs e)
    {

    }
}
