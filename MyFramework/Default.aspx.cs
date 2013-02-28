using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using MyFramework.BusinessLogic.Common;


public partial class _Default : BasePage
{
    protected void Page_Load(object sender, EventArgs e)
    {


    }
    protected void Button1_Click(object sender, EventArgs e)
    {

    }
    protected void TextBox1_TextChanged(object sender, EventArgs e)
    {
        ShowMessage(CommonFunction.getPinYin(this.TextBox1.Text));
    }
}