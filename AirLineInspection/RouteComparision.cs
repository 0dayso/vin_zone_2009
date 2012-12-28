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
    public partial class RouteComparision : Form
    {
        public RouteComparision()
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

        private void btnQuery_Click(object sender, EventArgs e)
        {
            AirLineHandle handle = new AirLineHandle();

            if (string.IsNullOrEmpty(txtDeparture.Text))
                return;

            if (string.IsNullOrEmpty(txtArrival.Text))
                return;

            if (string.IsNullOrEmpty(txtDepartureTime.Text))
                return;

            DateTime? departureTime = Convert.ToDateTime(txtDepartureTime.Text);
            

            txtResult.Text = handle.RouteTest(txtDeparture.Text, txtArrival.Text, departureTime, txtFlightNO.Text);
        }



        #region HttpListener
        HttpListenerOperation operation = new HttpListenerOperation();

        private void btnAdd_Click(object sender, EventArgs e)
        {
            listPrefixs.Items.Add(txtPrefix.Text);
        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            listPrefixs.Items.Remove(listPrefixs.SelectedItem);
        }

        private void btnStart_Click(object sender, EventArgs e)
        {
            
            IList<string> prefixList = new List<string>();

            foreach(object objvalue in listPrefixs.Items)
            {
                prefixList.Add(objvalue.ToString());
            }

            if (operation != null)
            {
                operation.PrefixList = prefixList;

                operation.Start();
            }
        }

        private void btnStop_Click(object sender, EventArgs e)
        {
            if (operation != null)
                operation.Stop();
        }
        #endregion

    }
}