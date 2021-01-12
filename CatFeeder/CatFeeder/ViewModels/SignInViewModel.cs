using CatFeeder.Models;
using CatFeeder.Services;
using System.Windows.Input;
using Xamarin.Forms;
using CatFeeder.Views;
using CatFeeder.Helpers;
using System;

using Newtonsoft.Json;
using CatFeeder.Models.Response.LoginResponse;
using CatFeeder.Models.Response;

namespace CatFeeder.ViewModels
{
    public class SignInViewModel : BaseViewModel
    {
        string email;
        string password;
        ImageSource imgSource;
        ICommand loginCommand;
        ICommand signUpCommand;
        ICommand forgotPasswordCommand;

        public SignInViewModel()
        {
            ImgSource = ImageSource.FromResource("CatFeeder.Images.xamarin.png");
            SignUpCommand = new Command(signUpFunction);
            LoginCommand = new Command(loginFunction);
            ForgotPasswordCommand = new Command(forgotPasswordFunction);
        }

        public async void forgotPasswordFunction()
        {
            await App.Current.MainPage.Navigation.PushAsync(new NavigationPage(new ForgotPassword()));
        }

        public async void loginFunction() 
        {
            string validationText = string.Empty;
            bool validation = true;
            if (string.IsNullOrWhiteSpace(Email) || !Email.IsValidEmailAddress())
            {
                validationText += "Email is not valid";
                validation = false;
            }
            if (string.IsNullOrWhiteSpace(Password) || Password.Length < 6)
            {
                if (validation)
                {
                  validationText += "Password must be at least six characters";
                }
                else
                {
                   validationText += "\n" + "Password must be at least six characters";
                }
                validation = false;
            }
            if (!validation)
            {
                await App.Current.MainPage.DisplayAlert("Error", validationText, "Ok");
            }
            else
            {
                BaseResponse<SignInResponse> data = await App.UserService.getUserByMailAndPassword(Email, Password);
                if (!data.HasError)
                {
                    await App.Current.MainPage.DisplayAlert("Message", "Welcome " + Constants.CURRENT_USER.Username + " !", "Ok");
                    await App.Current.MainPage.Navigation.PushAsync(new Map());
                }
                else
                {
                    await App.Current.MainPage.DisplayAlert("Message", data.Message + "!", "Ok");
                }
            }
        }

        public async void signUpFunction()
        {
            await App.Current.MainPage.Navigation.PushAsync(new NavigationPage(new SignUp())
            {
                BarBackgroundColor = Color.Yellow,
                BarTextColor = Color.Blue
            });
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
        public string LoginStatus
        {
            get => Constants.loginStatus; set
            {
                if (Constants.loginStatus != value)
                {
                    Constants.loginStatus = value;
                    onPropertyChanged("LoginStatus");
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
                }
            }
        }

        public ICommand LoginCommand { get => loginCommand; set => loginCommand = value; }
        public ICommand SignUpCommand { get => signUpCommand; set => signUpCommand = value; }
        public ICommand ForgotPasswordCommand { get => forgotPasswordCommand; set => forgotPasswordCommand = value; }
    }
}
