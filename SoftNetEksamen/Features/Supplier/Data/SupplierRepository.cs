using System;
using System.Collections.Generic;
using System.Data.SQLite;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using RepoDb;
using SoftNetEksamen.Core.Interfaces;

namespace SoftNetEksamen.Features.Supplier.Data
{
  public class SupplierRepository : IRepository<Models.Supplier>
  {
    private static readonly string ConnectionString =
      @$"Data Source={Directory.GetCurrentDirectory()}\mydb.db;Version=3;foreign keys=True;";

    private const string Sql = @"
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

    public async Task<Models.Supplier?> CreateAsync(Models.Supplier model)
    {
      model.Id = Guid.NewGuid();
      
      await using var connection = new SQLiteConnection(ConnectionString);
      var id = await connection.InsertAsync(model);
      
      return id is null ? null : model;
    }

    public async Task<IEnumerable<Models.Supplier>> ListAsync()
    {
      await using var connection = new SQLiteConnection(ConnectionString);
      return await connection.QueryAllAsync<Models.Supplier>();
    }

    public async Task<Models.Supplier?> ReadAsync(Guid id)
    {
      await using var connection = new SQLiteConnection(ConnectionString);
      var data = await connection.QueryAsync<Models.Supplier>(id);
      return data.FirstOrDefault();
    }

    public async Task<bool> UpdateAsync(Models.Supplier model)
    {
      await using var connection = new SQLiteConnection(ConnectionString);
      var rows = await connection.UpdateAsync(model);
      return rows > 0;
    }

    public async Task<Models.Supplier?> DeleteAsync(Guid id)
    {
      var data = await ReadAsync(id);
      if (data is null)
      {
        return null;
      }
      
      await using var connection = new SQLiteConnection(ConnectionString);
      var rows = await connection.DeleteAsync<Models.Supplier>(id);
      return rows > 0 ? data : null;
    }
  }
}