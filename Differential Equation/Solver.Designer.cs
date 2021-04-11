
namespace Differential_Equation
{
    partial class Solver
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            System.Windows.Forms.DataVisualization.Charting.ChartArea chartArea5 = new System.Windows.Forms.DataVisualization.Charting.ChartArea();
            System.Windows.Forms.DataVisualization.Charting.Legend legend5 = new System.Windows.Forms.DataVisualization.Charting.Legend();
            System.Windows.Forms.DataVisualization.Charting.Series series5 = new System.Windows.Forms.DataVisualization.Charting.Series();
            this.chart1 = new System.Windows.Forms.DataVisualization.Charting.Chart();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            this.h_data = new System.Windows.Forms.TextBox();
            this.textBox2 = new System.Windows.Forms.TextBox();
            this.textBox3 = new System.Windows.Forms.TextBox();
            this.panel1 = new System.Windows.Forms.Panel();
            this.eu = new System.Windows.Forms.CheckBox();
            this.rk = new System.Windows.Forms.CheckBox();
            this.ad = new System.Windows.Forms.CheckBox();
            this.fun = new System.Windows.Forms.CheckBox();
            ((System.ComponentModel.ISupportInitialize)(this.chart1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            this.panel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // chart1
            // 
            this.chart1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            chartArea5.Name = "ChartArea1";
            this.chart1.ChartAreas.Add(chartArea5);
            legend5.Name = "Legend1";
            this.chart1.Legends.Add(legend5);
            this.chart1.Location = new System.Drawing.Point(3, 0);
            this.chart1.Name = "chart1";
            series5.ChartArea = "ChartArea1";
            series5.Legend = "Legend1";
            series5.Name = "Series1";
            this.chart1.Series.Add(series5);
            this.chart1.Size = new System.Drawing.Size(795, 300);
            this.chart1.TabIndex = 0;
            this.chart1.Text = "chart1";
            // 
            // label1
            // 
            this.label1.Anchor = System.Windows.Forms.AnchorStyles.Bottom;
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(12, 356);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(32, 17);
            this.label1.TabIndex = 1;
            this.label1.Text = "h = ";
            // 
            // label2
            // 
            this.label2.Anchor = System.Windows.Forms.AnchorStyles.Bottom;
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(116, 344);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(49, 17);
            this.label2.TabIndex = 2;
            this.label2.Text = "y(0) = ";
            // 
            // label3
            // 
            this.label3.Anchor = System.Windows.Forms.AnchorStyles.Bottom;
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(116, 371);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(52, 17);
            this.label3.TabIndex = 3;
            this.label3.Text = "y\'(0) = ";
            // 
            // pictureBox1
            // 
            this.pictureBox1.Anchor = System.Windows.Forms.AnchorStyles.Bottom;
            this.pictureBox1.Image = global::Differential_Equation.Properties.Resources.CodeCogsEqn__2_;
            this.pictureBox1.Location = new System.Drawing.Point(15, 306);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new System.Drawing.Size(218, 33);
            this.pictureBox1.TabIndex = 4;
            this.pictureBox1.TabStop = false;
            // 
            // h_data
            // 
            this.h_data.Anchor = System.Windows.Forms.AnchorStyles.Bottom;
            this.h_data.Location = new System.Drawing.Point(44, 354);
            this.h_data.Name = "h_data";
            this.h_data.Size = new System.Drawing.Size(32, 22);
            this.h_data.TabIndex = 5;
            this.h_data.Text = "0,1";
            // 
            // textBox2
            // 
            this.textBox2.Anchor = System.Windows.Forms.AnchorStyles.Bottom;
            this.textBox2.Location = new System.Drawing.Point(166, 342);
            this.textBox2.Name = "textBox2";
            this.textBox2.Size = new System.Drawing.Size(26, 22);
            this.textBox2.TabIndex = 6;
            this.textBox2.Text = "1";
            // 
            // textBox3
            // 
            this.textBox3.Anchor = System.Windows.Forms.AnchorStyles.Bottom;
            this.textBox3.Location = new System.Drawing.Point(166, 365);
            this.textBox3.Name = "textBox3";
            this.textBox3.Size = new System.Drawing.Size(26, 22);
            this.textBox3.TabIndex = 7;
            this.textBox3.Text = "1";
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.fun);
            this.panel1.Controls.Add(this.ad);
            this.panel1.Controls.Add(this.rk);
            this.panel1.Controls.Add(this.eu);
            this.panel1.Location = new System.Drawing.Point(460, 307);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(338, 81);
            this.panel1.TabIndex = 8;
            // 
            // eu
            // 
            this.eu.AutoSize = true;
            this.eu.Location = new System.Drawing.Point(13, 10);
            this.eu.Name = "eu";
            this.eu.Size = new System.Drawing.Size(63, 21);
            this.eu.TabIndex = 0;
            this.eu.Text = "Euler";
            this.eu.UseVisualStyleBackColor = true;
            this.eu.CheckedChanged += new System.EventHandler(this.eu_CheckedChanged);
            // 
            // rk
            // 
            this.rk.AutoSize = true;
            this.rk.Location = new System.Drawing.Point(221, 10);
            this.rk.Name = "rk";
            this.rk.Size = new System.Drawing.Size(110, 21);
            this.rk.TabIndex = 1;
            this.rk.Text = "Runge-Kutta";
            this.rk.UseVisualStyleBackColor = true;
            this.rk.CheckedChanged += new System.EventHandler(this.rk_CheckedChanged);
            // 
            // ad
            // 
            this.ad.AutoSize = true;
            this.ad.Location = new System.Drawing.Point(13, 35);
            this.ad.Name = "ad";
            this.ad.Size = new System.Drawing.Size(73, 21);
            this.ad.TabIndex = 2;
            this.ad.Text = "Adams";
            this.ad.UseVisualStyleBackColor = true;
            this.ad.CheckedChanged += new System.EventHandler(this.ad_CheckedChanged);
            // 
            // fun
            // 
            this.fun.AutoSize = true;
            this.fun.Checked = true;
            this.fun.CheckState = System.Windows.Forms.CheckState.Checked;
            this.fun.Location = new System.Drawing.Point(221, 36);
            this.fun.Name = "fun";
            this.fun.Size = new System.Drawing.Size(84, 21);
            this.fun.TabIndex = 3;
            this.fun.Text = "Function";
            this.fun.UseVisualStyleBackColor = true;
            this.fun.CheckedChanged += new System.EventHandler(this.checkBox4_CheckedChanged);
            // 
            // Solver
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(800, 398);
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.textBox3);
            this.Controls.Add(this.textBox2);
            this.Controls.Add(this.h_data);
            this.Controls.Add(this.pictureBox1);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.chart1);
            this.Name = "Solver";
            this.Text = "Solver";
            this.Load += new System.EventHandler(this.Solver_Load);
            ((System.ComponentModel.ISupportInitialize)(this.chart1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.DataVisualization.Charting.Chart chart1;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.PictureBox pictureBox1;
        private System.Windows.Forms.TextBox h_data;
        private System.Windows.Forms.TextBox textBox2;
        private System.Windows.Forms.TextBox textBox3;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.CheckBox fun;
        private System.Windows.Forms.CheckBox ad;
        private System.Windows.Forms.CheckBox rk;
        private System.Windows.Forms.CheckBox eu;
    }
}