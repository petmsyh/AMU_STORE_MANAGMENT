using System.Data.SqlClient;

namespace AMU.store.Mngt.Data
{
    public static class Model22Repository
    {
        public static int CreateIssue(int? requestId, int issuedByUserId, string issuedToName, System.Collections.Generic.List<(string name,int qty)> items, string remarks = null)
        {
            using(var con = DbConnection.GetConnection())
            {
                con.Open();
                // enforce role: only StoreMan can create issues
                using (var check = con.CreateCommand())
                {
                    check.CommandText = "SELECT Role FROM Users WHERE UserId = @id";
                    check.Parameters.AddWithValue("@id", issuedByUserId);
                    var roleObj = check.ExecuteScalar();
                    var role = roleObj == null ? string.Empty : roleObj.ToString();
                    if (!string.Equals(role, "StoreMan", System.StringComparison.OrdinalIgnoreCase))
                        throw new System.UnauthorizedAccessException("Only users with role 'StoreMan' can issue property.");
                }
                using(var tran = con.BeginTransaction())
                {
                    try
                    {
                        using(var cmd = con.CreateCommand())
                        {
                            cmd.Transaction = tran;
                            cmd.CommandText = "INSERT INTO Model22Issues(RequestId, IssuedBy, IssuedTo, Remarks) VALUES(@r,@b,@t,@m); SELECT SCOPE_IDENTITY();";
                            cmd.Parameters.AddWithValue("@r", requestId.HasValue ? (object)requestId.Value : (object)System.DBNull.Value);
                            cmd.Parameters.AddWithValue("@b", issuedByUserId);
                            cmd.Parameters.AddWithValue("@t", issuedToName ?? "");
                            cmd.Parameters.AddWithValue("@m", remarks ?? "");
                            var id = cmd.ExecuteScalar();
                            var issueId = System.Convert.ToInt32(id);

                            foreach(var it in items)
                            {
                                using(var cmd2 = con.CreateCommand())
                                {
                                    cmd2.Transaction = tran;
                                    cmd2.CommandText = "INSERT INTO Model22Items(IssueId, PropertyName, Quantity) VALUES(@i,@n,@q)";
                                    cmd2.Parameters.AddWithValue("@i", issueId);
                                    cmd2.Parameters.AddWithValue("@n", it.name);
                                    cmd2.Parameters.AddWithValue("@q", it.qty);
                                    cmd2.ExecuteNonQuery();
                                }

                                // check inventory
                                using(var cmd3 = con.CreateCommand())
                                {
                                    cmd3.Transaction = tran;
                                    cmd3.CommandText = "SELECT Quantity FROM Inventory WHERE PropertyName=@n";
                                    cmd3.Parameters.AddWithValue("@n", it.name);
                                    var cur = cmd3.ExecuteScalar();
                                    var curQty = cur == null ? 0 : System.Convert.ToInt32(cur);
                                    if (curQty < it.qty)
                                    {
                                        throw new System.InvalidOperationException($"Insufficient stock for {it.name}");
                                    }

                                    using(var cmd4 = con.CreateCommand())
                                    {
                                        cmd4.Transaction = tran;
                                        cmd4.CommandText = "UPDATE Inventory SET Quantity = Quantity - @q WHERE PropertyName=@n";
                                        cmd4.Parameters.AddWithValue("@q", it.qty);
                                        cmd4.Parameters.AddWithValue("@n", it.name);
                                        cmd4.ExecuteNonQuery();
                                    }
                                }
                            }

                            tran.Commit();
                            return issueId;
                        }
                    }
                    catch
                    {
                        tran.Rollback();
                        throw;
                    }
                }
            }
        }
    }
}
