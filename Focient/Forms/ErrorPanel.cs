using System.Windows.Forms;

// ... (other using directives)

namespace Focient.Forms
{
    partial class ErrorPanel
    {
        private System.ComponentModel.IContainer components = null;

        private Label lblError;

        private void InitializeComponent()
        {
            this.lblError = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // lblError
            // 
            this.lblError.AutoSize = true;
            this.lblError.Location = new System.Drawing.Point(50, 20);
            this.lblError.Name = "lblError";
            this.lblError.Size = new System.Drawing.Size(250, 25);
            this.lblError.TabIndex = 0;
            this.lblError.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // ErrorPanel
            // 
            this.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.Controls.Add(this.lblError);
            this.Name = "ErrorPanel";
            this.Size = new System.Drawing.Size(300, 50);
            this.Visible = false;
            this.ResumeLayout(false);
            this.PerformLayout();
        }

        public void ShowError(string errorText)
        {
            lblError.Text = errorText;
            this.Visible = true;
            this.Location = new Point(50, this.ParentForm.PointToClient(new Point(0, this.ParentForm.Height - this.Height - 20)).X, 20); // Adjust position relative to its parent form
        }

        public void HideError()
        {
            this.Visible = false;
        }
    }
}