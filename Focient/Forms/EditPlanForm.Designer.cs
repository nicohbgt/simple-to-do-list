using System.ComponentModel;
using System.Windows.Forms;
using System.Drawing; // Pastikan System.Drawing diimpor untuk Point, Size, Color, Font

namespace Focient.Forms
{
    partial class EditPlanForm
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.txtPlanName = new System.Windows.Forms.TextBox();
            this.txtDescription = new System.Windows.Forms.TextBox();
            this.lblPlanName = new System.Windows.Forms.Label();
            this.lblDescription = new System.Windows.Forms.Label();
            this.lblDate = new System.Windows.Forms.Label();
            this.dtpPlanDate = new System.Windows.Forms.DateTimePicker(); // Tambahkan DateTimePicker
            this.lblIntensity = new System.Windows.Forms.Label(); // Tambahkan Label untuk Intensity
            this.comboIntensity = new System.Windows.Forms.ComboBox();
            this.btnSaveChanges = new System.Windows.Forms.Button();
            this.btnCancel = new System.Windows.Forms.Button();
            this.errorPanel = new System.Windows.Forms.Panel();
            this.lblError = new System.Windows.Forms.Label();
            this.errorPanel.SuspendLayout(); // Inisialisasi GroupBox/Panel yang berisi komponen
            this.SuspendLayout();

            // 
            // txtPlanName
            // 
            this.txtPlanName.Location = new System.Drawing.Point(120, 20); // Sesuaikan posisi
            this.txtPlanName.Name = "txtPlanName";
            this.txtPlanName.Size = new System.Drawing.Size(300, 23);
            this.txtPlanName.TabIndex = 0;
            // 
            // lblPlanName
            // 
            this.lblPlanName.AutoSize = true;
            this.lblPlanName.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0))); // Gunakan properti Font lengkap
            this.lblPlanName.Location = new System.Drawing.Point(20, 23); // Sesuaikan posisi
            this.lblPlanName.Name = "lblPlanName";
            this.lblPlanName.Size = new System.Drawing.Size(76, 19); // Sesuaikan Size, 19 adalah tinggi default untuk Font 10
            this.lblPlanName.TabIndex = 1;
            this.lblPlanName.Text = "Plan Name:";
            // 
            // lblDescription
            // 
            this.lblDescription.AutoSize = true;
            this.lblDescription.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblDescription.Location = new System.Drawing.Point(20, 103); // Sesuaikan posisi setelah Date dan Intensity
            this.lblDescription.Name = "lblDescription";
            this.lblDescription.Size = new System.Drawing.Size(81, 19);
            this.lblDescription.TabIndex = 3;
            this.lblDescription.Text = "Description:";
            // 
            // txtDescription
            // 
            this.txtDescription.Location = new System.Drawing.Point(120, 100); // Sesuaikan posisi
            this.txtDescription.Multiline = true;
            this.txtDescription.Name = "txtDescription";
            this.txtDescription.Size = new System.Drawing.Size(300, 100);
            this.txtDescription.TabIndex = 2;
            // 
            // lblDate
            // 
            this.lblDate.AutoSize = true;
            this.lblDate.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblDate.Location = new System.Drawing.Point(20, 49); // Sesuaikan posisi
            this.lblDate.Name = "lblDate";
            this.lblDate.Size = new System.Drawing.Size(41, 19);
            this.lblDate.TabIndex = 4;
            this.lblDate.Text = "Date:";
            // 
            // dtpPlanDate
            // 
            this.dtpPlanDate.Format = System.Windows.Forms.DateTimePickerFormat.Short; // Format tanggal singkat
            this.dtpPlanDate.Location = new System.Drawing.Point(120, 46); // Sesuaikan posisi
            this.dtpPlanDate.Name = "dtpPlanDate";
            this.dtpPlanDate.Size = new System.Drawing.Size(150, 23);
            this.dtpPlanDate.TabIndex = 5;
            // 
            // lblIntensity
            // 
            this.lblIntensity.AutoSize = true;
            this.lblIntensity.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblIntensity.Location = new System.Drawing.Point(20, 76); // Sesuaikan posisi
            this.lblIntensity.Name = "lblIntensity";
            this.lblIntensity.Size = new System.Drawing.Size(65, 19);
            this.lblIntensity.TabIndex = 6;
            this.lblIntensity.Text = "Intensity:";
            // 
            // comboIntensity
            // 
            this.comboIntensity.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboIntensity.Items.AddRange(new object[] {
            "LOW", // Tambahkan item default
            "MEDIUM",
            "HIGH"});
            this.comboIntensity.Location = new System.Drawing.Point(120, 73); // Sesuaikan posisi
            this.comboIntensity.Name = "comboIntensity";
            this.comboIntensity.Size = new System.Drawing.Size(150, 23);
            this.comboIntensity.TabIndex = 7;
            // 
            // btnSaveChanges
            // 
            this.btnSaveChanges.Location = new System.Drawing.Point(265, 220); // Sesuaikan posisi tombol
            this.btnSaveChanges.Name = "btnSaveChanges";
            this.btnSaveChanges.Size = new System.Drawing.Size(100, 30);
            this.btnSaveChanges.TabIndex = 8;
            this.btnSaveChanges.Text = "Save Changes";
            this.btnSaveChanges.UseVisualStyleBackColor = true;
            // 
            // btnCancel
            // 
            this.btnCancel.Location = new System.Drawing.Point(155, 220); // Sesuaikan posisi tombol
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new System.Drawing.Size(100, 30);
            this.btnCancel.TabIndex = 9;
            this.btnCancel.Text = "Cancel";
            this.btnCancel.UseVisualStyleBackColor = true;
            // 
            // errorPanel
            // 
            this.errorPanel.BackColor = System.Drawing.Color.LightCoral; // Warna latar belakang untuk error
            this.errorPanel.Controls.Add(this.lblError);
            this.errorPanel.Location = new System.Drawing.Point(0, 0); // Biasanya di bagian atas atau bawah form
            this.errorPanel.Name = "errorPanel";
            this.errorPanel.Size = new System.Drawing.Size(450, 30); // Sesuaikan lebar form
            this.errorPanel.TabIndex = 10;
            this.errorPanel.Visible = false; // Sembunyikan secara default
            this.errorPanel.Dock = System.Windows.Forms.DockStyle.Top; // Dock di bagian atas form
            // 
            // lblError
            // 
            this.lblError.AutoSize = true;
            this.lblError.ForeColor = System.Drawing.Color.DarkRed; // Warna teks error
            this.lblError.Location = new System.Drawing.Point(10, 8); // Posisi teks dalam panel
            this.lblError.Name = "lblError";
            this.lblError.Size = new System.Drawing.Size(32, 15);
            this.lblError.TabIndex = 0;
            this.lblError.Text = "Error Message";
            // 
            // EditPlanForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F); // Default DPI scaling
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(450, 270); // Sesuaikan ukuran form
            this.Controls.Add(this.errorPanel); // Tambahkan errorPanel ke Controls form paling awal
            this.Controls.Add(this.btnCancel);
            this.Controls.Add(this.btnSaveChanges);
            this.Controls.Add(this.comboIntensity);
            this.Controls.Add(this.lblIntensity); // Tambahkan label intensity
            this.Controls.Add(this.dtpPlanDate); // Tambahkan DateTimePicker
            this.Controls.Add(this.lblDate);
            this.Controls.Add(this.lblDescription);
            this.Controls.Add(this.txtDescription);
            this.Controls.Add(this.lblPlanName);
            this.Controls.Add(this.txtPlanName);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog; // Fixed size
            this.MaximizeBox = false; // Nonaktifkan maximize
            this.MinimizeBox = false; // Nonaktifkan minimize
            this.Name = "EditPlanForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent; // Muncul di tengah parent form
            this.Text = "Edit Plan";
            this.errorPanel.ResumeLayout(false);
            this.errorPanel.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TextBox txtPlanName;
        private System.Windows.Forms.TextBox txtDescription;
        private System.Windows.Forms.Label lblPlanName;
        private System.Windows.Forms.Label lblDescription;
        private System.Windows.Forms.Label lblDate;
        private System.Windows.Forms.DateTimePicker dtpPlanDate; // Deklarasi DateTimePicker
        private System.Windows.Forms.Label lblIntensity; // Deklarasi Label Intensity
        private System.Windows.Forms.ComboBox comboIntensity;
        private System.Windows.Forms.Button btnSaveChanges;
        private System.Windows.Forms.Button btnCancel;
        private System.Windows.Forms.Panel errorPanel;
        private System.Windows.Forms.Label lblError;
    }
}