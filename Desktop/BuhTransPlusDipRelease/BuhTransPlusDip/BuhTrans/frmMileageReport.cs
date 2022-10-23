using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Data.SqlClient;
using System.Reflection;
using Excel = Microsoft.Office.Interop.Excel;
using Word = Microsoft.Office.Interop.Word;
using DataTable = System.Data.DataTable;

namespace BuhTrans
{
    public partial class frmMileageReport : Form 
    {
        DataSet ds;
        DataTable dt;
        DataSet ds2;
        DataTable dt2;
        DataSet ds3;
        int i = 0;//перебор путевых листов
        double a, c = 0;
        double sumMileageTotal = 0;
        double sumMileageCargo = 0;
        double sumWeight = 0;
        double sumWeightTransit, sumWeightEAEU, sumCargoTurnover, sumCargoTurnoverTransit, sumCargoTurnoverEAEU = 0;

        private void label1_TextChanged(object sender, EventArgs e)
        {
            label1.Left = this.Width / 2 - label1.Width / 2;//привели в центр
        }

        private void label2_TextChanged(object sender, EventArgs e)
        {
            label2.Left = this.Width / 2 - label2.Width / 2;//привели в центр
        }

        double a2, a3, a4, a5 = 0; //вспомогательные переменные
        //int b = 0; //используется для цикла
        int ride = 0; //кол-во ездок
        DateTime start = frmPeriodSelection.start;
        DateTime end = frmPeriodSelection.end;
        


        public frmMileageReport()
        {
            InitializeComponent();
            tbStart.Text = Convert.ToString(start.ToString("dd.MM.yyyy"));
            tbEnd.Text = Convert.ToString(end.ToString("dd.MM.yyyy"));

            //таблица путевых листов
            try
            {
                using (DBConnection.connection = new SqlConnection(DBConnection.connectionString))
                {
                    DBConnection.connection.Open();
                    SqlDataAdapter dataAdapter2 = new SqlDataAdapter("SELECT W.Waybill_Number, W.Arrival_Date FROM Waybill W", DBConnection.connection);
                    ds3 = new DataSet();
                    dataAdapter2.Fill(ds3);//буферная таблица  
                    DBConnection.connection.Close();
                }

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }

            for (int u = 0; u < ds3.Tables[0].Rows.Count; u++)
            {
                if (Convert.ToDateTime(ds3.Tables[0].Rows[u][1]) >= start && Convert.ToDateTime(ds3.Tables[0].Rows[u][1]) <= end)
                {
                    i = Convert.ToInt32(ds3.Tables[0].Rows[u][0]);
                    //for (i = 1; i < 100; i++)
                    //{
                    //таблица маршрута
                    try
                    {
                        using (DBConnection.connection = new SqlConnection(DBConnection.connectionString))
                        {
                            DBConnection.connection.Open();
                            SqlDataAdapter dataAdapter = new SqlDataAdapter("SELECT Country [Страна], I.Starting_Point [Начальный пункт], I.Final_Point [Конечный пункт], I.Mileage [Пробег], I.Cargo_Weight [Вес груза т], I.CMR [Номер СМР], I.Label_Transit [Транзит], I.Label_EAEU_Countries [Страны ЕАЭС] FROM Itinerary I WHERE I.Id_Waybill = @itinerary", DBConnection.connection);
                            dataAdapter.SelectCommand.Parameters.Add("@itinerary", SqlDbType.Int).Value = i;
                            ds = new DataSet();
                            dataAdapter.Fill(ds);//буферная таблица
                            DBConnection.connection.Close();
                        }
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show(ex.Message);
                    }
                    //таблица заправок
                    try
                    {
                        using (DBConnection.connection = new SqlConnection(DBConnection.connectionString))
                        {
                            DBConnection.connection.Open();
                            SqlDataAdapter dataAdapter2 = new SqlDataAdapter("SELECT R.Date_Refill [Дата заправки], R.Volume [Количество] FROM Refill R WHERE R.Id_Waybill = @numberWaybill", DBConnection.connection);
                            dataAdapter2.SelectCommand.Parameters.Add("@numberWaybill", SqlDbType.Int).Value = i;
                            ds2 = new DataSet();
                            dataAdapter2.Fill(ds2);//буферная таблица  
                            DBConnection.connection.Close();
                        }

                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show(ex.Message);
                    }
                    //расчеты по таблице маршрута ПРОБЕГИ
                    for (int y = 0; y < ds.Tables[0].Rows.Count; y++)
                    {
                        try
                        {
                            a2 = Convert.ToDouble(ds.Tables[0].Rows[y][3]);
                            a3 = Convert.ToDouble(ds.Tables[0].Rows[y][3]);
                            sumMileageTotal += a2;
                            //b++;
                            if (Convert.ToDouble(ds.Tables[0].Rows[y][4]) != 0)
                            {
                                sumMileageCargo += a3;
                                ride++;
                            }
                        }

                        catch (Exception ex)
                        {
                            MessageBox.Show(ex.Message);
                        }
                    }
                    

                    //MessageBox.Show(Convert.ToString(sumMileageTotal));
                    //расчеты по таблице маршрута ПРОБЕГИ (грузооборот, перевезено)
                    try
                    {
                        for (int y = 0; y < ds.Tables[0].Rows.Count; y++)
                        {
                            a4 = Convert.ToDouble(ds.Tables[0].Rows[y][4]); //присваиваем вес из каждой строки
                            a5 = Convert.ToDouble(ds.Tables[0].Rows[y][3]) * Convert.ToDouble(ds.Tables[0].Rows[y][4]); //присваиваем грузооборот из каждой строки
                            sumCargoTurnover += a5; //накапливаем грузооборот
                            if (y == 0) { sumWeight += Convert.ToDouble(ds.Tables[0].Rows[y][4]); } //присваиваем изначально вес первой строки
                            if (string.IsNullOrEmpty(ds.Tables[0].Rows[y][4].ToString())) //если в строке не указан вес
                            { sumWeight = Convert.ToDouble(ds.Tables[0].Rows[y + 1][4]); }//то присваиваем вес следующей строки
                            if ((Convert.ToDouble(ds.Tables[0].Rows[y][4]) - Convert.ToDouble(ds.Tables[0].Rows[y + 1][4])) < 0)
                            {
                                c = (Convert.ToDouble(ds.Tables[0].Rows[y][4]) - Convert.ToDouble(ds.Tables[0].Rows[y + 1][4])) * (-1);
                                sumWeight += c;
                            }
                            if (ds.Tables[0].Rows[y][6].ToString() == "True")
                            {
                                sumWeightTransit += a4;
                                sumCargoTurnoverTransit += a5;
                            }
                            if (ds.Tables[0].Rows[y][7].ToString() == "True")
                            {
                                sumWeightEAEU += a4;
                                sumCargoTurnoverEAEU += a5;
                            }
                        }
                    }
                    catch (Exception ex)
                    {
                        //MessageBox.Show(ex.Message);
                    }
                    //sumWeight = sumWeight / 1000; //перевезено всего тыс. т.
                    //sumWeightTransit = sumWeightTransit / 1000; //перевезено всего транзитом тыс. т.
                    //sumWeightEAEU = sumWeightEAEU / 1000; //перевезето всего между странами ЕАЭС тыс. т.
                    //sumCargoTurnover = sumCargoTurnover / 1000; //грузооборот общий тыс.т-км
                    //sumCargoTurnoverTransit = sumCargoTurnoverTransit / 1000; //грузооборот транзитом тыс. т-км
                    //sumCargoTurnoverEAEU = sumCargoTurnoverEAEU / 1000; //грузооборот между странами ЕАЭС тыс. т-км                
                                                                        //}
                }
            }
            //sumMileageTotal = sumMileageTotal / 1000; //весь пробег тыс. км
            //sumMileageCargo = sumMileageCargo / 1000; //пробег с грузом тыс. км

            dvgMileageReport.Rows[0].Cells[0].Value = sumMileageTotal;
            dvgMileageReport.Rows[0].Cells[1].Value = sumMileageCargo;
            dvgMileageReport.Rows[0].Cells[2].Value = sumWeight;
            dvgMileageReport.Rows[0].Cells[3].Value = sumWeightTransit;
            dvgMileageReport.Rows[0].Cells[4].Value = sumWeightEAEU;
            dvgMileageReport.Rows[0].Cells[5].Value = sumCargoTurnover;
            dvgMileageReport.Rows[0].Cells[6].Value = sumCargoTurnoverTransit;
            dvgMileageReport.Rows[0].Cells[7].Value = sumCargoTurnoverEAEU;
            dvgMileageReport.Rows[0].Cells[8].Value = ride;
        }
    }
}

