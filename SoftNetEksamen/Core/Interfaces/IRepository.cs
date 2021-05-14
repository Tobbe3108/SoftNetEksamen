using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace SoftNetEksamen.Core.Interfaces
{
  public interface IRepository<T>
  {
    Task<T?> CreateAsync(T model);

    Task<IEnumerable<T>> ListAsync();

    Task<T?> ReadAsync(Guid id);

    Task<bool> UpdateAsync(T model);

    Task<T?> DeleteAsync(Guid id);
  }
}