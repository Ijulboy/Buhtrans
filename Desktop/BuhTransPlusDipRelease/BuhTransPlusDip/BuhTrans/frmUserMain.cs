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
    public partial class frmUserMain : Form
    {
        public frmUserMain()
        {
            InitializeComponent();
        }

        private void btnLogout_Click(object sender, EventArgs e)
        {
            Application.Restart();
            UserInfo.login = null;
            UserInfo.password = null;
            UserInfo.role = null;
        }

        private void TSMIExit_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void TSMICurrencies_Click(object sender, EventArgs e)
        {
            frmCurrencies currencyForm = new frmCurrencies();
            currencyForm.Show();
        }

        private void TSMIRateCurrency_Click(object sender, EventArgs e)
        {
            frmRateCurrency rateCurrencyForm = new frmRateCurrency();
            rateCurrencyForm.Show();
        }

        private void TSMICars_Click(object sender, EventArgs e)
        {
            frmCars carForm = new frmCars();
            carForm.Show();
        }

        private void TSMIEmployee_Click(object sender, EventArgs e)
        {
            frmEmployees employeesForm = new frmEmployees();
            employeesForm.Show();
        }

        private void TSMPAboutProgram_Click(object sender, EventArgs e)
        {
            frmAboutProgram aboutProgramForm = new frmAboutProgram();
            aboutProgramForm.Show();
        }

        private void STMIWaybills_Click(object sender, EventArgs e)
        {
            frmWaybillList waybillsForm = new frmWaybillList();
            waybillsForm.Show();
        }

        private void TSMIFuelNorm_Click(object sender, EventArgs e)
        {
            frmFuelConsumptionRates fuelNormForms = new frmFuelConsumptionRates();
            fuelNormForms.Show();
        }

        private void TSMMileageReport_Click(object sender, EventArgs e)
        {
            frmPeriodSelection periodSelection = new frmPeriodSelection();
            periodSelection.Show();
        }

        private void TSMIStatistic4TR_Click(object sender, EventArgs e)
        {
            frmPeriodSelection2 periodSelection2 = new frmPeriodSelection2();
            periodSelection2.Show();
        }
    }
}
