using System;
using System.Threading.Tasks;
using Android.Content;
using Android.Gms.Auth;
using Android.Gms.Auth.Api;
using Android.Gms.Auth.Api.SignIn;
using Android.Gms.Common;
using Android.Gms.Common.Apis;
using Android.OS;
using CatFeeder.DependencyServices;
using CatFeeder.Models.Google;
using Xamarin.Forms;

namespace CatFeeder.Droid.DependencyServices
{
    public class GoogleManager : Java.Lang.Object, IGoogleManager, GoogleApiClient.IConnectionCallbacks, GoogleApiClient.IOnConnectionFailedListener
    {
        public Action<GoogleUser, string> _onLoginComplete;
        public static GoogleApiClient _googleApiClient { get; set; }
        public static GoogleManager Instance { get; private set; }
        public GoogleManager()
        {
            Instance = this;
            GoogleSignInOptions gso = new GoogleSignInOptions.Builder(GoogleSignInOptions.DefaultSignIn)
                .RequestScopes(new Scope(Scopes.Email), new Scope(Scopes.PlusLogin))
                .RequestEmail()
                .RequestProfile()
                .Build();

            _googleApiClient = new GoogleApiClient.Builder(((MainActivity)Forms.Context).ApplicationContext)
                .AddConnectionCallbacks(this)
                .AddOnConnectionFailedListener(this)
                .AddApi(Auth.GOOGLE_SIGN_IN_API, gso)
                .AddScope(new Scope(Scopes.Profile))
                .Build();
        }

        public void Login(Action<GoogleUser, string> onLoginComplete)
        {
            _onLoginComplete = onLoginComplete;
            Intent signInIntent = Auth.GoogleSignInApi.GetSignInIntent(_googleApiClient);
            ((MainActivity)Forms.Context).StartActivityForResult(signInIntent, 1);
            _googleApiClient.Connect();
        }

        public void OnAuthCompleted(GoogleSignInResult result)
        {
            GoogleUser googleUser = new GoogleUser();
            if (result.IsSuccess)
            {
                Task.Factory.StartNew(() => {
                    if (result.SignInAccount.Account == null)
                    {
                        MessagingCenter.Send<string>("access token bilgisi alınamadı", "NoInternet");
                    }
                    var accessToken = GoogleAuthUtil.GetToken(Android.App.Application.Context, result.SignInAccount.Email, $"oauth2:{Scopes.Email} {Scopes.Profile}");
                    googleUser.AccessToken = accessToken;
                    MessagingCenter.Send<string>(accessToken, "googleAndroidAccessToken");
                  
                });
                GoogleSignInAccount accountt = result.SignInAccount;
                googleUser.Email = accountt.Email;
                googleUser.Name = accountt.GivenName;
                googleUser.Surname = accountt.FamilyName;
                googleUser.AccessToken = accountt.IdToken;
                //googleUser.Token = accountt.IdToken;

                googleUser.Picture = new Uri((accountt.PhotoUrl != null ? $"{accountt.PhotoUrl}" : $"https://autisticdating.net/imgs/profile-placeholder.jpg"));
                _onLoginComplete?.Invoke(googleUser, string.Empty);
            }
            else
            {
                _onLoginComplete?.Invoke(null, "An error occured!");
            }
        }

        public void Logout()
        {
            _googleApiClient.Disconnect();
        }

        public void OnConnected(Bundle connectionHint)
        {
        }

        public void OnConnectionFailed(ConnectionResult result)
        {
            _onLoginComplete?.Invoke(null, result.ErrorMessage);
        }

        public void OnConnectionSuspended(int cause)
        {
            _onLoginComplete?.Invoke(null, "Canceled!");

        }
    }
}
