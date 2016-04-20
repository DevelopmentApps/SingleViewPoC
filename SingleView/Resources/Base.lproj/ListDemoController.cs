using Foundation;
using System;
using System.CodeDom.Compiler;
using UIKit;

using ObjCRuntime;
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
            _location.RequestWhenInUseAuthorization();
            myMap = new MapView();
           
            
        }

        public override void ViewDidAppear(bool animated)
        {
            base.ViewDidAppear(animated);
            _location.StartUpdatingLocation();


            var myCamera = CameraPosition.FromCamera(_location.Location.Coordinate, zoom: 17);
            myMap = MapView.FromCamera(RectangleF.Empty, myCamera);

            View = myMap;

            myMap.Settings.MyLocationButton = true;
            myMap.MyLocationEnabled = true;

            _location.StopUpdatingLocation();

        }

        public override void ViewDidLoad()
        {
            base.ViewDidLoad();

          

            
           



        }

      



    }
}
