using System;
using System.ComponentModel;
using System.Drawing;
using System.Security.Permissions;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Text.RegularExpressions;

namespace MyFramework.Component
{
    public class PagerPageChangedEventArgs : EventArgs
    {
        public int CurPageIndex;
    }

    [DefaultProperty("StartPage"), ToolboxData("<{0}:PagerPro runat='server'></{0}:PagerPro>"), AspNetHostingPermission(SecurityAction.Demand, Level = AspNetHostingPermissionLevel.Minimal), AspNetHostingPermission(SecurityAction.InheritanceDemand, Level = AspNetHostingPermissionLevel.Minimal)]
    public class PagerPro : CompositeControl, INamingContainer
    {
        public Button btnGo;
        public LinkButton lbFirst;
        public LinkButton lbLast;
        public LinkButton lbNext;
        public LinkButton lbNextGroup;
        public LinkButton[] lbPagingButtons;
        public LinkButton lbPrevious;
        public LinkButton lbPreviousGroup;
        public const string ONMOUSEOUT = "return FOnMouseOut();";
        public const string ONMOUSEOVER = "return FOnMouseOver();";
        public const string SCRIPT_TEXT = "function FOnMouseOver(){var object=event.srcElement; if(object.tagName=='TD'){ object.style.backgroundColor='#FFE4C4'; } }function FOnMouseOut(){var object=event.srcElement; if(object.tagName=='TD'){ object.style.backgroundColor='#FFFFFF'; } }";
        public const string SCRIPT_ID = "PagerUI";
        public TextBox txtGoToPage;


        public delegate void PagerChangedEventHandler(object sender, PagerPageChangedEventArgs e);
        public event PagerChangedEventHandler PageIndexChanged;
        protected virtual void OnPageIndexChanged(PagerPageChangedEventArgs e)
        {
            if (PageIndexChanged != null)
                PageIndexChanged(this, e);
        }
        private void GoToPage(int pageIndex)
        {
            // Prepares event data
            PagerPageChangedEventArgs e = new PagerPageChangedEventArgs();
            e.CurPageIndex = pageIndex;
            OnPageIndexChanged(e);

        }
        protected override void AddAttributesToRender(HtmlTextWriter writer)
        {
            base.AddAttributesToRender(writer);
        }

        public void AddPageCountBoderStyle(HtmlTextWriter writer)
        {
            if (this.DefineStyle == StyleList.None)
            {
                writer.AddStyleAttribute(HtmlTextWriterStyle.BorderWidth, "1px");
                writer.AddStyleAttribute(HtmlTextWriterStyle.BorderColor, ColorTranslator.ToHtml(Color.DarkOrange));
                writer.AddStyleAttribute(HtmlTextWriterStyle.BorderStyle, BorderStyle.Solid.ToString());
                writer.AddStyleAttribute(HtmlTextWriterStyle.BackgroundColor, "#FFFFFF");
            }
            writer.AddStyleAttribute(HtmlTextWriterStyle.TextAlign, "center");
        }

        public void AddPagingButtons()
        {
            int numericButtonCount = 0;
            if (((this.StartPage + this.NumericButtonCount) - 1) <= this.PageCount)
            {
                numericButtonCount = this.NumericButtonCount;
            }
            else
            {
                numericButtonCount = (this.PageCount - this.StartPage) + 1;
            }
            this.lbPagingButtons = new LinkButton[numericButtonCount];
            for (int i = 0; i < numericButtonCount; i++)
            {
                int num3 = this.StartPage + i;
                this.lbPagingButtons[i] = new LinkButton();
                this.lbPagingButtons[i].CausesValidation = false;
                this.lbPagingButtons[i].ID = num3.ToString();
                this.lbPagingButtons[i].Text = num3.ToString();
                this.lbPagingButtons[i].Font.Underline = false;
                this.lbPagingButtons[i].ForeColor = Color.Red;
                if (num3 == this.PageIndex)
                {
                    this.lbPagingButtons[i].Enabled = false;
                }
                this.lbPagingButtons[i].Click += new EventHandler(this.PagerPro_Click);
                this.Controls.Add(this.lbPagingButtons[i]);
            }
        }

        public void AddTdBoderStyle(HtmlTextWriter writer)
        {
            if (this.DefineStyle == StyleList.Standard)
            {
                writer.AddStyleAttribute(HtmlTextWriterStyle.BorderWidth, "1px");
                writer.AddStyleAttribute(HtmlTextWriterStyle.BorderColor, ColorTranslator.ToHtml(Color.DarkOrange));
                writer.AddStyleAttribute(HtmlTextWriterStyle.BorderStyle, BorderStyle.Dotted.ToString());
                writer.AddStyleAttribute(HtmlTextWriterStyle.BackgroundColor, "#FFFFFF");
            }
            writer.AddStyleAttribute(HtmlTextWriterStyle.TextAlign, "center");
        }

        protected void btnGo_Click(object sender, EventArgs e)
        {
            string str = this.txtGoToPage.Text.Trim();
            if (Tool.isNumberR(str))
            {
                int num = Convert.ToInt32(str);
                if ((num <= this.PageCount) && (num >= 1))
                {
                    this.PageIndex = Convert.ToInt32(str);
                    int num2 = this.PageCount / this.NumericButtonCount;
                    int num3 = ((this.PageCount % this.NumericButtonCount) == 0) ? 0 : 1;
                    int num4 = num2 + num3;
                    for (int i = 0; i < num4; i++)
                    {
                        int num6 = (this.NumericButtonCount * i) + 1;
                        if ((this.PageIndex - num6) < this.NumericButtonCount)
                        {
                            this.StartPage = num6;
                            break;
                        }
                    }
                    this.AddPagingButtons();
                }
            }
        }

        protected override void CreateChildControls()
        {
            this.Controls.Clear();
            this.lbFirst = new LinkButton();
            this.lbFirst.CausesValidation = false;
            this.lbFirst.ID = "lbFirst";
            this.lbFirst.Text = " << ";
            this.lbFirst.Font.Underline = false;
            this.lbFirst.ToolTip = "Goto the first page";
            this.lbFirst.Click += new EventHandler(this.lbFirst_Click);
            this.lbLast = new LinkButton();
            this.lbLast.CausesValidation = false;
            this.lbLast.ID = "lbLast";
            this.lbLast.Text = " >> ";
            this.lbLast.Font.Underline = false;
            this.lbLast.ToolTip = "Goto the last page";
            this.lbLast.Click += new EventHandler(this.lbLast_Click);
            this.lbPrevious = new LinkButton();
            this.lbPrevious.CausesValidation = false;
            this.lbPrevious.ID = "lbPrvious";
            this.lbPrevious.Text = " < ";
            this.lbPrevious.Font.Underline = false;
            this.lbPrevious.ToolTip = "Goto the previous page";
            this.lbPrevious.Click += new EventHandler(this.lbPrevious_Click);
            this.lbNext = new LinkButton();
            this.lbNext.CausesValidation = false;
            this.lbNext.ID = "lbNext";
            this.lbNext.Text = " > ";
            this.lbNext.Font.Underline = false;
            this.lbNext.ToolTip = "Goto the next page";
            this.lbNext.Click += new EventHandler(this.lbNext_Click);
            this.lbPreviousGroup = new LinkButton();
            this.lbPreviousGroup.CausesValidation = false;
            this.lbPreviousGroup.ID = "lbPreviousGroup";
            this.lbPreviousGroup.Text = "...";
            this.lbPreviousGroup.Font.Underline = false;
            this.lbPreviousGroup.ToolTip = "Goto the previous group pages";
            this.lbPreviousGroup.Click += new EventHandler(this.lbPreviousGroup_Click);
            this.lbNextGroup = new LinkButton();
            this.lbNextGroup.CausesValidation = false;
            this.lbNextGroup.ID = "lbNextGroup";
            this.lbNextGroup.Text = "...";
            this.lbNextGroup.Font.Underline = false;
            this.lbNextGroup.ToolTip = "Goto the next group pages";
            this.lbNextGroup.Click += new EventHandler(this.lbNextGroup_Click);
            this.txtGoToPage = new TextBox();
            this.txtGoToPage.ID = "txtGoToPage";
            this.txtGoToPage.Text = "1";
            if (this.DefineStyle == StyleList.None)
            {
                this.txtGoToPage.BorderWidth = Unit.Parse("1px");
                this.txtGoToPage.BorderColor = Color.DarkOrange;
            }
            else
            {
                this.txtGoToPage.BorderWidth = Unit.Parse("0px");
            }
            this.btnGo = new Button();
            this.btnGo.CausesValidation = false;
            this.btnGo.ID = "btnGo";
            this.btnGo.Text = "Go";
            if (this.DefineStyle == StyleList.None)
            {
                this.btnGo.BorderWidth = Unit.Parse("1px");
                this.btnGo.BorderColor = Color.DarkOrange;
            }
            else
            {
                this.btnGo.BorderWidth = Unit.Parse("0px");
            }
            this.btnGo.Click += new EventHandler(this.btnGo_Click);
            this.Controls.Add(this.lbFirst);
            this.Controls.Add(this.lbLast);
            this.Controls.Add(this.lbPrevious);
            this.Controls.Add(this.lbNext);
            this.Controls.Add(this.lbPreviousGroup);
            this.Controls.Add(this.lbNextGroup);
            this.Controls.Add(this.txtGoToPage);
            this.Controls.Add(this.btnGo);
            this.AddPagingButtons();
        }

        protected void lbFirst_Click(object sender, EventArgs e)
        {
            this.StartPage = 1;
            this.PageIndex = 1;
            this.AddPagingButtons();
            this.GoToPage(this.PageIndex);
        }

        protected void lbLast_Click(object sender, EventArgs e)
        {
            if (this.PageIndex != this.PageCount)
            {
                int num = this.PageCount / this.NumericButtonCount;
                int num2 = this.PageCount % this.NumericButtonCount;
                if (num2 == 0)
                {
                    this.StartPage = (this.PageCount - this.NumericButtonCount) + 1;
                }
                else
                {
                    this.StartPage = (this.NumericButtonCount * num) + 1;
                }
                this.PageIndex = this.PageCount;
                this.AddPagingButtons();
                this.GoToPage(this.PageIndex);
            }
        }

        protected void lbNext_Click(object sender, EventArgs e)
        {
            int num = (this.StartPage + this.NumericButtonCount) - 1;
            if (num < this.PageCount)
            {
                if (this.PageIndex < num)
                {
                    this.PageIndex++;
                }
                else if (this.PageIndex == num)
                {
                    this.StartPage = this.PageIndex + 1;
                    this.PageIndex++;
                }
            }
            else if (num >= this.PageCount)
            {
                if (this.PageIndex < this.PageCount)
                {
                    this.PageIndex++;
                }
                else if (this.PageIndex == this.PageCount)
                {
                    return;
                }
            }
            this.AddPagingButtons();
            this.GoToPage(this.PageIndex);
        }

        protected void lbNextGroup_Click(object sender, EventArgs e)
        {
            int num = this.StartPage + this.NumericButtonCount;
            this.PageIndex = num;
            this.StartPage = num;
            this.AddPagingButtons();
            this.GoToPage(this.PageIndex);
        }

        protected void lbPrevious_Click(object sender, EventArgs e)
        {
            if (this.PageIndex > this.StartPage)
            {
                this.PageIndex--;
            }
            else if (this.PageIndex == this.StartPage)
            {
                if (this.StartPage == 1)
                {
                    return;
                }
                this.StartPage = this.PageIndex - this.NumericButtonCount;
                this.PageIndex--;
            }
            this.AddPagingButtons();
            this.GoToPage(this.PageIndex);
        }

        protected void lbPreviousGroup_Click(object sender, EventArgs e)
        {
            int num = this.StartPage - this.NumericButtonCount;
            this.PageIndex = num;
            this.StartPage = num;
            this.AddPagingButtons();
            this.GoToPage(this.PageIndex);
        }

        protected override void OnPreRender(EventArgs e)
        {
            base.OnPreRender(e);
            this.RenderJavaScript();
        }

        protected void PagerPro_Click(object sender, EventArgs e)
        {
            LinkButton button = (LinkButton)sender;
            int num = Convert.ToInt32(button.ID.Trim());
            this.PageIndex = num;
            this.AddPagingButtons();
            this.GoToPage(this.PageIndex);
        }

        protected override void RecreateChildControls()
        {
            this.EnsureChildControls();
        }

        protected override void Render(HtmlTextWriter writer)
        {
            if (this.ShowPagination())
            {
                this.AddAttributesToRender(writer);
                this.ShowFirstLastPreviousNextButtons();
                writer.AddAttribute(HtmlTextWriterAttribute.Border, "0");
                writer.AddAttribute(HtmlTextWriterAttribute.Cellpadding, "0");
                writer.AddAttribute(HtmlTextWriterAttribute.Cellspacing, "2");
                writer.RenderBeginTag(HtmlTextWriterTag.Table);
                writer.RenderBeginTag(HtmlTextWriterTag.Tr);

                if (this.ShowTotalNumber)
                {
                    this.AddTdBoderStyle(writer);
                    writer.RenderBeginTag(HtmlTextWriterTag.Td);
                    this.AddPageCountBoderStyle(writer);
                    writer.RenderBeginTag(HtmlTextWriterTag.Div);
                    writer.Write(string.Concat(new object[] { "&nbsp;", this.RecordCount, "&nbsp;" }));
                    writer.RenderEndTag();
                    writer.RenderEndTag();
                    writer.Write("&nbsp;&nbsp;");
                }
                if (this.ShowTotalPage)
                {
                    this.AddTdBoderStyle(writer);
                    writer.RenderBeginTag(HtmlTextWriterTag.Td);
                    this.AddPageCountBoderStyle(writer);
                    writer.RenderBeginTag(HtmlTextWriterTag.Div);
                    writer.Write(string.Concat(new object[] { "&nbsp;", this.PageIndex, " / ", this.PageCount.ToString(), "&nbsp;" }));
                    writer.RenderEndTag();
                    writer.RenderEndTag();
                    writer.Write("&nbsp;&nbsp;");
                }
                if (this.ShowFirst)
                {
                    this.RenderControl(writer, this.lbFirst);
                }
                if (this.ShowPreviousGroup)
                {
                    this.RenderControl(writer, this.lbPreviousGroup);
                }
                if (this.ShowPrevious)
                {
                    this.RenderControl(writer, this.lbPrevious);
                }
                this.RenderButtonsRange(writer);
                if (this.ShowNext)
                {
                    this.RenderControl(writer, this.lbNext);
                }
                if (this.ShowNextGroup)
                {
                    this.RenderControl(writer, this.lbNextGroup);
                }
                if (this.ShowLast)
                {
                    this.RenderControl(writer, this.lbLast);
                }
                if (this.ShowGotoPage)
                {
                    writer.Write("&nbsp;&nbsp;");
                    this.AddTdBoderStyle(writer);
                    writer.RenderBeginTag(HtmlTextWriterTag.Td);
                    this.txtGoToPage.Text = this.PageIndex.ToString();
                    this.txtGoToPage.RenderControl(writer);
                    writer.RenderEndTag();
                    this.AddTdBoderStyle(writer);
                    writer.RenderBeginTag(HtmlTextWriterTag.Td);
                    this.btnGo.RenderControl(writer);
                    writer.RenderEndTag();
                }
                
                writer.RenderEndTag();
                writer.RenderEndTag();
            }
        }

        public void RenderButtonsRange(HtmlTextWriter writer)
        {
            int numericButtonCount = 0;
            if (((this.StartPage + this.NumericButtonCount) - 1) <= this.PageCount)
            {
                numericButtonCount = this.NumericButtonCount;
            }
            else
            {
                numericButtonCount = (this.PageCount - this.StartPage) + 1;
            }
            for (int i = 0; i < numericButtonCount; i++)
            {
                this.RenderControl(writer, this.lbPagingButtons[i]);
            }
        }

        public void RenderControl(HtmlTextWriter writer, LinkButton lb)
        {
            this.AddTdBoderStyle(writer);
            writer.AddAttribute("onmouseover", ONMOUSEOVER);
            writer.AddAttribute("onmouseout", ONMOUSEOUT);
            writer.RenderBeginTag(HtmlTextWriterTag.Td);
            if (this.DefineStyle != StyleList.None)
            {
                writer.Write("&nbsp;");
            }
            lb.RenderControl(writer);
            if (this.DefineStyle != StyleList.None)
            {
                writer.Write("&nbsp;");
            }
            writer.RenderEndTag();
        }

        public void RenderJavaScript()
        {
            if (!this.Page.ClientScript.IsClientScriptBlockRegistered(SCRIPT_ID))
            {
                this.Page.ClientScript.RegisterClientScriptBlock(base.GetType(), SCRIPT_ID, SCRIPT_TEXT, true);
            }
        }

        public void ShowFirstLastPreviousNextButtons()
        {
            this.ShowFirstPreviousButtons();
            this.ShowNextLastButtons();
        }

        public void ShowFirstPreviousButtons()
        {
            if (this.PageIndex <= this.NumericButtonCount)
            {
                this.ShowPreviousGroup = false;
                if (this.PageIndex == 1)
                {
                    this.ShowFirst = false;
                    this.ShowPrevious = false;
                }
                else
                {
                    this.ShowFirst = true;
                    this.ShowPrevious = true;
                }
            }
            else
            {
                this.ShowFirst = true;
                this.ShowPrevious = true;
                this.ShowPreviousGroup = true;
            }
        }

        public void ShowNextLastButtons()
        {
            if ((this.PageCount - this.StartPage) < this.NumericButtonCount)
            {
                if (this.PageIndex == this.PageCount)
                {
                    this.ShowNext = false;
                    this.ShowLast = false;
                }
                else
                {
                    this.ShowNext = true;
                    this.ShowLast = true;
                }
                this.ShowNextGroup = false;
            }
            else
            {
                this.ShowNext = true;
                this.ShowLast = true;
                this.ShowNextGroup = true;
            }
        }

        public bool ShowPagination()
        {
            if ((this.RecordCount < this.PageSize) || (this.RecordCount == this.PageSize))
            {
                return false;
            }
            return true;
        }

        [Browsable(true), Description("Define the control's style"), Category("Appearance")]
        public StyleList DefineStyle
        {
            get
            {
                return ((this.ViewState["_defineStyle"] == null) ? StyleList.None : ((StyleList)this.ViewState["_defineStyle"]));
            }
            set
            {
                this.ViewState["_defineStyle"] = value;
            }
        }

        [Browsable(true), Description("The number of buttons that every group page button displays"), Category("Appearance")]
        public int NumericButtonCount
        {
            get
            {
                return ((this.ViewState["_numericButtonCount"] == null) ? 10 : ((int)this.ViewState["_numericButtonCount"]));
            }
            set
            {
                this.ViewState["_numericButtonCount"] = value;
            }
        }

        [Description("Readonly-Total number of pages"), Category("Appearance"), Browsable(true)]
        public int PageCount
        {
            get
            {
                return (((this.RecordCount % this.PageSize) == 0) ? (this.RecordCount / this.PageSize) : ((this.RecordCount / this.PageSize) + 1));
            }
        }

        [Category("Appearance"), Browsable(true), Description("Index of current page")]
        public int PageIndex
        {
            get
            {
                return ((this.ViewState["_pageIndex"] == null) ? 1 : ((int)this.ViewState["_pageIndex"]));
            }
            set
            {
                this.ViewState["_pageIndex"] = value;
            }
        }

        [Browsable(true), Description("The number of rows that every page displays"), Category("Appearance")]
        public int PageSize
        {
            get
            {
                return ((this.ViewState["_pageSize"] == null) ? 20 : ((int)this.ViewState["_pageSize"]));
            }
            set
            {
                this.ViewState["_pageSize"] = value;
            }
        }

        [Category("Appearance"), Description("Total rows of record"), Browsable(true)]
        public int RecordCount
        {
            get
            {
                return ((this.ViewState["_recordCount"] == null) ? 0 : ((int)this.ViewState["_recordCount"]));
            }
            set
            {
                this.ViewState["_recordCount"] = value;
            }
        }

        [Category("Appearance"), Description("Whether to show the first button"), Browsable(true)]
        public bool ShowFirst
        {
            get
            {
                return ((this.ViewState["_showFirst"] == null) || ((bool)this.ViewState["_showFirst"]));
            }
            set
            {
                this.ViewState["_showFirst"] = value;
            }
        }

        [Browsable(true), DefaultValue(false), Description("Whether to show the goto page button"), Category("Appearance")]
        public bool ShowGotoPage
        {
            get
            {
                return ((this.ViewState["_showGotoPage"] != null) && ((bool)this.ViewState["_showGotoPage"]));
            }
            set
            {
                this.ViewState["_showGotoPage"] = value;
            }
        }

        [Category("Appearance"), Browsable(true), Description("Whether to show the last button")]
        public bool ShowLast
        {
            get
            {
                return ((this.ViewState["_showLast"] == null) || ((bool)this.ViewState["_showLast"]));
            }
            set
            {
                this.ViewState["_showLast"] = value;
            }
        }

        [Category("Appearance"), Description("Whether to show the next button"), Browsable(true)]
        public bool ShowNext
        {
            get
            {
                return ((this.ViewState["_showNext"] == null) || ((bool)this.ViewState["_showNext"]));
            }
            set
            {
                this.ViewState["_showNext"] = value;
            }
        }

        [Category("Appearance"), Description("Whether to show the next group button"), Browsable(true)]
        public bool ShowNextGroup
        {
            get
            {
                return ((this.ViewState["_showNextGroup"] == null) || ((bool)this.ViewState["_showNextGroup"]));
            }
            set
            {
                this.ViewState["_showNextGroup"] = value;
            }
        }

        [Description("Whether to show the previous button"), Browsable(true), Category("Appearance")]
        public bool ShowPrevious
        {
            get
            {
                return ((this.ViewState["_showPrevious"] == null) || ((bool)this.ViewState["_showPrevious"]));
            }
            set
            {
                this.ViewState["_showPrevious"] = value;
            }
        }

        [Description("Whether to show the previous group button"), Category("Appearance"), Browsable(true)]
        public bool ShowPreviousGroup
        {
            get
            {
                return ((this.ViewState["_showPreviousGroup"] == null) || ((bool)this.ViewState["_showPreviousGroup"]));
            }
            set
            {
                this.ViewState["_showPreviousGroup"] = value;
            }
        }

        [DefaultValue(false), Description("Whether to show the total page"), Browsable(true), Category("Appearance")]
        public bool ShowTotalPage
        {
            get
            {
                return ((this.ViewState["_showTotalPage"] != null) && ((bool)this.ViewState["_showTotalPage"]));
            }
            set
            {
                this.ViewState["_showTotalPage"] = value;
            }
        }

        [DefaultValue(false), Description("Whether to show the total number"), Browsable(true), Category("Appearance")]
        public bool ShowTotalNumber
        {
            get
            {
                return ((this.ViewState["_showTotalNum"] != null) && ((bool)this.ViewState["_showTotalNum"]));
            }
            set
            {
                this.ViewState["_showTotalNum"] = value;
            }
        }

        [Browsable(true), Description("The start page index of every group pages"), Category("Appearance")]
        public int StartPage
        {
            get
            {
                return ((this.ViewState["_startPage"] == null) ? 1 : ((int)this.ViewState["_startPage"]));
            }
            set
            {
                this.ViewState["_startPage"] = value;
            }
        }

        public enum StyleList
        {
            None,
            Standard
        }
    }

    public class Tool
    {
        public static bool isNumberR(string str)
        {
            return ((str != null) && Regex.IsMatch(str, @"^\d+$"));
        }
    }
}
