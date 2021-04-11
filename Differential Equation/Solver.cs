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
            chart1.Series["Series1"].ChartType = System.Windows.Forms.DataVisualization.Charting.SeriesChartType.Spline;
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

            double a = 0;
            double b = 1;
            double h = Convert.ToDouble(h_data.Text);
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
            //ошибка
            answer.Add(y);
            answer.Add(z);
            return answer;
        }
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
    }
}
