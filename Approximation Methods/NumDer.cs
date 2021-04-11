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
    public partial class NumDer : Form
    {
        public NumDer()
        {
            InitializeComponent();
            this.dataGridView1.Rows.Add("Xi", "-0,2", "0,0", "0,2", "0,4", "0,6");
            this.dataGridView1.Rows.Add("f(Xi)", "1,7722", "1,5708", "1,3694", "1,1593", "0,9273");
        }

        private void NumDer_Load(object sender, EventArgs e)
        {

        }
        public List<object> numericalDer()
        {
            List<object> answer = new List<object>();

            List<double> x = new List<double>();
            List<double> y = new List<double>();

            for (int i = 0; i < dataGridView1.Rows.Count - 1; i++)
            {
                for (int j = 1; j < dataGridView1.Columns.Count; j++)
                {
                    if (i == 0)
                        x.Add(Convert.ToDouble(dataGridView1[j, i].Value));
                    else
                        y.Add(Convert.ToDouble(dataGridView1[j, i].Value));
                }
            }
            double xfix = Convert.ToDouble(textBox1.Text);
            answer.Add(der1(x, y, xfix));
            answer.Add(der2(x, y, xfix));
            
            return answer;
        }
        public int find_interval(List<double> x, double xfix)
        {
            int i = 0;
            for (i = 0; i < x.Count - 1; i++)
                if (x[i] <= xfix && xfix <= x[i + 1])
                    return i;
            return i;
        }

        public double der1(List<double> x, List<double> y, double xfix)
        {
            int i = find_interval(x, xfix);

            double tmp1 = (y[i + 1] - y[i]) / (x[i + 1] - x[i]);
            double tmp2 = ((y[i + 2] - y[i + 1]) / (x[i + 2] - x[i + 1]) - tmp1) /
                (x[i + 2] - x[i]) * (2 * xfix - x[i] - x[i + 1]);
            return tmp1 + tmp2;
        }
        //переделать для 5 точек
        /*public double der1(List<double> x, List<double> y, int index, double xfix) =>
            der(x, y, index) + (der(x, y, index + 1) -
            der(x, y, index)) * (2 * xfix - x[index] - x[index + 1]) / (x[index + 2] - x[index]);
*/
        public double der2(List<double> x, List<double> y, double xfix)
        {
            int i = find_interval(x, xfix);
            double tmp1 = (y[i + 2] - y[i + 1]) / (x[i + 2] - x[i + 1]);
            double tmp2 = (y[i + 1] - y[i]) / (x[i + 1] - x[i]);
            return 2 * (tmp1 - tmp2) / (x[i + 2] - x[i]);
        }
        private void button1_Click(object sender, EventArgs e)
        {

            for (int i = 2; i < 5; i++)
                this.Controls["label" + (i).ToString()].Text = "";
            List<object> answer = numericalDer();
            label2.Text = "First derivative = " + ((double)answer[0]).ToString();
            label4.Text = "Second derivative = " + ((double)answer[1]).ToString();


        }
    }
}
