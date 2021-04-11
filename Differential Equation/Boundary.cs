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
    public partial class Boundary : Form
    {
        public Boundary()
        {
            InitializeComponent();
        }

        private void Boundary_Load(object sender, EventArgs e)
        {
            chart1.Series.Add("Finite Difference Method");
            chart1.Series["Finite Difference Method"].ChartType = System.Windows.Forms.DataVisualization.Charting.SeriesChartType.Spline;
            // List<List<double>> points = finite_difference_method(Math.PI / 4, Math.PI / 3, 0, 1, -1, 1, 3 + Math.PI / 2, 3 + (Math.PI*(4 - Math.Sqrt(3)) / 3));
            List<List<double>> points = finite_difference_method();
            List<double> x = points[0];
            List<double> y = points[1];
            for (int i = 0; i < x.Count; i++)
                chart1.Series["Finite Difference Method"].Points.AddXY(x[i], y[i]);
        }
        public bool finish(double b, List<double> x, List<double> y, double delta = 2, double gamma = 1, double y1 = 3)
        {
            double varepsilon = 0.00000001;
            double y_der = first_der(x, y, b);
            return Math.Abs(delta * y[y.Count - 1] + gamma * y_der - y1) > varepsilon;
        }
        public List<List<double>> shooting_method(double a = 0, double b = 1, double alpha = 0, double beta = 1, double delta = 2,
            double gamma = 1, double y0 = -1, double y1 = 3, double h = 0.1)
        {
            List<List<double>> points = new List<List<double>>();
            double varepsilon = 0.00000001;

            return points;
        }
        public double f(double x, double y, double yder) =>
            (4 * y - 4 * x * yder) / (2 * x + 1);
        public double p(double x) =>
            4 * x / (2 * x + 1);
        public double q(double x) =>
            -4 / (2 * x + 1);
        public double f(double x) => 0;
        public double func(double x) =>
            x + Math.Exp(-2 * x);
        public double first_der(List<double> x, List<double> y, double xfix)
        {
            int i = 0;
            while (i < x.Count - 1 && x[i + 1] < xfix)
                i++;
            return (y[i + 1] - y[i]) / (x[i + 1] - x[i]);
        }
        public List<List<double>> finite_difference_method(double a = 0, double b = 1, double alpha = 0, double beta = 1, double delta = 2,
            double gamma = 1, double y0 = -1, double y1 = 3, double h = 0.1)
        {
            List<List<double>> points = new List<List<double>>();
            int n = (int)((b - a) / h);
            List<double> x = new List<double>();
            for (double i = a; i < b; i += h)
            {
                x.Add(i);

            }
            List<double> a_tma = new List<double>();
            List<double> b_tma = new List<double>();
            List<double> c_tma = new List<double>();
            List<double> d_tma = new List<double>();
            a_tma.Add(0);
            for (int i = 0; i < n - 1; i++)
                a_tma.Add(1 + p(x[i]) * h / 2);
            a_tma.Add(-gamma);
            b_tma.Add(alpha * h - beta);
            for (int i = 0; i < n-1; i++) 
                b_tma.Add(q(x[i]) * h *h -2 );
            b_tma.Add(delta * h + gamma);
            c_tma.Add(beta);
            for (int i = 0; i < n - 1; i++)
                c_tma.Add(1 + p(x[i]) * h / 2);
            c_tma.Add(0);
            d_tma.Add(y0 * h);
            for (int i = 0; i < n - 1; i++)
                d_tma.Add(f(x[i]) * h * h);
            d_tma.Add(y1 * h);

            List<double> y = TMA(a_tma.Count, a_tma, b_tma, c_tma, d_tma);
            points.Add(x);
            points.Add(y);
            return points;
        }
        //alpha = 0; beta = 1; delta = 2; gamma = 1; y0 = -1, y1 = 3 h = 0.1
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
        public double delta(List<double> k) =>
            (k[0] + 2 * k[1] + 2 * k[2] + k[3]) / 6;
        public List<List<double>> Runge_Kutta(List<double> x, double a, double b, double h)
        {
            List<List<double>> answer = new List<List<double>>();
            List<double> y = new List<double>();
            List<double> k = new List<double>() { 0, 0, 0, 0 };
            List<double> l = new List<double>() { 0, 0, 0, 0 };
            //for (int i = 0)
            List<double> z = new List<double>();
            z.Add(2);
            y.Add(1);
            for (int i = 0; i < x.Count; i++)
            {
                l[0] = h * f(x[i], y[i], z[i]);
                l[1] = h * f(x[i] + h / 2, y[i] + k[0] / 2, z[i] + l[0] / 2);
                l[2] = h * f(x[i] + h / 2, y[i] + k[1] / 2, z[i] + l[1] / 2);
                l[3] = h * f(x[i] + h / 2, y[i] + k[2], z[i] + l[2]);
                k[0] = h * z[i];
                k[1] = h * (z[i] + l[0] / 2);
                k[2] = h * (z[i] + l[1] / 2);
                k[3] = h * (z[i] + l[2]);
                z.Add(z[i] + delta(l));
                y.Add(y[i] + delta(k));
            }
            answer.Add(y);
            answer.Add(z);
            return answer;
        }
    }

}
