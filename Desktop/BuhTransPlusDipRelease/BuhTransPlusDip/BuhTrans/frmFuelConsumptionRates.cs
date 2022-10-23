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
    public partial class frmFuelConsumptionRates : Form
    {
        DataSet ds;
        public frmFuelConsumptionRates()
        {
            InitializeComponent();

            dvgFuelNorm.SelectionMode = DataGridViewSelectionMode.FullRowSelect; //выделение сразу всей строки
            dvgFuelNorm.AllowUserToAddRows = false;

            try
            {
                using (DBConnection.connection = new SqlConnection(DBConnection.connectionString))
                {
                    DBConnection.connection.Open();
                    DBConnection.adapter = new SqlDataAdapter(DBConnection.spGetAllNormFuel, DBConnection.connection);
                    ds = new DataSet();
                    DBConnection.adapter.Fill(ds);//буферная таблица
                    dvgFuelNorm.DataSource = ds.Tables[0];
                    dvgFuelNorm.Columns["Автомобиль"].ReadOnly = true;

                    //таблица растягивается на все окно

                    foreach (DataGridViewColumn column in dvgFuelNorm.Columns)
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

        private void btnSave_Click(object sender, EventArgs e)
        {
            try
            {
                using (DBConnection.connection = new SqlConnection(DBConnection.connectionString))
                {
                    DBConnection.connection.Open();
                    DBConnection.adapter = new SqlDataAdapter(DBConnection.spGetAllNormFuel, DBConnection.connection);
                    //т к запрос сложный нужны след. этапы
                    DBConnection.commandBilder = new SqlCommandBuilder(DBConnection.adapter);

                    DBConnection.adapter.UpdateCommand = new SqlCommand(DBConnection.spUpdateFuelNorm, DBConnection.connection);
                    DBConnection.adapter.UpdateCommand.CommandType = CommandType.StoredProcedure;
                    DBConnection.adapter.UpdateCommand.Parameters.Add(new SqlParameter("@norm", SqlDbType.Decimal, 0, "Норма расхода"));
                    DBConnection.adapter.UpdateCommand.Parameters.Add(new SqlParameter("@ratio", SqlDbType.Decimal, 0, "Коэффициент грузооборота"));
                    DBConnection.adapter.UpdateCommand.Parameters.Add(new SqlParameter("@car", SqlDbType.NVarChar, 50, "Автомобиль"));
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
