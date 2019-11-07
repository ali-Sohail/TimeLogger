using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using TimeLogger.Models;
using Xamarin.Forms;

namespace TimeLogger.ViewModels
{
    public class HomePageViewModel : BaseViewModel
    {
        #region Properties & Fields & Commands

        private IEnumerable<Item> _items;

        public IEnumerable<Item> Items
        {
            get => _items;
            set { SetProperty(ref _items, value); OnPropertyChanged(); }
        }

        private Item _item;

        public Item Item
        {
            get => _item;
            set => SetProperty(ref _item, value);
        }

        public ICommand SubmitCommand { get; set; }
        public ICommand InTimeNowCommand { get; set; }
        public ICommand OutTimeNowCommand { get; set; }

        #endregion Properties & Fields & Commands

        public HomePageViewModel()

        {
            Title = "Home Page";
            InitData();
            SubmitCommand = new Command(Save);
            InTimeNowCommand = new Command(() => { Item.InTime = DateTime.Now.TimeOfDay.ToString(); Save(); });
            OutTimeNowCommand = new Command(() => { Item.OutTime = DateTime.Now.TimeOfDay.ToString(); Save(); });
        }

        #region Methods

        private async void InitData()
        {
            Items = await DataStore.GetItemsAsync();
            Item = Items?.Where<Item>(x => x.TitleText == DateTime.Now.ToShortDateString()).FirstOrDefault();
            if (Item == null)
            {
                Item = new Item
                {
                    TitleText = DateTime.Now.ToShortDateString(),
                    Description = DateTime.Now.ToLongDateString(),
                    InTime = DateTime.Now.TimeOfDay.ToString(),
                    OutTime = DateTime.Now.TimeOfDay.ToString(),
                    Id = Guid.NewGuid().ToString()
                };
            }
        }

        private async void Save()
        {
            await DataStore.AddItemAsync(Item);
            Items = await DataStore.GetItemsAsync(true);
        }

        #endregion Methods
    }
}