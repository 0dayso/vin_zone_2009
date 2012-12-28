using System;
using System.Web;
using System.Web.Services;
using System.Web.Services.Protocols;
using System.Data;
using System.Data.SqlClient;
using System.IO;
using System.Xml;

[WebService(Namespace = "http://tempuri.org/")]
[WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
public class Service : System.Web.Services.WebService
{

    public Service()
    {
        //如果使用设计的组件，请取消注释以下行 
        //InitializeComponent(); 
    }
    private void InitializeComponent()
    {

    }

    [WebMethod]
    public string HelloWorld()
    {
        return "Hello World";
    }

    #region 获取client数据 加载到本地
    [WebMethod]
    public bool getXml(DataSet dataSet)
    {
        bool isExist = File.Exists(@"C:\dsData.txt");

        if (!isExist)
        {
            using (File.Create(@"C:\dsData.txt"))
            {

            }
        }

        using (StreamWriter sw = File.AppendText(@"C:\dsData.txt"))
        {
            foreach (DataRow dr in dataSet.Tables[0].Rows)
            {
                sw.WriteLine(Convert.ToString(dr[0]));
            }
            sw.Flush();
            sw.Close();
        }
        return true;
    }
    #endregion

    #region 获取ser数据 提供给cli调用.
    [WebMethod]
    public XmlDocument sendXml()
    {
        XmlDocument sendXML = new XmlDocument();
        DataTable dt = CreateDataSource();
        DataSet ds = new DataSet();
        ds.Tables.Add(dt);
        sendXML.LoadXml(ds.GetXml());
        return sendXML;
    }

    #endregion

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

}
