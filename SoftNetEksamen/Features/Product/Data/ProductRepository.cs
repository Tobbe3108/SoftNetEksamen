using System;
using System.Collections.Generic;
using System.Data.SQLite;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Mapster;
using RepoDb;
using SoftNetEksamen.Core.Interfaces;

namespace SoftNetEksamen.Features.Product.Data
{
  public class ProductRepository : IRepository<Models.Product>
  {
    private static readonly string ConnectionString =
      @$"Data Source={Directory.GetCurrentDirectory()}\mydb.db;Version=3;foreign keys=True;";

    private const string Sql = @"
    CREATE TABLE IF NOT EXISTS [Product]
    (
        Id TEXT PRIMARY KEY,
        Name TEXT,
        Description TEXT,
        Unit TEXT,
        Amount INTEGER,
        Price NUMERIC, 
        Stock INTEGER,
        CategoryId TEXT,
        SupplierId TEXT,
        FOREIGN KEY(CategoryId) REFERENCES Category(Id)
        FOREIGN KEY(SupplierId) REFERENCES Supplier(Id)
    );
    ";

    public ProductRepository()
    {
      if (!SqLiteBootstrap.IsInitialized)
      {
        SqLiteBootstrap.Initialize();
      }

      using var connection = new SQLiteConnection(ConnectionString);
      connection.ExecuteNonQuery(Sql);
    }

    public async Task<Models.Product?> CreateAsync(Models.Product model)
    {
      model.Id = Guid.NewGuid();
      
      await using var connection = new SQLiteConnection(ConnectionString);
      var id = await connection.InsertAsync(model);
      
      return id is null ? null : model;
    }

    public async Task<IEnumerable<Models.Product>> ListAsync()
    {
      await using var connection = new SQLiteConnection(ConnectionString);
      var data = await connection.QueryAllAsync("[Product]");
      return data.Adapt<IEnumerable<Models.Product>>();
    }

    public async Task<Models.Product?> ReadAsync(Guid id)
    {
      await using var connection = new SQLiteConnection(ConnectionString);
      var data = await connection.QueryAsync("[Product]", id);
      return data.Adapt<IEnumerable<Models.Product>>().FirstOrDefault();
    }

    public async Task<bool> UpdateAsync(Models.Product model)
    {
      await using var connection = new SQLiteConnection(ConnectionString);
      var rows = await connection.UpdateAsync(model);
      return rows > 0;
    }

    public async Task<Models.Product?> DeleteAsync(Guid id)
    {
      var data = await ReadAsync(id);
      if (data is null)
      {
        return null;
      }
      
      await using var connection = new SQLiteConnection(ConnectionString);
      var rows = await connection.DeleteAsync<Models.Product>(id);
      return rows > 0 ? data : null;
    }
  }
}