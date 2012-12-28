using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using mshtml;

namespace Represent
{
    public partial class FrmMain : Form
    {
        public FrmMain()
        {
            InitializeComponent();
        }

        private void FrmMain_Load(object sender, EventArgs e)
        {
            txtPrefixe.Text = "http://127.0.0.1:8000/";
        }

        System.Threading.Thread thread = null;
        private void btnStart_Click(object sender, EventArgs e)
        {
            //if (thread == null)
            //{
            //    thread = new System.Threading.Thread(Action.HttplistenerHandler.Start);
            //    thread.Start();
            //}
            //Action.HttplistenerHandler.Start();
 
        }

        private void btnEnd_Click(object sender, EventArgs e)
        {
            //if (thread != null)
            //{
            //    thread.Abort();
            //    thread = null;
            //}
            //Action.HttplistenerHandler.Stop();
        }

        private void btnAdd_Click(object sender, EventArgs e)
        {
            listBox1.Items.Add(txtPrefixe.Text.Trim());
            //Action.HttplistenerHandler.prefixes.Add(txtPrefixe.Text.Trim());
            txtPrefixe.Text = null;
        }

        
    }

}