using System.Configuration;
using System.Data.SqlClient;

namespace NeedyNest
{
    internal static class DbHelper
    {
        public static string ConnectionString =>
            ConfigurationManager.ConnectionStrings["NeedyNest"].ConnectionString;

        public static SqlConnection GetConnection() => new SqlConnection(ConnectionString);
    }
}
