using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;

namespace PortfolioSelection
{
    public static class Analysis
    {
        /// <summary>
        /// Сохраняет массив в файл
        /// </summary>
        /// <param name="x"></param>
        /// <param name="path"></param>
        public static void SaveArray(double[] x, string path)
        {
            using (StreamWriter sw = new StreamWriter(path))
            {
                for (int i = 0; i < x.Length; i++)
                {
                    sw.WriteLine(string.Format("{0:f4}", x[i]));
                }
            }

        }




        /// <summary>
        /// Генерирует оптимальный портфель по Марковицу
        /// </summary>
        /// <param name="table">Таблица с ценами акций </param>
        /// <param name="mp">Ограничение для мат. ожидания в процентах</param>
        /// <param name="Dp">Возвращает дисперсию процентах</param>
        /// <returns></returns>
        public static double[] Markowitz(TableOfPrice table, double mp, out double Dp)
        {
            mp = mp / 100;
            //Число компаний
            int n = table.CompanyCount();
            //Количество данных о каждой(объемы выборок)
            int T = table.RecordCount();

            double[] x = new double[n];

            //Генерируем матрицу доходностей 
            Matrix incomes = new Matrix(T, n);

            //Заполгяем матрицу доходов
            for (int i = 1; i < incomes.CountRow; i++)
            {
                for (int j = 0; j < incomes.CountColumn; j++)
                {
                    //Доход за промежуток времени
                    incomes[i, j] = (table.Price[i, j] - table.Price[i - 1, j]) / table.Price[i - 1, j];
                }

            }
            //Запись матрицы в файл
            using (StreamWriter sw = new StreamWriter(Environment.CurrentDirectory + "/incomes.csv"))
            {
                sw.WriteLine(incomes.ToString());
            }



            //Подсчитаем теперь среднюю доходность и риск по каждой из акций
            double[] m = new double[n];
            double[] sigma = new double[n];

            //Средний доход
            for (int i = 0; i < m.Length; i++)
            {
                for (int j = 0; j < incomes.CountRow; j++)
                {
                    m[i] += incomes[j, i];
                }
                m[i] = m[i] / incomes.CountRow;


            }

            //Считаем риск(волантильность)
            for (int i = 0; i < m.Length; i++)
            {
                for (int j = 0; j < incomes.CountRow; j++)
                {
                    sigma[i] += (incomes[j, i] - m[i]) * (incomes[j, i] - m[i]);
                }
                sigma[i] = Math.Sqrt(sigma[i] / (incomes.CountRow - 1));


            }


            //Далее считаем матрицу ковариаций
            Matrix V = new Matrix(n, n);
            for (int i = 0; i < V.CountRow; i++)
            {
                for (int j = 0; j < V.CountColumn; j++)
                {
                    //Считаем среднее попарных произведений отклонений ( то есть ковариацию)
                    for (int k = 0; k < T; k++)
                    {
                        //Суммируем
                        V[i, j] += (incomes[k, i] - m[i]) * (incomes[k, j] - m[j]);
                    }
                    //Усредняем
                    V[i, j] = V[i, j] / T;
                }
            }

            //Запись ковариационной матрицы в файл
            using (StreamWriter sw = new StreamWriter(Environment.CurrentDirectory + "/V.csv"))
            {
                sw.WriteLine(V.ToString());
            }



            //Единичный столбец (матрица из одного столбца и n строк)
            Matrix I = new Matrix(n, 1);
            for (int i = 0; i < n; i++)
            {
                I[i, 0] = 1;
            }

            //Массив m как матрица
            Matrix M = new Matrix(n, 1);
            for (int i = 0; i < n; i++)
            {
                M[i, 0] = m[i];
            }

            //Получаем обратную матрицу
            Matrix Vinv = V.Inv();

            Matrix a11 = I.Transpose() * Vinv * I;
            Matrix a12 = I.Transpose() * Vinv * M;
            Matrix a22 = M.Transpose() * Vinv * M;
            double d = a11[0, 0] * a22[0, 0] - a12[0, 0] * a12[0, 0];

            Matrix b = (a22[0, 0] * (Vinv * I) - a12[0, 0] * (Vinv * M)) / d;
            Matrix c = (a11[0, 0] * (Vinv * M) - a12[0, 0] * (Vinv * I)) / d;



            Matrix X = b + mp * c;



            //Запись решения в файл
            using (StreamWriter sw = new StreamWriter(Environment.CurrentDirectory + "/X.csv"))
            {
                sw.WriteLine(X.ToString());
            }
            //Копируем в массив
            for (int i = 0; i < n; i++)
            {
                x[i] = X[i, 0];
            }

            //Подсчет дисперсии
            Dp = (X.Transpose() * V * X)[0, 0] * 100 * 100;

            return x;
        }




        /// <summary>
        /// Генерирует оптимальный портфель по Шарпу
        /// </summary>
        /// <param name="table">Таблица с ценами акций </param>
        /// <param name="mp">Ограничение для мат. ожидания в процентах</param>
        /// <param name="PriceI">Цена эталонного портфеля</param>
        /// <param name="Dp">Возвращает дисперсию процентах</param>
        /// <returns></returns>
        public static double[] Sharp(TableOfPrice table, double[] PriceI, double mp, out double Dp)
        {
            mp = mp / 100;
            //Число компаний
            int n = table.CompanyCount();
            //Количество данных о каждой(объемы выборок)
            int T = table.RecordCount();


            double[] x = new double[n];

            //Анализируем эталонный портфель 
            double[] RI = new double[T];
            for (int i = 1; i < RI.Length; i++)
            {
                //Доход за промежуток времени
                RI[i] = (PriceI[i] - PriceI[i - 1]) / PriceI[i - 1];

            }

            //Статичтические значения для эталонного портфеля
            //М.о. дохода
            double mI = 0;
            for (int i = 0; i < RI.Length; i++)
            {
                mI += RI[i];
            }
            mI = mI / RI.Length;

            //Дисперсии оценка
            double DI = 0;
            for (int i = 0; i < RI.Length; i++)
            {
                DI += (RI[i] - mI) * (RI[i] - mI);
            }
            DI = DI / (RI.Length - 1);



            //Генерируем матрицу доходностей 
            Matrix incomes = new Matrix(T, n);

            //Заполгяем матрицу доходов
            for (int i = 1; i < incomes.CountRow; i++)
            {
                for (int j = 0; j < incomes.CountColumn; j++)
                {
                    //Доход за промежуток времени
                    incomes[i, j] = (table.Price[i, j] - table.Price[i - 1, j]) / table.Price[i - 1, j];
                }

            }
            //Запись матрицы в файл
            using (StreamWriter sw = new StreamWriter(Environment.CurrentDirectory + "/incomes.csv"))
            {
                sw.WriteLine(incomes.ToString());
            }

            //Подсчитаем теперь среднюю доходность и риск по каждой из акций
            double[] m = new double[n];
            double[] D = new double[n];

            //Средний доход
            for (int i = 0; i < m.Length; i++)
            {
                for (int j = 0; j < incomes.CountRow; j++)
                {
                    m[i] += incomes[j, i];
                }
                m[i] = m[i] / incomes.CountRow;
            }

            //Считаем риск(волантильность)
            for (int i = 0; i < m.Length; i++)
            {
                for (int j = 0; j < incomes.CountRow; j++)
                {
                    D[i] += (incomes[j, i] - m[i]) * (incomes[j, i] - m[i]);
                }
                D[i] = D[i] / (incomes.CountRow - 1);


            }



            //Ковариация эталанного портфеля с остальными
            double[] VIi = new double[n];
            for (int i = 0; i < VIi.Length; i++)
            {
                for (int j = 0; j < T; j++)
                {
                    VIi[i] += (incomes[j, i] - m[i]) * (RI[j] - mI);
                }
                VIi[i] = VIi[i] / (T - 1);
            }

            //К-ты бетта
            double[] betta = new double[n];
            for (int i = 0; i < betta.Length; i++)
            {
                betta[i] = VIi[i] / DI;
            }

            //К-ты альфа
            double[] alpha = new double[n];
            for (int i = 0; i < alpha.Length; i++)
            {
                alpha[i] = m[i] - betta[i] * mI;
            }

            double[] psi = new double[n];
            for (int i = 0; i < psi.Length; i++)
            {
                for (int j = 0; j < T; j++)
                {
                    psi[i] += (incomes[j, i] - alpha[i] - betta[i] * RI[j]) * (incomes[j, i] - alpha[i] - betta[i] * RI[j]);
                }
                psi[i] = psi[i] / (T - 2);
            }




            //Далее считаем матрицу ковариаций
            Matrix V = new Matrix(n, n);
            for (int i = 0; i < V.CountRow; i++)
            {
                for (int j = 0; j < V.CountColumn; j++)
                {
                    if (i == j)
                    {
                        V[i, j] = betta[i] * betta[i] * DI + psi[i];
                    }
                    else
                    {
                        V[i, j] = betta[i] * betta[j] * DI;
                    }
                }
            }

            //Запись ковариационной матрицы в файл
            using (StreamWriter sw = new StreamWriter(Environment.CurrentDirectory + "/V.csv"))
            {
                sw.WriteLine(V.ToString());
            }



            //Единичный столбец (матрица из одного столбца и n строк)
            Matrix I = new Matrix(n, 1);
            for (int i = 0; i < n; i++)
            {
                I[i, 0] = 1;
            }

            //Массив M из регрессионной модели
            Matrix M = new Matrix(n, 1);
            for (int i = 0; i < n; i++)
            {
                M[i, 0] = alpha[i] + betta[i] * mI;
            }



            SaveArray(alpha, Environment.CurrentDirectory + "/alpha.csv");
            SaveArray(betta, Environment.CurrentDirectory + "/betta.csv");
            SaveArray(psi, Environment.CurrentDirectory + "/psi.csv");
            SaveArray(D, Environment.CurrentDirectory + "/D.csv");
            SaveArray(VIi, Environment.CurrentDirectory + "/VI.csv");
            double[] arr = new double[1];
            arr[0]=DI;
            SaveArray(arr, Environment.CurrentDirectory + "/DI.csv");


            //Получаем обратную матрицу
            Matrix Vinv = V.Inv();

            Matrix a11 = I.Transpose() * Vinv * I;
            Matrix a12 = I.Transpose() * Vinv * M;
            Matrix a22 = M.Transpose() * Vinv * M;
            double d = a11[0, 0] * a22[0, 0] - a12[0, 0] * a12[0, 0];

            Matrix b = (a22[0, 0] * (Vinv * I) - a12[0, 0] * (Vinv * M)) / d;
            Matrix c = (a11[0, 0] * (Vinv * M) - a12[0, 0] * (Vinv * I)) / d;





            Matrix X = b + mp * c;
            //Запись в файл
            using (StreamWriter sw = new StreamWriter(Environment.CurrentDirectory + "/b.csv"))
            {
                sw.WriteLine(b.ToString());
            }
            using (StreamWriter sw = new StreamWriter(Environment.CurrentDirectory + "/c.csv"))
            {
                sw.WriteLine(c.ToString());
            }


            //Запись решения в файл
            using (StreamWriter sw = new StreamWriter(Environment.CurrentDirectory + "/X.csv"))
            {
                sw.WriteLine(X.ToString());
            }
            //Копируем в массив
            for (int i = 0; i < n; i++)
            {
                x[i] = X[i, 0];
            }

            //Подсчет дисперсии
            Dp = (X.Transpose() * V * X)[0, 0] * 100 * 100;

            return x;
        }

    }
}
