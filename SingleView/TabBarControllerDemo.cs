using Foundation;
using System;
using System.CodeDom.Compiler;
using UIKit;

namespace SingleView
{
	partial class TabBarControllerDemo : UITabBarController
	{
       //ViewController _viewController;
        GogAdsController _adsController;
        FoursquareVenueController _venuesController;


		public TabBarControllerDemo (IntPtr handle) : base (handle)
		{
            _venuesController = Storyboard.InstantiateViewController("FoursquareVenueController") as FoursquareVenueController;

            _adsController = Storyboard.InstantiateViewController("GogAdsController") as GogAdsController;

            _venuesController.Title = "Main";
            _venuesController.TabBarItem = new UITabBarItem("Search", UIImage.FromFile("DailyBooth/tab_messages.png"), 0);

            _adsController.Title = "Ads";
            _adsController.TabBarItem = new UITabBarItem("Ads", UIImage.FromFile("DailyBooth/tab_live.png"), 0);

            this.ViewControllers = new UIViewController[]
                {
                    
                    _adsController,
                    _venuesController
                };

           
        }
	}
}
