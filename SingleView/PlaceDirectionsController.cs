
using System;
using System.Drawing;
using System.CodeDom.Compiler;

using UIKit;
using Foundation;
using CoreLocation;

using Google.Maps;


namespace SingleView
{
	partial class PlaceDirectionsController : UIViewController
	{
        MapView _mapView;
        MapViewDirectionsDelegate _mapDelegate;
        CLLocationManager _location;

        public double Latitude { get; set; }
        public double Longitude { get; set; }

        public PlaceDirectionsController (IntPtr handle) : base (handle)
		{
            _location = new CLLocationManager();
            _location.RequestWhenInUseAuthorization();
            _mapView = new MapView();
        }
               
        public override void ViewWillAppear(bool animated)
        {
            base.ViewWillAppear(animated);

           

            _location.DesiredAccuracy = CLLocation.AccurracyBestForNavigation;
            _location.DistanceFilter = CLLocationDistance.FilterNone;

            ShowLocationOnMap();
        }

        public override void ViewWillDisappear(bool animated)
        {
            base.ViewWillDisappear(animated);

            _mapView.Clear();
            _mapDelegate.Lines.Clear();
            
           
        }

        private void ShowLocationOnMap()
        {
            _location.StartUpdatingLocation();

            var myCamera = CameraPosition.FromCamera(_location.Location.Coordinate, zoom: 12);

            _mapView = MapView.FromCamera(RectangleF.Empty, myCamera); 
            _mapView.MyLocationEnabled = true;
            _mapView.Settings.SetAllGesturesEnabled(true);

            CLLocationCoordinate2D destination;
            destination.Latitude = Latitude;
            destination.Longitude = Longitude;

            _mapDelegate = new MapViewDirectionsDelegate(_mapView, _location.Location.Coordinate,destination);
            _mapView.Delegate = _mapDelegate;

            _location.StopUpdatingLocation();

            View = _mapView;                      
        }
    }
}
