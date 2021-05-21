using System;
using System.Collections.Generic;
using System.Data.SQLite;
using System.Linq;
using System.Threading.Tasks;
using RepoDb;

namespace SoftNetEksamen.Core.Interfaces
{
  public interface IRepository<T> where T : class, IModel
  {
    public string ConnectionString { get; }
    public string Sql { get; }
    
    public async Task<T?> CreateAsync(T model)
    {
      model.Id = Guid.NewGuid();
      
      await using var connection = new SQLiteConnection(ConnectionString);
      var id = await connection.InsertAsync(model);
      
      return id is null ? null : model;
    }
    
    public async Task<IEnumerable<T>> ListAsync()
    {
      await using var connection = new SQLiteConnection(ConnectionString);
      return await connection.QueryAllAsync<T>();
    }
    
    public async Task<T?> ReadAsync(Guid id)
    {
      await using var connection = new SQLiteConnection(ConnectionString);
      var data = await connection.QueryAsync<T>(id);
      return data.FirstOrDefault();
    }
    
    public async Task<bool> UpdateAsync(T model)
    {
      await using var connection = new SQLiteConnection(ConnectionString);
      var rows = await connection.UpdateAsync(model);
      return rows > 0;
    }
    
    public async Task<T?> DeleteAsync(Guid id)
    {
      var data = await ReadAsync(id);
      if (data is null)
      {
        return null;
      }
      
      await using var connection = new SQLiteConnection(ConnectionString);
      var rows = await connection.DeleteAsync<T>(id);
      return rows > 0 ? data : null;
    }
  }
}