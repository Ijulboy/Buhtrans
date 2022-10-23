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
    public partial class frmRateCurrency : Form
    {
        DataSet ds;
        DataTable dt;
        public frmRateCurrency()
        {
            InitializeComponent();

            dataGVRateCurrencies.SelectionMode = DataGridViewSelectionMode.FullRowSelect; //выделение сразу всей строки для удаления
            dataGVRateCurrencies.AllowUserToAddRows = false;

            try
            {
                using (DBConnection.connection = new SqlConnection(DBConnection.connectionString))
                {
                    DBConnection.connection.Open();
                    DBConnection.adapter = new SqlDataAdapter(DBConnection.spGetAllRate, DBConnection.connection);
                    ds = new DataSet();
                    DBConnection.adapter.Fill(ds);//буферная таблица
                    dataGVRateCurrencies.DataSource = ds.Tables[0];
                   
                    //таблица растягивается на все окно
                    foreach (DataGridViewColumn column in dataGVRateCurrencies.Columns)
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

        private void btnDelete_Click(object sender, EventArgs e)
        {
                foreach (DataGridViewRow row in dataGVRateCurrencies.SelectedRows)
                dataGVRateCurrencies.Rows.Remove(row);
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            try
            {
                using (DBConnection.connection = new SqlConnection(DBConnection.connectionString))
                {
                    DBConnection.connection.Open();
                    DBConnection.adapter = new SqlDataAdapter(DBConnection.spGetAllRate, DBConnection.connection);
                    //т к запрос сложный нужны след. этапы
                    DBConnection.commandBilder = new SqlCommandBuilder(DBConnection.adapter);

                    DBConnection.adapter.InsertCommand = new SqlCommand(DBConnection.spInsertRate, DBConnection.connection);
                    DBConnection.adapter.InsertCommand.CommandType = CommandType.StoredProcedure;
                    DBConnection.adapter.InsertCommand.Parameters.Add(new SqlParameter("@codeCurrency", SqlDbType.NChar, 3, "Валюта"));
                    DBConnection.adapter.InsertCommand.Parameters.Add(new SqlParameter("@dateRate", SqlDbType.Date, 0, "Дата"));
                    DBConnection.adapter.InsertCommand.Parameters.Add(new SqlParameter("@rate", SqlDbType.Decimal, 0, "Курс"));

                    DBConnection.adapter.UpdateCommand = new SqlCommand(DBConnection.spUpdateRate, DBConnection.connection);
                    DBConnection.adapter.UpdateCommand.CommandType = CommandType.StoredProcedure;
                    DBConnection.adapter.UpdateCommand.Parameters.Add(new SqlParameter("@codeCurrency", SqlDbType.NChar, 3, "Валюта"));
                    DBConnection.adapter.UpdateCommand.Parameters.Add(new SqlParameter("@dateRate", SqlDbType.NVarChar, 20, "Дата"));
                    DBConnection.adapter.UpdateCommand.Parameters.Add(new SqlParameter("@rate", SqlDbType.Decimal, 0, "Курс"));


                    DBConnection.adapter.DeleteCommand = new SqlCommand(DBConnection.spDeleteRate, DBConnection.connection);
                    DBConnection.adapter.DeleteCommand.CommandType = CommandType.StoredProcedure;
                    DBConnection.adapter.DeleteCommand.Parameters.Add(new SqlParameter("@codeCurrency", SqlDbType.NChar, 3, "Валюта"));
                    DBConnection.adapter.DeleteCommand.Parameters.Add(new SqlParameter("@dateRate", SqlDbType.Date, 0, "Дата"));

                    DBConnection.adapter.Update(ds);
                }

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void btnSearch_Click(object sender, EventArgs e)
        {
            foreach (DataGridViewRow row in dataGVRateCurrencies.Rows)
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

        private void dataGVRateCurrencies_UserDeletingRow(object sender, DataGridViewRowCancelEventArgs e)
        {

            DialogResult dialogResult = MessageBox.Show("Вы хотите удалить эту строку?", "Удалить", MessageBoxButtons.OKCancel, MessageBoxIcon.Warning, MessageBoxDefaultButton.Button2); //button2 ПО умолчанию cancel
            if (dialogResult == DialogResult.Cancel)
                e.Cancel = true;
        }
    }
}
