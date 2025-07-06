// Focient/Forms/FormPlan.cs
using System;
using System.Collections.Generic;
using System.Windows.Forms;
using Focient.Models;
using Focient.Helpers;

namespace Focient.Forms
{
    public partial class FormPlan : Form
    {
        private int _currentPlanId;
        private string _currentPlanName;
        private ActivityModel _selectedActivity; // Menyimpan aktivitas yang sedang dipilih/diedit

        // Konstruktor (sudah direvisi sebelumnya, pastikan sesuai)
        public FormPlan(int planId, string planName)
        {
            InitializeComponent();
            _currentPlanId = planId;
            _currentPlanName = planName;
            this.Text = $"Aktivitas untuk Rencana: {_currentPlanName}";

            this.Load += FormPlan_Load; // Pastikan event Load diatur

            // Atur DataGridView
            dgvActivities.ReadOnly = true;
            dgvActivities.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            dgvActivities.AutoGenerateColumns = false;

            // Tambahkan kolom secara manual jika belum ada di Designer
            if (dgvActivities.Columns.Count == 0)
            {
                dgvActivities.Columns.Add(new DataGridViewTextBoxColumn { DataPropertyName = "Id", HeaderText = "ID", Name = "ActivityId", Visible = false }); // Sembunyikan ID
                dgvActivities.Columns.Add(new DataGridViewTextBoxColumn { DataPropertyName = "Name", HeaderText = "Aktivitas", Name = "ActivityName" });
                dgvActivities.Columns.Add(new DataGridViewTextBoxColumn { DataPropertyName = "StartTime", HeaderText = "Mulai", Name = "ActivityStartTime", DefaultCellStyle = { Format = "HH:mm" } });
                dgvActivities.Columns.Add(new DataGridViewTextBoxColumn { DataPropertyName = "EndTime", HeaderText = "Selesai", Name = "ActivityEndTime", DefaultCellStyle = { Format = "HH:mm" } });
                dgvActivities.Columns.Add(new DataGridViewCheckBoxColumn { DataPropertyName = "IsCompleted", HeaderText = "Selesai?", Name = "ActivityIsCompleted" });
            }

            // Tambahkan event handler untuk tombol dan DataGridView
            btnAddActivity.Click += btnAddActivity_Click;
            btnUpdateActivity.Click += btnUpdateActivity_Click;
            btnDeleteActivity.Click += btnDeleteActivity_Click;
            dgvActivities.CellClick += dgvActivities_CellClick; // Untuk memilih aktivitas

            // Inisialisasi tampilan tombol: Awalnya hanya "Add" yang aktif
            ResetActivityForm();
        }

        private void FormPlan_Load(object sender, EventArgs e)
        {
            LoadActivitiesForPlan(); // Muat aktivitas saat form dimuat
        }

        private void LoadActivitiesForPlan()
        {
            try
            {
                List<ActivityModel> activities = DatabaseHelper.GetActivitiesByPlanId(_currentPlanId);
                dgvActivities.DataSource = activities;

                // Opsional: Atur lebar kolom
                dgvActivities.AutoResizeColumns(DataGridViewAutoSizeColumnsMode.AllCells);
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Gagal memuat aktivitas: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void ResetActivityForm()
        {
            txtActivityName.Clear();
            // Set DateTimePickers ke waktu default yang masuk akal, misal 08:00 dan 09:00
            dtpStartTime.Value = DateTime.Today.AddHours(8).AddMinutes(0);
            dtpEndTime.Value = DateTime.Today.AddHours(9).AddMinutes(0);
            chkIsCompleted.Checked = false;

            _selectedActivity = null; // Hapus aktivitas yang dipilih
            btnAddActivity.Visible = true;
            btnUpdateActivity.Visible = false;
            btnDeleteActivity.Visible = false;
        }

        private void btnAddActivity_Click(object sender, EventArgs e)
        {
            // 1. Validasi Input
            string activityName = txtActivityName.Text.Trim();
            DateTime startTime = dtpStartTime.Value; // Hanya waktu yang relevan dari DTP
            DateTime endTime = dtpEndTime.Value;
            bool isCompleted = chkIsCompleted.Checked;

            if (string.IsNullOrWhiteSpace(activityName))
            {
                MessageBox.Show("Nama aktivitas tidak boleh kosong.", "Validasi", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            if (startTime >= endTime)
            {
                MessageBox.Show("Waktu selesai harus setelah waktu mulai.", "Validasi", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            try
            {
                // 2. Buat objek ActivityModel
                ActivityModel newActivity = new ActivityModel
                {
                    PlanId = _currentPlanId, // Kaitkan dengan rencana yang sedang dibuka
                    Name = activityName,
                    StartTime = startTime,
                    EndTime = endTime,
                    IsCompleted = isCompleted
                };

                // 3. Simpan ke Database
                DatabaseHelper.InsertActivity(newActivity);

                MessageBox.Show("Aktivitas berhasil ditambahkan.", "Sukses", MessageBoxButtons.OK, MessageBoxIcon.Information);
                LoadActivitiesForPlan(); // Refresh daftar aktivitas
                ResetActivityForm(); // Bersihkan form
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Terjadi kesalahan saat menambahkan aktivitas: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void dgvActivities_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0) // Pastikan bukan header kolom
            {
                DataGridViewRow selectedRow = dgvActivities.Rows[e.RowIndex];
                _selectedActivity = (ActivityModel)selectedRow.DataBoundItem; // Ambil objek model dari baris

                // Isi kontrol UI dengan data aktivitas yang dipilih
                txtActivityName.Text = _selectedActivity.Name;
                dtpStartTime.Value = _selectedActivity.StartTime;
                dtpEndTime.Value = _selectedActivity.EndTime;
                chkIsCompleted.Checked = _selectedActivity.IsCompleted;

                // Tampilkan tombol Update dan Delete, sembunyikan Add
                btnAddActivity.Visible = false;
                btnUpdateActivity.Visible = true;
                btnDeleteActivity.Visible = true;
            }
        }

        private void btnUpdateActivity_Click(object sender, EventArgs e)
        {
            if (_selectedActivity == null)
            {
                MessageBox.Show("Pilih aktivitas yang ingin diperbarui.", "Peringatan", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            // 1. Validasi Input (sama seperti Add)
            string activityName = txtActivityName.Text.Trim();
            DateTime startTime = dtpStartTime.Value;
            DateTime endTime = dtpEndTime.Value;
            bool isCompleted = chkIsCompleted.Checked;

            if (string.IsNullOrWhiteSpace(activityName))
            {
                MessageBox.Show("Nama aktivitas tidak boleh kosong.", "Validasi", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            if (startTime >= endTime)
            {
                MessageBox.Show("Waktu selesai harus setelah waktu mulai.", "Validasi", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            try
            {
                // 2. Perbarui objek _selectedActivity
                _selectedActivity.Name = activityName;
                _selectedActivity.StartTime = startTime;
                _selectedActivity.EndTime = endTime;
                _selectedActivity.IsCompleted = isCompleted;

                // 3. Perbarui di Database
                DatabaseHelper.UpdateActivity(_selectedActivity);

                MessageBox.Show("Aktivitas berhasil diperbarui.", "Sukses", MessageBoxButtons.OK, MessageBoxIcon.Information);
                LoadActivitiesForPlan(); // Refresh daftar aktivitas
                ResetActivityForm(); // Bersihkan form dan reset tombol
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Terjadi kesalahan saat memperbarui aktivitas: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnDeleteActivity_Click(object sender, EventArgs e)
        {
            if (_selectedActivity == null)
            {
                MessageBox.Show("Pilih aktivitas yang ingin dihapus.", "Peringatan", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            DialogResult confirm = MessageBox.Show(
                $"Apakah Anda yakin ingin menghapus aktivitas '{_selectedActivity.Name}'?",
                "Konfirmasi Hapus",
                MessageBoxButtons.YesNo,
                MessageBoxIcon.Question
            );

            if (confirm == DialogResult.Yes)
            {
                try
                {
                    // Hapus aktivitas dari database
                    DatabaseHelper.DeleteActivity(_selectedActivity.Id);

                    MessageBox.Show("Aktivitas berhasil dihapus.", "Sukses", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    LoadActivitiesForPlan(); // Refresh daftar aktivitas
                    ResetActivityForm(); // Bersihkan form dan reset tombol
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"Terjadi kesalahan saat menghapus aktivitas: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        // Opsional: Tombol Back atau tutup Form
        private void btnBack_Click(object sender, EventArgs e)
        {
            this.Close(); // Cukup tutup FormPlan, dan FormDashboard akan di-refresh
        }
    }
}