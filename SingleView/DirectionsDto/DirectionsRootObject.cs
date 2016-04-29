using System;
using System.Collections.Generic;
using System.Text;

namespace SingleView.DirectionsDto
{
    public class DirectionsRootObject
    {
        public List<Route> routes { get; set; }
        public string status { get; set; }
    }
}
