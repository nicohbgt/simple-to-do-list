namespace Focient;

public partial class Form1 : Form
{
    public Form1()
    {
        InitializeComponent();
    }
    
    private void btnRegister_Click(object sender, EventArgs e)
    {
        if (string.IsNullOrWhiteSpace(txtUsername.Text) || string.IsNullOrWhiteSpace(txtPassword.Text))
        {
            MessageBox.Show("Username and Password are required!");
            return;
        }

        var user = new UserModel
        {
            Username = txtUsername.Text,
            FullName = txtFullName.Text,
            Area = txtArea.Text,
            DateOfBirth = dtpDateOfBirth.Value
        };

        user.SetPassword(txtPassword.Text); // Hash password

        try
        {
            DatabaseHelper.InsertUser(user);
            MessageBox.Show("Registration successful!");
            // Tambahan: bisa arahkan ke halaman login, atau reset form
        }
        catch (Exception ex)
        {
            MessageBox.Show("Registration failed: " + ex.Message);
        }
    }
}