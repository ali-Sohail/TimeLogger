﻿using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace TimeLogger.Services
{
    public interface IDataStore<T>
    {
        Task<bool> CreateInstance();
        Task<bool> BeginWrite();
        Task<bool> DisposeInstance();
        Task<bool> AddItemAsync(T item);
        Task<bool> UpdateItemAsync(T item);
        Task<bool> DeleteItemAsync(string id);
        Task<T> GetItemAsync(string id);
        Task<IEnumerable<T>> GetItemsAsync(bool forceRefresh = false);
    }
}
