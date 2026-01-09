using System.Data.SqlClient;

namespace AMU.store.Mngt.Data
{
    public static class Model20Repository
    {
        public static int CreateRequest(string department, string purpose, int createdBy, System.Collections.Generic.List<(string name,int qty)> items)
        {
            using (var con = DbConnection.GetConnection())
            using (var cmd = con.CreateCommand())
            {
                con.Open();
                cmd.CommandText = @"INSERT INTO Model20Requests(Department, Purpose, CreatedBy) VALUES(@dept,@purpose,@created); SELECT SCOPE_IDENTITY();";
                cmd.Parameters.AddWithValue("@dept", department);
                cmd.Parameters.AddWithValue("@purpose", purpose);
                cmd.Parameters.AddWithValue("@created", createdBy);
                var id = cmd.ExecuteScalar();
                var reqId = System.Convert.ToInt32(id);

                foreach(var it in items)
                {
                    using(var cmd2 = con.CreateCommand())
                    {
                        cmd2.CommandText = "INSERT INTO Model20Items(RequestId, PropertyName, Quantity) VALUES(@r,@n,@q)";
                        cmd2.Parameters.AddWithValue("@r", reqId);
                        cmd2.Parameters.AddWithValue("@n", it.name);
                        cmd2.Parameters.AddWithValue("@q", it.qty);
                        cmd2.ExecuteNonQuery();
                    }
                }

                // audit
                AuditRepository.Log("CreateModel20", createdBy, "RequestId="+reqId);

                return reqId;
            }
        }
    }
}
