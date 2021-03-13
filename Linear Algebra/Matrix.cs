using System;
using System.Collections.Generic;
using System.Text;

namespace Linear_Algebra
{
    public class Matrix
    {
        #region(For easily writing matrix)
        public override string ToString()
        {
            string output = "";
            for (int i = 0; i < _Dimension; i++)
            {
                for (int j = 0; j < _Dimension; j++)
                    output += $"|  {_data[i, j]}  |";
                output += '\n';
            }
            return output;
        }
        #endregion

        #region(Dimensions)
        public const int _Dimension = 4;
        private double[,] _data = new double[_Dimension, _Dimension];
        public static int Dimension
        {
            get => _Dimension;

        }
        #endregion
        #region(For easily use of elements)
        public double this[int row, int column]
        {
            get =>
                (row < _Dimension && column < _Dimension) ?
                    _data[row, column] : throw new IndexOutOfRangeException();

            set => _data[row, column] = value;
        }
        #endregion

        #region(Determinant)
        public double Determinant
        {
            get
            {
                double det = 1;
                Matrix M = new Matrix();
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
        #endregion
        #region(Operation with matrices)
        public Matrix Inverse()
        {
            if (Determinant.Equals(0)) throw new ArithmeticException();
            Matrix a = new Matrix(_data);
            Matrix output = Matrix.EMatrix();
            for (int i = 0; i < _Dimension; ++i)
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
                for (j = 0; j < _Dimension; ++j)
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

        private void Changerows(Matrix a, int i, int j)
        {
            for (int k = 0; k < _Dimension; k++)
            {
                double t = a[i, k];
                a[i, k] = a[j, k];
                a[j, k] = t;
            }
        }

        public static Matrix operator +(Matrix a, Matrix b)
        {
            Matrix output = new Matrix();
            for (int i = 0; i < _Dimension; i++)
                for (int j = 0; j < _Dimension; j++)
                    output[i, j] = a[i, j] + b[i, j];
            return output;
        }

        public static Matrix operator ++(Matrix a)
        {
            for (int i = 0; i < _Dimension; i++)
                a[i, i]++;
            return a;
        }
        public static Matrix operator -(Matrix a)
        {
            for (int i = 0; i < _Dimension; ++i)
                for (int j = 0; j < _Dimension; ++j)
                    a[i, j] = -a[i, j];
            return a;
        }

        public static Matrix operator -(Matrix a, Matrix b)
        {
            return a + (-b);
        }

        public static Matrix operator --(Matrix a)
        {
            for (int i = 0; i < _Dimension; i++)
                a[i, i]--;
            return a;
        }

        public static Matrix operator *(Matrix a, Matrix b)
        {
            Matrix output = new Matrix();
            for (int k = 0; k < _Dimension; k++)
                for (int i = 0; i < _Dimension; i++)
                {
                    output[k, i] = 0f;
                    for (int j = 0; j < _Dimension; j++)
                        output[k, i] += a[k, j] * b[j, i];
                }
            return output;
        }
        public static Matrix operator *(Matrix a, double b)
        {
            Matrix output = new Matrix();
            for (int i = 0; i < _Dimension; i++)
            {
                for (int j = 0; j < _Dimension; j++)
                    output[i, j] = a[i, j] * b;
            }
            return output;
        }
        public static Matrix operator /(Matrix a, Matrix b)
        {
            return a * b.Inverse();
        }

        public static bool operator ==(Matrix a, Matrix b)
        {
            return a.Determinant == b.Determinant;
        }


        public static bool operator !=(Matrix a, Matrix b)
        {
            return !(a.Determinant == b.Determinant);
        }

        public static bool operator >(Matrix a, Matrix b)
        {
            return a.Determinant > b.Determinant;
        }

        public static bool operator <(Matrix a, Matrix b)
        {
            return a.Determinant < b.Determinant;
        }
        
        #endregion
        #region(Consturctors)
        public Matrix(params double[] data) //[1,0,0,0,0,1,0,0,0,0,1,0,0,0,0,1]
        {
            for (int i = 0; i < _Dimension; i++)
                for (int j = 0; j < _Dimension; j++)
                    this[i, j] = data[j + i * _Dimension];
        }

        public Matrix(List<double> data) //the same, but List
        {
            for (int i = 0; i < _Dimension; i++)
                for (int j = 0; j < _Dimension; j++)
                    this[i, j] = data[j + i * _Dimension];
        }

        public Matrix(double[,] data) // {{1,0,0,0}, {0,1,0,0}, {0,0,1,0}, {0,0,0,1}}
        {
            _data = data;
        }

        public Matrix(double data) //zero matrix with non-zero diagonal
        {
            for (int i = 0; i < _Dimension; i++)
                for (int j = 0; j < _Dimension; j++)
                    this[i, j] = (i == j) ? data : 0;
        }

        public Matrix() //zero matrix
        {
            for (int i = 0; i < _Dimension; i++)
                for (int j = 0; j < _Dimension; j++)
                    this[i, j] = 0;
        }
        #endregion
        #region(Identity matrix)
        public static Matrix EMatrix()
        {
            Matrix output = new Matrix();
            for (int i = 0; i < _Dimension; i++)
                output[i, i] = 1;
            return output;
        }
        #endregion
        #region(Lab1.1 Solving SLAE with LU decomposition) 
        #region(Choose the main element)
        public List<object> LeaderMustBeFirst(List<double> RP)
        {
            List<object> ret = new List<object>();
            Matrix A = new Matrix(_data);
            double sign_det = 1;
            double count = 0;
            for (int i = 0; i < _Dimension - 1; i++)
            {
                //count = 0;
                int max_i = i;
                double max = Math.Abs(A[i, i]);

                for (int j = i + 1; j < _Dimension; j++)
                {
                    double val = Math.Abs(A[j, i]);
                    if (val > max)
                    {
                        max_i = j;
                        max = val;
                        
                    }
                    if (max != 0.0f)
                    {
                        if (max_i != i)
                        {
                            Changerows(A, i, max_i);
                            double tmp = RP[i];
                            RP[i] = RP[max_i];
                            RP[max_i] = tmp;
                            count++;
                        }

                    }
                }
            }
            if (count % 2 == 0)
                sign_det = 1;
            else sign_det = -1;
            ret.Add(A);
            ret.Add(RP);
            ret.Add(sign_det);
            return ret;
        }
        #endregion
        #region(LU decomposition)
        public List<object> LUDec(List<double> RP)
        {
            Matrix A = new Matrix(_data);
            Matrix L = new Matrix();
            Matrix U = new Matrix();
            Matrix M = new Matrix();
            double sign_det = 1;
            List<Matrix> LU = new List<Matrix>();
            List<double> RP_changed = RP;
            double sum1 = 0, sum2 = 0;
            List<object> answer = new List<object>();
            U = A;
            L = EMatrix();
            for (int i = 0; i < _Dimension; i++)
            {
               // U = (Matrix)U.LeaderMustBeFirst(RP)[0];
                //RP_changed = (List<double>)U.LeaderMustBeFirst(RP)[1];
                //sign_det *= (double)U.LeaderMustBeFirst(RP)[2];
                M = EMatrix();
                for (int j = i + 1; j < _Dimension; j++)
                {
                    M[j, i] = U[j, i] / U[i, i];
                    for (int k = 0; k < _Dimension; k++)
                        U[j, k] -= M[j, i] * U[i, k];
                }
                L *= M;
            }


            LU.Add(L);
            LU.Add(U);
            answer.Add(LU);
            answer.Add(RP_changed);
            answer.Add(sign_det);
            return answer;
        }
        public double Udeterminant(Matrix U)
        {
            double det = 1;
            for (int i = 0; i < _Dimension; i++)
            {
                det *= U[i, i];
            }
            return det;
        }

        
  
        #endregion
        #region(SLAE Solve)

        public List<double> SOLVE(Matrix L, Matrix U, List<double> RP)
        {
            List<double> x = new List<double>();
            List<double> z = new List<double>();
            double sum = 0;
            //Lz = b
            x.Add(0f);
            z.Add(RP[0]);
            for (int i = 1; i < _Dimension; ++i)
            {
                for (int j = 0; j < i; ++j)
                    sum += L[i, j] * z[j];
                z.Add(RP[i]-sum);
                sum = 0;
                x.Add(0f);

            }
            //Ux = z
            double tmp = z[_Dimension - 1] / U[_Dimension - 1, _Dimension - 1]; 
            x[_Dimension-1] = z[_Dimension-1] / U[_Dimension-1, _Dimension-1];
            for (int i = _Dimension-1; i > -1; i--)
            {
                for (int j = i + 1; j < _Dimension; ++j)
                    sum += U[i, j] * x[j];
                x[i] = 1 / U[i, i] * (z[i] - sum);
                sum = 0;
            }
            return x;
        }
        public Matrix Inversion(Matrix L, Matrix U)
        {
            List<double> v = new List<double>();
            for (int i = 0; i < _Dimension; i++)
                v.Add(0f);
            Matrix Inversed = Matrix.EMatrix();
            for (int i = 0; i < _Dimension; ++i)
            {
                v[i] = 1f;
                List<double> x = SOLVE(L, U, v);

                v[i] = 0;
                for (int j = 0; j < _Dimension; ++j)
                {
                    Inversed[j,i] = x[j];
                }
            }
//            for (int i = 0; i )
            return Inversed;

        }
        #endregion
        #endregion
        
    }
}
