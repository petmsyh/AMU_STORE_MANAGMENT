using System;
using System.Data.SqlClient;

namespace AMU.store.Mngt.Data
{
    public static class DbSeeder
    {
        public static void Seed()
        {
            using (var con = DbConnection.GetConnection())
            using (var cmd = con.CreateCommand())
            {
                con.Open();

                // seed user if not exists
                cmd.CommandText = @"IF NOT EXISTS(SELECT 1 FROM Users WHERE Username = @u)
BEGIN
    INSERT INTO Users (FullName, Username, PasswordHash, Role, IsActive)
    VALUES (@full, @u, @pass, @role, 1)
END";
                cmd.Parameters.AddWithValue("@u", "petermessay");
                cmd.Parameters.AddWithValue("@full", "Peter Messay");
                cmd.Parameters.AddWithValue("@pass", "password123");
                cmd.Parameters.AddWithValue("@role", "Admin");
                cmd.ExecuteNonQuery();

                // seed properties
                cmd.CommandText = @"IF NOT EXISTS(SELECT 1 FROM Properties WHERE Name='Laptop') INSERT INTO Properties(Name) VALUES('Laptop')";
                cmd.Parameters.Clear();
                cmd.ExecuteNonQuery();

                cmd.CommandText = @"IF NOT EXISTS(SELECT 1 FROM Properties WHERE Name='Chair') INSERT INTO Properties(Name) VALUES('Chair')";
                cmd.ExecuteNonQuery();

                // seed sample purchase (Purchase + PurchaseItems)
                cmd.CommandText = @"IF NOT EXISTS(SELECT 1 FROM Purchases WHERE Reference='PR-2025-001')
BEGIN
    INSERT INTO Purchases(Reference) VALUES('PR-2025-001');
    DECLARE @pid INT = SCOPE_IDENTITY();
    INSERT INTO PurchaseItems(PurchaseId, PropertyName, Quantity) VALUES(@pid, 'Laptop', 5);
    INSERT INTO PurchaseItems(PurchaseId, PropertyName, Quantity) VALUES(@pid, 'Chair', 20);
END";
                cmd.ExecuteNonQuery();
            }
        }
    }
}
