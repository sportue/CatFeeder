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
            var bowlLocation =  new BowlLocation { Latitude = 37.4239983333333, Longitude = -122.085 };


            return bowlLocation;
        }

    }
}
