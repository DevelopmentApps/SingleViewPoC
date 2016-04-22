using Foundation;
using System;
using System.CodeDom.Compiler;
using UIKit;
using System.Text;
using System.Threading.Tasks;
using CoreLocation;
using Google.Maps;
using Google.MobileAds;
using CoreGraphics;

namespace SingleView
{
    partial class MapAddressController : UIViewController
    {
        int _numOfCalls = 0;

        CLLocationManager _location;

        Address _googleMapsAddress;
            
        LoadingOverlay _loadingOverlay;

       

       

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

        private void GetCurrentUserLocation()
        {

            ////This code is for real update location
            //_location.StartUpdatingLocation();
            //Google.Maps.Geocoder currentGeo = new Geocoder();
            //currentGeo.ReverseGeocodeCord(_location.Location.Coordinate, ShowAddress);
            //_location.StopUpdatingLocation();


            ////This code is for fake location
            ////This is Washington DC
            //CLLocationCoordinate2D fakeLocation = new CLLocationCoordinate2D(38.910486, -77.039297);
            ////This is puebla city and a neighborhood
            //CLLocationCoordinate2D fakeLocation = new CLLocationCoordinate2D(19.040667, -98.212204);
            ////Queretaro City and a neighborhood
            //CLLocationCoordinate2D fakeLocation = new CLLocationCoordinate2D(20.638512, -100.111806);
            ////Baja California Sur
            CLLocationCoordinate2D fakeLocation = new CLLocationCoordinate2D(24.143993, -110.316417);
            Google.Maps.Geocoder currentGeo = new Geocoder();
            currentGeo.ReverseGeocodeCord(fakeLocation, ShowAddress);
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

                lblCountry.Text = _googleMapsAddress.Country + " Country";
                lblAdministrativeArea.Text = _googleMapsAddress.AdministrativeArea + " AdminArea";
                lblLocality.Text = _googleMapsAddress.Locality + " Locality";
                lblSublocality.Text = _googleMapsAddress.SubLocality + " SubLocal";
                lblPostalCode.Text = _googleMapsAddress.PostalCode + " Zip";
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
