﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;

namespace PortfolioSelection
{
    /// <summary>
    /// Матрица заданной размерности
    /// </summary>
    public class Matrix
    {
        private double[,] matrix;
        private int n;
        private int m;

        /// <summary>
        /// Количество строк
        /// </summary>
        public int CountRow
        {
            get
            {
                return n;
            }
        }
        /// <summary>
        /// Количество столбцов
        /// </summary>
        public int CountColumn
        {
            get
            {
                return m;
            }
        }

        /// <summary>
        /// Возвращает true если матрица квадратная
        /// </summary>
        public bool isSquare
        {
            get
            {
                if (n == m)
                    return true;
                return false;
            }
        }

        /// <summary>
        /// Возвращает i строку в виде массива
        /// </summary>
        /// <param name="i">Номер строки</param>
        /// <returns></returns>
        public double[] Row(int i)
        {
            double[] result = new double[m];
            for (int j = 0; j < m; j++)
            {
                result[j] = this[i, j];
            }
            return result;
        }
        /// <summary>
        /// Задает i строку матрицы
        /// </summary>
        /// <param name="i">Номер строки</param>
        /// <param name="row">Строка</param>
        public void Row(int i, double[] row)
        {
            if (row.Length != this.m)
            {
                throw new Exception("Размерность строки не подходит");

            }
            for (int j = 0; j < m; j++)
            {
                this[i, j] = row[j];
            }
        }
        /// <summary>
        /// Возвращает j столбец
        /// </summary>
        /// <param name="j"></param>
        /// <returns></returns>
        public double[] Column(int j)
        {
            double[] result = new double[n];
            for (int i = 0; i < n; i++)
            {
                result[i] = this[i, j];
            }
            return result;
        }
        /// <summary>
        /// Задает j столбец
        /// </summary>
        /// <param name="j">Номер столбца</param>
        /// <param name="column">Столбец</param>
        public void Column(int j, double[] column)
        {
            if (column.Length != this.n)
            {
                throw new Exception("Размерность столбца не подходит");

            }
            for (int i = 0; i < n; i++)
            {
                this[i, j] = column[i];
            }
        }


        /// <summary>
        /// Создает матрицу заданной размерности
        /// </summary>
        /// <param name="n">Количество строк</param>
        /// <param name="m">Количество столбцов</param>
        public Matrix(int n, int m)
        {
            this.n = n;
            this.m = m;
            matrix = new double[n, m];
            matrix.Initialize();

        }
        /// <summary>
        /// Создает матрицу из двумерного постоянного массива
        /// </summary>
        /// <param name="matrix">Двумерный массив</param>
        public Matrix(double[,] matrix)
        {
            this.n = matrix.GetLength(0);
            this.m = matrix.GetLength(1);
            this.matrix = new double[n, m];
            for (int i = 0; i < n; i++)
            {
                for (int j = 0; j < m; j++)
                {
                    this.matrix[i, j] = matrix[i, j];
                }
            }
        }
        /// <summary>
        /// Элемент i-ой строки и j-го стлбца
        /// </summary>
        /// <param name="i">Номер строки</param>
        /// <param name="j">Номер столбца</param>
        /// <returns></returns>
        public double this[int i, int j]
        {
            get
            {
                return matrix[i, j];
            }
            set
            {
                matrix[i, j] = value;
            }
        }


        /// <summary>
        /// Сложение двух матриц
        /// </summary>
        /// <param name="a"></param>
        /// <param name="b"></param>
        /// <returns></returns>
        public static Matrix operator +(Matrix a, Matrix b)
        {
            if ((a.n == b.n) && (a.m == b.m))
            {
                int n = a.n;
                int m = a.m;
                Matrix result = new Matrix(n, m);

                for (int i = 0; i < n; i++)
                {
                    for (int j = 0; j < m; j++)
                    {
                        result[i, j] = a[i, j] + b[i, j];
                    }
                }
                return result;

            }
            throw new Exception("Разная размерность матриц, их нельзя сложить");
        }

        /// <summary>
        /// Разность матриц
        /// </summary>
        /// <param name="a"></param>
        /// <param name="b"></param>
        /// <returns></returns>
        public static Matrix operator -(Matrix a, Matrix b)
        {
            if ((a.n == b.n) && (a.m == b.m))
            {
                int n = a.n;
                int m = a.m;
                Matrix result = new Matrix(n, m);

                for (int i = 0; i < n; i++)
                {
                    for (int j = 0; j < m; j++)
                    {
                        result[i, j] = a[i, j] - b[i, j];
                    }
                }
                return result;

            }
            throw new Exception("Разная размерность матриц, их нельзя вычитать");
        }

        /// <summary>
        /// Унарный минус
        /// </summary>
        /// <param name="a"></param>
        /// <returns></returns>
        public static Matrix operator -(Matrix a)
        {
            int n = a.n;
            int m = a.m;
            Matrix result = new Matrix(n, m);


            for (int i = 0; i < n; i++)
            {
                for (int j = 0; j < m; j++)
                {
                    result[i, j] = -a[i, j];
                }

            }
            return result;
        }

        /// <summary>
        /// Умножение матриц
        /// </summary>
        /// <param name="a"></param>
        /// <param name="b"></param>
        /// <returns></returns>
        public static Matrix operator *(Matrix a, Matrix b)
        {
            if (a.m == b.n)
            {
                int midle = a.m;

                int n = a.n;
                int m = b.m;
                Matrix result = new Matrix(n, m);

                for (int i = 0; i < n; i++)
                {
                    for (int j = 0; j < m; j++)
                    {

                        for (int k = 0; k < midle; k++)
                        {
                            result[i, j] += a[i, k] * b[k, j];

                        }
                    }
                }
                return result;

            }
            throw new Exception("Количество строк не совпадает с количеством столбцов");
        }

        /// <summary>
        /// Умножение матрицы на число 
        /// </summary>
        /// <param name="x">Число</param>
        /// <param name="A">Матрица</param>
        /// <returns></returns>
        public static Matrix operator *(double x, Matrix A)
        {
            Matrix result = new Matrix(A.n, A.m);

            for (int i = 0; i < A.n; i++)
            {
                for (int j = 0; j < A.m; j++)
                {
                    result[i, j] = A[i, j] * x;
                }
            }
            return result;
        }
        /// <summary>
        /// Деление матрицы на число 
        /// </summary>
        /// <param name="x">Число</param>
        /// <param name="A">Матрица</param>
        /// <returns></returns>
        public static Matrix operator /(Matrix A, double x)
        {
            Matrix result = new Matrix(A.n, A.m);

            for (int i = 0; i < A.n; i++)
            {
                for (int j = 0; j < A.m; j++)
                {
                    result[i, j] = A[i, j] / x;
                }
            }
            return result;
        }




        /// <summary>
        /// Заполняет массив случайными  вещественными числами 
        /// </summary>
        /// <param name="MinValue">Минимальное значение</param>
        /// <param name="MaxValue">Максимальное значение</param>
        public void RandomDoubleMatrix(double MinValue, double MaxValue)
        {
            Random rand = new Random();

            for (int i = 0; i < n; i++)
            {
                for (int j = 0; j < m; j++)
                {
                    matrix[i, j] = MinValue + rand.NextDouble() * Math.Abs(MaxValue - MinValue);

                }
            }
        }

        /// <summary>
        /// Заполняет массив случайными  целыми числами 
        /// </summary>
        /// <param name="MinValue">Минимальное значение</param>
        /// <param name="MaxValue">Максимальное значение</param>
        public void RandomIntMatrix(int MinValue, int MaxValue)
        {
            Random rand = new Random();
            for (int i = 0; i < n; i++)
            {
                for (int j = 0; j < m; j++)
                {
                    matrix[i, j] = rand.Next(MinValue, MaxValue);

                }
            }
        }
        /// <summary>
        /// Возвращает транспонированную матрицу
        /// </summary>
        public Matrix Transpose()
        {
            double[,] temp = new double[m, n];
            for (int i = 0; i < n; i++)
            {
                for (int j = 0; j < m; j++)
                {
                    temp[j, i] = matrix[i, j];
                }
            }
            return new Matrix(temp);

        }

        /// <summary>
        /// Создает копию данной матрицы
        /// </summary>
        /// <returns></returns>
        public Matrix Copy()
        {
            return new Matrix(this.matrix);
        }

        /// <summary>
        /// Возвращает определитель матрицы
        /// </summary>
        /// <returns></returns>
        public double Det()
        {
            //Копирование всех входных параметров
            Matrix A = this.Copy();
            double det = 1;

            //Прямой ход метода Гаусса
            if (A.CountColumn != A.CountRow)
            {
                throw new Exception("Матрица на квадратная");
            }
            int n = A.CountColumn;

            int transposition = 1;//четность перестановки

            //Получение треугольной матрицы - прямой ход 
            for (int k = 0; k < n; k++)
            {
                //Ищем максимальный элемент в k строке
                double[] row = A.Row(k);
                int maxNum = Computation.MaxAbs(row);
                if (row[maxNum] == 0)
                {
                    return 0;
                }

                if (k != maxNum)
                {
                    transposition *= -1;
                    //обмен
                    double[] temp = A.Column(maxNum);//сохраняю максимальный столбец 
                    A.Column(maxNum, A.Column(k)); //на место максимального стоблца ставлю ("первый") k столбец
                    A.Column(k, temp);
                }



                for (int m = k + 1; m < n; m++) //взял m-ую строку 
                {
                    double cmk = -A[m, k] / A[k, k];//к-т для умножения k строки
                    for (int j = k; j < n; j++)
                    {
                        A[m, j] = A[m, j] + cmk * A[k, j];
                    }
                }
            }

            //Подсчет произведения диагональных элементов
            for (int i = 0; i < n; i++)
            {
                det *= A[i, i];
            }
            return det * transposition;
        }


        /// <summary>
        /// ПОлучает матрицу обратную к данной
        /// </summary>
        /// <returns></returns>
        public Matrix Inv()
        {

            if (!this.isSquare) throw new Exception("Матрица не квадратная. Невозможно получить обратную");

            Matrix inv = new Matrix(n, n);

            for (int i = 0; i < n; i++)
            {
                double[] b = new double[n];
                b.Initialize();
                b[i] = 1;

                inv.Column(i, Computation.Gauss(this, b));

            }

            return inv;
        }

        /// <summary>
        /// Возвращает строковое представление матрицы
        /// </summary>
        /// <returns></returns>
        public override string ToString()
        {

            StringBuilder strbuild = new StringBuilder(String.Empty);
            for (int i = 0; i < n; i++)
            {
                for (int j = 0; j < m; j++)
                {
                    strbuild.AppendFormat("{0:f4};", matrix[i, j]);
                }
                strbuild.Append(Environment.NewLine);

            }

            return strbuild.ToString();
        }

        /// <summary>
        /// Возвращает норму матрицы - максимальную из сумм модулей элементов строки
        /// </summary>
        /// <returns></returns>
        public double Norm()
        {
            double[] sum = new double[this.CountRow];
            sum.Initialize();

            for (int i = 0; i < n; i++)
            {
                for (int j = 0; j < m; j++)
                {
                    sum[i] = +Math.Abs(this[i, j]);
                }

            }
            return sum.Max();
        }





    }
}
