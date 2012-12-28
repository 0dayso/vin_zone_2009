using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.Collections;


namespace MailTo
{
    public partial class QQ : Form
    {
        public QQ()
        {
            InitializeComponent();
        }

        public Dictionary<int, string> dic = new Dictionary<int, string>();
        public string qq;
        public int num;
        private void button1_Click(object sender, EventArgs e)
        {
            string qqmail = "@qq.com";
            qq = QQTxt.Text.ToString();
            num = int.Parse(NumTxt.Text.ToString());
            for (int i = 0; i < num + 1; i++)
            {
                int QQint = int.Parse(qq)+i;
                string mail=QQint.ToString()+qqmail;
                dic.Add(QQint, mail);
            }
            this.DialogResult = DialogResult.OK;

        }
    }
}