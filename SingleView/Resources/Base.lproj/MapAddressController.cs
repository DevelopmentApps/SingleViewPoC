using Foundation;
using System;
using System.CodeDom.Compiler;
using UIKit;

using Google.Maps;
using CoreLocation;
using System.Text;

namespace SingleView
{
    partial class MapAddressController : UIViewController
    {
        CLLocationManager _location;

        Address _googleMapsAddress;

        public MapAddressController(IntPtr handle) : base(handle)
        {
            _location = new CLLocationManager();
            

            _googleMapsAddress = new Address();
        }

        public override void ViewDidLoad()
        {
            base.ViewDidLoad();

            //_location.RequestWhenInUseAuthorization();
            //_location.StartUpdatingLocation();


            // _location.LocationsUpdated += _location_LocationsUpdated;

           

            Google.Maps.Geocoder currentGeo = new Geocoder();

            //_location.RequestLocation();

            currentGeo.ReverseGeocodeCord(_location.Location.Coordinate,ShowAddress);
        }

        private void ShowAddress(ReverseGeocodeResponse response, NSError error)
        {
            if (error == null)
            {
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
            }
        }

        
       

        //private void _location_LocationsUpdated(object sender, CLLocationsUpdatedEventArgs e)
        //{
        //    foreach (var item in e.Locations)
        //    {
        //        lblLatitude.Text = System.Convert.ToString(item.Coordinate.Latitude);
        //        lblLongitude.Text = System.Convert.ToString(item.Coordinate.Longitude);
        //    }
        //}
    }
}
