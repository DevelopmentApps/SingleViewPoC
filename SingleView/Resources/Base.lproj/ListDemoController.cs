using Foundation;
using System;
using System.CodeDom.Compiler;
using UIKit;

using ObjCRuntime;
using Google.Maps;
using System.Drawing;
using CoreLocation;
using System.Threading.Tasks;

namespace SingleView
{
	partial class ListDemoController : UIPageViewController
	{
        MapView myMap;

        CLLocationManager _location;

        LoadingOverlay _loadingDemo;

        public ListDemoController (IntPtr handle) : base (handle)
		{
            _location = new CLLocationManager();
            _location.RequestWhenInUseAuthorization();
            myMap = new MapView();
           
            
        }

        public override void ViewDidAppear(bool animated)
        {
            base.ViewDidAppear(animated);

            _location.DesiredAccuracy = CLLocation.AccurracyBestForNavigation;
            _location.DistanceFilter = CLLocationDistance.FilterNone;

            var bounds = UIScreen.MainScreen.Bounds;
            _loadingDemo = new LoadingOverlay(bounds);
            View.Add(_loadingDemo);
            View.BringSubviewToFront(_loadingDemo);

            Task.Factory.StartNew(() => { InvokeOnMainThread(() => ShowLocationOnMap()); }).ContinueWith(t =>
            {
                InvokeOnMainThread(() => _loadingDemo.Hide());
            });

            if (_loadingDemo != null)
            {
                _loadingDemo.Dispose();
            }

           

        }

        private void ShowLocationOnMap()
        {
            _location.StartUpdatingLocation();


            var myCamera = CameraPosition.FromCamera(_location.Location.Coordinate, zoom: 17);
            myMap = MapView.FromCamera(RectangleF.Empty, myCamera);

            View = myMap;

            myMap.Settings.MyLocationButton = true;
            myMap.MyLocationEnabled = true;

            _location.StopUpdatingLocation();
        }

      



    }
}
