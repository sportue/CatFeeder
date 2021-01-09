using CatFeeder.Models.Google;
using CatFeeder.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CatFeeder.ViewModels
{
    public class GoogleMapViewModel
    {

        public GoogleMapViewModel()
        {

        }

        public class BowlLocation
        {
            public double Latitude { get; set; }
            public double Longitude { get; set; }
        }
        internal async Task<BowlLocation> LoadBowl()
        {
            // Burası API den gelecek
            var bowlLocation =  new BowlLocation { Latitude = 40.99502, Longitude = 29.06440 };

            return bowlLocation;
        }

        internal async Task<System.Collections.Generic.List<Xamarin.Forms.GoogleMaps.Position>> LoadRoute()
        {
            var googleDirection = await ApiServices.ServiceClientInstance.GetDirections("28.127612", "82.309793", "28.163485", "82.314771");
            if (googleDirection.Routes != null && googleDirection.Routes.Count > 0)
            {
                var positions = (Enumerable.ToList(PolylineHelper.Decode(googleDirection.Routes.First().OverviewPolyline.Points)));
                return positions;
            }
            else
            {
                await App.Current.MainPage.DisplayAlert("Alert", "GOOGLE API ERROR", "Ok");
                return null;

            }

        }


    }
}
