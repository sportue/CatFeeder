using System.Windows.Input;
using Xamarin.Forms;
using CatFeeder.Views;

namespace CatFeeder.ViewModels
{
    public class MapViewModel
    {
        ICommand googleMapCommand;
        public MapViewModel()
        {
            GoogleMapCommand = new Command(googleMapFunction);
        }

        public async void googleMapFunction()
        {
            await App.Current.MainPage.Navigation.PushAsync(new NavigationPage(new GoogleMap()));
        }

        public ICommand GoogleMapCommand { get => googleMapCommand; set => googleMapCommand = value; }
    }
}
