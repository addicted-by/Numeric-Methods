
namespace Approximation_Methods
{
    partial class Integral
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
            this.label1 = new System.Windows.Forms.Label();
            this.x0 = new System.Windows.Forms.TextBox();
            this.xk = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.h1 = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.h2 = new System.Windows.Forms.TextBox();
            this.label4 = new System.Windows.Forms.Label();
            this.Console = new System.Windows.Forms.RichTextBox();
            this.pictureBox2 = new System.Windows.Forms.PictureBox();
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox2)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Dock = System.Windows.Forms.DockStyle.Top;
            this.label1.Location = new System.Drawing.Point(88, 0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(41, 17);
            this.label1.TabIndex = 2;
            this.label1.Text = "X0 = ";
            // 
            // x0
            // 
            this.x0.Dock = System.Windows.Forms.DockStyle.Top;
            this.x0.Location = new System.Drawing.Point(88, 17);
            this.x0.Name = "x0";
            this.x0.Size = new System.Drawing.Size(269, 22);
            this.x0.TabIndex = 3;
            this.x0.Text = "-2";
            // 
            // xk
            // 
            this.xk.Dock = System.Windows.Forms.DockStyle.Top;
            this.xk.Location = new System.Drawing.Point(88, 56);
            this.xk.Name = "xk";
            this.xk.Size = new System.Drawing.Size(269, 22);
            this.xk.TabIndex = 5;
            this.xk.Text = "2";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Dock = System.Windows.Forms.DockStyle.Top;
            this.label2.Location = new System.Drawing.Point(88, 39);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(40, 17);
            this.label2.TabIndex = 4;
            this.label2.Text = "Xk = ";
            // 
            // h1
            // 
            this.h1.Dock = System.Windows.Forms.DockStyle.Top;
            this.h1.Location = new System.Drawing.Point(88, 95);
            this.h1.Name = "h1";
            this.h1.Size = new System.Drawing.Size(269, 22);
            this.h1.TabIndex = 7;
            this.h1.Text = "1,0";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Dock = System.Windows.Forms.DockStyle.Top;
            this.label3.Location = new System.Drawing.Point(88, 78);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(40, 17);
            this.label3.TabIndex = 6;
            this.label3.Text = "h1 = ";
            // 
            // h2
            // 
            this.h2.Dock = System.Windows.Forms.DockStyle.Top;
            this.h2.Location = new System.Drawing.Point(88, 134);
            this.h2.Name = "h2";
            this.h2.Size = new System.Drawing.Size(269, 22);
            this.h2.TabIndex = 9;
            this.h2.Text = "0,5";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Dock = System.Windows.Forms.DockStyle.Top;
            this.label4.Location = new System.Drawing.Point(88, 117);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(44, 17);
            this.label4.TabIndex = 8;
            this.label4.Text = "h2 =  ";
            // 
            // Console
            // 
            this.Console.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.Console.BackColor = System.Drawing.SystemColors.InactiveCaptionText;
            this.Console.ForeColor = System.Drawing.Color.LawnGreen;
            this.Console.Location = new System.Drawing.Point(20, 207);
            this.Console.Name = "Console";
            this.Console.Size = new System.Drawing.Size(528, 231);
            this.Console.TabIndex = 10;
            this.Console.Text = "";
            // 
            // pictureBox2
            // 
            this.pictureBox2.BackColor = System.Drawing.Color.White;
            this.pictureBox2.BackgroundImageLayout = System.Windows.Forms.ImageLayout.None;
            this.pictureBox2.Dock = System.Windows.Forms.DockStyle.Left;
            this.pictureBox2.Image = global::Approximation_Methods.Properties.Resources.CodeCogsEqn;
            this.pictureBox2.Location = new System.Drawing.Point(0, 0);
            this.pictureBox2.Name = "pictureBox2";
            this.pictureBox2.Size = new System.Drawing.Size(88, 450);
            this.pictureBox2.SizeMode = System.Windows.Forms.PictureBoxSizeMode.AutoSize;
            this.pictureBox2.TabIndex = 1;
            this.pictureBox2.TabStop = false;
            // 
            // pictureBox1
            // 
            this.pictureBox1.BackColor = System.Drawing.Color.White;
            this.pictureBox1.Dock = System.Windows.Forms.DockStyle.Right;
            this.pictureBox1.Image = global::Approximation_Methods.Properties.Resources.CodeCogsEqn__1_;
            this.pictureBox1.Location = new System.Drawing.Point(357, 0);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new System.Drawing.Size(212, 450);
            this.pictureBox1.TabIndex = 0;
            this.pictureBox1.TabStop = false;
            // 
            // Integral
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.White;
            this.ClientSize = new System.Drawing.Size(569, 450);
            this.Controls.Add(this.Console);
            this.Controls.Add(this.h2);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.h1);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.xk);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.x0);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.pictureBox2);
            this.Controls.Add(this.pictureBox1);
            this.Name = "Integral";
            this.Text = "Integral";
            this.Load += new System.EventHandler(this.Integral_Load);
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox2)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.PictureBox pictureBox1;
        private System.Windows.Forms.PictureBox pictureBox2;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox x0;
        private System.Windows.Forms.TextBox xk;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox h1;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.TextBox h2;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.RichTextBox Console;
    }
}