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
            for (int i = 0; i < _Dimension1; i++)
            {
                for (int j = 0; j < _Dimension2; j++)
                    output += $"|  {_data[i, j]}  |";
                output += '\n';
            }
            return output;
        }
        #endregion

        #region(Dimensions)
        public int _Dimension1; //row
        public int _Dimension2; //column
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
        #region(SVD)
        public List<Object> SVD()
        {
            List<Object> answer = new List<Object>();
            /*Tensor b = new Tensor(Rows, Rows);
            Tensor a = new Tensor(Columns, Columns);
            for (int i = 0; i < Rows; i++)
            {
                a[i, 0] = Math.Sqrt(1d/(Rows));
            }
            Tensor Sigma = new Tensor(Rows, Columns);
            Tensor X = new Tensor(_data, Rows, Columns);
            double sum1 = 0, sum2 = 0;
            for (int i = 0; i < Rows; i++)
            {
                for (int j = 0; j < Rows; j++)
                {
                    sum1 += X[0, j] * a[j, 0];
                    sum2 += a[j, 0] * a[j, 0];
                }
                b[i, 0] = sum1 / sum2;
            }
            sum1 = 0;
            sum2 = 0;

            for (int i = 0; i < Rows; i++)
            {
                for (int j = 0; j < Rows; j++)
                {
                    sum1 += b[j, 0] * X[j, 0];
                    sum2 += b[j, 0] * b[j, 0];
                }
                a[i, 1] = sum1 / sum2;
            }
            Tensor P = b * a;
            Tensor sumP = new Tensor(P.Rows, P.Columns);
            sumP += P;
            X -= P;
            int column = 1;
            int iter = 0;
            while (X.norm_2() > 0.00001)
            {
                *//*if (iter == 694)
                {
                    for (int i = column; i < Columns; i++)
                    {
                        sum1 = 0;
                        sum2 = 0;
                        for (int k = 0; k < Rows; k++)
                        {
                            for (int j = 0; j < Rows; j++)
                            {
                                sum1 += X[i, j] * a[j, i];
                                sum2 += a[j, i] * a[j, i];
                            }
                            b[k, i] = sum1 / sum2;
                        }
                        sum1 = 0;
                        sum2 = 0;
                        if (i + 1 < Columns)
                        {
                            for (int k = 0; k < Rows; k++)
                            {
                                for (int j = 0; j < Rows; j++)
                                {
                                    sum1 += b[j, i] * X[j, i];
                                    sum2 += b[j, i] * b[j, i];
                                }
                                a[k, i + 1] = sum1 / sum2;
                            }
                        }
                    }
                    column = 0;
                    P = b * a;
                    X -= P;
                    for (int i = 0; i < Rows; i++)
                    {
                        a[i, 0] = Math.Sqrt(1d / (Rows));
                    }
                    iter++;

                }
*//*                for (int i = column; i < Columns; i++)
                {
                    sum1 = 0;
                    sum2 = 0;
                    for (int k = 0; k < Rows; k++)
                    {
                        for (int j = 0; j < Rows; j++)
                        {
                            sum1 += X[i, j] * a[j, i];
                            sum2 += a[j, i] * a[j, i];
                        }
                        b[k, i] = sum1 / sum2;
                    }
                    sum1 = 0;
                    sum2 = 0;
                    if (i + 1 < Columns)
                    {
                        for (int k = 0; k < Rows; k++)
                        {
                            for (int j = 0; j < Rows; j++)
                            {
                                sum1 += b[j, i] * X[j, i];
                                sum2 += b[j, i] * b[j, i];
                            }
                            a[k, i + 1] = sum1 / sum2;
                        }
                    }
                }
                column = 0;
                P = b * a;
                X  = X - P;
                for (int i = 0; i < Rows; i++)
                {
                    a[i, 0] = Math.Sqrt(1d / (Rows));
                }
                iter++;
                
            }
            for (int j = 0; j < Columns; ++j)
            {

                Tensor tmpa = new Tensor(Rows, 1);
                Tensor tmpb = new Tensor(Rows, 1);
                for (int k = 0; k < Rows; k++)
                {
                    tmpa[k, 0] = a[k, j];
                    tmpb[k, 0] = b[k, j];
                }
                for (int i = 0; i < Rows; ++i)
                {
                    
                    //a[i, j] /= tmpa.norm_2();
                    //b[i, j] /= tmpb.norm_2();
                    Sigma[j, j] = tmpa.norm_2() / tmpb.norm_2();
                }
            }
            
            answer.Add(a);
            answer.Add(b);
            answer.Add(Sigma);
            return answer;*/

            int rank_A = Rank();
            Tensor A = new Tensor(Rows,Columns);
            A = A + this;
            
            Tensor U = new Tensor(Rows, rank_A);
            Tensor V = new Tensor(Columns, rank_A);
            List<double> Sigma = new List<double>();
            Tensor sigma_t = new Tensor(Rows, Columns);
            double varepsilon = 0.01, delta = 0.001, lambda = 2;
            double iter = (int)(Math.Log(4*Math.Log(2*Columns / delta) / (varepsilon * delta) / (2*lambda)));
            Tensor x0 = new Tensor(random_normal(this), Columns, 1);
            Tensor xi_vec = new Tensor(Rows, 1);

            for (int i = 0; i < rank_A; i++)
            {
                for (int j = 0; j < iter; j++)
                {
                    Tensor Trans = A.Tranpose();
                    xi_vec = (Trans * A) * x0;
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
                
                Tensor u_i = new Tensor(Rows, 1);
                u_i = (A * v_i) * (1 / Sigma[i]);

                for (int k = 0; k < U.Rows; k++)
                {
                    U[k, i] = u_i[k, 0];
                }
                // x0 = xi_vec;
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
        public List<double> random_normal(Tensor A)
        {
            List<double> x = new List<double>();
            int M = 0; int sigma = 1;
            Random tmp = new Random();
            for (int i = 0; i < A.Rows; i++)
            {
                x.Add(f_normal(tmp.Next(5)));
            }
            return x;

        }
        public double f_normal(double x) =>
            1 / Math.Sqrt(2 * Math.PI) * Math.Exp(-(x * x) / 2);
        public int Rank()
        {
            int rank = Math.Min(Rows, Columns);
            Tensor a = new Tensor(Rows, Columns);
            a = a + this;
        //    a *= this;
            List<bool> line_used = new List<bool>();
            for (int i = 0; i < Rows; i++)
                line_used.Add(false);
            for (int i = 0; i < Columns; i++)
            {
                int j;
                for (j = 0; j < Rows; j++)
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
    }

}
/* 
            int rank_A = Rank();
            Tensor A = new Tensor(_data, Rows,Columns);
            Tensor U = new Tensor(Rows, rank_A);
            Tensor V = new Tensor(Columns, rank_A);
            List<double> Sigma = new List<double>();
            Tensor sigma_t = new Tensor(Rows, Columns);
            double varepsilon = 0.01, delta = 0.001, lambda = 2;
            double iter = (int)(Math.Log(4*Math.Log(2*Columns / delta) / (varepsilon * delta) / (2*lambda)));
            Tensor x0 = new Tensor(random_normal(this), Columns, 1);
            Tensor xi_vec = new Tensor(Rows, 1);

            for (int i = 0; i < rank_A; i++)
            {
                for (int j = 0; j < iter; j++)
                {
                    Tensor tmp1 = new Tensor(Rows, Rows);
                    tmp1 = A.Tranpose() * this;
                    xi_vec = tmp1 * x0;
                    //for (int k = 0; k < xi_vec.Rows; k++)
                      //  x0[k,0] = xi_vec[k,0];
                }
                Tensor v_i = new Tensor(Columns, 1);
                for (int k = 0; k < Columns; k++)
                {
                    V[k, i] = xi_vec[k, 0] / xi_vec.norm_2();
                    v_i[k, 0] = V[k, i];
                }

                Sigma.Add((A * v_i).norm_2());
                
                Tensor u_i = new Tensor(Rows, 1);
                u_i = (A * v_i) * (1 / Sigma[i]);

                for (int k = 0; k < U.Rows; k++)
                {
                    U[k, i] = u_i[k, 0];
                }
                x0 = xi_vec;
                A = A - U * (V.Tranpose()) * Sigma[i];

            }
            answer.Add(U);
            answer.Add(V);
            for (int i = 0; i < rank_A; i++)
                sigma_t[i, i] = Sigma[i];
            answer.Add(sigma_t);
            return answer;*/

