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
    public partial class Solver : Form
    {
        
        public Solver()
        {
            InitializeComponent();
            chart1.Series.Add("Euler");
            chart1.ChartAreas["ChartArea1"].AxisX.Title = "X";
            chart1.ChartAreas["ChartArea1"].AxisY.Title = "Y";
            chart1.ChartAreas["ChartArea1"].AxisX.ArrowStyle =
                System.Windows.Forms.DataVisualization.Charting.AxisArrowStyle.SharpTriangle;
            chart1.ChartAreas["ChartArea1"].AxisY.ArrowStyle =
                System.Windows.Forms.DataVisualization.Charting.AxisArrowStyle.SharpTriangle;
            chart1.Series["Euler"].Color = Color.Green;
            chart1.Series["Euler"].ChartType = System.Windows.Forms.DataVisualization.Charting.SeriesChartType.Spline;
            chart1.Series.Add("Runge Kutta");
            chart1.Series["Runge Kutta"].Color = Color.Red;
            chart1.Series["Runge Kutta"].ChartType = System.Windows.Forms.DataVisualization.Charting.SeriesChartType.Spline;
            chart1.Series.Add("Adams");
            chart1.Series["Adams"].Color = Color.Chocolate;
            chart1.Series["Adams"].ChartType = System.Windows.Forms.DataVisualization.Charting.SeriesChartType.Spline;
            chart1.Series["Adams"].BorderWidth = 2;
        }

        private void Solver_Load(object sender, EventArgs e)
        {
            chart1.Series["Series1"].ChartType = System.Windows.Forms.DataVisualization.Charting.SeriesChartType.Spline;
            chart1.Series["Series1"].LegendText = "Exact function";
            
            double a = 0;
            double b = 1;
            double h = Convert.ToDouble(h_data.Text);
            double h1 = h / 10;
            List<double> x = get_points(a, b, h);
            List<double> y_euler = Euler(x, a, b, h);
            if (eu.Checked)
                for (int i = 0; i < x.Count; i++)
                    chart1.Series["Euler"].Points.AddXY(x[i], y_euler[i]);
            if (fun.Checked)
                for (int i = 0; i < x.Count; i++)
                    chart1.Series["Series1"].Points.AddXY(x[i], f(x[i]));
            List<double> y_runge = Runge_Kutta(x, a, b, h)[0];
            List<double> z_runge = Runge_Kutta(x, a, b, h)[1];
            if (rk.Checked)
                for (int i = 0; i < x.Count; i++)
                    chart1.Series["Runge Kutta"].Points.AddXY(x[i], y_runge[i]);
            List<double> y_adams = Adams(x, a, b, h, y_runge, z_runge);
            if (ad.Checked)
                for (int i = 0; i < x.Count; i++)
                    chart1.Series["Adams"].Points.AddXY(x[i], y_adams[i]);
            List<double> y_euler1 = Euler(x, a, b, h1);
            List<double> y_runge1 = Runge_Kutta(x, a, b, h1)[0];
            List<double> z_runge1 = Runge_Kutta(x, a, b, h1)[1];
            List<double> y_adams1 = Adams(x, a, b, h, y_runge1, z_runge1);
            List<List<double>> res = new List<List<double>>();
            res.Add(y_euler);
            res.Add(y_euler1);
            res.Add(y_runge);
            res.Add(y_runge1);
            res.Add(y_adams);
            res.Add(y_adams1);
            List<List<double>> errs = Runge_Romberg_method(res, h / h1);
            List<double> err_euler = errs[0];
            richTextBox1.Text += "Euler errors\n";
            foreach (double err in err_euler)
                richTextBox1.Text += err.ToString() + "\n";
            richTextBox1.Text += "\n-----------------------------------------------------\n";
            List<double> err_runge = errs[1];
            List<double> err_adams = errs[2];
            richTextBox1.Text += "Runge errors\n";
            foreach (double err in err_runge)
                richTextBox1.Text += err.ToString() + "\n";
            richTextBox1.Text += "\n-----------------------------------------------------\n";
            richTextBox1.Text += "Adams errors\n";
            foreach (double err in err_adams)
                richTextBox1.Text += err.ToString() + "\n";
            richTextBox1.Text += "\n-----------------------------------------------------\n";

            richTextBox1.Text += "Exact errors\n";
            List<double> exact = new List<double>();
            foreach (double X in x)
                exact.Add(f(X));
            
            List<List<double>> exact_err = exact_error(res,exact);
            List<double> exact_euler = exact_err[0];
            List<double> exact_runge = exact_err[1];
            List<double> exact_adams = exact_err[2];
            richTextBox1.Text += "Euler and exact errors\n";
            foreach (double err in exact_euler)
                richTextBox1.Text += err.ToString() + "\n";
            richTextBox1.Text += "\n-----------------------------------------------------\n";
            richTextBox1.Text += "Runge and exact errors\n";
            foreach (double err in exact_runge)
                richTextBox1.Text += err.ToString() + "\n";
            richTextBox1.Text += "\n-----------------------------------------------------\n";

            richTextBox1.Text += "Adams and exact errors\n";
            foreach (double err in exact_adams)
                richTextBox1.Text += err.ToString() + "\n";
            richTextBox1.Text += "\n-----------------------------------------------------\n";





        }
        public List<double> get_points(double a, double b, double h)
        {
            List<double> x = new List<double>();
            x.Add(a);
            while (x[x.Count - 1] <= b)
                x.Add(x[x.Count - 1] + h);
            return x;
        }
        public List<double> Euler(List<double> x, double a, double b, double h)
        {
            List<double> y = new List<double>();
            List<double> z = new List<double>();
            z.Add(2); y.Add(1);
            for (int i = 0; i < x.Count; i++)
            {
                z.Add(z[i] + h * f(x[i], y[i], z[i]));
                y.Add(y[i] + h * z[i]);
            }
            
            return y;
        }
        public List<List<double>> Runge_Kutta(List<double> x, double a, double b, double h)
        {
            List<List<double>> answer = new List<List<double>>(); 
            List<double> y = new List<double>();
            List<double> k = new List<double>() { 0, 0, 0, 0 };
            List<double> l = new List<double>() { 0, 0, 0, 0 };
            //for (int i = 0)
            List<double> z = new List<double>();
            z.Add(1);
            y.Add(1);
            for (int i = 0; i < x.Count; i++)
            {
                l[0] = h * f(x[i], y[i], z[i]);
                k[0] = h * g(x[i], y[i], z[i]);
                l[1] = h * f(x[i] + h / 2, y[i] + k[0] / 2, z[i] + l[0] / 2);
                k[1] = h * g(x[i] + l[0] / 2, y[i] + l[0] / 2, z[i] + l[0] / 2);
                l[2] = h * f(x[i] + h / 2, y[i] + k[1] / 2, z[i] + l[1] / 2);
                k[2] = h * g(x[i] + l[1] / 2, y[i] + l[1] / 2, z[i] + l[1] / 2);
                l[3] = h * f(x[i] + h / 2, y[i] + k[2], z[i] + l[2]);
                k[3] = h * g(x[i] + l[2], y[i] + l[2], z[i] + l[2]);
                z.Add(z[i] + delta(l));
                y.Add(y[i] + delta(k));
            }
            answer.Add(y);
            answer.Add(z);
            return answer;
        }

        public double g(double x, double y, double z) => z;
        public List<double> Adams(List<double> x, double a, double b, double h, List<double> y_runge, List<double> z_runge)
        {
            List<double> y = new List<double>();
            List<double> z = new List<double>();
            z.Add(2);
            z.Add(z_runge[1]);
            z.Add(z_runge[2]);
            z.Add(z_runge[3]);
            y.Add(1);
            y.Add(y_runge[1]);
            y.Add(y_runge[2]);
            y.Add(y_runge[3]);
            for (int i = 3; i < x.Count - 1; i++)
            {
                double tmp1 = z[i] + h * (55 * f(x[i], y[i], z[i]) - 59 * f(x[i - 1], y[i - 1], z[i - 1]) +
                    37 * f(x[i - 2], y[i - 2], z[i - 2]) - 9 * f(x[i - 3], y[i - 3], z[i - 3])) / 24;
                double tmp2 = y[i] + h * (55 * (z[i]) - 59 * (z[i - 1])
                    + 37 * (z[i - 2]) - 9 * (z[i - 3])) / 24;
                z.Add(tmp1);
                y.Add(tmp2);
            }
            return y;
        }
        public double f(double x) =>
            (1 + x) * Math.Exp(x * x);
        public double delta(List<double> k) =>
            (k[0] + 2 * k[1] + 2 * k[2] + k[3]) / 6;
        public double f(double x, double y, double z) =>
            4 * x * z - (4 * x * x - 2) * y;
        public List<List<double>> Runge_Romberg_method(List<List<double>> res, double k)
        {
            List<List<double>> ans = new List<List<double>>();
            List<double> err_euler = new List<double>();
            List<double> err_runge = new List<double>();
            List<double> err_adams = new List<double>();
            for (int i = 0; i < res[0].Count; i++)
            {
                err_euler.Add(Math.Abs(res[0][i] - res[1][i]));
            }
            for (int i = 0; i < res[2].Count; i++)
            {
                err_runge.Add(Math.Abs(res[2][i] - res[3][i]) / (Math.Pow(k,4) - 1));
            }
            for (int i = 0; i < res[4].Count; i++)
            {
                err_adams.Add(Math.Abs(res[4][i] - res[5][i]) / (Math.Pow(k, 4) - 1));
            }
            ans.Add(err_euler);
            ans.Add(err_runge);
            ans.Add(err_adams);
            
            return ans;
        }

        public List<List<double>> exact_error(List<List<double>> res, List<double> exact)
        {
            //res[0] -euler
            List<List<double>> ans = new List<List<double>>();
            List<double> err_euler = new List<double>();
            List<double> err_runge = new List<double>();
            List<double> err_adams = new List<double>();
            for (int i = 0; i < Math.Min(res[0].Count, exact.Count); i++)
                err_euler.Add(Math.Abs(res[0][i] - exact[i]));
            for (int i = 0; i < Math.Min(res[2].Count, exact.Count); i++)
                err_runge.Add(Math.Abs(res[2][i] - exact[i]));
            for (int i = 0; i < Math.Min(res[4].Count, exact.Count); i++)
                err_adams.Add(Math.Abs(res[4][i] - exact[i]));
            ans.Add(err_euler);
            ans.Add(err_runge);
            ans.Add(err_adams);
            return ans;

        }

        private void checkBox4_CheckedChanged(object sender, EventArgs e)
        {
            chart1.Series["Series1"].Points.Clear();
            chart1.Series["Euler"].Points.Clear();
            chart1.Series["Runge Kutta"].Points.Clear();
            chart1.Series["Adams"].Points.Clear();
            Solver_Load(sender, e);
        }

        private void ad_CheckedChanged(object sender, EventArgs e)
        {
            chart1.Series["Series1"].Points.Clear();
            chart1.Series["Euler"].Points.Clear();
            chart1.Series["Runge Kutta"].Points.Clear();
            chart1.Series["Adams"].Points.Clear();
            Solver_Load(sender, e);
        }

        private void rk_CheckedChanged(object sender, EventArgs e)
        {
            chart1.Series["Series1"].Points.Clear();
            chart1.Series["Euler"].Points.Clear();
            chart1.Series["Runge Kutta"].Points.Clear();
            chart1.Series["Adams"].Points.Clear();
            Solver_Load(sender, e);
        }

        private void eu_CheckedChanged(object sender, EventArgs e)
        {
            chart1.Series["Series1"].Points.Clear();
            chart1.Series["Euler"].Points.Clear();
            chart1.Series["Runge Kutta"].Points.Clear();
            chart1.Series["Adams"].Points.Clear();
            Solver_Load(sender, e);
        }

        private void button1_Click(object sender, EventArgs e)
        {
            chart1.Series.Clear();
            chart1.Series.Add(new System.Windows.Forms.DataVisualization.Charting.Series("Series1"));
            chart1.Series.Add("Euler");
            chart1.ChartAreas["ChartArea1"].AxisX.Title = "X";
            chart1.ChartAreas["ChartArea1"].AxisY.Title = "Y";
            chart1.ChartAreas["ChartArea1"].AxisX.ArrowStyle =
                System.Windows.Forms.DataVisualization.Charting.AxisArrowStyle.SharpTriangle;
            chart1.ChartAreas["ChartArea1"].AxisY.ArrowStyle =
                System.Windows.Forms.DataVisualization.Charting.AxisArrowStyle.SharpTriangle;
            chart1.Series["Euler"].Color = Color.Green;
            chart1.Series["Euler"].ChartType = System.Windows.Forms.DataVisualization.Charting.SeriesChartType.Spline;
            chart1.Series.Add("Runge Kutta");
            chart1.Series["Runge Kutta"].Color = Color.Red;
            chart1.Series["Runge Kutta"].ChartType = System.Windows.Forms.DataVisualization.Charting.SeriesChartType.Spline;
            chart1.Series.Add("Adams");
            chart1.Series["Adams"].Color = Color.Chocolate;
            chart1.Series["Adams"].ChartType = System.Windows.Forms.DataVisualization.Charting.SeriesChartType.Spline;
            chart1.Series["Adams"].BorderWidth = 2;
            Solver_Load(sender, e);
        }
    }
}
