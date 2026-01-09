using System;
using System.Data.SqlClient;

namespace AMU.store.Mngt.Data
{
    public static class DbInitializer
    {
        public static void EnsureSchema()
        {
            using (var con = DbConnection.GetConnection())
            using (var cmd = con.CreateCommand())
            {
                con.Open();

                // Users table exists already in your workspace SQL, but create if not
                cmd.CommandText = @"
IF OBJECT_ID('dbo.Users') IS NULL
BEGIN
    CREATE TABLE dbo.Users (
        UserId INT IDENTITY PRIMARY KEY,
        FullName NVARCHAR(100),
        Username NVARCHAR(50) UNIQUE,
        PasswordHash NVARCHAR(255),
        Role NVARCHAR(50),
        IsActive BIT
    );
END

IF OBJECT_ID('dbo.Properties') IS NULL
BEGIN
    CREATE TABLE dbo.Properties (
        PropertyId INT IDENTITY PRIMARY KEY,
        Name NVARCHAR(200) NOT NULL
    );
END

IF OBJECT_ID('dbo.Model20Requests') IS NULL
BEGIN
    CREATE TABLE dbo.Model20Requests (
        RequestId INT IDENTITY PRIMARY KEY,
        Department NVARCHAR(200),
        Purpose NVARCHAR(1000),
        CreatedBy INT,
        CreatedAt DATETIME DEFAULT(GETDATE()),
        Status NVARCHAR(50) DEFAULT('Pending')
    );
END
ELSE
BEGIN
    -- ensure columns for approval exist
    IF COL_LENGTH('dbo.Model20Requests','ApprovedBy') IS NULL
        ALTER TABLE dbo.Model20Requests ADD ApprovedBy INT NULL;
    IF COL_LENGTH('dbo.Model20Requests','ApprovedAt') IS NULL
        ALTER TABLE dbo.Model20Requests ADD ApprovedAt DATETIME NULL;
END

IF OBJECT_ID('dbo.Model20Items') IS NULL
BEGIN
    CREATE TABLE dbo.Model20Items (
        ItemId INT IDENTITY PRIMARY KEY,
        RequestId INT REFERENCES dbo.Model20Requests(RequestId),
        PropertyId INT,
        PropertyName NVARCHAR(200),
        Quantity INT
    );
END

IF OBJECT_ID('dbo.Purchases') IS NULL
BEGIN
    CREATE TABLE dbo.Purchases (
        PurchaseId INT IDENTITY PRIMARY KEY,
        Reference NVARCHAR(100),
        CreatedAt DATETIME DEFAULT(GETDATE())
    );
END

IF OBJECT_ID('dbo.PurchaseItems') IS NULL
BEGIN
    CREATE TABLE dbo.PurchaseItems (
        PurchaseItemId INT IDENTITY PRIMARY KEY,
        PurchaseId INT REFERENCES dbo.Purchases(PurchaseId),
        PropertyName NVARCHAR(200),
        Quantity INT
    );
END

IF OBJECT_ID('dbo.Inventory') IS NULL
BEGIN
    CREATE TABLE dbo.Inventory (
        InventoryId INT IDENTITY PRIMARY KEY,
        PropertyName NVARCHAR(200),
        Quantity INT
    );
END

IF OBJECT_ID('dbo.Model22Issues') IS NULL
BEGIN
    CREATE TABLE dbo.Model22Issues (
        IssueId INT IDENTITY PRIMARY KEY,
        RequestId INT NULL,
        IssuedBy INT,
        IssuedTo NVARCHAR(100),
        IssuedAt DATETIME DEFAULT(GETDATE()),
        Remarks NVARCHAR(1000)
    );
END

IF OBJECT_ID('dbo.Model22Items') IS NULL
BEGIN
    CREATE TABLE dbo.Model22Items (
        IssueItemId INT IDENTITY PRIMARY KEY,
        IssueId INT REFERENCES dbo.Model22Issues(IssueId),
        PropertyName NVARCHAR(200),
        Quantity INT
    );
END

IF OBJECT_ID('dbo.AuditLogs') IS NULL
BEGIN
    CREATE TABLE dbo.AuditLogs (
        AuditId INT IDENTITY PRIMARY KEY,
        Action NVARCHAR(200),
        PerformedBy INT NULL,
        Details NVARCHAR(2000) NULL,
        PerformedAt DATETIME DEFAULT(GETDATE())
    );
END
";
                cmd.ExecuteNonQuery();
            }
        }
    }
}
