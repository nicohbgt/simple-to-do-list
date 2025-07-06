using System.Windows.Forms;

// ... (other using directives)

namespace Focient.Forms
{
    partial class ErrorPanel
    {
        private System.ComponentModel.IContainer components = null;

        private Label lblError;

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