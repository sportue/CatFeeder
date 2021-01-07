using CatFeeder.Models;
using CatFeeder.Models.Request.LoginRequest;
using CatFeeder.Services;
using CatFeeder.Views;
using System.Windows.Input;
using Xamarin.Forms;

namespace CatFeeder.ViewModels
{
    public class SignUpViewModel : BaseViewModel
    {
        string username;
        string firstName;
        string lastName;
        string email;
        string password;
        string passwordRetry;
        ImageSource imgSource;
        ICommand signUpCommand;

        public SignUpViewModel()
        {
            ImgSource = ImageSource.FromResource("CatFeeder.Images.emptyuser.png");
            SignUpCommand = new Command(signUpFunction);
        }
        public async void signUpFunction()
        {
            SignUpRequest req = new SignUpRequest();
            req.Password = Password;
            req.PasswordRetry = PasswordRetry;
            req.Email = Email;
            req.FirstName = FirstName;
            req.LastName = LastName;
            req.Username = Username;
            User user = await App.UserService.addUser(req);
            if (user != null)
            {
                await App.Current.MainPage.DisplayAlert("Message", "Sign Up is successful," + " Welcome " + user.Username + " !", "Ok");
                Constants.CURRENT_USER = user;
                await App.Current.MainPage.Navigation.PushAsync(new NavigationPage(new Map()));
            }
        }

        public string Username
        {
            get => username; set
            {
                if (username != value)
                {
                    username = value;
                }
            }
        }
        public string FirstName
        {
            get => firstName; set
            {
                if (firstName != value)
                {
                    firstName = value;
                }
            }
        }
        public string LastName
        {
            get => lastName; set
            {
                if (lastName != value)
                {
                    lastName = value;
                }
            }
        }
        public string Email
        {
            get => email; set
            {
                if (email != value)
                {
                    email = value;
                }
            }
        }
        public string Password
        {
            get => password; set
            {
                if (password != value)
                {
                    password = value;
                }
            }
        }
        public string PasswordRetry
        {
            get => passwordRetry; set
            {
                if (passwordRetry != value)
                {
                    passwordRetry = value;
                }
            }
        }
        public ImageSource ImgSource
        {
            get => imgSource; set
            {
                if (imgSource != value)
                {
                    imgSource = value;
                    //onPropertyChanged("ImgSource");
                }
            }
        }

        public ICommand SignUpCommand { get => signUpCommand; set => signUpCommand = value; }
    }
}
