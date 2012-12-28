using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using CRM.Buzlogic.Common;

namespace DSCRM
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }
        [STAThread]
        static void Main()
        {
            Application.Run(new Form1());
        }

        private void button1_Click(object sender, EventArgs e)
        {
            CRM.Buzlogic.Common.DSCRM.HOTELSTATEDataTable  dt = new CRM.Buzlogic.Common.DSCRM.HOTELSTATEDataTable();
            DataRow dr = dt.NewRow();
            dr["HOTELSTATEID"] = "6";
            dr["HOTELSTATE"] = "cc";
            dr["OrderBy"] = "1";
            dt.Rows.Add(dr);
            
            DataRow dr1 = dt.NewRow();
            dr1["HOTELSTATEID"] = "7";
            dr1["HOTELSTATE"] = "dd";
            dr1["OrderBy"] = "2";
            dt.Rows.Add(dr1);
            DatatableUpdate.DataTableUpdate(dt);
        }

    }
}