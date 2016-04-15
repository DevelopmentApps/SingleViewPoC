﻿using System;
using Foundation;
using UIKit;

namespace SingleView
{
    public partial class ViewController : UIViewController
    {
     
        private UiDemoController _demoController;



        public ViewController(IntPtr handle) : base(handle)
        {

            if (NSLocale.PreferredLanguages.Length > 0)
            {
                var pref = NSLocale.PreferredLanguages[0];
            }

                //this allows to register the controller so we can perform a segue withouth a "classic" segue
                _demoController = Storyboard.InstantiateViewController("UiDemoController") as UiDemoController;
           
        }

        public override void ViewDidLoad()
        {
            

            base.ViewDidLoad();
            // Perform any additional setup after loading the view, typically from a nib.

          

            btnDemo.TouchUpInside += BtnDemo_TouchUpInside;
            btnTransition.TouchUpInside += BtnTransition_TouchUpInside;

           



        }



        public override void ViewWillAppear(bool animated)
        {
            base.ViewWillAppear(animated);
            lblDemo.Hidden = false;
        }

        public override void ViewDidAppear(bool animated)
        {
            base.ViewDidAppear(animated); 
        }

        public override void ViewWillDisappear(bool animated)
        {
            base.ViewWillDisappear(animated);
            lblDemo.Hidden = true;
            lblDemo.Text = "Try again";
            
        }

        public override void ViewDidDisappear(bool animated)
        {
            base.ViewDidDisappear(animated);
            
        }



        public override void DidReceiveMemoryWarning()
        {
            base.DidReceiveMemoryWarning();
            // Release any cached data, images, etc that aren't in use.
        }


        /// <summary>
        /// This code is only when segue is added via the visual editor
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        //public override void PrepareForSegue(UIStoryboardSegue segue, NSObject sender)
        //{
        //    base.PrepareForSegue(segue, sender);

        //    var newDemoController = segue.DestinationViewController as UiDemoController;

        //    if(newDemoController != null)
        //    { newDemoController.MessageToShow = "Wow! This Works!"; }
        //}


            ///This is to perform validations before segue execution
        //public override void ShouldPerformSegue(string segueIdentifier, NSObject sender)
        //{
        //    return false;//segue will not be performed
        //    return true; // segue will be performed
        //}

        private void BtnDemo_TouchUpInside(object sender, EventArgs e)
        {
            lblDemo.Text = "you touched me!";

            //yo send an alert
            var okAlertController = UIAlertController.Create("Warning!", "This is a simple sample button", UIAlertControllerStyle.Alert);
            okAlertController.AddAction(UIAlertAction.Create("Ok", UIAlertActionStyle.Default, OkButtonAction));

            PresentViewController(okAlertController, true, null);

        }

        private void BtnTransition_TouchUpInside(object sender, EventArgs e)
        {
            
           

            if (_demoController != null)
            {
                _demoController.MessageToShow = "A Lot of Bugs!!";


                NavigationController.PushViewController(_demoController, true);


            }

        }
        
        private void OkButtonAction(UIAlertAction obj)
        {
            
            lblDemo.Text = "alert";
        }

     

       


    }
}