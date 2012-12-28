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

public partial class 多维表头_MutilGridHeader : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            bingGrid();
            bingRepeater();
        }
    }

    /// <summary>
    /// 创建数据源
    /// </summary>
    /// <returns></returns>
    private DataTable CreateDataSource()
    {
        DataTable dt = new DataTable();
        dt.Columns.Add("id");
        dt.Columns.Add("name");
        dt.Columns.Add("sex");
        dt.Columns.Add("age");
        dt.Rows.Add("1", "lt", "male", "00");
        dt.Rows.Add("2", "lt", "male", "00");
        dt.Rows.Add("3", "lt", "male", "00");
        dt.Rows.Add("4", "lt", "male", "00");
        dt.Rows.Add("5", "lt", "male", "00");
        dt.Rows.Add("6", "lt", "male", "00");
        dt.Rows.Add("7", "lt", "male", "00");
        dt.Rows.Add("8", "lt", "male", "00");
        dt.Rows.Add("9", "lt", "male", "00");
        dt.Rows.Add("10", "lt", "male", "00");
        dt.Rows.Add("11", "lt", "male", "00");
        dt.Rows.Add("12", "lt", "male", "00");
        dt.Rows.Add("13", "lt", "male", "00");
        dt.Rows.Add("14", "lt", "male", "00");
        dt.Rows.Add("15", "lt", "male", "00");
        dt.Rows.Add("16", "lt", "male", "00");
        dt.Rows.Add("17", "lt", "male", "00");
        dt.Rows.Add("18", "lt", "male", "00");
        dt.Rows.Add("19", "lt", "male", "00");
        dt.Rows.Add("20", "lt", "male", "00");
        dt.Rows.Add("21", "lt", "male", "00");
        return dt;
    }



    void bingGrid()
    {
        this.GridView1.DataSource = CreateDataSource();
        this.GridView1.DataBind();
    }

    PagedDataSource pds = new PagedDataSource();
    bool allowPage = true;
    //分页
    void bingRepeater()
    {
        pds.DataSource = CreateDataSource().DefaultView;
        //pds.AllowCustomPaging = true;
        pds.AllowPaging = allowPage;
        //pds.AllowServerPaging = true;
        pds.PageSize = 3;

        int currentPage = Convert.ToInt32(Request["page"]);

        //设当前页
        pds.CurrentPageIndex = currentPage;

        //设几个超链接
        if (!pds.IsFirstPage)
        {
            lnkUp.NavigateUrl = Request.CurrentExecutionFilePath + "?page=" + (currentPage - 1);
        }

        if (!pds.IsLastPage)
        {
            lnkDown.NavigateUrl = Request.CurrentExecutionFilePath + "?page=" + (currentPage + 1);
        }


        lbl_info.Text = "第" + (currentPage + 1) + "页、共" + pds.PageCount + "页";

        Repeater1.DataSource = pds;
        Repeater1.DataBind();


    }


    protected void GridView1_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        this.GridView1.PageIndex = e.NewPageIndex;
        bingGrid();
    }

    protected void btnToExcel_Click(object sender, EventArgs e)
    {
        Response.Charset = "GB2312";
        Response.ContentEncoding = System.Text.Encoding.GetEncoding("GB2312");
        Response.AddHeader("content-disposition", "attachment; filename=" + HttpUtility.UrlEncode("test" + DateTime.Now.ToShortDateString() + ".xls", System.Text.Encoding.UTF8));

        Response.ContentType = "application/ms-excel";
        Response.Write("<meta http-equiv=Content-Type content=\"text/html; charset=GB2312\">");
        System.IO.StringWriter tw = new System.IO.StringWriter();
        allowPage = false;
        bingRepeater();

        HtmlTextWriter hw = new HtmlTextWriter(tw);
        this.Repeater1.RenderControl(hw);
        Response.Write(tw.ToString());
        allowPage = true;
        bingRepeater();
        Response.End();
    }
}
