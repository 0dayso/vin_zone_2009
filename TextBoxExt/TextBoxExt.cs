using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace NTextBoxExt
{
    [DefaultProperty("Text")]
    [ToolboxData("<{0}:TextBoxExt runat=server></{0}:TextBoxExt>")]
    public class TextBoxExt : TextBox
    {
        //System.Web.UI.WebControls.TextBox;
        [Bindable(true)]
        [Category("Appearance")]
        [DefaultValue("")]
        [Localizable(true)]
        //public string Text
        //{
        //    get
        //    {
        //        String s = (String)ViewState["Text"];
        //        return ((s == null) ? String.Empty : s);
        //    }

        //    set
        //    {
        //        ViewState["Text"] = value;
        //    }
        //}
        private System.Web.UI.WebControls.ListBox m_lstShowChoice = null;
        private void lstBox_MouseUp(object sender, System.Windows.Forms.MouseEventArgs e)
        {
            ListBox box = (ListBox)sender;
            if ((box.SelectedIndex > -1) && !this.ReadOnly)
            {
                this.Text = box.SelectedItem.ToString();
                //选择后文本框失去了焦点，这里移回来
                this.Focus();
            }
        }

        protected override void RenderContents(HtmlTextWriter output)
        {
            output.Write(Text);
        }
    }
}
