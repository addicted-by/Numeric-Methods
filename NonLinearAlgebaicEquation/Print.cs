using System;
using System.Collections.Generic;
using System.Text;

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
            Console.WriteLine("\n===========================================");

        }
        public void Print_2_2(List<double> answer)
        {
            Console.WriteLine("Lab1: Newton Method"
                + "\n===========================================");
            Console.WriteLine("Root x = " + answer[0].ToString());
            Console.WriteLine("Function's value in root = " + answer[2].ToString());
            Console.WriteLine("Iterations:" + answer[1].ToString());
            Console.WriteLine("\n===========================================");
        }
        #endregion
    }
}
