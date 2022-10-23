using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Text.RegularExpressions;//регулярные выражения для сравнения
using System.Data.SqlClient;
using static BuhTrans.Cryption;
using System.IO;

namespace BuhTrans
{
    public partial class frmSingup : Form
    {
        string passEncription = "123ki";
        string EncryptionS;
        string DecryptionS;
        public frmSingup()
        {
            InitializeComponent();
        }

        private void btnCancel2_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btnOk_Click(object sender, EventArgs e)
        {          
            try
            {
                EncryptionS = Cryption.SymmetricEncryptionUtility.Encrypt(tbPassword.Text, passEncription);
                //MessageBox.Show(EncryptionS);
                DecryptionS = Cryption.SymmetricEncryptionUtility.Decrypt(EncryptionS, passEncription);
                //MessageBox.Show(DecryptionS);
            }
            catch
            {
                MessageBox.Show("Ошибка при шифровании данных!");
            }

            try
            {
                if (string.IsNullOrWhiteSpace(tbLogin.Text) || string.IsNullOrWhiteSpace(tbPassword.Text) ||
                       string.IsNullOrWhiteSpace(tbPasswordConf.Text))
                    MessageBox.Show("Вы не заполнили обязательные поля!");
                else
                {
                    MessageBox.Show("Пользователь зарегестрирован!");

                    using ((DBConnection.connection = new SqlConnection(DBConnection.connectionString)))
                    {
                        DBConnection.connection.Open();
                        DBConnection.command = new SqlCommand(DBConnection.spInsertUserData, DBConnection.connection);
                        DBConnection.command.CommandType = CommandType.StoredProcedure;

                        DBConnection.command.Parameters.Add(new SqlParameter("@login", tbLogin.Text));
                        DBConnection.command.Parameters.Add(new SqlParameter("@password", EncryptionS));
                        DBConnection.command.Parameters.Add(new SqlParameter("@firstname", tbFirstName.Text));
                        DBConnection.command.Parameters.Add(new SqlParameter("@lastname", tbLastName.Text));
                        DBConnection.command.Parameters.Add(new SqlParameter("@birthday", dtpBirthDate.Value));
                        DBConnection.command.Parameters.Add(new SqlParameter("@email", tbEmail.Text));

                        DBConnection.command.ExecuteNonQuery();

                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void tbLogin_Validating(object sender, CancelEventArgs e)
        {
            if (String.IsNullOrEmpty(tbLogin.Text.Trim()))
                epUserDialog.SetError(tbLogin, "Пожалуйста, введите логин");
            else if (!Regex.IsMatch(tbLogin.Text.Trim(), "^[a-zA-Z0-9]+$"))// ^ - начало строки, $ - конец строки, между ними м.б. данные, если в середине нужно это найти значит эти знаки не ставятся
                epUserDialog.SetError(tbLogin, "Логин должен содержать хотя бы одну букву или цифру");
            else
                epUserDialog.Clear();
                    
        }

        private void tbPassword_Validating(object sender, CancelEventArgs e)
        {
            if (String.IsNullOrEmpty(tbPassword.Text.Trim()))
                epUserDialog.SetError(tbPassword, "Пожалуйста, введите пароль");
            else if (!Regex.IsMatch(tbPassword.Text.Trim(), "^(?=.*[a-zA-Z])(?=.\\d)[a-zA-Z\\d]{4,}$"))// проверка на то что в середени будет хотябы один символ большой или маленький (буква) + хотя бы одна цифра+ min 8 символов       
                epUserDialog.SetError(tbPassword, "Пароль должен содержать минимум четыре символа, где первый символ обязательно латинская буква, а второй - цифра");
            else
                epUserDialog.Clear();
        }

       
        private void tbPasswordConf_Validating(object sender, CancelEventArgs e)
        {
            if (String.IsNullOrEmpty(tbPasswordConf.Text.Trim()))
                epUserDialog.SetError(tbPasswordConf, "Пожалуйста, повторите пароль");
            else if (tbPassword.Text.Trim()!=tbPasswordConf.Text.Trim())
                epUserDialog.SetError(tbPasswordConf, "Введенные пароли не совпадают");
            else
                epUserDialog.Clear();
        }

        private void tbFirstName_Validating(object sender, CancelEventArgs e)
        {
            if (!Regex.IsMatch(tbFirstName.Text.Trim(), "^[a-zA-Zа-яА-Я]*$"))// +-это хотя бы один, * - либо вообще не будет букв либо только буквы
                epUserDialog.SetError(tbFirstName, "Имя должно содержать только буквы");
            else
                epUserDialog.Clear();
        }

        private void tbLastName_Validating(object sender, CancelEventArgs e)
        {
            if (!Regex.IsMatch(tbLastName.Text.Trim(), "^[a-zA-Zа-яА-Я]*$"))// +-это хотя бы один, * - либо вообще не будет букв либо только буквы
                epUserDialog.SetError(tbLastName, "Фамилия должна содержать только буквы");
            else
                epUserDialog.Clear();
        }

        private void dtpBirthDate_ValueChanged(object sender, EventArgs e)
        {
            dtpBirthDate.CustomFormat = "dd.MM.yyyy";
        }

        private void tbEmail_TextChanged(object sender, EventArgs e)
        {
            if (!Regex.IsMatch(tbEmail.Text.Trim(), "^\\w+([-+.']\\w+)*@\\w+([-.]\\w+)*\\.\\w+([-.]\\w+)*$"))//w-все спец символы можно заменить либо один либо много
                epUserDialog.SetError(tbEmail, "Введите правильный адрес электронной почты в формате:abc@mail.com");
            else
                epUserDialog.Clear();
        }
    }
}
