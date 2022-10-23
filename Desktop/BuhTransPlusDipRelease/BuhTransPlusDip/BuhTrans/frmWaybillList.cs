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
    public partial class frmWaybillList : Form
    {
        DataSet ds;
        DataTable dt;
        public static int a = 0;
        public frmWaybillList()
        {
            InitializeComponent();

            dgvWayList.SelectionMode = DataGridViewSelectionMode.FullRowSelect; //выделение сразу всей строки для удаления
            dgvWayList.AllowUserToAddRows = false;
            dgvWayList.ReadOnly = true;
            
            try
            {
                using (DBConnection.connection = new SqlConnection(DBConnection.connectionString))
                {
                    DBConnection.connection.Open();
                    DBConnection.adapter = new SqlDataAdapter(DBConnection.spGetAllWayList, DBConnection.connection);
                    ds = new DataSet();
                    DBConnection.adapter.Fill(ds);//буферная таблица
                    dgvWayList.DataSource = ds.Tables[0];

                    //таблица растягивается на все окно
                    foreach (DataGridViewColumn column in dgvWayList.Columns)
                    {
                        column.AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
                    }

                    DBConnection.connection.Close();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void btnAdd_Click(object sender, EventArgs e)
        {
            frmWaybills waybillsForm = new frmWaybills();
            waybillsForm.Show();
        }

        private void btnUpdate_Click(object sender, EventArgs e)
        {
            try
            {
                using (DBConnection.connection = new SqlConnection(DBConnection.connectionString))
                {
                    DBConnection.connection.Open();
                    DBConnection.adapter = new SqlDataAdapter(DBConnection.spGetAllWayList, DBConnection.connection);
                    ds = new DataSet();
                    DBConnection.adapter.Fill(ds);//буферная таблица
                    dgvWayList.DataSource = ds.Tables[0];

                    //таблица растягивается на все окно
                    int width = this.Width - dgvWayList.Width;

                    foreach (DataGridViewColumn column in dgvWayList.Columns)
                    {
                        column.AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCells;
                        width += column.Width;
                    }
                    this.Width = width + 80;
                    DBConnection.connection.Close();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }

            foreach (DataGridViewColumn column in dgvWayList.Columns)
            {
                column.AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
            }

        }

        private void dgvWayList_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            a = Convert.ToInt32(dgvWayList.Rows[dgvWayList.CurrentCell.RowIndex].Cells[0].Value);
            //MessageBox.Show(Convert.ToString(a));
            frmWaybillsContent waybillsContentForm = new frmWaybillsContent();
            waybillsContentForm.Show();            
        }
    }
}
