using CatFeeder.Services;
using CatFeeder.Views;
using System.Windows.Input;
using Xamarin.Forms;

namespace CatFeeder.ViewModels
{
    public class MainPageViewModel : BaseViewModel
    {
        ImageSource imgSource;
        ICommand signInCommand;
        ICommand signUpCommand;
        ICommand auth;

        public MainPageViewModel()
        {
            ImgSource = ImageSource.FromResource("CatFeeder.Images.xamarin.png");
            SignInCommand = new Command(signInFunction);
            SignUpCommand = new Command(signUpFunction);
            Auth = new Command(authFunction);

        }

        public async void authFunction()
        {
            if (LoginStatus == "Oturum Açma Başarılı")
            {
                await App.Current.MainPage.Navigation.PushAsync(new NavigationPage(new Map()));
            }

        }

        public async void signUpFunction()
        {
            await App.Current.MainPage.Navigation.PushAsync(new NavigationPage(new SignUp()));
        }
        public async void signInFunction()
        {
            await App.Current.MainPage.Navigation.PushAsync(new NavigationPage(new SignIn()));
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

        public ICommand SignUpCommand { get => signUpCommand; set => signUpCommand = value; }
        public ICommand Auth { get => auth; set => auth = value; }
        public ICommand SignInCommand { get => signInCommand; set => signInCommand = value; }
    }
}
