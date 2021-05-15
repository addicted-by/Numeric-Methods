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
<<<<<<< HEAD

        public Matrix Tranpose()
        {
            Matrix answer = EMatrix(Dimension);
            answer += this;
            double tmp;
            for (int i = 0; i < answer.Dimension; i++)
            {
                for (int j = 0; j < i; j++)
                {
                    tmp = answer[i, j];
                    answer[i, j] = answer[j, i];
                    answer[j, i] = tmp;
                }
            }
            return answer;
        }
=======
>>>>>>> parent of 5f1fef0 (3-5)
        
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
            return Inversed;

        }
        #endregion
        #endregion
<<<<<<< HEAD

        #region (Lab1.3 Solving SLAE (Simple and Zeidel))

        #region(Norms)
        public double norm_C()
        {
            double answer = 0;
            Matrix A = new Matrix(_data);
            for (int i = 0; i < _Dimension; i++)
            {
                double sum = 0;
                for (int j = 1; j < _Dimension; j++)
                {
                    sum += Math.Abs(A[i, j]);
                }
                if (sum > answer)
                    answer = sum;
            }
            return answer;
        }

        public double norm_1()
        {
            Matrix A = new Matrix(_data);
            double answer = 0;
            for (int j = 0; j < _Dimension; j++)
            {
                double sum = 0;
                for (int i = 0; i < _Dimension; i++)
                {
                    sum += Math.Abs(A[i, j]);
                }
                if (sum > answer)
                    answer = sum;
            }
            return answer;
        }
        public double norm_2()
        {
            Matrix A = new Matrix(_data);
            double answer = 0;
            
            for (int i = 0; i < _Dimension; i++)
            {
                for (int j = 0; j < _Dimension; j++)
                {
                    answer += A[i, j] * A[i, j];
                }
            }
            return Math.Sqrt(answer);
        }

        public double Cond()
        {
            Matrix A = new Matrix(_data);
            return A.norm_2() * A.Inverse().norm_2();
        }
        #endregion
        #region(Simple Iteration Method)
        public List<object> SIM(List<double> b)
        {
            Matrix A = new Matrix(_data);
            List<object> answer = new List<object>();
            List<double> beta = new List<double>();
            Matrix alpha = new Matrix();
            
            for (int i = 0; i < _Dimension; i++)
            {
                for (int j = 0; j < _Dimension; j++)
                {
                    if (i == j)
                        alpha[i, j] = 0;
                    else 
                        alpha[i, j] = -A[i, j] / A[i, i];
                }
                beta.Add(b[i] / A[i, i]);
            }
            List<double> x = beta;
            
            int k = 0;
            double varepsilon_k = 1;
            double varepsilon = 0.0000001;
            double coef = alpha.norm_C() / (1 - alpha.norm_C());
            while (varepsilon_k > varepsilon)
            {
                List<double> x_prev = x;
                x = this.Vecsum(beta, alpha.Multiply_vec(x));
                varepsilon_k = coef * (this.Vecnorm(this.Vecdiff(x, x_prev)));
                k++;
            }
            answer.Add(k);
            answer.Add(x);
            return answer;
        }
        #endregion
        public List<double> Vecdiff(List<double> x, List<double> x_prev)
        {
            List<double> answer = new List<double>();
            for (int i = 0; i < x.Count; ++i)
            {
                answer.Add(x[i] - x_prev[i]);
            }
            return answer;
        }


        public List<double> Vecsum(List<double> x, List<double> x_prev)
        {
            List<double> answer = new List<double>();
            for (int i = 0; i < x.Count; ++i)
            {
                answer.Add(x[i] + x_prev[i]);
            }
            return answer;
        }

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

        public List<double> Multiply_vec(List<double> x)
        {
            Matrix a = new Matrix(_data);
            List<double> answer = new List<double>();
            double sum = 0;
            for (int i = 0; i < _Dimension; i++)
            {
                for (int j = 0; j < _Dimension; j++)
                {
                    sum += a[i, j] * x[j];  
                }
                answer.Add(sum);
                sum = 0;
            }
            return answer;
        }
         #region(Seidel Method)
        public List<object> Seidel(List<double> b)
        {
            List<object> answer = new List<object>();
            Matrix A = new Matrix(_data);
            Matrix alpha = new Matrix();
            Matrix B = new Matrix();
            Matrix C = new Matrix();
            List<double> beta = new List<double>();
            for (int i = 0; i < _Dimension; i++)
            {
                for (int j = 0; j < _Dimension; j++)
                {
                    if (i == j)
                        alpha[i, j] = 0;
                    else
                        alpha[i, j] = -A[i, j] / A[i, i];
                }
                beta.Add(b[i] / A[i, i]);
            }
            double varepsilon_k = 1;
            double varepsilon = 0.0001;
            List<double> x_prev = new List<double>();
            List<double> x = new List<double>();
            for (int i = 0; i < beta.Count; i++)
                x.Add(beta[i]);
            int k = 0;
            double coef = alpha.norm_C() / (1 - alpha.norm_C());
            while (varepsilon_k > varepsilon)
            {
                for (int i = 0; i < x.Count; i++)
                    x_prev.Add(x[i]);
              for (int i = 0; i < x.Count; i++)
                {
                    x[i] = 0;
                    for (int j = 0; j < x.Count; j++)
                        x[i] += alpha[i, j] * x[j];
                    x[i] += beta[i];
                }
                
                varepsilon_k = coef * (this.Vecnorm(this.Vecdiff(x, x_prev)));
                x_prev.Clear();
                k++;
            }

            answer.Add(k);
            answer.Add(x);

            return answer;
        }
        #endregion
        #endregion


        #region(Lab1.4 Rotation Method)
        public List<object> Rotation(int n)
        {
            List<object> answer = new List<object>();
            Matrix U_eigen = EMatrix(n);
            Matrix a = new Matrix(_data, n);
            Matrix a_new = a;
            int k = 0;
            int t;
            double sum = 0;
            for (int i = 0; i < a.Dimension; i++)
            {
                for (int j = 0; j < a.Dimension; j++)
                {
                    if (i != j)
                        sum += a[i, j] * a[i, j];
                }
            }
            double varepsilon = 0.001;
            int i_max = 0, j_max = 1;
            Matrix U_k = EMatrix(3);
            while (Math.Sqrt(sum) > varepsilon)
            {

                double max = a[0, 1];
                for (int i = 0; i < a.Dimension - 1; i++)
                    for (int j = i + 1; j < a.Dimension; j++)
                    {
                        if (i != j)
                            if (Math.Abs(a[i, j]) > max)
                            {
                                i_max = i;
                                j_max = j;
                                max = Math.Abs(a[i, j]);
                            }
                    }
                double phi;
                double check = 2 * a[i_max, j_max] / (a[i_max, i_max] - a[j_max, j_max]);
                check = Math.Atan(check) / 2;
                if (a[i_max, i_max] == a[j_max, j_max]) phi = Math.PI / 4;
                else phi = check;

                U_k[i_max, j_max] = -Math.Sin(phi);
                U_k[j_max, i_max] = Math.Sin(phi);
                U_k[i_max, i_max] = Math.Cos(phi);
                U_k[j_max, j_max] = Math.Cos(phi);
                max = a[0, 1];
                Matrix tmp = EMatrix(3);
                tmp *= U_k;
                Matrix tmp_1 = U_k.Tranpose();
                a_new = tmp_1 * a;
                a_new = a_new * tmp;
                a = a_new;
                sum = 0;
                for (int i = 0; i < a.Dimension; i++)
                {
                    for (int j = 0; j < a.Dimension; j++)
                    {
                        if (i > j)
                            sum += a[i, j] * a[i, j];
                    }
                }
                k++;
                U_eigen = U_eigen * U_k;
                U_k = EMatrix(3);
                i_max = 0;
                j_max = 1;
            }
            answer.Add(a);
            answer.Add(k);
            answer.Add(U_eigen);
            return answer;

        }

        public List<double> scalar()
        {
            List<double> solution = new List<double>();
            Matrix A = new Matrix(_data, _Dimension);
            for (int i = 0; i < _Dimension - 1; i++)
            {
                for (int j = i + 1; j < _Dimension; j++)
                {
                    solution.Add(0);
                    for (int k = 0; k < _Dimension; k++)
                    {
                        solution[solution.Count - 1] += A[k, i] * A[k, j];
                    }
                }
            }
            return solution;
        } 
        #endregion
        #region(Lab1.5 QR-Decomposition)

        public Matrix HaushodlerMatrix(int index)
        {
            Matrix E = EMatrix(3);
            Matrix H1 = new Matrix(_data,3);
            List<double> x1 = new List<double>();
            List<double> b = new List<double>();
            for (int i = 0; i < H1.Dimension; i++)
            {
                b.Add(H1[i,index]);
            }
            double norm = Vecnorm(b);
            for (int i = 0; i < H1.Dimension; i++)
            {
                if (i == index)
                    x1.Add(H1[i, index] + Math.Sign(H1[i, index]) * norm);
                else if (i < index)
                    x1.Add(0);
                else
                    x1.Add(H1[i,index]);
            }
            Matrix v1 = new Matrix(3);
            for (int i = 0; i < v1.Dimension; i++)
                v1[i, 0] = x1[i];
            Matrix tmp = EMatrix(3);
            tmp = tmp * v1;
            Matrix v1_T = tmp.Tranpose();
            Matrix tmp1 = v1 * v1_T;
            Matrix tmp2 = v1_T * v1;
            Matrix x_tmp = new Matrix(3);
            x_tmp = tmp1 * (1/(tmp2[0, 0]));
            Matrix x = x_tmp * 2;
            H1 = E - x;
            
            return H1;
        }
        public List<object> QR()
        {
            List<object> answer = new List<object>();
            Matrix A = new Matrix(_data,3);
            List<Matrix> H = new List<Matrix>();
            for (int i = 0; i < _Dimension; ++i)
            {
                Matrix H0 = A.HaushodlerMatrix(i);
                A = H0 * A;
                H.Add(H0);
            }
            for (int i = 0; i < H.Count - 1; i++)
            {
                H[i + 1] = H[i] * H[i + 1]; //Q
            }
            Matrix A0 = EMatrix(3);
            A0 = A * EMatrix(3);
            double varepsilon = 0.000001;
            
            answer.Add(A); //r
            answer.Add(H[H.Count - 1]); //q
            return answer;
        }
        List<object> Eigen(Matrix A, int index)
        {
            List<object> answer = new List<object>();
            double varepsilon = 0.01;
            double sum = 0, sum1 = 0;
            List<object> res = new List<object>();
            Matrix A_i = EMatrix(3);
            A_i = A_i * A;
            bool flag = true;
            while (flag)
            {
                Matrix Q = (Matrix)A_i.QR()[1];
                Matrix R = (Matrix)A_i.QR()[0];
                A_i = R * Q;
                Matrix a = EMatrix(3);
                a = A_i * a;
                for (int i = 1; index + i < Dimension; i++)
                {
                    sum += a[index + i, index] * a[index + i, index];
                }

                for (int i = 2; index + i < Dimension; i++)
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
                else if (Math.Sqrt(sum1) <= varepsilon )
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
        List<double> get_roots(Matrix A, int index)
        {
            //Matrix A = new Matrix(_data, Dimension);
            List<double> x = new List<double>();
            double a_11 = A[index, index], a_12, a_21, a_22;
            if (index + 1 < Dimension)
            { a_12 = A[index, index + 1];
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
            Matrix A = new Matrix(_data, Dimension);
            double varepsilon = 0.001;
            
            /*List<object> answer = new List<object>();
            List<double> res = new List<double>();
            Matrix A_i = new Matrix(_data, Dimension);
            int i = 0;
            while (i < Dimension)
            {
                List<object> eigen = Eigen(A_i, i);
                if ((bool)eigen[1])
                {
                    foreach (var x in (List<double>)eigen[0])
                        res.Add(x);
                    i += 2;
                }
                else
                {
                    res.Add((double)eigen[0]);
                    i++;
                }
                A_i = (Matrix)eigen[2];
            }
            answer.Add(res);
            answer.Add(i);*/
            double min = Math.Abs(A[1,0]);

            for (int i = 0; i < Dimension-1; i++)
                if (Math.Abs(A[i + 1, i]) < min)
                    min = Math.Abs(A[i + 1, i]);
            while (min > varepsilon / 10000)
            {
                Matrix Q = (Matrix)A.QR()[1];
                Matrix R = (Matrix)A.QR()[0];
                A = R * Q;
                for (int i = 0; i < Dimension - 1; i++)
                    if (Math.Abs(A[i + 1, i]) < min)
                        min = Math.Abs(A[i + 1, i]);
            }

            for (int i = 0; i < Dimension; i++)
            {
                answer.Add(A[i, i]);
            }    
            if (Math.Abs(A[1,0]) > 0.01)
            {
                answer[1] = (((A[1, 1] / 2 + A[0, 0]) / 2).ToString() + " + i * (" + get_roots(A,0)[0].ToString() + ")");
                answer[2] = (((A[1, 1] / 2 + A[0, 0]) / 2).ToString() + " - i * (" + get_roots(A,0)[0].ToString() + ")");
            }
            if (Math.Abs(A[2, 1]) > 0.01)
            {
                answer[1] = (((A[1, 1] / 2 + A[2,2])/2).ToString() + " + i * (" + get_roots(A, 1)[1].ToString() + ")");
                answer[2] = (((A[1, 1] / 2 + A[2, 2]) / 2).ToString() + " - i * (" + get_roots(A, 1)[1].ToString() + ")");
            }
            return answer;
        }

        bool finish_iter_complex(Matrix A, int index, double varepsilon)
        {
            Matrix Q = (Matrix)A.QR()[1];
            Matrix R = (Matrix)A.QR()[0];
            Matrix A_k = R * Q;
            List<double> lambda1 = get_roots(A, index);
            List<double> lambda2 = get_roots(A_k, index);
            if (Math.Abs(lambda1[0] - lambda2[0]) <= varepsilon &
                Math.Abs(lambda1[1] - lambda2[1]) <= varepsilon)
                return true;
            else return false;
        }
        #endregion

=======
        
>>>>>>> parent of 5f1fef0 (3-5)
    }
}
