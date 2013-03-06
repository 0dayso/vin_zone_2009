using System;
using System.Web;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using System.Text;
using System.Collections.Generic;
using System.Collections;

namespace MyFramework.BusinessLogic.Common
{
    /// <summary>
    /// ������������pageҳ�Ļ���
    /// </summary>
    public class BasePage : Page
    {
        private string msAppDir = "";
        private System.Web.UI.Control moBodyHeaderControl = null;
        private System.Web.UI.HtmlControls.HtmlForm moForm = null;
        private System.Collections.ArrayList moValidators = new System.Collections.ArrayList();
        private bool mbShowSystemErrors = true;
        private bool mbValidateSession = true;
        private System.Web.UI.LiteralControl moScriptSourceLiteral = null;
        private bool mbShowPageHeader = true;
        protected IPageBeginControl moPageBeginControl;
        protected NameObjectCollection moSharedInfo = null;
        private bool mbShowTabHeader = true;
        /// <summary>
        /// �Ƿ�ǿ������
        /// </summary>
        protected bool isForce = false;
        protected string LoingOutMessage = string.Empty;
        public UserSession UserSession
        {
            get
            {
                if (this.Session["UserSession"] != null)
                {
                    return (UserSession)this.Session["UserSession"];
                }
                else
                {
                    if (this.Page.Request.Cookies["User"] != null && this.Page.Request.Cookies["User"].Value != "")
                    {
                        UserSession loUserSession = new UserSession(this.Session);
                        if (loUserSession.ReLogin(this.Page.Request.Cookies["User"].Value))
                            return (UserSession)this.Session["UserSession"];
                    }

                    return null;
                }
            }
        }

        protected bool IsShowTabHeader
        {
            get
            {
                return this.mbShowTabHeader;
            }
            set
            {
                this.mbShowTabHeader = value;
                if (this.moPageBeginControl != null)
                {
                    this.moPageBeginControl.IsShowTabHeader = value;
                }

            }
        }
        protected bool IsShowPageHeader
        {
            get
            {
                if (Convert.ToInt32(System.Configuration.ConfigurationSettings.AppSettings["ShowPageHeader"]) == 1)
                    return this.mbShowPageHeader;
                else
                    return false;
            }
            set
            {
                this.mbShowPageHeader = value;
                if (this.moPageBeginControl != null)
                {
                    this.moPageBeginControl.IsShowPageHeader = value;
                }
            }
        }
        public NameObjectCollection SharedInfo
        {
            get
            {
                if (this.moSharedInfo == null) this.moSharedInfo = new NameObjectCollection();
                return this.moSharedInfo;
            }
        }
        public NameObjectCollection CallerSharedInfo
        {
            get
            {
                if (this.PreviousPage != null)
                    return ((BasePage)this.Context.Handler).SharedInfo;
                else return new NameObjectCollection();
            }
        }
        public BasePage()
        {

            this.Load += new System.EventHandler(this.BasePage_Load);
            this.Error += new System.EventHandler(this.HandlePageError);
            this.RegisterScriptSource("javascript", "/JavaScript/Pages/BasePage.js");
            this.RegisterScriptSource("javascript", "/JavaScript/Pages/UIValidation.js");
            this.RegisterScriptSource("javascript", "/JavaScript/Common/BasicFunctions.js");
            string pageBeginControlStr = this.GetPageBeginControlStr();
            Control child = base.LoadControl(pageBeginControlStr);
            this.moPageBeginControl = (IPageBeginControl)child;
            this.moPageBeginControl.PageCaption = this.msPageCaption;
            this.moPageBeginControl.IsShowPageHeader = this.IsShowPageHeader;
            this.Controls.Add(child);
        }
        protected virtual string GetPageBeginControlStr()
        {
            return System.Configuration.ConfigurationSettings.AppSettings["PageBeginControl"];
        }

        protected virtual string GetPageEndControlStr()
        {
            return System.Configuration.ConfigurationSettings.AppSettings["PageEndControl"];
        }
        protected string HtmlEncode(string tsText)
        {
            return this.Server.HtmlEncode(tsText);
        }

        public static string AppendQueryString(string tsUrl, string tsQueryString)
        {
            if (tsQueryString == "") return tsUrl;
            if (tsUrl.IndexOf("?") >= 0) return tsUrl + "&" + tsQueryString;
            else
            {
                return tsUrl + "?" + tsQueryString;
            }
        }

        public static string AppendQueryValue(string tsUrl, string tsQueryName, string tsQueryValue)
        {
            int lnStartIndex = tsUrl.IndexOf("?" + tsQueryName + "=");
            if (!(lnStartIndex >= 0))
            {
                lnStartIndex = tsUrl.IndexOf("&" + tsQueryName + "=");
            }
            if (lnStartIndex > 0)
            {
                int lnEndIndex = tsUrl.IndexOf("&", lnStartIndex + tsQueryName.Length + 2);
                if (lnEndIndex > 0)
                {
                    return tsUrl.Substring(0, lnStartIndex + tsQueryName.Length + 2) + tsQueryValue + tsUrl.Substring(lnEndIndex, tsUrl.Length - lnEndIndex);
                }
                else
                {
                    return tsUrl.Substring(0, lnStartIndex + tsQueryName.Length + 2) + tsQueryValue;
                }
            }
            else
            {
                return BasePage.AppendQueryString(tsUrl, tsQueryName + "=" + tsQueryValue);
            }
        }

        public string AppPath
        {
            get
            {
                if (this.msAppDir == "") this.msAppDir = System.Configuration.ConfigurationSettings.AppSettings["AppDir"];
                return msAppDir;
            }
        }

        public string AppendAppPath(string tsUrl)
        {
            if (tsUrl.Length > 0 && tsUrl[0] != '/' && tsUrl[0] != '\\') tsUrl = "/" + tsUrl;
            return "/" + this.AppPath + tsUrl;
        }

        private void BasePage_Load(object sender, System.EventArgs e)
        {


        }
        protected virtual void HandlePageError(object sender, System.EventArgs e)
        {
            this.ShowException(Server.GetLastError());
        }

        public void RegisterValidator(BaseValidator toValidator)
        {
            this.moValidators.Add(toValidator);
        }


        public System.Web.UI.HtmlControls.HtmlForm FormControl
        {
            get
            {
                if (this.moForm == null)
                {
                    if (this.Controls != null)
                    {
                        foreach (Control loControl in this.Controls)
                        {
                            if (loControl is System.Web.UI.HtmlControls.HtmlForm)
                            {
                                this.moForm = (System.Web.UI.HtmlControls.HtmlForm)loControl;
                            }
                        }
                    }
                }
                return this.moForm;

            }

        }

        /// <summary>
        ///  �����������Ĵ�����ʾ��ҳ����
        /// </summary>
        /// <param name="toException">������Ϣ</param>
        /// <returns></returns>
        protected string GetExceptionMessage(Exception toException)
        {
            string lsExceptionMessage = "";
            Exception loException = toException;
            while (loException != null)
            {
                lsExceptionMessage += loException.Message + "\r\n";
                loException = loException.InnerException;
            }
            return lsExceptionMessage;
        }

        static public string ConvertToHtml(string tsSrcString)
        {
            StringBuilder loStringBuilder = new StringBuilder(tsSrcString);

            loStringBuilder.Replace("\r\n", "<br>");

            return loStringBuilder.ToString();
        }


        protected override void Construct()
        {
            base.Construct();
        }

        public virtual string AppendAppInfo(string tsUrl)
        {
            return AppendQueryString(tsUrl, GetMenuQueryString());
        }

        public void ShowException(string tsException)
        {
            this.ShowException(new Exception(tsException));
        }
        public void ShowException(Exception toException)
        {
            this.Session["ErrorMessage"] = GetExceptionMessage(toException);
            this.Session["Exception"] = toException;
            this.Session["CurrentUrl"] = this.Request.Url.PathAndQuery;

            HttpCookie aa = new HttpCookie("User");
            aa.Value = "User";
            Response.AppendCookie(aa);
            if (this.Page.Request.Cookies["User"] == null || this.Page.Request.Cookies["User"].Value == "")
            {
                this.Response.Write("�û������ߣ������µ�½");
                this.Response.End();
            }
            else
            {
                if (this.UserSession == null)
                {
                    this.Response.Redirect("/" + this.AppPath + "/login.aspx", true);
                }
                else
                {
                    if (this.IsShowPageHeader)
                        this.Response.Redirect(this.AppendAppInfo("/" + this.AppPath + "/SystemFrame/ShowExceptionMessage.aspx"), true);
                    else
                        this.Response.Redirect("/" + this.AppPath + "/SystemFrame/ShowExceptionMessage.aspx?ShowHeader=0", true);
                }
            }

        }
        protected System.Web.UI.Control BodyHeaderControl
        {
            get
            {
                if (this.moBodyHeaderControl == null)
                {
                    if (this.Controls != null)
                    {
                        int lnIndex = this.Controls.IndexOf(this.FormControl) - 1;
                        if (lnIndex >= 0) this.moBodyHeaderControl = this.Controls[lnIndex];
                    }
                }
                return this.moBodyHeaderControl;
            }
        }

        protected int GetBodyHeaderControlIndex()
        {
            Control loControl = this.BodyHeaderControl;
            if (loControl != null)
            {
                return this.Controls.IndexOf(this.BodyHeaderControl);
            }
            return 0;
        }
        protected void InsertBodyAttributes(string lsAttributes)
        {
            int lnBodyHeaderIndex = GetBodyHeaderControlIndex();
            if (lnBodyHeaderIndex >= 0)
            {
                System.Web.UI.LiteralControl loHTMLHeader = (System.Web.UI.LiteralControl)this.Controls[lnBodyHeaderIndex];
                string lsHTMLHeader = loHTMLHeader.Text.ToLower();
                int lnPos = lsHTMLHeader.IndexOf("body");
                lsHTMLHeader = loHTMLHeader.Text;
                lsHTMLHeader = lsHTMLHeader.Substring(0, lnPos + 4) + " " + lsAttributes + " " + lsHTMLHeader.Substring(lnPos + 4, lsHTMLHeader.Length - lnPos - 4);
                loHTMLHeader.Text = lsHTMLHeader;
            }
        }

        protected override System.Web.UI.ControlCollection CreateControlCollection()
        {
            return base.CreateControlCollection();
        }

        protected override void RenderChildren(System.Web.UI.HtmlTextWriter writer)
        {
            base.RenderChildren(writer);
        }

        protected override void Render(System.Web.UI.HtmlTextWriter writer)
        {
            base.Render(writer);
        }

        protected virtual void HandleException(Exception toException)
        {
            if (this.mbShowSystemErrors)
            {
                throw toException;
            }
            else
            {
                this.ShowException(toException);
            }
        }

        protected override void OnPreInit(EventArgs e)
        {
            base.OnPreInit(e);
            // session   ͬ�û���ε�¼��֤
            //Hashtable hOnline = (Hashtable)Application["Online"];
            //if (hOnline != null)
            //{
            //    IDictionaryEnumerator idE = hOnline.GetEnumerator();

            //    while (idE.MoveNext())
            //    {
            //        if (idE.Key != null && idE.Key.ToString().Equals(Session.SessionID))
            //        {
            //            //already login
            //            if (idE.Value != null && "XXXXXX".Equals(idE.Value.ToString()))
            //            {
            //                hOnline.Remove(Session.SessionID);
            //                Application.Lock();
            //                Application["Online"] = hOnline;
            //                Application.UnLock();
            //                isLogin = true;
            //                MessageBox("����ʺ����ڱ𴦵�½���㱻ǿ�����ߣ�", this.Page);
            //            }
            //            else
            //            {
            //                isLogin = true;
            //            }
            //            break;
            //        }
            //    }
            //}

            // cookie ͬ�û���ε�¼��֤
            //�Ե�¼�û�����ҳ�������֤
            if (Session["onlineUserID"] != null && this.Page.Request.Cookies[Convert.ToString(Session["onlineUserID"])] != null)
            {
                /* ��ȡ�ͻ��˵��û����߱�ʶGuid
                 * �����ʶGuid�����˲�һ��,���ض����ظ���¼ҳ��
                 */

                string m_strUserOnlineID = this.Page.Request.Cookies[Convert.ToString(Session["onlineUserID"])].Value;//  .GetCookie("UserOnlineID");
                if (!string.IsNullOrEmpty(m_strUserOnlineID))
                {
                    Dictionary<string, string> userlist = Application["OnlineUserList"] as Dictionary<string, string>;
                    if (userlist != null && m_strUserOnlineID != userlist[Convert.ToString(Session["onlineUserID"])])
                    {
                        //MessageBox("����ʺ����ڱ𴦵�½���㱻ǿ�����ߣ�", this.Page);
                        //MessageBox("����ʺ����ڱ𴦵�½���㱻ǿ�����ߣ�", this.Page, "Login.aspx");
                        isForce = true;

                    }
                }
                /******** End *******/
            }
        }
        protected override void CreateChildControls()
        {
            base.CreateChildControls();
            // ����ҳ������
            InsertBodyAttributes("leftMargin=\"0\" topMargin=\"0\" MARGINHEIGHT=\"0\" MARGINWIDTH=\"0\" ");
        }

        private System.Web.UI.LiteralControl ScriptSourceLiteral
        {
            get
            {
                if (this.moScriptSourceLiteral == null) this.moScriptSourceLiteral = new System.Web.UI.LiteralControl();
                return this.moScriptSourceLiteral;
            }
        }

        /// <summary>
        /// ע��һ���ű��ļ�.
        /// </summary>
        /// <param name="tsLanguage"></param>
        /// <param name="tsSource"></param>
        public void RegisterScriptSource(string tsLanguage, string tsSource)
        {
            string lsRootPath = "";
            if (tsSource.Length > 0)
            {
                if (tsSource[0] == "/"[0] || tsSource[0] == "\\"[0]) lsRootPath = this.AppPath;
            }
            this.ScriptSourceLiteral.Text += "<script language=\"" + tsLanguage + "\" src=\"/" + lsRootPath + tsSource + "\"></script>";

        }

        /// <summary>
        /// �����ͻ�����Ϣ
        /// </summary>
        /// <param name="str_Message">��Ҫ��ʾ����Ϣ</param>
        /// <param name="page">��ǰҳ����</param>
        public static void MessageBox(string str_Message, Page page)
        {
            str_Message = str_Message.Replace("'", "��").Replace(";", "��").Replace("\r\n", "\\n").Replace("\r", "").Replace("\n", "<br>");//���˵������ַ�
            string strScript = "<script>alert('" + str_Message + "');</script>";
            page.ClientScript.RegisterStartupScript(page.GetType(), "alertMessage", strScript);
        }
        /// <summary>
        /// �����ͻ�����Ϣ
        /// </summary>
        /// <param name="str_Message">��Ҫ��ʾ����Ϣ</param>
        /// <param name="page">��ǰҳ����</param>
        /// <param name="url">��תҳ���url</param>
        public static void MessageBox(string str_Message, Page page, string url)
        {
            str_Message = str_Message.Replace("'", "��").Replace(";", "��").Replace("\r\n", "\\n").Replace("\r", "").Replace("\n", "<br>");//���˵������ַ�
            url = url.Replace("'", "��").Replace(";", "��").Replace("\r\n", "\\n").Replace("\r", "").Replace("\n", "<br>");//���˵������ַ�
            string strScript = "<script>alert('" + str_Message + "');window.location.href ='" + url + "';</script>";
            page.ClientScript.RegisterStartupScript(page.GetType(), "alertMessage", strScript);
        }


        public void ShowMessage(string tsMessage)
        {
            this.SharedInfo.Add("TransferMessage", tsMessage);
            this.Server.Transfer(this.AppendAppInfo("/" + this.AppPath + "/SystemFrame/ShowMessage.aspx"));
        }

        protected virtual void ValidateSession()
        {
            if (this.UserSession == null)
            {
                this.ShowMessage("��ĻỰ�Ѿ����ڣ������µǣ�");
            }
        }

        protected override void OnInit(EventArgs e)
        {
            base.OnInit(e);

            this.Controls.Add(this.ScriptSourceLiteral);
            this.Controls.Add(new LiteralControl("<link rel=\"Stylesheet\" href=\"/MyFramework/CSS/AppStyles.css\" type=\"text/css\" />"));
        }

        private string msPageCaption = "";
        public string PageCaption
        {
            get
            {
                return this.msPageCaption;
            }
            set
            {
                this.msPageCaption = value;
                if (this.moPageBeginControl != null)
                {
                    this.moPageBeginControl.PageCaption = value;
                    this.Page.Title = value;
                }
            }


        }
        private string msInsertUrl;
        public string InsertUrl
        {
            get
            {
                return this.msInsertUrl;
            }
            set
            {
                this.msInsertUrl = value;
                if (this.moPageBeginControl != null)
                {
                    this.moPageBeginControl.InsertUrl = value;
                }

            }
        }
        private bool msIsOpen;
        public bool IsOpen
        {
            get
            {
                return this.msIsOpen;
            }
            set
            {
                this.msIsOpen = value;
                if (this.moPageBeginControl != null)
                {
                    this.moPageBeginControl.IsOpen = value;
                }

            }
        }
        public string GetMenuQueryString()
        {
            return "TopMenuId=" + this.Request["TopMenuId"] + "&LeftMenuId=" + this.Request["LeftMenuId"];
        }
    }


    public interface IPageBeginControl
    {
        string PageCaption { get; set; }
        string InsertUrl { get; set; }
        bool IsOpen { get; set; }
        bool IsShowPageHeader { get; set; }
        bool IsShowTabHeader { get; set; }
    }
}

