// Focient/Forms/EditPlanForm.cs
using System;
using System.Windows.Forms;
using Focient.Models;
using Focient.Helpers;

namespace Focient.Forms
{
    public partial class EditPlanForm : Form
    {
        private int _planIdToEdit; // Akan 0 untuk tambah baru, > 0 untuk edit
        private PlanModel _originalPlan; // Menyimpan data asli jika mode edit

        // Konstruktor untuk Tambah Rencana Baru
        public EditPlanForm()
        {
            InitializeComponent();
            _planIdToEdit = 0; // Menandakan mode tambah baru
            this.Text = "Tambah Rencana Baru";
            InitializeIntensityComboBox();
        }

        // Konstruktor untuk Edit Rencana yang Sudah Ada
        public EditPlanForm(int planId)
        {
            InitializeComponent();
            _planIdToEdit = planId; // Menandakan mode edit
            this.Text = "Edit Rencana";
            InitializeIntensityComboBox();
        }

        private void EditPlanForm_Load(object sender, EventArgs e)
        {
            if (_planIdToEdit > 0) // Mode Edit
            {
                try
                {
                    // Muat data rencana dari database
                    // Pastikan hanya user yang login yang bisa mengedit rencananya sendiri
                    _originalPlan = DatabaseHelper.GetPlanById(_planIdToEdit, UserManager.CurrentUserId);

                    if (_originalPlan != null)
                    {
                        // Isi kontrol UI dengan data rencana
                        txtName.Text = _originalPlan.Name;
                        dtpDate.Value = _originalPlan.Date;
                        txtDescription.Text = _originalPlan.Description;

                        // Set ComboBox IntensityLevel
                        // Pastikan item di cbIntensityLevel sama dengan string enum
                        IntensityLevel.SelectedItem = _originalPlan.IntensityLevel.ToString();
                    }
                    else
                    {
                        MessageBox.Show("Rencana tidak ditemukan atau Anda tidak memiliki izin untuk mengeditnya.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        this.Close(); // Tutup form jika rencana tidak ditemukan/tidak diizinkan
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"Gagal memuat data rencana: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    this.Close();
                }
            }
            else // Mode Tambah Baru
            {
                Date.Value = DateTime.Today; // Default tanggal hari ini
                // Default intensity bisa LOW atau null tergantung kebutuhan
                IntensityLevel.SelectedIndex = 0; // Pilih item pertama (misal LOW)
            }
        }

        private void InitializeIntensityComboBox()
        {
            // Isi ComboBox dengan nilai dari enum IntensityLevel
            IntensityLevel.Items.Clear();
            foreach (string level in Enum.GetNames(typeof(IntensityLevel)))
            {
                IntensityLevel.Items.Add(level);
            }
            if (IntensityLevel.Items.Count > 0)
            {
                IntensityLevel.SelectedIndex = 0; // Default pilih yang pertama (misal LOW)
            }
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            // 1. Validasi Input
            string planName = Name.Text.Trim();
            DateTime planDate = Date.Value.Date;
            string description = txtDescription.Text.Trim();
            IntensityLevel selectedIntensity;

            if (string.IsNullOrWhiteSpace(planName))
            {
                MessageBox.Show("Nama rencana tidak boleh kosong.", "Validasi", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            if (IntensityLevel.SelectedItem == null)
            {
                MessageBox.Show("Pilih tingkat intensitas.", "Validasi", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            // Konversi string dari ComboBox ke enum IntensityLevel
            selectedIntensity = (IntensityLevel)Enum.Parse(typeof(IntensityLevel), cbIntensityLevel.SelectedItem.ToString());

            try
            {
                if (_planIdToEdit == 0) // Mode Tambah Baru
                {
                    PlanModel newPlan = new PlanModel
                    {
                        UserId = UserManager.CurrentUserId, // Kaitkan dengan user yang sedang login
                        Name = planName,
                        Date = planDate,
                        IntensityLevel = selectedIntensity,
                        Description = string.IsNullOrWhiteSpace(description) ? null : description
                    };
                    DatabaseHelper.InsertPlan(newPlan);
                }
                else // Mode Edit
                {
                    // Pastikan _originalPlan tidak null jika di mode edit
                    if (_originalPlan == null)
                    {
                        MessageBox.Show("Terjadi kesalahan: Data rencana asli tidak ditemukan.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        this.DialogResult = DialogResult.Cancel;
                        this.Close();
                        return;
                    }

                    _originalPlan.Name = planName;
                    _originalPlan.Date = planDate;
                    _originalPlan.IntensityLevel = selectedIntensity;
                    _originalPlan.Description = string.IsNullOrWhiteSpace(description) ? null : description;

                    // Perbarui di database
                    bool success = DatabaseHelper.UpdatePlan(_originalPlan);
                    if (!success)
                    {
                        MessageBox.Show("Gagal memperbarui rencana. Mungkin ada masalah database atau izin.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        this.DialogResult = DialogResult.Cancel;
                        return;
                    }
                }

                this.DialogResult = DialogResult.OK; // Beri tahu form pemanggil (Dashboard) bahwa operasi berhasil
                this.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Terjadi kesalahan saat menyimpan rencana: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.Cancel; // Beri tahu form pemanggil bahwa operasi dibatalkan
            this.Close();
        }
    }
}