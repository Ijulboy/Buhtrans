using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace BuhTrans
{
    public partial class frmAdminStart : Form
    {
        public frmAdminStart()
        {
            InitializeComponent();
        }

        private void TSMIExit_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void TSMIUsers_Click(object sender, EventArgs e)
        {
            frmAdminMain frmAdminMain = new frmAdminMain();
            frmAdminMain.Show();
        }

        private void btnLogout_Click(object sender, EventArgs e)
        {
            Application.Restart();
            UserInfo.login = null;
            UserInfo.password = null;
            UserInfo.role = null;
        }
    }
}
