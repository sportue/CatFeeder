using System.Windows.Input;
using Xamarin.Forms;
using CatFeeder.Views;

namespace CatFeeder.ViewModels
{
    public class ForgotPasswordViewModel
    {
        string email;
        string username;
        ICommand sendPasswordRetryCommand;

        public ForgotPasswordViewModel()
        {
            SendPasswordRetryCommand = new Command(sendPasswordRetryFunction);
        }

        public async void sendPasswordRetryFunction()
        {
           var data = await App.UserService.ForgotPassword(Email, Username);

            await App.Current.MainPage.DisplayAlert("Message", data.Message, "Ok");

            if (!data.HasError)
            {
                await App.Current.MainPage.Navigation.PushAsync(new NavigationPage(new SignIn()));
            }

        }


        
        public string Email { get => email; set => email = value; }
        public string Username { get => username; set => username = value; }
        public ICommand SendPasswordRetryCommand { get => sendPasswordRetryCommand; set => sendPasswordRetryCommand = value; }
    }
}
