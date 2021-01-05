using System;
using System.Collections.Generic;
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
            var bowlLocation =  new BowlLocation { Latitude = 40.994414, Longitude = 29.039801 };

            return bowlLocation;
        }

        
    }
}
