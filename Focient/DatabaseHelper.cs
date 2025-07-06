public static class DatabaseHelper
{
    private static string connectionString = "Data Source=focient.db;Version=3;";

    public static void InitializeDatabase()
    {
        if (!File.Exists("focient.db"))
        {
            SQLiteConnection.CreateFile("focient.db");
        }

        using var conn = new SQLiteConnection(connectionString);
        conn.Open();

        string createUsersTable = @"
            CREATE TABLE IF NOT EXISTS Users (
                Id INTEGER PRIMARY KEY AUTOINCREMENT,
                Username TEXT NOT NULL UNIQUE,
                PasswordHash TEXT NOT NULL,
                FullName TEXT,
                DateOfBirth DATE,
                Area TEXT
            );";

        string createTasksTable = @"
            CREATE TABLE IF NOT EXISTS Tasks (
                Id INTEGER PRIMARY KEY AUTOINCREMENT,
                UserId INTEGER NOT NULL,
                Title TEXT NOT NULL,
                Description TEXT,
                Status TEXT DEFAULT 'Open' CHECK (Status IN ('Open', 'Completed')),
                FOREIGN KEY (UserId) REFERENCES Users(Id)
            );";

        string createPlansTable = @"
            CREATE TABLE IF NOT EXISTS Plans (
                Id INTEGER PRIMARY KEY AUTOINCREMENT,
                UserId INTEGER,
                PlanName TEXT NOT NULL,
                DateOfPlan DATE NOT NULL,
                IntensityTabIndex INTEGER DEFAULT 0, -- Assuming IntensityLevel will be an integer index (0-2) for simplicity
                Description TEXT,
                FOREIGN KEY (UserId) REFERENCES Users(Id)
            );";

        string createActivitiesTable = @"
            CREATE TABLE IF NOT EXISTS Activities (
                Id INTEGER PRIMARY KEY AUTOINCREMENT,
                PlanId INTEGER,
                ActivityName TEXT NOT NULL,
                StartTime DATETIME NOT NULL,
                EndTime DATETIME NOT NULL,
                IntensityLevel INTEGER,
                FOREIGN KEY (PlanId) REFERENCES Plans(Id)
            );";

        using var cmd1 = new SQLiteCommand(createUsersTable, conn);
        cmd1.ExecuteNonQuery();

        using var cmd2 = new SQLiteCommand(createTasksTable, conn);
        cmd2.ExecuteNonQuery();

        using var cmd3 = new SQLiteCommand(createPlansTable, conn);
        cmd3.ExecuteNonQuery();

        using var cmd4 = new SQLiteCommand(createActivitiesTable, conn);
        cmd4.ExecuteNonQuery();
    }
}

