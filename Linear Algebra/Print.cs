using System;
using System.Collections.Generic;
using System.Text;

namespace Linear_Algebra
{
    class Print
    {
        #region (Write 1.1 Task)
        public void Task1_1Print(List<double> Matrix_list, List<double> RP_list, Matrix L, Matrix U, List<double> solution, double det)
        {
            int i = 1, j = 0;
            Console.WriteLine("Lab1. Solving SLAE with LU decomposition" + "\n -----------------------");
            Console.WriteLine("System of Linear Equations" + "\n -----------------------");

            foreach (double x in Matrix_list)
            {
                if (i < 4)
                {
                    Console.Write("(" + Convert.ToString(x) + ")*x" + Convert.ToString(i) + "+");
                    i++;
                }
                else
                {
                    Console.Write("(" + Convert.ToString(x) + ")*x" + Convert.ToString(i) + "=" + Convert.ToString(RP_list[j]));
                    Console.WriteLine("\n");
                    i = 1;
                    j++;
                }
            }
            Console.WriteLine("LU-decomposition: \n");
            Console.WriteLine("Matrix L \n" + L.ToString() + "\n");
            Console.WriteLine("-----------------------");
            Console.WriteLine("Matrix U \n" + U.ToString());
            Console.WriteLine("------------------------" + "\nSolution:");
            i = 0;
            foreach (double x in solution)
            {
                Console.WriteLine("x"+ Convert.ToString(i+1)+" = "+Convert.ToString(x));
                i++;
            }
            Console.WriteLine("------------------------" + "\nDeterminant:");
            Console.WriteLine("|A| = "  + Convert.ToString(det));

        }
        #endregion
        #region(Write 1.2 Task)
        public void Task1_2Print(List<double> a, List<double> b, List<double> c, List<double> d, List<double> solution)
        {
            int i = 1;
            Console.WriteLine("\nLab 2. Tridiagonal Matrix Algorithm" + "\n -----------------------");
            foreach (double x in a)
            {
                Console.Write("a" + Convert.ToString(i) + " = " + Convert.ToString(x) + " ");
                i++;
            }
            Console.WriteLine("");
            i = 1;
            foreach (double x in b)
            {
                Console.Write("b" + Convert.ToString(i) + " = " + Convert.ToString(x) + " ");
                i++;
            }
            i = 1;
            Console.WriteLine("");
            foreach (double x in c)
            {
                Console.Write("c" + Convert.ToString(i) + " = " + Convert.ToString(x) + " ");
                i++;
            }
            i = 1;
            Console.WriteLine("");
            foreach (double x in d)
            {
                Console.Write("d" + Convert.ToString(i) + " = " + Convert.ToString(x) + " ");
                i++;
            }
            Console.WriteLine("\nAnswer:" + "\n -----------------------");

            i = 0;
            foreach (double x in solution)
            {
                Console.WriteLine("x" + Convert.ToString(i + 1) + " = " + Convert.ToString(x));
                i++;
            }
        }
        #endregion
        #region(Write 1.3 Task)

        public void Task1_3Print(List<double> Matrix_list, List<double> RP_list, List<double> answer1, int count1, List<double> answer2, int count2)
        {
            Console.WriteLine("Lab3. Simple iteration and Seidel Methods. \n---------------------- \nSystem of Linear Equations" + "\n -----------------------");
            int i = 1, j = 0;
            foreach (double x in Matrix_list)
            {
                if (i < 4)
                {
                    Console.Write("(" + Convert.ToString(x) + ")*x" + Convert.ToString(i) + "+");
                    i++;
                }
                else
                {
                    Console.Write("(" + Convert.ToString(x) + ")*x" + Convert.ToString(i) + "=" + Convert.ToString(RP_list[j]));
                    Console.WriteLine("\n");
                    i = 1;
                    j++;
                }
            }
            Console.WriteLine("Simple Iteration Method" + "\n -----------------------");
            i = 1;
            foreach (double x in answer1)
            {
                Console.Write("x" + Convert.ToString(i) + " = " + Convert.ToString(x) + "\n");
                i++;
            }
            Console.WriteLine("\nIterations: " + Convert.ToString(count1));

            Console.WriteLine("\nSeidel Method" + "\n -----------------------");

            i = 1;
            foreach (double x in answer2)
            {
                Console.Write("x" + Convert.ToString(i) + " = " + Convert.ToString(x) + "\n");
                i++;
            }
            Console.WriteLine("\nIterations: " + Convert.ToString(count2));
        }
        #endregion
        #region(Write 1.4 Task)
        public void Task1_4Print(Matrix a, int n, List<object> answer)
        {
            Console.WriteLine("\nLab4 Rotation Method\n---------------------- \nMatrix:\n");
            Console.WriteLine(a.ToString());
            Console.WriteLine("----------------------\nEigen Values\n");
            for (int i = 0; i < n; i++)
            {
                Console.WriteLine("lambda" + Convert.ToString(i+1) + " = " + ((Matrix)(answer[0]))[i,i]);
            }
            Console.WriteLine("\nIterations: " + Convert.ToString(answer[1]));
            Console.WriteLine("Eigen vectors: \n\n" + ((Matrix)answer[2]).ToString());
        }
        #endregion
    }
}
