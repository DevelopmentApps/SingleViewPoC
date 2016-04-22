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
	[Register ("PlacesController")]
	partial class PlacesController
	{
		[Outlet]
		[GeneratedCode ("iOS Designer", "1.0")]
		UILabel lblAddress { get; set; }

		[Outlet]
		[GeneratedCode ("iOS Designer", "1.0")]
		UILabel lblErrorToShow { get; set; }

		[Outlet]
		[GeneratedCode ("iOS Designer", "1.0")]
		UILabel lblPlace { get; set; }

		[Outlet]
		[GeneratedCode ("iOS Designer", "1.0")]
		UITextView txtViewAttributions { get; set; }

		void ReleaseDesignerOutlets ()
		{
			if (lblAddress != null) {
				lblAddress.Dispose ();
				lblAddress = null;
			}
			if (lblErrorToShow != null) {
				lblErrorToShow.Dispose ();
				lblErrorToShow = null;
			}
			if (lblPlace != null) {
				lblPlace.Dispose ();
				lblPlace = null;
			}
			if (txtViewAttributions != null) {
				txtViewAttributions.Dispose ();
				txtViewAttributions = null;
			}
		}
	}
}
