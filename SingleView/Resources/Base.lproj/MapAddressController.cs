using Foundation;
using System;
using System.CodeDom.Compiler;
using UIKit;

using Google.Maps;
using CoreLocation;
using System.Text;
using System.Threading.Tasks;

namespace SingleView
{
    partial class MapAddressController : UIViewController
    {
        int _numOfCalls = 0;

        CLLocationManager _location;

        Address _googleMapsAddress;

        LoadingOverlay _loadingOverlay;

        UIView []_views;

        public MapAddressController(IntPtr handle) : base(handle)
        {
            _location = new CLLocationManager();
            _location.RequestWhenInUseAuthorization();
            _googleMapsAddress = new Address();

          

        }



        public override void ViewDidAppear(bool animated)
        {
            base.ViewDidAppear(animated);




            if (_location.Location != null)
            {

                _location.DesiredAccuracy = CLLocation.AccurracyBestForNavigation;
                _location.DistanceFilter = CLLocationDistance.FilterNone;

                var bounds = UIScreen.MainScreen.Bounds;
                _loadingOverlay = new LoadingOverlay(bounds);

                View.Add(_loadingOverlay);
                View.BringSubviewToFront(_loadingOverlay);


                _views = View.Subviews;
                Task.Factory.StartNew(() => { InvokeOnMainThread(() => GetCurrentUserLocation()); }).ContinueWith(t => 
                {
                   InvokeOnMainThread( () => _loadingOverlay.Hide());
                });

                //Task.Factory.StartNew(() =>  GetCurrentUserLocation() ).ContinueWith(t => { _loadingOverlay.Hide(); }, TaskScheduler.FromCurrentSynchronizationContext());


                _loadingOverlay.Dispose();

            }
            else if (_location.Location ==null)
            {
                lblError.Text = "location services are not enabled for this App.";
                ClearLabels();
            }

           lblNumCalls.Text = System.Convert.ToString(_numOfCalls += 1);
        }

        public override void ViewWillDisappear(bool animated)
        {
            base.ViewWillDisappear(animated);
            
        }

        private void GetCurrentUserLocation()
        {
       

            _location.StartUpdatingLocation();
            Google.Maps.Geocoder currentGeo = new Geocoder();
            currentGeo.ReverseGeocodeCord(_location.Location.Coordinate, ShowAddress);
            _location.StopUpdatingLocation();
        }

        private void ShowAddress(ReverseGeocodeResponse response, NSError error)
        {
            if (error == null)
            {
                lblError.Text = string.Empty;
                lblErrorDomain.Text = string.Empty;
                StringBuilder fullAddress = new StringBuilder();

                _googleMapsAddress = response.FirstResult;

                lblLatitude.Text = System.Convert.ToString(response.FirstResult.Coordinate.Latitude);
                lblLongitude.Text = System.Convert.ToString(response.FirstResult.Coordinate.Longitude);

                lblCountry.Text = _googleMapsAddress.Country;
                lblAdministrativeArea.Text = _googleMapsAddress.AdministrativeArea;
                lblLocality.Text = _googleMapsAddress.Locality;
                lblSublocality.Text = _googleMapsAddress.SubLocality;
                lblPostalCode.Text = _googleMapsAddress.PostalCode;
                lblThoroughfare.Text = _googleMapsAddress.Thoroughfare + " Thorough";

                foreach (var item in response.FirstResult.Lines)
                {
                    fullAddress.Append(item).Append(" ");
                }

                fullAddress.Length--;

                lblAddressLines.Text = fullAddress.ToString() + " full";
            }

            else if (error != null)
            {
                lblError.Text = error.LocalizedDescription;
                lblErrorDomain.Text = error.Domain;
                ClearLabels();
            }
        }

        private void ClearLabels()
        {

            lblLatitude.Text = string.Empty;
            lblLongitude.Text = string.Empty;

            lblCountry.Text = string.Empty;
            lblAdministrativeArea.Text = string.Empty;
            lblLocality.Text = string.Empty;
            lblSublocality.Text = string.Empty;
            lblPostalCode.Text = string.Empty;
            lblThoroughfare.Text = string.Empty;
            lblAddressLines.Text = string.Empty;
        }





    }


}
