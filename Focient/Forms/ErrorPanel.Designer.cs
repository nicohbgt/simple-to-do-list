// Focient/Forms/ErrorPanel.Designer.cs
// Ini adalah file yang dihasilkan secara otomatis oleh Windows Forms Designer.
// Jangan mengeditnya secara manual kecuali Anda tahu persis apa yang Anda lakukan.

using System.Drawing; // Perlu diimpor untuk Point, Size, Color, Font

namespace Focient.Forms
{
    partial class ErrorPanel
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

        #region Component Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.lblError = new System.Windows.Forms.Label(); // Inisialisasi Label
            this.SuspendLayout(); // Memulai layout suspending

            //
            // lblError
            //
            this.lblError.AutoSize = true; // Ukuran label otomatis menyesuaikan teks
            this.lblError.BackColor = System.Drawing.Color.Transparent; // Agar latar belakang panel terlihat
            this.lblError.Dock = System.Windows.Forms.DockStyle.Fill; // Mengisi seluruh area panel
            this.lblError.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0))); // Font standar
            this.lblError.ForeColor = System.Drawing.Color.DarkRed; // Warna teks untuk pesan error
            this.lblError.Location = new System.Drawing.Point(0, 0); // Posisi relatif ke parent controlnya (ErrorPanel)
            this.lblError.Name = "lblError";
            this.lblError.Padding = new System.Windows.Forms.Padding(5); // Jarak padding
            this.lblError.Size = new System.Drawing.Size(294, 44); // Ukuran awal (akan menyesuaikan dengan teks)
            this.lblError.TabIndex = 0;
            this.lblError.Text = "Error Message"; // Teks placeholder
            this.lblError.TextAlign = System.Drawing.ContentAlignment.MiddleCenter; // Teks di tengah secara horizontal dan vertikal

            //
            // ErrorPanel
            //
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F); // Skala otomatis
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.LightCoral; // Warna latar belakang untuk panel error
            this.Controls.Add(this.lblError); // Tambahkan lblError ke dalam panel
            this.Name = "ErrorPanel"; // Nama kontrol pengguna
            this.Size = new System.Drawing.Size(300, 50); // Ukuran default panel
            this.Visible = false; // Sembunyikan secara default
            this.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right))); // Mengaitkan ke atas, kiri, dan kanan parent-nya

            this.ResumeLayout(false); // Melanjutkan layout
            this.PerformLayout(); // Memaksa layout ulang
        }

        private System.Windows.Forms.Label lblError; // Deklarasi kontrol Label
        
        #endregion
    }
}