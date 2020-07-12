using Computer_Wifi_Remote_Xamarin.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Xamarin.Views.Actions;

namespace Computer_Wifi_Remote_Xamarin.Services
{
    public class ActionItemStore : IDataStore<Item>
    {
        List<Item> items;

        public ActionItemStore()
        {
            items = new List<Item>
            {
                new Item { Id = Guid.NewGuid().ToString(), Text = "Hello World", Description="Sends Hello World to the server.", Page = new HelloWorldPage() },
                new Item { Id = Guid.NewGuid().ToString(), Text = "Media Control", Description="Various controls for media.", Page=new MediaControlPage() },
                new Item { Id = Guid.NewGuid().ToString(), Text = "System Control", Description="Various controls for the system.", Page=new SystemControlPage() },
                new Item { Id = Guid.NewGuid().ToString(), Text = "Remote Control", Description="Remote control your device.", Page = new RemoteControlPage()},
            };
        }

        public async Task<bool> AddItemAsync(Item item)
        {
            items.Add(item);

            return await Task.FromResult(true);
        }

        public async Task<bool> UpdateItemAsync(Item item)
        {
            var oldItem = items.Where((Item arg) => arg.Id == item.Id).FirstOrDefault();
            items.Remove(oldItem);
            items.Add(item);

            return await Task.FromResult(true);
        }

        public async Task<bool> DeleteItemAsync(string id)
        {
            var oldItem = items.Where((Item arg) => arg.Id == id).FirstOrDefault();
            items.Remove(oldItem);

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