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

namespace BuhTrans
{
    public partial class frmCars : Form
    {
        DataSet ds;
        DataTable dt;
        public frmCars()
        {
            InitializeComponent();

            dataGVCar.SelectionMode = DataGridViewSelectionMode.FullRowSelect; //выделение сразу всей строки для удаления
            dataGVCar.AllowUserToAddRows = false;

            try
            {
                using (DBConnection.connection = new SqlConnection(DBConnection.connectionString))
                {
                    DBConnection.connection.Open();
                    DBConnection.adapter = new SqlDataAdapter(DBConnection.spGetAllCar, DBConnection.connection);
                    ds = new DataSet();
                    DBConnection.adapter.Fill(ds);//буферная таблица
                    dataGVCar.DataSource = ds.Tables[0];
                    dataGVCar.Columns["Номер"].ReadOnly = true;

                    //таблица растягивается на все окно
                    foreach (DataGridViewColumn column in dataGVCar.Columns)
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

        private void btnAdd_Click(object sender, EventArgs e)
        {
            DataRow row = ds.Tables[0].NewRow();
            ds.Tables[0].Rows.Add(row);
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            try
            {
                using (DBConnection.connection = new SqlConnection(DBConnection.connectionString))
                {
                    DBConnection.connection.Open();
                    DBConnection.adapter = new SqlDataAdapter(DBConnection.spGetAllCar, DBConnection.connection);
                    //т к запрос сложный нужны след. этапы
                    DBConnection.commandBilder = new SqlCommandBuilder(DBConnection.adapter);

                    DBConnection.adapter.InsertCommand = new SqlCommand(DBConnection.spInsertCar, DBConnection.connection);
                    DBConnection.adapter.InsertCommand.CommandType = CommandType.StoredProcedure;
                    DBConnection.adapter.InsertCommand.Parameters.Add(new SqlParameter("@nameCar", SqlDbType.NVarChar, 50, "Автомобиль, гос. номер"));
                    DBConnection.adapter.InsertCommand.Parameters.Add(new SqlParameter("@nameTrailer", SqlDbType.NVarChar, 30, "Прицеп, гос. номер"));
                    
                    DBConnection.adapter.UpdateCommand = new SqlCommand(DBConnection.spUpdateCar, DBConnection.connection);
                    DBConnection.adapter.UpdateCommand.CommandType = CommandType.StoredProcedure;
                    DBConnection.adapter.UpdateCommand.Parameters.Add(new SqlParameter("@nameCar", SqlDbType.NVarChar, 50, "Автомобиль, гос. номер"));
                    DBConnection.adapter.UpdateCommand.Parameters.Add(new SqlParameter("@nameTrailer", SqlDbType.NVarChar, 30, "Прицеп, гос. номер"));
                    DBConnection.adapter.UpdateCommand.Parameters.Add(new SqlParameter("@id", SqlDbType.Int, 0, "Номер"));

                    DBConnection.adapter.DeleteCommand = new SqlCommand(DBConnection.spDeleteCar, DBConnection.connection);
                    DBConnection.adapter.DeleteCommand.CommandType = CommandType.StoredProcedure;
                    DBConnection.adapter.DeleteCommand.Parameters.Add(new SqlParameter("@id", SqlDbType.Int, 0, "Номер"));

                    DBConnection.adapter.Update(ds);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }



        private void dataGVCar_UserDeletingRow(object sender, DataGridViewRowCancelEventArgs e)
        {
            DialogResult dialogResult = MessageBox.Show("Вы хотите удалить эту строку?", "Удалить", MessageBoxButtons.OKCancel, MessageBoxIcon.Warning, MessageBoxDefaultButton.Button2); //button2 ПО умолчанию cancel
            if (dialogResult == DialogResult.Cancel)
                e.Cancel = true;
        }

        private void btnSearch1_Click(object sender, EventArgs e)
        {
            foreach (DataGridViewRow row in dataGVCar.Rows)
            {
                row.Selected = false;
                foreach (DataGridViewCell cell in row.Cells)
                {
                    if ((cell.Value != null) && (cell.Value.ToString().Contains(textBox1.Text)))
                    {
                        row.Selected = true;
                        break;
                    }
                }
            }
        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            foreach (DataGridViewRow row in dataGVCar.SelectedRows)
            dataGVCar.Rows.Remove(row);
        }
    }
}

