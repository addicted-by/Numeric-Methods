using System;
using System.Collections.Generic;
namespace Course_Work__SVD_
{
    class Program
    {
        static void Main(string[] args)
        {
            Tensor a = new Tensor(new List<double>() { 2, 2, 1, 1,1,2 }, 3, 2);
            //Tensor b = new Tensor(new List<double>() { 2, 3 }, 2, 1);
            //Tensor a = new Tensor(new List<double>() { 1, 2, 3, 4, 5, 6 }, 3, 2);
            Console.WriteLine(a.ToString());
            List<Object> answer = a.SVD();
            Tensor U = (Tensor)answer[0];
            Tensor V = (Tensor)answer[1];
            Tensor Sigma = (Tensor)answer[2];
            //List<double> sigma = (List<double>)answer[2];
            Console.WriteLine(U.ToString() + " " + V.ToString());
            Console.WriteLine(Sigma.ToString());
            //Console.Write((Tensor)(answer[0]).ToString() + " " + answer[2].ToString() + " " + answer[1].ToString());
            Console.WriteLine(a.Rank());
            Tensor sigma = (Tensor)answer[2];
            Tensor new_last = U * Sigma;
            Tensor tmp = new_last * V.Tranpose();
            Console.WriteLine("Check" + tmp.ToString());
            //Console.WriteLine(b.ToString());
        //    Console.WriteLine((a * b).ToString());
        }
    }
}
