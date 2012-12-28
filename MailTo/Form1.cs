using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.Collections;
using System.Net.Mail;
using System.Text.RegularExpressions;
using System.IO;
using System.Threading;
using System.Xml;

namespace MailTo
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }
        public ArrayList paths = new ArrayList();//保存附件地址


        /*单发*/
        private void button2_Click(object sender, EventArgs e)
        {
            //if (!check())
            //    return;
            //if (t_to.SelectedIndex != -1)
            //{
            //    mailunit mu = new mailunit(t_stmp.Text, t_from.Text, t_pwd.Text, t_to.Text, t_subject.Text, t_body.Text,paths);
            //    mu.SendMail();
            //}
            //else
            //    MessageBox.Show("请先选中一个收件人！");
        }

        /*群发*/
        private void button1_Click(object sender, EventArgs e)
        {
            // yin cang 
            this.Hide();

            if (!check())
                return;
            //初始化
            toolStripProgressBar1.Visible = true;
            toolStripStatusLabel3.Text = "";
            toolStripProgressBar1.Maximum = t_to.Items.Count;

            ArrayList alist = new ArrayList();

            for (int i = 0; i < t_to.Items.Count; i++)
            {
                string mail;
                if (i < senderLst.Items.Count)
                {
                    mail = t_to.Items[i].ToString() + "," + senderLst.Items[i].ToString();

                }
                else
                {
                    mail = t_to.Items[i].ToString() + "," + senderLst.Items[i % senderLst.Items.Count].ToString();
                }
                alist.Add(mail);

            }





            int sendnum = 15;// 每次发送15封邮件
            // this.Invoke

            for (int j = 0; j < alist.Count; j++)
            {
                string senderStr = alist[j].ToString();
                string[] str = senderStr.Split(new char[] { ',', ',', ',' });
                string stmpStr = str[1];
                string fromStr = str[2];
                string pwdStr = str[3];
                string toMail = str[0];

                mailunit mu = new mailunit(stmpStr, fromStr, pwdStr, toMail, t_subject.Text, t_body.BodyInnerHTML.ToString(), paths);
                Thread mythread = new Thread(new ThreadStart(mu.SendMail2));
                if (sendnum == 0)
                {
                    Thread.Sleep(1000 * 40);
                    sendnum = 15;
                }
                mythread.Start();
                mythread.Join();

                sendnum--;

                string info = "【结果】发送成功：" + All.success.ToString() + "条  发送失败：" + All.fail.ToString() + "条";
                this.niMessage.ShowBalloonTip(1500, "消息", info, ToolTipIcon.Info);

            }


            //while (All.runing != 0)
            //{
            //    toolStripProgressBar1.Value = t_to.Items.Count - All.runing;
            //    Application.DoEvents();
            //    toolStripStatusLabel3.Text = "发送成功：" + All.success.ToString() + "条  发送失败：" + All.fail.ToString() + "条";
            //}

            // toolStripProgressBar1.Visible = false;
            //All.success = 0;
            //All.fail = 0;
            //MessageBox.Show("【结果】发送成功：" + All.success.ToString() + "条  发送失败：" + All.fail.ToString() + "条");
        }

        /*检查输入参数*/
        public bool check()
        {
            string err = "注意：\n\n";
            int errCount = 0;
            //if (t_stmp.Text.Trim().Length == 0)
            //{
            //    err += "Smtp服务器地址不能为空！\r\n";
            //    errCount++;
            //}
            //if (t_from.Text.Trim().Length == 0)
            //{
            //    err += "用户名不能为空！\r\n";
            //    errCount++;
            //}
            //if (t_pwd.Text.Trim().Length == 0)
            //{
            //    err += "密码不能为空！\r\n";
            //    errCount++;
            //}
            if (t_subject.Text.Trim().Length == 0)
            {
                err += "标题不能为空！\r\n";
                errCount++;
            }
            if (t_to.Items.Count == 0)
            {
                err += "收件人不能为空！\r\n";
                errCount++;
            }
            if (t_body.BodyInnerHTML.ToString().Length == 0)
            {
                err += "正文不能为空！\r\n";
                errCount++;
            }
            Regex myreg = new Regex(@"^\w+([-+.]\w+)*@\w+([-.]\w+)*\.\w+([-.]\w+)*$"); //email验证
            //if (!myreg.IsMatch(t_from.Text))
            //{
            //    err += "用户名格式不正确！\r\n";
            //    errCount++;
            //}
            //看还有没有其他需要验证的
            if (errCount == 0)
                return true;
            else
            {
                MessageBox.Show(err, "提示");
                return false;
            }
        }

        /*添加收件人*/
        private void button3_Click(object sender, EventArgs e)
        {
          


         
            // 打开选择路径
            //FileStream fs = new FileStream(openFileDialog1.FileName, FileMode.Open, FileAccess.Read);
            //  FileStream fs = new FileStream(Application.StartupPath + "\\address.txt", FileMode.Open, FileAccess.Read);
            
            string oneEmail;
            if (File.Exists(Application.StartupPath + "\\address.txt"))
            {  
                #region  导入默认位置的邮件地址
                StreamReader sr = new StreamReader(Application.StartupPath + "\\address.txt");
                //  文件存在则导入
                Regex myreg = new Regex(@"^\w+([-+.]\w+)*@\w+([-.]\w+)*\.\w+([-.]\w+)*$"); //email验证           
                while (sr.Peek() != -1)
                {
                    oneEmail = sr.ReadLine();
                    if (myreg.IsMatch(oneEmail))
                        t_to.Items.Add(oneEmail);
                }   
                #endregion
            }
            else 
            {
                #region   单个地址添加
                add ad = new add();
                if (ad.ShowDialog() == DialogResult.OK && !t_to.Items.Contains(ad.email))
                    t_to.Items.Add(ad.email);
                //DataTable dt = new DataTable("email");
                //dt.Columns.Add("address");
                //dt.Rows.Add(ad.email);




                //dt.WriteXml(Application.StartupPath + "\\address.xml",XmlWriteMode.);
                //XmlDocument doc = new XmlDocument();
                //doc.Load("a.xml ");
                //doc.DocumentElement.AppendChild(doc.ImportNode(doc.DocumentElement.FirstChild, true));
                //XmlTextWriter xw = new XmlTextWriter(new StreamWriter("a.xml "));
                //xw.Formatting = Formatting.Indented;
                //doc.WriteTo(xw);
                //xw.Close();
                #endregion
            }



         


        }
        /*删除收件人*/
        private void button4_Click(object sender, EventArgs e)
        {
            if (t_to.SelectedIndex != -1)
                t_to.Items.RemoveAt(t_to.SelectedIndex);
        }
        /*清空附件*/
        private void linkLabel2_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            t_files.Text = string.Empty;
            paths.Clear();
        }
        /*添加附件*/
        private void linkLabel1_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            openFileDialog1.Filter = "所有文件(*.*)|*.*";
            if (openFileDialog1.ShowDialog() == DialogResult.OK)
            {
                string[] files = openFileDialog1.FileNames;
                foreach (string file in files)
                {
                    paths.Add(file);
                    t_files.Text += Path.GetFileName(file) + "  ";
                }
            }
        }
        /*从文件导入*/
        private void button5_Click(object sender, EventArgs e)
        {
            openFileDialog1.Filter = "文本文件(*.txt)|*.txt";
            if (openFileDialog1.ShowDialog() == DialogResult.OK)
            {
                FileStream fs = new FileStream(openFileDialog1.FileName, FileMode.Open, FileAccess.Read);
                StreamReader sr = new StreamReader(fs);
                string oneEmail;
                Regex myreg = new Regex(@"^\w+([-+.]\w+)*@\w+([-.]\w+)*\.\w+([-.]\w+)*$"); //email验证           
                while (sr.Peek() != -1)
                {
                    oneEmail = sr.ReadLine();
                    if (myreg.IsMatch(oneEmail))
                        t_to.Items.Add(oneEmail);
                }
            }
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            toolStripProgressBar1.Visible = false;


        }
        /*帮助*/
        private void linkLabel3_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            help hel = new help();
            hel.Show();
        }
        //添加QQ邮箱功能
        private void button6_Click(object sender, EventArgs e)
        {
            QQ q = new QQ();
            if (q.ShowDialog() == DialogResult.OK && q.dic.Count != 0)
            {
                for (int i = 0; i < q.dic.Count; i++)
                {

                    string e_mail = q.dic[int.Parse(q.qq) + i];
                    if (!t_to.Items.Contains(e_mail))
                    {
                        t_to.Items.Add(e_mail);
                    }
                }
            }
        }

        private void button7_Click(object sender, EventArgs e)
        {
            AddSender addsender = new AddSender();
            if (addsender.ShowDialog() == DialogResult.OK && !senderLst.Items.Contains(addsender.senderstring))
            {
                senderLst.Items.Add(addsender.senderstring);
            }

        }


        private void niMessage_Click(object sender, EventArgs e)
        {
            this.Show();
        }

    }
}