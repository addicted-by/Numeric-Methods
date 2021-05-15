using System;
using System.Collections.Generic;
namespace Linear_Algebra
{
    class Program
    {
        

        static void Main(string[] args)
        {
            #region(Lab1.1)
            string[] _lines_1 = System.IO.File.ReadAllLines(@"test1.1.txt");
            List<double> Matrix_list = new List<double>();
            List<double> RP_list = new List<double>();
            
            foreach (string x in _lines_1[0].Split())
            {
                Matrix_list.Add(Convert.ToInt32(x));
            }
            foreach (string x in _lines_1[1].Split())
            {
                RP_list.Add(Convert.ToInt32(x));
            }
            Print print = new Print();          
            Matrix m1 = new Matrix(Matrix_list);
            Matrix m1_copy = Matrix.EMatrix();
            m1_copy *= m1;
            //Console.WriteLine(m1.LeaderMustBeFirst(RP_list)[0].ToString());
            /*List<object> good = m1.LeaderMustBeFirst(RP_list);
            m1 = (Matrix)good[0];
            RP_list = (List<double>)good[1];
            */Matrix Inversed = new Matrix();
            /*foreach (double x in RP_list)
            {
                Console.WriteLine(x);
            }*/

            List<Matrix> LU = (List<Matrix>)m1.LUDec(RP_list)[0];
            RP_list = (List<double>)m1.LUDec(RP_list)[1];
            // double f = m1.Determinant;
            double z = m1.Determinant;
            
            //Console.WriteLine(Inversed.ToString());
            List<double> solution = m1.SOLVE(LU[0], LU[1], RP_list);
            Matrix U_copy = Matrix.EMatrix();
            U_copy *= LU[1];
            Matrix L_copy = Matrix.EMatrix();
            L_copy *= LU[0];
            Matrix A_new = L_copy * U_copy;
            Console.WriteLine(m1_copy.ToString());
            //Console.WriteLine(A_new.ToString());
            Matrix A_inversed = m1_copy.Inversion(L_copy, U_copy);
            Matrix mustbeIdentity = m1_copy * A_inversed;
            Console.WriteLine("Inversed:\nA^(-1)\n" + A_inversed.ToString());
            Console.WriteLine("AA^(-1):\n" + mustbeIdentity.ToString());
            Inversed = (U_copy.Inverse() * L_copy.Inverse());
            //Console.WriteLine(Inversed.ToString());
            Console.WriteLine(Inversed.ToString());
            /*Console.WriteLine("A\n" + m1_copy.ToString());
            Console.WriteLine("LU\n" + A_new.ToString());
            
            print.Task1_1Print(Matrix_list, RP_list, LU[0], LU[1], solution, z);
            */
            print.Task1_1Print(Matrix_list, RP_list, LU[0], LU[1], solution, z);
            #endregion
            #region(Lab1.2)
            string[] _lines_2 = System.IO.File.ReadAllLines(@"test1.2.txt");
            List<double> a = new List<double>();
            List<double> b = new List<double>();
            List<double> c = new List<double>();
            List<double> d = new List<double>();
            int n = 0;
            foreach (string x in _lines_2[0].Split())
            {
                n = Convert.ToInt32(x);
            }
            //var n = Convert.ToInt32(_lines_2[0].Split());
            foreach (string x in _lines_2[1].Split())
            {
                a.Add(Convert.ToInt32(x));
            }
            foreach (string x in _lines_2[2].Split())
            {
                b.Add(Convert.ToInt32(x));
            }
            foreach (string x in _lines_2[3].Split())
            {
                c.Add(Convert.ToInt32(x));
            }
            foreach (string x in _lines_2[4].Split())
            {
                d.Add(Convert.ToInt32(x));
            }
            a.Insert(0, 0f);
            c.Add(0f);
            List<double> solution_12 = TMA(n,a,b,c,d);
            print.Task1_2Print(a,b,c,d, solution_12);
            #endregion
            
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
                P.Add(-c[i] / (a[i] * P[i-1] + b[i]));
                Q.Add((d[i] - a[i] * Q[i - 1]) / (b[i] + a[i]*P[i-1]));
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
