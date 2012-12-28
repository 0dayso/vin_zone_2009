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
using System.Data.OracleClient;
using Pager;
using FeliControls;

public partial class GridViewPager : System.Web.UI.Page
{
    public int count = 0;
    protected void Page_Load(object sender, EventArgs e)
    {
        GetListByPage(1);
        //this.GridView1.DataSource = GetListByPage();
        //this.GridView1.DataBind();
        this.Pager1.PageSize = 20;
        this.Pager1.RecordCount = count;
    }
    /// <summary>
    /// 分页获取数据列表
    /// </summary>
    public void GetListByPage(int p_curPage)
    {
        Response.Write(DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "</br>");
        string dbcon = ConfigurationManager.ConnectionStrings["dbcon"].ToString();
        OracleConnection oraCon = new OracleConnection(dbcon);
        oraCon.Open();
        OracleParameter[] parameters = {
                    new OracleParameter("p_tableName", OracleType.NVarChar,50),
                    new OracleParameter("p_strWhere", OracleType.NVarChar,50),
                    new OracleParameter("p_orderColumn", OracleType.NVarChar,50),
                    new OracleParameter("p_orderStyle", OracleType.NVarChar,50),
                    new OracleParameter("p_curPage", OracleType.Number),
                    new OracleParameter("p_pageSize", OracleType.Number),
                    new OracleParameter("p_fields", OracleType.NVarChar,50),
                    new OracleParameter("p_totalRecords", OracleType.Number),
                    new OracleParameter("p_totalPages", OracleType.Number),
                    new OracleParameter("v_cur",OracleType.Cursor)};
        parameters[0].Direction = ParameterDirection.Input;
        parameters[1].Direction = ParameterDirection.Input;
        parameters[2].Direction = ParameterDirection.Input;
        parameters[3].Direction = ParameterDirection.Input;
        parameters[4].Direction = ParameterDirection.Input;
        parameters[5].Direction = ParameterDirection.Input;
        parameters[6].Direction = ParameterDirection.Input;
        parameters[7].Direction = ParameterDirection.Output;
        parameters[8].Direction = ParameterDirection.Output;
        parameters[9].Direction = ParameterDirection.Output;
        parameters[0].Value = "T_HO_ORDER_INFO";
        parameters[1].Value = "1=1";
        parameters[2].Value = "order_id";
        parameters[3].Value = "desc";
        parameters[4].Value = p_curPage;
        parameters[5].Value = 20;
        parameters[6].Value = "order_id";
        OracleCommand oraCom = new OracleCommand("PCK_System.USP_GetRecordByPage", oraCon);
        oraCom.CommandType = CommandType.StoredProcedure;
        oraCom.Parameters.AddRange(parameters);
        DataTable dt = new DataTable();
        oraCom.ExecuteNonQuery();
        OracleDataReader oraReader;
        oraReader = (OracleDataReader)parameters[9].Value;
        count = Convert.ToInt32(parameters[7].Value);
        dt.Load(oraReader);
        this.GridView1.DataSource = dt;
        this.GridView1.DataBind();
        Response.Write(DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss" )+ "</br>");
        oraCon.Close();
        oraCon.Dispose();
        oraCon = null;
    }
    protected void Button1_Click(object sender, EventArgs e)
    {
        //this.GridView1.DataSource = GetListByPage();
        //this.GridView1.DataBind();
        //string dbcon = ConfigurationManager.ConnectionStrings["dbcon"].ToString();
        //OracleConnection Oraclecon = new OracleConnection(dbcon);
        //Oraclecon.Open();
        //OracleCommand myCMD = new OracleCommand();
        //myCMD.Connection = Oraclecon;
        //myCMD.CommandText = "curspkg_join.open_join_cursor1";
        //myCMD.CommandType = CommandType.StoredProcedure;
        //myCMD.Parameters.Add(new OracleParameter("io_cursor", OracleType.Cursor)).Direction = ParameterDirection.Output;
        //myCMD.Parameters.Add("n_Empno", OracleType.Number, 4).Value = 123;
        //OracleDataReader myReader;
        //myCMD.ExecuteNonQuery();
        //myReader = (OracleDataReader)myCMD.Parameters["io_cursor"].Value;
        //while (myReader.Read())
        //{
        //    for (int x = 0; x < myReader.FieldCount - 1; x++)
        //    {
        //        Response.Write("列" + myReader[x] + ":" + myReader[x]);
        //    }
        //}
        //myReader.Close();
        //Oraclecon.Close();
    }
    protected void Button2_Click(object sender, EventArgs e)
    {
    }
    protected void GridView1_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
    }
    protected void GridView1_PageIndexChanged(object sender, EventArgs e)
    {
    }
    protected void Pager1_PageIndexChanged(object sender, PageChangedEventArgs e)
    {
        GetListByPage(e.CurrentPageIndex);
    }
}
