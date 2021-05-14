using System;
using System.Collections.Generic;
using System.Data.SQLite;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using RepoDb;
using SoftNetEksamen.Core.Interfaces;

namespace SoftNetEksamen.Features.Category.Data
{
  public class CategoryRepository : IRepository<Models.Category>
  {
    private static readonly string ConnectionString =
      @$"Data Source={Directory.GetCurrentDirectory()}\mydb.db;Version=3;foreign keys=True;";

    private const string Sql = @"
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

    public async Task<Models.Category?> CreateAsync(Models.Category model)
    {
      model.Id = Guid.NewGuid();
      
      await using var connection = new SQLiteConnection(ConnectionString);
      var id = await connection.InsertAsync(model);
      
      return id is null ? null : model;
    }

    public async Task<IEnumerable<Models.Category>> ListAsync()
    {
      await using var connection = new SQLiteConnection(ConnectionString);
      return await connection.QueryAllAsync<Models.Category>();
    }

    public async Task<Models.Category?> ReadAsync(Guid id)
    {
      await using var connection = new SQLiteConnection(ConnectionString);
      var data = await connection.QueryAsync<Models.Category>(id);
      return data.FirstOrDefault();
    }

    public async Task<bool> UpdateAsync(Models.Category model)
    {
      await using var connection = new SQLiteConnection(ConnectionString);
      var rows = await connection.UpdateAsync(model);
      return rows > 0;
    }

    public async Task<Models.Category?> DeleteAsync(Guid id)
    {
      var data = await ReadAsync(id);
      if (data is null)
      {
        return null;
      }
      
      await using var connection = new SQLiteConnection(ConnectionString);
      var rows = await connection.DeleteAsync<Models.Category>(id);
      return rows > 0 ? data : null;
    }
  }
}