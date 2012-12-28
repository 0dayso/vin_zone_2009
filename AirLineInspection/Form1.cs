using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.Threading;
using BLL.Ctrip;
using Models;
using System.Configuration;
using BLL.Common.Operation;
using BLL.Common.Interface;

namespace AirLineInspection
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        public Thread thread;

        #region 执行线程
        /// <summary>
        /// 执行线程
        /// </summary>
        private void ExcuteThreading()
        {
            thread = new Thread(AirLineHandle.HandleAirLine);
            thread.SetApartmentState(ApartmentState.STA);

            thread.Start();

        }
        #endregion

        private void Form1_Load(object sender, EventArgs e)
        {
            ExcuteThreading();
        }

        private void Form1_FormClosed(object sender, FormClosedEventArgs e)
        {
            if (thread != null)
            {
                thread.Abort();
            }
        }

    }
}