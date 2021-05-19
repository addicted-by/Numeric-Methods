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
        public List<object> numericalDerCheck()
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
            var coefs = newtonCoeffs(x, y);

            answer.Add(der1_right(coefs[1], coefs[2], coefs[3], xfix));
            answer.Add(der2_right(coefs[2], coefs[3], xfix));

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
        public double f(List<double> x, List<double> y)
        {
            if (x.Count == 2)
                return (y[0] - y[1]) / (x[0] - x[1]);
            List<double> tmp1 = new List<double>();
            List<double> tmp2 = new List<double>();
            foreach (var f in x)
            {
                tmp1.Add(f);
                tmp2.Add(f);
            }
            
            tmp1.RemoveAt(tmp1.Count - 1);
            tmp2.RemoveAt(0);
            List<double> tmp1f = new List<double>();
            List<double> tmp2f = new List<double>();
            foreach (var f in y)
            {
                tmp1f.Add(f);
                tmp2f.Add(f);
            }
            tmp1f.RemoveAt(tmp1f.Count - 1);
            tmp2f.RemoveAt(0);
            return (f(tmp1, tmp1f) - f(tmp2, tmp2f)) / (x[0] - x[x.Count - 1]);
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

        public double der1_right(List<double> b, List<double> c, List<double> d, double x)
        {
            return b[0] + c[0] * (2 * x - c[1] - c[2]) + d[0] * (3 * x * x - 2 * x * (d[1] + d[2]) + d[1] * d[2] - 2 * x * d[3] + d[3] * (d[1] + d[2]));
        }
        public double der2_right(List<double> c, List<double> d, double x)
        {
            return 2 * c[0] + d[0] * (6 * x - 2 * (d[1] + d[2]) - 2 * d[3]);
        }
        public List<List<double>> newtonCoeffs(List<double> x, List<double> y)
        {
            List<List<double>> ans = new List<List<double>>();
            ans.Add(new List<double>());
            ans[ans.Count - 1].Add(y[0]);
            var p = y[0];
            List<double> tmpx = new List<double>();
            double tmp, tmpf;
            for (int i = 0; i < x.Count -1; ++i)
            {
                ans.Add(new List<double>());
                tmp = 1;
                tmpx = new List<double>();
                for (int j = 0; j <= i; j++)
                {
                    ans[ans.Count - 1].Add(x[j]);
                    tmpx.Add(x[j]);
                }
                tmpx.Add(x[i + 1]);
                tmpf = f(tmpx, y);
                p += tmp * tmpf;
                ans[ans.Count - 1].Insert(0,tmpf);
            }
            
            return ans;
        }
        private void button1_Click(object sender, EventArgs e)
        {

            for (int i = 2; i < 5; i++)
                this.Controls["label" + (i).ToString()].Text = "";
            List<object> answer = numericalDerCheck();
            List<object> answer1 = numericalDer();
            label2.Text = "First derivative(Check) = " + ((double)answer[0]).ToString();
            label4.Text = "Second derivative(Check) = " + ((double)answer[1]).ToString();
            label6.Text = "First derivative(Newton) = " + ((double)answer1[0]).ToString();
            label7.Text = "Second derivative(Newton) = " + ((double)answer1[1]).ToString();
        }
    }
}
