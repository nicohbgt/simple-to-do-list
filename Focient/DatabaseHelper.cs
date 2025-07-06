using System;
using System.Data.SQLite;
using System.IO;
using Focient.Models; // Pastikan namespace sesuai struktur project

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
                IntensityTabIndex INTEGER DEFAULT 0,
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

    // ✅ Fungsi untuk menyimpan user ke database
    public static void InsertUser(UserModel user)
    {
        using (var conn = new SQLiteConnection(connectionString))
        {
            conn.Open();
            string query = @"
                INSERT INTO Users (Username, PasswordHash, FullName, DateOfBirth, Area)
                VALUES (@Username, @PasswordHash, @FullName, @DateOfBirth, @Area);";

            using (var cmd = new SQLiteCommand(query, conn))
            {
                cmd.Parameters.AddWithValue("@Username", user.Username);
                cmd.Parameters.AddWithValue("@PasswordHash", user.PasswordHash);
                cmd.Parameters.AddWithValue("@FullName", user.FullName);
                cmd.Parameters.AddWithValue("@DateOfBirth", user.DateOfBirth.ToString("yyyy-MM-dd"));
                cmd.Parameters.AddWithValue("@Area", user.Area);

                cmd.ExecuteNonQuery();
            }
        }
    }

    public static bool HasPlanOnDate(int userId, DateTime date)
    {
        using var conn = new SQLiteConnection(connectionString);
        conn.Open();

        string query = "SELECT COUNT(*) FROM Plans WHERE UserId = @UserId AND DateOfPlan = @Date";
        using var cmd = new SQLiteCommand(query, conn);
        cmd.Parameters.AddWithValue("@UserId", userId);
        cmd.Parameters.AddWithValue("@Date", date.Date);

        var count = Convert.ToInt32(cmd.ExecuteScalar());
        return count > 0;
    }

    public static List<PlanModel> GetPlansForDate(int userId, DateTime date)
    {
        var plans = new List<PlanModel>();
        using var conn = new SQLiteConnection(connectionString);
        conn.Open();

        string query = "SELECT * FROM Plans WHERE UserId = @UserId AND DateOfPlan = @Date";
        using var cmd = new SQLiteCommand(query, conn);
        cmd.Parameters.AddWithValue("@UserId", userId);
        cmd.Parameters.AddWithValue("@Date", date.Date);

        using var reader = cmd.ExecuteReader();
        while (reader.Read())
        {
            plans.Add(new PlanModel
            {
                Id = Convert.ToInt32(reader["Id"]),
                UserId = Convert.ToInt32(reader["UserId"]),
                PlanName = reader["PlanName"].ToString(),
                DateOfPlan = DateTime.Parse(reader["DateOfPlan"].ToString()),
                Intensity = (IntensityLevel)Convert.ToInt32(reader["IntensityTabIndex"]),
                Description = reader["Description"].ToString()
            });
        }

        return plans;
    }

    public static List<ActivityModel> GetActivitiesByPlanId(int planId)
    {
        var activities = new List<ActivityModel>();
        using var conn = new SQLiteConnection(connectionString);
        conn.Open();

        string query = "SELECT * FROM Activities WHERE PlanId = @PlanId";
        using var cmd = new SQLiteCommand(query, conn);
        cmd.Parameters.AddWithValue("@PlanId", planId);

        using var reader = cmd.ExecuteReader();
        while (reader.Read())
        {
            activities.Add(new ActivityModel
            {
                Id = Convert.ToInt32(reader["Id"]),
                PlanId = Convert.ToInt32(reader["PlanId"]),
                ActivityName = reader["ActivityName"].ToString(),
                StartTime = DateTime.Parse(reader["StartTime"].ToString()),
                EndTime = DateTime.Parse(reader["EndTime"].ToString()),
                Intensity = (IntensityLevel)Convert.ToInt32(reader["IntensityLevel"])
            });
        }

        return activities;
    }

    public static int InsertPlan(PlanModel plan)
    {
        if (plan == null)
            throw new ArgumentNullException(nameof(plan));

        using var conn = new SQLiteConnection(connectionString);
        conn.Open();

        string query = @"
            INSERT INTO Plans (UserId, PlanName, DateOfPlan, IntensityTabIndex, Description)
            VALUES (@UserId, @PlanName, @DateOfPlan, @IntensityTabIndex, @Description);
            SELECT last_insert_rowid();";

        using var cmd = new SQLiteCommand(query, conn);
        cmd.Parameters.AddWithValue("@UserId", plan.UserId);
        cmd.Parameters.AddWithValue("@PlanName", plan.PlanName ?? string.Empty);
        cmd.Parameters.AddWithValue("@DateOfPlan", plan.DateOfPlan.Date);
        cmd.Parameters.AddWithValue("@IntensityTabIndex", (int)plan.Intensity);
        cmd.Parameters.AddWithValue("@Description", plan.Description ?? string.Empty);

        object result = cmd.ExecuteScalar();
        return result != null ? Convert.ToInt32(result) : -1; // return -1 if insert failed
    }

    public static void InsertActivity(ActivityModel activity)
    {
        using var conn = new SQLiteConnection(connectionString);
        conn.Open();

        string query = @"
            INSERT INTO Activities (PlanId, ActivityName, StartTime, EndTime, IntensityLevel)
            VALUES (@PlanId, @ActivityName, @StartTime, @EndTime, @IntensityLevel);";

        using var cmd = new SQLiteCommand(query, conn);
        cmd.Parameters.AddWithValue("@PlanId", activity.PlanId);
        cmd.Parameters.AddWithValue("@ActivityName", activity.ActivityName);
        cmd.Parameters.AddWithValue("@StartTime", activity.StartTime);
        cmd.Parameters.AddWithValue("@EndTime", activity.EndTime);
        cmd.Parameters.AddWithValue("@IntensityLevel", (int)activity.Intensity);

        cmd.ExecuteNonQuery();
    }

    public static void UpdatePlan(PlanModel plan)
    {
        using var conn = new SQLiteConnection(connectionString);
        conn.Open();

        string query = @"
            UPDATE Plans
            SET PlanName = @PlanName, DateOfPlan = @DateOfPlan, IntensityTabIndex = @IntensityTabIndex, Description = @Description
            WHERE Id = @Id;";

        using var cmd = new SQLiteCommand(query, conn);
        cmd.Parameters.AddWithValue("@Id", plan.Id);
        cmd.Parameters.AddWithValue("@PlanName", plan.PlanName ?? string.Empty);
        cmd.Parameters.AddWithValue("@DateOfPlan", plan.DateOfPlan.Date);
        cmd.Parameters.AddWithValue("@IntensityTabIndex", (int)plan.Intensity);
        cmd.Parameters.AddWithValue("@Description", plan.Description ?? string.Empty);

        cmd.ExecuteNonQuery();
    }

    public static void DeletePlan(int planId)
    {
        using var conn = new SQLiteConnection(connectionString);
        conn.Open();

        string query = "DELETE FROM Plans WHERE Id = @Id;";
        using var cmd = new SQLiteCommand(query, conn);
        cmd.Parameters.AddWithValue("@Id", planId);

        cmd.ExecuteNonQuery();
    }

    public static void DeleteActivity(int activityId)
    {
        using var conn = new SQLiteConnection(connectionString);
        conn.Open();

        string query = "DELETE FROM Activities WHERE Id = @Id;";
        using var cmd = new SQLiteCommand(query, conn);
        cmd.Parameters.AddWithValue("@Id", activityId);

        cmd.ExecuteNonQuery();
    }

    public static UserModel GetUserByUsername(string username)
    {
        using var conn = new SQLiteConnection(connectionString);
        conn.Open();

        string query = "SELECT * FROM Users WHERE Username = @Username";
        using var cmd = new SQLiteCommand(query, conn);
        cmd.Parameters.AddWithValue("@Username", username);

        using var reader = cmd.ExecuteReader();
        if (reader.Read())
        {
            return new UserModel
            {
                Id = Convert.ToInt32(reader["Id"]),
                Username = reader["Username"].ToString(),
                PasswordHash = reader["PasswordHash"].ToString(),
                FullName = reader["FullName"].ToString(),
                DateOfBirth = DateTime.Parse(reader["DateOfBirth"].ToString()),
                Area = reader["Area"].ToString()
            };
        }

        return null; // User not found
    }

    public static List<UserModel> GetAllUsers()
    {
        var users = new List<UserModel>();
        using var conn = new SQLiteConnection(connectionString);
        conn.Open();

        string query = "SELECT * FROM Users";
        using var cmd = new SQLiteCommand(query, conn);

        using var reader = cmd.ExecuteReader();
        while (reader.Read())
        {
            users.Add(new UserModel
            {
                Id = Convert.ToInt32(reader["Id"]),
                Username = reader["Username"].ToString(),
                PasswordHash = reader["PasswordHash"].ToString(),
                FullName = reader["FullName"].ToString(),
                DateOfBirth = DateTime.Parse(reader["DateOfBirth"].ToString()),
                Area = reader["Area"].ToString()
            });
        }

        return users;
    }
}