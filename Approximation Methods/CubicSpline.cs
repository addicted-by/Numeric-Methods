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
    public partial class CubicSpline : Form
    {
        public CubicSpline()
        {
            InitializeComponent();
            this.dataGridView1.Rows.Add("Xi", "0,0", "1,7", "3,4", "5,1", "6,8");
            this.dataGridView1.Rows.Add("f(Xi)", "0,0", "1,3038", "1,8439", "2,2583", "2,6077");
            
        }

        private void CubicSpline_Load(object sender, EventArgs e)
        {
            chart1.Series["Series1"].LegendText = "Cubic Spline";
            chart1.Series["Series1"].ChartType = System.Windows.Forms.DataVisualization.Charting.SeriesChartType.Spline;
            chart1.Series["Series1"].Color = Color.Red;
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
            List<object> answer = Spline();
            List<double> x1 = (List<double>)answer[0];
            List<double> y1 = (List<double>)answer[1];
            for (int i = 0; i < x1.Count; i++)
                chart1.Series["Series1"].Points.AddXY(x1[i], y1[i]);
            label1.Text = "F(X*) = " + Convert.ToString((double)answer[2]);
            
        }
        public List<object> Spline()
        {
            List<object> answer = new List<object>();
            double xfix = Convert.ToDouble(textBox5.Text);
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
            List<double> a_tma = new List<double>();
            a_tma.Add(0); a_tma.Add(h(x, 2)); a_tma.Add(h(x, 3));
            List<double> b_tma = new List<double>();
            b_tma.Add(2 * (h(x, 1) + h(x, 2))); b_tma.Add(2 * (h(x, 2) + h(x, 3)));
            b_tma.Add(2 * (h(x, 3) + h(x, 4)));
            List<double> d_tma = new List<double>();
            d_tma.Add(3 * ((y[2] - y[1]) / h(x, 2) - (y[1] - y[0]) / h(x, 1)));
            d_tma.Add(3 * ((y[3] - y[2]) / h(x, 3) - (y[2] - y[1]) / h(x, 2)));
            d_tma.Add(3 * ((y[4] - y[3]) / h(x, 4) - (y[3] - y[2]) / h(x, 3)));
            List<double> c_tma = new List<double>();
            c_tma.Add(h(x,2)); c_tma.Add(h(x, 3)); c_tma.Add(0);
            List<double> c = new List<double>();
            c.Add(0);
            List<double> tmp = TMA(3, a_tma, b_tma, c_tma, d_tma);
            for (int i = 0; i < tmp.Count; i++)
                c.Add(tmp[i]);
            List<double> a = new List<double>();
            for (int i = 0; i < y.Count-1; i++)
            {
                a.Add(y[i]);
            }
            List<double> b = new List<double>();
            for (int i = 0; i < y.Count - 2; i++)
                b.Add((y[i + 1] - y[i]) / h(x, i+1) - h(x, i+1) * (c[i + 1] + 2 * c[i]) / 3);
            b.Add((y[4] - y[3]) / h(x, 4) - h(x, 4) * c[3] * 2 / 3);
            List<double> d = new List<double>();
            for (int i = 0; i < y.Count - 2; i++)
                d.Add((c[i+1] - c[i]) / (3 * h(x, i+1)));
            d.Add(-c[3] / (3 * h(x, 4)));
            double xprev, yprev, xtmp, ytmp;
            List<double> xres = new List<double>(), 
                yres = new List<double>();
            for (int i = 0; i < a.Count; i++)
            {
                xres.Add(x[i]);
                yres.Add(y[i]);
                xtmp = x[i] + 0.01;
                ytmp = s(xtmp, x[i], a[i], b[i], c[i], d[i]);
                xres.Add(xtmp);
                yres.Add(ytmp);
                while (xtmp < x[i+1])
                {
                    xtmp = xtmp + 0.01;
                    ytmp = s(xtmp, x[i], a[i], b[i], c[i], d[i]);
                    xres.Add(xtmp);
                    yres.Add(ytmp);

                } 
            }

            xres.Add(x[a.Count]);
            yres.Add(y[a.Count]);
            answer.Add(xres);
            answer.Add(yres);
            answer.Add(s(xfix, x[1], a[1], b[1], c[1], d[1]));
            return answer;
        }

        public double s(double x, double xprev, double a, double b,
            double c, double d) =>
            a + b * (x - xprev) + c * (x - xprev) * (x - xprev) + d * (x-xprev) * (x - xprev) * (x - xprev);
        public double h(List<double> x, int i) => x[i] - x[i - 1];

        private void button1_Click(object sender, EventArgs e)
        {
            chart1.Series.Clear();
            chart1.Series.Add("Series1");
            CubicSpline_Load(sender, e);
        }
        #region(Lab 1.2 Tridiagonal Matrix Algorithm)
        static public List<double> TMA(int n, List<double> a, List<double> b, List<double> c, List<double> d)
        {
            List<double> P = new List<double>();
            List<double> Q = new List<double>();

            //direct
            P.Add(-c[0] / b[0]);
            Q.Add(d[0] / b[0]);
            List<double> answer = new List<double>();
            answer.Add(0f);
            for (int i = 1; i < n; i++)
            {
                //if (i < n-1)
                P.Add(-c[i] / (a[i] * P[i - 1] + b[i]));
                Q.Add((d[i] - a[i] * Q[i - 1]) / (b[i] + a[i] * P[i - 1]));
                answer.Add(0f);
            }

            //back
            answer[n - 1] = Q[n - 1];
            for (int i = n - 2; i > -1; i--)
            {
                answer[i] = P[i] * answer[i + 1] + Q[i];
            }
            return answer;
        }
        #endregion
    }
}
