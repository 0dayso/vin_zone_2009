namespace Represent
{
    partial class FrmCtripSniff
    {
        /// <summary>
        /// 必需的设计器变量。
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// 清理所有正在使用的资源。
        /// </summary>
        /// <param name="disposing">如果应释放托管资源，为 true；否则为 false。</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows 窗体设计器生成的代码

        /// <summary>
        /// 设计器支持所需的方法 - 不要
        /// 使用代码编辑器修改此方法的内容。
        /// </summary>
        private void InitializeComponent()
        {
            this.panel1 = new System.Windows.Forms.Panel();
            this.textBox1 = new System.Windows.Forms.TextBox();
            this.dateTimePicker1 = new System.Windows.Forms.DateTimePicker();
            this.txtEndAirport = new System.Windows.Forms.TextBox();
            this.txtStartAirport = new System.Windows.Forms.TextBox();
            this.button1 = new System.Windows.Forms.Button();
            this.panel2 = new System.Windows.Forms.Panel();
            this.dvgFlightResult = new System.Windows.Forms.DataGridView();
            this.panel1.SuspendLayout();
            this.panel2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dvgFlightResult)).BeginInit();
            this.SuspendLayout();
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.textBox1);
            this.panel1.Controls.Add(this.dateTimePicker1);
            this.panel1.Controls.Add(this.txtEndAirport);
            this.panel1.Controls.Add(this.txtStartAirport);
            this.panel1.Controls.Add(this.button1);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Left;
            this.panel1.Location = new System.Drawing.Point(0, 0);
            this.panel1.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(267, 540);
            this.panel1.TabIndex = 0;
            // 
            // textBox1
            // 
            this.textBox1.Location = new System.Drawing.Point(4, 295);
            this.textBox1.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.textBox1.Multiline = true;
            this.textBox1.Name = "textBox1";
            this.textBox1.Size = new System.Drawing.Size(253, 196);
            this.textBox1.TabIndex = 3;
            // 
            // dateTimePicker1
            // 
            this.dateTimePicker1.Location = new System.Drawing.Point(52, 144);
            this.dateTimePicker1.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.dateTimePicker1.Name = "dateTimePicker1";
            this.dateTimePicker1.Size = new System.Drawing.Size(156, 25);
            this.dateTimePicker1.TabIndex = 7;
            // 
            // txtEndAirport
            // 
            this.txtEndAirport.Location = new System.Drawing.Point(52, 98);
            this.txtEndAirport.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.txtEndAirport.Name = "txtEndAirport";
            this.txtEndAirport.Size = new System.Drawing.Size(132, 25);
            this.txtEndAirport.TabIndex = 6;
            this.txtEndAirport.Text = "WUH";
            // 
            // txtStartAirport
            // 
            this.txtStartAirport.Location = new System.Drawing.Point(52, 62);
            this.txtStartAirport.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.txtStartAirport.Name = "txtStartAirport";
            this.txtStartAirport.Size = new System.Drawing.Size(132, 25);
            this.txtStartAirport.TabIndex = 5;
            this.txtStartAirport.Text = "BJS";
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(65, 190);
            this.button1.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(100, 29);
            this.button1.TabIndex = 4;
            this.button1.Text = "查询";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // panel2
            // 
            this.panel2.Controls.Add(this.dvgFlightResult);
            this.panel2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel2.Location = new System.Drawing.Point(267, 0);
            this.panel2.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(966, 540);
            this.panel2.TabIndex = 1;
            // 
            // dvgFlightResult
            // 
            this.dvgFlightResult.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dvgFlightResult.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dvgFlightResult.Location = new System.Drawing.Point(0, 0);
            this.dvgFlightResult.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.dvgFlightResult.Name = "dvgFlightResult";
            this.dvgFlightResult.RowTemplate.Height = 23;
            this.dvgFlightResult.Size = new System.Drawing.Size(966, 540);
            this.dvgFlightResult.TabIndex = 2;
            // 
            // FrmCtripSniff
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1233, 540);
            this.Controls.Add(this.panel2);
            this.Controls.Add(this.panel1);
            this.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.Name = "FrmCtripSniff";
            this.Text = "FrmCtripSniff";
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dvgFlightResult)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.DateTimePicker dateTimePicker1;
        private System.Windows.Forms.TextBox txtEndAirport;
        private System.Windows.Forms.TextBox txtStartAirport;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.Panel panel2;
        private System.Windows.Forms.DataGridView dvgFlightResult;
        private System.Windows.Forms.TextBox textBox1;

    }
}