namespace Focient.Forms
{
    partial class FormRegister
    {
        private System.ComponentModel.IContainer components = null;

        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
                components.Dispose();
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        private void InitializeComponent()
        {
            this.txtFullName = new System.Windows.Forms.TextBox();
            this.txtUsername = new System.Windows.Forms.TextBox();
            this.txtPassword = new System.Windows.Forms.TextBox();
            this.dtpDateOfBirth = new System.Windows.Forms.DateTimePicker();
            this.txtArea = new System.Windows.Forms.TextBox();
            this.btnRegister = new System.Windows.Forms.Button();
            this.btnAlreadyRegister = new System.Windows.Forms.Button();
            this.lblTitle = new System.Windows.Forms.Label();
            this.lblSubtitle = new System.Windows.Forms.Label();
            this.SuspendLayout();

            // Form Title
            this.lblTitle.Text = "FOCIENT";
            this.lblTitle.Font = new System.Drawing.Font("Segoe UI", 16F, System.Drawing.FontStyle.Bold);
            this.lblTitle.Location = new System.Drawing.Point(100, 20);
            this.lblTitle.Size = new System.Drawing.Size(200, 30);
            this.lblTitle.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;

            // Subtitle
            this.lblSubtitle.Text = "Simple and Efficient to gain your focus";
            this.lblSubtitle.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.lblSubtitle.Location = new System.Drawing.Point(50, 50);
            this.lblSubtitle.Size = new System.Drawing.Size(300, 20);
            this.lblSubtitle.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;

            // Full Name
            this.txtFullName.PlaceholderText = "Full Name";
            this.txtFullName.Location = new System.Drawing.Point(100, 90);
            this.txtFullName.Size = new System.Drawing.Size(200, 23);

            // Username
            this.txtUsername.PlaceholderText = "Username";
            this.txtUsername.Location = new System.Drawing.Point(100, 125);
            this.txtUsername.Size = new System.Drawing.Size(200, 23);

            // Date of Birth
            this.dtpDateOfBirth.Location = new System.Drawing.Point(100, 160);
            this.dtpDateOfBirth.Size = new System.Drawing.Size(200, 23);

            // Area
            this.txtArea.PlaceholderText = "Area";
            this.txtArea.Location = new System.Drawing.Point(100, 195);
            this.txtArea.Size = new System.Drawing.Size(200, 23);

            // Password
            this.txtPassword.PlaceholderText = "Password";
            this.txtPassword.UseSystemPasswordChar = true;
            this.txtPassword.Location = new System.Drawing.Point(100, 230);
            this.txtPassword.Size = new System.Drawing.Size(200, 23);

            // Register Button
            this.btnRegister.Text = "Continue";
            this.btnRegister.Location = new System.Drawing.Point(100, 270);
            this.btnRegister.Size = new System.Drawing.Size(200, 30);
            this.btnRegister.UseVisualStyleBackColor = true;
            this.btnRegister.Click += new System.EventHandler(this.btnRegister_Click);

            // Already Register Button
            this.btnAlreadyRegister.Text = "Already Register";
            this.btnAlreadyRegister.Location = new System.Drawing.Point(100, 310);
            this.btnAlreadyRegister.Size = new System.Drawing.Size(200, 30);
            this.btnAlreadyRegister.UseVisualStyleBackColor = true;
            this.btnAlreadyRegister.Click += new System.EventHandler(this.btnAlreadyRegister_Click);

            // FormRegister
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.ClientSize = new System.Drawing.Size(400, 370);
            this.Controls.Add(this.lblTitle);
            this.Controls.Add(this.lblSubtitle);
            this.Controls.Add(this.txtFullName);
            this.Controls.Add(this.txtUsername);
            this.Controls.Add(this.dtpDateOfBirth);
            this.Controls.Add(this.txtArea);
            this.Controls.Add(this.txtPassword);
            this.Controls.Add(this.btnRegister);
            this.Controls.Add(this.btnAlreadyRegister);
            this.Name = "FormRegister";
            this.Text = "Register";
            this.ResumeLayout(false);
            this.PerformLayout();
        }

        #endregion

        private System.Windows.Forms.TextBox txtFullName;
        private System.Windows.Forms.TextBox txtUsername;
        private System.Windows.Forms.TextBox txtPassword;
        private System.Windows.Forms.TextBox txtArea;
        private System.Windows.Forms.DateTimePicker dtpDateOfBirth;
        private System.Windows.Forms.Button btnRegister;
        private System.Windows.Forms.Button btnAlreadyRegister;
        private System.Windows.Forms.Label lblTitle;
        private System.Windows.Forms.Label lblSubtitle;
    }
}