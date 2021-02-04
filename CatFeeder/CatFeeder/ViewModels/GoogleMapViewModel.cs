using CatFeeder.Models.Request.MapRequest;
using CatFeeder.Models.Response;
using CatFeeder.Models.Response.MapResponse;
using CatFeeder.Services;
using System.Collections.Generic;
using System.Threading.Tasks;
using Xamarin.Essentials;

namespace CatFeeder.ViewModels
{
    public class GoogleMapViewModel : BaseViewModel
    {

        public GoogleMapViewModel()
        {

        }

        public class BowlLocation
        {
            public double Latitude { get; set; }
            public double Longitude { get; set; }
        }
        internal async Task<List<BowlLocation>> LoadBowl()
        {
            var location = await Geolocation.GetLastKnownLocationAsync();
            var lastKnownLocation = new MapRequest()
            {
                Latitude = location.Latitude,
                Longitude = location.Latitude
            };
            BaseResponse<MapCoordinates> data = await App.MapService.TargetLocations(lastKnownLocation);
            var bowlLocation = new List<BowlLocation>();
            foreach (var item in data.Data.Items)
            {
                var bowl = new BowlLocation()
                {
                    Latitude = item.Latitude,
                    Longitude = item.Longitude
                };
                bowlLocation.Add(bowl);
            }


            return bowlLocation;
        }

    }
}
