using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

[assembly: TagPrefix("FeliControls", "Feli")]
namespace FeliControls
{
    [DefaultProperty("ShowCountInfo")]
    [DefaultEvent("PageIndexChanged")]
    [ToolboxData("<{0}:Pager runat=server></{0}:Pager>")]
    public class Pager : CompositeControl
    {
        private bool blnNoClick = true;

        #region ----子控件----

        private Button btnFirst;
        private Button btnPrev;
        private Button btnNext;
        private Button btnLast;
        private Button btnDiy;

        private TextBox txtPageIndex;

        #endregion

        #region -----导航按钮属性-----

        [Bindable(true)]
        [Category("Appearance")]
        [DefaultValue("")]
        [Localizable(true)]
        [Description("是否显示数据总数及当前显示数据信息")]
        public bool ShowCountInfo
        {
            get
            {
                bool s = true;
                if(!this.DesignMode)
                {
                    if (ViewState["ShowCountInfo"] != null)
                    {
                        s = (bool)ViewState["ShowCountInfo"];
                    }
                }
                return s;
            }

            set
            {
                ViewState["ShowCountInfo"] = value;
            }
        }
        /// <summary>
        /// 第一页按钮文字描述
        /// </summary>
        [Bindable(true)]
        [Category("分页按钮属性")]
        [DefaultValue("")]
        [Localizable(true)]
        [Description("第一页按钮文字")]
        public string FirstPageText
        {
            get
            {
                EnsureChildControls();
                return btnFirst.Text;
            }

            set
            {
                EnsureChildControls();
                btnFirst.Text = value;
            }
        }
        /// <summary>
        /// 上一页按钮文字描述
        /// </summary>
        [Bindable(true)]
        [Category("分页按钮属性")]
        [DefaultValue("")]
        [Localizable(true)]
        [Description("上一页按钮文字")]
        public string PrevPageText
        {
            get
            {
                EnsureChildControls();
                return btnPrev.Text;
            }

            set
            {
                EnsureChildControls();
                btnPrev.Text = value;
            }
        }
        /// <summary>
        /// 下一页按钮文字描述
        /// </summary>
        [Bindable(true)]
        [Category("分页按钮属性")]
        [DefaultValue("")]
        [Localizable(true)]
        [Description("下一页按钮文字")]
        public string NextPageText
        {
            get
            {
                EnsureChildControls();
                return btnNext.Text;
            }

            set
            {
                EnsureChildControls();
                btnNext.Text = value;
            }
        }
        /// <summary>
        /// 最后一页按钮文字描述
        /// </summary>
        [Bindable(true)]
        [Category("分页按钮属性")]
        [DefaultValue("")]
        [Localizable(true)]
        [Description("最后一页按钮文字")]
        public string LastPageText
        {
            get
            {
                EnsureChildControls();
                return btnLast.Text;
            }

            set
            {
                EnsureChildControls();
                btnLast.Text = value;
            }
        }

        /// <summary>
        /// GO按钮文字描述
        /// </summary>
        [Bindable(true)]
        [Category("分页按钮属性")]
        [DefaultValue("")]
        [Localizable(true)]
        [Description("自定义跳转到页面按钮文字")]
        public string BtnGoText
        {
            get
            {
                EnsureChildControls();
                return btnDiy.Text;
            }

            set
            {
                EnsureChildControls();
                btnDiy.Text = value;
            }
        }

        #endregion

        #region ----分页属性----

        public int CurrentPageIndex
        {
            get
            {
                int idx = 1;
                if (!this.DesignMode)
                {
                    if (txtPageIndex.Text != null && txtPageIndex.Text != "")
                    {
                        idx = Convert.ToInt32(txtPageIndex.Text.Trim());
                    }
                }
                return idx;
            }
            set
            {
                EnsureChildControls();
                
                if (value < 2)
                {
                    value = 1;
                }
                if (value >= this.PageCount)
                {
                    value = this.PageCount;
                }
                if (ViewState["CurrentPageIndex"] != null)
                {
                    string strPage = ViewState["CurrentPageIndex"].ToString();
                }
                txtPageIndex.Text = value.ToString();
            }
        }
        /// <summary>
        /// 总页数
        /// </summary>
        public int PageCount
        {
            get
            {
                if (this.DesignMode)
                {
                    return 0;
                }
                else
                {
                    return RecordCount % PageSize == 0 ? RecordCount / PageSize : (RecordCount / PageSize) + 1;
                }
            }
        }
        /// <summary>
        /// 每页显示的记录数
        /// </summary>
        [Bindable(true)]
        [Category("分页信息")]
        [DefaultValue("")]
        [Localizable(true)]
        [Description("每页显示的数据数")]
        public int PageSize
        {
            get
            {
                int _size = 30;
                if (!this.DesignMode)
                {
                    if (ViewState["PageSize"] != null)
                    {
                        _size = (int)ViewState["PageSize"];
                    }
                }
                return _size;
            }
            set
            {
                ViewState["PageSize"] = value;
            }
        }
        
        /// <summary>
        /// 总记录数
        /// </summary>
        [Bindable(true)]
        [Category("分页信息")]
        [DefaultValue("")]
        [Localizable(true)]
        [Description("总记录数")]
        public int RecordCount
        {
            get
            {
                int idx = 0;
                if (!this.DesignMode)
                {
                    if (ViewState["RecordCount"] != null)
                    {
                        idx = (int)ViewState["RecordCount"];
                    }
                }
                return idx;
            }
            set
            {
                ViewState["RecordCount"] = value;
            }
        }

        #endregion

        /// <summary>
        /// 重写控件HTML代码开始标签
        /// </summary>
        protected override HtmlTextWriterTag TagKey
        {
            get
            {
                return HtmlTextWriterTag.Div;
            }
        }

        protected override void CreateChildControls()
        {
            this.Controls.Clear();

            btnFirst = new Button();
            btnFirst.ID = "btnFirst";
            btnFirst.CommandName = "Page";
            btnFirst.CommandArgument = "ButtonFirst";
            btnFirst.Click += new EventHandler(Change_PageIndex);
            btnFirst.Text = "第一页";
            btnFirst.CssClass = "input_border felipager_btn_first";
            
            this.Controls.Add(btnFirst);

            btnPrev = new Button();
            btnPrev.ID = "btnPrev";
            btnPrev.CommandName = "Page";
            btnPrev.CommandArgument = "ButtonPrev";
            btnPrev.Click += new EventHandler(Change_PageIndex);
            btnPrev.Text = "上一页";
            btnPrev.CssClass = "input_border mr20 felipager_btn_prev";
            
            this.Controls.Add(btnPrev);

            btnNext = new Button();
            btnNext.ID = "btnNext";
            btnNext.CommandName = "Page";
            btnNext.CommandArgument = "ButtonNext";
            btnNext.Click += new EventHandler(Change_PageIndex);
            btnNext.Text = "下一页";
            btnNext.CssClass = "input_border ml20 felipager_btn_next";
            
            this.Controls.Add(btnNext);

            btnLast = new Button();
            btnLast.ID = "btnLast";
            btnLast.CommandName = "Page";
            btnLast.CommandArgument = "ButtonLast";
            btnLast.Click += new EventHandler(Change_PageIndex);
            btnLast.Text = "最后一页";
            btnLast.CssClass = "input_border felipager_btn_last";
            
            this.Controls.Add(btnLast);

            btnDiy = new Button();
            btnDiy.ID = "btnDiy";
            btnDiy.CommandName = "Page";
            btnDiy.CommandArgument = "ButtonDiy";
            btnDiy.Click += new EventHandler(Change_PageIndex);
            btnDiy.Text = "GO";
            btnDiy.CssClass = "input_border felipager_btn_go";
            
            this.Controls.Add(btnDiy);

            txtPageIndex = new TextBox();
            txtPageIndex.ID = "txtPageIndex";
            txtPageIndex.Width = 30;
            txtPageIndex.CssClass = "input_border felipager_txt_pageindex";
            
            this.Controls.Add(txtPageIndex);

            base.CreateChildControls();
        }

        #region ----事件相关----

        public delegate void PageChangedEventHandler(object sender, PageChangedEventArgs e);

        private static readonly object EventPageIndexChanged = new object();

        public event PageChangedEventHandler PageIndexChanged
        {
            add
            {
                base.Events.AddHandler(EventPageIndexChanged, value);
            }
            remove
            {
                base.Events.RemoveHandler(EventPageIndexChanged, value);
            }
        }

        protected virtual void OnPageIndexChanged(PageChangedEventArgs e)
        {
            PageChangedEventHandler handler = Events[EventPageIndexChanged] as PageChangedEventHandler;
            if (handler != null)
            {
                handler(this, e);
            }
        }

        void Change_PageIndex(object sender, EventArgs e)
        {
            //触发了点击事件，则将blnNoClick设为false.
            blnNoClick = false;
            Button btn = (Button)sender;
            if (btn.CommandName == "Page")
            {
                PageChangedEventArgs ee = new PageChangedEventArgs();
                if (btn.CommandArgument == "ButtonFirst")
                {
                    this.CurrentPageIndex = 1;
                }
                if (btn.CommandArgument == "ButtonPrev")
                {
                    this.CurrentPageIndex -= 1;
                }
                if (btn.CommandArgument == "ButtonNext")
                {
                    this.CurrentPageIndex += 1;
                }
                if (btn.CommandArgument == "ButtonLast")
                {
                    this.CurrentPageIndex = this.PageCount;
                }
                if (btn.CommandArgument == "ButtonDiy")
                {
                    int intDiyIdx = 1;
                    //如果输入数字不合法，则跳转到第一页
                    if (int.TryParse(this.txtPageIndex.Text, out intDiyIdx))
                    {
                        if (intDiyIdx > 0)
                        {
                            this.CurrentPageIndex = intDiyIdx;
                        }
                        else
                        {
                            this.Page.RegisterStartupScript("tishi", "<script>alert('页索引必须为大于0的整数');</script>");
                            this.CurrentPageIndex = 1;
                        }
                    }
                    else
                    {
                        this.Page.RegisterStartupScript("tishi", "<script>alert('页索引必须为整数');</script>");
                        this.CurrentPageIndex = 1;
                    }
                }
                ee.CurrentPageIndex = this.CurrentPageIndex;
                ee.PageCount = this.PageCount;
                ee.PageSize = this.PageSize;
                ee.RecordCount = this.RecordCount;
                this.OnPageIndexChanged(ee);
            }
        }

        #endregion

        protected override void RenderContents(HtmlTextWriter output)
        {
            //如果没有触发点击事件，则将当前页索引设为1
            if (blnNoClick)
            {
                this.CurrentPageIndex = 1;
            }
            //output.WriteAttribute("class", "felipager_main");
            //output.AddAttribute(HtmlTextWriterAttribute.Class, "felipager_main");
            //output.RenderBeginTag(HtmlTextWriterTag.Div);
            output.AddAttribute(HtmlTextWriterAttribute.Class, "felipager_displayinfo");
            output.RenderBeginTag(HtmlTextWriterTag.Div);
            //如果需要显示数据总数信息，则显示
            if (this.ShowCountInfo)
            {
                int intStart = this.PageSize * (this.CurrentPageIndex - 1);
                if (intStart < 1)
                {
                    intStart = 0;
                }
                int intEnd = this.PageSize * this.CurrentPageIndex;
                if (intEnd > this.RecordCount)
                {
                    intEnd = this.RecordCount;
                }
                intStart += 1;//每页的第一条的序号应该是1，而不是0
                output.Write("共有 {0} 条数据，当前显示 {1} - {2} 条", this.RecordCount, intStart, intEnd);
            }
            output.RenderEndTag();

            output.AddAttribute(HtmlTextWriterAttribute.Class, "felipager_navbox");
            output.RenderBeginTag(HtmlTextWriterTag.Div);

            #region ----控制导航按钮状态

            //如果是第一页，则该按钮不可用
            if (this.CurrentPageIndex == 1)
            {
                btnFirst.Enabled = false;
            }
            else
            {
                btnFirst.Enabled = true;
            }
            //如果是第一页，则该按钮不可用
            if (this.CurrentPageIndex == 1)
            {
                btnPrev.Enabled = false;
            }
            else
            {
                btnPrev.Enabled = true;
            }
            //如果是最后一页，则该按钮不可用
            if (this.CurrentPageIndex >= this.PageCount)
            {
                btnNext.Enabled = false;
            }
            else
            {
                btnNext.Enabled = true;
            }
            //如果是最后一页，则该按钮不可用
            if (this.CurrentPageIndex >= this.PageCount)
            {
                btnLast.Enabled = false;
            }
            else
            {
                btnLast.Enabled = true;
            }
            //如果只有一页，则该按钮不可用
            if (this.PageCount <= 1)
            {
                btnDiy.Enabled = false;
            }
            else
            {
                btnDiy.Enabled = true;
            }
            //如果只有一页，则该输入框不可用
            if (this.PageCount <= 1)
            {
                txtPageIndex.Enabled = false;
            }
            else
            {
                txtPageIndex.Enabled = true;
            }

            #endregion

            btnFirst.RenderControl(output);
            btnPrev.RenderControl(output);
            output.Write("当前第");
            txtPageIndex.Text = this.CurrentPageIndex.ToString();
            txtPageIndex.RenderControl(output);
            btnDiy.RenderControl(output);
            output.Write("页，共{0}页", this.PageCount);
            btnNext.RenderControl(output);
            btnLast.RenderControl(output);

            output.RenderEndTag();
            //output.RenderEndTag();

            //output.Write(Text);
        }
    }
}
