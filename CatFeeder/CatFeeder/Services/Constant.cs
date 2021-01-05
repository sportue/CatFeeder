using CatFeeder.Models;

namespace CatFeeder.Services
{
    public class Constants
    {
        public static User CURRENT_USER;
        public static string loginStatus;
        public static string AppName = "OAuthNativeFlow";

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
        public static string GoogleAndroidClientId = "923711648016-t686mqe5loqan1qgjb1c196euatiajdo.apps.googleusercontent.com";
        public static string GoogleScope = "https://www.googleapis.com/auth/userinfo.email";
        public static string GoogleAuthorizeUrl = "https://accounts.google.com/o/oauth2/auth";
        public static string GoogleAccessTokenUrl = "https://www.googleapis.com/oauth2/v4/token";
        public static string GoogleUserInfoUrl = "https://www.googleapis.com/oauth2/v2/userinfo";
        public static string GoogleiOSRedirectUrl = string.Empty;
        public static string GoogleAndroidRedirectUrl = "com.googleusercontent.apps.923711648016-t686mqe5loqan1qgjb1c196euatiajdo:/oauth2redirect";
        #endregion

        #region GoogleMap
        public const string GoogleMapsApiKey = "AIzaSyDIBWQnIVZJb3znc3B8EVJ5sFEd-L0QoU8";
        #endregion


        public static string SignIn = "http://sportue.xyz/api/login/signIn";
        public static string SignUp = "http://sportue.xyz/api/login/signUp";
        public static string ForgotPassword = "http://sportue.xyz/api/Login/ForgetPassword";
        public static string SignInWithFacebook = "http://sportue.xyz/api/Login/Facebook";

    }
}
