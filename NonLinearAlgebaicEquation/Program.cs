using System;
using System.Collections.Generic;
using Linear_Algebra;
namespace Nonlinear_Algebra
{
    class Program
    {
        static void Main(string[] args)
        {
            Print print = new Print();
            print.Print_2_1(FixedPointIteration());
            print.Print_2_2(Newton());
            // List<object> answer = FixedPointIteration_forSystem();
            List<object> answer = Newton_forSystem();
            Matrix x = (Matrix)answer[0];
            List<double> last = new List<double>();
            last.Add(x[0, 0]);
            last.Add(x[1, 0]);
            Console.WriteLine(x.ToString());
            Console.WriteLine((int)answer[1]);
        }
        #region(Lab1: Solving Nonlinear Equations)
        #region(Fixed Point Iteration Method)
        static public List<double> FixedPointIteration()
        {
            double a = -2, b = 0, x_prev = (b - a) / 2 + a;
            double varepsilon = 0.00000000000002;
            int count = 0;
            double max = Math.Abs(Derivation(a));
            double tmp;
            double x = a + 0.0001;
            List<double> answer = new List<double>();
            while (x <= b)
            {
                tmp = Math.Abs(Derivation(x));
                if (tmp > max)
                    max = tmp;
                x += 0.0001;
            }
            x = Phi(x_prev, max);
            double q = 0.01;
            while (q / (1 - q) * Math.Abs(x - x_prev) > varepsilon)
            {
                count++;
                x_prev = x;
                x = Phi(x, max);
            }
            answer.Add(x);
            answer.Add(count);
            answer.Add(f(x));
            return answer;
        }
        #endregion
        #region(Newton Method)
        static public List<double> Newton()
        {
            List<double> answer = new List<double>();
            double a = -2, b = 0, x_prev = 0;
            double varepsilon = 0.00002;
            int count = 0;
            double x = (b - a) / 2 + a;
            while (Math.Abs(x - x_prev) > varepsilon)
            {
                count++;
                x_prev = x;
                x -= f(x) / Derivation(x);
            }
            answer.Add(x);
            answer.Add(count);
            answer.Add(f(x));
            return answer;
        }
        #endregion
        #region(Function and work functions)
        static public double f(double x) =>
            Math.Pow(2, x) + x * x - 2;
        static public double Derivation(double x) =>
            (f(x + 0.0001) - f(x - 0.0001)) / 0.0002;
        static public double Phi(double x, double max) =>
            x - Math.Sign(Derivation(x)) / max * f(x);
        #endregion
        #endregion
        #region(Lab2: Solving Systems of nonlinear equations)
        #region(Fixed Point Iteration Method)
        static public List<object> FixedPointIteration_forSystem()
        {
            List<object> answer = new List<object>();
            Matrix xprev = new Matrix(2);
            xprev[0, 0] = 1;
            xprev[1, 0] = 1;
            double varepsilon = 0.00001;
            double q = 0.1;
            int count = 0;
            double coef = q / (1 - q);
            Matrix x = phi(xprev);
            while (coef * (x - xprev).norm_2() > varepsilon)
            {
                xprev = x;
                x = phi(x);
                count++;
            }
            answer.Add(x);
            answer.Add(count);
            return answer;
        }
        #endregion
        static public double f1(double x1, double x2) =>
            x1 * x1 + x2 * x2 - 4;
        static public double f2(double x1, double x2) =>
            x1 - Math.Exp(x2) + 2;
        static public double der11(double x1, double x2) =>
            (f1(x1 + 0.01, x2) - f1(x1 - 0.01, x2)) / 0.02;
        static public double der22(double x1, double x2) =>
            (f2(x1, x2 + 0.01) - f2(x1, x2 - 0.02)) / 0.02;
        static public double der12(double x1, double x2) =>
            (f1(x1, x2 + 0.01) - f1(x1, x2 - 0.01)) / 0.02;
        static public double der21(double x1, double x2) =>
            (f2(x1 + 0.01, x2) - f2(x1 - 0.02, x2)) / 0.02;
        static public Matrix Jacobi(double x1, double x2)
        {
            Matrix answer = new Matrix(2);
            answer[0, 0] = der22(x1, x2);
            answer[0, 1] = -der12(x1, x2);
            answer[1, 0] = -der21(x1, x2);
            answer[1, 1] = der11(x1, x2);
            double det = answer[1, 1] * answer[0, 0] -
                answer[0, 1] * answer[1, 0];
            return answer * (1 / det);
        }
        static public Matrix phi(Matrix x)
        {
            Matrix answer = new Matrix(2);
            answer[0, 0] = Math.Sqrt(4 - x[1, 0]);
            answer[1, 0] = Math.Log(x[0, 0] + 2);
            return answer;
        }
        #region(Newton)
        static public List<object> Newton_forSystem()
        {
            List<object> answer = new List<object>();
            Matrix xprev = new Matrix(2);
            xprev[0, 0] = 2;
            xprev[1, 0] = 1;
            double varepsilon = 0.000000000000001;
            double q = 0.1;
            int count = 0;
            double coef = q / (1 - q);
            Matrix x = phi_newton(xprev);
            while (coef * (x - xprev).norm_2() > varepsilon)
            {
                xprev = x;
                x = phi(x);
                count++;
            }
            answer.Add(x);
            answer.Add(count);
            return answer;
        }
        static public Matrix phi_newton(Matrix x) =>
            x - (Jacobi(x[0, 1], x[1, 0]) * f_newton(x));
        static public Matrix f_newton(Matrix x)
        {
            Matrix answer = new Matrix(2);
            answer[0, 0] = f1(x[0, 0], x[1, 0]);
            answer[1, 0] = f2(x[0, 0], x[1, 0]);
            return answer;

        }
        #endregion
        #endregion
    }
}
