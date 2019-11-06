using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TimeLogger.Models;
using Realms;

namespace TimeLogger.Services
{
    public sealed class MockDataStore : IDataStore<Item>
    {
        private readonly IEnumerable<Item> items;

        private readonly Realm realm;

        public MockDataStore()
        {
            realm = Realm.GetInstance();
            items = realm.All<Item>();
        }

        public async Task<bool> AddItemAsync(Item item)
        {
            realm.Write(() => realm.Add(item, true));
            return await Task.FromResult(true);
        }

        public async Task<bool> UpdateItemAsync(Item item)
        {
            var oldItem = items.Where((Item arg) => arg.Id == item.Id).FirstOrDefault();
            realm.Remove(oldItem);
            realm.Add(item);

            return await Task.FromResult(true);
        }

        public async Task<bool> DeleteItemAsync(string id)
        {
            var oldItem = items.Where((Item arg) => arg.Id == id).FirstOrDefault();
            realm.Remove(oldItem);

            return await Task.FromResult(true);
        }

        public async Task<Item> GetItemAsync(string id)
        {
            return await Task.FromResult(items.FirstOrDefault(s => s.Id == id));
        }

        public async Task<IEnumerable<Item>> GetItemsAsync(bool forceRefresh = false)
        {
            if (forceRefresh)
            {
                return realm.All<Item>();
            }
            return await Task.FromResult(items);
        }
    }
}