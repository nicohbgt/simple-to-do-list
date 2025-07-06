// Focient/Forms/FormRegister.cs
using System;
using System.Windows.Forms;
using Focient.Models; // Pastikan namespace Anda benar
using Focient.Helpers; // Pastikan namespace Anda benar
using BCrypt.Net; // Pastikan Anda sudah menginstal paket NuGet BCrypt.Net-Next

namespace Focient.Forms
{
    public partial class FormRegister : Form
    {
        public FormRegister()
        {
            InitializeComponent();
            // Sembunyikan panel error di awal (jika Anda punya ErrorPanel)
            // errorPanel.Visible = false;
        }

        private void btnRegister_Click(object sender, EventArgs e)
        {
            // Anda mungkin memiliki metode ShowError/HideError di form Anda
            // HideError(); // Sembunyikan error sebelumnya

            // 1. Validasi Input
            string fullName = txtFullName.Text.Trim();
            string username = txtUsername.Text.Trim();
            DateTime dateOfBirth = dtpDateOfBirth.Value.Date; // Ambil hanya tanggalnya
            string area = txtArea.Text.Trim(); // Area bisa kosong
            string password = txtPassword.Text;
            string confirmPassword = txtConfirmPassword.Text;

            if (string.IsNullOrWhiteSpace(fullName) || string.IsNullOrWhiteSpace(username) || string.IsNullOrWhiteSpace(password) || string.IsNullOrWhiteSpace(confirmPassword))
            {
                MessageBox.Show("Semua kolom wajib diisi.", "Registrasi Gagal", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                // ShowError("Semua kolom wajib diisi.");
                return;
            }

            if (password.Length < 6) // Contoh minimal panjang password
            {
                MessageBox.Show("Password minimal 6 karakter.", "Registrasi Gagal", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                // ShowError("Password minimal 6 karakter.");
                return;
            }

            if (password != confirmPassword)
            {
                MessageBox.Show("Konfirmasi password tidak cocok.", "Registrasi Gagal", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                // ShowError("Konfirmasi password tidak cocok.");
                return;
            }

            // 2. Cek apakah username sudah ada (dengan memanggil GetUserByUsername)
            try
            {
                UserModel existingUser = DatabaseHelper.GetUserByUsername(username);
                if (existingUser != null)
                {
                    MessageBox.Show("Username sudah terdaftar. Gunakan username lain.", "Registrasi Gagal", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    // ShowError("Username sudah terdaftar. Gunakan username lain.");
                    return;
                }

                // 3. Hash Password
                string passwordHash = BCrypt.Net.BCrypt.HashPassword(password);

                // 4. Buat objek UserModel
                UserModel newUser = new UserModel
                {
                    FullName = fullName,
                    Username = username,
                    DateOfBirth = dateOfBirth,
                    Area = string.IsNullOrWhiteSpace(area) ? null : area, // Simpan null jika kosong
                    PasswordHash = passwordHash
                };

                // 5. Simpan ke Database
                DatabaseHelper.InsertUser(newUser);

                MessageBox.Show("Registrasi berhasil! Silakan login.", "Registrasi Sukses", MessageBoxButtons.OK, MessageBoxIcon.Information);
                this.DialogResult = DialogResult.OK; // Beri tahu form pemanggil bahwa registrasi berhasil
                this.Close(); // Tutup form registrasi
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Terjadi kesalahan saat registrasi: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                // ShowError($"Terjadi kesalahan saat registrasi: {ex.Message}");
                // Log error lebih detail di lingkungan produksi
            }
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.Cancel;
            this.Close();
        }

        // Contoh metode untuk menampilkan error jika Anda menggunakan ErrorPanel
        // private void ShowError(string message) { lblError.Text = message; errorPanel.Visible = true; }
        // private void HideError() { lblError.Text = string.Empty; errorPanel.Visible = false; }
    }
}