using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Differential_Equation
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            Solver solv = new Solver();
            solv.ShowDialog();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            Boundary boundary = new Boundary();
            boundary.ShowDialog();
        }
    }
}
