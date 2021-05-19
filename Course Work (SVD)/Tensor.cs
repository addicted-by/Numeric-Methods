using System;
using System.Collections.Generic;
using System.Text;

namespace Course_Work__SVD_
{
    class Tensor
    {
        #region(For easily writing matrix)
        public override string ToString()
        {
            string output = "";
            if (_Dimension1 < 7 || _Dimension2 < 7)
            {
                for (int i = 0; i < _Dimension1; i++)
                {
                    for (int j = 0; j < _Dimension2; j++)
                        output += $"|  {_data[i, j]}  |";
                    output += '\n';
                }
            }
            else
            {
                for (int i = 0; i < 7; i++)
                {
                    for (int j = 0; j < 7; j++)
                        output += $"|  {_data[i, j]}  |";
                    output += '\n';
                }
            }
            return output;
        }
        #endregion

        #region(Dimensions)
        public int _Dimension1; //rows
        public int _Dimension2; //columns
        private double[,] _data;
        public int Rows
        {
            get => _Dimension1;
            set
            {
                _Dimension1 = value;
            }

        }
        public int Columns
        {
            get => _Dimension2;
            set
            {
                _Dimension2 = value;
            }

        }
        #endregion
        #region(For easily use of elements)
        public double this[int row, int column]
        {
            get =>
                (row < _Dimension1 && column < _Dimension2) ?
                    _data[row, column] : throw new IndexOutOfRangeException();

            set => _data[row, column] = value;
        }
        #endregion

        //#region(Determinant)
        /* public double Determinant
         {
             get
             {
                 double det = 1;
                 Tensor M = new Tensor();
                 for (int i = 0; i < _Dimension; i++)
                     for (int j = 0; j < _Dimension; j++)
                         M[i, j] = _data[i, j];
                 for (int i = 0; i < _Dimension; ++i)
                 {
                     int j = i;
                     while (j < _Dimension && M[j, i] == 0)
                         ++j;
                     if (j == _Dimension)
                         return 0;
                     if (j != i)
                     {
                         det *= -1;
                         Changerows(M, i, j);
                     }
                     double leader = M[i, i];
                     for (int k = i; k < _Dimension; ++k)
                     {
                         M[i, k] /= leader;
                     }
                     det *= leader;
                     for (int k = i + 1; k < _Dimension; ++k)
                     {
                         leader = M[k, i];
                         for (j = i; j < _Dimension; ++j)
                         {
                             M[k, j] -= M[i, j] * leader;
                         }
                     }
                 }
                 return det;
             }
         }
        */ //add later
        #region(Operation with matrices)
        /*public Tensor Inverse()
        {
            //if (Determinant.Equals(0)) throw new ArithmeticException();
            Tensor a = new Tensor(_data);
            Tensor output = Tensor.I();
            for (int i = 0; i < _Dimension1; ++i)
            {
                int j = i;
                while (a[j, i] == 0)
                    ++j;
                if (i != j)
                {
                    Changerows(a, i, j);
                    Changerows(output, i, j);
                }
                double leader = a[i, i];
                for (j = 0; j < _Dimension2; ++j)
                {
                    a[i, j] /= leader;
                    output[i, j] /= leader;
                }
                for (int k = i + 1; k < _Dimension; ++k)
                {
                    leader = a[k, i];
                    for (j = 0; j < _Dimension; ++j)
                    {
                        a[k, j] -= a[i, j] * leader;
                        output[k, j] -= output[i, j] * leader;
                    }
                }
            }

            for (int i = (int)_Dimension - 1; i >= 0; --i)
            {
                for (int k = i - 1; k >= 0; --k)
                {
                    double leader = a[k, i];
                    for (int j = 0; j < _Dimension; ++j)
                    {
                        a[k, j] -= a[i, j] * leader;
                        output[k, j] -= output[i, j] * leader;
                    }
                }
            }
            return output;
        }
*/
        public int Rank()
        {
            int rank = Math.Max(Rows, Columns);
            Tensor a = new Tensor(Rows, Columns);
            a = a + this;
            List<bool> line_used = new List<bool>();
            for (int i = 0; i < Rows; i++)
                line_used.Add(false);
            for (int i = 0; i < Columns; ++i)
            {
                int j;
                for (j = 0; j < Rows; ++j)
                    if (!line_used[j] && Math.Abs(a[j, i]) > 0.0001)
                        break;
                if (j == Rows)
                {
                    rank = rank - 1;
                }

                else
                {
                    line_used[j] = true;
                    for (int p = i + 1; p < Columns; ++p)
                        a[j, p] /= a[j, i];
                    for (int k = 0; k < Rows; ++k)
                    {
                        if (k != j && Math.Abs(a[k, i]) > 0.0001)
                            for (int p = i + 1; p < Columns; ++p)
                                a[k, p] -= a[j, p] * a[k, i];
                    }

                }

            }
            return rank;
        }
        private void Changerows(Tensor a, int i, int j)
        {
            for (int k = 0; k < _Dimension2; k++)
            {
                double t = a[i, k];
                a[i, k] = a[j, k];
                a[j, k] = t;
            }
        }

        public static Tensor operator +(Tensor a, Tensor b)
        {
            if (a.Rows == b.Rows && a.Columns == b.Columns)
            {
                int dimension1 = a.Rows;
                int dimension2 = a.Columns;
                Tensor output = new Tensor(dimension1, dimension2);
                for (int i = 0; i < dimension1; i++)
                    for (int j = 0; j < dimension2; j++)
                        output[i, j] = a[i, j] + b[i, j];
                return output;
            }
            else return new Tensor();
        }

        public static Tensor operator ++(Tensor a)
        {
            int dimension = Math.Min(a.Rows, a.Columns);
            for (int i = 0; i < dimension; i++)
                a[i, i]++;
            return a;
        }
        public static Tensor operator -(Tensor a)
        {
            int dimension1 = a.Rows;
            int dimension2 = a.Columns;
            for (int i = 0; i < dimension1; ++i)
                for (int j = 0; j < dimension2; ++j)
                    a[i, j] = -a[i, j];
            return a;
        }

        public static Tensor operator -(Tensor a, Tensor b) => a + (-b);

        public static Tensor operator --(Tensor a)
        {
            int dimension = Math.Min(a.Rows, a.Columns);
            for (int i = 0; i < dimension; i++)
                a[i, i]--;
            return a;
        }

        public static Tensor operator *(Tensor a, Tensor b)
        {
            if (a.Columns == b.Rows)
            {
                Tensor output = new Tensor(a.Rows, b.Columns);
                for (int k = 0; k < a.Rows; k++)
                    for (int i = 0; i < b.Columns; i++)
                    {
                        output[k, i] = 0f;
                        for (int j = 0; j < a.Columns; j++)
                            output[k, i] += a[k, j] * b[j, i];
                    }
                return output;
            }
            return new Tensor(a.Rows, a.Columns);
        }
        public static Tensor operator *(Tensor a, double b)
        {
            
            Tensor output = new Tensor(a.Rows, a.Columns);
            for (int i = 0; i < a.Rows; i++)
            {
                for (int j = 0; j < a.Columns; j++)
                    output[i, j] = a[i, j] * b;
            }
            return output;
        }
      /*  public static Tensor operator /(Tensor a, Tensor b)
        {
            return a * b.Inverse();
        }*/

/*        public static bool operator ==(Tensor a, Tensor b)
        {
            return a.Determinant == b.Determinant;
        }


        public static bool operator !=(Tensor a, Tensor b)
        {
            return !(a.Determinant == b.Determinant);
        }*/

 /*       public static bool operator >(Tensor a, Tensor b)
        {
            return a.Determinant > b.Determinant;
        }

        public static bool operator <(Tensor a, Tensor b)
        {
            return a.Determinant < b.Determinant;
        }
*/
        public Tensor Tranpose()
        {
            Tensor answer = new Tensor(Columns, Rows);
            double tmp;
            for (int i = 0; i < answer.Columns; i++)
            {
                for (int j = 0; j < answer.Rows; j++)
                {/*
                    tmp = answer[i, j];
                    answer[i, j] = answer[j, i];*/
                    answer[j, i] = this[i,j];
                }
            }
            return answer;
        }

        #endregion
        #region(Constructors)
        public Tensor(int rows = 4, int columns = 4, params double[] data) //[1,0,0,0,0,1,0,0,0,0,1,0,0,0,0,1]
        {
            this.Rows = rows;
            this.Columns = columns;
            this._data = new double[Rows, Columns];
            for (int i = 0; i < Rows; i++)
                for (int j = 0; j < Columns; j++)
                    this[i, j] = data[j + i * Rows];

        }

        public Tensor(List<double> data, int rows = 4, int columns = 4) //the same, but List
        {
            this.Rows = rows;
            this.Columns = columns;
            this._data = new double[Rows, Columns];
            for (int i = 0; i < Rows; i++)
                for (int j = 0; j < Columns; j++)
                    this[i, j] = data[j + i * Columns];
        }

        public Tensor(double[,] data, int rows = 4, int columns = 4) // {{1,0,0,0}, {0,1,0,0}, {0,0,1,0}, {0,0,0,1}}
        {
            this.Rows = rows;
            this.Columns = columns;
            _data = data;
        }

        public Tensor(double data, int rows = 4, int columns = 4) //zero matrix with non-zero diagonal
        {
            this.Rows = rows;
            this.Columns = columns;
            this._data = new double[Rows, Columns];
            for (int i = 0; i < Rows; i++)
                for (int j = 0; j < Columns; j++)
                    this[i, j] = (i == j) ? data : 0;
        }

        public Tensor(int rows = 4, int columns = 4) //zero matrix
        {
            this.Rows = rows;
            this.Columns = columns;
            this._data = new double[Rows, Columns];
            for (int i = 0; i < Rows; i++)
                for (int j = 0; j < Columns; j++)
                    this[i, j] = 0;
        }
        #endregion
        public double norm_2()
        {
            Tensor A = new Tensor(_data, Rows, Columns);
            double answer = 0;

            for (int i = 0; i < Rows; i++)
            {
                for (int j = 0; j < Columns; j++)
                {
                    answer += A[i, j] * A[i, j];
                }
            }
            return Math.Sqrt(answer);
        }
        #region(Identity matrix)
        public static Tensor I(int rows = 4, int columns = 4)
        {
            Tensor output = new Tensor(rows, columns);

            for (int i = 0; i < Math.Min(rows, columns); i++)
                output[i, i] = 1;
            return output;
        }
        #endregion
        
        #region(1. Iterative SVD)


        public List<Object> SVD()
        {
            List<Object> answer = new List<Object>();
            int rank_A = Rank();
            Tensor A = new Tensor(Rows,Columns);
            A = A + this;
            
            Tensor U = new Tensor(Rows, rank_A);
            Tensor V = new Tensor(Columns, rank_A);
            List<double> Sigma = new List<double>();
            Tensor sigma_t = new Tensor(rank_A, rank_A);
            double varepsilon = 0.01, delta = 0.001, lambda = 2;
            double iter = (int)(Math.Log(4*Math.Log(2*Columns / delta) / (varepsilon * delta) / (2*lambda)));
            
            
            for (int i = 0; i < rank_A; i++)
            {
                Tensor x0 = new Tensor(random_normal(this), Columns, 1);
                Tensor xi_vec = new Tensor();
                Tensor AtA = A.Tranpose();
                AtA *= A;
                for (int j = 0; j < iter; j++)
                {
                    xi_vec = AtA * x0;
                    for (int k = 0; k < x0.Rows; k++)
                        x0[k, 0] = xi_vec[k, 0];
                }
                Tensor v_i = new Tensor(Columns, 1);
                for (int k = 0; k < Columns; k++)
                {
                    V[k, i] = xi_vec[k, 0] / xi_vec.norm_2();
                    v_i[k, 0] = V[k, i];
                }

                Sigma.Add((A * v_i).norm_2());
                
                Tensor u_i = (A * v_i) * (1 / Sigma[i]);

                for (int k = 0; k < U.Rows; k++)
                {
                    U[k, i] = u_i[k, 0];
                }
                Tensor U_i = new Tensor(U.Rows, 1);
                Tensor V_i = new Tensor(V.Rows, 1);
                for (int h = 0; h < U.Rows; h++)
                    U_i[h, 0] = U[h, i];
                for (int h = 0; h < V.Rows; h++)
                    V_i[h, 0] = V[h, i];
                Tensor tmp_tra = new Tensor(V_i.Columns, V_i.Rows);
                tmp_tra = V_i.Tranpose();

                A = A - (U_i * tmp_tra) * Sigma[i];

            }
            answer.Add(U);
            answer.Add(V);
            for (int i = 0; i < rank_A; i++)
                sigma_t[i, i] = Sigma[i];
            answer.Add(sigma_t);
            return answer;
        }
        #endregion
        #region(SVD)
        public List<object> analitic_SVD()
        {
            List<object> answer = new List<object>();
            //A*A^T = U(SS^T)U^T
            //A^T*A = V(S^TS)V^T
            Tensor U = new Tensor(Rows, Rows);
            Tensor V = new Tensor(Columns, Columns);
            Tensor A = new Tensor(Rows, Columns);
            A += this;
            Tensor tmp_T = A.Tranpose() * A;
            /*List<object> x = tmp_T.Rotation();
            V = (Tensor)x[2];
            Tensor Sigma = new Tensor(Rows, Columns);
            Tensor tmp = (Tensor)x[0];
            for (int i = 0; i < Math.Min(Rows, Columns); i++)
                Sigma[i, i] = Math.Sqrt(tmp[i, i]);

            Console.WriteLine("Matrix V (analitic + rotation): \n " + V.ToString());
            tmp_T = A * A.Tranpose();
            x = tmp_T.Rotation();
            Console.WriteLine("Sigma: \n" + Sigma.ToString());
            U = (Tensor)x[2];
            Console.WriteLine("Matrix U (analitic + rotation): \n" + U.ToString());
            Tensor check = U * Sigma;
            Console.WriteLine(check.ToString());
            check = check * V.Tranpose();
            Console.WriteLine("Check:" + check.ToString());*/
            return answer;
        }
        #endregion
        #region (Normal distribution vector)
        public List<double> random_normal(Tensor A)
        {
            List<double> x = new List<double>();
            int M = 0; int sigma = 1;
            Random tmp = new Random();
            for (int i = 0; i < A.Columns; i++)
            {
                x.Add(f_normal(tmp.Next(5)));
            }
            return x;

        }
        
        public double f_normal(double x) =>
            1 / Math.Sqrt(2 * Math.PI) * Math.Exp(-(x * x) / 2);
        #endregion

        public double Vecnorm(List<double> x)
        {
            double answer;
            double sum = 0;
            for (int i = 0; i < x.Count; ++i)
            {
                sum += x[i] * x[i];
            }
            answer = Math.Sqrt(sum);
            return answer;
        }

        #region (QR Decomposition)
        public Tensor HaushodlerMatrix(int index)
        {
            Tensor E = I(Rows,Rows);
            Tensor H1 = I(Rows, Rows);
            H1 *= this;
            List<double> x1 = new List<double>();
            List<double> b = new List<double>();
            for (int i = 0; i < H1.Rows; i++)
            {
                b.Add(H1[i, index]);
            }
            double norm = Vecnorm(b);
            for (int i = 0; i < H1.Rows; i++)
            {
                if (i == index)
                    x1.Add(H1[i, index] + Math.Sign(H1[i, index]) * norm);
                else if (i < index)
                    x1.Add(0);
                else
                    x1.Add(H1[i, index]);
            }
            Tensor v1 = new Tensor(Rows, Rows);
            for (int i = 0; i < v1.Rows; i++)
                v1[i, 0] = x1[i];
            Tensor tmp = I(Rows, Rows);
            tmp = tmp * v1;
            Tensor v1_T = tmp.Tranpose();
            Tensor tmp1 = v1 * v1_T;
            Tensor tmp2 = v1_T * v1;
            Tensor x_tmp = new Tensor(Rows,Rows);
            x_tmp = tmp1 * (1 / (tmp2[0, 0]));
            Tensor x = x_tmp * 2;
            H1 = E - x;

            return H1;
        }
        public List<object> QR()
        {
            List<object> answer = new List<object>();
            Tensor A = I(Rows, Columns);
            A += this;
            List<Tensor> H = new List<Tensor>();
            for (int i = 0; i < Rows; ++i)
            {
                Tensor H0 = A.HaushodlerMatrix(i);
                A = H0 * A;
                H.Add(H0);
            }
            for (int i = 0; i < H.Count - 1; i++)
            {
                H[i + 1] = H[i] * H[i + 1]; //Q
            }
            Tensor A0 = I(Rows, Rows);
            A0 = A * I(Rows,Rows);
           // double varepsilon = 0.000001;

            answer.Add(A); //r
            answer.Add(H[H.Count - 1]); //q
            return answer;
        }
        public List<object> Eigen(Tensor A, int index)
        {
            List<object> answer = new List<object>();
            double varepsilon = 0.01;
            double sum = 0, sum1 = 0;
            List<object> res = new List<object>();
            Tensor A_i = I(Rows, Columns);
            A_i = A_i * A;
            bool flag = true;
            while (flag)
            {
                Tensor Q = (Tensor)A_i.QR()[1];
                Tensor R = (Tensor)A_i.QR()[0];
                A_i = R * Q;
                Tensor a = I(Rows, Columns);
                a = A_i * a;
                for (int i = 1; index + i < Rows; i++)
                {
                    sum += a[index + i, index] * a[index + i, index];
                }

                for (int i = 2; index + i < Rows; i++)
                {
                    sum1 += a[index + i, index] * a[index + i, index];
                }
                if (Math.Sqrt(sum1) <= varepsilon & finish_iter_complex(A_i, index, varepsilon))
                {

                    res.Add(get_roots(A_i, index));
                    res.Add(true);
                    res.Add(A_i);
                    flag = false;
                }
                else if (Math.Sqrt(sum1) <= varepsilon)
                {

                    res.Add(a[index, index]);
                    res.Add(false);
                    res.Add(A_i);
                    flag = false;
                }
                sum = 0;
                sum1 = 0;

            }
            //while ()
            return res;
        }
        public List<double> get_roots(Tensor A, int index)
        {
            //Matrix A = new Matrix(_data, Dimension);
            List<double> x = new List<double>();
            double a_11 = A[index, index], a_12, a_21, a_22;
            if (index + 1 < Rows)
            {
                a_12 = A[index, index + 1];
                a_21 = A[index + 1, index];
                a_22 = A[index + 1, index + 1];
            }
            else
            { a_12 = 0; a_21 = 0; a_22 = 0; }
            x.Add((1 + Math.Sqrt((1 - 4 * (a_12 * a_21 -
                a_12 - a_11 * a_22 / (a_11 + a_22)))) / 2));
            x.Add((1 - Math.Sqrt((1 - 4 * (a_12 * a_21 -
                a_12 - a_11 * a_22 / (a_11 + a_22)))) / 2));

            return x;

        }

        public List<object> QR_method()
        {
            List<object> answer = new List<object>();
            Tensor A = I(Rows, Columns);
            A += this;
            double varepsilon = 0.001;
            double min = Math.Abs(A[1, 0]);

            for (int i = 0; i < Rows - 1; i++)
                if (Math.Abs(A[i + 1, i]) < min)
                    min = Math.Abs(A[i + 1, i]);
            while (min > varepsilon / 100)
            {
                Tensor Q = (Tensor)A.QR()[1];
                Tensor R = (Tensor)A.QR()[0];
                A = R * Q;
                for (int i = 0; i < Rows - 1; i++)
                    if (Math.Abs(A[i + 1, i]) < min)
                        min = Math.Abs(A[i + 1, i]);
            }
            Console.WriteLine(A.ToString());
            for (int i = 0; i < Rows; i++)
            {
                answer.Add(A[i, i]);
            }
            /*if (Math.Abs(A[1, 0]) > 0.01)
            {
                answer[1] = (((A[1, 1] / 2 + A[0, 0]) / 2).ToString() + " + i * (" + get_roots(A, 0)[0].ToString() + ")");
                answer[2] = (((A[1, 1] / 2 + A[0, 0]) / 2).ToString() + " - i * (" + get_roots(A, 0)[0].ToString() + ")");
            }
            if (Math.Abs(A[2, 1]) > 0.01)
            {
                answer[1] = (((A[1, 1] / 2 + A[2, 2]) / 2).ToString() + " + i * (" + get_roots(A, 1)[1].ToString() + ")");
                answer[2] = (((A[1, 1] / 2 + A[2, 2]) / 2).ToString() + " - i * (" + get_roots(A, 1)[1].ToString() + ")");
            }*/
            return answer;
        }

        bool finish_iter_complex(Tensor A, int index, double varepsilon)
        {
            Tensor Q = (Tensor)A.QR()[1];
            Tensor R = (Tensor)A.QR()[0];
            Tensor A_k = R * Q;
            List<double> lambda1 = get_roots(A, index);
            List<double> lambda2 = get_roots(A_k, index);
            if (Math.Abs(lambda1[0] - lambda2[0]) <= varepsilon &
                Math.Abs(lambda1[1] - lambda2[1]) <= varepsilon)
                return true;
            else return false;
        }
        #endregion

    }

}