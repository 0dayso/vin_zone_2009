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
using System.IO;
using MyFramework.BusinessLogic.Common;

public partial class 二进制_Default : BasePage
{
    protected void Page_Load(object sender, EventArgs e)
    {

    }
    protected void btnImport_Click(object sender, EventArgs e)
    {
        ExcelImport();
    }

    private void ExcelImport()
    {
        string strPath = fileSelect.FileName;

        if (!ValidImportFile(strPath))
        {
            return;
        }

        strPath = GetUpLoadPath(strPath);

        try
        {
            //保存
            this.fileSelect.PostedFile.SaveAs(strPath);

            StreamReader sr = new StreamReader(strPath);
            char [] aa=   sr.ReadToEnd().ToCharArray();
            for (int i = 0; i < aa.Length;i++ )
            {
                Response.Write(Convert.ToString(aa[i],2));
            }
            
 
        }
        //catch
        //{

        //}
        finally
        {
            //删除
            if (System.IO.File.Exists(strPath))
            {
             //   System.IO.File.Delete(strPath);
            }
        }
    }


    /// <summary>
    /// 验证要导入的文件
    /// </summary>
    /// <param name="strPath"></param>
    /// <returns></returns>
    private bool ValidImportFile(string strPath)
    {

        if (string.IsNullOrEmpty(strPath))
        {
            Page.ClientScript.RegisterStartupScript(this.GetType(), "", "alert('请选择文件路径！');", true);
            return false;
        }

        //if (!string.Equals(System.IO.Path.GetExtension(strPath), ".xls"))
        //{
        //    Page.ClientScript.RegisterStartupScript(this.GetType(), "", "alert('请选择导入Excel文件！');", true);
        //    return false;
        //}
        return true;
    }

    /// <summary>
    /// 获取上传路径
    /// </summary>
    /// <param name="strFileName">文件路径</param>
    /// <returns></returns>
    private string GetUpLoadPath(string strFileName)
    {
        int index = strFileName.LastIndexOf(".");
        strFileName = strFileName.Substring(0, index) + "_" + DateTime.Now.ToString("yyyyMMddhhmmss") + strFileName.Substring(index);
        return Server.MapPath("/MyFramework/Upload") + "/" + strFileName;
    }
}
