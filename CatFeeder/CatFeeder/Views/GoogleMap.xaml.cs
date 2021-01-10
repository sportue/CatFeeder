using CatFeeder.ViewModels;
using Plugin.ExternalMaps;
using System;
using System.Linq;
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
        private string selectedPinName;
        private Position selectedPosition;
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
                 
                    Position = new Position(content.Latitude, content.Longitude)
                };
         
                map.Pins.Add(BowlPin);
               
                 map.PinClicked += (sender2, args) => {
                     //selectedPinName = ((Map)sender2).Label;
                     selectedPosition = args.Pin.Position;
                    //  Uri uri = new Uri("http://maps.google.com/maps?daddr=" + "40.99502" + "," + "29.06440");/
                    // Device.OpenUri(uri);
                    CrossExternalMaps.Current.NavigateTo("Target", selectedPosition.Latitude, selectedPosition.Longitude);
                };
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
                Pin CurrentLocationPin = new Pin()
                {
                    Type = PinType.Place,
                    Label = "Current",
                    Position = new Position(location.Latitude, location.Longitude),
                    //Position = new Position(40.99441, 29.03968),
                   Icon = (Device.RuntimePlatform == Device.Android) ? BitmapDescriptorFactory.FromBundle("imgbin_feed-garfield.png") :
                                                             BitmapDescriptorFactory.FromView(new Image()
                                                             { Source = "imgbin_feed-garfield.png", WidthRequest = 50, HeightRequest = 50 }),
                };

                map.Pins.Add(CurrentLocationPin);


                var target = new Position(Convert.ToDouble(content.Latitude), Convert.ToDouble(content.Longitude));
                var diameter = CalculateDistance(target, CurrentLocationPin.Position);

                map.MoveToRegion(MapSpan.FromCenterAndRadius(CurrentLocationPin.Position, Distance.FromMeters(10000)));
            }
            catch (Exception)
            {

                throw;
            }
        }
        async void map_PinDragEnd(System.Object sender, Xamarin.Forms.GoogleMaps.PinDragEventArgs e)
        {
            var positions = new Position(e.Pin.Position.Latitude, e.Pin.Position.Longitude);//Latitude, Longitude
            map.MoveToRegion(MapSpan.FromCenterAndRadius(positions, Distance.FromMeters(500)));
            await App.Current.MainPage.DisplayAlert("Alert", "Pick up location : Latitude :" + e.Pin.Position.Latitude + " Longitude :" + e.Pin.Position.Longitude, "Ok");
        }

        void map_PinDragStart(System.Object sender, Xamarin.Forms.GoogleMaps.PinDragEventArgs e)
        {


        }

        // burası araştıralacak
        private int CalculateDistance(Position target, Position currentLocation)
        {
            var retval = (int)Math.Sqrt(Math.Pow(target.Longitude - currentLocation.Longitude, 2) + Math.Pow(target.Latitude - currentLocation.Latitude, 2));
            return retval;
        }


     

    }
}