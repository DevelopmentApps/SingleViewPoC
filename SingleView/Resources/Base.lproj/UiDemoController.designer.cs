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
	[Register ("UiDemoController")]
	partial class UiDemoController
	{
		[Outlet]
		[GeneratedCode ("iOS Designer", "1.0")]
		UIButton btnNewPage { get; set; }

		[Outlet]
		[GeneratedCode ("iOS Designer", "1.0")]
		UIButton btnTransitionToAddress { get; set; }

		[Outlet]
		[GeneratedCode ("iOS Designer", "1.0")]
		UILabel lblMessageToUser { get; set; }

		[Outlet]
		[GeneratedCode ("iOS Designer", "1.0")]
		UILabel lblTexto { get; set; }

		[Outlet]
		[GeneratedCode ("iOS Designer", "1.0")]
		UIPickerView pcrView { get; set; }

		void ReleaseDesignerOutlets ()
		{
			if (btnNewPage != null) {
				btnNewPage.Dispose ();
				btnNewPage = null;
			}
			if (btnTransitionToAddress != null) {
				btnTransitionToAddress.Dispose ();
				btnTransitionToAddress = null;
			}
			if (lblMessageToUser != null) {
				lblMessageToUser.Dispose ();
				lblMessageToUser = null;
			}
			if (lblTexto != null) {
				lblTexto.Dispose ();
				lblTexto = null;
			}
			if (pcrView != null) {
				pcrView.Dispose ();
				pcrView = null;
			}
		}
	}
}
