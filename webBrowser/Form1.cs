using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;


using System.Threading;

namespace webBrowser
{


    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        public enum eProcess
        {
            登录搜索 = 1,//一个页面
            点击链接 = 2,
            点击完成 = 3
        }
        public enum eKey
        {
            新中大 = 1,//一个页面
            新中大公司 = 2,
            新中大银色快车 = 3,
            新大中=4,
            北京新中大服务=5,
            新中大服务=6,
            新中大技术 = 7,
            新中大维修=8

        }


        eProcess process;

        private void Form1_Load(object sender, EventArgs e)
        {

        }

        #region 打开百度填入搜索内容 进行搜素
        /// <summary>
        /// 打开百度填入搜索内容 进行搜素
        /// </summary>
        void search()
        {
            HtmlElement aa = this.webBrowser1.Document.GetElementById("kw");
            string []agr=new string [5];//{};
            Random r = new Random();
            int i= r.Next(1, 8);
            aa.InnerText = ((eKey)System.Enum.Parse(typeof(eKey), i.ToString())).ToString();
            HtmlElement su = this.webBrowser1.Document.GetElementById("su");
            process = eProcess.点击链接;
            su.InvokeMember("Click");

        }
        #endregion

        #region 点击指定链接
        /// <summary>
        /// 点击指定链接
        /// </summary>
        void afterClick()
        {
            HtmlElement aw2 = this.webBrowser1.Document.GetElementById("aw2");
            if(aw2==null)
            {
             aw2 = this.webBrowser1.Document.GetElementById("dfs0");
            }                  
            aw2.InvokeMember("Click");
            process = eProcess.点击完成;      
        }
        #endregion

        private void button1_Click(object sender, EventArgs e)
        {
            this.webBrowser1.Url = new Uri("http://www.baidu.com");
            process = eProcess.登录搜索;
        }

        private void webBrowser1_DocumentCompleted(object sender, WebBrowserDocumentCompletedEventArgs e)
        {

            SetAllWebItemSelf();
            switch (process)
            {
                case eProcess.登录搜索:
                    search();
                    break;
                case eProcess.点击链接:
                    afterClick();
                    break;
                case eProcess.点击完成:
                    Thread.Sleep(3000);//点击完成后停止3秒
                    this.webBrowser1.Url = new Uri("http://www.baidu.com");
                    process = eProcess.登录搜索;
                    break;
            }

        }

        private void webBrowser1_NewWindow(object sender, CancelEventArgs e)
        {

        }
        #region 设置当前窗口打开
        private void SetAllWebItemSelf()
        {
            try
            {
                foreach (HtmlElement item in this.webBrowser1.Document.All)
                {
                    if (item.TagName.ToLower().Equals("iframe", StringComparison.OrdinalIgnoreCase) == false)
                    {
                        try
                        {
                            item.SetAttribute("target", "_self");
                        }
                        catch { }
                    }

                    if (item.TagName.ToLower() == "a")
                    {
                        try
                        {
                            item.SetAttribute("target", "_self");// 设置 当前窗口打开
                        }
                        catch { }
                    }

                }
            }
            catch { }
        }
        #endregion
    }
}