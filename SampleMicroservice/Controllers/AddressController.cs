using System;
using System.Net.Http;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using SampleMicroservice.Models;

namespace SampleMicroservice.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AddressController : ControllerBase
    {
        private readonly string _mapsApiKey;
        private readonly IConfiguration _configuration;
        private HttpClient _client = new HttpClient() { BaseAddress = new Uri("http://localhost:5000") };

        public AddressController(IConfiguration configuration)
        {
            this._configuration = configuration;
            _mapsApiKey = _configuration.GetSection("MapBoxAPIKey").GetSection("Key").Value;
        }

        public async void ProcessAddress()
        {
            var address = new Address();

            if (address.Coordinates.Longitude == 0 || address.Coordinates.Latitude == 0)
            {
                var geoCodes = await GetGeoCodePoints(address);

                address.Coordinates.Latitude = geoCodes.Coordinates.Latitude;
                address.Coordinates.Longitude = geoCodes.Coordinates.Longitude;
            }
        }

        private async Task<GeoCodePoints> GetGeoCodePoints(Address address)
        {
            var url = $"https://api.mapbox.com/geocoding/v5/mapbox.places/" +
                $"{address.FullAddress}.json?access_token={_mapsApiKey}";

            var response = (await _client.GetAsync(url)).Content.ReadAsStringAsync().Result;

            return new GeoCodePoints();

        }
    }
}
