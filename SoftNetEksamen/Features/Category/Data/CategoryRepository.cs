using System.Data.SQLite;
using System.IO;
using RepoDb;
using SoftNetEksamen.Core.Interfaces;

namespace SoftNetEksamen.Features.Category.Data
{
  public class CategoryRepository : IRepository<Models.Category>
  {
    public string ConnectionString => @$"Data Source={Directory.GetCurrentDirectory()}\mydb.db;Version=3;foreign keys=True;";
    
    public string Sql => @"
    CREATE TABLE IF NOT EXISTS [Category]
    (
        Id TEXT PRIMARY KEY,
        Name TEXT,
        Description TEXT
    );
    ";

    public CategoryRepository()
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