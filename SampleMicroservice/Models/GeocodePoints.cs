using System;
namespace SampleMicroservice.Models
{
    public class GeoCodePoints
    {
        public GeoCodePoints()
        {
        }

        public string Type { get; set; }
        public Coordinates Coordinates { get; set; }
    }
}
