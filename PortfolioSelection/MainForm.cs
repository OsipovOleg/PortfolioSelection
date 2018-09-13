using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.IO;

namespace PortfolioSelection
{
    public partial class MainForm : Form
    {
        public MainForm()
        {
            InitializeComponent();
        }

        /// <summary>
        /// Обнобляет таблицу на форме с доходностью компаний
        /// </summary>
        public void UpdateGrid()
        {
            if (table != null)
            {
                GridIncome.Rows.Clear();
                //Запоняем названия компаний
                for (int i = 0; i < table.CompanyCount(); i++)
                {
                    GridIncome.Columns.Add(table.CompanyName[i], table.CompanyName[i]);
                }

                //Заполняем доходы
                for (int i = 0; i < table.RecordCount(); i++)
                {
                    GridIncome.Rows.Add();
                    for (int j = 0; j < table.CompanyCount(); j++)
                    {
                        GridIncome[j, i].Value = table.Price[i, j];
                    }

                }




            }
        }


        /// <summary>
        /// Таблица с ценами каждой компании
        /// </summary>
        TableOfPrice table;



        /// <summary>
        /// Считывает данные из файла по доходностям компаний
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void получитьДанныеItem_Click(object sender, EventArgs e)
        {

            openFile.InitialDirectory = Environment.GetFolderPath(Environment.SpecialFolder.DesktopDirectory);
            openFile.Filter = "csv files (*.csv)|*.txt|All files (*.*)|*.*";
            openFile.FilterIndex = 2;
            openFile.RestoreDirectory = true;

            if (openFile.ShowDialog() == DialogResult.OK)
            {
                table = new TableOfPrice(openFile.FileName);
                UpdateGrid();
            }
        }

        /// <summary>
        /// Выполняет генерацию оптимального портфеля по Марковицу
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void марковицItem_Click(object sender, EventArgs e)
        {
            double mp = double.Parse(this.mpText.Text);
            if (table != null)
            {
                double Dp;
                double[] x = Analysis.Markowitz(table, mp, out Dp);


                MessageBox.Show("Анализ успешно выполнен", "Отчет", MessageBoxButtons.OK, MessageBoxIcon.Information);

                ChartForm f = new ChartForm();

                var points = f.MainChart.Series[0].Points;
                points.Clear();
                for (int i = 0; i < x.Length; i++)
                {
                    points.AddY(x[i]);


                }
                f.ShowDialog();
                DpText.Text = string.Format("{0:f4}", Dp);
                GridXResult(x);
            }


        }


        private void GridXResult(double[] x)
        {
            //Заполняем доходы
            GridX.Rows.Clear();
            for (int i = 0; i < x.Length; i++)
            {
                GridX.Rows.Add();

                GridX[0, i].Value = table.CompanyName[i];
                GridX[1, i].Value = String.Format("{0:f4}%", x[i] * 100);

            }

        }

        private void шарпItem_Click(object sender, EventArgs e)
        {

            if (table != null)
            {
                double mp = double.Parse(this.mpText.Text);
                openFile.InitialDirectory = Environment.GetFolderPath(Environment.SpecialFolder.DesktopDirectory);
                openFile.Filter = "csv files (*.csv)|*.txt|All files (*.*)|*.*";
                openFile.FilterIndex = 2;
                openFile.RestoreDirectory = true;
                openFile.FileName = "PriceI.csv";

                //Цены доп портфеля
                double[] PriceI = new double[table.RecordCount()];
                if (openFile.ShowDialog() == DialogResult.OK)
                {
                    using (StreamReader sr = new StreamReader(openFile.FileName))
                    {
                        for (int i = 0; i < table.RecordCount(); i++)
                        {

                            PriceI[i] = double.Parse(sr.ReadLine());


                        }
                    }


                    //Если цены для эталонного портфеля получены ...
                    double Dp;
                    double[] x = Analysis.Sharp(table, PriceI, mp, out Dp);


                    MessageBox.Show("Анализ успешно выполнен", "Отчет", MessageBoxButtons.OK, MessageBoxIcon.Information);

                    ChartForm f = new ChartForm();

                    var points = f.MainChart.Series[0].Points;
                    points.Clear();
                    for (int i = 0; i < x.Length; i++)
                    {
                        points.AddY(x[i]);


                    }
                    f.ShowDialog();
                    DpText.Text = string.Format("{0:f4}", Dp);
                    GridXResult(x);

                }


            }

        }

    }
}
