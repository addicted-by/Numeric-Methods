using System;
using System.Collections.Generic;
using System.Text;
using Linear_Algebra;
namespace Nonlinear_Algebra
{
    class Print
    {

        #region(Lab1)
        public void Print_2_1(List<double> answer)
        {
            Console.WriteLine("Lab1: Fixed Point Iteration Method"
                + "\n===========================================");
            Console.WriteLine("Root x = " + answer[0].ToString());
            Console.WriteLine("Function's value in root = " + answer[2].ToString());
            Console.WriteLine("Iterations:" + answer[1].ToString());
            Console.WriteLine("===========================================\n");

        }
        public void Print_2_2(List<double> answer)
        {
            Console.WriteLine("Lab1: Newton Method"
                + "\n===========================================");
            Console.WriteLine("Root x = " + answer[0].ToString());
            Console.WriteLine("Function's value in root = " + answer[2].ToString());
            Console.WriteLine("Iterations:" + answer[1].ToString());
            Console.WriteLine("===========================================\n");
        }
        #endregion
        public void Print_2_3(List<object> answer)
        {
            Matrix x1 = (Matrix)answer[0];
            Console.WriteLine("Lab2: Fixed Point Iteration Method for system"
                + "\n===========================================");

            Console.WriteLine("Root x1 = " + x1[0, 0].ToString());
            Console.WriteLine("Root x2 = " + x1[1, 0].ToString());
            Console.WriteLine("Iterations: "+((int)answer[1]).ToString());
            Console.WriteLine("===========================================\n");

        }
        public void Print_2_4(List<object> answer)
        {
            List<double> x2 = (List<double>)answer[0];
            Console.WriteLine("Lab2: Newton Method for system"
                + "\n===========================================");
            Console.WriteLine("Root x1 = " + x2[0].ToString());
            Console.WriteLine("Root x2 = "+x2[1].ToString());
            Console.WriteLine("Iterations: " + ((int)answer[1]).ToString());
            Console.WriteLine("===========================================\n");

        }
    }
}
