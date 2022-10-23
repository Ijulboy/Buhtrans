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
using System.Windows.Controls;

namespace BuhTrans
{
    public partial class frmWaybills : Form
    {
        DataSet ds;
        DataTable dt;
        DataSet ds2;
        DataTable dt2;
        DataSet ds3; //таблица норм расхода топлива
        double a, c = 0;
        double sumRefill = 0;
        double sumMileageTotal = 0;
        double sumMileageCargo = 0;
        double sumWeight = 0;
        double sumWeightTransit, sumWeightEAEU, sumCargoTurnover, sumCargoTurnoverTransit, sumCargoTurnoverEAEU = 0;
        double a2, a3, a4, a5 = 0;
        int b = 0;
        double norm, ratio = 0;
        

        public frmWaybills()
        {
            InitializeComponent();

            /////Вычисления

            dgvItinerary.SelectionMode = DataGridViewSelectionMode.FullRowSelect; //выделение сразу всей строки для удаления
            dgvItinerary.AllowUserToAddRows = true;
            dgvRefill.SelectionMode = DataGridViewSelectionMode.FullRowSelect; //выделение сразу всей строки для удаления
            dgvRefill.AllowUserToAddRows = false;
            foreach (DataGridViewColumn column in dgvItinerary.Columns)
            {
                column.SortMode = DataGridViewColumnSortMode.NotSortable;
            }

            tbSpeedometerDeparture.Text = Convert.ToString(0);
            tbFuelDeparture.Text = Convert.ToString(0);
            tbFuelArrival.Text = Convert.ToString(0);
            tbSpeedometerArrival.Text = Convert.ToString(0);
            ////списки
            try
            {
                using (DBConnection.connection = new SqlConnection(DBConnection.connectionString))
                {
                    DBConnection.connection.Open();
                    DBConnection.adapter = new SqlDataAdapter(DBConnection.spGetAllCar22, DBConnection.connection);
                    ds = new DataSet();
                    DBConnection.adapter.Fill(ds);//буферная таблица
                    this.cbCar.DataSource = ds.Tables[0];
                    this.cbCar.DisplayMember = "";
                    this.cbCar.ValueMember = "Name_Number_Car";
                    this.cbCar.SelectedIndex = -1;
                    this.cbTrailer.DataSource = ds.Tables[0];
                    this.cbTrailer.DisplayMember = "";
                    this.cbTrailer.ValueMember = "Trailer_Number";
                    this.cbTrailer.SelectedIndex = -1;
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
                    DBConnection.adapter = new SqlDataAdapter(DBConnection.spGetAllFuel, DBConnection.connection);
                    ds = new DataSet();
                    DBConnection.adapter.Fill(ds);//буферная таблица
                    this.cbFuel.DataSource = ds.Tables[0];
                    this.cbFuel.DisplayMember = "";
                    this.cbFuel.ValueMember = "Type_Fuel";
                    this.cbFuel.SelectedIndex = -1;
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
                    DBConnection.adapter = new SqlDataAdapter(DBConnection.spGetAllDriver, DBConnection.connection);
                    ds = new DataSet();
                    DBConnection.adapter.Fill(ds);//буферная таблица
                    this.cbDriver.DataSource = ds.Tables[0];
                    this.cbDriver.DisplayMember = "";
                    this.cbDriver.ValueMember = "Name";
                    this.cbDriver.SelectedIndex = -1;
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
                    DBConnection.adapter = new SqlDataAdapter(DBConnection.spGetAllItinerary, DBConnection.connection);
                    ds = new DataSet();
                    DBConnection.adapter.Fill(ds);//буферная таблица
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
                    DBConnection.adapter = new SqlDataAdapter(DBConnection.spGetAllRefil, DBConnection.connection);
                    ds2 = new DataSet();
                    DBConnection.adapter.Fill(ds2);//буферная таблица
                    dgvRefill.DataSource = ds2.Tables[0];

                    //таблица растягивается на все окно
                    foreach (DataGridViewColumn column in dgvRefill.Columns)
                    {
                        column.AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void btnDeleteStagesMovement_Click(object sender, EventArgs e)
        {
            foreach (DataGridViewRow row in dgvItinerary.SelectedRows)
                dgvItinerary.Rows.Remove(row);
        }

        private void dgvItinerary_UserDeletingRow(object sender, DataGridViewRowCancelEventArgs e)
        {
            DialogResult dialogResult = MessageBox.Show("Вы хотите удалить эту строку?", "Удалить", MessageBoxButtons.OKCancel, MessageBoxIcon.Warning, MessageBoxDefaultButton.Button2); //button2 ПО умолчанию cancel
            if (dialogResult == DialogResult.Cancel)
                e.Cancel = true;
        }

        private void tbSpeedometerDeparture_TextChanged(object sender, EventArgs e)
        {
            tbSpeedometerArrival.Text = tbSpeedometerDeparture.Text;
        }

        private void tbFuelDeparture_TextChanged(object sender, EventArgs e)
        {
            tbFuelArrival.Text = tbFuelDeparture.Text;
        }

        private void cbCar_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                using (DBConnection.connection = new SqlConnection(DBConnection.connectionString))
                {
                    DBConnection.connection.Open();
                    DBConnection.adapter = new SqlDataAdapter(DBConnection.spGetAllNormFuel, DBConnection.connection);
                    ds3 = new DataSet();
                    DBConnection.adapter.Fill(ds3);//буферная таблица
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
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
        }

        private void btnAddRefill_Click(object sender, EventArgs e)
        {
            DataRow row = ds2.Tables[0].NewRow();
            ds2.Tables[0].Rows.Add(row);
        }

        private void btnDeleteRefill_Click(object sender, EventArgs e)
        {
            foreach (DataGridViewRow row in dgvRefill.SelectedRows)
                dgvRefill.Rows.Remove(row);
        }

        private void dgvRefill_UserDeletingRow(object sender, DataGridViewRowCancelEventArgs e)
        {
            DialogResult dialogResult = MessageBox.Show("Вы хотите удалить эту строку?", "Удалить", MessageBoxButtons.OKCancel, MessageBoxIcon.Warning, MessageBoxDefaultButton.Button2); //button2 ПО умолчанию cancel
            if (dialogResult == DialogResult.Cancel)
                e.Cancel = true;
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            Form.ActiveForm.Close();
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            try
            {
                using (DBConnection.connection = new SqlConnection(DBConnection.connectionString))
                {
                    DBConnection.connection.Open();
                    DBConnection.command = new SqlCommand(DBConnection.spInsertWaybill, DBConnection.connection);
                    DBConnection.command.CommandType = CommandType.StoredProcedure;
                    DBConnection.command.Parameters.Add(new SqlParameter("@waybillNumber", tbNumberWaybill.Text));
                    DBConnection.command.Parameters.Add(new SqlParameter("@departureDate", dtpDepartureDate.Value));
                    DBConnection.command.Parameters.Add(new SqlParameter("@arrivalDate", dtpArrivalDate.Value));
                    DBConnection.command.Parameters.Add(new SqlParameter("@speedometerDeparture", tbSpeedometerDeparture.Text));
                    DBConnection.command.Parameters.Add(new SqlParameter("@speedometerArrival", tbSpeedometerArrival.Text));
                    DBConnection.command.Parameters.Add(new SqlParameter("@fuelDeparture", tbFuelDeparture.Text));
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

        private void dgvItinerary_DefaultValuesNeeded(object sender, DataGridViewRowEventArgs e)
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
    

        private void dgvRefill_CellValueChanged(object sender, DataGridViewCellEventArgs e)
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
            try
            {
                tbFuelArrival.Text = Convert.ToString(Convert.ToDouble(tbFuelDeparture.Text) + sumRefill - Convert.ToDouble(tbFuelRates.Text));
            }
            catch
            {
                MessageBox.Show("Заполните остаток топлива на начало пути!");
            }
            if (Convert.ToDouble(tbFuelArrival.Text) < 0) { tbFuelArrival.BackColor = Color.Red; }
            else { tbFuelArrival.BackColor = Color.White; }
            try
            {
                tbSpeedometerArrival.Text = Convert.ToString(Convert.ToInt32(tbSpeedometerDeparture.Text) + sumMileageTotal);
            }
            catch
            {
                MessageBox.Show("Заполните показания спидометра на начало пути!");
            }
            if (Convert.ToInt32(tbSpeedometerArrival.Text) < 0) { tbSpeedometerArrival.BackColor = Color.Red; }
            else { tbSpeedometerArrival.BackColor = Color.White; }
        }

        private void dgvItinerary_CellValueChanged_1(object sender, DataGridViewCellEventArgs e)
        {
            sumMileageTotal = 0;
            sumMileageCargo = 0;
            sumWeight = 0;
            sumWeightTransit = 0;
            sumWeightEAEU = 0;
            sumCargoTurnover = 0;
            sumCargoTurnoverTransit = 0;
            sumCargoTurnoverEAEU = 0;

            b = 0;

            foreach (DataGridViewRow row in dgvItinerary.Rows)
            {
                try
                {
                    a2 = Convert.ToDouble(row.Cells[3].Value);
                    a3 = Convert.ToDouble(row.Cells[3].Value);
                    sumMileageTotal += a2;
                    b++;
                    if (Convert.ToDouble(row.Cells[4].Value)!=0)
                    {
                        sumMileageCargo += a3;
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
                for (int i = 0; i < b-1; i++)
                {
                    a4 = Convert.ToDouble(dgvItinerary.Rows[i].Cells[4].Value);
                    a5 = Convert.ToDouble(dgvItinerary.Rows[i].Cells[3].Value) * Convert.ToDouble(dgvItinerary.Rows[i].Cells[4].Value);
                    sumCargoTurnover += a5; 
                    if (i == 0) { sumWeight = Convert.ToDouble(dgvItinerary.Rows[i].Cells[4].Value); }
                    if (string.IsNullOrEmpty(dgvItinerary.Rows[i].Cells[4].Value.ToString()))
                            { sumWeight = Convert.ToDouble(dgvItinerary.Rows[i + 1].Cells[4].Value); }
                    if ((Convert.ToDouble(dgvItinerary.Rows[i].Cells[4].Value) - Convert.ToDouble(dgvItinerary.Rows[i + 1].Cells[4].Value))<0)
                    {
                        c = (Convert.ToDouble(dgvItinerary.Rows[i].Cells[4].Value) - Convert.ToDouble(dgvItinerary.Rows[i + 1].Cells[4].Value))*(-1);
                        sumWeight += c;
                    }
                    if (dgvItinerary.Rows[i].Cells[6].Value.ToString() == "True" )
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
            catch(Exception ex)
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
            try
            {
                tbSpeedometerArrival.Text = Convert.ToString(Convert.ToInt32(tbSpeedometerDeparture.Text) + sumMileageTotal);
            }
            catch
            {
                MessageBox.Show("Заполните показания спидометра на начало пути!");               
            }
            if (Convert.ToInt32(tbSpeedometerArrival.Text) < 0) { tbSpeedometerArrival.BackColor = Color.Red; }
            else { tbSpeedometerArrival.BackColor = Color.White; }
            try
            {
                tbFuelArrival.Text = Convert.ToString(Convert.ToDouble(tbFuelDeparture.Text) + sumRefill - Convert.ToDouble(tbFuelRates.Text));
            }
            catch
            {
                MessageBox.Show("Заполните остаток топлива на начало пути!");
            }
            if (Convert.ToDouble(tbFuelArrival.Text) < 0) { tbFuelArrival.BackColor = Color.Red; }
            else { tbFuelArrival.BackColor = Color.White; }
        }            
    }
}

