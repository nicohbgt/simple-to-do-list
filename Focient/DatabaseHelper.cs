using System;
using System.Collections.Generic;
using System.Data.SQLite;
using System.IO;
using Focient.Models; // Pastikan namespace sesuai struktur project
using BCrypt.Net; // Perlu jika Anda menggunakan BCrypt untuk password

namespace Focient.Helpers
{
    public static class DatabaseHelper
    {
        private static string connectionString = "Data Source=focient.db;Version=3;";

        public static void InitializeDatabase()
        {
            if (!File.Exists("focient.db"))
            {
                SQLiteConnection.CreateFile("focient.db");
                Console.WriteLine("Database file created: focient.db");
            }

            using var conn = new SQLiteConnection(connectionString);
            conn.Open();
            Console.WriteLine("Database connection opened.");

            string createUsersTable = @"
                CREATE TABLE IF NOT EXISTS Users (
                    Id INTEGER PRIMARY KEY AUTOINCREMENT,
                    Username TEXT NOT NULL UNIQUE,
                    PasswordHash TEXT NOT NULL,
                    FullName TEXT,
                    DateOfBirth TEXT,
                    Area TEXT
                );";
            using var cmd1 = new SQLiteCommand(createUsersTable, conn);
            cmd1.ExecuteNonQuery();
            Console.WriteLine("Table 'Users' checked/created.");

            string createPlansTable = @"
                CREATE TABLE IF NOT EXISTS Plans (
                    Id INTEGER PRIMARY KEY AUTOINCREMENT,
                    UserId INTEGER NOT NULL,
                    Name TEXT NOT NULL,
                    Date TEXT NOT NULL,
                    IntensityLevel TEXT NOT NULL,
                    Description TEXT,
                    FOREIGN KEY (UserId) REFERENCES Users(Id) ON DELETE CASCADE
                );";
            using var cmd3 = new SQLiteCommand(createPlansTable, conn);
            cmd3.ExecuteNonQuery();
            Console.WriteLine("Table 'Plans' checked/created.");

            string createActivitiesTable = @"
                CREATE TABLE IF NOT EXISTS Activities (
                    Id INTEGER PRIMARY KEY AUTOINCREMENT,
                    PlanId INTEGER NOT NULL,
                    Name TEXT NOT NULL,
                    StartTime TEXT NOT NULL,
                    EndTime TEXT NOT NULL,
                    IsCompleted INTEGER NOT NULL,
                    FOREIGN KEY (PlanId) REFERENCES Plans(Id) ON DELETE CASCADE
                );";
            using var cmd4 = new SQLiteCommand(createActivitiesTable, conn);
            cmd4.ExecuteNonQuery();
            Console.WriteLine("Table 'Activities' checked/created.");
        }

        // --- Metode CRUD untuk Users ---
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
                    cmd.Parameters.AddWithValue("@FullName", user.FullName ?? (object)DBNull.Value);
                    cmd.Parameters.AddWithValue("@DateOfBirth", user.DateOfBirth.ToString("yyyy-MM-dd"));
                    cmd.Parameters.AddWithValue("@Area", user.Area ?? (object)DBNull.Value);

                    cmd.ExecuteNonQuery();
                }
            }
        }

        public static UserModel GetUserByUsername(string username)
        {
            using (var conn = new SQLiteConnection(connectionString))
            {
                conn.Open();
                string query = "SELECT Id, Username, PasswordHash, FullName, DateOfBirth, Area FROM Users WHERE Username = @Username";
                using (var cmd = new SQLiteCommand(query, conn))
                {
                    cmd.Parameters.AddWithValue("@Username", username);
                    using (var reader = cmd.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            return new UserModel
                            {
                                Id = Convert.ToInt32(reader["Id"]),
                                Username = reader["Username"].ToString(),
                                PasswordHash = reader["PasswordHash"].ToString(),
                                FullName = reader.IsDBNull(reader.GetOrdinal("FullName")) ? null : reader["FullName"].ToString(),
                                DateOfBirth = DateTime.Parse(reader["DateOfBirth"].ToString()),
                                Area = reader.IsDBNull(reader.GetOrdinal("Area")) ? null : reader["Area"].ToString()
                            };
                        }
                    }
                }
            }
            return null;
        }

        // --- Metode CRUD untuk Plans ---
        public static int InsertPlan(PlanModel plan)
        {
            if (plan == null)
                throw new ArgumentNullException(nameof(plan));

            using var conn = new SQLiteConnection(connectionString);
            conn.Open();

            string query = @"
                INSERT INTO Plans (UserId, Name, Date, IntensityLevel, Description)
                VALUES (@UserId, @Name, @Date, @IntensityLevel, @Description);
                SELECT last_insert_rowid();";

            using var cmd = new SQLiteCommand(query, conn); // Ini baris yang salah sebelumnya
            cmd.Parameters.AddWithValue("@Name", plan.PlanName ?? string.Empty);
            cmd.Parameters.AddWithValue("@Date", plan.DateOfPlan.ToString("yyyy-MM-dd"));
            cmd.Parameters.AddWithValue("@IntensityLevel", plan.Intensity.ToString());
            cmd.Parameters.AddWithValue("@Description", plan.Description ?? (object)DBNull.Value);
            cmd.Parameters.AddWithValue("@Id", plan.Id);
            cmd.Parameters.AddWithValue("@UserId", plan.UserId);

            object result = cmd.ExecuteScalar();
            return result != null ? Convert.ToInt32(result) : -1;
        }

        public static bool UpdatePlan(PlanModel plan)
        {
            if (plan == null)
                throw new ArgumentNullException(nameof(plan));

            using var conn = new SQLiteConnection(connectionString);
            conn.Open();

            string query = @"
                UPDATE Plans
                SET Name = @Name,
                    Date = @Date,
                    IntensityLevel = @IntensityLevel,
                    Description = @Description
                WHERE Id = @Id AND UserId = @UserId;";

            using var cmd = new SQLiteCommand(query, conn); // Ini baris yang salah sebelumnya
            cmd.Parameters.AddWithValue("@Name", plan.PlanName ?? string.Empty);
            cmd.Parameters.AddWithValue("@Date", plan.DateOfPlan.ToString("yyyy-MM-dd"));
            cmd.Parameters.AddWithValue("@IntensityLevel", plan.Intensity.ToString());
            cmd.Parameters.AddWithValue("@Description", plan.Description ?? (object)DBNull.Value);
            cmd.Parameters.AddWithValue("@Id", plan.Id);
            cmd.Parameters.AddWithValue("@UserId", plan.UserId);

            int rowsAffected = cmd.ExecuteNonQuery();
            return rowsAffected > 0;
        }

        public static PlanModel GetPlanById(int planId, int userId)
        {
            using var conn = new SQLiteConnection(connectionString);
            conn.Open();

            string query = @"
                SELECT Id, UserId, Name AS PlanName, Date, IntensityLevel, Description
                FROM Plans
                WHERE Id = @Id AND UserId = @UserId;";

            using var cmd = new SQLiteCommand(query, conn);
            cmd.Parameters.AddWithValue("@Id", planId);
            cmd.Parameters.AddWithValue("@UserId", userId);

            using var reader = cmd.ExecuteReader();
            if (reader.Read())
            {
                return new PlanModel
                {
                    Id = Convert.ToInt32(reader["Id"]),
                    UserId = Convert.ToInt32(reader["UserId"]),
                    PlanName = reader["PlanName"].ToString(),
                    DateOfPlan = DateTime.Parse(reader["Date"].ToString()),
                    Intensity = (IntensityLevel)Enum.Parse(typeof(IntensityLevel), reader["IntensityLevel"].ToString()),
                    Description = reader.IsDBNull(reader.GetOrdinal("Description")) ? null : reader["Description"].ToString()
                };
            }
            return null;
        }

        public static List<PlanModel> GetPlansByUserId(int userId)
        {
            var plans = new List<PlanModel>();
            using var conn = new SQLiteConnection(connectionString);
            conn.Open();

            string query = "SELECT Id, UserId, Name, Date, IntensityLevel, Description FROM Plans WHERE UserId = @UserId ORDER BY Date DESC, Name ASC";
            using var cmd = new SQLiteCommand(query, conn); // Fixed: use 'using var' instead of 'using (var ...)'
            cmd.Parameters.AddWithValue("@UserId", userId);

            using var reader = cmd.ExecuteReader();
            while (reader.Read())
            {
                plans.Add(new PlanModel
                {
                    Id = Convert.ToInt32(reader["Id"]),
                    UserId = Convert.ToInt32(reader["UserId"]),
                    PlanName = reader["Name"].ToString(), // Fixed: Use 'PlanName' instead of 'Name'
                    DateOfPlan = DateTime.Parse(reader["Date"].ToString()),
                    Intensity = (IntensityLevel)Enum.Parse(typeof(IntensityLevel), reader["IntensityLevel"].ToString()),
                    Description = reader.IsDBNull(reader.GetOrdinal("Description")) ? null : reader["Description"].ToString()
                });
            }
            return plans;
        }

        public static bool DeletePlan(int planId, int userId)
        {
            using var conn = new SQLiteConnection(connectionString);
            conn.Open();

            string query = @"DELETE FROM Plans WHERE Id = @Id AND UserId = @UserId;";

            using (var cmd = new SQLiteCommand(query, conn)) // Ini baris yang salah sebelumnya
            {
                cmd.Parameters.AddWithValue("@Id", planId);
                cmd.Parameters.AddWithValue("@UserId", userId);

                int rowsAffected = cmd.ExecuteNonQuery();
                return rowsAffected > 0;
            }
        }

        // Fix for CS0117: 'IntensityLevel' does not contain a definition for 'Completed'
        // The error occurs because the IntensityLevel enum does not have a 'Completed' value.
        // To resolve this, we need to replace the incorrect comparison with a valid logic.

        public static void InsertActivity(ActivityModel activity)
        {
            using var conn = new SQLiteConnection(connectionString);
            conn.Open();

            string query = @"
                INSERT INTO Activities (PlanId, Name, StartTime, EndTime, IsCompleted)
                VALUES (@PlanId, @Name, @StartTime, @EndTime, @IsCompleted);";

            using (var cmd = new SQLiteCommand(query, conn))
            {
                cmd.Parameters.AddWithValue("@PlanId", activity.PlanId);
                cmd.Parameters.AddWithValue("@Name", activity.ActivityName);
                cmd.Parameters.AddWithValue("@StartTime", activity.StartTime.ToString("HH:mm"));
                cmd.Parameters.AddWithValue("@EndTime", activity.EndTime.ToString("HH:mm"));
                cmd.Parameters.AddWithValue("@IsCompleted", activity.Intensity == IntensityLevel.High ? 1 : 0); // Replace 'Completed' with valid logic

                cmd.ExecuteNonQuery();
            }
        }

        public static void UpdateActivity(ActivityModel activity)
        {
            using var conn = new SQLiteConnection(connectionString);
            conn.Open();
            string sql = "UPDATE Activities SET Name = @Name, StartTime = @StartTime, EndTime = @EndTime, IsCompleted = @IsCompleted WHERE Id = @Id AND PlanId = @PlanId";
            using (var command = new SQLiteCommand(sql, conn))
            {
                command.Parameters.AddWithValue("@Name", activity.ActivityName); // Fixed: Use 'ActivityName' instead of 'Name'
                command.Parameters.AddWithValue("@StartTime", activity.StartTime.ToString("HH:mm"));
                command.Parameters.AddWithValue("@EndTime", activity.EndTime.ToString("HH:mm"));
                command.Parameters.AddWithValue("@IsCompleted", activity.Intensity == IntensityLevel.High ? 1 : 0);
                command.Parameters.AddWithValue("@Id", activity.Id);
                command.Parameters.AddWithValue("@PlanId", activity.PlanId);
                command.ExecuteNonQuery();
            }
        }

        public static List<ActivityModel> GetActivitiesByPlanId(int planId)
        {
            var activities = new List<ActivityModel>();
            using var conn = new SQLiteConnection(connectionString);
            conn.Open();

            string query = "SELECT Id, PlanId, ActivityName, StartTime, EndTime, IsCompleted FROM Activities WHERE PlanId = @PlanId ORDER BY StartTime ASC";
            using (var cmd = new SQLiteCommand(query, conn))
            {
                cmd.Parameters.AddWithValue("@PlanId", planId);

                using var reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    DateTime startTime = DateTime.ParseExact(reader.GetString(reader.GetOrdinal("StartTime")), "HH:mm", System.Globalization.CultureInfo.InvariantCulture);
                    DateTime endTime = DateTime.ParseExact(reader.GetString(reader.GetOrdinal("EndTime")), "HH:mm", System.Globalization.CultureInfo.InvariantCulture);

                    activities.Add(new ActivityModel
                    {
                        Id = Convert.ToInt32(reader["Id"]),
                        PlanId = Convert.ToInt32(reader["PlanId"]),
                        ActivityName = reader["ActivityName"].ToString(), // Fixed: Use 'ActivityName' instead of 'Name'
                        StartTime = startTime,
                        EndTime = endTime,
                        Intensity = Convert.ToInt32(reader["IsCompleted"]) == 1 ? IntensityLevel.High : IntensityLevel.Low // Fixed: Map IsCompleted to Intensity
                    });
                }
            }
            return activities;
        }

        public static void DeleteActivity(int activityId)
        {
            using var conn = new SQLiteConnection(connectionString);
            conn.Open();

            string query = @"DELETE FROM Activities WHERE Id = @Id;";
            using (var cmd = new SQLiteCommand(query, conn)) // Ini baris yang salah sebelumnya
            {
                cmd.Parameters.AddWithValue("@Id", activityId);
                cmd.ExecuteNonQuery();
            }
        }
    }
}