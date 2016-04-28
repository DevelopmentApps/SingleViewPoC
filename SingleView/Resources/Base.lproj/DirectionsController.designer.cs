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
	[Register ("DirectionsController")]
	partial class DirectionsController
	{
        [Outlet]
        [GeneratedCode("iOS Designer", "1.0")]
        UIButton btnDemo { get; set; }

        void ReleaseDesignerOutlets ()
		{
            if (btnDemo != null)
            {
                btnDemo.Dispose();
                btnDemo = null;
            }
        }
	}
}
