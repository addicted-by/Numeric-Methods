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
    public partial class Integral : Form
    {
        public Integral()
        {
            InitializeComponent();
        }

        private void Integral_Load(object sender, EventArgs e)
        {
            List<double> steps = new List<double>();
            steps.Add(Convert.ToDouble(h1.Text));
            steps.Add(Convert.ToDouble(h2.Text));
            double x_0 = Convert.ToDouble(x0.Text), x = Convert.ToDouble(xk.Text);
            List<double> rec = new List<double>();
            List<double> trapeze = new List<double>();
            List<double> sim = new List<double>();
            foreach (double h in steps)
            {
                List<double> X = get_points(x_0, x, h);
                List<double> Y = get_values(X);
                
                rec.Add(rectangle_method(X, h));
                trapeze.Add(trapeze_method(Y, h));
                sim.Add(Simpson_method(X, h));
            }
            Console.Text = "Rectangle method (step = " + steps[0] + 
                " ): " + rec[0] + "\n";
            Console.Text += "Rectangle method (step = " + steps[1] +
                " ): " + rec[1] + "\n" + "--------------------------------------------------\n";
            Console.Text += "Trapeze method (step = " + steps[0] +
                " ): " + trapeze[0] + "\n";
            Console.Text += "Trapeze method (step = " + steps[1] +
                " ): " + trapeze[1] + "\n" + "--------------------------------------------------\n";
            Console.Text += "Simpson method (step = " + steps[0] +
                " ): " + sim[0] + "\n";
            Console.Text += "Simpson method (step = " + steps[1] +
                " ): " + sim[1] + "\n" + "--------------------------------------------------\n";
            Console.Text = Runge_Romberg_Rithardson(steps, rec, trapeze, sim, Console.Text);

        }
        public double f(double x) =>
            1 / (3 * x * x + 4 * x + 2);
        public List<double> get_points(double x0, double x, double h)
        {
            List<double> answer = new List<double>();
            for (double i = x0; i < x + h; i += h)
            {
                answer.Add(i);
            }
            return answer;
        }
        public List<double> get_values(List<double> x)
        {
            List<double> answer = new List<double>();
            foreach (double i in x)
                answer.Add(f(i));
            return answer;
        }
        public double rectangle_method(List<double> x, double h)
        {
            double sum = 0;
            for (int i = 0; i < x.Count - 1; i++)
                sum += f((x[i + 1] + x[i]) / 2);
            return h * sum;
        }
        public double trapeze_method(List<double> y, double h)
        {
            int n = y.Count - 1;
            double sum = 0;
            for (int i = 1; i < n -1; i++)
            {
                sum += y[i];
            }
            return h * (y[0] / 2 + sum + y[n]);
        }
        public double Simpson_method(List<double> y, double h)
        {
            int n = y.Count - 1;
            double sum = 0;
            for (int i = 1; i < n ; i++)
            {
                sum += f(y[i - 1]) + 4 * f((y[i - 1] + y[i]) / 2f) + f(y[i]);
            }
            return sum * h / 6;
        }

        public string Runge_Romberg_Rithardson(List<double> steps, List<double> rec, List<double> trapeze, List<double> sim, string error_end)
        {
            List<string> answer = new List<string>();
            error_end += "\nErrors: \n";
            double k = steps[0] / steps[1];
            double analitic_solution = 1.8574186872187473;
            List<double> err_rec = new List<double>();
            err_rec.Add(Math.Abs(rec[0] - rec[1]) / (k * k - 1));
            err_rec.Add(Math.Abs(rec[0] - analitic_solution) / (k * k - 1));
            List<double> err_trapeze = new List<double>();
            err_trapeze.Add(Math.Abs(trapeze[0] - trapeze[1]) / (k * k - 1));
            err_trapeze.Add(Math.Abs(trapeze[0] - analitic_solution) / (k * k - 1));
            List<double> err_sim = new List<double>();
            err_sim.Add(Math.Abs(sim[0] - sim[1]) / (Math.Pow(k, 4) - 1));
            err_sim.Add(Math.Abs(sim[0] - analitic_solution) / (Math.Pow(k, 4) - 1));
            error_end += "\nRectangle error: " +
                err_rec[0].ToString() + " || " + err_rec[1].ToString() + "\n";
            error_end += "Trapeze error: " +
                err_trapeze[0].ToString() + " || " + err_trapeze[1].ToString() + "\n";
            error_end += "Simpson error: " +
                err_sim[0].ToString() + " || " + err_sim[1].ToString() + "\n";
            return error_end; 

        }
    }
}
