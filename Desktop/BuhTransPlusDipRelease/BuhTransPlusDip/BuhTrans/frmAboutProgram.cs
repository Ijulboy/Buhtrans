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
    public partial class frmAboutProgram : Form
    {
        public frmAboutProgram()
        {
            InitializeComponent();
        }

        private void btnSite_Click(object sender, EventArgs e)
        {
            var url = "https://julia.shturval.by/buhtransplus";
            System.Diagnostics.Process.Start("IExplore.exe", url);
        }

        private void btnUserGuide_Click(object sender, EventArgs e)
        {
            var url = "http://julia.shturval.by/buhtransplus/files/buhtransplus-readme.pdf";
            System.Diagnostics.Process.Start("IExplore.exe", url);
        }
    }
}
