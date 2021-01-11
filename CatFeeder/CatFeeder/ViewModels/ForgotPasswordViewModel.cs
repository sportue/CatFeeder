using System.Windows.Input;
using Xamarin.Forms;
using CatFeeder.Views;
using CatFeeder.Helpers;

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
            string validationText = string.Empty;
            bool validation = true;

            if (string.IsNullOrWhiteSpace(Email) || !Email.IsValidEmailAddress())
            {
                validationText += "Email is not valid";
                validation = false;
            }

            if (string.IsNullOrWhiteSpace(Username) || Username.Length < 3)
            {
                if (validation)
                {
                    validationText += "Username must be at least three characters";
                }
                else
                {
                    validationText += "\n" + "Username must be at least three characters";
                }
                validation = false;
            }

            if (!validation)
            {
                await App.Current.MainPage.DisplayAlert("Error", validationText, "Ok");
            }
            else
            {
                var data = await App.UserService.ForgotPassword(Email, Username);
                await App.Current.MainPage.DisplayAlert("Message", data.Message, "Ok");
                if (!data.HasError)
                {
                    await App.Current.MainPage.Navigation.PushAsync(new NavigationPage(new SignIn()));
                }
            }
        }

        
        public string Email { get => email; set => email = value; }
        public string Username { get => username; set => username = value; }
        public ICommand SendPasswordRetryCommand { get => sendPasswordRetryCommand; set => sendPasswordRetryCommand = value; }
    }
}
