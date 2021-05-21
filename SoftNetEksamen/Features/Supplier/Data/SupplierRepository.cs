using System.Data.SQLite;
using System.IO;
using RepoDb;
using SoftNetEksamen.Core.Interfaces;

namespace SoftNetEksamen.Features.Supplier.Data
{
  public class SupplierRepository : IRepository<Models.Supplier>
  {
    public string ConnectionString =>
      @$"Data Source={Directory.GetCurrentDirectory()}\mydb.db;Version=3;foreign keys=True;";

    public string Sql => @"
    CREATE TABLE IF NOT EXISTS [Supplier]
    (
        Id TEXT PRIMARY KEY,
        Name TEXT,
        Address TEXT,
        PostCode INTEGER,
        Contact TEXT,
        Email TEXT,
        PhoneNumber TEXT
    );
    ";

    public SupplierRepository()
    {
      if (!SqLiteBootstrap.IsInitialized)
      {
        SqLiteBootstrap.Initialize();
      }

      using var connection = new SQLiteConnection(ConnectionString);
      connection.ExecuteNonQuery(Sql);
    }
  }
}