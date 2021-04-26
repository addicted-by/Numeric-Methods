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

            //List<object> answer1 = FixedPointIteration_forSystem();
            print.Print_2_3(FixedPointIteration_forSystem());
            print.Print_2_4(Newton_forSystem());
            
        }
        #region(Lab1: Solving Nonlinear Equations)
        #region(Fixed Point Iteration Method)
        static public List<double> FixedPointIteration()
        {
            double a = 0, b = 1, x_prev = (b - a) / 2 + a;
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
            double q = getq(a,b, max);
            Console.WriteLine(q + "\n");
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
            double a = 0, b = 1, x_prev = 0;
            double varepsilon = 0.00002;
            int count = 0;
            double x = 1;
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
            Math.Pow(2,x) * Math.Log(2) + 2*x;
        static public double Phi(double x, double max) =>
           x - Math.Sign(Derivation(x)) / max * f(x);

        static public double dphi(double x, double max) =>
           Math.Sign(Derivation(x)) / max;
        static public double getq(double a, double b, double max) =>
            Math.Max(Math.Abs(dphi(a, max)), Math.Abs(dphi(b, max)));
        // 
        #endregion
        static public double get_q_system(double x1, double x2) 
        {
            double max_phi1 = (Math.Abs(derphi_12(x1, x2)));
            double max_phi2 = (Math.Abs(derphi21(x1, x2)));
            return Math.Max(max_phi1, max_phi1);
        }
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
            double q = get_q_system(xprev[0,0], xprev[1,0]);
            Console.WriteLine(q + "\n");
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
        //static public double f1(double x1, double x2) =>
        //  x1 * x1 + x2 * x2 - 4;
        static public double f1(double x1, double x2) => x1 - Math.Cos(x2) - 1;
        static public double f2(double x1, double x2) =>
            10* Math.Exp(x2) - (x1 + 1) - 10*Math.Exp(2);// x1 - Math.Exp(x2) + 2;
        static public double der11(double x1, double x2) =>
            1;//2 * x1;
        static public double der22(double x1, double x2) =>
            Math.Sin(x2);//-Math.Exp(x2);
        static public double der12(double x1, double x2) =>
            -1 / ((x1+1)*Math.Log(10));//2 * x2;
        static public double der21(double x1, double x2) =>
            1;
        static public double derphi_sec(double x1, double x2) => 0;
        static public double derphi_12(double x1, double x2) =>
            -x2 / Math.Sqrt((4 - x2 * x2));
        static public double derphi21(double x1, double x2) =>
            1 / (x1 + 2);
        static public Matrix Jacobi(double x1, double x2)
        {
            Matrix answer = new Matrix(2);
            answer[0, 0] = der11(x1, x2);
            answer[0, 1] = der12(x1, x2);
            answer[1, 0] = der21(x1, x2);
            answer[1, 1] = der22(x1, x2);
            double det = answer[1, 1] * answer[0, 0] -
                answer[0, 1] * answer[1, 0];
            return answer;
        }
        static public Matrix phi(Matrix x)
        {
            Matrix answer = new Matrix(2);
            answer[0, 0] = Math.Sqrt(4 - x[1, 0] * x[1,0]);
            answer[1, 0] = Math.Log(x[0, 0] + 2);
            return answer;
        }
        static public List<double> delta_x(double x1, double x2)
        {
            List<double> answer = new List<double>();
            Matrix A = Jacobi(x1, x2);
            List<double> RP = new List<double>() { -f1(x1, x2), -f2(x1, x2) };
            List<object> solution = A.Seidel(RP);
            answer = (List<double>)solution[1];
            return answer;
        }
        #region(Newton)
        static public List<object> Newton_forSystem()
        {
            List<object> answer = new List<object>();
            List<double> xprev = new List<double>();
            xprev.Add(2);
            xprev.Add(1);
            double varepsilon = 0.001;
            double q = 0.1;
            int count = 0;
            double coef = q / (1 - q);
            Matrix tmp = new Matrix();
            List<double> delta = delta_x(xprev[0], xprev[1]);
            List<double> x = new List<double>();
            for (int i = 0; i < xprev.Count; i++)
                x.Add(xprev[i] + delta[i]);
            List<double> diff = new List<double>();
            List<double> tmp1 = delta_x(xprev[0], xprev[1]);

            for (int i = 0; i < x.Count; i++)
            {
                diff.Add(x[i] - xprev[i]);
            }
            while ( tmp.Vecnorm(tmp1) > varepsilon)
            {
                for (int i = 0; i < x.Count; i++)
                    xprev[i] = x[i];
                tmp1 = delta_x(xprev[0], xprev[1]);
                for (int i = 0; i < x.Count; i++)
                    x[i] += tmp1[i];
                count++;
                diff.Clear();
                for (int i = 0; i < x.Count; i++)
                {
                    diff.Add(x[i] - xprev[i]);
                }
            }
            answer.Add(x);
            answer.Add(count);
            return answer;
        }/*
        static public Matrix phi_newton(Matrix x) =>
            x - (Jacobi(x[0, 1], x[1, 0]) * f_newton(x));*/
       /* static public Matrix f_newton(Matrix x)
        {
            Matrix answer = new Matrix(2);
            answer[0, 0] = f1(x[0, 0], x[1, 0]);
            answer[1, 0] = f2(x[0, 0], x[1, 0]);
            return answer;

        }*/
        #endregion
        #endregion
    }
}
