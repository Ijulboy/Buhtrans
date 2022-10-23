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
    public partial class frmPeriodSelection2 : Form
    {
        public frmPeriodSelection2()
        {
            InitializeComponent();
        }

        public static DateTime start, end, start2, end2;
        DataSet ds;
        DataTable dt;
        DataSet ds2;
        DataTable dt2;
        DataSet ds3;
        int i = 0;//перебор путевых листов
        double a, c = 0;
        double sumMileageTotal, sumMileageTotal2 = 0;
        double sumWeight, sumWeight2 = 0;
        double sumCargoTurnover, sumCargoTurnover2 = 0;
        double a2, a4, a5 = 0; //вспомогательные переменные    
        string quarter; //номер квартала


        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btnForm_Click(object sender, EventArgs e)
        {
            start = dtpStartDate.Value;
            end = dtpEndDate.Value;
            start2 = start.AddYears(-1);
            end2 = end.AddYears(-1);


            //таблица путевых листов
            try
            {
                using (DBConnection.connection = new SqlConnection(DBConnection.connectionString))
                {
                    DBConnection.connection.Open();
                    SqlDataAdapter dataAdapter3 = new SqlDataAdapter("SELECT W.Waybill_Number, W.Arrival_Date FROM Waybill W", DBConnection.connection);
                    ds3 = new DataSet();
                    dataAdapter3.Fill(ds3);//буферная таблица  
                    DBConnection.connection.Close();
                }

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }

            if (start.Day == 01 && start.Month == 01 && end.Day == 31 && end.Month == 03) quarter = "1";
            else if (start.Day == 01 && start.Month == 04 && end.Day == 30 && end.Month == 06) quarter = "2";
            else if (start.Day == 01 && start.Month == 07 && end.Day == 30 && end.Month == 09) quarter = "3";
            else if (start.Day == 01 && start.Month == 10 && end.Day == 31 && end.Month == 12) quarter = "4";
            else MessageBox.Show("Выбранный период для отчета не является кварталом!");

            for (int u = 0; u < ds3.Tables[0].Rows.Count; u++)
            {
                if (Convert.ToDateTime(ds3.Tables[0].Rows[u][1]) >= start && Convert.ToDateTime(ds3.Tables[0].Rows[u][1]) <= end)
                {
                    i = Convert.ToInt32(ds3.Tables[0].Rows[u][0]);
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
                            sumMileageTotal += a2;                            
                        }

                        catch (Exception ex)
                        {
                            MessageBox.Show(ex.Message);
                        }
                    }

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
                        }
                    }
                    catch (Exception ex)
                    {
                        //MessageBox.Show(ex.Message);
                    }
                }

                if (Convert.ToDateTime(ds3.Tables[0].Rows[u][1]) >= start2 && Convert.ToDateTime(ds3.Tables[0].Rows[u][1]) <= end2)
                {
                    i = Convert.ToInt32(ds3.Tables[0].Rows[u][0]);
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
                            sumMileageTotal2 += a2;
                        }

                        catch (Exception ex)
                        {
                            MessageBox.Show(ex.Message);
                        }
                    }

                    //расчеты по таблице маршрута ПРОБЕГИ (грузооборот, перевезено)
                    try
                    {
                        for (int y = 0; y < ds.Tables[0].Rows.Count; y++)
                        {
                            a4 = Convert.ToDouble(ds.Tables[0].Rows[y][4]); //присваиваем вес из каждой строки
                            a5 = Convert.ToDouble(ds.Tables[0].Rows[y][3]) * Convert.ToDouble(ds.Tables[0].Rows[y][4]); //присваиваем грузооборот из каждой строки
                            sumCargoTurnover2 += a5; //накапливаем грузооборот
                            if (y == 0) { sumWeight2 += Convert.ToDouble(ds.Tables[0].Rows[y][4]); } //присваиваем изначально вес первой строки
                            if (string.IsNullOrEmpty(ds.Tables[0].Rows[y][4].ToString())) //если в строке не указан вес
                            { sumWeight2 = Convert.ToDouble(ds.Tables[0].Rows[y + 1][4]); }//то присваиваем вес следующей строки
                            if ((Convert.ToDouble(ds.Tables[0].Rows[y][4]) - Convert.ToDouble(ds.Tables[0].Rows[y + 1][4])) < 0)
                            {
                                c = (Convert.ToDouble(ds.Tables[0].Rows[y][4]) - Convert.ToDouble(ds.Tables[0].Rows[y + 1][4])) * (-1);
                                sumWeight2 += c;
                            }
                        }
                    }
                    catch (Exception ex)
                    {
                        //MessageBox.Show(ex.Message);
                    }
                }
            }
            sumWeight = sumWeight / 1000; //перевезено всего тыс. т. за текущий период          
            sumCargoTurnover = sumCargoTurnover / 1000; //грузооборот общий тыс.т-км за текущий период
            sumMileageTotal = sumMileageTotal / 1000; //общий пробег тыс. км за текущий период
            sumWeight2 = sumWeight2 / 1000; //перевезено всего тыс. т. за прошлый год         
            sumCargoTurnover2 = sumCargoTurnover2 / 1000; //грузооборот общий тыс.т-км за прошлый год
            sumMileageTotal2 = sumMileageTotal2 / 1000; //общий пробег тыс. км за прошлый год

            Excel.Application oXL;
            Excel._Workbook oWB;
            Excel._Worksheet oSheet;
            Excel.Range oRng;

            oXL = new Excel.Application();
            oWB = (Excel._Workbook)(oXL.Workbooks.Open(Environment.CurrentDirectory + "\\ШаблонСтатистика4ТР.xltx"));
            oXL.Visible = true;
            oSheet = (Excel._Worksheet)oWB.ActiveSheet; 
            oSheet.get_Range("quarter").Value2 = quarter; 
            oSheet.get_Range("year").Value2 = end.ToString("yy"); 
            oSheet.get_Range("weight1").Value2 = sumWeight;
            oSheet.get_Range("cargoTurnover1").Value2 = sumCargoTurnover;
            oSheet.get_Range("mileage1").Value2 = sumMileageTotal;
            oSheet.get_Range("weight2").Value2 = sumWeight2;
            oSheet.get_Range("cargoTurnover2").Value2 = sumCargoTurnover2;
            oSheet.get_Range("mileage2").Value2 = sumMileageTotal2;
        }
    }
}
