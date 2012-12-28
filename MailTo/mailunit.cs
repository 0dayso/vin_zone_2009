using System;
using System.Collections.Generic;
using System.Text;
using System.Collections;
using System.Windows.Forms;
using System.Net.Mail;
using System.IO;
namespace MailTo
{
    class mailunit
    {
        public string smtp;
        public string from;
        public string pwd;
        public string to;
        public string subject;
        public string body;
        public ArrayList paths;
        public mailunit(string Psmtp, string Pfrom, string Ppwd, string Pto, string Psubject, string Pbody,ArrayList Ppaths)
        {
            smtp = Psmtp;
            from = Pfrom;
            pwd = Ppwd;
            to = Pto;
            subject = Psubject;
            body = Pbody;
            paths = Ppaths;
        }
        /*发邮件*/
        public bool SendMail()
        {
            //创建smtpclient对象
            System.Net.Mail.SmtpClient client = new SmtpClient();
            client.Host = smtp;
            client.UseDefaultCredentials = false;
            client.Credentials = new System.Net.NetworkCredential(from, pwd);
            client.DeliveryMethod = SmtpDeliveryMethod.Network;

            //创建mailMessage对象 
            System.Net.Mail.MailMessage message = new MailMessage(from, to);
            message.Subject = subject;
            //正文默认格式为html
            message.Body = body;
            message.IsBodyHtml = true;
            message.BodyEncoding = System.Text.Encoding.UTF8;

            //添加附件
            if (paths.Count != 0)
            {
                foreach (string path in paths)
                {
                    Attachment data = new Attachment(path, System.Net.Mime.MediaTypeNames.Application.Octet);
                    message.Attachments.Add(data);
                }
            }

            try
            {
                client.Send(message);
                MessageBox.Show("Email successfully sent.");
                return true;
            }
            catch (Exception ex)
            {
                MessageBox.Show("Send Email Failed." + ex.ToString());
                return false;
            }
        }

        /*发邮件：线程中使用*/
        public void SendMail2()
        {
            All.runing++;
            //创建smtpclient对象
            System.Net.Mail.SmtpClient client = new SmtpClient();

            client.Host = smtp;
            client.UseDefaultCredentials = false;
            client.Credentials = new System.Net.NetworkCredential(from, pwd);
            
            client.DeliveryMethod = SmtpDeliveryMethod.Network;
            

            //创建mailMessage对象 
            System.Net.Mail.MailMessage message = new MailMessage(from, to);
            message.Subject = subject;
            //正文默认格式为html
            message.Body = body;
            message.IsBodyHtml = true;
            
            message.BodyEncoding = System.Text.Encoding.UTF8;
            

            //添加附件
            if (paths.Count != 0)
            {
                foreach (string path in paths)
                {
                    Attachment data = new Attachment(path, System.Net.Mime.MediaTypeNames.Application.Octet);
                    message.Attachments.Add(data);
                }
            }

            try
            {
                client.Send(message);

             
                 
                All.success++;
                All.runing--;
            }
            catch (Exception ex)
            {
                File.Create(Application.StartupPath + "\\Error.txt");

                StreamWriter sw = new StreamWriter(Application.StartupPath + "\\Error.txt");
                sw.WriteLine(ex.Message+ex.Source);


                All.fail++;
                All.runing--;
            }
        }
    }
}
