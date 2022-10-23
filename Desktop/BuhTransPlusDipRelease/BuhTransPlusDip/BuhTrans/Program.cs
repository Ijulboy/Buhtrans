using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace BuhTrans
{
    static class Program
    {
        public static bool openMainForm { get; set; }
        /// <summary>
        /// Главная точка входа для приложения.
        /// </summary>
        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            openMainForm = false;
            if ((bool)Properties.Settings.Default.firstRun == true) {
                Application.Run(new frmNameServ());
                Properties.Settings.Default.firstRun = false;
                //Save setting
                Properties.Settings.Default.Save();
            }
            
            Application.Run(new frmLogin());

            if (openMainForm)
            {
                if (UserInfo.role.Trim() == "Admin") Application.Run(new frmAdminStart());
                else Application.Run(new frmUserMain());
            }

        }
    }
}
