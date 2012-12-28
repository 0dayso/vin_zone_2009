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
using doxml;

public partial class _Default : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {

    }
    string fileName = string.Empty;
    protected void btnUpload_Click(object sender, EventArgs e)
    {
        //fullfileName获得全路径
        string fullfileName = this.fulXML.PostedFile.FileName ;
        string serPath = Server.MapPath("upfile/");
       

       // Page.ClientScript.RegisterStartupScript(this.GetType(), "", "alert('" + fullfileName + "');");
        //获得文件名
        fileName = fullfileName.Substring(fullfileName.LastIndexOf("\\") + 1);
        this.fulXML.SaveAs(serPath+fileName);

        //文件类型
        string fileType = fullfileName.Substring(fullfileName.LastIndexOf(".") + 1);
        if (this.fulXML.PostedFile.FileName != null)
        {
            if (fileType.ToLower() == "xml")
            {
                DataSet ds = new DataSet();
                ds.ReadXml(serPath + fileName);



                Service myService = new Service();
                //调用webService中的方法.
                myService.getXml(ds);

                this.lblMessage.Text = "数据插入成功,谢谢!";
            }
            else
            {
                this.lblMessage.Text = "请选择xml文件,谢谢!";
            }
        }
        else
        {
            this.lblMessage.Text = "请选择你要上传的文件,谢谢!";
        }


    }
}
