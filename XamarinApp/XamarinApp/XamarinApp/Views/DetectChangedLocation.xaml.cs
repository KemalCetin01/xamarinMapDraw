using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using XamarinApp.Models;
using XamarinApp.Services;
using XamarinApp.Services.ApiServices;

namespace XamarinApp.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class DetectChangedLocation : ContentPage
    {
        private static string tempLocation;
        private KonumApiService _konumApiService;
        private const int sleepTime= 1800; //180000 3dk
        ILocationUpdateService loc;
        public DetectChangedLocation()
        {
            InitializeComponent();
            _konumApiService = new KonumApiService();
        }

        protected override void OnAppearing()
        {
            base.OnAppearing();
            loc = DependencyService.Get<ILocationUpdateService>();
            loc.LocationChanged += LocationChangeHandler;
            //loc.LocationChanged += (object sender, ILocationEventArgs args) =>
            //{
            //    lat.Text = args.Latitude.ToString();
            //    lng.Text = args.Longitude.ToString();

            //    //var locationsTemp = awat _konumApiService.getTodayByUserId(1);
            //};
            //{
            //    lat.Text = args.Latitude.ToString();
            //    lng.Text = args.Longitude.ToString();
            //    //if (tempLocation != (args.Latitude.ToString() + '-' + args.Longitude.ToString()))
            //    //{
            //    //    tempLocation = (args.Latitude.ToString() + '-' + args.Longitude.ToString());
            //    //     LocationModel konumModel = new LocationModel();
            //    //    konumModel.LatLongt = tempLocation;
            //    //    konumModel.UserID = 1;
            //    //    //konumModel.createTime = DateTime.Now;
            //    //    konumModel.createTime = DateTime.Today;
            //    //    var result = await _konumApiService.AddAsync(konumModel);
            //    //}
            //};

            loc.getUserLocation();
        }
        public void LocationChangeHandler(object sender, ILocationEventArgs args)
        {
            try
            {
                LocationModel konumModel = new LocationModel();
                konumModel.UserID = 1;

                var convLat = Math.Round(args.Latitude, 5).ToString(); 
                var convLongt = Math.Round(args.Longitude, 5).ToString();
               // System.Threading.Thread.Sleep(sleepTime);
                if (tempLocation != (convLat + '-' + convLongt + '-'+konumModel.UserID.ToString()))
                {
                    lat.Text = args.Latitude.ToString();
                    lng.Text = args.Longitude.ToString();
                    tempLocation = (convLat + '-' + convLongt);
                    konumModel.LatLongt = tempLocation;
                    konumModel.createTime = DateTime.Today;
                    var result = _konumApiService.AddAsync(konumModel);

                    tempLocation +='-'+ konumModel.UserID.ToString(); //user bağımsız konum değişimi
                }
            }
            catch (Exception ex)
            {

                throw ex;
            }

        }

        protected override void OnDisappearing()
        {
            base.OnDisappearing();
            ILocationEventArgs args = null;
            loc = null;
        }
    }
}