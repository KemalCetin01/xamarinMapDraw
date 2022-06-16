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
        public async Task<DirectionResponse> GetDirectionResponseAsync(string origin, string destination,bool isGetLocations=false)
        {
            double originLatitude;
            double originLongitude;
            double destinationLatitude;
            double destinationLongitude;
            if (isGetLocations)
            {
                string[] splOrigin = origin.Split('-');
                originLatitude = Convert.ToDouble(splOrigin[0]);
                originLongitude = Convert.ToDouble(splOrigin[1]);
                string[] splDestination = destination.Split('-');
                destinationLatitude = Convert.ToDouble(splDestination[0]);
                destinationLongitude = Convert.ToDouble(splDestination[1]);
            }
            else
            {
                var originLocations = await Geocoding.GetLocationsAsync(origin);
                var originLocation = originLocations?.FirstOrDefault();
                originLatitude = originLocation.Latitude;
                originLongitude = originLocation.Longitude;

                var destinationLocations = await Geocoding.GetLocationsAsync(destination);
                var destinationLocation = destinationLocations?.FirstOrDefault();
                destinationLatitude = destinationLocation.Latitude;
                destinationLongitude = destinationLocation.Longitude;

                if (originLocation == null || destinationLocation == null)
                {
                    return null;
                }
            }


            if (originLatitude != 0 || originLongitude != 0 || destinationLatitude != 0 || destinationLongitude != 0)
            {
                string url = string.Format(baseRouteUrl) + $"{originLongitude},{originLatitude};" +
                    $"{destinationLongitude},{destinationLatitude}?overview=full&geometries=polyline&steps=false";

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
