using System.Drawing; // Perlu diimpor untuk Point, Size, Color, Font
using System.Windows.Forms; // Perlu diimpor untuk Windows Forms

namespace Focient.Forms
{
    partial class ErrorPanel : System.Windows.Forms.UserControl
    {
        private System.ComponentModel.IContainer designerComponents = null;
        protected override void Dispose(bool disposing)
        {
            if (disposing && (designerComponents != null))
            {
                designerComponents.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Component Designer generated code
        private void InitializeComponent()
        {
            this.lblErrorDesigner = new System.Windows.Forms.Label(); // Updated name for designer-generated field
            this.SuspendLayout(); // Memulai layout suspending

            //
            // lblErrorDesigner
            //
            this.lblErrorDesigner.AutoSize = true; // Ukuran label otomatis menyesuaikan teks
            this.lblErrorDesigner.BackColor = System.Drawing.Color.Transparent; // Agar latar belakang panel terlihat
            this.lblErrorDesigner.Dock = System.Windows.Forms.DockStyle.Fill; // Mengisi seluruh area panel
            this.lblErrorDesigner.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0))); // Font standar
            this.lblErrorDesigner.ForeColor = System.Drawing.Color.DarkRed; // Warna teks untuk pesan error
            this.lblErrorDesigner.Location = new System.Drawing.Point(0, 0); // Posisi relatif ke parent controlnya (ErrorPanel)
            this.lblErrorDesigner.Name = "lblErrorDesigner";
            this.lblErrorDesigner.Padding = new System.Windows.Forms.Padding(5); // Jarak padding
            this.lblErrorDesigner.Size = new System.Drawing.Size(294, 44); // Ukuran awal (akan menyesuaikan dengan teks)
            this.lblErrorDesigner.TabIndex = 0;
            this.lblErrorDesigner.Text = "Error Message"; // Teks placeholder
            this.lblErrorDesigner.TextAlign = System.Drawing.ContentAlignment.MiddleCenter; // Teks di tengah secara horizontal dan vertikal

            //
            // ErrorPanel
            //
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F); // Skala otomatis
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.LightCoral; // Warna latar belakang untuk panel error
            this.Controls.Add(this.lblErrorDesigner); // Updated reference
            this.Name = "ErrorPanel"; // Nama kontrol pengguna
            this.Size = new System.Drawing.Size(300, 50); // Ukuran default panel
            this.Visible = false; // Sembunyikan secara default
            this.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right))); // Mengaitkan ke atas, kiri, dan kanan parent-nya

            this.ResumeLayout(false); // Melanjutkan layout
            this.PerformLayout(); // Memaksa layout ulang
        }

        private System.Windows.Forms.Label lblErrorDesigner; // Updated name for designer-generated field
        
        #endregion
    }
}