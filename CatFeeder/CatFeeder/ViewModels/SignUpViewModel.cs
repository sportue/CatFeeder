using CatFeeder.Helpers;
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
            string validationText = string.Empty;
            bool validation = true;
            if (string.IsNullOrWhiteSpace(Email) || !Email.IsValidEmailAddress())
            {
                validationText += "Email is not valid";
                validation = false;
            }
            if (string.IsNullOrWhiteSpace(Username) || Username.Length <3)
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
            if (string.IsNullOrWhiteSpace(Password) || (Password != PasswordRetry) || Password.Length < 6)
            {
                if (validation)
                {
                    if (string.IsNullOrEmpty(Password) || Password.Length < 6)
                    {
                        validationText += "Password must be at least six characters";
                    }
                    else
                    {
                        validationText += "Password do not match";
                    }
                }
                else
                {
                    if ( string.IsNullOrEmpty(Password) || Password.Length < 6 )
                    {
                        validationText += "\n" + "Password must be at least six characters";
                    }
                    else
                    {
                        validationText += "\n" + "Password do not match";
                    }
                }
                validation = false;
            }
            if (!validation)
            {
                await App.Current.MainPage.DisplayAlert("Error", validationText, "Ok");
            }
            else
            {
                SignUpRequest req = new SignUpRequest();
                req.Password = Password;
                req.PasswordRetry = PasswordRetry;
                req.Email = Email;
                req.FirstName = FirstName;
                req.LastName = LastName;
                req.Username = Username;
                var data = await App.UserService.addUser(req);
               
                if (!data.HasError)
                {
                    await App.Current.MainPage.DisplayAlert("Message", "Sign Up is successful," + " Welcome " + Constants.CURRENT_USER.Username + " !", "Ok");
                    await App.Current.MainPage.Navigation.PushAsync(new Map());
                }
                else
                {
                    await App.Current.MainPage.DisplayAlert("Message", data.Message +"!", "Ok");
                }
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
