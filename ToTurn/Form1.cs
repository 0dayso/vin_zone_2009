using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace ToTurn
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }
        
////十进制转二进制
//Console.WriteLine("十进制166的二进制表示: "+Convert.ToString(166, 2));
////十进制转八进制
//Console.WriteLine("十进制166的八进制表示: "+Convert.ToString(166, 8));
////十进制转十六进制
//Console.WriteLine("十进制166的十六进制表示: "+Convert.ToString(166, 16));
   　
////二进制转十进制
//Console.WriteLine("二进制 111101 的十进制表示: "+Convert.ToInt32("111101", 2));
////八进制转十进制
//Console.WriteLine("八进制 44 的十进制表示: "+Convert.ToInt32("44", 8));
////十六进制转十进制
//Console.WriteLine("十六进制 CC的十进制表示: "+Convert.ToInt32("CC", 16));

        // 十进制转二进制
        private void button1_Click(object sender, EventArgs e)
        {
            this.txt2.Text = Convert.ToString(Convert.ToInt32(this.txt1.Text.Trim()),2);
        }

        // 十进制转8进制
        private void button2_Click(object sender, EventArgs e)
        {
            this.txt2.Text = Convert.ToString(Convert.ToInt32(this.txt1.Text.Trim()), 8);
        }


        // 十进制转16进制
        private void button3_Click(object sender, EventArgs e)
        {
            this.txt2.Text = Convert.ToString(Convert.ToInt32(this.txt1.Text.Trim()), 16);
        }


        // 2进制转10进制
        private void button4_Click(object sender, EventArgs e)
        {
            this.txt2.Text = Convert.ToString(long.Parse(this.txt1.Text.Trim()), 2);
            
        }
    }
}
