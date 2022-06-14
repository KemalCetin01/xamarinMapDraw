using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using XamarinApp.Services;

namespace XamarinApp.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class DetectChangedLocation : ContentPage
    {
        ILocationUpdateService loc;
        public DetectChangedLocation()
        {
            InitializeComponent();
        }

        protected override void OnAppearing()
        {
            base.OnAppearing();
            loc = DependencyService.Get<ILocationUpdateService>();
            loc.LocationChanged += (object sender, ILocationEventArgs args) => {
                lat.Text = args.Latitude.ToString();
                lng.Text = args.Longitude.ToString();
            };
            loc.getUserLocation();
        }
        protected override void OnDisappearing()
        {
            base.OnDisappearing();
            ILocationEventArgs args = null ;
            loc = null;
        }
    }
}