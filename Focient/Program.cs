using System;
using System.Windows.Forms;
using Focient.Forms;
using Focient.Helpers; // Penting: Import namespace Helpers

namespace Focient
{
    static class Program
    {
        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);

            // 1. Inisialisasi Database (pastikan tabel dibuat jika belum ada)
            // Ini harus dieksekusi di awal sekali, sebelum interaksi database lainnya.
            DatabaseHelper.InitializeDatabase();

            // 2. Mulai dengan form Login
            FormLogin loginForm = new FormLogin();

            // Tampilkan form login sebagai dialog modal
            if (loginForm.ShowDialog() == DialogResult.OK)
            {
                // Jika login berhasil (DialogResult.OK), tampilkan FormDashboard
                // Pastikan FormDashboard siap menerima user yang login
                Application.Run(new FormDashboard()); // FormDashboard akan memuat data berdasarkan UserManager.CurrentUserId
            }
            else
            {
                // Jika login dibatalkan atau gagal (DialogResult.Cancel), keluar dari aplikasi
                Application.Exit();
            }
        }
    }
}