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
    public partial class frmCurrencies : Form
    {
        DataSet ds;
        DataTable dt;
        public frmCurrencies()
        {
            InitializeComponent();

            dataGVGuideCurrencies.SelectionMode = DataGridViewSelectionMode.FullRowSelect; //выделение сразу всей строки для удаления
            dataGVGuideCurrencies.AllowUserToAddRows = false;

            try
            {
                using (DBConnection.connection = new SqlConnection(DBConnection.connectionString))
                {
                    DBConnection.connection.Open();
                    DBConnection.adapter = new SqlDataAdapter(DBConnection.spGetAllGuideCur, DBConnection.connection);
                    ds = new DataSet();
                    DBConnection.adapter.Fill(ds);//буферная таблица
                    dataGVGuideCurrencies.DataSource = ds.Tables[0];

                    //таблица растягивается на все окно
                    foreach (DataGridViewColumn column in dataGVGuideCurrencies.Columns)
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
            foreach (DataGridViewRow row in dataGVGuideCurrencies.SelectedRows)
                dataGVGuideCurrencies.Rows.Remove(row);
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            try
            {
                using (DBConnection.connection = new SqlConnection(DBConnection.connectionString))
                {
                    DBConnection.connection.Open();
                    DBConnection.adapter = new SqlDataAdapter(DBConnection.spGetAllGuideCur, DBConnection.connection);
                    //т к запрос сложный нужны след. этапы
                    DBConnection.commandBilder = new SqlCommandBuilder(DBConnection.adapter);

                    DBConnection.adapter.InsertCommand = new SqlCommand(DBConnection.spInsertGuideCur, DBConnection.connection);
                    DBConnection.adapter.InsertCommand.CommandType = CommandType.StoredProcedure;
                    DBConnection.adapter.InsertCommand.Parameters.Add(new SqlParameter("@CodeCurrency", SqlDbType.NChar, 3, "Код валюты"));
                    DBConnection.adapter.InsertCommand.Parameters.Add(new SqlParameter("@NameCurrency", SqlDbType.NVarChar, 20, "Наименование валюты"));
                    DBConnection.adapter.InsertCommand.Parameters.Add(new SqlParameter("@Multiplicity", SqlDbType.Int, 0, "Кратность"));

                    DBConnection.adapter.UpdateCommand = new SqlCommand(DBConnection.spUpdateGuideCur, DBConnection.connection);
                    DBConnection.adapter.UpdateCommand.CommandType = CommandType.StoredProcedure;
                    DBConnection.adapter.UpdateCommand.Parameters.Add(new SqlParameter("@CodeCurrency", SqlDbType.NChar, 3, "Код валюты"));
                    DBConnection.adapter.UpdateCommand.Parameters.Add(new SqlParameter("@NameCurrency", SqlDbType.NVarChar, 20, "Наименование валюты"));
                    DBConnection.adapter.UpdateCommand.Parameters.Add(new SqlParameter("@Multiplicity", SqlDbType.Int, 0, "Кратность"));


                    DBConnection.adapter.DeleteCommand = new SqlCommand(DBConnection.spDeleteGuideCur, DBConnection.connection);
                    DBConnection.adapter.DeleteCommand.CommandType = CommandType.StoredProcedure;
                    DBConnection.adapter.DeleteCommand.Parameters.Add(new SqlParameter("@CodeCurrency", SqlDbType.NChar, 3, "Код валюты"));

                    DBConnection.adapter.Update(ds);
                }

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }

        }



        //фильтр
       /* private void textBox1_TextChanged(object sender, EventArgs e)
        {
            (dataGVGuideCurrencies.DataSource as DataTable).DefaultView.RowFilter =
                String.Format($"[Наименование валюты] like '%{textBox1.Text}%'");
        }*/


        private void dataGVGuideCurrencies_UserDeletingRow(object sender, DataGridViewRowCancelEventArgs e)
        {

            DialogResult dialogResult = MessageBox.Show("Вы хотите удалить эту строку?", "Удалить", MessageBoxButtons.OKCancel, MessageBoxIcon.Warning, MessageBoxDefaultButton.Button2); //button2 ПО умолчанию cancel
            if (dialogResult == DialogResult.Cancel)
                e.Cancel = true;
        }

        private void btnSearch_Click(object sender, EventArgs e)
        {
            foreach (DataGridViewRow row in dataGVGuideCurrencies.Rows)
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
    }
}
