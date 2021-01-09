using CatFeeder.ViewModels;
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
                                                           { Source = "imgbin_feed-garfield.png", WidthRequest = 50, HeightRequest = 50 }),
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
                Pin CurrentLocationPin = new Pin()
                {
                    Type = PinType.Place,
                    Label = "Current",
                    // Position = new Position(location.Latitude, location.Longitude)
                    Position = new Position(40.99441, 29.03968)
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

        async void TrackPath_Clicked(System.Object sender, System.EventArgs e)
        {

            var pathcontent = await googleMapViewModel.LoadRoute();

            map.Polylines.Clear();

            var polyline = new Xamarin.Forms.GoogleMaps.Polyline();
            polyline.StrokeColor = Color.Black;
            polyline.StrokeWidth = 3;

            foreach (var p in pathcontent)
            {
                polyline.Positions.Add(p);

            }
            map.Polylines.Add(polyline);

            map.MoveToRegion(MapSpan.FromCenterAndRadius(new Xamarin.Forms.GoogleMaps.Position(polyline.Positions[0].Latitude, polyline.Positions[0].Longitude), Xamarin.Forms.GoogleMaps.Distance.FromMiles(0.50f)));

            var pin = new Xamarin.Forms.GoogleMaps.Pin
            {
                Type = PinType.SearchResult,
                Position = new Xamarin.Forms.GoogleMaps.Position(polyline.Positions.First().Latitude, polyline.Positions.First().Longitude),
                Label = "Pin",
                Address = "Pin",
                Tag = "CirclePoint",
                Icon = (Device.RuntimePlatform == Device.Android) ? BitmapDescriptorFactory.FromBundle("imgbin_feed-garfield.png") : BitmapDescriptorFactory.FromView(new Image() { Source = "imgbin_feed-garfield.png", WidthRequest = 25, HeightRequest = 25 })

            };
            map.Pins.Add(pin);

            var positionIndex = 1;

            Device.StartTimer(TimeSpan.FromSeconds(1), () =>
            {
                if (pathcontent.Count > positionIndex)
                {
                    UpdatePostions(pathcontent[positionIndex]);
                    positionIndex++;
                    return true;
                }
                else
                {
                    return false;
                }
            });
        }

        async void UpdatePostions(Xamarin.Forms.GoogleMaps.Position position)
        {
            if (map.Pins.Count == 1 && map.Polylines != null && map.Polylines?.Count > 1)
                return;

            var cPin = map.Pins.FirstOrDefault();

            if (cPin != null)
            {
                cPin.Position = new Xamarin.Forms.GoogleMaps.Position(position.Latitude, position.Longitude);
                cPin.Icon = (Device.RuntimePlatform == Device.Android) ? BitmapDescriptorFactory.FromBundle("CarPins.png") : BitmapDescriptorFactory.FromView(new Image() { Source = "CarPins.png", WidthRequest = 25, HeightRequest = 25 });
                map.MoveToRegion(MapSpan.FromCenterAndRadius(cPin.Position, Distance.FromMeters(200)));
                var previousPosition = map.Polylines?.FirstOrDefault()?.Positions?.FirstOrDefault();
                map.Polylines?.FirstOrDefault()?.Positions?.Remove(previousPosition.Value);
            }
            else
            {
                map.Polylines?.FirstOrDefault()?.Positions?.Clear();
            }
        }
    }
}