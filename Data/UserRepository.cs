using System.Data;

namespace NeedyNest.Data
{
    /// <summary>
    /// Repository for member (signup) operations. Centralizes the SQL so forms
    /// don't each hand-write the same queries. Extend this as more queries move
    /// out of the UI.
    /// </summary>
    internal static class UserRepository
    {
        public static int CountPending() =>
            Db.ScalarInt("SELECT COUNT(*) FROM signup WHERE status = 0");

        public static bool Exists(string username) =>
            Db.ScalarInt("SELECT COUNT(*) FROM signup WHERE username = @u", ("@u", username)) > 0;

        public static string GetRole(string username) =>
            Db.ScalarString("SELECT role FROM signup WHERE username = @u", ("@u", username));

        public static string GetPasswordHash(string username) =>
            Db.ScalarString("SELECT password FROM signup WHERE username = @u", ("@u", username));

        public static void SetStatus(string username, int status) =>
            Db.NonQuery("UPDATE signup SET status = @s WHERE username = @u", ("@s", status), ("@u", username));

        public static void SetPassword(string username, string passwordHash) =>
            Db.NonQuery("UPDATE signup SET password = @p WHERE username = @u", ("@p", passwordHash), ("@u", username));

        public static DataTable GetPending() =>
            Db.Table(
                "SELECT username, first_name AS [First Name], last_name AS [Last Name], " +
                "role AS [Role], uni_name AS [University], contact_number AS [Contact] " +
                "FROM signup WHERE status = 0");

        public static DataTable GetAll() =>
            Db.Table("SELECT first_name, last_name, role, username, contact_number, status, uni_name FROM signup");
    }
}
