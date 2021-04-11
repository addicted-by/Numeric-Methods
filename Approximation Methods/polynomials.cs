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
    public partial class Polynomials : Form
    {
        public Polynomials()
        {
            InitializeComponent();
        }
        //public List<double> X;
       // public double xfix;
        private void Polynomials_Load(object sender, EventArgs e)
        {
            chart1.Series["Series1"].LegendText = "f = sqrt(x)";
            chart1.Series["Series1"].Color = Color.Red;
            chart1.ChartAreas["ChartArea1"].AxisX.Title = "X";
            chart1.ChartAreas["ChartArea1"].AxisY.Title = "Y";
            chart1.ChartAreas["ChartArea1"].AxisX.ArrowStyle =
                System.Windows.Forms.DataVisualization.Charting.AxisArrowStyle.SharpTriangle;
            chart1.ChartAreas["ChartArea1"].AxisY.ArrowStyle = 
                System.Windows.Forms.DataVisualization.Charting.AxisArrowStyle.SharpTriangle;
            chart1.Series["Series1"].ChartType = System.Windows.Forms.DataVisualization.Charting.SeriesChartType.Spline;
            for (double i = 0; i < 20; i += 0.1)
            {
                chart1.Series["Series1"].Points.AddXY(i, Math.Sqrt(i));
            }


        }

        private void button1_Click(object sender, EventArgs e)
        {
            chart1.Series.Clear();
            chart1.Series.Add("Series1");
            for (double i = 0; i < 20; i += 0.1)
            {
                chart1.Series["Series1"].Points.AddXY(i, Math.Sqrt(i));
            }
            chart1.Series["Series1"].LegendText = "f = sqrt(x)";
            chart1.Series["Series1"].Color = Color.Red;
            chart1.ChartAreas["ChartArea1"].AxisX.Title = "X";
            chart1.ChartAreas["ChartArea1"].AxisY.Title = "Y";
            chart1.ChartAreas["ChartArea1"].AxisX.ArrowStyle =
                System.Windows.Forms.DataVisualization.Charting.AxisArrowStyle.SharpTriangle;
            chart1.ChartAreas["ChartArea1"].AxisY.ArrowStyle =
                System.Windows.Forms.DataVisualization.Charting.AxisArrowStyle.SharpTriangle;
            chart1.Series["Series1"].ChartType = System.Windows.Forms.DataVisualization.Charting.SeriesChartType.Spline;

            List<double> x = new List<double>();
            x.Add(Convert.ToDouble(textBox1.Text));
            x.Add(Double.Parse(textBox2.Text));
            x.Add(Convert.ToDouble(textBox3.Text));
            x.Add(Convert.ToDouble(textBox4.Text));
            double xfix;
            xfix = Convert.ToDouble(textBox5.Text);
            List<object> answer = Lagrange_Polynom(x, xfix);
            lagrange.Visible = true;
            Lagrange_label.Visible = true;
            lagrange.Text += Convert.ToString(answer[0]);
            chart1.Series.Add("Series2");
            chart1.Series["Series2"].LegendText = "Lagrange Polynom";
            chart1.Series["Series2"].Color = Color.Blue;
            chart1.Series["Series2"].ChartType = System.Windows.Forms.DataVisualization.Charting.SeriesChartType.Spline;
            for (double i = 0; i < 10; i += 0.1)
            {
                double tmp1 = Math.Sqrt(i);
                double tmp = (double)(Lagrange_Polynom(x, i)[1]);
               // chart1.Series["Series2"].Points.AddXY(i, tmp);
            }
            Newton_label.Visible = true;
            newton.Visible = true;
            List<object> answer_newton = Newton(x, xfix);
            newton.Text += Convert.ToString(answer_newton[0]);
            chart1.Series.Add("Series3");
            chart1.Series["Series3"].LegendText = "Newton Polynom";
            chart1.Series["Series3"].Color = Color.Green;
            chart1.Series["Series3"].ChartType = System.Windows.Forms.DataVisualization.Charting.SeriesChartType.Spline;
            for (double i = 0; i < 10; i += 0.1)
            {
                double tmp = (double)(Newton(x, i)[1]);
                chart1.Series["Series3"].BorderWidth = 2;

                chart1.Series["Series3"].Points.AddXY(i, tmp);
            }
            label5.Text += "Error Lagrange " + (string)answer[2];
            label7.Text += "Error Newton " + Convert.ToString((double)answer_newton[2]);

        }
        #region(Lab1. Lagrange and Newton Polynoms)
        #region(Lagrange)
        public List<object> Lagrange_Polynom(List<double> x, double xfix)
        {
            List<object> answer = new List<object>();
            double l = 0d;
            double tmp;
            string str = "";
            string tmp_string;
            double coef;
            for (int i = 0; i < x.Count; i++)
            {
                tmp = 1;
                coef = 1;
                tmp_string = " ";
                for (int j = 0; j < x.Count; j++)
                {
                    if (i != j)
                    {
                        coef /= (x[i] - x[j]);
                        tmp *= (xfix - x[j]);
                        tmp_string += ("(x - " + Convert.ToString(x[j]) + ")");
                    }
                }
                coef *= Math.Sqrt(x[i]);
                if (Math.Sign(coef) == 1)
                    str += (Convert.ToString(coef) + tmp_string + "+");
                else
                {
                    if (str.Length > 1)
                        str.Remove(str.Length - 2, 1);
                    str += (" - " + (Convert.ToString(-coef) + tmp_string + "+"));

                }
                l += tmp * coef;
            }
            str.Remove(str.Length - 1, 1);
            answer.Add(str);
            answer.Add(l);
            string error = Convert.ToString(Math.Abs(Math.Sqrt(xfix) - l));
            answer.Add(error);
            return answer;
        }

        #endregion

        #region(Newton)
        public List<object> Newton(List<double> x, double xfix)
        {
            List<object> answer = new List<object>();
            double p = Math.Sqrt(x[0]);
            double l = 0d;
            double tmp;
            string str = "";
            string tmp_string;
            double coef;
            double tmp2; 
            for (int i = 0; i < x.Count-1; i++)
            {
                tmp_string = "";
                tmp = 1;
                List<double> tmp_list = new List<double>();
                for (int j = 0; j <= i; j++)
                {
                    tmp_string += ("(x - " + x[j].ToString() + ")");
                    tmp *= xfix - x[j];
                    tmp_list.Add(x[j]);
                }
                tmp_list.Add(x[i + 1]);
                tmp2 =  (tmp_list.Count > 1) ? f(tmp_list) : Math.Sqrt(tmp_list[0]);
                p += tmp * tmp2;
                //f(tmp_list) 1 раз
                if (Math.Sign(tmp2) == 1)
                {
                    str += "+" + Convert.ToString(tmp2) + tmp_string;
                }
                else
                    str += "-" + Convert.ToString(-tmp2) + tmp_string;
            }
            answer.Add(str);
            answer.Add(p);
            answer.Add(Math.Abs(p-Math.Sqrt(xfix))); //вывести ошибку на экран
            return answer;


        }
        public double f(List<double> x)
        {
            if (x.Count == 2)
                return (Math.Sqrt(x[0]) - Math.Sqrt(x[1])) / (x[0] - x[1]);
            List<double> tmp1 = new List<double>(), tmp2 = new List<double>();
            tmp1.Add(x[0]);
            for (int i = 1; i < x.Count - 1; i++)
            {
                tmp1.Add(x[i]);
                tmp2.Add(x[i]);
            }
            tmp2.Add(x[x.Count - 1]);
            return (f(tmp1) - f(tmp2)) / (x[0] - x[x.Count - 1]);

 
        }

        #endregion
#endregion
    }
}
