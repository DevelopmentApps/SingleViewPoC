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
		UILabel lblPlaceAddress { get; set; }

		[Outlet]
		[GeneratedCode ("iOS Designer", "1.0")]
		UILabel lblPlaceError { get; set; }

		[Outlet]
		[GeneratedCode ("iOS Designer", "1.0")]
		UILabel lblPlaceName { get; set; }

		void ReleaseDesignerOutlets ()
		{
			if (lblPlaceAddress != null) {
				lblPlaceAddress.Dispose ();
				lblPlaceAddress = null;
			}
			if (lblPlaceError != null) {
				lblPlaceError.Dispose ();
				lblPlaceError = null;
			}
			if (lblPlaceName != null) {
				lblPlaceName.Dispose ();
				lblPlaceName = null;
			}
		}
	}
}
