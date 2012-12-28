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
using MyFramework.BusinessLogic.Common.SystemFrame;
using MyFramework.BusinessLogic.Common;
public partial class SystemFrame_PageLeft : System.Web.UI.UserControl
{
    public string lsleftHtml = "";
    protected void Page_Load(object sender, EventArgs e)
    {
        if (this.Page.Session["MenuStructure"] == null)
        {
            if (this.Page.Request.Cookies["User"] != null && this.Page.Request.Cookies["User"].Value != "")
            {
                UserSession loUserSession = new UserSession(this.Session);
                //if (!loUserSession.ReLogin(this.Page.Request.Cookies["User"].Value))
                //    throw new Exception("用户已下线，请重新登陆！");
            }
            else 
            { 
                //throw new Exception("用户已下线，请重新登陆！");
            }
        }
        lsleftHtml = "<table id=\"LeftMenuTable\"  width=120  border=\"0\" cellpadding=\"0\" cellspacing=\"0\" >";
        GetLeftMenuHtml();
        lsleftHtml += "</table>";
        //this.Page.Response.Write(lsleftHtml);
     
    }

    private void GetLeftMenuHtml()
    {
       
        MenuStructure loMenuStructure = (MenuStructure)this.Page.Session["MenuStructure"];
       
        if (this.Page.Request.QueryString["TopMenuId"] != null)
        {
            string lsTopMenuId=this.Page.Request.QueryString["TopMenuId"].ToString();
         
          //  Menuitem loMenuitem = loMenuStructure.FindTopMenuitem(lsTopMenuId);
            //if (loMenuitem != null)
            //{
                //for (int lnIndex = 0; lnIndex < loMenuitem.Subitems.Count; lnIndex++)
                //{
                //    Menuitem lotempMenu = (Menuitem)loMenuitem.Subitems[lnIndex];
                    lsleftHtml += "<tr height=\"24\">";
                   // Boolean isSelectedMenu = false;
                    //if (this.Page.Request["LeftMenuID"] != null)
                    //{
                    //    string loSelectedMenuid = this.Page.Request["LeftMenuID"].ToString();
                    //    Menuitem loSelectedMenu = loMenuStructure.FindMenuitem(loMenuitem.Subitems, loSelectedMenuid);
                    //    if (loSelectedMenu!=null)
                    //         if (lotempMenu.MenuID == loSelectedMenu.MenuID || lotempMenu.ParentMenuID == loSelectedMenuid || lotempMenu.MenuID == loSelectedMenu.ParentMenuID) isSelectedMenu = true;
                    //    //+ lotempMenu.MenuID + "')\" background='Image/TopImage/anniu_left_02.gif' >";

                    //}
                   // string lsbackground = "/MyFramework/Image/GLeftImage/crm_left.gif";
                    //if (isSelectedMenu) 
                       // lsbackground = "/MyFramework/Image/GLeftImage/crm_left1.gif";
                    lsleftHtml += "<td  colspan=2  align=\"left\"  id=menuTitle11111   onclick=\"showsubmenu('2222222');return false;\" background='3333333' >";
                    //  lsleftHtml +="<table width=\"82%\" border=\"0\" align=\"right\" cellpadding=\"0\" cellspacing=\"0\"><tr><td>";

                    lsleftHtml += "&nbsp;&nbsp;&nbsp; <a href=\"#\" class=\"L_caidan01\">444444444</a></td></tr>";
                    // lsleftHtml += "</table></td></tr>";
                    //if (lotempMenu.Subitems.Count != 0)
                    //{

                        //if (isSelectedMenu)
                        //    lsleftHtml += "<tr id='submenu" + lotempMenu.MenuID + "' style='display:'>";
                        //else
                        //    lsleftHtml += "<tr id='submenu" + lotempMenu.MenuID + "' style='display:none'>";
                        ////background='/MyFramework/Image/GLeftImage/crm_cd_02.gif'
                        ////background='/MyFramework/Image/GLeftImage/crm_cd_02.gif'
                        ////<img src='/MyFramework/Image/GLeftImage/crm_cd_05.gif' width=\"120\" height=\"6\">
                        //lsleftHtml += "<td><table width=\"100%\" border=\"0\" align=\"right\" cellpadding\"0\" cellspacing=\"0\" >";
                        //lsleftHtml += "<tr><td height=4 background='/MyFramework/Image/GLeftImage/crm_cd_02.gif'></td></tr>";
                        //lsleftHtml += "<tr><td background='/MyFramework/Image/GLeftImage/crm_cd_02.gif'><table  border=\"0\" align=\"right\" cellpadding\"0\" cellspacing=\"0\" width=\"90%\">";
                        //for (int lnIndexJ = 0; lnIndexJ < lotempMenu.Subitems.Count; lnIndexJ++)
                        //{
                        //    Menuitem loEndMenu = (Menuitem)lotempMenu.Subitems[lnIndexJ];

                        //    lsleftHtml += "<tr >";
                        //    lsleftHtml += "<td width=\"12px\" ><img src='/MyFramework/Image/GLeftImage/arrow.gif' width=\"11\" height=\"11\"></td>";
                        //    lsleftHtml += " <td height=\"19\" valign=\"bottom\" ><a class=\"L_caidan02\" href=\"" + BasePage.AppendQueryString(loEndMenu.HyperLink, "TopMenuID=" + lotempMenu.ParentMenuID + "&LeftMenuID=" + loEndMenu.MenuID) + "\">" + loEndMenu.Caption + "</a></td>";
                        //    lsleftHtml += "</tr>";
                        //}
                        //lsleftHtml += "</table></td></tr>";
                        //lsleftHtml += "<tr><td height=6 background='/MyFramework/Image/GLeftImage/crm_cd_05.gif'></td></tr>";
                        //lsleftHtml += "</table></td></tr>";
                    //}

                    lsleftHtml += "<tr height=\"2\"><td colspan=\"2\" ></td></tr>";

                //}
           // }
        }
    }

}
