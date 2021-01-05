using Android.App;
using Xamarin.Forms.Xaml;

[assembly: XamlCompilation(XamlCompilationOptions.Compile)]

[assembly: UsesPermission(Android.Manifest.Permission.AccessCoarseLocation)]
[assembly: UsesPermission(Android.Manifest.Permission.AccessFineLocation)]

[assembly : UsesFeature("android.hardware.localiton", Required = true)]
[assembly: UsesFeature("android.hardware.localiton.gps", Required = true)]
[assembly: UsesFeature("android.hardware.localiton.network", Required = true)]