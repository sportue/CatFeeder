using CatFeeder.Models;
using CatFeeder.Services;
using System;
using System.Threading.Tasks;
using Xamarin.Auth;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using Xamarin.Essentials;
using Newtonsoft.Json;
using System.Diagnostics;
using CatFeeder.Helpers;
using CatFeeder.Models.Response;
using CatFeeder.Models.Response.LoginResponse;
using CatFeeder.DependencyServices;
using CatFeeder.Models.Google;

namespace CatFeeder.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class SignIn : ContentPage
    {
        private readonly IGoogleManager _googleManager;
        public Command GoogleLoginCommand { get; set; }
        public Command GoogleLogoutCommand { get; set; }
        private GoogleUser googleUser;

        public SignIn()
        {
            InitializeComponent();
         // buttonLogin.IsEnabled = false;
//#if DEBUG
//            Email.Text = "cihanoguz92@gmail.com";
//            entryPassword.Text = "123456";
//#endif
        }


        private async void LoginFb_Clicked(object sender, EventArgs e)
        {
            string clientId = String.Empty;
            string redirectUri = String.Empty;

            switch (Device.RuntimePlatform)
            {
                case Device.iOS:
                    clientId = Constants.FacebookiOSClientId;
                    redirectUri = Constants.FacebookiOSClientId;
                    break;
                case Device.Android:
                    clientId = Constants.FacebookAndroidClientId;
                    redirectUri = Constants.FacebookAndroidRedirectUrl;
                    break;
            }
            var Authenticator =
                new OAuth2Authenticator(
                    clientId,
                    Constants.FacebookScope,
                    new Uri(Constants.FacebookAuthorizeUrl),
                    new Uri(Constants.FacebookAndroidRedirectUrl),
                    null,
                    false);
            var Presenter = new Xamarin.Auth.Presenters.OAuthLoginPresenter();
            Presenter.Login(Authenticator);
            Authenticator.Completed += Authenticator_Completed;
        }

        private async void Authenticator_Completed(object sender, AuthenticatorCompletedEventArgs e)
        {

            if (e.IsAuthenticated)
            {
                string accsessToken = e.Account.Properties["access_token"];
                await LoginWithFacebookFunctionAsync(accsessToken);
            }

        }

        public async Task LoginWithFacebookFunctionAsync(string tokenAccess)
        {
            BaseResponse<SignInResponse> data = await App.UserService.getUserByFacebookToken(tokenAccess);

            if (!data.HasError)
            {
                await App.Current.MainPage.DisplayAlert("Message", " Welcome " + Constants.CURRENT_USER.FirstName+ " " + Constants.CURRENT_USER.LastName + " !", "Ok");
                await App.Current.MainPage.Navigation.PushAsync(new Map());
            }
            else
            {
                await App.Current.MainPage.DisplayAlert("Message", data.Message + "!", "Ok");
            }
        }

        #region Google Login
        private void GoogleLogin()
        {
            _googleManager.Login(OnLoginComplete);
        }

        private void OnLoginComplete(GoogleUser googleUser, string message)
        {
            if (googleUser != null)
            {
                this.googleUser = googleUser;
            }
            else
            {
                DisplayAlert("Hata", message, "Tamam");
            }
        }

        void ButtonGoogle_Clicked(object sender, System.EventArgs e)
        {
            DependencyService.Get<IGoogleManager>().Login(OnGoogleLoginComplete);
        }

        private async void OnGoogleLoginComplete(GoogleUser googleUser, string message)
        {
            if (googleUser != null)
            {
                var takenModel = googleUser;
                switch (Device.RuntimePlatform)
                {
                    case Device.Android:
                        {
                            MessagingCenter.Subscribe<string>(this, "googleAndroidAccessToken", async (obj) =>
                            {
                                takenModel.AccessToken = obj;
                                // TODO: service calling
                            });
                            break;
                        }
                    case Device.iOS:
                        {
                            var takenAccessToken = takenModel.AccessToken;
                            // TODO: service calling
                            break;
                        }
                    default:

                        break;
                }
            }
        }
        #endregion

        void BorderlessEntryMail_TextChanged(System.Object sender, Xamarin.Forms.TextChangedEventArgs e)
        {
            if (string.IsNullOrWhiteSpace(Email.Text) || !Email.Text.IsValidEmailAddress())
                labelEmailError.HeightRequest = 30;
            else
                labelEmailError.HeightRequest = 0;
            ValidateForm();
        }
        void BorderlessEntryPassword_TextChanged(System.Object sender, Xamarin.Forms.TextChangedEventArgs e)
        {
            if (e.NewTextValue.Length < 6)
                labelPasswordError.HeightRequest = 30;
            else
                labelPasswordError.HeightRequest = 0;
            ValidateForm();
        }
        void ValidateForm()
        {
            bool validation = true;
            if (string.IsNullOrWhiteSpace(Email.Text) || !Email.Text.IsValidEmailAddress())
            {
                validation = false;
            }
           
            if (string.IsNullOrWhiteSpace(entryPassword.Text) || entryPassword.Text.Length < 6)
            {
                validation = false;
            }
            buttonLogin.IsEnabled = validation;
        }
    }
}