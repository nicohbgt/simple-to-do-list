using System;
using BCrypt.Net;

public class UserModel
{
    public int Id { get; set; }
    public string FullName { get; set; }
    public string Username { get; set; }
    public string PasswordHash { get; set; } // Hashed password
    public DateTime DateOfBirth { get; set; }
    public string Area { get; set; }

    public UserModel() { }

    // Method to hash and store password securely
    public void SetPassword(string password)
    {
        PasswordHash = BCrypt.Net.BCrypt.HashPassword(password);
    }

    // Method to verify a raw password with the stored hash
    public bool VerifyPassword(string password)
    {
        return BCrypt.Net.BCrypt.Verify(password, PasswordHash);
    }
}