using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Stratis.Services
{
    public interface IDataStore<T>
    {
        List<T> Items { get; }
        Task<bool> AddItemAsync(T item);
        Task<bool> UpdateItemAsync(T item);
        Task<bool> DeleteItemAsync(T item);
        Task<T> GetItemAsync(string id);
        Task<IEnumerable<T>> GetItemsAsync(bool forceRefresh = false);
    }
}
