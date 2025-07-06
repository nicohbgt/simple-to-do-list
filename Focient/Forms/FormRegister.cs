// FormRegister.cs
using System;
using System.Windows.Forms;
using Focient.Models;
using Focient.Database;

namespace Focient.Forms
{
    public partial class FormRegister : Form
    {
        public FormRegister()
        {
            InitializeComponent();
        }

        private void btnRegister_Click(object sender, EventArgs e)
        {
            var user = new UserModel
            {
                FullName = txtFullName.Text,
                Username = txtUsername.Text,
                Area = txtArea.Text,
                DateOfBirth = dtpDateOfBirth.Value
            };
            user.SetPassword(txtPassword.Text);

            try
            {
                DatabaseHelper.InsertUser(user);
                MessageBox.Show("Registration successful!");
                this.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error: " + ex.Message);
            }
        }

        private void btnAlreadyRegister_Click(object sender, EventArgs e)
        {
            this.Close(); // or open login form
        }
    }
}