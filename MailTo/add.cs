using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.Text.RegularExpressions;
////该源码下载自win.51aspx.com(５1ａｓｐｘ．ｃｏｍ)

namespace MailTo
{
    public partial class add : Form
    {
        public add()
        {
            InitializeComponent();
        }
        public string email;
        private void button1_Click(object sender, EventArgs e)
        {
           Regex myreg=new Regex(@"^\w+([-+.]\w+)*@\w+([-.]\w+)*\.\w+([-.]\w+)*$"); //email验证
           if (myreg.IsMatch(textBox1.Text))
           {
               email = textBox1.Text;
               this.DialogResult = DialogResult.OK;
           }
           else
           {
               textBox1.Text = "";
               msg.Text = "邮箱格式不正确，请重新输入！";
               this.DialogResult = DialogResult.None;
           }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.Cancel;
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {
            msg.Text = "";
        }

        private void textBox1_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                Regex myreg = new Regex(@"^\w+([-+.]\w+)*@\w+([-.]\w+)*\.\w+([-.]\w+)*$"); //email验证
                if (myreg.IsMatch(textBox1.Text))
                {
                    email = textBox1.Text;
                    this.DialogResult = DialogResult.OK;
                }
                else
                {
                    textBox1.Text = "";
                    msg.Text = "邮箱格式不正确，请重新输入！";
                    this.DialogResult = DialogResult.None;
                }
            }
        }
    }
}