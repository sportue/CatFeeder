using CatFeeder.Models;
using CatFeeder.Services;
using System.Windows.Input;
using Xamarin.Forms;
using CatFeeder.Views;
using System;
using Plugin.GoogleClient;
using Plugin.GoogleClient.Shared;
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
        public Command GoogleLoginCommand { get; }

        public IGoogleClientManager _googleService = CrossGoogleClient.Current;
        public SignInViewModel()
        {

            ImgSource = ImageSource.FromResource("CatFeeder.Images.xamarin.png");
            SignUpCommand = new Command(signUpFunction);
            LoginCommand = new Command(loginFunction);
            GoogleLoginCommand = new Command(GoogleLogin);

        }

        private void GoogleLogin(object obj)
        {
            try
            {
                if (!string.IsNullOrEmpty(_googleService.AccessToken))
                {
                    //Always require user authentication
                    _googleService.Logout();
                }

                EventHandler<GoogleClientResultEventArgs<GoogleUser>> userLoginDelegate = null;
                userLoginDelegate = async (object sender, GoogleClientResultEventArgs<GoogleUser> e) =>
                {
                    switch (e.Status)
                    {
                        case GoogleActionStatus.Completed:
                                var googleUserString = JsonConvert.SerializeObject(e.Data);
                            break;

                            //    var socialLoginData = new NetworkAuthData
                            //{
                            //    Id = e.Data.Id,
                            //    Logo = authNetwork.Icon,
                            //    Foreground = authNetwork.Foreground,
                            //    Background = authNetwork.Background,
                            //    Picture = e.Data.Picture.AbsoluteUri,
                            //    Name = e.Data.Name,
                            //};

                         
                    }

                    _googleService.OnLogin -= userLoginDelegate;
                };

                _googleService.OnLogin += userLoginDelegate;

            }
            catch (Exception ex)
            {
            }

        }

        public async void loginFunction()
        {
            User u = await App.UserService.getUserByMailAndPassword(Email, Password);
            if (u == null)
            {
                LoginStatus = "Oturum Açma Başarısız";
            }
            else
            {
                LoginStatus = "Oturum Açma Başarılı";

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
    }
}
