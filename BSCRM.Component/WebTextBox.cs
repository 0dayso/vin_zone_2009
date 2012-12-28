using System;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.ComponentModel;
using System.Drawing;
using System.Text;
using System.Text.RegularExpressions;
using System.Collections;
using System.Data;

namespace MyFramework.Component
{

    #region 枚举数据
    /// <summary>
    /// 验证数据类型
    /// </summary>
    public enum DataType
    {
        Never,					//不验证
        String,					//字符串
        Num,                    //数字
        Int,					//整数
        IntPostive,				//大于0的整数
        IntZeroPostive,			//大于等于0的整数
        Float,					//数字
        FloatPostive,			//大于0的数字
        FloatZeroPostive,		//大于等于0的数字
        Url,
        Mail,
        ChineseChars,			//汉字
        EnglishChars,			//英文
        EngNum,					//英文和数字
        EngNumUnerline,			//英文、数字和下划线
        PhoneNumber,			//电话号码
        MobileNumber,			//手机号码
        PostalCode,				//邮政编码
        HtmlTag,				//Html标记
        DateTime,               //时间
        Custom,
        ValidChar              //有效字符
    }
    #endregion

    /// Attribute DefaultProperty指定组件的默认属性，ToolboxData指定当从IDE工具中的工具箱中拖动自定义控件时为它生成的默认标记
    [DefaultProperty("AllowEmpty"), ToolboxData("<{0}:WebTextBox runat=server></{0}:WebTextBox>")]
    //类MyControl派生自WebControl
    public class WebTextBox : System.Web.UI.WebControls.TextBox
    {
        #region 子控件
        //private System.Web.UI.WebControls.TextBox txtDataInput = new TextBox();
        Label lblExplain = new Label();			//初始化提示信息
        private System.Web.UI.WebControls.RequiredFieldValidator rfvDataInput = new RequiredFieldValidator();
        private System.Web.UI.WebControls.RegularExpressionValidator revDataInput = new RegularExpressionValidator();
        private Panel pnlFrame = new Panel();				//承载其它控件的容器Panel控件
        public DropDownList drpList = new DropDownList();	//可输入下拉框模式下的下拉框
        #endregion

        private string m_Value;	//下拉框选中的值
        private string initialString;
        private string error = "";
        private Hashtable ht = new Hashtable();

        #region 控件自定义属性

        [Bindable(true)]
        [Category("自定义信息区")]
        [Browsable(true)]
        [Description("Encode")]
        [DefaultValue("true")]
        public override string Text
        {
            get
            {
                return base.Text == null ? "" : System.Web.HttpUtility.HtmlDecode(base.Text);
            }
            set
            {
                base.Text = System.Web.HttpUtility.HtmlEncode(value);
            }
        }

        [Bindable(true)]
        [Category("自定义信息区")]
        [Browsable(true)]
        [Description("Encode")]
        [DefaultValue("true")]
        public bool IsEncode
        {
            get { return ViewState["IsEncode"] == null ? false : (bool)ViewState["IsEncode"]; }
            set { ViewState["IsEncode"] = value; }
        }

        [Bindable(true)]
        [Category("自定义信息区")]
        [Browsable(true)]
        [Description("是否允许空值")]
        [DefaultValue("true")]
        public bool AllowEmpty
        {
            get { return ViewState["AllowEmpty"] == null ? true : (bool)ViewState["AllowEmpty"]; }
            set { ViewState["AllowEmpty"] = value; }
        }

        [Bindable(true)]
        [Category("自定义信息区")]
        [Browsable(true)]
        [Description("不允许空的提示消息")]
        [DefaultValue("true")]
        public string EmptyMessage
        {
            get { return ViewState["EmptyMessage"] == null ? "*不能为空" : (string)ViewState["EmptyMessage"]; }
            set { ViewState["EmptyMessage"] = value; }
        }

        [Bindable(true)]
        [Category("自定义信息区")]
        [Browsable(true)]
        [Description("输入提示信息")]
        [DefaultValue("true")]
        public string InputExplain
        {
            get { return ViewState["InputExplain"] == null ? "" : (string)ViewState["InputExplain"]; }
            set { ViewState["InputExplain"] = value; }
        }

        [Bindable(true)]
        [Category("自定义信息区")]
        [Browsable(true)]
        [Description("验证数据类型，默认为不验证")]
        [DefaultValue("IntPostive")]
        public DataType ValidType
        {
            get { return ViewState["ValidType"] == null ? DataType.Never : (DataType)ViewState["ValidType"]; }
            set { ViewState["ValidType"] = value; }
        }

        [Bindable(true)]
        [Browsable(true)]
        [Category("自定义信息区")]
        [Description("自定义验证错误信息")]
        [DefaultValue("")]
        public string ValidError
        {
            get { return ViewState["ValidError"] == null ? "" : (string)ViewState["ValidError"]; }
            set { ViewState["ValidError"] = value; }
        }

        [Bindable(true)]
        [Browsable(true)]
        [Category("自定义信息区")]
        [Description("自定义用于验证的正则表达式，ValidType 为 Custom 时有效")]
        [DefaultValue("")]
        public string ValidExpressionCustom
        {
            get { return ViewState["ValidExpressionCustom"] == null ? "" : (string)ViewState["ValidExpressionCustom"]; }
            set { ViewState["ValidExpressionCustom"] = value; }
        }

        [Bindable(true)]
        [Browsable(true)]
        [Category("自定义信息区")]
        [Description("输入框处于焦点时的背景颜色")]
        [DefaultValue("")]
        public string BackColorFocus
        {
            get { return ViewState["ColorFocus"] == null ? "#FFEDD6" : (string)ViewState["ColorFocus"]; }
            set { ViewState["ColorFocus"] = value; }
        }
        [Bindable(true)]
        [Browsable(true)]
        [Category("自定义信息区")]
        [Description("输入框处于焦点时的CSS类名")]
        [DefaultValue("")]
        public string CssFocus
        {
            get { return ViewState["CssFocus"] == null ? "inputintofocus" : (string)ViewState["CssFocus"]; }
            set { ViewState["CssFocus"] = value; }
        }

        [Bindable(true)]
        [Browsable(true)]
        [Category("自定义信息区")]
        [Description("输入提示信息的CSS类名")]
        [DefaultValue("")]
        public string CssExplain
        {
            get { return ViewState["CssExplain"] == null ? "" : (string)ViewState["CssExplain"]; }
            set { ViewState["CssExplain"] = value; }
        }


        [Bindable(true)]
        [Browsable(true)]
        [Category("自定义信息区")]
        [Description("错误信息提示的CSS类名")]
        [DefaultValue("")]
        public string CssError
        {
            get { return ViewState["CssError"] == null ? "" : (string)ViewState["CssError"]; }
            set { ViewState["CssError"] = value; }
        }

        [Bindable(true)]
        [Browsable(true)]
        [Category("自定义信息区")]
        [Description("可调整控件高度，该模式下，TextBox的TextMode属性自动转换为MultiLine模式")]
        [DefaultValue("")]
        public bool IsRegulateHeight
        {
            get { return ViewState["IsRegulateHeight"] == null ? false : (bool)ViewState["IsRegulateHeight"]; }
            set
            {
                ViewState["IsRegulateHeight"] = value;
                base.TextMode = TextBoxMode.MultiLine;
            }
        }

        [Bindable(true)]
        [Browsable(true)]
        [Category("自定义信息区")]
        [Description("调节高度递增或递减行数因子")]
        [DefaultValue("")]
        public int RegulateRows
        {
            get { return ViewState["RegulateRows"] == null ? 1 : (int)ViewState["RegulateRows"]; }
            set
            {
                ViewState["RegulateRows"] = value;
            }
        }

        [Bindable(true)]
        [Browsable(true)]
        [Category("自定义信息区")]
        [Description("增加控件高度的图片路径，请使用相对站点根的相对路径")]
        [DefaultValue("")]
        public string ImageIconPlus
        {
            get
            {
                if (ViewState["ImageIconPlus"] == null)
                {
                    return "<span style=\"font-size:14px;font-weight:600;cursor:hand;\" onclick=\"PlusHeight('" + this.ClientID + "');\">+</span>";
                }
                else
                {
                    return "<img src=\"" + (string)ViewState["ImageIconPlus"] + "\" border=\"0\" style=\"cursor:hand;\" onclick=\"PlusHeight('" + this.ClientID + "');\"/>";
                }
            }
            set
            {
                ViewState["ImageIconPlus"] = value;
            }
        }
        [Bindable(true)]
        [Browsable(true)]
        [Category("自定义信息区")]
        [Description("减小控件高度的图片路径，请使用相对站点根的相对路径")]
        [DefaultValue("")]
        public string ImageIconMinus
        {
            get
            {
                if (ViewState["ImageIconMinus"] == null)
                {
                    return "<span style=\"font-size:14px;font-weight:600;cursor:hand;\" onclick=\"MinusHeight('" + this.ClientID + "');\">-</span>";
                }
                else
                {
                    return "<img src=\"" + (string)ViewState["ImageIconMinus"] + "\" border=\"0\" style=\"cursor:hand;\" onclick=\"MinusHeight('" + this.ClientID + "');\"/>";
                }
            }
            set
            {
                ViewState["ImageIconMinus"] = value;
            }
        }

        [Bindable(true)]
        [Browsable(true)]
        [Category("自定义信息区")]
        [Description("下拉框，该模式下，TextBox为可输入的下拉框")]
        [DefaultValue("")]
        public bool HasDropDownList
        {
            get { return ViewState["HasDropDownList"] == null ? false : (bool)ViewState["HasDropDownList"]; }
            set
            {
                ViewState["HasDropDownList"] = value;
                base.TextMode = TextBoxMode.SingleLine;
            }
        }

        [Bindable(true)]
        [Browsable(true)]
        [Category("自定义信息区")]
        [Description("下是设置默认项")]
        [DefaultValue("")]
        public bool HasDefaultItem
        {
            get { return ViewState["HasDefaultItem"] == null ? false : (bool)ViewState["HasDefaultItem"]; }
            set
            {
                ViewState["HasDefaultItem"] = value;
                base.TextMode = TextBoxMode.SingleLine;
            }
        }

        //[Bindable(true)]
        //[Browsable(true)]
        //[Category("自定义信息区")]
        //[Description("下拉框显示按钮，请使用相对站点根的相对路径")]
        //[DefaultValue("")]
        //public string ImageIconShowDropDown
        //{
        //    get { return ViewState["ImageIconShowDropDown"] == null ? "" : (string)ViewState["ImageIconShowDropDown"]; }
        //    set { ViewState["ImageIconShowDropDown"] = value; }
        //}

        //[Bindable(true)]
        //[Browsable(true)]
        //[Category("自定义信息区")]
        //[Description("下拉框隐藏按钮，请使用相对站点根的相对路径")]
        //[DefaultValue("")]
        //public string ImageIconHideDropDown
        //{
        //    get { return ViewState["ImageIconHideDropDown"] == null ? "" : (string)ViewState["ImageIconHideDropDown"]; }
        //    set {ViewState["ImageIconHideDropDown"] = value; }
        //}

        [Bindable(true)]
        [Browsable(false)]
        [Category("自定义信息区")]
        [Description("下拉框的数据源")]
        [DefaultValue("")]
        public DataTable DropDownListDataSource
        {
            get { return ViewState["DropDownListDataSource"] == null ? null : (DataTable)ViewState["DropDownListDataSource"]; }
            set
            {
                ViewState["DropDownListDataSource"] = value;
            }
        }

        [Bindable(true)]
        [Browsable(true)]
        [Category("Data")]
        [Description("下拉框的数据源的显示字段")]
        [DefaultValue("")]
        public string DataTextField
        {
            get { return ViewState["DataTextField"] == null ? "" : (string)ViewState["DataTextField"]; }
            set
            {
                ViewState["DataTextField"] = value;
            }
        }

        [Bindable(true)]
        [Browsable(true)]
        [Category("Data")]
        [Description("下拉框的数据源的值字段")]
        [DefaultValue("")]
        public string DataValueField
        {
            get { return ViewState["DataValueField"] == null ? "" : (string)ViewState["DataValueField"]; }
            set
            {
                ViewState["DataValueField"] = value;
            }
        }

        [Bindable(true)]
        [Browsable(true)]
        [Category("Data")]
        [Description("下拉框的默认项的文本")]
        [DefaultValue("")]
        public string DefaultItemText
        {
            get { return ViewState["DefaultItemText"] == null ? "" : (string)ViewState["DefaultItemText"]; }
            set
            {
                ViewState["DefaultItemText"] = value;
            }
        }

        [Bindable(true)]
        [Browsable(true)]
        [Category("Data")]
        [Description("下拉框的默认项的值")]
        [DefaultValue("")]
        public string DefaultItemValue
        {
            get { return ViewState["DefaultItemValue"] == null ? "" : (string)ViewState["DefaultItemValue"]; }
            set
            {
                ViewState["DefaultItemValue"] = value;
            }
        }

        [Bindable(true)]
        [Browsable(true)]
        [Category("Data")]
        [Description("下拉框的默认项的值")]
        [DefaultValue("")]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Content), PersistenceMode(PersistenceMode.InnerProperty)]
        public string Value
        {
            get { return m_Value; }
            set { m_Value = value; }
        }

        #endregion

        #region 构造函数
        public WebTextBox() { }
        #endregion

        #region EnsureChildControls
        protected override void EnsureChildControls()
        {
            this.rfvDataInput.CssClass = this.CssError;
            this.rfvDataInput.ErrorMessage = EmptyMessage;
            this.rfvDataInput.Display = System.Web.UI.WebControls.ValidatorDisplay.Dynamic;
            this.rfvDataInput.EnableViewState = true;
            this.rfvDataInput.ControlToValidate = base.ID;

            this.revDataInput.CssClass = this.CssError;
            this.revDataInput.ErrorMessage = EmptyMessage;
            this.revDataInput.Display = System.Web.UI.WebControls.ValidatorDisplay.Dynamic;
            this.revDataInput.EnableViewState = true;
            this.revDataInput.ControlToValidate = base.ID;

            this.lblExplain.CssClass = this.CssExplain;
            this.lblExplain.EnableViewState = true;
            this.lblExplain.Text = InputExplain;

            //将子控件添加到此自定义控件中
            this.Controls.Add(lblExplain);
            this.Controls.Add(rfvDataInput);
            this.Controls.Add(revDataInput);
            this.Controls.Add(drpList);
            this.Controls.Add(pnlFrame);
        }
        #endregion

        /// <summary>
        /// 根据设置的验证数据类型返回不同的正则表达式样
        /// </summary>
        /// <returns></returns>
        #region GetRegex
        private string GetValidRegex()
        {
            string regex = @"(\S)";
            switch (this.ValidType)
            {
                case DataType.Never:
                    break;
                case DataType.Num:
                    error = "*必须为数字";
                    regex = @"[0-9]*";
                    break;
                case DataType.Int:
                    error = "*必须为整数";
                    regex = @"(-)?(\d+)";
                    break;
                case DataType.IntPostive:
                    error = "*必须为大于0的整数";
                    regex = @"[0-9]*[1-9][0-9]*";
                    break;
                case DataType.IntZeroPostive:
                    error = "*必须为不小于0的整数";
                    regex = @"(\d+)";
                    break;
                case DataType.Float:
                    error = "*必须为数字";
                    regex = @"(-)?(\d+)(((\.)(\d)+))?";
                    break;
                case DataType.FloatPostive:
                    error = "*必须为大于0的数字";
                    regex = @"(([0-9]+\.[0-9]*[1-9][0-9]*)|([0-9]*[1-9][0-9]*\.[0-9]+)|([0-9]*[1-9][0-9]*))";
                    break;
                case DataType.FloatZeroPostive:
                    error = "*必须为不小于0的数字";
                    regex = @"(\d+)(((\.)(\d)+))?";
                    break;
                case DataType.Url:
                    error = "*URL格式错误";
                    regex = @"(http://)?([\w-]+\.)+[\w-]+(/[\w- ./?%&=]*)?";
                    break;
                case DataType.Mail:
                    error = "*EMail格式错误";
                    regex = @"\w+([-+.]\w+)*@\w+([-.]\w+)*\.\w+([-.]\w+)*";
                    break;
                case DataType.ChineseChars:
                    error = "*只能输入中文";
                    regex = @"[\u4e00-\u9fa5]*";
                    break;
                case DataType.EnglishChars:
                    error = "*只能输入英文";
                    regex = @"[a-zA-Z]*";
                    break;
                case DataType.EngNum:
                    error = "*只能输入英文和数字";
                    regex = @"[a-zA-Z0-9]*";
                    break;
                case DataType.EngNumUnerline:
                    error = "*只能输入英文、数字和下划线";
                    regex = @"[a-zA-Z0-9_]*";
                    break;
                case DataType.PhoneNumber:
                    error = "*电话号码格式错误";
                    regex = @"(86)?(-)?(0\d{2,3})?(-)?(\d{7,8})(-)?(\d{1,5})?";
                    break;
                case DataType.MobileNumber:
                    error = "*手机号码格式错误";
                    regex = @"13[0-9]{1}[0-9]{8}$|^15[0-9]{1}[0-9]{8}$|^18[0-9]{1}[0-9]{8}$";
                    break;
                case DataType.PostalCode:
                    error = "*邮编格式错误";
                    regex = @"\d{6}";
                    break;
                case DataType.DateTime:
                    error = "*时间格式错误";
                    regex = @"((2[0-3])|([0-1][0-9]))[:]([0-5]\d{1})";
                    break;
                case DataType.HtmlTag:
                    error = "*Html格式错误";
                    regex = @"(<(.*)>.*<\/\1>)|(<(.*) \/>)";
                    break;
                case DataType.Custom:
                    error = "*格式错误";
                    regex = this.ValidExpressionCustom;
                    break;
                case DataType.ValidChar:
                    error = "请输入有效字符";
                    regex = @"([\u4e00-\u9fa5a-zA-Z0-9_])+";
                    break;
                default:
                    break;
            }
            if (this.ValidError.Trim() != "")	//如果设置自定义错误提示，则使用自定义错误提示
                error = this.ValidError;
            return regex;
        }
        #endregion

        #region 将此控件呈现给指定的输出参数
        /// <summary> 
        /// 将此控件呈现给指定的输出参数。
        /// </summary>
        /// <param name="output"> 要写出到的 HTML 编写器 </param>
        protected override void Render(HtmlTextWriter output)
        {
            if (this.BackColorFocus != "")
            {
                base.Attributes.Add("onfocus", "this.style.background='" + this.BackColorFocus + "'");
                //if(!HasDropDownList)
                base.Attributes.Add("onblur", "this.style.background='#ffffff'");
                //else
                //	base.Attributes.Add("onblur", "this.style.background='#ffffff';if(!document.getElementById('" + this.ClientID + "List').focused){document.getElementById('" + this.ClientID + "List').style.display='none';}");
            }
            //if (this.CssFocus != "")
            //{
            //    base.Attributes.Add("onmousedown", "this.className='" + this.CssFocus + "'");
            //    base.Attributes.Add("onmouseout", "this.className='" + this.CssClass + "'");
            //}
            if (this.HasDropDownList)
            {
                base.Attributes.Add("title", "点击显示下拉框");
                base.Attributes.Add("onclick", "ShowList('" + this.ClientID + "');");
            }
            //输出
            base.Render(output);
            output.Write("&nbsp;");

            if (InputExplain.Trim() != "")
            {
                this.lblExplain.ID = "lbl" + base.ID;
                this.lblExplain.RenderControl(output);
            }

            if (!this.AllowEmpty)
            {
                this.rfvDataInput.ID = "rfv" + base.ID;
                this.rfvDataInput.ControlToValidate = base.ID;
                this.rfvDataInput.RenderControl(output);
               
            }

            if (this.ValidType != DataType.Never && this.ValidType != DataType.String)
            {
                this.revDataInput.ID = "rev" + base.ID;
                this.revDataInput.ControlToValidate = base.ID;
                this.revDataInput.ValidationExpression = this.GetValidRegex();
                this.revDataInput.ErrorMessage = error;
                this.revDataInput.RenderControl(output);
            }

            //可调整高度
            if (this.IsRegulateHeight)
            {
                #region
                StringBuilder sbscript = new StringBuilder();
                sbscript.Append("\n<!--调节输入框高度 begin-->\n");
                sbscript.Append("	<script type=\"text/javascript\">\n");
                sbscript.Append("		//Plus\n");
                sbscript.Append("		function PlusHeight(id)\n");
                sbscript.Append("		{\n");
                sbscript.Append("			var txtInput = document.getElementById(id);\n");
                sbscript.Append("				txtInput.rows = parseInt(txtInput.rows) + " + this.RegulateRows + ";\n");
                //sbscript.Append("			txtInput.style.height = txtInput.style.height+15;\n");
                sbscript.Append("		}\n");
                sbscript.Append("		//Minus\n");
                sbscript.Append("		function MinusHeight(id)\n");
                sbscript.Append("		{\n");
                sbscript.Append("			var txtInput = document.getElementById(id);\n");
                sbscript.Append("			if( parseInt(txtInput.rows) >= " + (this.RegulateRows + 1) + " )\n");
                sbscript.Append("				txtInput.rows = parseInt(txtInput.rows) - " + this.RegulateRows + ";\n");
                sbscript.Append("		}\n");
                sbscript.Append("	</script>\n");
                sbscript.Append("<!--调节输入框高度 end-->\n");

                System.Web.UI.ClientScriptManager client = this.Page.ClientScript;
                Type cstype = Page.GetType();
                if (!client.IsStartupScriptRegistered(cstype, "regulateheight"))
                    client.RegisterStartupScript(cstype, "regulateheight", sbscript.ToString());

                StringBuilder sbhtml = new StringBuilder();
                sbhtml.Append("<div style=\"height:24px;\">");
                sbhtml.Append("	" + this.ImageIconMinus + "&nbsp;&nbsp;" + this.ImageIconPlus);
                sbhtml.Append("</div>");
                output.Write(sbhtml.ToString());
                #endregion
            }
            //下拉框模式
            if (this.HasDropDownList)
            {
                #region
                //已经改为点击输入框的时候激发客户端事件
                //output.Write("<img id=\"" + this.ClientID + "Icon\" src=\"" + this.ImageIconShowDropDown + "\" border=\"0\" style=\"cursor:hand;\" onclick=\"ShowList('" + this.ClientID + "');\" align=\"absmiddle\" />\n");
                int width = Convert.ToInt32(base.Width.Value);
                width = width == 0 ? 100 : width;
                output.AddAttribute(System.Web.UI.HtmlTextWriterAttribute.Style, "display:none;position:absolute;");
                output.AddAttribute(System.Web.UI.HtmlTextWriterAttribute.Id, this.ClientID + "List");
                output.RenderBeginTag(HtmlTextWriterTag.Div);
                output.AddAttribute(System.Web.UI.HtmlTextWriterAttribute.Style, "margin-left:" + width + "px;");
                output.RenderBeginTag(HtmlTextWriterTag.Span);

                drpList.ID = base.ID + "Items";
                drpList.Style.Add("margin-left", "-" + width + "px");
                drpList.Attributes.Add("onchange", "document.getElementById('" + this.ClientID + "').value=this.options[this.selectedIndex].text;document.getElementById('" + this.ClientID + "List').style.display='none';");
                //drpList.Attributes.Add("onblur", "this.style.display='none'");
                if (this.DropDownListDataSource != null && this.DropDownListDataSource.Rows.Count > 0)
                {
                    drpList.DataTextField = this.DataTextField;
                    drpList.DataValueField = this.DataValueField;
                    drpList.DataSource = this.DropDownListDataSource.DefaultView;
                    drpList.DataBind();
                }
                if (HasDefaultItem)
                    drpList.Items.Insert(0, new ListItem(this.DefaultItemText, this.DefaultItemValue));
                foreach (ListItem item in drpList.Items)
                {
                    if (item.Text == base.Text.Trim())
                    {
                        item.Selected = true;
                        break;
                    }
                }
                this.drpList.RenderControl(output);
                output.RenderEndTag();
                output.RenderEndTag();

                StringBuilder sbscript = new StringBuilder();
                #region js内容
                sbscript.Append("\n<script type=\"text/javascript\">\n");
                sbscript.Append("	<!--下拉框显示隐藏 begin-->\n");
                sbscript.Append("	function ShowList(id)\n");
                sbscript.Append("	{\n");
                sbscript.Append("		var txtInput = document.getElementById(id);\n");
                sbscript.Append("		var drpList = document.getElementById(id+'List');\n");
                //sbscript.Append("		var drpIcon = document.getElementById(id+'Icon');\n");
                sbscript.Append("		var drpItems = document.getElementById(id+'Items');\n");	//下拉框
                sbscript.Append("		if(drpList.style.display == 'block')\n");
                sbscript.Append("		{\n");
                sbscript.Append("			txtInput.title = '点击显示下拉框';\n");
                sbscript.Append("			drpList.style.display = 'none';\n");
                //sbscript.Append("			drpIcon.src = '" + this.ImageIconShowDropDown + "';\n");
                sbscript.Append("		}\n");
                sbscript.Append("		else\n");
                sbscript.Append("		{\n");
                sbscript.Append("			txtInput.title = '点击关闭下拉框';\n");
                sbscript.Append("			drpList.style.display = 'block';\n");
                sbscript.Append("			drpList.style.left = GetOffsetLeft(txtInput)+'px';\n");
                sbscript.Append("			drpList.style.top = (GetOffsetTop(txtInput)+1)+'px';\n");
                sbscript.Append("			drpItems.style.width = txtInput.offsetWidth+'px';\n");
                //sbscript.Append("			drpIcon.src = '" + this.ImageIconHideDropDown + "';\n");
                sbscript.Append("		}\n");
                sbscript.Append("	}\n");
                sbscript.Append("	<!--下拉框显示隐藏 end-->\n");
                sbscript.Append("	function GetOffsetTop(e) \n");
                sbscript.Append("	{\n");
                sbscript.Append("		var offsetTop = e.offsetTop;\n");
                sbscript.Append("		var offsetParent = e.offsetParent;\n");
                sbscript.Append("		while(offsetParent)\n");
                sbscript.Append("		{\n");
                sbscript.Append("			offsetTop += offsetParent.offsetTop;\n");
                sbscript.Append("			offsetParent = offsetParent.offsetParent;\n");
                sbscript.Append("		}\n");
                sbscript.Append("		return offsetTop+e.offsetHeight;\n");
                sbscript.Append("	}\n");
                sbscript.Append("	function GetOffsetLeft(e) \n");
                sbscript.Append("	{\n");
                sbscript.Append("		var offsetLeft = e.offsetLeft;\n");
                sbscript.Append("		var offsetParent = e.offsetParent;\n");
                sbscript.Append("		while(offsetParent) \n");
                sbscript.Append("		{\n");
                sbscript.Append("			offsetLeft += offsetParent.offsetLeft;\n");
                sbscript.Append("			offsetParent = offsetParent.offsetParent;\n");
                sbscript.Append("		}\n");
                sbscript.Append("		return offsetLeft;\n");
                sbscript.Append("	}\n");
                sbscript.Append("</script>\n");
                # endregion
                System.Web.UI.ClientScriptManager client = this.Page.ClientScript;
                Type cstype = Page.GetType();
                if (!client.IsStartupScriptRegistered(cstype, "showlist"))
                    client.RegisterStartupScript(cstype, "showlist", sbscript.ToString());

                #endregion
            }

        }
        #endregion

    }
}

