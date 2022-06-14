using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Essentials;
using XamarinApp.Models;

namespace XamarinApp.Services
{
    public class OsrmRouteService
    {
        private readonly string baseRouteUrl = "https://router.project-osrm.org/route/v1/driving/";
        private HttpClient _httpClient;
        public OsrmRouteService()
        {
            _httpClient = new HttpClient();
        }
        public async Task<DirectionResponse> GetDirectionResponseAsync(string origin, string destination)
        {
            var originLocations = await Geocoding.GetLocationsAsync(origin);
            var originLocation = originLocations?.FirstOrDefault();

            var destinationLocations = await Geocoding.GetLocationsAsync(destination);
            var destinationLocation = destinationLocations?.FirstOrDefault();

            if (originLocation == null || destinationLocation == null)
            {
                return null;
            }
            if (originLocation != null || destinationLocation != null)
            {
                string url = string.Format(baseRouteUrl) + $"{originLocation.Longitude},{originLocation.Latitude};" +
                    $"{destinationLocation.Longitude},{destinationLocation.Latitude}?overview=full&geometries=polyline&steps=false";

                var response = await _httpClient.GetAsync(url);
                if (response.IsSuccessStatusCode)
                {
                    var json = await response.Content.ReadAsStringAsync();
                    var result = JsonConvert.DeserializeObject<DirectionResponse>(json);
                    return result;
                }
            }
            else
            {
                return null;
            }
            return null;
        }
    }
}
