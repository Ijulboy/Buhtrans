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
    public partial class frmPeriodSelection : Form
    {
        public static DateTime start, end;
        public frmPeriodSelection()
        {
            InitializeComponent();
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btnForm_Click(object sender, EventArgs e)
        {
            start = dtpStartDate.Value;
            end = dtpEndDate.Value;
            frmMileageReport mileageReport = new frmMileageReport();
            mileageReport.Show();
            //MessageBox.Show(start.ToString("dd.MM.yyyy"));
            //MessageBox.Show(end.ToString("dd.MM.yyyy"));
        }
    }
}
