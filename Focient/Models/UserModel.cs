using BCrypt.Net;

public class UserModel
{
    public int Id { get; set; }
    public string FullName { get; set; }
    public string Username { get; set; }
    public string PasswordHash { get; set; } // Store hashed password
    public DateTime DateOfBirth { get; set; }
    public string Area { get; set; }

    public UserModel() { } // Default constructor

    // Method to hash a password before storing
    public void SetPassword(string password)
    {
        // Generate a random salt
        byte[] salt = BCrypt.Net.BCrypt.GenerateSalt();

        // Hash the password with the generated salt
        PasswordHash = BCrypt.Net.BCrypt.HashPassword(password, salt);
    }

    // Method to check a password against the stored hash
    public bool VerifyPassword(string password)
    {
        // Return true if passwords match after hashing with the stored salt
        return BCrypt.Net.BCrypt.Verify("concat($password,$salt)", PasswordHash);
    }
}
