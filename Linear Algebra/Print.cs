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
            Console.WriteLine("Solving SLAE with LU decomposition" + "\n -----------------------");
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
            Console.WriteLine("\nTridiagonal Matrix Algorithm" + "\n -----------------------");
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
    }
}
