using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;

public partial class Mydemo_图表_Default : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
       

        this.Chart1.ChartAreas["Default"].CursorX.UserEnabled = true;
        this.Chart1.ChartAreas["Default"].AxisX.View.MinSize = 3;
         
        this.Chart1.ChartAreas["Default"].AxisX.View.Position = 10.0;
        this.Chart1.ChartAreas["Default"].AxisX.View.Size = 25.0;

        DataTable dt = CreateDataSource();

        this.Chart1.DataSource = dt;
        //柱形图
        this.Chart1.Series["aa"].ValueMembersY = "maleNum";
        this.Chart1.Series["bb"].ValueMembersY = "femaleNum";

        this.Chart1.Series["aa"].ValueMemberX = "calssID";
        this.Chart1.Series["bb"].ValueMemberX = "calssID";
      
        this.Chart1.DataBind();

        this.Chart1.ChartAreas["Default"].CursorX.UserEnabled = true;
        this.Chart1.ChartAreas["Default"].AxisX.View.MinSize = 3;
        this.Chart1.ChartAreas["Default"].AxisX.View.Size = 5;
        this.Chart1.Visible = true;

    
    

    }
    /// <summary>
    /// 创建数据源
    /// </summary>
    /// <returns></returns>
    private DataTable CreateDataSource()
    {
        DataTable dt = new DataTable("testTable");
        dt.Columns.Add("calssID");
        dt.Columns.Add("maleNum");
        dt.Columns.Add("femaleNum");
        dt.Columns.Add("age");
        dt.Rows.Add("1", 15, 20, "00");
        
        dt.Rows.Add("6", 10, 18, "00");
     
        dt.Rows.Add("3", 14, 22, "00");
       
        return dt;
    }
}