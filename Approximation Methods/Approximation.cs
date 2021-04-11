using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Linear_Algebra;
namespace Approximation_Methods
{
    public partial class Approximation : Form
    {
        public Approximation()
        {
            InitializeComponent();
            this.dataGridView1.Rows.Add("Xi", "0,0", "0,2", "0,4", "0,6", "0,8", "1,0");
            this.dataGridView1.Rows.Add("f(Xi)", "1,0", "1,0032", "1,0512", "1,2592", "1,8192", "3,0");
        }

        private void Approximation_Load(object sender, EventArgs e)
        {
            chart1.Series["Series1"].ChartType = System.Windows.Forms.DataVisualization.Charting.SeriesChartType.Spline;
            chart1.Series["Series1"].Color = Color.Red;
            chart1.Series["Series1"].LegendText = "First degree approximating polynom";
            chart1.Series.Add("Series2");
            chart1.Series["Series2"].LegendText = "Dots";
            chart1.Series["Series2"].ChartType = System.Windows.Forms.DataVisualization.Charting.SeriesChartType.Point;
            chart1.Series["Series2"].Color = Color.Black;
            chart1.ChartAreas["ChartArea1"].AxisX.Title = "X";
            chart1.ChartAreas["ChartArea1"].AxisY.Title = "Y";
            chart1.ChartAreas["ChartArea1"].AxisX.ArrowStyle =
                System.Windows.Forms.DataVisualization.Charting.AxisArrowStyle.SharpTriangle;
            chart1.ChartAreas["ChartArea1"].AxisY.ArrowStyle =
                System.Windows.Forms.DataVisualization.Charting.AxisArrowStyle.SharpTriangle;
            chart1.Series.Add("Series3");
            chart1.Series["Series3"].LegendText = "Second degree approximating polynom";
            chart1.Series["Series3"].Color = Color.Green;
            chart1.Series["Series3"].ChartType = System.Windows.Forms.DataVisualization.Charting.SeriesChartType.Spline;
            List<object> answer = ApproximationPolynom();
            List<double> x1 = (List<double>)answer[0];
            List<double> y1 = (List<double>)answer[1];
            List<double> x2 = (List<double>)answer[3];
            List<double> y2 = (List<double>)answer[4];
            string err = ((double)answer[2]).ToString();
            string err1 = ((double)answer[5]).ToString();
            for (int i = 0; i < x1.Count; i++)
                chart1.Series["Series1"].Points.AddXY(x1[i], y1[i]);
            for (int i = 0; i < x2.Count; i++)
                chart1.Series["Series3"].Points.AddXY(x2[i], y2[i]);
            label1.Text += "Error(first degree) = " + err;
            label2.Text += "Error(second degree) = " + err1;

        }
        public List<object> ApproximationPolynom()
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
            for (int i = 0; i < x.Count; i++)
            {
                chart1.Series["Series2"].Points.AddXY(x[i], y[i]);
            }
            Matrix asys = new Matrix(2);
            asys[0, 0] = x.Count;
            asys[0, 1] = x.Sum();
            asys[1, 0] = x.Sum();
            double tmp = 0;
            foreach (double i in x)
            {
                tmp += i * i;
            }
            asys[1, 1] = tmp;
            tmp = 0;
            for (int i = 0; i < x.Count; i++)
            {
                tmp += x[i] * y[i];

            }
            List<double> b = new List<double>();
            b.Add(y.Sum()); b.Add(tmp);
            List<Matrix> LU = (List<Matrix>)asys.LUDec(b)[0];
            List<double> a = asys.SOLVE(LU[0], LU[1], b);
            List<double> xres = new List<double>(), yres = new List<double>();
            xres.Add(x[0]);
            yres.Add(f(xres[xres.Count - 1], a));
            xres.Add(x[0] + 0.01);
            yres.Add(f(xres[xres.Count - 1], a));
            while (xres[xres.Count - 1] < x[x.Count - 1])
            {
                xres.Add(xres[xres.Count - 1] + 0.01);
                yres.Add(f(xres[xres.Count - 1], a));
            }
            double err = 0;
            for (int i = 0; i < x.Count; i++)
            {
                err += (f(x[i], a) - y[i]) * (f(x[i], a) - y[i]);
            }
            asys = new Matrix(3);
            asys[0, 0] = x.Count; asys[1, 0] = x.Sum(); asys[0, 1] = x.Sum();
            tmp = 0;
            foreach (double i in x)
                tmp += i * i;
            asys[0, 2] = tmp; asys[1, 1] = tmp; asys[2, 0] = tmp;
            tmp = 0;
            foreach (double i in x)
            {
                tmp += Math.Pow(i, 3);

            }
            asys[1, 2] = tmp;
            asys[2, 1] = tmp;
            tmp = 0;
            foreach (double i in x)
            {
                tmp += Math.Pow(i, 4);

            }
            asys[2, 2] = tmp;
            b = new List<double>();
            b.Add(y.Sum());
            tmp = 0;
            for (int i = 0; i < x.Count; i++)
            {
                tmp += x[i] * y[i];
            }
            b.Add(tmp);
            tmp = 0;
            for (int i = 0; i < x.Count; i++)
            {
                tmp += x[i] * x[i] * y[i];
            }
            b.Add(tmp);
            LU = (List<Matrix>)asys.LUDec(b)[0];
            a = asys.SOLVE(LU[0], LU[1], b);
            List<double> xres1 = new List<double>(), yres1 = new List<double>();
            xres1.Add(x[0]);
            yres1.Add(f(xres1[xres1.Count - 1], a));
            xres1.Add(x[0] + 0.01);
            yres1.Add(f(xres1[xres1.Count - 1], a));
            while (xres1[xres1.Count - 1] < x[x.Count - 1])
            {
                xres1.Add(xres1[xres1.Count - 1] + 0.01);
                yres1.Add(f(xres1[xres1.Count - 1], a));
            }
            double err1 = 0;
            for (int i = 0; i < x.Count; i++)
            {
                err1 += (f(x[i], a) - y[i]) * (f(x[i], a) - y[i]);
            }
            answer.Add(xres);
            answer.Add(yres);
            answer.Add(err);
            answer.Add(xres1);
            answer.Add(yres1);
            answer.Add(err1);
            return answer;
        }

        public double f(double x, List<double> list) =>
            (list.Count == 2) ? 
                list[0] + list[1] * x : list[0] +
                    list[1] * x + list[2] * x * x;

        private void button1_Click(object sender, EventArgs e)
        {
            chart1.Series.Clear();
            chart1.Series.Add("Series1");
            label1.Text = "";
            label2.Text = "";
            Approximation_Load(sender, e);
        }
    }

}
