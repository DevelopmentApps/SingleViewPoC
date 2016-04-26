using CoreLocation;
using Foundation;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using PetWee.ThirdParty.Foursquare.Dto;
using System;
using System.CodeDom.Compiler;
using System.Collections.Generic;
using System.Text;
using UIKit;

namespace SingleView
{
	partial class FoursquareVenueController : UIViewController
	{
        private string _clientId;
        private string _clientSecret;
        private string _versioningDate;
        private string _responseFormat;

        CLLocationManager _location;

        private UIWindow _window;

		public FoursquareVenueController (IntPtr handle) : base (handle)
		{
            _location = new CLLocationManager();
            _location.RequestWhenInUseAuthorization();

            _window = new UIWindow(UIScreen.MainScreen.Bounds);
            
            _clientId = "CKDMY5SDLOIDUBWPGAY1VAW1HWVXPZXK2RBFB4HAFO5DYHT2";
            _clientSecret = "SUUAZFZD1QHSYDMO0ECXR3E0W00S0PEYZORZHSVGXY3ODBDV";
            _versioningDate = "20160425";
            _responseFormat = "foursquare";
        }

       
        public override void ViewWillAppear(bool animated)
        {
            base.ViewWillAppear(animated);

            if (_location.Location != null)
            {

                _location.DesiredAccuracy = CLLocation.AccurracyBestForNavigation;
                _location.DistanceFilter = CLLocationDistance.FilterNone;
                _location.StartUpdatingLocation();

                StringBuilder currentLocation = new StringBuilder();
                currentLocation.Append(System.Convert.ToString(_location.Location.Coordinate.Latitude));
                currentLocation.Append(",");
                currentLocation.Append(System.Convert.ToString(_location.Location.Coordinate.Longitude));

                SearchVenues(currentLocation.ToString());

                _location.StopUpdatingLocation();

            }
            else if (_location.Location == null)
            {
                lblError.Text = "location services are not enabled for this App.";
                ClearLabels();
            }
            
        }

        

        private void SearchVenues(string location)
        {
            

            var restClient = new RestSharp.RestClient("https://api.foursquare.com/v2/venues/search");
            
            var request = new RestSharp.RestRequest();
            //request.AddParameter("ll", "19.369084, -99.179388");
            request.AddParameter("ll", location);
            request.AddParameter("limit", 50);
            request.AddParameter("query", "pet friendly");
            request.AddParameter("client_id", _clientId);
            request.AddParameter("client_secret", _clientSecret);
            request.AddParameter("v", _versioningDate);
            request.AddParameter("m", _responseFormat);
            var response = restClient.Execute(request);

            JObject jsonSerializer = JObject.Parse(response.Content);

            var parsedObject = JsonConvert.DeserializeObject<VenueResponse>(jsonSerializer["response"].ToString());

            tblView.Source = new RootTableSource(parsedObject.Venues.ToArray());
        }

        private void ClearLabels()
        {
            lblError.Text = string.Empty;
        }
    }
}
