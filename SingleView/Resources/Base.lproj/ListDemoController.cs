using Foundation;
using System;
using System.CodeDom.Compiler;
using UIKit;

using Google.Maps;
using System.Drawing;

namespace SingleView
{
	partial class ListDemoController : UIPageViewController
	{
        MapView myMap;

		public ListDemoController (IntPtr handle) : base (handle)
		{
           
		}

        public override void LoadView()
        {
            myMap.MyLocationEnabled = true;
            var myCamera = CameraPosition.FromCamera(myMap.MyLocation.Coordinate, zoom:6);
            myMap = MapView.FromCamera(RectangleF.Empty, myCamera);
            View = myMap;
        }

        public override void ViewWillAppear(bool animated)
        {
            base.ViewWillAppear(animated);
            
           
        }

        public override void ViewDidLoad()
        {
            base.ViewDidLoad(); 
            
        }
    }
}
