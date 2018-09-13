using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;

namespace PortfolioSelection
{

    /// <summary>
    /// Методы работы с уравнениями и матрицами 
    /// </summary>
    public static class Computation
    {

        /// <summary>
        /// Возвращает номер максимального 
        /// по модулю элемента
        /// </summary>
        /// <param name="mas"></param>
        /// <returns></returns>
        public static int MaxAbs(double[] mas)
        {
            if (mas.Length == 0) throw new Exception("Массив пуст. Нет max");

            int maxNum = 0;
            for (int i = 0; i < mas.Length; i++)
            {
                if (Math.Abs(mas[maxNum]) < Math.Abs(mas[i]))
                {
                    maxNum = i;
                }
            }
            return maxNum;

        }


        /// <summary>
        /// Решение системы методом Гаусса. Для решения матрица системы должна быть квадратной.
        /// </summary>
        /// <param name="A">Матрица системы</param>
        /// <param name="b">Вектор свободных членов</param>
        public static double[] Gauss(Matrix A, double[] b)
        {
            //Копирование всех входных параметров
            Matrix _A = A.Copy();
            double[] _b = new double[b.Length];
            for (int i = 0; i < b.Length; i++)
            {
                _b[i] = b[i];
            }

            //Прямой ход метода Гаусса
            if ((!_A.isSquare) || (_b.Length != _A.CountColumn))
            {
                throw new Exception("Матрица на квадратная или столбец свободных членов не соответствует размерности матрицы. Метод Гаусса не применим");
            }
            int n = _A.CountColumn;

            int[] transposition = new int[n];//вектор перестановок
            for (int i = 0; i < n; i++)
            {
                transposition[i] = i;
            }

            //Получение треугольной матрицы - прямой ход 
            for (int k = 0; k < n; k++)
            {
                //Ищем максимальный элемент в k строке
                double[] row = _A.Row(k);
                int maxNum = MaxAbs(row);
                if (row[maxNum] == 0)
                {
                    throw new Exception("Все элементы строки нулевые. Такая система имеет множество решений");
                }
                if (maxNum != k)
                {
                    //обмен
                    double[] temp = _A.Column(maxNum);//сохраняю максимальный столбец 
                    _A.Column(maxNum, _A.Column(k)); //на место максимального стоблца ставлю ("первый") k столбец
                    _A.Column(k, temp);
                    int temp_num = transposition[k];
                    transposition[k] = transposition[maxNum];
                    transposition[maxNum] = temp_num;
                }



                for (int m = k + 1; m < n; m++) //взял m-ую строку 
                {
                    double cmk = -_A[m, k] / _A[k, k];//к-т для умножения k строки
                    for (int j = k; j < n; j++)
                    {
                        _A[m, j] = _A[m, j] + cmk * _A[k, j];
                    }
                    _b[m] = _b[m] + cmk * _b[k];

                }
            }


            //Обратный ход

            double[] x = new double[n];

            for (int k = n - 1; k >= 0; k--)
            {
                double sum = 0;
                for (int i = k + 1; i < n; i++)
                {
                    sum += _A[k, i] * x[transposition[i]];
                }
                x[transposition[k]] = (_b[k] - sum) / _A[k, k];

            }
            return x;

        }

        /// <summary>
        /// Решение СЛАУ методом простой итерации
        /// </summary>
        /// <param name="A_arg">Квадратная матрица системы</param>
        /// <param name="b_arg">Вектор свободных членов</param>
        /// <returns></returns>
        public static double[] SuccessiveIteration(Matrix A_arg, double[] b_arg, double accuracy)
        {
            //Копирование всех входных параметров
            Matrix A = A_arg.Copy();

            if ((!A.isSquare) || (b_arg.Length != A.CountColumn))
            {
                throw new Exception("Матрица на квадратная или столбец свободных членов не соответствует размерности матрицы. Метод Гаусса не применим");
            }
            int n = A.CountColumn;

            Matrix x = new Matrix(n, 1);
            Matrix b = new Matrix(n, 1);
            for (int i = 0; i < n; i++)
            {
                b[i, 0] = b_arg[i];
            }



            double t = 1.0 / (A.Norm());
            int iter = 0;

            while ((A * x - b).Norm() > accuracy)
            {
                x = (t * b) + x - (t * A) * x;
                iter++;

            }

            Console.WriteLine("Произведено {0} итер.", iter);

            double[] result = new double[n];
            for (int i = 0; i < n; i++)
            {
                result[i] = x[i, 0];
            }

            return result;
        }


        /// <summary>
        /// Решение системы линейных уравнений методом Зейделя
        /// </summary>
        /// <param name="A_arg">Матрица системы</param>
        /// <param name="b_arg">Столбец свободных членов</param>
        /// <param name="accuracy">Точность вычисления</param>
        /// <returns></returns>
        public static double[] Zeidel(Matrix A_arg, double[] b_arg, double accuracy)
        {
            //Копирование всех входных параметров
            Matrix A = A_arg.Copy();

            if ((!A.isSquare) || (b_arg.Length != A.CountColumn))
            {
                throw new Exception("Матрица на квадратная или столбец свободных членов не соответствует размерности матрицы. Метод Гаусса не применим");
            }
            int n = A.CountColumn;

            Matrix x = new Matrix(n, 1);
            Matrix b = new Matrix(n, 1);
            for (int i = 0; i < n; i++)
            {
                b[i, 0] = b_arg[i];
            }


            int iter = 0;

            while ((A * x - b).Norm() > accuracy)
            {
                iter++;
                for (int i = 0; i < n; i++)
                {
                    double temp = 0;
                    for (int j = 0; j < n; j++)
                    {
                        if (j == i) continue;
                        temp += A[i, j] * x[j, 0];
                    }
                    x[i, 0] = (b[i, 0] - temp) / A[i, i];
                }


            }

            Console.WriteLine("Произведено {0} итер.", iter);


            double[] result = new double[n];
            for (int i = 0; i < n; i++)
            {
                result[i] = x[i, 0];
            }

            return result;


        }


        /// <summary>
        ///Метод прогонки для СЛАУ с трехдиагональной матрицей
        /// </summary>
        /// <param name="A_arg"></param>
        /// <param name="b_arg"></param>
        /// <param name="accuracy"></param>
        /// <returns></returns>
        public static double[] DoubleSweepMethod(Matrix A_arg, double[] b_arg)
        {
            //Копирование всех входных параметров
            Matrix A = A_arg.Copy();

            if ((!A.isSquare) || (b_arg.Length != A.CountColumn))
            {
                throw new Exception("Матрица на квадратная или столбец свободных членов не соответствует размерности матрицы. Метод прогонки не применим");
            }
            int n = A.CountColumn;

            Matrix r = new Matrix(n, 1);
            for (int i = 0; i < n; i++)
            {
                r[i, 0] = b_arg[i];
            }

            double[] delta = new double[n];
            double[] lambda = new double[n];

            delta[0] = -A[0, 1] / A[0, 0];
            lambda[0] = r[0, 0] / A[0, 0];
            for (int i = 1; i < n - 1; i++)
            {

                delta[i] = -A[i, i + 1] / (A[i, i] + A[i, i - 1] * delta[i - 1]);

                lambda[i] = (r[i, 0] - A[i, i - 1] * lambda[i - 1]) / (A[i, i] + A[i, i - 1] * delta[i - 1]);

            }
            double[] x = new double[n];
            x[n - 1] = (r[n - 1, 0] - A[n - 1, n - 2] * lambda[n - 2]) / (A[n - 1, n - 1] + A[n - 1, n - 2] * delta[n - 2]);

            for (int i = n - 2; i >= 1; i--)
            {
                x[i] = (-A[i, i + 1] / (A[i, i] + A[i, i - 1] * delta[i - 1])) * x[i + 1] + (r[i, 0] - A[i, i - 1] * lambda[i - 1]) / (A[i, i] + A[i, i - 1] * delta[i - 1]);

            }
            x[0] = (r[0, 0] - A[0, 1] * x[1]) / A[0, 0];

            return x;


        }


    }
}
