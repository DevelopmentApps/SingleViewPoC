using System;
using System.Collections.Generic;
using System.Text;

using Google.Maps;
using CoreLocation;
using System.Globalization;
using Newtonsoft.Json;
using SingleView.DirectionsDto;
using UIKit;

namespace SingleView
{
    public class MapViewDirectionsDelegate : MapViewDelegate
    {
       
        const string googleDirectionsApiUrl = @"http://maps.googleapis.com/maps/api/directions/json?origin=";

       
        private readonly MapView _map;
        private CLLocationCoordinate2D _origin;
        private CLLocationCoordinate2D _destination;

        public readonly List<Google.Maps.Polyline> Lines;            

        public MapViewDirectionsDelegate(MapView map,CLLocationCoordinate2D origin ,CLLocationCoordinate2D destination)
        {
           
            Lines = new List<Google.Maps.Polyline>();

            _map = map;
            _origin = origin;
            _destination = destination;
           

            var originMarker = new Marker { Position = origin, Map = _map };
            var destinationMarker = new Marker { Position = destination, Map = _map};

            PrepareDirectionsRestQuery();
        }

        //public override void DidTapAtCoordinate(MapView mapView, CLLocationCoordinate2D coordinate)
        //{

        //    //Create/Add Marker 
        //    var marker = new Marker { Position = coordinate, Map = mapView };
        //    Locations.Add(coordinate);

        //    if (Locations.Count > 1)
        //    {
        //        SetDirectionsQuery();
        //    }
        //}


        private void PrepareDirectionsRestQuery()
        {
           
            if (Lines.Count > 0)
            {
                foreach (var line in Lines)
                {
                    line.Map = null;
                }
                Lines.Clear();
            }

            //Start building Directions URL
            var sb = new System.Text.StringBuilder();
            sb.Append(googleDirectionsApiUrl);
            sb.Append(System.Convert.ToString(_origin.Latitude));
            sb.Append(",");
            sb.Append(System.Convert.ToString(_origin.Longitude));
            sb.Append("&");
            sb.Append("destination=");
            sb.Append(System.Convert.ToString(_destination.Latitude));
            sb.Append(",");
            sb.Append(System.Convert.ToString(_destination.Longitude));
            sb.Append("&sensor=true");

           

            //Get directions through Google Web Service
            var directionsTask = ConsultAndGetDirections(sb.ToString());

            var jSonData = directionsTask;

            //Deserialize string to object
            var routes = JsonConvert.DeserializeObject<DirectionsRootObject>(jSonData);
           

            foreach (var route in routes.routes)
            {
                //Encode path from polyline passed back
                var path = Path.FromEncodedPath(route.overview_polyline.points);

                //Create line from Path
                var line = Google.Maps.Polyline.FromPath(path);
                line.StrokeWidth = 10f;
                line.StrokeColor = UIColor.Red;
                line.Geodesic = true;

                //Place line on map
                line.Map = _map;
                Lines.Add(line);

            }

        }

        private String ConsultAndGetDirections(string url)
        {
            var client = new RestSharp.RestClient(url);

            RestSharp.RestRequest request = new RestSharp.RestRequest();

            var response = client.Execute(request);

            string directions = response.Content;

            return directions;

        }

    }
}
