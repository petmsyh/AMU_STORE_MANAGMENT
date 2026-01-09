using System.Data.SqlClient;

namespace AMU.store.Mngt.Data
{
    public static class Model19Repository
    {
        public static void ReceivePurchase(string purchaseRef, System.Collections.Generic.List<(string name,int qty)> items)
        {
            using (var con = DbConnection.GetConnection())
            {
                con.Open();
                using (var tran = con.BeginTransaction())
                {
                    try
                    {
                        using (var cmd = con.CreateCommand())
                        {
                            cmd.Transaction = tran;
                            // ensure purchase exists
                            cmd.CommandText = @"IF NOT EXISTS(SELECT 1 FROM Purchases WHERE Reference=@ref)
BEGIN
    INSERT INTO Purchases(Reference) VALUES(@ref);
END
SELECT PurchaseId FROM Purchases WHERE Reference=@ref";
                            cmd.Parameters.AddWithValue("@ref", purchaseRef);
                            var pid = cmd.ExecuteScalar();
                            var purchaseId = System.Convert.ToInt32(pid);

                            // Insert purchase items and update inventory within transaction
                            foreach (var it in items)
                            {
                                using (var c2 = con.CreateCommand())
                                {
                                    c2.Transaction = tran;
                                    c2.CommandText = "INSERT INTO PurchaseItems(PurchaseId, PropertyName, Quantity) VALUES(@p,@n,@q)";
                                    c2.Parameters.AddWithValue("@p", purchaseId);
                                    c2.Parameters.AddWithValue("@n", it.name);
                                    c2.Parameters.AddWithValue("@q", it.qty);
                                    c2.ExecuteNonQuery();
                                }

                                using (var c3 = con.CreateCommand())
                                {
                                    c3.Transaction = tran;
                                    c3.CommandText = @"IF EXISTS(SELECT 1 FROM Inventory WHERE PropertyName=@n)
BEGIN
    UPDATE Inventory SET Quantity = Quantity + @q WHERE PropertyName=@n
END
ELSE
BEGIN
    INSERT INTO Inventory(PropertyName, Quantity) VALUES(@n, @q)
END";
                                    c3.Parameters.AddWithValue("@n", it.name);
                                    c3.Parameters.AddWithValue("@q", it.qty);
                                    c3.ExecuteNonQuery();
                                }
                            }
                        }

                        // commit
                        tran.Commit();
                    }
                    catch
                    {
                        try { tran.Rollback(); } catch { }
                        throw;
                    }
                }
            }
        }
    }
}
