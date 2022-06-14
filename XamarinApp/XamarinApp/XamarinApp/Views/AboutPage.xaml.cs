using System;
using System.ComponentModel;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Xamarin.Essentials;
using Xamarin.Forms;
using Xamarin.Forms.Maps;
using Xamarin.Forms.Xaml;
using XamarinApp.ViewModels;

namespace XamarinApp.Views
{
    public partial class AboutPage : ContentPage
    {
        public AboutPage()
        {
            InitializeComponent();
           BindingContext = new RouteViewModel();
            RouteViewModel.map = map;

            DisplayCurLoc(); // validation içinde yapılacak
        }

        public async void DisplayCurLoc()
        {
            try
            {
                var request = new GeolocationRequest(GeolocationAccuracy.Medium, TimeSpan.FromSeconds(10));
                var location = await Geolocation.GetLocationAsync(request);
                if (location != null)
                {
                    Position p = new Position(location.Latitude,location.Longitude);
                    MapSpan mapSpan = MapSpan.FromCenterAndRadius(p,Distance.FromKilometers(.444));
                    map.MoveToRegion(mapSpan);
                    await GetLocationName(p);

                    Console.WriteLine($"Latitude: {location.Latitude}, Longitude: {location.Longitude}, Altitude: {location.Altitude}");
                }
            }
            catch (FeatureNotSupportedException fnsEx)
            {
                // Handle not supported on device exception
            }
            catch (FeatureNotEnabledException fneEx)
            {
                // Handle not enabled on device exception
            }
            catch (PermissionException pEx)
            {
                // Handle permission exception
            }
            catch (Exception ex)
            {
                // Unable to get location
            }
        }
        public async Task GetLocationName(Position position)
        {
            try
            {
                var placeMarks = await Geocoding.GetPlacemarksAsync(position.Latitude, position.Longitude);
                var placeMark = placeMarks?.FirstOrDefault();
                if (placeMark != null)
                {
                    //Origin.Text = string.Format($"{placeMark.FeatureName},{placeMark.SubAdminArea},{placeMark.PostalCode}");
                    Origin.Text = string.Format($"{placeMark.Thoroughfare},{" No: "+placeMark.SubThoroughfare},{placeMark.SubLocality},{placeMark.SubAdminArea},{placeMark.AdminArea},{placeMark.PostalCode}");
                }
                else
                {
                    Origin.Text = string.Empty;
                }
            }
            catch (Exception ex)
            {
                Debug.WriteLine("Kemal Hata: "+ ex.ToString());
            }

        }
    }
}