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
	[Register ("FoursquareVenueController")]
	partial class FoursquareVenueController
	{
		[Outlet]
		[GeneratedCode ("iOS Designer", "1.0")]
		UILabel lblError { get; set; }

		[Outlet]
		[GeneratedCode ("iOS Designer", "1.0")]
		UITableView tblView { get; set; }

		void ReleaseDesignerOutlets ()
		{
			if (lblError != null) {
				lblError.Dispose ();
				lblError = null;
			}
			if (tblView != null) {
				tblView.Dispose ();
				tblView = null;
			}
		}
	}
}
