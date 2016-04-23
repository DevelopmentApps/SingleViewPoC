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
           
                base.ViewDidAppear(animated);

            //FilterPlaces();
            GetGooglePlacesPicker();
            //GetCurrentPlace();




        }

        private void GetCurrentPlace()
        {
            PlacesClient client = new PlacesClient();

            _location.StartUpdatingLocation();

            client.CurrentPlace(CurrentPlace);
        }

        private void CurrentPlace(PlaceLikelihoodList likelihoodList, NSError error)
        {
            if (error == null && likelihoodList != null)
            {
                foreach (var item in likelihoodList.Likelihoods)
                {
                    var currentPlace = item.Place;
                    lblPlace.Text = currentPlace.Name;

                }
            }
            else if (error != null)
            {
                lblErrorToShow.Text = error.LocalizedDescription;
            }
        }

        private void FilterPlaces()
        {
            AutocompleteFilter filter = new AutocompleteFilter();
            PlacesClient client = new PlacesClient();

            _location.StartUpdatingLocation();

            filter.Type = PlacesAutocompleteTypeFilter.City;
            CLLocationCoordinate2D NorthWest = new CLLocationCoordinate2D(_location.Location.Coordinate.Latitude + 0.001, _location.Location.Coordinate.Longitude + 0.001);
            CLLocationCoordinate2D SouthWest = new CLLocationCoordinate2D(_location.Location.Coordinate.Latitude - 0.001, _location.Location.Coordinate.Longitude - 0.001);

       
            client.AutocompleteQuery("Insurgen", new CoordinateBounds(NorthWest, SouthWest), filter, FilterResult);

            _location.StopUpdatingLocation();
        }

        private void FilterResult(AutocompletePrediction[] results, NSError error)
        {
            if (error == null && results != null)
            {
                foreach (var item in results)
                {
                 
                  
                }
            }
            else if (error != null)
            {
                lblErrorToShow.Text = error.LocalizedDescription;
            }
        }

        private void GetGooglePlacesPicker()
        {
            if (_location.Location != null)
            {

                _location.DesiredAccuracy = CLLocation.AccurracyBestForNavigation;
                _location.DistanceFilter = CLLocationDistance.FilterNone;

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
            lblPlace.Text = string.Empty;
            lblAddress.Text = string.Empty;
            lblErrorToShow.Text = string.Empty;
        }
    }
}
