using System;
using System.Collections.Generic;
using System.Text;

namespace CatFeeder.Models.Response.MapResponse
{
    public class MapCoordinates
    {
        public MapCoordinates()
        {
            Items = new List<BowlLocation>();
        }
        public List<BowlLocation> Items { get; set; }

        public class BowlLocation
        {
            public double Latitude { get; set; }
            public double Longitude { get; set; }
        }
    }
}
