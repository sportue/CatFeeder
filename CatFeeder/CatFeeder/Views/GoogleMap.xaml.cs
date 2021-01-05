using CatFeeder.ViewModels;
using System;
using Xamarin.Essentials;
using Xamarin.Forms;
using Xamarin.Forms.GoogleMaps;
using Xamarin.Forms.Xaml;

namespace CatFeeder.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class GoogleMap : ContentPage
    {
        GoogleMapViewModel googleMapViewModel;
        public GoogleMap()
        {
            InitializeComponent();
            BindingContext = googleMapViewModel = new GoogleMapViewModel(); 
        }

        async void Button_Clicked(object sender, System.EventArgs e)
        {
            var content = await googleMapViewModel.LoadBowl();

            if (content != null)
            {
                Pin BowlPin = new Pin()
                {
                    Label = "Bowl",
                    Type = PinType.Place,
                    Icon = (Device.RuntimePlatform == Device.Android) ? BitmapDescriptorFactory.FromBundle("imgbin_feed-garfield.png") : 
                                                             BitmapDescriptorFactory.FromView(new Image() 
                                                           { Source = "imgbin_feed-garfield.png", WidthRequest = 20, HeightRequest = 20 }),
                    Position = new Position(content.Latitude, content.Longitude)
                };
                map.Pins.Add(BowlPin);
            }

            try
            {
                var location = await Geolocation.GetLastKnownLocationAsync();
                if ( location == null)
                {
                    location = await Geolocation.GetLocationAsync(new GeolocationRequest
                    {
                        DesiredAccuracy = GeolocationAccuracy.Medium,
                        Timeout = TimeSpan.FromSeconds(30)
                    }) ;
                }
                var target = new Position(Convert.ToDouble(content.Latitude), Convert.ToDouble(content.Longitude));


                map.MoveToRegion(MapSpan.FromCenterAndRadius(target, Distance.FromMeters(500)));
            }
            catch (Exception)
            {

                throw;
            }
    

        }
    }
}