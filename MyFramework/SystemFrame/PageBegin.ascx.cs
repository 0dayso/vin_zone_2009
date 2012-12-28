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
public partial class SystemFrame_PageBegin : System.Web.UI.UserControl, IPageBeginControl
{

    protected void Page_Load(object sender, EventArgs e)
    {
        this.SysDateTime = System.DateTime.Now.ToString() + System.DateTime.Now.ToString("dddd");
        if (this.msInsertUrl != "")
        {
            if (this.IsOpen)
            {
                this.msToolhref = this.msInsertUrl;
                this.msToolBar = "<img src=\"/MyFramework/Image/GLeftImage/Insert.gif\" border=\"0\" align=\"absbottom\" ><font color=#ff6600>新建</font>";
            }
            else
                this.msToolBar = "<img src=\"/MyFramework/Image/GLeftImage/Insert.gif\" border=\"0\" align=\"absbottom\" onclick=\" window.location.href='" + this.msInsertUrl + "';return false;\"><font onclick=\" window.location.href='" + this.msInsertUrl + "';return false;\" color=#ff6600>新建</font>";

        }
    }
    public string SysDateTime = "";
    private string msCaption = "";
    private bool mbIsShowPageHeader = false;
    private bool mbIsShowTabHeader = true;
    private string msToolBar = "";
    private string msInsertUrl = "";
    private bool msIsOpen = false;
    private string msToolhref = "#";
    public string PageCaption
    {
        get
        {
            return this.msCaption;
        }
        set
        {
            this.msCaption = value;
           
        }
    }
    public string InsertUrl
    {
        get
        {
            return this.msInsertUrl;
        }
        set
        {
            this.msInsertUrl = value;

        }
    }
    public string ToolBar
    {
        get
        {
            return this.msToolBar;
        }
        set
        {
            this.msToolBar = value;

        }
       
    }
    public string Toolhref
    {
        get
        {
            return this.msToolhref;
        }
        set
        {
            this.msToolhref = value;

        }

    }
    public bool IsShowPageHeader
    {
        get { return this.mbIsShowPageHeader; }
        set { this.mbIsShowPageHeader = value; }
    }
    public bool IsOpen
    {
        get { return this.msIsOpen; }
        set { this.msIsOpen = value; }
    }
    public bool IsShowTabHeader
    {
        get { return this.mbIsShowTabHeader; }
        set { this.mbIsShowTabHeader = value; }
    }
}
