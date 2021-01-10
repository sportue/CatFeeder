using CatFeeder.Models;
using CatFeeder.Services;
using System.Windows.Input;
using Xamarin.Forms;
using CatFeeder.Views;
using CatFeeder.Helpers;
using System;

using Newtonsoft.Json;

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
            //GoogleLoginCommand = new Command(GoogleLogin);
            ForgotPasswordCommand = new Command(forgotPasswordFunction);

        }


        public async void forgotPasswordFunction()
        {
            
            await App.Current.MainPage.Navigation.PushAsync(new NavigationPage(new ForgotPassword()));
        }



        public async void loginFunction() 
        {
           
            User user = await App.UserService.getUserByMailAndPassword(Email, Password);
            if (user == null)
            {
                LoginStatus = "Oturum Açma Başarısız";
            }
            else
            {
                LoginStatus = "Oturum Açma Başarılı";
                Constants.CURRENT_USER = user;
                await App.Current.MainPage.Navigation.PushAsync(new NavigationPage(new Map()));

            }
        }

        public async void signUpFunction()
        {
            await App.Current.MainPage.Navigation.PushAsync(new NavigationPage(new SignUp()));
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
