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
using MyFramework.BusinessLogic.Common.SystemFrame;
using System.Collections.Generic;
//using DAL;
public partial class MainPage : System.Web.UI.Page 
{
    public string lsTopHtml = "";
    public string TopMenuId = "";
    public string lsTopDefUrl = "";
    protected void Page_Load(object sender, EventArgs e)
    {
        AjaxPro.Utility.RegisterTypeForAjax(typeof(MainPage));
        TopMenuId = this.Request.QueryString["TopMenuId"].ToString();
        //if (!IsPostBack && this.meuMain.Items.Count == 0)
        //    ((UserSession)this.Session["UserSession"]).InitMenu();
            GenerateTopMenu();

            HiddenKnowledge();
         
    }
    private void GenerateTopMenu()
    {


    
        lsTopHtml = "<table id=\"toptable\" border=\"0\" cellspacing=\"0\" cellpadding=\"0\">";
  
        lsTopHtml += "<tr>";

        //List<Menuitem> loArrayList = ((MenuStructure)this.Session["MenuStructure"]).TopMenuitems;
        //for(int lnIndex=0;lnIndex<loArrayList.Count;lnIndex++)
        //{
        //    Menuitem loMenuItem = (Menuitem)loArrayList[lnIndex];
        //    if (loMenuItem.ParentMenuID == null || loMenuItem.ParentMenuID == "")
        //    {
        //        if (lsTopDefUrl == "") lsTopDefUrl = loMenuItem.HyperLink;
                lsTopHtml += "<td height=\"28\" background=\"Image/GTopImage/crm_topdh_01.gif\">";
                lsTopHtml += GenerateTopMenuItem();
                lsTopHtml += "</td>";
                lsTopHtml += "<td width=\"2\"><img src=\"Image/GTopImage/crm_topdh_02.gif\" width=\"6\" height=\"28\"></td>";
        //    }
           
        //}
       // lsTopHtml += "<td>&nbsp;</td>";
        lsTopHtml += "</tr></table>";
        if (lsTopDefUrl == "") lsTopDefUrl = "Default.aspx?TopMenuId=" + TopMenuId;
     
    }
    private string GenerateTopMenuItem()
    {
        string lsHtml = " <table  border=\"0\" cellpadding=\"0\" cellspacing=\"0\" >";
        lsHtml += "<tr>";
        lsHtml += "<td width=\"9\" ><img src=\"Image/GTopImage/crm_topdh_01.gif\" width=\"9\" height=\"28\"></td>";
        lsHtml += "<td width=\"14\"><img src=\"Image/GTopImage/crm_topdh_03.gif\" width=\"11\" height=\"28\"></td>";
        lsHtml += "<td  align=\"center\"><a href=\"#\" class=\"L_top1\" onclick=\"topMenuonClick('MenuID','HyperLink');\">Caption </a></td>";
        lsHtml += "<td width=\"9\" ><img src=\"Image/GTopImage/crm_topdh_01.gif\" width=\"9\" height=\"28\"></td>";
        lsHtml += "</tr></table>";
       
	    return lsHtml;
    }
    [AjaxPro.AjaxMethod]
    public DataTable GetMessage()
    {
//        UserSession loUserSession = (UserSession)Session["UserSession"];
//        if (null != loUserSession.UserInfo.UserId)
//        {
//            int RECIVER_EMPLOYEE_ID = loUserSession.UserInfo.UserId;
//            string sql = "select * from (";
//             sql += @"select MSG_TITLE, MSG_CONTENT, MESSAGE_ID,
//                                    case   when awake_time is null then 'show'
//                                              when awake_time < sysdate then 'show'
//                                              end state
//                            from T_S_MESSAGE where RECIVER_EMPLOYEE_ID = " + RECIVER_EMPLOYEE_ID;
//            if (loUserSession.UserInfo.PositionID.HasValue)
//            {

//                sql += " or POSITION_ID = " + loUserSession.UserInfo.PositionID.Value;
//            }
//            sql += ") where state = 'show'";
//            DataTable ldtDeal = new DataTable();
//            DBA.FillDataTable(ldtDeal, sql);
//            if (0 != ldtDeal.Rows.Count)
//                return ldtDeal;
//        }
        return null;
    }


    [AjaxPro.AjaxMethod]
    public void DeleteMessage(int MESSAGE_ID)
    {
        //string sql = "delete from T_S_MESSAGE where MESSAGE_ID = " + MESSAGE_ID;

        //DAL.DBA.ExecuteNonQuery(sql);

    }

    public bool IsInnerInternet()
    {
       return Request.Url.Host.Contains("192.168");
    }

    public void HiddenKnowledge()
    {
        if (IsInnerInternet())
            this.spanKnowledge.Visible = true;
        else
            this.spanKnowledge.Visible = false;
    }
}
