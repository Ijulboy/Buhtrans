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
using System.IO;
using static BuhTrans.Cryption;

namespace BuhTrans
{
    public partial class frmNameServ : Form
    {
        public frmNameServ()
        {
            InitializeComponent();
        }

        public void frmNameServ_Load(object sender, EventArgs e)
        {
            if ((bool)Properties.Settings.Default.firstRun == true)
            {
                //First application run
                //Update setting
                Properties.Settings.Default.firstRun = false;
                //Save setting
                Properties.Settings.Default.Save();
                //Create new instance of Dialog you want to show
                //FirstDialogForm fdf = new FirstDialogForm();
                //Show the dialog
                //fdf.ShowDialog();
        }
            else
            {
                //Not first time of running application.
            }
        }

        private void btnLogin_Click(object sender, EventArgs e)
        {
            Properties.Settings.Default["serv"] = tbNameBase.Text.ToString();
            Properties.Settings.Default.Save();
            this.Close();
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            this.Close();
        }

    }
}
