using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using TimeLogger.Models;
using Xamarin.Forms;

namespace TimeLogger.ViewModels
{
    public class AddPageViewModel : BaseViewModel
    {
        #region Properties & Fields & Commands

        private Item _item;

        public Item Item
        {
            get => _item;
            set => SetProperty(ref _item, value);
        }

        private DateTime _dateTime = DateTime.Now;

        public DateTime DateTimeValue
        {
            get => _dateTime;
            set => SetProperty(ref _dateTime, value, async () => await DateCheck(value));
        }

        public ICommand SubmitCommand { get; set; }

        #endregion Properties & Fields & Commands

        public AddPageViewModel()
        {
            InitData(DateTime.Now);
            SubmitCommand = new Command(Save);
            _ = DateCheck(DateTime.Now);
        }

        #region Methods

        private void InitData(DateTime dateTime)
        {
            Item = new Item
            {
                TitleText = dateTime.ToShortDateString(),
                Description = dateTime.ToLongDateString(),
                InTime = dateTime.TimeOfDay.ToString(),
                OutTime = dateTime.TimeOfDay.ToString()
            };
        }

        private async void Save()
        {
            if (Item.Id == null)
            {
                Item.Id = Guid.NewGuid().ToString();
                await DataStore.AddItemAsync(Item);
            }
            else
            {
                await DataStore.UpdateItemAsync(Item);
            }
            await DateCheck(DateTimeValue);
            await App.Current.MainPage.DisplayAlert("Message", "Log Saved", "ok");
        }

        private async Task DateCheck(DateTime dateTime)
        {
            var item = await DataStore.GetItemAsync(dateTime.ToShortDateString());
            if (item != null)
            {
                Item = item;
            }
            else
            {
                InitData(dateTime);
            }
            OnPropertyChanged("Item");
        }

        #endregion Methods
    }
}