using System;
using System.Collections.Generic;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using FeliControls;


public partial class _Default : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        this.Pager1.PageSize = 20;
        this.Pager1.RecordCount = 600;
    }


    protected void Pager1_PageIndexChanged(object sender, PageChangedEventArgs e)
    {
        Response.Write(e.CurrentPageIndex);
    }

    protected void GridView1_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {

    }
}