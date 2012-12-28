

using System;
using System.IO;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.HtmlControls;
using System.Collections.Specialized;
using System.Text;
using System.ComponentModel;
using System.ComponentModel.Design;
using System.Collections;


namespace BSCRM.Component
{
    /// <summary>
    /// validateTextBox 的摘要说明。
    /// </summary>
    /// 

    [Description("自验证TextBox控件")]
    [Designer(typeof(validateTextBoxDesigner))]
    [ToolboxData("<{0}:validateTextBox runat=server></{0}:validateTextBox>")]
    public class validateTextBox : System.Web.UI.WebControls.TextBox
    {


        #region"	公共属性	"


        [Browsable(true), Description("是否进行验证，设置为false时不验证，其他所有［验证相关属性］设置无效，控件为普通textbox"), Category("验证相关属性"), DefaultValue(true)]
        public virtual bool Check
        {
            get
            {
                object obj1 = this.ViewState["Check"];
                if (obj1 != null)
                {
                    return (bool)obj1;
                }
                return true;
            }

            set
            {
                this.ViewState["Check"] = value;
            }
        }



        [Browsable(true), Description("验证出错的提示方式，设置为true时弹出警告框，设置为false时文本筐背景变化"), Category("验证相关属性"), DefaultValue(true)]
        public virtual bool ErrWarning
        {
            get
            {
                object obj1 = this.ViewState["ErrWarning"];
                if (obj1 != null)
                {
                    return (bool)obj1;
                }
                return true;
            }

            set
            {
                this.ViewState["ErrWarning"] = value;
            }
        }


        [Browsable(true), Description("验证失败时文本框的背景色，仅在ErrWarning设置为false时有效。"), Category("验证相关属性"), DefaultValue("#ff0000")]
        public virtual string ErrColor
        {
            get
            {
                object obj1 = this.ViewState["ErrColor"];
                if (obj1 != null)
                {
                    return (string)obj1;
                }
                return "#ff0000";
            }

            set
            {
                this.ViewState["ErrColor"] = value;
            }
        }


        [Browsable(true), Description("与文本框关联的Button的ID号，若验证失败则该button不可用"), Category("验证相关属性"), DefaultValue("")]
        public virtual string SubmitButton
        {
            get
            {
                object obj1 = this.ViewState["SubmitButton"];
                if (obj1 != null)
                {
                    return ((string)obj1).Trim();
                }
                return "";
            }

            set
            {
                this.ViewState["SubmitButton"] = value;
            }
        }


        [Browsable(true), Description("是否进行自动去除首尾空格。"), Category("验证相关属性"), DefaultValue(true)]
        public virtual bool Trim
        {
            get
            {
                object obj1 = this.ViewState["Trim"];
                if (obj1 != null)
                {
                    return (bool)obj1;
                }
                return true;
            }

            set
            {
                this.ViewState["Trim"] = value;
            }
        }


        [Browsable(true), Description("文本框输入的最少字符数，设置为0时不限制最少输入"), Category("验证相关属性"), DefaultValue(0)]
        public virtual int LengthMin
        {
            get
            {
                object obj1 = this.ViewState["LengthMin"];
                if (obj1 != null)
                {
                    return (int)obj1;
                }
                return 0;
            }

            set
            {
                if (value < 0)
                {
                    throw new ArgumentOutOfRangeException("value");
                }
                this.ViewState["LengthMin"] = value;
            }
        }



        [Browsable(true), Description("文本框输入的最多字符数，设置为0时不限制最多输入"), Category("验证相关属性"), DefaultValue(0)]
        public virtual int LengthMax
        {
            get
            {
                object myobj = this.ViewState["LengthMax"];
                if (myobj != null)
                {
                    return (int)myobj;
                }
                return 0;
            }

            set
            {
                if (value < 0)
                {
                    throw new ArgumentOutOfRangeException("value");
                }
                this.ViewState["LengthMax"] = value;
            }
        }


        [Browsable(true), Description("自定义正则表达式，仅在RegularExpressionType设置为Custom时有效。"), Category("验证相关属性"), DefaultValue("")]
        public virtual string RegexSting
        {
            get
            {
                object obj1 = this.ViewState["RegexSting"];
                if (obj1 != null)
                {
                    return (string)obj1;
                }
                return "";
            }

            set
            {
                this.ViewState["RegexSting"] = value;
            }
        }

        [Browsable(true), Description("自定义正则表达式验证出错时的提示信息，仅在RegularExpressionType设置为Custom并且ErrWarning设置为True时有效。"), Category("验证相关属性"), DefaultValue("")]
        public virtual string RegexErr
        {
            get
            {
                object obj1 = this.ViewState["RegexErr"];
                if (obj1 != null)
                {
                    return (string)obj1;
                }
                return "";
            }

            set
            {
                this.ViewState["RegexErr"] = value;
            }
        }



        [Browsable(true), Description("文本框输入的正则表达式验证，设置为None时不验证，Int为非负整数，Date为1982-08-26格式日期，Simple只能输入字母数字下划线，Email为电子信箱，Custom为自定义正则表达式。"), Category("验证相关属性"), DefaultValue(0)]
        public RegularExpressionTypeList RegularExpressionType
        {
            get
            {
                object obj = ViewState["RegularExpressionType"];
                return (obj == null) ? RegularExpressionTypeList.None : (RegularExpressionTypeList)obj;
            }
            set { ViewState["RegularExpressionType"] = value; }
        }




        #endregion




        //正则表达式类别
        public enum RegularExpressionTypeList : byte
        {
            None,

            Int,

            Date,

            Simple,

            Email,

            Custom
        }


        #region"	私有属性	"


        //提交按钮客户端编号
        private string SubmitClientID
        {
            get
            {
                Control myControl = this.NamingContainer.FindControl(this.SubmitButton);
                if (myControl == null)
                {
                    return this.SubmitButton;
                }
                else
                {
                    return myControl.ClientID;
                }
            }
        }


        //提交按钮生效的条件
        private string SubmitOK
        {
            get
            {
                string str = "";
                for (int i = 0; i < this.Parent.Controls.Count; i++)
                {
                    try
                    {
                        validateTextBox myControl = (validateTextBox)this.Parent.Controls[i];
                        if (myControl.Check && myControl.SubmitButton == this.SubmitButton)
                        {
                            str += (bool)(str == "") ? script_Var_CheckOK_Head + myControl.ClientID : "&&" + script_Var_CheckOK_Head + myControl.ClientID;
                        }
                    }
                    catch { }
                }
                return str;
            }
        }


        //客户端脚本函数名称
        private string script_Function_Name
        {
            get
            {
                return "Function_" + this.ClientID.ToString();
            }
        }



        //客户端脚本提交按钮变量名称
        private string script_Var_SubmitName
        {
            get
            {
                return "Button_" + this.SubmitClientID;
            }
        }


        //客户端脚本自动执行函数名称
        private string script_AutoFunction_Name
        {
            get
            {
                return "Auto_" + this.SubmitClientID;
            }
        }


        //客户端脚本是否通过验证变量名称的前缀
        private string script_Var_CheckOK_Head
        {
            get
            {
                return "Pass_";
            }
        }



        //客户端脚本是否通过验证变量名称
        private string script_Var_CheckOK
        {
            get
            {
                return script_Var_CheckOK_Head + this.ClientID.ToString();
            }
        }


        #endregion


        public validateTextBox()
        {

        }

        protected override void Render(HtmlTextWriter writer)
        {
            if (this.Check)
            {
                String scriptString = "" + writer.NewLine;
                scriptString += @"<script language=JavaScript>" + writer.NewLine;

                scriptString += @"	if(" + this.script_AutoFunction_Name + @"==null){;" + writer.NewLine;
                scriptString += @"		var " + script_AutoFunction_Name + @"=function(){" + writer.NewLine;
                scriptString += @"			var " + this.script_Var_SubmitName + @"=document.getElementById(""" + this.SubmitClientID + @""");" + writer.NewLine;
                scriptString += @"			try{" + this.script_Var_SubmitName + @".disabled=""disabled"";}catch(e){}" + writer.NewLine;
                scriptString += @"		}" + writer.NewLine;
                scriptString += @"			window.attachEvent(""onload""," + this.script_AutoFunction_Name + @");" + writer.NewLine;
                scriptString += @"	}" + writer.NewLine;

                scriptString += @"		var " + this.script_Var_CheckOK + @"=false;" + writer.NewLine;


                scriptString += @"	function " + this.script_Function_Name + @"(){" + writer.NewLine;
                scriptString += @"		var mySubmit=document.getElementById(""" + this.SubmitClientID + @""");" + writer.NewLine;
                scriptString += @"		try{mySubmit.disabled=""disabled"";}catch(e){}" + writer.NewLine;
                scriptString += @"		var myInput=document.getElementById(""" + this.ClientID + @""");" + writer.NewLine;
                if (this.Trim)
                {
                    scriptString += @"		myInput.value=myInput.value.replace(/(^\s*)|(\s*$)/g, """");" + writer.NewLine;
                }


                //最少输入验证
                if (this.LengthMin > 0)
                {
                    scriptString += @"		var minLength=" + this.LengthMin + @";" + writer.NewLine;
                    scriptString += @"		if(myInput.value.replace(/[^\x00-\xff]/g,""aa"").length<minLength){" + writer.NewLine;
                    scriptString += @"			" + this.script_Var_CheckOK + @"=false;" + writer.NewLine;
                    scriptString += @"			" + this.ErrMsgFormat(@"输入内容不得少于【" + this.LengthMin.ToString() + @"】个字符！") + writer.NewLine;
                    scriptString += @"			return false;" + writer.NewLine;
                    scriptString += @"		}" + writer.NewLine;
                }

                //最多输入验证
                if (this.LengthMax > 0)
                {
                    scriptString += @"		var maxLength=" + this.LengthMax.ToString() + @";" + writer.NewLine;
                    scriptString += @"		if(myInput.value.replace(/[^\x00-\xff]/g,""cc"").length>maxLength){" + writer.NewLine;
                    scriptString += @"			" + this.script_Var_CheckOK + @"=false;" + writer.NewLine;
                    scriptString += @"			" + this.ErrMsgFormat(@"输入内容不得多于【" + this.LengthMax.ToString() + @"】个字符！") + writer.NewLine;
                    scriptString += @"			return false;" + writer.NewLine;
                    scriptString += @"		}" + writer.NewLine;

                }


                //正则表达式验证
                switch (this.RegularExpressionType)
                {
                    case RegularExpressionTypeList.None:   //不验证
                        break;
                    case RegularExpressionTypeList.Int:   //非负整数
                        scriptString += @"	var regExp_Int = new RegExp(""^\\d+$"");" + writer.NewLine;
                        scriptString += @"	if(!regExp_Int.test(myInput.value)){" + writer.NewLine;
                        scriptString += @"			" + this.script_Var_CheckOK + @"=false;" + writer.NewLine;
                        scriptString += @"			" + this.ErrMsgFormat(@"请输入一个非负整数！") + writer.NewLine;
                        scriptString += @"			return false;" + writer.NewLine;
                        scriptString += @"	}" + writer.NewLine;
                        break;
                    case RegularExpressionTypeList.Date:   //日期
                        scriptString += @"	var regExp_Date = new RegExp(""^((((19|20)(([02468][048])|([13579][26]))-02-29))|((20[0-9][0-9])|(19[0-9][0-9]))-((((0[1-9])|(1[0-2]))-((0[1-9])|(1\d)|(2[0-8])))|((((0[13578])|(1[02]))-31)|(((01,3-9])|(1[0-2]))-(29|30)))))$"");" + writer.NewLine;
                        scriptString += @"	if(!regExp_Date.test(myInput.value)){" + writer.NewLine;
                        scriptString += @"			" + this.script_Var_CheckOK + @"=false;" + writer.NewLine;
                        scriptString += @"			" + this.ErrMsgFormat(@"请输入【1982-08-26】格式的日期！") + writer.NewLine;
                        scriptString += @"			return false;" + writer.NewLine;
                        scriptString += @"	}" + writer.NewLine;
                        break;
                    case RegularExpressionTypeList.Email:   //电子信箱
                        scriptString += @"	var regExp_Email = new RegExp(""^[\\w-]+(\\.[\\w-]+)*@[\\w-]+(\\.[\\w-]+)+$"");" + writer.NewLine;
                        scriptString += @"	if(!regExp_Email.test(myInput.value)){" + writer.NewLine;
                        scriptString += @"			" + this.script_Var_CheckOK + @"=false;" + writer.NewLine;
                        scriptString += @"			" + this.ErrMsgFormat(@"请输入电子信箱！") + writer.NewLine;
                        scriptString += @"			return false;" + writer.NewLine;
                        scriptString += @"	}" + writer.NewLine;
                        break;

                    case RegularExpressionTypeList.Simple:   //一般字符
                        scriptString += @"	var regExp_Simple = new RegExp(""^\\w+$"");" + writer.NewLine;
                        scriptString += @"	if(!regExp_Simple.test(myInput.value)){" + writer.NewLine;
                        scriptString += @"			" + this.script_Var_CheckOK + @"=false;" + writer.NewLine;
                        scriptString += @"			" + this.ErrMsgFormat(@"只能输入数字、字母和下划线！") + writer.NewLine;
                        scriptString += @"			return false;" + writer.NewLine;
                        scriptString += @"	}" + writer.NewLine;
                        break;
                    case RegularExpressionTypeList.Custom:   //自定义
                        scriptString += @"	var regExp_Custom = new RegExp(""" + this.RegexSting + @""");" + writer.NewLine;
                        scriptString += @"	if(!regExp_Custom.test(myInput.value)){" + writer.NewLine;
                        scriptString += @"			" + this.script_Var_CheckOK + @"=false;" + writer.NewLine;
                        scriptString += @"			" + this.ErrMsgFormat(this.RegexErr) + writer.NewLine;
                        scriptString += @"			return false;" + writer.NewLine;
                        scriptString += @"	}" + writer.NewLine;
                        break;
                    default:
                        break;
                }

                scriptString += @"		" + this.script_Var_CheckOK + @"=true;" + writer.NewLine;
                scriptString += @"		myInput.style.backgroundColor="""";" + writer.NewLine;
                scriptString += @"		try{if(" + this.SubmitOK + @"){mySubmit.disabled="""";}}catch(e){}" + writer.NewLine;
                scriptString += @"		return true;" + writer.NewLine;
                scriptString += @"	}" + writer.NewLine;

                scriptString += @"<";
                scriptString += @"/";
                scriptString += @"script>" + writer.NewLine;

                writer.Write(scriptString);
                writer.AddAttribute("onBlur", this.script_Function_Name + "()");

            }
            base.Render(writer);
        }

        private string ErrMsgFormat(string strMsg)
        {

            if (this.ErrWarning)
            {
                //警告窗口
                return @"alert(""" + strMsg.Trim() + @""");";
            }
            else
            {
                //背景变色
                return @"myInput.style.backgroundColor=""" + this.ErrColor + @""";";
            }
        }



    }



    /// <summary>
    /// 自验证textbox控件设计器。
    /// </summary>
    public class validateTextBoxDesigner : System.Web.UI.Design.TextControlDesigner
    {
        private validateTextBox vTB;

        public validateTextBoxDesigner()
        {
            this.ReadOnly = true;
        }

        public override string GetDesignTimeHtml()
        {

            vTB = (validateTextBox)Component;
            StringWriter sw = new StringWriter();
            HtmlTextWriter writer = new HtmlTextWriter(sw);
            vTB.RenderControl(writer);

            string myStr = @"<span style=""width:10px; height:20px;color:#ff0000;font-size:18px; line-height:18px;margin-right:-5px;margin-left:5px;"">V</span>";

            return vTB.Check ? myStr + sw.ToString() : sw.ToString();

        }

        /// <summary>
        /// 获取在呈现控件时遇到错误后在设计时为指定的异常显示的 HTML。
        /// </summary>
        /// <param name="e">要为其显示错误信息的异常。</param>
        /// <returns>设计时为指定的异常显示的 HTML。</returns>
        protected override string GetErrorDesignTimeHtml(Exception e)
        {
            string errorstr = "创建控件时出错：" + e.Message;
            return CreatePlaceHolderDesignTimeHtml(errorstr);
        }
    }




}
