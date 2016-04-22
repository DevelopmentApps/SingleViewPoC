using CoreGraphics;
using Foundation;
using Google.MobileAds;
using System;
using System.CodeDom.Compiler;
using UIKit;

namespace SingleView
{
	partial class GogAdsController : UIViewController
	{
        const string _bannerId = "ca-app-pub-9392740787233531/5815325201";

        bool _viewOnScreen = false;

        public GogAdsController (IntPtr handle) : base (handle)
		{
		}
        public override void ViewDidLoad()
        {
            base.ViewDidLoad();

            ShowBannerIntoView();
        }

        private void ShowBannerIntoView()
        {

            var adView = new BannerView(size: AdSizeCons.SmartBannerPortrait, origin: new CGPoint(-10, 0))
            {
                AdUnitID = _bannerId,
                RootViewController = this
            };

            adView.AdReceived += (object sender, EventArgs e) =>
            {
                if (!_viewOnScreen)
                    View.AddSubview(adView);
                _viewOnScreen = true;
            };
            adView.LoadRequest(Request.GetDefaultRequest());
        }
    }
}
