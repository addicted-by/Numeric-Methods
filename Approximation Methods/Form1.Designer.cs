
namespace Approximation_Methods
{
    partial class Form1
    {
        /// <summary>
        /// Обязательная переменная конструктора.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Освободить все используемые ресурсы.
        /// </summary>
        /// <param name="disposing">истинно, если управляемый ресурс должен быть удален; иначе ложно.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Код, автоматически созданный конструктором форм Windows

        /// <summary>
        /// Требуемый метод для поддержки конструктора — не изменяйте 
        /// содержимое этого метода с помощью редактора кода.
        /// </summary>
        private void InitializeComponent()
        {
            this.polynomials = new System.Windows.Forms.Button();
            this.Cubic = new System.Windows.Forms.Button();
            this.approxim = new System.Windows.Forms.Button();
            this.der = new System.Windows.Forms.Button();
            this.integral = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // polynomials
            // 
            this.polynomials.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.polynomials.BackColor = System.Drawing.Color.DimGray;
            this.polynomials.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.polynomials.Font = new System.Drawing.Font("Perpetua Titling MT", 7.8F);
            this.polynomials.Location = new System.Drawing.Point(114, 12);
            this.polynomials.Name = "polynomials";
            this.polynomials.Size = new System.Drawing.Size(256, 23);
            this.polynomials.TabIndex = 0;
            this.polynomials.Text = "Lab1. Lagrange and Newton Polynomials";
            this.polynomials.UseVisualStyleBackColor = false;
            this.polynomials.Click += new System.EventHandler(this.polynomials_Click);
            // 
            // Cubic
            // 
            this.Cubic.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.Cubic.BackColor = System.Drawing.Color.DimGray;
            this.Cubic.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.Cubic.Font = new System.Drawing.Font("Perpetua Titling MT", 7.8F);
            this.Cubic.Location = new System.Drawing.Point(114, 41);
            this.Cubic.Name = "Cubic";
            this.Cubic.Size = new System.Drawing.Size(256, 23);
            this.Cubic.TabIndex = 5;
            this.Cubic.Text = "Lab2. Cubic Spline";
            this.Cubic.UseVisualStyleBackColor = false;
            this.Cubic.Click += new System.EventHandler(this.Cubic_Click);
            // 
            // approxim
            // 
            this.approxim.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.approxim.BackColor = System.Drawing.Color.DimGray;
            this.approxim.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.approxim.Font = new System.Drawing.Font("Perpetua Titling MT", 7.8F);
            this.approxim.Location = new System.Drawing.Point(114, 70);
            this.approxim.Name = "approxim";
            this.approxim.Size = new System.Drawing.Size(256, 23);
            this.approxim.TabIndex = 6;
            this.approxim.Text = "Lab3. Approximation polynomials";
            this.approxim.UseVisualStyleBackColor = false;
            this.approxim.Click += new System.EventHandler(this.approxim_Click);
            // 
            // der
            // 
            this.der.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.der.BackColor = System.Drawing.Color.DimGray;
            this.der.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.der.Font = new System.Drawing.Font("Perpetua Titling MT", 7.8F);
            this.der.Location = new System.Drawing.Point(114, 99);
            this.der.Name = "der";
            this.der.Size = new System.Drawing.Size(256, 23);
            this.der.TabIndex = 7;
            this.der.Text = "Lab4. Numeric derivative";
            this.der.UseVisualStyleBackColor = false;
            this.der.Click += new System.EventHandler(this.der_Click);
            // 
            // integral
            // 
            this.integral.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.integral.BackColor = System.Drawing.Color.DimGray;
            this.integral.BackgroundImageLayout = System.Windows.Forms.ImageLayout.None;
            this.integral.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.integral.Font = new System.Drawing.Font("Perpetua Titling MT", 7.8F);
            this.integral.Location = new System.Drawing.Point(114, 128);
            this.integral.Name = "integral";
            this.integral.Size = new System.Drawing.Size(256, 23);
            this.integral.TabIndex = 8;
            this.integral.Text = "Lab5. Numeric Integral";
            this.integral.UseVisualStyleBackColor = false;
            this.integral.Click += new System.EventHandler(this.integral_Click);
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.AutoSize = true;
            this.BackColor = System.Drawing.Color.DarkGray;
            this.ClientSize = new System.Drawing.Size(498, 162);
            this.Controls.Add(this.integral);
            this.Controls.Add(this.der);
            this.Controls.Add(this.approxim);
            this.Controls.Add(this.Cubic);
            this.Controls.Add(this.polynomials);
            this.Name = "Form1";
            this.Text = "Approximation Methods";
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Button polynomials;
        private System.Windows.Forms.Button Cubic;
        private System.Windows.Forms.Button approxim;
        private System.Windows.Forms.Button der;
        private System.Windows.Forms.Button integral;
    }
}

