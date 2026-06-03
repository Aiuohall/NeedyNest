using System;
using System.Data;
using System.Data.SqlClient;

namespace NeedyNest.Data
{
    /// <summary>
    /// Tiny data-access helper that removes the repetitive open/command/parameter
    /// boilerplate from the forms. Parameters are passed as (name, value) tuples.
    ///
    ///     int n = Db.ScalarInt("SELECT COUNT(*) FROM signup WHERE status = 0");
    ///     Db.NonQuery("UPDATE signup SET status=@s WHERE username=@u", ("@s", 1), ("@u", name));
    /// </summary>
    internal static class Db
    {
        private static SqlConnection Open()
        {
            var con = DbHelper.GetConnection();
            con.Open();
            return con;
        }

        public static int NonQuery(string sql, params (string Name, object Value)[] ps)
        {
            using (var con = Open())
            using (var cmd = Cmd(con, sql, ps))
                return cmd.ExecuteNonQuery();
        }

        public static object Scalar(string sql, params (string Name, object Value)[] ps)
        {
            using (var con = Open())
            using (var cmd = Cmd(con, sql, ps))
                return cmd.ExecuteScalar();
        }

        public static int ScalarInt(string sql, params (string Name, object Value)[] ps)
        {
            object r = Scalar(sql, ps);
            return (r == null || r == DBNull.Value) ? 0 : Convert.ToInt32(r);
        }

        public static string ScalarString(string sql, params (string Name, object Value)[] ps)
        {
            object r = Scalar(sql, ps);
            return (r == null || r == DBNull.Value) ? null : r.ToString();
        }

        public static DataTable Table(string sql, params (string Name, object Value)[] ps)
        {
            using (var con = Open())
            using (var cmd = Cmd(con, sql, ps))
            {
                var dt = new DataTable();
                using (var adapter = new SqlDataAdapter(cmd))
                    adapter.Fill(dt);
                return dt;
            }
        }

        private static SqlCommand Cmd(SqlConnection con, string sql, (string Name, object Value)[] ps)
        {
            var cmd = new SqlCommand(sql, con);
            foreach (var p in ps)
                cmd.Parameters.AddWithValue(p.Name, p.Value ?? DBNull.Value);
            return cmd;
        }
    }
}
