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
    public partial class frmEmployees : Form
    {
        DataSet ds;
        DataTable dt;
        public frmEmployees()
        {
            InitializeComponent();

            dataGVEmployee.SelectionMode = DataGridViewSelectionMode.FullRowSelect; //выделение сразу всей строки для удаления
            dataGVEmployee.AllowUserToAddRows = false;

            try
            {
                using (DBConnection.connection = new SqlConnection(DBConnection.connectionString))
                {
                    DBConnection.connection.Open();
                    DBConnection.adapter = new SqlDataAdapter(DBConnection.spGetAllEmployee, DBConnection.connection);
                    ds = new DataSet();
                    DBConnection.adapter.Fill(ds);//буферная таблица
                    dataGVEmployee.DataSource = ds.Tables[0];
                    dataGVEmployee.Columns["Номер"].ReadOnly = true;

                    //таблица растягивается на все окно

                    foreach (DataGridViewColumn column in dataGVEmployee.Columns)
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
            foreach (DataGridViewRow row in dataGVEmployee.SelectedRows)
            dataGVEmployee.Rows.Remove(row);
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            try
            {
                using (DBConnection.connection = new SqlConnection(DBConnection.connectionString))
                {
                    DBConnection.connection.Open();
                    DBConnection.adapter = new SqlDataAdapter(DBConnection.spGetAllEmployee, DBConnection.connection);
                    //т к запрос сложный нужны след. этапы
                    DBConnection.commandBilder = new SqlCommandBuilder(DBConnection.adapter);

                    DBConnection.adapter.InsertCommand = new SqlCommand(DBConnection.spInsertEmployee, DBConnection.connection);
                    DBConnection.adapter.InsertCommand.CommandType = CommandType.StoredProcedure;
                    DBConnection.adapter.InsertCommand.Parameters.Add(new SqlParameter("@name", SqlDbType.NVarChar, 50, "Ф.И.О."));
                    DBConnection.adapter.InsertCommand.Parameters.Add(new SqlParameter("@position", SqlDbType.NVarChar, 30, "Должность"));
                    DBConnection.adapter.InsertCommand.Parameters.Add(new SqlParameter("@salary", SqlDbType.Decimal, 0, "Оклад"));

                    DBConnection.adapter.UpdateCommand = new SqlCommand(DBConnection.spUpdateEmployee, DBConnection.connection);
                    DBConnection.adapter.UpdateCommand.CommandType = CommandType.StoredProcedure;
                    DBConnection.adapter.UpdateCommand.Parameters.Add(new SqlParameter("@name", SqlDbType.NVarChar, 50, "Ф.И.О."));
                    DBConnection.adapter.UpdateCommand.Parameters.Add(new SqlParameter("@position", SqlDbType.NVarChar, 30, "Должность"));
                    DBConnection.adapter.UpdateCommand.Parameters.Add(new SqlParameter("@salary", SqlDbType.Decimal, 0, "Оклад"));
                    DBConnection.adapter.UpdateCommand.Parameters.Add(new SqlParameter("@id", SqlDbType.Int, 0, "Номер"));

                    DBConnection.adapter.DeleteCommand = new SqlCommand(DBConnection.spDeleteEmployee, DBConnection.connection);
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

        private void btnSearch_Click(object sender, EventArgs e)
        {
            foreach (DataGridViewRow row in dataGVEmployee.Rows)
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

        private void dataGVEmployee_UserDeletingRow(object sender, DataGridViewRowCancelEventArgs e)
        {
            DialogResult dialogResult = MessageBox.Show("Вы хотите удалить эту строку?", "Удалить", MessageBoxButtons.OKCancel, MessageBoxIcon.Warning, MessageBoxDefaultButton.Button2); //button2 ПО умолчанию cancel
            if (dialogResult == DialogResult.Cancel)
                e.Cancel = true;
        }
    }
}
