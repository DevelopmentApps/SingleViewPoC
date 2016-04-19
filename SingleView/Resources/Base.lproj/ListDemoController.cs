using Foundation;
using System;
using System.CodeDom.Compiler;
using UIKit;

using Google.Maps;
using System.Drawing;
using CoreLocation;

namespace SingleView
{
	partial class ListDemoController : UIPageViewController
	{
        MapView myMap;

        CLLocationManager _location;


        public ListDemoController (IntPtr handle) : base (handle)
		{
            _location = new CLLocationManager();
            myMap = new MapView();
            _location.DesiredAccuracy = CoreLocation.CLLocation.AccurracyBestForNavigation;
            
        }

             
        public override void ViewDidLoad()
        {
            base.ViewDidLoad();

           
            _location.RequestWhenInUseAuthorization();



            //var myCamera = CameraPosition.FromCamera(latitude: 7.2935273,
            //    longitude: 80.6387523,
            //    zoom: 13);


            var myCamera = CameraPosition.FromCamera(_location.Location.Coordinate, zoom: 13);
            myMap = MapView.FromCamera(RectangleF.Empty, myCamera);

            View = myMap;

            myMap.Settings.MyLocationButton = true;
            myMap.MyLocationEnabled = true;

            
          
         

            
        }
    }
}
