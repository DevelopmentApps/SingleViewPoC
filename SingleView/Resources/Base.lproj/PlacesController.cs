using System;
using System.CodeDom.Compiler;

using Foundation;

using UIKit;

using Google.Maps;
using CoreLocation;

namespace SingleView
{
	partial class PlacesController : UIViewController
	{
        CLLocationManager _location;

        Place _googlePlace;
        PlacePickerConfig _googlePlacePickerConfig;
        PlacePicker _googlePlacePicker;
            

		public PlacesController (IntPtr handle) : base (handle)
		{
            _location = new CLLocationManager();
            _location.RequestWhenInUseAuthorization();
            
        }

        public override void ViewDidAppear(bool animated)
        {
            if (_location.Location != null)
            {
                base.ViewDidAppear(animated);

                _location.DesiredAccuracy = CLLocation.AccurracyBestForNavigation;
                _location.DistanceFilter = CLLocationDistance.FilterNone;
                

                lblErrorToShow.Text = string.Empty;
                ClearLabels();

                

               
                CLLocationCoordinate2D NorthWest = new CLLocationCoordinate2D(_location.Location.Coordinate.Latitude + 0.001, _location.Location.Coordinate.Longitude + 0.001);
                CLLocationCoordinate2D SouthWest = new CLLocationCoordinate2D(_location.Location.Coordinate.Latitude - 0.001, _location.Location.Coordinate.Longitude - 0.001);

                _googlePlacePickerConfig = new PlacePickerConfig(new CoordinateBounds(NorthWest, SouthWest));
                _googlePlacePicker = new PlacePicker(_googlePlacePickerConfig);
                _googlePlacePicker.PrepareForInterfaceBuilder();
                _googlePlacePicker.PickPlaceWithCallback(ShowPickPlace);
            }
            else if (_location.Location == null)
            {
                lblErrorToShow.Text = "location services are not enabled for this App.";
                ClearLabels();
            }


        }

        private void ShowPickPlace(Place result, NSError error)
        {

            if (error == null && result != null)
            {
                lblPlace.Text = result.Name;
                lblAddress.Text = result.FormattedAddress;
                txtViewAttributions.AttributedText = result.Attributions;
                
            }
            else if (result == null)
            {
                lblErrorToShow.Text = "No place selected";            }
            else if (error != null)
            {
                lblErrorToShow.Text = error.LocalizedDescription;
                ClearLabels();
            }
        }

        private void ClearLabels()
        {

        }
    }
}
