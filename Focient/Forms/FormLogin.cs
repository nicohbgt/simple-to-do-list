// Focient/Forms/FormLogin.cs
using System;
using System.Windows.Forms;
using Focient.Models;
using Focient.Helpers;
using BCrypt.Net;

namespace Focient.Forms
{
    public partial class FormLogin : Form
    {
        public FormLogin()
        {
            InitializeComponent();
            // errorPanel.Visible = false;
        }

        private void btnLogin_Click(object sender, EventArgs e)
        {
            // HideError();

            string username = txtUsername.Text.Trim();
            string password = txtPassword.Text;

            if (string.IsNullOrWhiteSpace(username) || string.IsNullOrWhiteSpace(password))
            {
                MessageBox.Show("Username dan password tidak boleh kosong.", "Login Gagal", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                // ShowError("Username dan password tidak boleh kosong.");
                return;
            }

            try
            {
                // 1. Ambil user dari database berdasarkan username
                UserModel user = DatabaseHelper.GetUserByUsername(username);

                if (user != null)
                {
                    // 2. Verifikasi password
                    if (BCrypt.Net.BCrypt.Verify(password, user.PasswordHash))
                    {
                        // Login berhasil!
                        // 3. Simpan informasi user yang login ke UserManager
                        UserManager.SetCurrentUser(user.Id, user.Username, user.FullName);

                        MessageBox.Show($"Selamat datang, {user.FullName}!", "Login Sukses", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        this.DialogResult = DialogResult.OK; // Penting: Beri tahu Program.cs bahwa login berhasil
                        this.Close();
                    }
                    else
                    {
                        MessageBox.Show("Username atau password salah.", "Login Gagal", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        // ShowError("Username atau password salah.");
                    }
                }
                else
                {
                    MessageBox.Show("Username atau password salah.", "Login Gagal", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    // ShowError("Username atau password salah.");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Terjadi kesalahan saat login: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                // ShowError($"Terjadi kesalahan saat login: {ex.Message}");
            }
        }

        private void btnRegisterHere_Click(object sender, EventArgs e)
        {
            // Buka FormRegister
            FormRegister registerForm = new FormRegister();
            this.Hide(); // Sembunyikan form login sementara
            if (registerForm.ShowDialog() == DialogResult.OK)
            {
                // Jika registrasi berhasil, biarkan user tetap di form login
                MessageBox.Show("Akun Anda telah berhasil dibuat. Silakan login.", "Registrasi Selesai", MessageBoxButtons.OK, MessageBoxIcon.Information);
                // Opsional: Isi username yang baru diregistrasi ke txtUsername
                // txtUsername.Text = registerForm.RegisteredUsername;
            }
            this.Show(); // Tampilkan kembali form login
        }

        // private void ShowError(string message) { lblError.Text = message; errorPanel.Visible = true; }
        // private void HideError() { lblError.Text = string.Empty; errorPanel.Visible = false; }
    }
}