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
    public partial class frmAdminMain : Form
    {
        DataSet ds;
        DataTable dt;
        public frmAdminMain()
        {
            InitializeComponent();

            dataGVUsersData.SelectionMode = DataGridViewSelectionMode.FullRowSelect; //выделение сразу всей строки для удаления
            dataGVUsersData.AllowUserToAddRows = false;

            try
            {
                using (DBConnection.connection = new SqlConnection(DBConnection.connectionString))
                {
                    DBConnection.connection.Open();
                    DBConnection.adapter = new SqlDataAdapter(DBConnection.spGetAllUserDate, DBConnection.connection);
                    ds = new DataSet();
                    DBConnection.adapter.Fill(ds);//буферная таблица
                    dataGVUsersData.DataSource = ds.Tables[0];
                    dataGVUsersData.Columns["Код пользователя"].ReadOnly = true;

                    //таблица растягивается на все окно
                    foreach (DataGridViewColumn column in dataGVUsersData.Columns)
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

        private void btnSearch_Click(object sender, EventArgs e) //работает с полученными данными грид
        {
            foreach(DataGridViewRow row in dataGVUsersData.Rows)
            {
                row.Selected = false;
                foreach(DataGridViewCell cell in row.Cells)
                {
                    if ((cell.Value != null) && (cell.Value.ToString().Contains(textBox1.Text)))
                    {
                        row.Selected = true;
                        break;
                    }
                       
                }
            }
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {
            (dataGVUsersData.DataSource as DataTable).DefaultView.RowFilter =
                String.Format($"[Логин] like '%{textBox1.Text}%'");
            
        }

        private void btnAdd_Click(object sender, EventArgs e)
        {
            DataRow row = ds.Tables[0].NewRow();
            ds.Tables[0].Rows.Add(row);
        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            foreach (DataGridViewRow row in dataGVUsersData.SelectedRows)
                dataGVUsersData.Rows.Remove(row);
        }

        private void dataGVUsersData_UserDeletingRow(object sender, DataGridViewRowCancelEventArgs e) //DataGridViewRowCancelEventArgs отмена выполняющегося действия
        {
            DialogResult dialogResult = MessageBox.Show("Вы хотите удалить строку?", "Удалить", MessageBoxButtons.OKCancel, MessageBoxIcon.Warning, MessageBoxDefaultButton.Button2); //button2 ПО умолчанию cancel
            if (dialogResult == DialogResult.Cancel)
                e.Cancel = true;
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            try
            {
                using (DBConnection.connection  = new SqlConnection(DBConnection.connectionString))
                {
                    DBConnection.connection.Open();
                    DBConnection.adapter = new SqlDataAdapter(DBConnection.spGetAllUserDate, DBConnection.connection);
                    //т к запрос сложный нужны след. этапы
                    DBConnection.commandBilder = new SqlCommandBuilder(DBConnection.adapter);

                    DBConnection.adapter.InsertCommand = new SqlCommand(DBConnection.spInsertUserData, DBConnection.connection);
                    DBConnection.adapter.InsertCommand.CommandType = CommandType.StoredProcedure;
                    DBConnection.adapter.InsertCommand.Parameters.Add(new SqlParameter("@login", SqlDbType.NVarChar, 15, "Логин"));
                    DBConnection.adapter.InsertCommand.Parameters.Add(new SqlParameter("@password", SqlDbType.NVarChar, 12, "Пароль"));
                    DBConnection.adapter.InsertCommand.Parameters.Add(new SqlParameter("@firstname", SqlDbType.NVarChar, 30, "Имя"));
                    DBConnection.adapter.InsertCommand.Parameters.Add(new SqlParameter("@lastname", SqlDbType.NVarChar, 30, "Фамилия"));
                    DBConnection.adapter.InsertCommand.Parameters.Add(new SqlParameter("@birthday", SqlDbType.Date, 0, "Дата рождения"));
                    DBConnection.adapter.InsertCommand.Parameters.Add(new SqlParameter("@email", SqlDbType.NVarChar, 30, "Email"));
                    DBConnection.adapter.InsertCommand.Parameters.Add(new SqlParameter("@role", SqlDbType.NVarChar, 30, "Роль"));

                    DBConnection.adapter.UpdateCommand = new SqlCommand(DBConnection.spUpdateUserData, DBConnection.connection);
                    DBConnection.adapter.UpdateCommand.CommandType = CommandType.StoredProcedure;
                    DBConnection.adapter.UpdateCommand.Parameters.Add(new SqlParameter("@userid", SqlDbType.Int, 0, "Код пользователя"));
                    DBConnection.adapter.UpdateCommand.Parameters.Add(new SqlParameter("@login", SqlDbType.NVarChar, 15, "Логин"));
                    DBConnection.adapter.UpdateCommand.Parameters.Add(new SqlParameter("@password", SqlDbType.NVarChar, 12, "Пароль"));
                    DBConnection.adapter.UpdateCommand.Parameters.Add(new SqlParameter("@firstname", SqlDbType.NVarChar, 30, "Имя"));
                    DBConnection.adapter.UpdateCommand.Parameters.Add(new SqlParameter("@lastname", SqlDbType.NVarChar, 30, "Фамилия"));
                    DBConnection.adapter.UpdateCommand.Parameters.Add(new SqlParameter("@birthday", SqlDbType.Date, 0, "Дата рождения"));
                    DBConnection.adapter.UpdateCommand.Parameters.Add(new SqlParameter("@email", SqlDbType.NVarChar, 30, "Email"));
                    DBConnection.adapter.UpdateCommand.Parameters.Add(new SqlParameter("@role", SqlDbType.NVarChar, 30, "Роль"));

                    DBConnection.adapter.DeleteCommand = new SqlCommand(DBConnection.spDeleteUserDate, DBConnection.connection);
                    DBConnection.adapter.DeleteCommand.CommandType = CommandType.StoredProcedure;
                    DBConnection.adapter.DeleteCommand.Parameters.Add(new SqlParameter("@userid", SqlDbType.Int, 0, "Код пользователя"));

                    DBConnection.adapter.Update(ds);
                }                
               
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
    }
}
