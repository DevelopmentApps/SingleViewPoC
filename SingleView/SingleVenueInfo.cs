using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using PetWee.ThirdParty.Foursquare.Dto;
using System;
using System.Collections.Generic;
using System.Text;

namespace SingleView
{
    public class SingleVenueInfo
    {
        private string _clientId;
        private string _clientSecret;
        private string _versioningDate;
        private string _responseFormat;

        public SingleVenueResponse GetIndividualVenueInformation(string venueId, string language)
        {

            if (!string.IsNullOrWhiteSpace(language))
            {
                language.Substring(0, 2);
            }
            else if (string.IsNullOrWhiteSpace(language))
            {
                language = "en";
            }

            _clientId = "CKDMY5SDLOIDUBWPGAY1VAW1HWVXPZXK2RBFB4HAFO5DYHT2";
            _clientSecret = "SUUAZFZD1QHSYDMO0ECXR3E0W00S0PEYZORZHSVGXY3ODBDV";
            _versioningDate = "20160425";
            _responseFormat = "foursquare";

            var restClient = new RestSharp.RestClient("https://api.foursquare.com/v2/venues/" + venueId);

            var request = new RestSharp.RestRequest();
            request.AddParameter("client_id", _clientId);
            request.AddParameter("client_secret", _clientSecret);
            request.AddParameter("v", _versioningDate);
            request.AddParameter("m", _responseFormat);
            request.AddHeader("Accept-Language", language);

            var response = restClient.Execute(request);

            JObject jsonSerializer = JObject.Parse(response.Content);

            SingleVenueResponse parsedObject = JsonConvert.DeserializeObject<SingleVenueResponse>(jsonSerializer["response"].ToString());

            return parsedObject;
        }
    }
}
