using System;
using Android.App;
using Android.Content;
using Android.Content.PM;
using Android.OS;
using CatFeeder.Helpers;

namespace CatFeeder.Droid
{
	[Activity(Label = "CustomUrlSchemeInterceptorActivity", NoHistory = true, LaunchMode = LaunchMode.SingleTop)]
	[IntentFilter(
	new[] { Intent.ActionView }, 
	Categories = new[] { Intent.CategoryDefault, Intent.CategoryBrowsable },
	DataSchemes = new[] { "com.googleusercontent.apps.217481810703-0lcdu01g68n96f6jdc89hq33r8e7l3r6" },
	DataPath = "/oauth2redirect")]
	public class CustomUrlSchemeInterceptorActivity : Activity
	{
		protected override void OnCreate(Bundle savedInstanceState)
		{
			base.OnCreate(savedInstanceState);

			// Convert Android.Net.Url to Uri
			var uri = new Uri(Intent.Data.ToString());

			// Load redirectUrl page
			AuthenticationState.Authenticator.OnPageLoading(uri);

			Finish();
		}
	}
}
