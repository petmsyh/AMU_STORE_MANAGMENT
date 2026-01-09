using System.Data.SqlClient;

namespace AMU.store.Mngt.Data
{
    public static class AuditRepository
    {
        public static void Log(string action, int? performedBy, string details)
        {
            using(var con = DbConnection.GetConnection())
            using(var cmd = con.CreateCommand())
            {
                con.Open();
                cmd.CommandText = "INSERT INTO AuditLogs(Action, PerformedBy, Details) VALUES(@a,@p,@d)";
                cmd.Parameters.AddWithValue("@a", action ?? "");
                cmd.Parameters.AddWithValue("@p", performedBy.HasValue? (object)performedBy.Value : (object)System.DBNull.Value);
                cmd.Parameters.AddWithValue("@d", details ?? "");
                cmd.ExecuteNonQuery();
            }
        }
    }
}
