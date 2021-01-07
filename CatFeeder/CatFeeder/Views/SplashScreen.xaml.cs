using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace CatFeeder.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class SplashScreen : ContentPage
    {
        public SplashScreen()
        {
            InitializeComponent();
        }

        protected override async void OnAppearing()
        {
            base.OnAppearing();
            await checkToken();
        }
        protected override  void OnDisappearing()
        {
            base.OnDisappearing();
            Navigation.RemovePage(this);
        }
        public async Task<bool> checkToken()
        {
            bool result = await App.UserService.LoginWithToken();
            await Task.Delay(2000);
            if (result)
            {
                await Navigation.PushAsync(new Map());
            }
            else
            {
                await Navigation.PushAsync(new MainPage(false));
            }
            return result; 
    }
    }

 
}