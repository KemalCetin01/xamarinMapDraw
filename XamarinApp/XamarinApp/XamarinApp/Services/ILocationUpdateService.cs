using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace XamarinApp.Services
{
   public interface ILocationUpdateService
    {
        void getUserLocation();
        event EventHandler<ILocationEventArgs> LocationChanged;
    }
    public interface ILocationEventArgs
    {
         double Latitude { get; set; }
         double Longitude { get; set; }
    }
}
