using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.Text.RegularExpressions;

namespace MailTo
{
    public partial class AddSender : Form
    {
        public AddSender()
        {
            InitializeComponent();
        }

        public string senderstring;
        private void button2_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.Cancel;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            Regex myreg = new Regex(@"^\w+([-+.]\w+)*@\w+([-.]\w+)*\.\w+([-.]\w+)*$"); //email验证
            if (myreg.IsMatch(senderTxt.Text) && senderTxt.Text.Trim() != "" && pwdTxt.Text.Trim() != "")
            {
                senderstring = t_stmp.Text.Trim() + "," + senderTxt.Text.Trim() + "," + pwdTxt.Text.Trim();
                this.DialogResult = DialogResult.OK;
            }
            else
            {
                t_stmp.Text = "";
                senderTxt.Text = "";
                pwdTxt.Text = "";
                msg.Text = "添加格式不正确，请重新输入！";
                this.DialogResult = DialogResult.None;
            }
        }
    }
}