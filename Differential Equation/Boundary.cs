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
            List<List<double>> points = finite_difference_method();
            List<double> x = points[0];
            List<double> y = points[1];

            List<List<double>> points_newh_f = new List<List<double>>();
            points_newh_f = finite_difference_method(0, 1, 0, 1, 2, 1, -1, 3, 0.05);

            List<List<double>> res_finite = new List<List<double>>();
            res_finite.Add(y);
            res_finite.Add(points_newh_f[1]);

            
            for (int i = 0; i < x.Count; i++)
                chart1.Series["Finite Difference Method"].Points.AddXY(x[i], y[i]);
            chart1.Series.Add("Finite Difference Dots");
            chart1.Series["Finite Difference Dots"].ChartType = System.Windows.Forms.DataVisualization.Charting.SeriesChartType.FastPoint;
            for (int i = 0; i < x.Count - 1; i++)
                chart1.Series["Finite Difference Dots"].Points.AddXY(x[i], y[i]);

            chart1.Series.Add("Shooting Method");
            chart1.Series["Shooting Method"].ChartType = System.Windows.Forms.DataVisualization.Charting.SeriesChartType.Spline;
            points = shooting_method();
            List<List<double>> points_newh = new List<List<double>>();
            points_newh = shooting_method(0, 1, 0, 1, 2, 1, -1, 3, 0.05);
            List<List<double>> res_shoot = new List<List<double>>();
            res_shoot.Add(points[1]); res_shoot.Add(points_newh[1]);
            x = points[0]; y = points[1];
            
            for (int i = 0; i < y.Count; i++)
                chart1.Series["Shooting Method"].Points.AddXY(x[i], y[i]);
            chart1.Series.Add("Shooting Dots");
            chart1.Series["Shooting Dots"].ChartType = System.Windows.Forms.DataVisualization.Charting.SeriesChartType.FastPoint;
            for (int i = 0; i < x.Count - 1; i++)
                chart1.Series["Shooting Dots"].Points.AddXY(x[i], y[i]);
            y.Clear();
            foreach (var f in x)
                y.Add(func(f));
            chart1.Series.Add("Exact");
            chart1.Series["Exact"].ChartType = System.Windows.Forms.DataVisualization.Charting.SeriesChartType.Spline;
            chart1.Series["Exact"].Color = Color.Green;
            for (int i = 0; i < x.Count - 1; i++)
                chart1.Series["Exact"].Points.AddXY(x[i], y[i]);
            chart1.Series.Add("Exact Dots");
            chart1.Series["Exact Dots"].ChartType = System.Windows.Forms.DataVisualization.Charting.SeriesChartType.FastPoint;
            for (int i = 0; i < x.Count - 1; i++)
                chart1.Series["Exact Dots"].Points.AddXY(x[i], y[i]);
            //chart1.Series["Exact_dots"].Legend = " ";

            chart1.Series.Remove(chart1.Series["Series1"]);
            richTextBox2.Text = "--------------------------------------------------------------------------------------------------\n";
            richTextBox2.Text += "RUNGE-ROMBERG-RITCHARDSON ERRORS\n"
                + "--------------------------------------------------------------------------------------------------\n"; 
            List<List<double>> errors_RR = Runge_Romberg_method(res_finite, res_shoot, 0.1 / 0.05);
            richTextBox2.Text += "finite errors \n";
            foreach (var f in errors_RR[0])
                richTextBox2.Text += f.ToString() + "\n";
            richTextBox2.Text += "shooting errors \n";
            foreach (var f in errors_RR[1])
                richTextBox2.Text += f.ToString() + "\n";

            richTextBox2.Text += "--------------------------------------------------------------------------------------------------"
                + "\nEXACT ERRORS\n" + "--------------------------------------------------------------------------------------------------\n";

            List<List<double>> exact_errors = exact_error(finite_difference_method()[1], shooting_method()[1], y);

            richTextBox2.Text += "finite-exact error \n";
            foreach (var f in exact_errors[0])
                richTextBox2.Text += f.ToString() + "\n";
            richTextBox2.Text += "finite-exact error \n";
            foreach (var f in exact_errors[1])
                richTextBox2.Text += f.ToString() + "\n";
            double iter = shooting_method()[3][0];
            richTextBox2.Text += "--------------------------------------------------------------------------------------------------\n" + "Iterations: " + iter.ToString();
            List<double> n0 = shooting_method()[4];
            richTextBox2.Text += "--------------------------------------------------------------------------------------------------\n";
            foreach (var f in n0)
                richTextBox2.Text += f.ToString() + "\n";
        }
        public List<List<double>> shooting_method(double a = 0, double b = 1, double alpha = 0, double beta = 1, double delta = 2,
            double gamma = 1, double y0 = -1, double y1 = 3, double h = 0.1)
        {

            double n_prev = 100, n = 10;
            double y_der = (y0 - alpha * n_prev) / beta;
            var x = new List<double>();
            List<double> n0 = new List<double>();
            n0.Add(n_prev);
            n0.Add(n);
            for (double i = a; i < b+h; i += h)
                x.Add(i);
            var ans_prev = Runge_Kutta(x, a, b, h, n_prev, y_der);
            y_der = (y0 - alpha * n) / beta;
            var ans = Runge_Kutta(x, a, b, h, n, y_der);
            int iter = 2;

            while (check_finish(x, ans[0], b, delta, gamma, y1))
            {
                iter++;
                double tmp = n;
                n = get_n(n_prev, n, ans_prev, ans,x);
                n0.Add(n);
                n_prev = tmp;
                ans_prev.Clear();
                foreach (var f in ans)
                    ans_prev.Add(f);
                y_der = (y0 - alpha * n) / beta;
                ans = Runge_Kutta(x, a, b, h, n, y_der);
                
            }

            ans.Insert(0, x);
            List<double> iters = new List<double>();
            iters.Add(iter);
            ans.Add(iters);
            ans.Add(n0);
            return ans;
        }
        public double get_n(double n_prev, double n, List<List<double>> ans_prev, List<List<double>> ans, List<double> x, double b = 1, double delta = 2, double gamma = 1, double y1 = 3)
        {
            //List<double> x = new List<double>();
            
            List<double> y = new List<double>();
            foreach (var f in ans_prev[0])
                y.Add(f);
            double y_der = first_der(x, y, b);
            double phi_n_prev = delta * y[y.Count - 1] + gamma * y_der - y1;
            y.Clear();
            
            foreach (var f in ans[0])
                y.Add(f);
            y_der = first_der(x, y, b);
            double phi_n = delta * y[y.Count - 1] + gamma * y_der - y1;
            return n - (n - n_prev) / (phi_n - phi_n_prev) * phi_n;

        }
        public bool check_finish(List<double> x, List<double >y, double b = 1, double delta = 2, double gamma = 1, double y1 = 1, double eps = 1E-12)
        {
            int iter;
            double y_der = first_der(x, y, b);
            
            return (Math.Abs(delta * y[y.Count - 1] + gamma * y_der - y1) > eps);
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
            while (i < x.Count - 1 && x[i + 1] + 1E-8 < xfix)
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
                a_tma.Add(1 - p(x[i]) * h / 2);
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
        public List<List<double>> Runge_Kutta(List<double> x, double a, double b, double h, double y0 = 1, double y_der = 1)
        {
            var n = (int)(1 / h);
            List<List<double>> answer = new List<List<double>>();
            List<double> y = new List<double>();
            List<double> k = new List<double>() { 0, 0, 0, 0 };
            List<double> l = new List<double>() { 0, 0, 0, 0 };
            //for (int i = 0)
            List<double> z = new List<double>();
            
            z.Add(y_der);
            y.Add(y0);
            for (int i = 0; i < n; i++)
            {
                l[0] = h * f(x[i], y[i], z[i]);
                k[0] = h * g(x[i], y[i], z[i]);
                l[1] = h * f(x[i] + h / 2, y[i] + k[0] / 2, z[i] + l[0] / 2);
                k[1] = h * g(x[i] + h / 2, y[i] + k[0] / 2, z[i] + l[0] / 2);
                l[2] = h * f(x[i] + h / 2, y[i] + k[1] / 2, z[i] + l[1] / 2);
                k[2] = h * g(x[i] + h / 2, y[i] + k[1] / 2, z[i] + l[1] / 2);
                l[3] = h * f(x[i] + h, y[i] + k[2], z[i] + l[2]);
                k[3] = h * g(x[i] + h, y[i] + k[2], z[i] + l[2]);
                z.Add(z[i] + delta(l));
                y.Add(y[i] + delta(k));
            }
            answer.Add(y);
            answer.Add(z);
            return answer;
        }
        public double g(double x, double y, double z) => z;

        public List<List<double>> Runge_Romberg_method(List<List<double>> res_finite, List<List<double>> res_shoot, double k)
        {
            
            List<List<double>> ans = new List<List<double>>();
            List<double> err_finite = new List<double>();
            List<double> err_shooting = new List<double>();
            for (int i = 0; i < res_finite[0].Count; i++)
            {
                err_finite.Add(Math.Abs(res_finite[0][i] - res_finite[1][i]) / (Math.Pow(k, 1) - 1)) ;
            }
            for (int i = 0; i < res_shoot[0].Count; i++)
            {
                err_shooting.Add(Math.Abs(res_shoot[0][i] - res_shoot[1][i]) / (Math.Pow(k, 1) - 1));
            }
            
            ans.Add(err_finite);
            ans.Add(err_shooting);
           // ans.Add(err_adams);

            return ans;
        }

        public List<List<double>> exact_error(List<double> res_finite, List<double> res_shoot, List<double> exact)
        {
            //res[0] -euler
            List<List<double>> ans = new List<List<double>>();
            List<double> err_finite = new List<double>();
            List<double> err_shoot = new List<double>();
            for (int i = 0; i < Math.Min(res_finite.Count, exact.Count); i++)
                err_finite.Add(Math.Abs(res_finite[i] - exact[i]));
            for (int i = 0; i < Math.Min(res_shoot.Count, exact.Count); i++)
                err_shoot.Add(Math.Abs(res_shoot[i] - exact[i]));
            ans.Add(err_finite);
            ans.Add(err_shoot);
            return ans;

        }

    }

}
