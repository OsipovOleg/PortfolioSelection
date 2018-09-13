using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;

namespace PortfolioSelection
{
    /// <summary>
    /// Таблица с ценами акций
    /// </summary>
    public class TableOfPrice
    {
        //Матрица цен по каждой компании
        public double[,] Price;

        /// <summary>
        /// Число компаний 
        /// </summary>
        /// <returns></returns>
        public int CompanyCount()
        {
            return Price.GetLength(1);
        }
        /// <summary>
        /// Число записей о каждой компании
        /// </summary>
        /// <returns></returns>
        public int RecordCount()
        {
            return Price.GetLength(0);
        }

        /// <summary>
        /// Имена каждой компании
        /// </summary>
        public string[] CompanyName;

        /// <summary>
        /// Считываем данные по каждой из компаний из файла 
        /// данные записаны в csv файл (разделители столбцов ;) 
        /// </summary>
        /// <param name="path"></param>
        public TableOfPrice(string path)
        {
            using (StreamReader sr = new StreamReader(path))
            {
                //Считываем количество данных
                int countRecord = int.Parse(sr.ReadLine());
                //Считываем имена компаний
                CompanyName = sr.ReadLine().Split(';');

                //Создаем массив цен, заданной размерности
                Price = new double[countRecord, CompanyName.Length];

                for (int i = 0; i < countRecord; i++)
                {

                    string[] temp = sr.ReadLine().Split(';');
                    //Считываем из файла строки и преобразуем их в числа
                    for (int j = 0; j < temp.Length; j++)
                    {
                        Price[i, j] = Double.Parse(temp[j]);
                    }
                }

            }
        }



    }
}
