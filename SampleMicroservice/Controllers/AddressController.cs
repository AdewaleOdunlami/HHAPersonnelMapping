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
        private readonly string _bingMapsApiKey;
        private readonly IConfiguration _configuration;
        private HttpClient _client = new HttpClient() { BaseAddress = new Uri("http://localhost:5000") };

        public AddressController(IConfiguration configuration)
        {
            this._configuration = configuration;
            _bingMapsApiKey = _configuration.GetSection("BingMapsAPIKey").GetSection("Key").Value;
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
            //with zip
            if (!string.IsNullOrEmpty(address.Zip.Trim()))
            {
                var url = $"http://dev.virtualearth.net/REST/v1/Locations/{address.Country}/{address.State}/{address.Zip}/{address.FullAddress}?key={_bingMapsApiKey}";
                var response = (await _client.GetAsync(url)).Content.ReadAsStringAsync().Result;
            }

            return new GeoCodePoints();

            //without zip
            //if (!string.IsNullOrEmpty(address.Zip.Trim()))
            //{
            //    var url = $"http://dev.virtualearth.net/REST/v1/Locations/{address.Country}/{address.FullAddress}?key={_bingMapsApiKey}";
            //    var response = (await _client.GetAsync(url)).Content.ReadAsAsync<GeoCodePoints>().Result;
            //    return response;
            //}

            //bulk search

            //http://dev.virtualearth.net/REST/v1/Locations?locality=Greenville&maxResults=10&key={BingMapsAPIKey}
        }
    }
}
