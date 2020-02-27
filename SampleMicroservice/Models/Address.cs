using System;
using System.ComponentModel.DataAnnotations;

namespace SampleMicroservice.Models
{
    public class Address
    {
        public Address()
        {
            Country = "US";
            HouseNumber = 1;
            Street = "Unknown Street";
            City = "Bubbling";
            State = "VA";
            Zip = "22554";
        }

        [Required]
        public int HouseNumber { get; set; }
        [Required]
        public string Street { get; set; }
        [Required]
        public string City { get; set; }
        [Required]
        [StringLength(2, ErrorMessage = "Maximum of 2 characters.")]
        public string State { get; set; }
        public string Country { get; set; }
        [Required]
        [StringLength(10, MinimumLength = 5)]
        public string Zip { get; set; }
        public string FullAddress
        {
            get
            {
                return $"{HouseNumber} {Street}";
            }
        }
        public Coordinates Coordinates { get; set; }
    }
}
