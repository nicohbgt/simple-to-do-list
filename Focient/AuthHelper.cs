using BCrypt.Net;

public static class AuthHelper
{
    public static string HashPassword(string password)
    {
        return BCrypt.Net.BCrypt.HashPassword(password);
    }

    public static bool VerifyPassword(string inputPassword, string storedHash)
    {
        return BCrypt.Net.BCrypt.Verify(inputPassword, storedHash);
    }
}
