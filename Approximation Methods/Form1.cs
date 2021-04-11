using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Approximation_Methods
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void polynomials_Click(object sender, EventArgs e)
        {
            Polynomials pol = new Polynomials();
            
            pol.ShowDialog();
            //this.Close();


        }

        private void approxim_Click(object sender, EventArgs e)
        {
            Approximation app = new Approximation();
            app.ShowDialog();
        }

        private void Cubic_Click(object sender, EventArgs e)
        {
            CubicSpline cub = new CubicSpline();
            cub.ShowDialog();
        }

        private void der_Click(object sender, EventArgs e)
        {
            NumDer numDer = new NumDer();
            numDer.ShowDialog();
        }

        private void integral_Click(object sender, EventArgs e)
        {
            Integral integ = new Integral();
            integ.ShowDialog();
 
        }
    }
}
