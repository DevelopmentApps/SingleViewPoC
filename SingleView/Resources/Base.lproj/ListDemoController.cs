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
            myMap = new MapView();
        }

        public override void LoadView()
        {
            
            base.LoadView();
           
            var myCamera = CameraPosition.FromCamera(latitude: 7.2935273,
                longitude: 80.6387523,
                zoom: 13);
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

            myMap.Settings.MyLocationButton = true;
            myMap.MyLocationEnabled = true;
         

            
        }
    }
}
