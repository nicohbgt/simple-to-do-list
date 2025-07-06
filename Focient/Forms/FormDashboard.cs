// Focient/Forms/FormDashboard.cs
using System;
using System.Collections.Generic;
using System.Linq; // Perlu untuk LINQ, seperti .Where()
using System.Windows.Forms;
using Focient.Models;
using Focient.Helpers;

namespace Focient.Forms
{
    public partial class FormDashboard : Form
    {
        public FormDashboard()
        {
            InitializeComponent();
            this.Load += new EventHandler(FormDashboard_Load); // Pastikan ini ada
            // Pastikan dgvPlans diatur agar tidak bisa diedit langsung
            dgvPlans.ReadOnly = true;
            dgvPlans.SelectionMode = DataGridViewSelectionMode.FullRowSelect; // Pilih seluruh baris
            dgvPlans.AutoGenerateColumns = false; // Kita akan definisikan kolom secara manual

            // Tambahkan kolom secara manual jika belum ada di Designer
            // Ini penting agar konsisten dengan PlanModel dan bisa mengatur visibilitas
            if (dgvPlans.Columns.Count == 0)
            {
                dgvPlans.Columns.Add(new DataGridViewTextBoxColumn { DataPropertyName = "Id", HeaderText = "ID", Name = "PlanId", Visible = false }); // Sembunyikan ID
                dgvPlans.Columns.Add(new DataGridViewTextBoxColumn { DataPropertyName = "Name", HeaderText = "Nama Rencana", Name = "PlanName" });
                dgvPlans.Columns.Add(new DataGridViewTextBoxColumn { DataPropertyName = "Date", HeaderText = "Tanggal", Name = "PlanDate", DefaultCellStyle = { Format = "yyyy-MM-dd" } });
                dgvPlans.Columns.Add(new DataGridViewTextBoxColumn { DataPropertyName = "IntensityLevel", HeaderText = "Intensitas", Name = "PlanIntensity" });
                dgvPlans.Columns.Add(new DataGridViewTextBoxColumn { DataPropertyName = "Description", HeaderText = "Deskripsi", Name = "PlanDescription" });
                // UserId tidak perlu ditampilkan, tapi ada di DataSource
            }

            // Tambahkan event handler untuk tombol
            btnAddPlan.Click += btnAddPlan_Click;
            btnEditPlan.Click += btnEditPlan_Click;
            btnDeletePlan.Click += btnDeletePlan_Click;
            btnOpenPlan.Click += btnOpenPlan_Click; // Untuk membuka FormPlan (detail aktivitas)
        }

        private void FormDashboard_Load(object sender, EventArgs e)
        {
            // Pastikan ada user yang login
            if (!UserManager.IsLoggedIn)
            {
                MessageBox.Show("Anda harus login untuk mengakses dashboard.", "Akses Ditolak", MessageBoxButtons.OK, MessageBoxIcon.Stop);
                this.Close(); // Tutup form dashboard jika tidak login
                Application.Exit(); // Atau kembali ke login
                return;
            }

            this.Text = $"Dashboard - {UserManager.CurrentUserFullName}"; // Tampilkan nama user di judul form
            LoadPlans(); // Panggil metode untuk memuat rencana
        }

        private void LoadPlans()
        {
            try
            {
                // Muat rencana berdasarkan ID pengguna yang sedang login
                List<PlanModel> plans = DatabaseHelper.GetPlansByUserId(UserManager.CurrentUserId);
                dgvPlans.DataSource = plans;

                // Opsional: Atur lebar kolom agar terlihat rapi
                dgvPlans.AutoResizeColumns(DataGridViewAutoSizeColumnsMode.AllCells);
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Gagal memuat rencana: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                // Tambahkan logging yang lebih baik di sini
            }
        }

        private void btnAddPlan_Click(object sender, EventArgs e)
        {
            // Buka EditPlanForm untuk mode tambah baru
            EditPlanForm editForm = new EditPlanForm(); // Konstruktor default untuk tambah baru

            if (editForm.ShowDialog() == DialogResult.OK)
            {
                // Jika rencana baru berhasil ditambahkan, refresh daftar rencana
                LoadPlans();
                MessageBox.Show("Rencana berhasil ditambahkan.", "Sukses", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        private void btnEditPlan_Click(object sender, EventArgs e)
        {
            if (dgvPlans.SelectedRows.Count > 0)
            {
                // Ambil ID rencana dari baris yang dipilih
                int selectedPlanId = Convert.ToInt32(dgvPlans.SelectedRows[0].Cells["PlanId"].Value);

                // Buka EditPlanForm untuk mode edit dengan PlanId yang dipilih
                EditPlanForm editForm = new EditPlanForm(selectedPlanId); // Konstruktor dengan PlanId untuk edit

                if (editForm.ShowDialog() == DialogResult.OK)
                {
                    // Jika rencana berhasil diedit, refresh daftar rencana
                    LoadPlans();
                    MessageBox.Show("Rencana berhasil diperbarui.", "Sukses", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }
            else
            {
                MessageBox.Show("Pilih rencana yang ingin diedit.", "Peringatan", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        private void btnDeletePlan_Click(object sender, EventArgs e)
        {
            if (dgvPlans.SelectedRows.Count > 0)
            {
                int selectedPlanId = Convert.ToInt32(dgvPlans.SelectedRows[0].Cells["PlanId"].Value);
                string planName = dgvPlans.SelectedRows[0].Cells["PlanName"].Value.ToString();

                DialogResult confirm = MessageBox.Show(
                    $"Apakah Anda yakin ingin menghapus rencana '{planName}' beserta semua aktivitas di dalamnya?",
                    "Konfirmasi Hapus",
                    MessageBoxButtons.YesNo,
                    MessageBoxIcon.Question
                );

                if (confirm == DialogResult.Yes)
                {
                    try
                    {
                        // Hapus rencana dari database berdasarkan PlanId dan UserId yang login
                        // Penting: Pastikan relasi FOREIGN KEY dengan ON DELETE CASCADE di database sudah benar
                        // agar aktivitas terkait juga terhapus otomatis.
                        bool isDeleted = DatabaseHelper.DeletePlan(selectedPlanId, UserManager.CurrentUserId);
                        if (isDeleted)
                        {
                            LoadPlans(); // Refresh daftar rencana setelah dihapus
                            MessageBox.Show("Rencana berhasil dihapus.", "Sukses", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        }
                        else
                        {
                            MessageBox.Show("Rencana tidak ditemukan atau Anda tidak memiliki izin untuk menghapusnya.", "Gagal Hapus", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        }
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show($"Gagal menghapus rencana: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
            }
            else
            {
                MessageBox.Show("Pilih rencana yang ingin dihapus.", "Peringatan", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        private void btnOpenPlan_Click(object sender, EventArgs e)
        {
            if (dgvPlans.SelectedRows.Count > 0)
            {
                int selectedPlanId = Convert.ToInt32(dgvPlans.SelectedRows[0].Cells["PlanId"].Value);
                string planName = dgvPlans.SelectedRows[0].Cells["PlanName"].Value.ToString();

                // Buka FormPlan dan berikan ID rencana yang dipilih
                FormPlan planDetailForm = new FormPlan(selectedPlanId, planName); // Melewatkan nama juga untuk judul FormPlan
                planDetailForm.ShowDialog(); // Tampilkan sebagai dialog modal

                // Setelah FormPlan ditutup, Anda bisa refresh dashboard jika perlu
                // Misalnya, jika ada aktivitas yang selesai di FormPlan yang memengaruhi ringkasan di dashboard
                LoadPlans();
            }
            else
            {
                MessageBox.Show("Pilih rencana untuk melihat aktivitasnya.", "Peringatan", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }
    }
}