using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;
using Xamarin.Forms.Maps;
using XamarinApp.Models;
using XamarinApp.Services;
using XamarinApp.Services.ApiServices;

namespace XamarinApp.ViewModels
{
   public class RouteViewModel:MyViewModel
    {
        private string _origin;

    
        public string Origin
        {
            get { return _origin; }
            set { _origin = value; OnPropertyChanged(); }
        }

        private string _destination;
        public string Destination
        {
            get { return _destination; }
            set { _destination = value; OnPropertyChanged(); }
        }
        
        private double _routeDuration;
        public double RouteDuration
        {
            get { return _routeDuration; }
            set { _routeDuration = value; OnPropertyChanged(); }
        }
        
        private double _routeDistance;
        public double RouteDistance
        {
            get { return _routeDistance; }
            set { _routeDistance = value; OnPropertyChanged(); }
        }
        
        private double _fare;
        public double Fare
        {
            get { return _fare; }
            set { _fare = value; OnPropertyChanged(); }
        }
        
        bool _showRouteDetails;
        public bool ShowRouteDetails
        {
            get { return _showRouteDetails; }
            set { _showRouteDetails = value; OnPropertyChanged(); }
        }

        public static Map map;
        public Command GetRouteCommand { get; }
        private OsrmRouteService services;
        private DirectionResponse dr;
        private KonumApiService _konumApiService;
        public RouteViewModel()
        {
            ShowRouteDetails = false;
            map = new Map();
            services = new OsrmRouteService();
            dr = new DirectionResponse();
            GetRouteCommand = new Command(async()=>await loadRouteASync(Origin,Destination));
            _konumApiService = new KonumApiService();
        }

        public async Task loadRouteASync(string origin,string destination)
        {

            var current = Xamarin.Essentials.Connectivity.NetworkAccess;
            
            if (current!=Xamarin.Essentials.NetworkAccess.Internet)
            {
                await DisplayAlert("Error","You Must be connected internet","Ok");
                return;
            }
            if (origin==null||destination==null)
            {
                await DisplayAlert("Error", "Origin or destination must not be empty", "Ok");
                return;
            }

            IsBusy = true;
            List<Route> routes = new List<Route>();
            List<Waypoint> wayPoints = new List<Waypoint>();
            List<LatLong> locations = new List<LatLong>();

            LocationModel konumModel = new LocationModel();

            konumModel.UserID = 1;
            konumModel.createTime = DateTime.Now;
            var locationsTemp = await _konumApiService.getTodayByUserId(konumModel.UserID);

            dr = await services.GetDirectionResponseAsync(locationsTemp.First().LatLongt, locationsTemp.Last().LatLongt,true);
            if (dr!=null)
            {
                ShowRouteDetails = false;
                await Task.Delay(100);
                routes = dr.Routes.ToList();
                wayPoints = dr.Waypoints.ToList();
                RouteDuration = Math.Round((Double)routes[0].Duration/60,0);

                RouteDistance = Math.Round((Double)routes[0].Distance/160,1);

                Fare = Math.Round((Double)RouteDistance*1.25, 2);

                if (Fare <= 6) { Fare = 6; }

                locations = DecodePolylinePoints(routes[0].Geometry.ToString());

                //var originLocations = await Xamarin.Essentials.Geocoding.GetLocationsAsync(origin);
                //var originLocation = originLocations?.FirstOrDefault();

                //var destinationLocations = await Xamarin.Essentials.Geocoding.GetLocationsAsync(destination);
                //var destinationLocation = destinationLocations?.FirstOrDefault();

                //var firstPinLocation = locations[0];
                //var lastPinLocation = locations[locations.Count()-1];


                var firstPinLocation = locationsTemp.First().LatLongt;
                var lastPinLocation = locationsTemp.Last().LatLongt;

                string[] splFirstPinLocation = firstPinLocation.Split('-');
                Double firstPinLocation_latitude = Convert.ToDouble(splFirstPinLocation[0]);
                Double firstPinLocation_longitude = Convert.ToDouble(splFirstPinLocation[1]);

                string[] splLastPinLocation = lastPinLocation.Split('-');
                Double LastPinLocation_latitude = Convert.ToDouble(splLastPinLocation[0]);
                Double LastPinLocation_longitude = Convert.ToDouble(splLastPinLocation[1]);


                Pin originPin = new Pin
                {
                    Label = "Origin",
                    Address = Origin,
                    Type = PinType.Place,
                    Position = new Position(firstPinLocation_latitude, firstPinLocation_longitude)
                };
                map.Pins.Add(originPin);

                Pin destinationPin = new Pin
                {
                    Label = "Destination",
                    Address = Destination,
                    Type = PinType.Place,
                    Position = new Position(LastPinLocation_latitude, LastPinLocation_longitude)
                };
                map.Pins.Add(destinationPin);

                map.HasZoomEnabled = true;
                map.HasScrollEnabled = true;
                MapSpan mapSpan = MapSpan.FromCenterAndRadius(new Position(firstPinLocation_latitude, firstPinLocation_longitude),Distance.FromKilometers(5));
                map.MoveToRegion(mapSpan);
                // instantiate a polyline
                Polyline animatedPolyline = new Polyline
                {
                    StrokeColor = Color.Black,
                    StrokeWidth = 7,
                };

               
                foreach (var item in locationsTemp)
                {
                    string[] spllocationTemp = item.LatLongt.Split('-');
                    animatedPolyline.Geopath.Add(new Position(Convert.ToDouble(spllocationTemp[0]), Convert.ToDouble(spllocationTemp[1])));
                    map.MapElements.Add(animatedPolyline);

                }
                //locationsTemp.First().LatLongt();
                //foreach (var location in locations)
                //{
                //    animatedPolyline.Geopath.Add(new Position(location.Lat, location.Lng));
                //    // add the polyline to the map's MapElements collection

                //    map.MapElements.Add(animatedPolyline);

                //    LocationModel konumModel = new LocationModel();
                //    konumModel.LatLongt = location.Lat + "-" + location.Lng;
                //    konumModel.UserID = 1;
                //    //konumModel.createTime = DateTime.Now;
                //    konumModel.createTime = DateTime.Today;
                //    var result =  _konumApiService.AddAsync(konumModel);
                //}


                ShowRouteDetails = true;
                IsBusy = false;
            }
        }

        private List<LatLong> DecodePolylinePoints(string encodedPoints)
        {
            if (encodedPoints == null || encodedPoints == "") return null;
            List<LatLong> poly = new List<LatLong>();
            char[] polylinechars = encodedPoints.ToCharArray();
            int index = 0;

            int currentLat = 0;
            int currentLng = 0;
            int next5bits;
            int sum;
            int shifter;

            try
            {
                while (index < polylinechars.Length)
                {
                    // calculate next latitude
                    sum = 0;
                    shifter = 0;
                    do
                    {
                        next5bits = (int)polylinechars[index++] - 63;
                        sum |= (next5bits & 31) << shifter;
                        shifter += 5;
                    } while (next5bits >= 32 && index < polylinechars.Length);

                    if (index >= polylinechars.Length)
                        break;

                    currentLat += (sum & 1) == 1 ? ~(sum >> 1) : (sum >> 1);

                    //calculate next longitude
                    sum = 0;
                    shifter = 0;
                    do
                    {
                        next5bits = (int)polylinechars[index++] - 63;
                        sum |= (next5bits & 31) << shifter;
                        shifter += 5;
                    } while (next5bits >= 32 && index < polylinechars.Length);

                    if (index >= polylinechars.Length && next5bits >= 32)
                        break;

                    currentLng += (sum & 1) == 1 ? ~(sum >> 1) : (sum >> 1);
                    LatLong p = new LatLong();
                    p.Lat = Convert.ToDouble(currentLat) / 100000.0;
                    p.Lng = Convert.ToDouble(currentLng) / 100000.0;
                    poly.Add(p);
                }
            }
            catch (Exception ex)
            {
                // logo it
            }
            return poly;
        }
    }
}
