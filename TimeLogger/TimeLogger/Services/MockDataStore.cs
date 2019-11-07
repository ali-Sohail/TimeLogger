using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TimeLogger.Models;
using Realms;
using System.Diagnostics;

namespace TimeLogger.Services
{
    public sealed class MockDataStore : IDataStore<Item>
    {
        private IEnumerable<Item> items;

        private readonly Realm realm;

        public MockDataStore()
        {
            realm = Realm.GetInstance();
            items = realm.All<Item>().OrderBy(x => x.TitleText);
        }

        public async Task<bool> AddItemAsync(Item item)
        {
            try
            {
                realm.Write(() => realm.Add(item, true));
                return await Task.FromResult(true);
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex.Message);
                return await Task.FromResult(false);
            }
        }

        public async Task<bool> UpdateItemAsync(Item item)
        {
            try
            {
                realm.Write(() =>
                {
                    var oldItem = realm.All<Item>().Where((Item arg) => arg.TitleText == item.TitleText).FirstOrDefault();
                    oldItem = item;
                    realm.Add(oldItem, true);
                });
                return await Task.FromResult(true);
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex.Message);
                return await Task.FromResult(false);
            }
        }

        public async Task<bool> DeleteItemAsync(string TitleText)
        {
            realm.Write(() =>
            {
                var oldItem = realm.All<Item>().Where((Item arg) => arg.TitleText == TitleText).FirstOrDefault();
                realm.Remove(oldItem);
            });
            return await Task.FromResult(true);
        }

        public async Task<Item> GetItemAsync(string TitleText)
        {
            return await Task.FromResult(realm.All<Item>().FirstOrDefault(s => s.TitleText == TitleText));
        }

        public async Task<IEnumerable<Item>> GetItemsAsync(bool forceRefresh = false)
        {
            if (forceRefresh)
            {
                items = realm.All<Item>();
                return items;
            }
            return await Task.FromResult(items);
        }
    }
}