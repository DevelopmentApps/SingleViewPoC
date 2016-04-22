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

        UIWindow _window;

        BannerView _adView;

        public GogAdsController (IntPtr handle) : base (handle)
		{
            _window = new UIWindow(UIScreen.MainScreen.Bounds);

           

            
        }

        public override void ViewWillAppear(bool animated)
        {
            base.ViewWillAppear(animated);

            ShowBannerIntoView();

          
        }

        public override void ViewWillDisappear(bool animated)
        {
            base.ViewWillDisappear(animated);
            _adView.Dispose();
        }

        private void ShowBannerIntoView()
        {

            _adView = new BannerView(size: AdSizeCons.SmartBannerPortrait, origin: new CGPoint(0, _window.Bounds.Bottom - 50))
            {
                AdUnitID = _bannerId,
                RootViewController = this
            };

            _adView.AdReceived += (object sender, EventArgs e) =>
            {                
                    View.AddSubview(_adView);
                _viewOnScreen = true;
            };
            _adView.LoadRequest(Request.GetDefaultRequest());
        }
    }
}
