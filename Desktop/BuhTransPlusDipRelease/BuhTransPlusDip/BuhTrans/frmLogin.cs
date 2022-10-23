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
    public partial class frmLogin : Form
    {
        string passEncription = "123ki";
        string pass;
        string DecryptionS;
        
        public frmLogin()
        {
            InitializeComponent();
        }

        private void checkBox1_CheckedChanged(object sender, EventArgs e)
        {
            tbPass.PasswordChar = checkBox1.Checked ? '*' : '\0';
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            this.Close();
        }


        private void btnLogin_Click(object sender, EventArgs e)
        {
            String result = "";
            if (isValid())
            {
                try
                {
                    using ((DBConnection.connection = new SqlConnection(DBConnection.connectionString)))
                    {
                        DBConnection.connection.Open();
                        DBConnection.command = new SqlCommand(DBConnection.spPassDecrypt, DBConnection.connection);
                        DBConnection.command.CommandType = CommandType.StoredProcedure;

                        DBConnection.command.Parameters.Add(new SqlParameter("@login", tbLogin.Text));
                        DBConnection.command.Parameters.Add(new SqlParameter
                        {
                            ParameterName = "@passDecryption",
                            SqlDbType = SqlDbType.NVarChar,
                            Size = 50,
                            Direction = ParameterDirection.Output
                        });

                        DBConnection.command.ExecuteNonQuery();

                        pass = DBConnection.command.Parameters["@passDecryption"].Value.ToString().Trim();
                        //MessageBox.Show(pass);
                        DecryptionS = Cryption.SymmetricEncryptionUtility.Decrypt(pass, passEncription);
                    }
                }
                catch (Exception ex)
                {
                    result = ex.Message.ToString();
                }

                try 
                {
                    using ((DBConnection.connection = new SqlConnection(DBConnection.connectionString)))
                    {
                        DBConnection.connection.Open();
                        DBConnection.command = new SqlCommand(DBConnection.spGetUserData, DBConnection.connection);
                        DBConnection.command.CommandType = CommandType.StoredProcedure;
                                               
                        DBConnection.command.Parameters.Add(new SqlParameter ("@login", tbLogin.Text));
                        DBConnection.command.Parameters.Add(new SqlParameter ("@password", tbPass.Text));
                        DBConnection.command.Parameters.Add(new SqlParameter 
                                                                            {ParameterName = "@role", 
                                                                              SqlDbType = SqlDbType.NVarChar, 
                                                                              Size = 10, 
                                                                              Direction = ParameterDirection.Output});
                        DBConnection.command.Parameters.Add(new SqlParameter ("@passDecryption", DecryptionS));
                        
                        DBConnection.command.ExecuteNonQuery();

                        String temp = DBConnection.command.Parameters["@role"].Value.ToString().Trim();
                                                
                        if (String.IsNullOrEmpty(temp) || temp.Length < 2)
                        {
                            result = "Неверный логин или пароль!";
                        }
                        else
                        {
                            UserInfo.login = DBConnection.command.Parameters["@login"].Value.ToString().Trim();
                            UserInfo.password = DBConnection.command.Parameters["@password"].Value.ToString().Trim();
                            UserInfo.role = temp;
                            result = "1";
                        }

                    }
                }
                catch (Exception ex)
                {
                    result = ex.Message.ToString();
                }
                
                if (result == "1")
                {
                    Program.openMainForm = true;
                    this.Close();
                }
                else MessageBox.Show(result);
            }
        }

        private Boolean isValid()
        {
            bool temp = false;
            if (String.IsNullOrEmpty(tbLogin.Text.Trim()))
            {
                epUserDialog.SetError(tbLogin, "Пожалуйста, введите логин");
            }
            if (String.IsNullOrEmpty(tbPass.Text.Trim()))
            {
                epUserDialog.SetError(tbPass, "Пожалуйста, введите пароль");
            }
            else temp = true;
            return temp;
        }

        private void linkLabel1_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            frmSingup frmS = new frmSingup();
            frmS.ShowDialog();
        }

    }
}
