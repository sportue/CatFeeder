﻿using CatFeeder.Models;
using CatFeeder.Services;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Auth;
using Xamarin.Essentials;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace CatFeeder.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class SignIn : ContentPage
    {
        //Account account;
        // AccountStore store;
        public SignIn()
        {
            InitializeComponent();
            //  store = AccountStore.Create();

        }


        private async void LoginFb_Clicked(object sender, EventArgs e)
        {
            await Permission();

            string clientId = String.Empty;
            string redirectUri = String.Empty;

            switch (Device.RuntimePlatform)
            {
                case Device.iOS:
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
            User u = await App.UserService.getUserByFacebookToken(tokenAccess);
            if (u == null)
            {
                Constants.loginStatus = "Oturum Açma Başarısız";
            }
            else
            {
                Constants.loginStatus = "Oturum Açma Başarılı";

                await App.Current.MainPage.Navigation.PushAsync(new NavigationPage(new Map()));

            }
        }

        private async void LoginGoogle_Clicked(object sender, EventArgs e)
        {
            await Permission();

            string clientId = null;
            string redirectUri = null;

            switch (Device.RuntimePlatform)
            {
                case Device.iOS:
                    clientId = Constants.GoogleiOSClientId;
                    redirectUri = Constants.GoogleiOSRedirectUrl;
                    break;

                case Device.Android:
                    clientId = Constants.GoogleAndroidClientId;
                    redirectUri = Constants.GoogleAndroidRedirectUrl;
                    break;
            }


            // account = store.FindAccountsForService(Constants.AppName).FirstOrDefault();

            var authenticator = new OAuth2Authenticator(
                clientId,
                null,
                Constants.GoogleScope,
                new Uri(Constants.GoogleAuthorizeUrl),
                new Uri(redirectUri),
                new Uri(Constants.GoogleAccessTokenUrl),
                null,
                true);



            authenticator.Completed += GoogleOnAuthCompleted;
            authenticator.Error += GoogleOnAuthError;

            AuthenticationState.Authenticator = authenticator;

            var presenter = new Xamarin.Auth.Presenters.OAuthLoginPresenter();
            presenter.Login(authenticator);

        }
        async void GoogleOnAuthCompleted(object sender, AuthenticatorCompletedEventArgs e)
        {
            var authenticator = sender as OAuth2Authenticator;
            if (authenticator != null)
            {
                authenticator.Completed -= GoogleOnAuthCompleted;
                authenticator.Error -= GoogleOnAuthError;
            }

            User user = null;
            if (e.IsAuthenticated)
            {
                // If the user is authenticated, request their basic user data from Google
                // UserInfoUrl = https://www.googleapis.com/oauth2/v2/userinfo
                var request = new OAuth2Request("GET", new Uri(Constants.GoogleUserInfoUrl), null, e.Account);
                var response = await request.GetResponseAsync();
                if (response != null)
                {
                    // Deserialize the data and store it in the account store
                    // The users email address will be used to identify data in SimpleDB
                    string userJson = await response.GetResponseTextAsync();
                    user = JsonConvert.DeserializeObject<User>(userJson);
                }

                if (user != null)
                {
                    App.Current.MainPage = new NavigationPage(new Map());

                }

                //   await store.SaveAsync(account = e.Account, Constants.AppName);
                await DisplayAlert("Email address", user.Email, "OK");
            }
        }

        void GoogleOnAuthError(object sender, AuthenticatorErrorEventArgs e)
        {
            var authenticator = sender as OAuth2Authenticator;
            if (authenticator != null)
            {
                authenticator.Completed -= GoogleOnAuthCompleted;
                authenticator.Error -= GoogleOnAuthError;
            }
            Debug.WriteLine("Authentication error: " + e.Message);
            Preferences.Set("Token", Constants.CURRENT_USER.Token);
            //var token = Preferences.Get("Token", "");
        }

        private async Task<PermissionStatus> Permission()
        {
            var permission = await Permissions.CheckStatusAsync<Permissions.LocationWhenInUse>();

            if (permission != PermissionStatus.Granted)
            {
                while (permission == PermissionStatus.Granted)
                {
                    permission = await Permissions.RequestAsync<Permissions.LocationWhenInUse>();
                }
            }

            return permission;
        }

    }
}