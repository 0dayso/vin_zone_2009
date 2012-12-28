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

public partial class GroupRowsDemo : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            bingGrid();
            bingRepeater();
        }
    }

    #region 静态数据datatable充当数据源
    /// <summary>
    /// 创建数据源
    /// </summary>
    /// <returns>dt</returns>
    private DataTable CreateDataSource()
    {
        DataTable dt = new DataTable();
        dt.Columns.Add("id");
        dt.Columns.Add("name");
        dt.Columns.Add("sex");
        dt.Columns.Add("age");
        dt.Rows.Add("1", "lt", "male", "00");
        dt.Rows.Add("1", "lt", "male", "00");
        dt.Rows.Add("1", "lt", "male", "00");
        dt.Rows.Add("4", "lt", "male", "00");
        dt.Rows.Add("4", "lt", "male", "00");
        dt.Rows.Add("4", "lt", "male", "00");
        dt.Rows.Add("7", "lt", "male", "00");
        dt.Rows.Add("7", "lt", "male", "00");
        dt.Rows.Add("7", "lt", "male", "00");
        dt.Rows.Add("7", "lt", "male", "00");
        dt.Rows.Add("11", "lt", "male", "00");
        dt.Rows.Add("11", "lt", "male", "00");
        dt.Rows.Add("11", "lt", "male", "00");
        dt.Rows.Add("11", "lt", "male", "00");
        dt.Rows.Add("11", "lt", "male", "00");
        dt.Rows.Add("16", "lt", "male", "00");
        dt.Rows.Add("17", "lt", "male", "00");
        dt.Rows.Add("17", "lt", "male", "00");
        dt.Rows.Add("17", "lt", "male", "00");
        dt.Rows.Add("20", "lt", "male", "00");
        dt.Rows.Add("20", "lt", "male", "00");
        return dt;
    }
    #endregion


    void bingGrid()
    {
        this.grvGroupRows.DataSource = CreateDataSource();
        this.grvGroupRows.DataBind();
        GroupRows(this.grvGroupRows, 0);
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
        this.grvGroupRows.PageIndex = e.NewPageIndex;
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


    protected void GridView1_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        //   GroupRows(this.grvGroupRows, 0);
    }


    /// <summary> 
    /// 合并GridView中某列相同信息的行（单元格） 
    /// </summary> 
    /// <param name="GridView1">GridView</param> 
    /// <param name="cellNum">第几列</param> 
    public static void GroupRows(GridView grv, int cellNum)
    {
        // i行控制器
        int i = 0, rowSpanNum = 1;
        while (i < grv.Rows.Count - 1)
        {
            GridViewRow gvr = grv.Rows[i];
            for (++i; i < grv.Rows.Count; i++)
            {
                GridViewRow gvrNext = grv.Rows[i];
                if (gvr.Cells[cellNum].Text == gvrNext.Cells[cellNum].Text)
                {
                    gvrNext.Cells[cellNum].Visible = false;
                    rowSpanNum++;
                }
                else
                {
                    gvr.Cells[cellNum].RowSpan = rowSpanNum;
                    rowSpanNum = 1;
                    break;
                }
                if (i == grv.Rows.Count - 1)
                {
                    gvr.Cells[cellNum].RowSpan = rowSpanNum;
                }
            }
        }
    }

    protected void btnToXML_Click(object sender, EventArgs e)
    {
        DataSet ds = new DataSet();
        DataTable dt = CreateDataSource();
        ds.Tables.Add(dt);
        System.Xml.XmlDocument menuds = new System.Xml.XmlDocument();                
        menuds.LoadXml(ds.GetXml());
        menuds.Save(@"C:\xmlData.xml");
    }
}
