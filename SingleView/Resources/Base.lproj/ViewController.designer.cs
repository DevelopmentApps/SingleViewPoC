// WARNING
//
// This file has been generated automatically by Xamarin Studio from the outlets and
// actions declared in your storyboard file.
// Manual changes to this file will not be maintained.
//
using Foundation;
using System;
using System.CodeDom.Compiler;
using UIKit;

namespace SingleView
{
	[Register ("ViewController")]
	partial class ViewController
	{
		[Outlet]
		[GeneratedCode ("iOS Designer", "1.0")]
		UIButton btnAd { get; set; }

		[Outlet]
		[GeneratedCode ("iOS Designer", "1.0")]
		UIButton btnDemo { get; set; }

		[Outlet]
		[GeneratedCode ("iOS Designer", "1.0")]
		UIButton btnFoursquare { get; set; }

		[Outlet]
		[GeneratedCode ("iOS Designer", "1.0")]
		UIButton btnTransition { get; set; }

		[Outlet]
		[GeneratedCode ("iOS Designer", "1.0")]
		UILabel lblDemo { get; set; }

		void ReleaseDesignerOutlets ()
		{
			if (btnAd != null) {
				btnAd.Dispose ();
				btnAd = null;
			}
			if (btnDemo != null) {
				btnDemo.Dispose ();
				btnDemo = null;
			}
			if (btnFoursquare != null) {
				btnFoursquare.Dispose ();
				btnFoursquare = null;
			}
			if (btnTransition != null) {
				btnTransition.Dispose ();
				btnTransition = null;
			}
			if (lblDemo != null) {
				lblDemo.Dispose ();
				lblDemo = null;
			}
		}
	}
}
