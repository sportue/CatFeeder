using CatFeeder.Models;

namespace CatFeeder.Services
{
    public class Constants
    {
        public static User CURRENT_USER;
        public static string loginStatus;
        public const string CALLBACK_SCHEME = "myapp";

        #region FacebookAuthorize
        public static string FacebookiOSClientId = string.Empty;
        public static string FacebookAuthorizeUrl = "https://www.facebook.com/dialog/oauth";
        public static string FacebookAndroidClientId = "886642638791083";
        public static string FacebookAccessTokenUrl = "https://www.facebook.com/connect/login_success.html";
        public static string FacebookAndroidRedirectUrl = "https://www.facebook.com/connect/login_success.html";
        public static string FacebookiOSRedirectUrl = string.Empty;
        public static string FacebookScope = "email";

        #endregion

        #region GoogleAuthorize

        public static string GoogleiOSClientId = string.Empty;
        public static string GoogleAndroidClientId = "217481810703-0lcdu01g68n96f6jdc89hq33r8e7l3r6.apps.googleusercontent.com";
        public static string GoogleScope = "https://www.googleapis.com/auth/userinfo.email";
        public static string GoogleAuthorizeUrl = "https://accounts.google.com/o/oauth2/auth";      
        public static string GoogleAccessTokenUrl = "https://www.googleapis.com/oauth2/v4/token";
       
        public static string GoogleUserInfoUrl = "https://www.googleapis.com/oauth2/v2/userinfo";
        public static string GoogleiOSRedirectUrl = string.Empty;
        public static string GoogleAndroidRedirectUrl = "com.googleusercontent.apps.217481810703-0lcdu01g68n96f6jdc89hq33r8e7l3r6:/oauth2redirect";
        #endregion

        #region GoogleMap
        public const string GoogleMapsApiKey = "AIzaSyDxnQqG4FP0hF2X5RilaGRXGyfILqnmDKg";
                                              


        #endregion


        public static string SignIn = "http://sportue.xyz/sportue/api/login/signIn";
        public static string SignUp = "http://sportue.xyz/sportue/api/login/signUp";
        public static string ForgotPassword = "http://sportue.xyz/sportue/api/Login/ForgetPassword";
        public static string SignInWithFacebook = "http://sportue.xyz/sportue/api/Login/Facebook";
        public static string TargetLocations = "http://sportue.xyz/sportue/api/Map/TargetLocations";

    }
}
