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
using System.Data.OracleClient;

public partial class ShowExceptionMessage : BasePage
{
    string msPromptLevel = "1";

  
    public ShowExceptionMessage(): base()
    {
       // this.mbValidateSession = false;
    }
    private void Page_Load(object sender, System.EventArgs e)
    {
        
        if (this.Request["ShowHeader"] == "0")
        {
            this.IsShowPageHeader = false;
            this.btnBack.Visible = false;
        }
        else
        {
            this.btnClose.Visible = false;
        }
        msPromptLevel = System.Configuration.ConfigurationSettings.AppSettings["ExceptionPromptLevel"];
        if (msPromptLevel == "") msPromptLevel = "1";
       // this.PageCaption = "Error Message";
        System.Exception loException = (Exception)this.Session["Exception"];
        System.Data.DataTable loMessageDt = new DataTable();
        loMessageDt.Columns.Add("Message", typeof(string));
        if (loException != null)
        {
            while (loException != null)
            {
                if (loException is Exception)
                {
                 //   if (((VicException)loException).ExcpetionId >= 1000)
                 //   {
                        // User Error
                        loMessageDt.Rows.Add(new object[] { this.GetExceptionMessage(loException) });
                 //   }
                 //   else
                 //   {
                 //      // System Error
                 //      if (msPromptLevel == "2" || msPromptLevel == "3") loMessageDt.Rows.Add(new object[] { this.GetExceptionMessage(loException) });
                 //   }
                }
                else
                {
                    if (msPromptLevel == "2" || msPromptLevel == "3") loMessageDt.Rows.Add(new object[] { this.GetExceptionMessage(loException) });
                }
                loException = loException.InnerException;
            }
            if (loMessageDt.Rows.Count == 0)
            {
                loMessageDt.Rows.Add(new object[] { "未知错误. 如查看详细错误信息请与管理员联系." });
            }
        }
        else
        {
            loMessageDt.Rows.Add(new object[] { "未知错误. 会话已过期." });
        }
        this.grdMessage.DataSource = loMessageDt;
        this.grdMessage.DataBind();

        if (!IsPostBack)
        {
           // this.SaveLog();
        }
    }

    new string GetExceptionMessage(Exception loException)
    {
        string lsMessage = loException.Message;
        if (msPromptLevel == "3")
        {
            lsMessage += "\n" + loException.StackTrace;
        }

        return lsMessage.Replace("\n", "<br>");
    }


    protected void grdMessage_SelectedIndexChanged(object sender, EventArgs e)
    {

    }
    protected void btnBack_ServerClick(object sender, EventArgs e)
    {
        if (this.Session["CurrentUrl"]!=null)
            this.Response.Redirect(this.Session["CurrentUrl"].ToString());
        else
            this.Response.Redirect("/" + this.AppPath + "/SystemFrame/ShowExceptionMessage.aspx?ShowHeader=0", true);
    }

    protected void SaveLog()
    {

//        System.Exception loException = (Exception)this.Session["Exception"];
//        if (loException != null)
//        {
//            string lsErr = "";
//            string lsErrUrl = "";
//            if (this.Session["CurrentUrl"] != null)
//                lsErrUrl = this.Session["CurrentUrl"].ToString();
//            int lnLogID = (Int32)CommonDBFunction.GenerateIdentity("T_S_LOG"); //主键自动生成
//            int? lnUseID = null;
//            if (this.Session["UserSession"] != null)
//                lnUseID = this.UserSession.UserInfo.UserId;

//            while (loException != null)
//            {
//                lsErr = loException.Message + "\r\n" + loException.StackTrace;
//                loException = loException.InnerException;
//            }
//            if (lsErr != "")
//            {
//                OracleCommand loOraComn = DAL.DBA.GetOraCommand();
//                string lsSQL = @"insert into T_S_LOG (
//                                                    Log_ID,
//                                                    Log_Error,
//                                                    Log_Time,
//                                                    Log_Page,
//                                                    Employee_ID)
//                                            values(
//                                                    :Log_ID,
//                                                    :Log_Error,
//                                                    :Log_Time,
//                                                    :Log_Page,
//                                                    :Employee_ID)";
//                try
//                {
//                    loOraComn.CommandText = lsSQL;
//                    DBUtil.AddParameter(loOraComn, "Log_ID", lnLogID);
//                    DBUtil.AddParameter(loOraComn, "Log_Error", lsErr);
//                    DBUtil.AddParameter(loOraComn, "Log_Time", System.DateTime.Now);
//                    DBUtil.AddParameter(loOraComn, "Log_Page", lsErrUrl);
//                    DBUtil.AddParameter(loOraComn, "Employee_ID", lnUseID);
//                    loOraComn.ExecuteNonQuery();
//                }
//                catch (Exception ex)
//                {
//                }
//                finally
//                {
//                    DAL.DBA.RealseOraCommand(loOraComn);
//                }
//            }
//        }

    }
}
