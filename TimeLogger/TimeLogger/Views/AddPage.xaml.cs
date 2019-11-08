using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TimeLogger.ViewModels;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace TimeLogger.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class AddPage : ContentPage
    {
        public AddPage()
        {
            InitializeComponent();
        }

        protected override void OnAppearing()
        {
            base.OnAppearing();
            (BindingContext as AddPageViewModel).OnAppearing();
        }

        protected override void OnDisappearing()
        {
            base.OnDisappearing();
            (BindingContext as AddPageViewModel).OnDisappearing();
        }
    }
}