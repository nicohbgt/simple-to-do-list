namespace Focient
{
    public static class UserManager
    {
        public static int CurrentUserId { get; private set; }
        public static string CurrentUsername { get; private set; }
        public static string CurrentUserFullName { get; private set; } // Tambahan untuk nama lengkap

        public static void SetCurrentUser(int userId, string username, string fullName)
        {
            CurrentUserId = userId;
            CurrentUsername = username;
            CurrentUserFullName = fullName;
        }

        public static void ClearCurrentUser() // Untuk logout
        {
            CurrentUserId = 0;
            CurrentUsername = null;
            CurrentUserFullName = null;
        }

        public static bool IsLoggedIn
        {
            get { return CurrentUserId > 0; }
        }
    }
}