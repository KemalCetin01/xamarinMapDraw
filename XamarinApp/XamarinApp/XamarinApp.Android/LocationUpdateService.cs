using Android.App;
using Android.Content;
using Android.Locations;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using XamarinApp.Droid;
using XamarinApp.Services;

[assembly: Xamarin.Forms.Dependency(typeof(LocationUpdateService))]
namespace XamarinApp.Droid
{
    public class LocationEventArgs : ILocationEventArgs
    {
        public double Latitude { get; set; }
        public double Longitude { get; set; }
    }
    public class LocationUpdateService : Java.Lang.Object, ILocationUpdateService, ILocationListener
    {
        LocationManager locationManager;
        public event EventHandler<ILocationEventArgs> LocationChanged;
        event EventHandler<ILocationEventArgs> ILocationUpdateService.LocationChanged
        {
            add
            {
                LocationChanged += value;
            }
            remove
            {
                LocationChanged -= value;

            }
        }
        public void getUserLocation()
        {
            locationManager = (LocationManager)MainActivity.Context.GetSystemService(Context.LocationService);
            locationManager.RequestLocationUpdates(LocationManager.GpsProvider, 0, 0, this);
        }
        ~LocationUpdateService()
        {
            locationManager.RemoveUpdates(this);
        }
        public void OnLocationChanged(Location location)
        {
            if (location != null)
            {
                LocationEventArgs args = new LocationEventArgs
                {
                    Latitude = location.Latitude,
                    Longitude = location.Longitude
                };
                LocationChanged(this, args);
            }
        }
    
        public void OnProviderDisabled(string provider)
        {
            throw new NotImplementedException();
        }

        public void OnProviderEnabled(string provider)
        {
            throw new NotImplementedException();
        }

        public void OnStatusChanged(string provider, [GeneratedEnum] Availability status, Bundle extras)
        {
            throw new NotImplementedException();
        }
    }
}