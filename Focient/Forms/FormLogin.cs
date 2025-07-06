using System;
using System.Data.SQLite;
using System.Windows.Forms;
using Focient.Models;

namespace Focient.Forms
{
    public partial class FormLogin : Form
    {
        public FormLogin()
        {
            InitializeComponent();
        }

        private void btnLogin_Click(object sender, EventArgs e)
        {
            string username = txtUsername.Text.Trim();
            string password = txtPassword.Text;

            try
            {
                // Ambil user dari database berdasarkan username
                UserModel user = DatabaseHelper.GetUserByUsername(username);

                if (user == null)
                {
                    MessageBox.Show("Username not found.");
                    return;
                }

                // Verifikasi password
                if (!user.VerifyPassword(password))
                {
                    MessageBox.Show("Incorrect password.");
                    return;
                }

                MessageBox.Show("Login successful!");
                // TODO: Tampilkan dashboard atau halaman utama
                this.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error: " + ex.Message);
            }
        }
    }
}
