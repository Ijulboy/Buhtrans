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
    public partial class frmWaybillsContent : Form 
    {
            DataSet ds;
            DataTable dt;
            DataSet ds2;
            DataTable dt2;
            DataSet ds3;
            DataSet dsWay;
            //DataTable dt3;
            double a, c = 0;
            double sumRefill = 0;
            double sumMileageTotal = 0;
            double sumMileageCargo = 0;
            double sumWeight = 0;
            double sumWeightTransit, sumWeightEAEU, sumCargoTurnover, sumCargoTurnoverTransit, sumCargoTurnoverEAEU = 0;
            double a2, a3, a4, a5 = 0;
            int b = 0;
            int rows = frmWaybillList.a;
            int ride = 0; //кол-во ездок
            double norm, ratio = 0;


        public frmWaybillsContent()
        {
                InitializeComponent();

                dgvItinerary.SelectionMode = DataGridViewSelectionMode.FullRowSelect; //выделение сразу всей строки для удаления
                dgvItinerary.AllowUserToAddRows = true;
                dgvRefill.SelectionMode = DataGridViewSelectionMode.FullRowSelect; //выделение сразу всей строки для удаления
                dgvRefill.AllowUserToAddRows = false;            
                tbNumberWaybill.Text = Convert.ToString(rows);

            ///Обновление Waybill
            try
            {
                using (DBConnection.connection = new SqlConnection(DBConnection.connectionString))
                {
                    DBConnection.connection.Open();
                    DBConnection.adapter = new SqlDataAdapter(DBConnection.spGetAllWaybill, DBConnection.connection);
                    dsWay = new DataSet();
                    DBConnection.adapter.Fill(dsWay);//буферная таблица
                   
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }

            ////списки
            try
                {
                    using (DBConnection.connection = new SqlConnection(DBConnection.connectionString))
                    {
                        DBConnection.connection.Open();
                        SqlDataAdapter dataAdapter3 = new SqlDataAdapter("SELECT C.Id_Car, C.Name_Number_Car, C.Trailer_Number FROM Car C RIGHT OUTER JOIN Waybill W ON W.Id_Car = C.Id_Car WHERE W.Waybill_Number = @numWay", DBConnection.connection);
                        dataAdapter3.SelectCommand.Parameters.Add("@numWay", SqlDbType.Int).Value = rows;
                        ds = new DataSet();
                        dataAdapter3.Fill(ds);//буферная таблица
                        this.cbCar.DataSource = ds.Tables[0];
                        this.cbCar.DisplayMember = "Name_Number_Car";
                        this.cbCar.ValueMember = "Name_Number_Car";
                        this.cbCar.SelectedIndex = 0;
                        this.cbTrailer.DataSource = ds.Tables[0];
                        this.cbTrailer.DisplayMember = "Trailer_Number";
                        this.cbTrailer.ValueMember = "Trailer_Number";
                        this.cbTrailer.SelectedIndex = 0;
                        DBConnection.connection.Close();
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }

                try
                {
                    using (DBConnection.connection = new SqlConnection(DBConnection.connectionString))
                    {
                        DBConnection.connection.Open();
                        SqlDataAdapter dataAdapter4 = new SqlDataAdapter("SELECT F.Type_Fuel FROM Fuel F JOIN Refill R ON R.Id_Fuel = F.Id_Fuel JOIN Waybill W ON W.Waybill_Number = R.Id_Waybill WHERE W.Waybill_Number = @numWay OR W.Waybill_Number= 1 GROUP BY F.Type_Fuel", DBConnection.connection);
                        dataAdapter4.SelectCommand.Parameters.Add("@numWay", SqlDbType.Int).Value = rows;
                        ds = new DataSet();
                        dataAdapter4.Fill(ds);//буферная таблица
                        this.cbFuel.DataSource = ds.Tables[0];
                        this.cbFuel.DisplayMember = "";
                        this.cbFuel.ValueMember = "Type_Fuel";
                        this.cbFuel.SelectedIndex = 0;
                        DBConnection.connection.Close();
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }

                try
                {
                    using (DBConnection.connection = new SqlConnection(DBConnection.connectionString))
                    {
                        DBConnection.connection.Open();
                        SqlDataAdapter dataAdapter5 = new SqlDataAdapter("SELECT E.Name FROM Employee E RIGHT OUTER JOIN Waybill W ON W.Id_Driver = E.Id_Employee WHERE W.Waybill_Number = @numWay", DBConnection.connection);
                        dataAdapter5.SelectCommand.Parameters.Add("@numWay", SqlDbType.Int).Value = rows;
                        ds = new DataSet();
                        dataAdapter5.Fill(ds);//буферная таблица
                        this.cbDriver.DataSource = ds.Tables[0];
                        this.cbDriver.DisplayMember = "";
                        this.cbDriver.ValueMember = "Name";
                        this.cbDriver.SelectedIndex = 0;
                        DBConnection.connection.Close();
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }

                try
                {
                    using (DBConnection.connection = new SqlConnection(DBConnection.connectionString))
                    {
                        DBConnection.connection.Open();
                        SqlDataAdapter dataAdapter6 = new SqlDataAdapter("SELECT W.Speedometer_Arrival, W.Speedometer_Departure, W.Arrival_Date, W.Departure_Date, W.Remaining_Fuel_Arrival, W.Remaining_Fuel_Departure FROM Waybill W WHERE W.Waybill_Number = @numWay", DBConnection.connection);
                        dataAdapter6.SelectCommand.Parameters.Add("@numWay", SqlDbType.Int).Value = rows;
                        ds = new DataSet();
                        dataAdapter6.Fill(ds);//буферная таблица                    
                        this.tbSpeedometerDeparture.Text = ds.Tables[0].Rows[0][1].ToString();
                        this.tbSpeedometerArrival.Text = ds.Tables[0].Rows[0][0].ToString();
                        this.tbFuelDeparture.Text = ds.Tables[0].Rows[0][5].ToString();
                        this.tbFuelArrival.Text = ds.Tables[0].Rows[0][4].ToString();
                        this.dtpDepartureDate.Value = Convert.ToDateTime(ds.Tables[0].Rows[0][3]);
                        this.dtpArrivalDate.Value = Convert.ToDateTime(ds.Tables[0].Rows[0][2]);
                        DBConnection.connection.Close();                        
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }
            ////////////////

            try
                {
                    using (DBConnection.connection = new SqlConnection(DBConnection.connectionString))
                    {
                    DBConnection.connection.Open();
                    SqlDataAdapter dataAdapter2 = new SqlDataAdapter("SELECT Country [Страна], I.Starting_Point [Начальный пункт], I.Final_Point [Конечный пункт], I.Mileage [Пробег], I.Cargo_Weight [Вес груза т], I.CMR [Номер СМР], I.Label_Transit [Транзит], I.Label_EAEU_Countries [Страны ЕАЭС] FROM Itinerary I WHERE I.Id_Waybill = @itinerary", DBConnection.connection);
                    dataAdapter2.SelectCommand.Parameters.Add("@itinerary", SqlDbType.Int).Value = rows;
                    ds = new DataSet();
                    dataAdapter2.Fill(ds);//буферная таблица
                    dgvItinerary.DataSource = ds.Tables[0];
                        //таблица растягивается на все окно
                        foreach (DataGridViewColumn column in dgvItinerary.Columns)
                        {
                            column.AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
                        }
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }

                try
                {
                using (DBConnection.connection = new SqlConnection(DBConnection.connectionString))
                {
                    DBConnection.connection.Open();
                    SqlDataAdapter dataAdapter = new SqlDataAdapter("SELECT R.Date_Refill [Дата заправки], R.Volume [Количество] FROM Refill R WHERE R.Id_Waybill = @numberWaybill", DBConnection.connection);
                    dataAdapter.SelectCommand.Parameters.Add("@numberWaybill", SqlDbType.Int).Value = rows;
                    ds2 = new DataSet();
                    dataAdapter.Fill(ds2);
                    dgvRefill.DataSource = ds2.Tables[0];
                }
                    //таблица растягивается на все окно
                    foreach (DataGridViewColumn column in dgvRefill.Columns)
                    {
                        column.AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
                    }
                }                
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }
                finally{
                DBConnection.connection.Close();
                }

            try
            {
                using (DBConnection.connection = new SqlConnection(DBConnection.connectionString))
                {
                    DBConnection.connection.Open();
                    DBConnection.adapter = new SqlDataAdapter(DBConnection.spGetAllNormFuel, DBConnection.connection);
                    ds3 = new DataSet();
                    DBConnection.adapter.Fill(ds3);//буферная таблица
                    //DBConnection.connection.Close();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void btnDeleteStagesMovement_Click_1(object sender, EventArgs e)
        {
            foreach (DataGridViewRow row in dgvItinerary.SelectedRows)
                dgvItinerary.Rows.Remove(row);
        }

        private void dgvItinerary_UserDeletingRow_1(object sender, DataGridViewRowCancelEventArgs e)
        {
            DialogResult dialogResult = MessageBox.Show("Вы хотите удалить эту строку?", "Удалить", MessageBoxButtons.OKCancel, MessageBoxIcon.Warning, MessageBoxDefaultButton.Button2); //button2 ПО умолчанию cancel
            if (dialogResult == DialogResult.Cancel)
                e.Cancel = true;
        }

        private void Calculation_Click(object sender, EventArgs e)
        {
            sumMileageTotal = 0;
            sumMileageCargo = 0;
            sumWeight = 0;
            sumWeightTransit = 0;
            sumWeightEAEU = 0;
            sumCargoTurnover = 0;
            sumCargoTurnoverTransit = 0;
            sumCargoTurnoverEAEU = 0;
            ride = 0;

            b = 0;

            sumRefill = 0;

            //вывод нормы расхода топлива
            for (int i = 0; i < ds3.Tables[0].Rows.Count; i++)
            {
                if (cbCar.Text == ds3.Tables[0].Rows[i]["Автомобиль"].ToString())
                {
                    tbNormFuel.Text = ds3.Tables[0].Rows[i]["Норма расхода"].ToString();
                    norm = Convert.ToDouble(ds3.Tables[0].Rows[i]["Норма расхода"].ToString());
                    ratio = Convert.ToDouble(ds3.Tables[0].Rows[i]["Коэффициент грузооборота"].ToString());
                }
            }

            foreach (DataGridViewRow row in dgvRefill.Rows)
            {
                try
                {
                    a = Convert.ToDouble(row.Cells[1].Value);
                    sumRefill += a;
                    tbTotal.Text = Convert.ToString(sumRefill);
                    tbSumRefill.Text = Convert.ToString(sumRefill);
                }
                catch (Exception ex)
                {
                    sumRefill = 0;
                }
            }

            foreach (DataGridViewRow row in dgvItinerary.Rows)
            {
                try
                {
                    a2 = Convert.ToDouble(row.Cells[3].Value);
                    a3 = Convert.ToDouble(row.Cells[3].Value);
                    sumMileageTotal += a2;
                    b++;
                    if (Convert.ToDouble(row.Cells[4].Value) != 0)
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
            tbMileageTotal.Text = Convert.ToString(sumMileageTotal / 1000);
            tbMileageCargo.Text = Convert.ToString(sumMileageCargo / 1000);
            try
            {
                for (int i = 0; i < b - 1; i++)
                {
                    a4 = Convert.ToDouble(dgvItinerary.Rows[i].Cells[4].Value);
                    a5 = Convert.ToDouble(dgvItinerary.Rows[i].Cells[3].Value) * Convert.ToDouble(dgvItinerary.Rows[i].Cells[4].Value);
                    sumCargoTurnover += a5;
                    if (i == 0) { sumWeight = Convert.ToDouble(dgvItinerary.Rows[i].Cells[4].Value); }
                    if (string.IsNullOrEmpty(dgvItinerary.Rows[i].Cells[4].Value.ToString()))
                    { sumWeight = Convert.ToDouble(dgvItinerary.Rows[i + 1].Cells[4].Value); }
                    if ((Convert.ToDouble(dgvItinerary.Rows[i].Cells[4].Value) - Convert.ToDouble(dgvItinerary.Rows[i + 1].Cells[4].Value)) < 0)
                    {
                        c = (Convert.ToDouble(dgvItinerary.Rows[i].Cells[4].Value) - Convert.ToDouble(dgvItinerary.Rows[i + 1].Cells[4].Value)) * (-1);
                        sumWeight += c;
                    }
                    if (dgvItinerary.Rows[i].Cells[6].Value.ToString() == "True")
                    {
                        sumWeightTransit += a4;
                        sumCargoTurnoverTransit += a5;
                    }
                    if (dgvItinerary.Rows[i].Cells[7].Value.ToString() == "True")
                    {
                        sumWeightEAEU += a4;
                        sumCargoTurnoverEAEU += a5;
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            tbTransportedTotal.Text = Convert.ToString(sumWeight / 1000);
            tbTransportedTransit.Text = Convert.ToString(sumWeightTransit / 1000);
            tbTransportedEAEU.Text = Convert.ToString(sumWeightEAEU / 1000);
            tbCargoTurnoverTotal.Text = Convert.ToString(sumCargoTurnover / 1000);
            tbCargoTurnoverTransit.Text = Convert.ToString(sumCargoTurnoverTransit / 1000);
            tbCargoTurnoverEAEU.Text = Convert.ToString(sumCargoTurnoverEAEU / 1000);
            tbFuelRates.Text = Convert.ToString((norm * sumMileageTotal + sumCargoTurnover * ratio) / 100);
        }

        private void btnFuelMeteringCard_Click(object sender, EventArgs e)
        {
            Excel.Application oXL;
            Excel._Workbook oWB;
            Excel._Worksheet oSheet;
            Excel.Range oRng;

            oXL = new Excel.Application();
            oWB = (Excel._Workbook)(oXL.Workbooks.Open(Environment.CurrentDirectory + "\\ШаблонКарточкаУчетаТоплива.xltx"));
            oXL.Visible = true;
            oSheet = (Excel._Worksheet)oWB.ActiveSheet;
            oSheet.get_Range("month").Value2 = dtpArrivalDate.Value.ToString("MMMM");
            oSheet.get_Range("year").Value2 = dtpArrivalDate.Value.ToString("yyyy");
            oSheet.get_Range("car").Value2 = Convert.ToString(cbCar.Text)+" / "+ Convert.ToString(cbTrailer.Text);
            oSheet.get_Range("typeFuel").Value2 = Convert.ToString(cbFuel.Text);
            oSheet.get_Range("linearRate").Value2 = Convert.ToString(tbNormFuel.Text);
            oSheet.get_Range("numberWay").Value2 = Convert.ToInt32(tbNumberWaybill.Text);
            oSheet.get_Range("dateDeparture").Value2 = dtpDepartureDate.Value.ToString("dd.MM.yy");
            oSheet.get_Range("dateArrival").Value2 = dtpArrivalDate.Value.ToString("dd.MM.yy");
            oSheet.get_Range("driver").Value2 = Convert.ToString(cbDriver.Text);
            oSheet.get_Range("speedometerDeparture").Value2 = Convert.ToString(tbSpeedometerDeparture.Text);
            //oSheet.get_Range("speedometerArrival").Value2 = Convert.ToString(tbSpeedometerArrival.Text);
            oSheet.get_Range("totalMileage").Value2 = Convert.ToString(sumMileageTotal);
            oSheet.get_Range("mileageCargo").Value2 = Convert.ToString(sumMileageCargo);
            oSheet.get_Range("cargoTransported").Value2 = Convert.ToString(sumWeight);
            oSheet.get_Range("rides").Value2 = Convert.ToString(ride);
            oSheet.get_Range("cargoTurnover").Value2 = Convert.ToString(sumCargoTurnover);
            oSheet.get_Range("fuelDeparture").Value2 = Convert.ToString(tbFuelDeparture.Text);
            oSheet.get_Range("fuelArrival").Value2 = Convert.ToString(tbFuelArrival.Text);
            oSheet.get_Range("fuelConsum").Value2 = Convert.ToDouble(tbFuelRates.Text);
            oSheet.get_Range("refill").Value2 = Convert.ToDouble(tbSumRefill.Text);
            char i = 'H';
            char u = 'I';
            for (int o=0; o<dgvItinerary.Rows.Count-1; o++)
            {
                try
                {
                    if (Convert.ToDouble(dgvItinerary.Rows[o].Cells[4].Value.ToString()) != 0)
                    {
                        oSheet.get_Range(i + "16").Value2 = Convert.ToInt32(dgvItinerary.Rows[o].Cells[3].Value);
                        oSheet.get_Range(u + "16").Value2 = Convert.ToDouble(dgvItinerary.Rows[o].Cells[4].Value);
                    }
                    i++; i++; i++;
                    u++; u++; u++;
                }
                catch { }
            }

        }

        private void btnExcel_Click(object sender, EventArgs e)
        {
            Excel.Application oXL;
            Excel._Workbook oWB;
            Excel._Worksheet oSheet;
            Excel.Range oRng;

            oXL = new Excel.Application();
            oWB = (Excel._Workbook)(oXL.Workbooks.Open(Environment.CurrentDirectory + "\\ШаблонПутевойЛист.xltx"));
            oXL.Visible = true;
            oSheet = (Excel._Worksheet)oWB.ActiveSheet;
            try
            {
                oSheet.get_Range("numWay").Value2 = Convert.ToInt32(tbNumberWaybill.Text);
                oSheet.get_Range("Date").Value2 = dtpDepartureDate.Value.ToString("dd");
                oSheet.get_Range("month").Value2 = dtpDepartureDate.Value.ToString("MM");
                oSheet.get_Range("yearWaybill").Value2 = dtpDepartureDate.Value.ToString("yy");
                oSheet.get_Range("carModel").Value2 = Convert.ToString(cbCar.Text);
                oSheet.get_Range("trailerModel").Value2 = Convert.ToString(cbTrailer.Text);
                oSheet.get_Range("driver").Value2 = Convert.ToString(cbDriver.Text);
                oSheet.get_Range("speedometerDeparture").Value2 = Convert.ToString(tbSpeedometerDeparture.Text);
                oSheet.get_Range("speedometerArrival").Value2 = Convert.ToString(tbSpeedometerArrival.Text);
                oSheet.get_Range("departureDate").Value2 = dtpDepartureDate.Value.ToString("dd.MM.yyyy");
                oSheet.get_Range("arrivalDate").Value2 = dtpArrivalDate.Value.ToString("dd.MM.yyyy");
                oSheet.get_Range("fuelDeparture").Value2 = Convert.ToString(tbFuelDeparture.Text);
                oSheet.get_Range("fuelArrival").Value2 = Convert.ToString(tbFuelArrival.Text);
                oSheet.get_Range("departure").Value2 = dtpDepartureDate.Value.ToString("dd.MM.yy");
                oSheet.get_Range("arrival").Value2 = dtpArrivalDate.Value.ToString("dd.MM.yy");
                oSheet.get_Range("rides").Value2 = Convert.ToString(ride);
                oSheet.get_Range("mileage").Value2 = Convert.ToString(sumMileageTotal);
                oSheet.get_Range("mileageCargo").Value2 = Convert.ToString(sumMileageCargo);
                oSheet.get_Range("totalTransported").Value2 = Convert.ToString(sumWeight);
                oSheet.get_Range("cargoTurnover").Value2 = Convert.ToString(sumCargoTurnover);
                oSheet.get_Range("fuelConsumption").Value2 = Convert.ToDouble(tbFuelRates.Text);
                int i = 23;
                foreach (DataGridViewRow row in dgvRefill.Rows)
                {
                    try
                    {
                        oSheet.get_Range("AY" + i).Value2 = Convert.ToDateTime(row.Cells[0].Value).ToString("dd.MM.yyyy");
                        oSheet.get_Range("BS" + i).Value2 = Convert.ToString(cbFuel.Text);
                        oSheet.get_Range("CM" + i).Value2 = Convert.ToString(row.Cells[1].Value);
                        i++;
                    }

                    catch (Exception ex)
                    {
                        MessageBox.Show(ex.Message);
                    }
                }
                int j = 37;
                foreach (DataGridViewRow row in dgvItinerary.Rows)
                {
                    try
                    {
                        oSheet.get_Range("AW" + j).Value2 = Convert.ToString(row.Cells[1].Value);
                        oSheet.get_Range("BQ" + j).Value2 = Convert.ToString(row.Cells[2].Value);
                        oSheet.get_Range("CK" + j).Value2 = Convert.ToString(row.Cells[3].Value);
                        oSheet.get_Range("DQ" + j).Value2 = Convert.ToDouble(row.Cells[4].Value);
                        if (string.IsNullOrEmpty(Convert.ToString(row.Cells[4].Value)) == false)
                        {
                            oSheet.get_Range("DA" + j).Value2 = Convert.ToString("Сборный груз");
                        }
                        j++;
                    }

                    catch (Exception ex)
                    {
                        MessageBox.Show(ex.Message);
                    }
                }
            }
            catch (Exception ex) { MessageBox.Show("Вы не рассчитали экономические показатели для заполнения формы!"); }
        }

        private void btnAddRefill_Click_1(object sender, EventArgs e)
        {
            DataRow row = ds2.Tables[0].NewRow();
            ds2.Tables[0].Rows.Add(row);
        }

        private void btnDeleteRefill_Click_1(object sender, EventArgs e)
        {
            foreach (DataGridViewRow row in dgvRefill.SelectedRows)
                dgvRefill.Rows.Remove(row);
        }

        private void dgvRefill_UserDeletingRow_1(object sender, DataGridViewRowCancelEventArgs e)
        {
            DialogResult dialogResult = MessageBox.Show("Вы хотите удалить эту строку?", "Удалить", MessageBoxButtons.OKCancel, MessageBoxIcon.Warning, MessageBoxDefaultButton.Button2); //button2 ПО умолчанию cancel
            if (dialogResult == DialogResult.Cancel)
                e.Cancel = true;
        }

        private void btnClose_Click_1(object sender, EventArgs e)
        {
            Form.ActiveForm.Close();
        }

        private void btnSave_Click_1(object sender, EventArgs e)
        {
            try
            {
                using (DBConnection.connection = new SqlConnection(DBConnection.connectionString))
                {
                    DBConnection.connection.Open();
                    DBConnection.command = new SqlCommand(DBConnection.spUpdateWaybill, DBConnection.connection);
                    DBConnection.command.CommandType = CommandType.StoredProcedure;
                    DBConnection.command.Parameters.Add(new SqlParameter("@waybillNumber", tbNumberWaybill.Text));
                    DBConnection.command.Parameters.Add(new SqlParameter("@departureDate", dtpDepartureDate.Value));
                    DBConnection.command.Parameters.Add(new SqlParameter("@arrivalDate", dtpArrivalDate.Value));
                    DBConnection.command.Parameters.Add(new SqlParameter("@speedometerDeparture", tbSpeedometerDeparture.Text));
                    DBConnection.command.Parameters.Add(new SqlParameter("@speedometerArrival", tbSpeedometerArrival.Text));
                    DBConnection.command.Parameters.Add(new SqlParameter("@fuelDeparture", Convert.ToDecimal(tbFuelDeparture.Text)));
                    DBConnection.command.Parameters.Add(new SqlParameter("@fuelArrival", Convert.ToDecimal(tbFuelArrival.Text)));
                    DBConnection.command.Parameters.Add(new SqlParameter("@typeFuel", cbFuel.Text));
                    DBConnection.command.Parameters.Add(new SqlParameter("@nameCar", cbCar.Text));
                    DBConnection.command.Parameters.Add(new SqlParameter("@nameDriver", cbDriver.Text));
                    DBConnection.command.ExecuteNonQuery();
                }

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }

            try
            {
                using (DBConnection.connection = new SqlConnection(DBConnection.connectionString))
                {
                    DBConnection.connection.Open();
                    DBConnection.adapter = new SqlDataAdapter(DBConnection.spGetAllItinerary, DBConnection.connection);
                    //т к запрос сложный нужны след. этапы
                    DBConnection.commandBilder = new SqlCommandBuilder(DBConnection.adapter);

                    DBConnection.adapter.InsertCommand = new SqlCommand(DBConnection.spInsertItinerary, DBConnection.connection);
                    DBConnection.adapter.InsertCommand.CommandType = CommandType.StoredProcedure;
                    DBConnection.adapter.InsertCommand.Parameters.Add(new SqlParameter("@country", SqlDbType.NVarChar, 20, "Страна"));
                    DBConnection.adapter.InsertCommand.Parameters.Add(new SqlParameter("@startPoint", SqlDbType.NVarChar, 25, "Начальный пункт"));
                    DBConnection.adapter.InsertCommand.Parameters.Add(new SqlParameter("@final", SqlDbType.NVarChar, 25, "Конечный пункт"));
                    DBConnection.adapter.InsertCommand.Parameters.Add(new SqlParameter("@mileage", SqlDbType.Int, 0, "Пробег"));
                    DBConnection.adapter.InsertCommand.Parameters.Add(new SqlParameter("@cargo", SqlDbType.Decimal, 0, "Вес груза т"));
                    DBConnection.adapter.InsertCommand.Parameters.Add(new SqlParameter("@cmr", SqlDbType.NVarChar, 10, "Номер СМР"));
                    DBConnection.adapter.InsertCommand.Parameters.Add(new SqlParameter("@labelTransit", SqlDbType.Bit, 0, "Транзит"));
                    DBConnection.adapter.InsertCommand.Parameters.Add(new SqlParameter("@labelEAEU", SqlDbType.Bit, 0, "Страны ЕАЭС"));
                    DBConnection.adapter.InsertCommand.Parameters.Add(new SqlParameter("@waybillNumber", tbNumberWaybill.Text));

                    DBConnection.adapter.UpdateCommand = new SqlCommand(DBConnection.spUpdateItinerary, DBConnection.connection);
                    DBConnection.adapter.UpdateCommand.CommandType = CommandType.StoredProcedure;
                    DBConnection.adapter.UpdateCommand.Parameters.Add(new SqlParameter("@country", SqlDbType.NVarChar, 20, "Страна"));
                    DBConnection.adapter.UpdateCommand.Parameters.Add(new SqlParameter("@startPoint", SqlDbType.NVarChar, 25, "Начальный пункт"));
                    DBConnection.adapter.UpdateCommand.Parameters.Add(new SqlParameter("@final", SqlDbType.NVarChar, 25, "Конечный пункт"));
                    DBConnection.adapter.UpdateCommand.Parameters.Add(new SqlParameter("@mileage", SqlDbType.Int, 0, "Пробег"));
                    DBConnection.adapter.UpdateCommand.Parameters.Add(new SqlParameter("@cargo", SqlDbType.Decimal, 0, "Вес груза т"));
                    DBConnection.adapter.UpdateCommand.Parameters.Add(new SqlParameter("@cmr", SqlDbType.NVarChar, 10, "Номер СМР"));
                    DBConnection.adapter.UpdateCommand.Parameters.Add(new SqlParameter("@labelTransit", SqlDbType.Bit, 0, "Транзит"));
                    DBConnection.adapter.UpdateCommand.Parameters.Add(new SqlParameter("@labelEAEU", SqlDbType.Bit, 0, "Страны ЕАЭС"));
                    DBConnection.adapter.UpdateCommand.Parameters.Add(new SqlParameter("@waybillNumber", tbNumberWaybill.Text));
                    DBConnection.adapter.UpdateCommand.Parameters.Add(new SqlParameter("@idItinerary", SqlDbType.Int, 0, "Номер"));

                    DBConnection.adapter.DeleteCommand = new SqlCommand(DBConnection.spDeleteItinerary, DBConnection.connection);
                    DBConnection.adapter.DeleteCommand.CommandType = CommandType.StoredProcedure;
                    DBConnection.adapter.DeleteCommand.Parameters.Add(new SqlParameter("@id", SqlDbType.Int, 0, "Номер"));

                    DBConnection.adapter.Update(ds);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }

            try
            {
                using (DBConnection.connection = new SqlConnection(DBConnection.connectionString))
                {
                    DBConnection.connection.Open();
                    DBConnection.adapter = new SqlDataAdapter(DBConnection.spGetAllRefil, DBConnection.connection);

                    DBConnection.commandBilder = new SqlCommandBuilder(DBConnection.adapter);

                    DBConnection.adapter.InsertCommand = new SqlCommand(DBConnection.spInsertRefill, DBConnection.connection);
                    DBConnection.adapter.InsertCommand.CommandType = CommandType.StoredProcedure;
                    DBConnection.adapter.InsertCommand.Parameters.Add(new SqlParameter("@typeFuel", cbFuel.Text));
                    DBConnection.adapter.InsertCommand.Parameters.Add(new SqlParameter("@waybillNumber", tbNumberWaybill.Text));
                    DBConnection.adapter.InsertCommand.Parameters.Add(new SqlParameter("@dataRefill", SqlDbType.Date, 0, "Дата заправки"));
                    DBConnection.adapter.InsertCommand.Parameters.Add(new SqlParameter("@valumeRefill", SqlDbType.Decimal, 0, "Количество"));

                    DBConnection.adapter.UpdateCommand = new SqlCommand(DBConnection.spUpdateRefill, DBConnection.connection);
                    DBConnection.adapter.UpdateCommand.CommandType = CommandType.StoredProcedure;
                    DBConnection.adapter.UpdateCommand.Parameters.Add(new SqlParameter("@typeFuel", cbFuel.Text));
                    DBConnection.adapter.UpdateCommand.Parameters.Add(new SqlParameter("@waybillNumber", tbNumberWaybill.Text));
                    DBConnection.adapter.UpdateCommand.Parameters.Add(new SqlParameter("@dataRefill", SqlDbType.Date, 0, "Дата заправки"));
                    DBConnection.adapter.UpdateCommand.Parameters.Add(new SqlParameter("@valumeRefill", SqlDbType.Decimal, 0, "Количество"));
                    DBConnection.adapter.UpdateCommand.Parameters.Add(new SqlParameter("@idRefill", SqlDbType.Int, 0, "Номер"));


                    DBConnection.adapter.DeleteCommand = new SqlCommand(DBConnection.spDeleteRefill, DBConnection.connection);
                    DBConnection.adapter.DeleteCommand.CommandType = CommandType.StoredProcedure;
                    DBConnection.adapter.DeleteCommand.Parameters.Add(new SqlParameter("@id", SqlDbType.Int, 0, "Номер"));

                    DBConnection.adapter.Update(ds2);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            Form.ActiveForm.Close();
        }

        private void dgvItinerary_DefaultValuesNeeded_1(object sender, DataGridViewRowEventArgs e)
        {
            var dgv = sender as DataGridView;
            if (dgv == null)
                return;

            e.Row.Cells[3].Value = 0;
            e.Row.Cells[4].Value = 0;
            e.Row.Cells[6].Value = "False";
            e.Row.Cells[7].Value = "False";

            // This line will commit the new line to the binding source
            dgv.BindingContext[dgv.DataSource].EndCurrentEdit();
        }

        private void dgvRefill_CellValueChanged_1(object sender, DataGridViewCellEventArgs e)
        {
            sumRefill = 0;
            foreach (DataGridViewRow row in dgvRefill.Rows)
            {
                try
                {
                    a = Convert.ToDouble(row.Cells[1].Value);
                    sumRefill += a;
                    tbTotal.Text = Convert.ToString(sumRefill);
                    tbSumRefill.Text = Convert.ToString(sumRefill);
                }
                catch (Exception ex)
                {
                    sumRefill = 0;
                }
            }
        }

        private void dgvItinerary_CellValueChanged(object sender, DataGridViewCellEventArgs e)
        {
            sumMileageTotal = 0;
            sumMileageCargo = 0;
            sumWeight = 0;
            sumWeightTransit = 0;
            sumWeightEAEU = 0;
            sumCargoTurnover = 0;
            sumCargoTurnoverTransit = 0;
            sumCargoTurnoverEAEU = 0;
            ride = 0;

            b = 0;

            foreach (DataGridViewRow row in dgvItinerary.Rows)
            {
                try
                {
                    a2 = Convert.ToDouble(row.Cells[3].Value);
                    a3 = Convert.ToDouble(row.Cells[3].Value);
                    sumMileageTotal += a2;
                    b++;
                    if (Convert.ToDouble(row.Cells[4].Value) != 0)
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
            tbMileageTotal.Text = Convert.ToString(sumMileageTotal / 1000);
            tbMileageCargo.Text = Convert.ToString(sumMileageCargo / 1000);
            try
            {
                for (int i = 0; i < b - 1; i++)
                {
                    a4 = Convert.ToDouble(dgvItinerary.Rows[i].Cells[4].Value);
                    a5 = Convert.ToDouble(dgvItinerary.Rows[i].Cells[3].Value) * Convert.ToDouble(dgvItinerary.Rows[i].Cells[4].Value);
                    sumCargoTurnover += a5;
                    if (i == 0) { sumWeight = Convert.ToDouble(dgvItinerary.Rows[i].Cells[4].Value); }
                    if (string.IsNullOrEmpty(dgvItinerary.Rows[i].Cells[4].Value.ToString()))
                    { sumWeight = Convert.ToDouble(dgvItinerary.Rows[i + 1].Cells[4].Value); }
                    if ((Convert.ToDouble(dgvItinerary.Rows[i].Cells[4].Value) - Convert.ToDouble(dgvItinerary.Rows[i + 1].Cells[4].Value)) < 0)
                    {
                        c = (Convert.ToDouble(dgvItinerary.Rows[i].Cells[4].Value) - Convert.ToDouble(dgvItinerary.Rows[i + 1].Cells[4].Value)) * (-1);
                        sumWeight += c;
                    }
                    if (dgvItinerary.Rows[i].Cells[6].Value.ToString() == "True")
                    {
                        sumWeightTransit += a4;
                        sumCargoTurnoverTransit += a5;
                    }
                    if (dgvItinerary.Rows[i].Cells[7].Value.ToString() == "True")
                    {
                        sumWeightEAEU += a4;
                        sumCargoTurnoverEAEU += a5;
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            tbTransportedTotal.Text = Convert.ToString(sumWeight / 1000);
            tbTransportedTransit.Text = Convert.ToString(sumWeightTransit / 1000);
            tbTransportedEAEU.Text = Convert.ToString(sumWeightEAEU / 1000);
            tbCargoTurnoverTotal.Text = Convert.ToString(sumCargoTurnover / 1000);
            tbCargoTurnoverTransit.Text = Convert.ToString(sumCargoTurnoverTransit / 1000);
            tbCargoTurnoverEAEU.Text = Convert.ToString(sumCargoTurnoverEAEU / 1000);
            tbFuelRates.Text = Convert.ToString(Convert.ToDouble((norm * sumMileageTotal + sumCargoTurnover * ratio) / 100));
        }              

    }
}
