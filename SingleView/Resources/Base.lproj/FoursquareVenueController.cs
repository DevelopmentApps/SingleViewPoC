using Foundation;
using PetWee.ThirdParty.Foursquare.Dto;
using System;
using System.CodeDom.Compiler;
using System.Collections.Generic;
using UIKit;

namespace SingleView
{
	partial class FoursquareVenueController : UIViewController
	{
        private string _clientId;
        private string _clientSecret;
        private string _versioningDate;
        private string _responseFormat;

		public FoursquareVenueController (IntPtr handle) : base (handle)
		{
            _clientId = "CKDMY5SDLOIDUBWPGAY1VAW1HWVXPZXK2RBFB4HAFO5DYHT2";
            _clientSecret = "SUUAZFZD1QHSYDMO0ECXR3E0W00S0PEYZORZHSVGXY3ODBDV";
            _versioningDate = "20160424";
            _responseFormat = "foursquare";
        }

        public override void ViewDidLoad()
        {
            base.ViewDidLoad();

            SearchVenues();
        }

        private void SearchVenues()
        {
            var restClient = new RestSharp.RestClient("https://api.foursquare.com/v2/venues/search");

            var request = new RestSharp.RestRequest();
            request.RootElement = "response";
            request.AddParameter("ll", "19.369084, -99.179388");
            request.AddParameter("client_id", _clientId);
            request.AddParameter("client_secret", _clientSecret);
            request.AddParameter("v",_versioningDate);
            request.AddParameter("m", _responseFormat);
            // var response = restClient.Execute(request);
            //var content = response.Content;

            //RestSharp.RestResponse<Venue> typedVenueRespone = (RestSharp.RestResponse<Venue>)restClient.Execute<Venue>(request);
            //var venuesList = typedVenueRespone.Data;

            var newResponse = restClient.Execute<Response>(request);
            var list = newResponse.Data.Venues;
           
          
           
        }
    }
}