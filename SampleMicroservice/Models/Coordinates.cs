using System;
namespace SampleMicroservice.Models
{
    public class Coordinates
    {
        public Coordinates()
        {
            Longitude = 0;
            Latitude = 0;
        }

        public long Longitude { get; set; }
        public long Latitude { get; set; }
    }  
}
