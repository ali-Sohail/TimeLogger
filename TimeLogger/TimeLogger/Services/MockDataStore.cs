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
        private readonly List<Item> items;

        private readonly Realm realm;

        public MockDataStore()
        {
            realm = Realm.GetInstance();
            items = new List<Item>()
            {
                new Item { Id = Guid.NewGuid().ToString(), TitleText = "First item", Description="This is an item description." },
                new Item { Id = Guid.NewGuid().ToString(), TitleText = "Second item", Description="This is an item description." },
                new Item { Id = Guid.NewGuid().ToString(), TitleText = "Third item", Description="This is an item description." },
                new Item { Id = Guid.NewGuid().ToString(), TitleText = "Fourth item", Description="This is an item description." },
                new Item { Id = Guid.NewGuid().ToString(), TitleText = "Fifth item", Description="This is an item description." },
                new Item { Id = Guid.NewGuid().ToString(), TitleText = "Sixth item", Description="This is an item description." }
            };
        }

        public async Task<bool> AddItemAsync(Item item)
        {
            realm.Write(() => realm.Add(item));
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
            return await Task.FromResult(items);
        }
    }
}