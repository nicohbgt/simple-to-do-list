namespace Focient.Forms
{
    partial class FormLogin
    {
        private System.ComponentModel.IContainer components = null;

        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null)) components.Dispose();
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        private void InitializeComponent()
        {
            this.txtUsername = new System.Windows.Forms.TextBox();
            this.txtPassword = new System.Windows.Forms.TextBox();
            this.btnLogin = new System.Windows.Forms.Button();
            this.lblTitle = new System.Windows.Forms.Label();
            this.lblSubtitle = new System.Windows.Forms.Label();
            this.lblLoginHeader = new System.Windows.Forms.Label();
            this.SuspendLayout();

            // Title (FOCIENT)
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

            // Header: LOGIN
            this.lblLoginHeader.Text = "LOGIN";
            this.lblLoginHeader.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Bold);
            this.lblLoginHeader.Location = new System.Drawing.Point(140, 80);
            this.lblLoginHeader.Size = new System.Drawing.Size(120, 30);
            this.lblLoginHeader.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;

            // Username
            this.txtUsername.PlaceholderText = "Username";
            this.txtUsername.Location = new System.Drawing.Point(100, 120);
            this.txtUsername.Size = new System.Drawing.Size(200, 23);

            // Password
            this.txtPassword.PlaceholderText = "Password";
            this.txtPassword.Location = new System.Drawing.Point(100, 155);
            this.txtPassword.Size = new System.Drawing.Size(200, 23);
            this.txtPassword.UseSystemPasswordChar = true;

            // Login Button
            this.btnLogin.Text = "Login";
            this.btnLogin.Location = new System.Drawing.Point(100, 190);
            this.btnLogin.Size = new System.Drawing.Size(200, 30);
            this.btnLogin.UseVisualStyleBackColor = true;
            this.btnLogin.Click += new System.EventHandler(this.btnLogin_Click);

            // FormLogin
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.ClientSize = new System.Drawing.Size(400, 250);
            this.Controls.Add(this.lblTitle);
            this.Controls.Add(this.lblSubtitle);
            this.Controls.Add(this.lblLoginHeader);
            this.Controls.Add(this.txtUsername);
            this.Controls.Add(this.txtPassword);
            this.Controls.Add(this.btnLogin);
            this.Name = "FormLogin";
            this.Text = "Login";
            this.ResumeLayout(false);
            this.PerformLayout();
        }

        #endregion

        private System.Windows.Forms.TextBox txtUsername;
        private System.Windows.Forms.TextBox txtPassword;
        private System.Windows.Forms.Button btnLogin;
        private System.Windows.Forms.Label lblTitle;
        private System.Windows.Forms.Label lblSubtitle;
        private System.Windows.Forms.Label lblLoginHeader;
    }
}
