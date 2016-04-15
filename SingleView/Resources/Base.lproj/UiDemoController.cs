
using CoreGraphics;
using Foundation;
using Plugin.Connectivity.Abstractions;
using System;
using System.CodeDom.Compiler;
using System.Collections.Generic;
using System.Linq;

using System.Threading;
using System.Threading.Tasks;
using UIKit;

namespace SingleView
{
	partial class UiDemoController : UIViewController
	{
        ListDemoController _mapsDemoController;

        LoadingOverlay _loadingDemo;

        //UIActivityIndicatorView spinner;

        //private CancellationTokenSource cancellationToken;

        //private ConnectivityChangedEventHandler _connectionChanged;

        //private int _eventInvoationList;   

        private IList<object> values;

        public string MessageToShow { get; set; }

		public UiDemoController (IntPtr handle) : base (handle)
		{
            //this allows to register the controller so we can perform a segue withouth a "classic" segue
            _mapsDemoController = Storyboard.InstantiateViewController("ListDemoController") as ListDemoController;

        }

        

        public override void ViewDidLoad()
        {

            //Observable.FromEventPattern<ConnectivityChangedEventHandler, ConnectivityChangedEventArgs>(handler => this._connectionChanged += handler, handler => this._connectionChanged -= handler).Take(1).Subscribe();


            base.ViewDidLoad();


            /////this method will not be used, is buggy and reentrant
           // Plugin.Connectivity.CrossConnectivity.Current.ConnectivityChanged += Current_ConnectivityChanged;

           // this._connectionChanged  +=  this.Current_ConnectivityChanged;
           // _connectionChanged += Current_ConnectivityChanged;
           //_eventInvoationList = _connectionChanged.GetInvocationList().Length;

            btnNewPage.TouchUpInside += BtnNewPage_TouchUpInside;
           
            lblTexto.Enabled = false;           
            this.Title = MessageToShow;

            //foreach (var item in Plugin.Connectivity.CrossConnectivity.Current.ConnectionTypes)
            //{
            //    values.Add(item.ToString());
            //}

            UpdateVewPicker();
           
          
        }

       

        public override void ViewDidDisappear(bool animated)
        {
            base.ViewDidDisappear(animated);
           
            this.DismissViewController(true, null);
            


        }

        private void BtnNewPage_TouchUpInside(object sender, EventArgs e)
        {

            var bounds = UIScreen.MainScreen.Bounds;
            _loadingDemo = new LoadingOverlay(bounds);
            View.Add(_loadingDemo);
            View.BringSubviewToFront(_loadingDemo);

            Task.Factory.StartNew(() => { InvokeOnMainThread(() => ConnectionReview()); }).ContinueWith(t => { _loadingDemo.Hide(); }, TaskScheduler.FromCurrentSynchronizationContext());

            //_eventInvoationList = _connectionChanged.GetInvocationList().Length;

            //  InvokeOnMainThread(() => showIndicator()); 

            //ConnectionReview();

            //InvokeOnMainThread(() => showIndicator());

            //if (_connectionChanged != null)
            //{
            //    foreach (var item in _connectionChanged.GetInvocationList())
            //    {
            //        item.ToString();
            //    }
            //}

            //perform the segue transition after all previous work finished
            NavigationController.PushViewController(_mapsDemoController, true);
        }

        private void UpdateVewPicker()
        {
            if (values == null)
            {
                values = new List<object>();
            }
            else
            {
                values.Clear();
            }
          
            values.Add(Plugin.Connectivity.CrossConnectivity.Current.ConnectionTypes.FirstOrDefault().ToString());
            

            PickerModelDemo pickmodel = new PickerModelDemo(values);
            pcrView.Model = pickmodel;
            pcrView.ShowSelectionIndicator = true;
        }

        /// <summary>
        /// this event must be handled only in view that are going to perform internet operations otherwise is not needed. By the way, this method is buggy and reentrant we are not going to use it.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        //private void Current_ConnectivityChanged(object sender, ConnectivityChangedEventArgs e)
        //{
        //    //CancellationTokenSource newCTS;

        //    //if (cancellationToken != null)
        //    //{
        //    //    cancellationToken.Cancel();
        //    //}

        //    //    newCTS = new CancellationTokenSource();
        //    //    cancellationToken = newCTS;
        //    //try
        //    //{


        //        var bounds = UIScreen.MainScreen.Bounds;
        //        _loadingDemo = new LoadingOverlay(bounds);
        //        View.Add(_loadingDemo);
        //        View.BringSubviewToFront(_loadingDemo);

        //        Task.Factory.StartNew(() => { InvokeOnMainThread(() => ConnectionReview()); }).ContinueWith(t => { _loadingDemo.Hide(); }, TaskScheduler.FromCurrentSynchronizationContext());

        //    //_eventInvoationList = _connectionChanged.GetInvocationList().Length;

        //    //if (_connectionChanged != null)
        //    //{
        //    //    foreach (var item in _connectionChanged.GetInvocationList())
        //    //    {
        //    //        item.ToString();
        //    //    }
        //    //}

        //    //}
        //    //catch (OperationCanceledException)
        //    //{
        //    //    Console.WriteLine("Operation Cancelled");
        //    //}
        //    //catch (Exception)
        //    //{

        //    //    Console.WriteLine("Something worse happened");
        //    //}
        //    //finally
        //    //{


        //    //}


        //    //if (cancellationToken == newCTS)
        //    //    cancellationToken = null;







        //    // UpdateVewPicker();

        //    //  InvokeOnMainThread(()=> showIndicator());
        //    // ConnectionReview();
        //    //InvokeOnMainThread(() =>  hideIndicator());
        //}

        private  void ConnectionReview()
        {
            ////This method is not reliable, we can't know for sure when a site is available or not, we only will check if we can perform the operation on internet, if something wrong happens the catch the Exception and move on;
            //bool isGoogleReachable = await IsInternetConnectionAvailable();

            //if (isGoogleReachable == true)
            //{
            //    lblTexto.Text = "Google is reachable";
            //}
            //else if (isGoogleReachable == false)
            //{
            //    lblTexto.Text = "google is not reachable";
            //}

            //this is to simulate slow network speed to allow the overlay to be showed
            System.Threading.Thread.Sleep(3000);
            var currentConnectionType = Plugin.Connectivity.CrossConnectivity.Current.ConnectionTypes;

            var currentNetworkReachability = Plugin.Connectivity.Reachability.InternetConnectionStatus();

            if (currentConnectionType.FirstOrDefault().ToString() == Plugin.Connectivity.Abstractions.ConnectionType.WiFi.ToString() && currentNetworkReachability.ToString() == Plugin.Connectivity.NetworkStatus.ReachableViaWiFiNetwork.ToString())
            {

                lblMessageToUser.Text = "seems that you are connected via wifi";
               
            }
            else if (currentConnectionType.FirstOrDefault().ToString() == Plugin.Connectivity.Abstractions.ConnectionType.Cellular.ToString() && currentNetworkReachability.ToString() == Plugin.Connectivity.NetworkStatus.ReachableViaCarrierDataNetwork.ToString())
            {
                lblMessageToUser.Text = "seems that you are connected via cellular";
                
            }
            else
            {
                lblMessageToUser.Text = "seems that you have no connection";
               
            }
            //this._connectionChanged -= this.Current_ConnectivityChanged;

          

            //this._connectionChanged += this.Current_ConnectivityChanged;

            //_eventInvoationList = _connectionChanged.GetInvocationList().Length;

            //if (_connectionChanged != null)
            //{
            //    foreach (var item in _connectionChanged.GetInvocationList())
            //    {
            //        item.ToString();
            //    }
            //}
        }


        /////This method didn't work
        //private void showIndicator()
        //{
        //    if (spinner == null)
        //    {
                
        //        spinner = new UIActivityIndicatorView(UIActivityIndicatorViewStyle.Gray);  
        //        spinner.HidesWhenStopped = true;
        //        spinner.Color = UIColor.Gray;
                
        //    }
                      
        //    View.AddSubview(spinner);
        //    View.BringSubviewToFront(spinner);
        //    spinner.StartAnimating();
        //}

        //private void hideIndicator()
        //{
        //    if (spinner == null)
        //        return;
        //    if (!spinner.IsAnimating)
        //        return;
        //    spinner.StopAnimating();
        //}

        ////This method is not reliable, we can't know for sure when a site is available or not, we only will check if we can perform the operation on internet, if something wrong happens the catch the Exception and move on;
        //private async Task<bool> IsInternetConnectionAvailable()
        //{


        //    bool isInternetConnectionAvailable = await Plugin.Connectivity.CrossConnectivity.Current.IsReachable("www.google.com", 6000);


        //   if (isInternetConnectionAvailable)
        //    {
        //        return true;
        //    }



        //    return false;
        //}


    }

    public class PickerModelDemo : UIPickerViewModel
    {
        private IList<object> values;

        public event EventHandler<PickerChangedEventArgs> PickerChanged;

        public PickerModelDemo(IList<Object> values)
        {
            this.values = values;
        }

        public override nint GetComponentCount(UIPickerView picker)
        {
            return 1;
        }

        public override nint GetRowsInComponent(UIPickerView pickerView, nint component)
        {
            return values.Count;
        }

        public override string GetTitle(UIPickerView pickerView, nint row, nint component)
        {
            var demo = System.Convert.ToInt32(row);
            return values[demo].ToString();
        }

        public override nfloat GetRowHeight(UIPickerView pickerView, nint component)
        {
            return 40f;
        }

        public override void Selected(UIPickerView pickerView, nint row, nint component)
        {
            var rowValue = System.Convert.ToInt32(row);

            if (this.PickerChanged != null)
            {
                this.PickerChanged(this, new PickerChangedEventArgs { SelectedValue = values[rowValue] });
            }

        }

    }

    public class PickerChangedEventArgs : EventArgs
    {
        public object SelectedValue { get; set; }
    }

    public class LoadingOverlay : UIView
    {
        // control declarations
        UIActivityIndicatorView activitySpinner;
        UILabel loadingLabel;

        public LoadingOverlay(CGRect frame) : base(frame)
        {
            // configurable bits
            BackgroundColor = UIColor.Black;
            Alpha = 0.75f;
            AutoresizingMask = UIViewAutoresizing.All;

            nfloat labelHeight = 22;
            nfloat labelWidth = Frame.Width - 20;

            // derive the center x and y
            nfloat centerX = Frame.Width / 2;
            nfloat centerY = Frame.Height / 2;

            // create the activity spinner, center it horizontall and put it 5 points above center x
            activitySpinner = new UIActivityIndicatorView(UIActivityIndicatorViewStyle.WhiteLarge);
            activitySpinner.Frame = new CGRect(
                centerX - (activitySpinner.Frame.Width / 2),
                centerY - activitySpinner.Frame.Height - 20,
                activitySpinner.Frame.Width,
                activitySpinner.Frame.Height);
            activitySpinner.AutoresizingMask = UIViewAutoresizing.All;
            AddSubview(activitySpinner);
            activitySpinner.StartAnimating();

            // create and configure the "Loading Data" label
            loadingLabel = new UILabel(new CGRect(
                centerX - (labelWidth / 2),
                centerY + 20,
                labelWidth,
                labelHeight
                ));
            
            loadingLabel.BackgroundColor = UIColor.Clear;
            loadingLabel.TextColor = UIColor.White;
            loadingLabel.Text = NSBundle.MainBundle.LocalizedString("Loading","This message is for loading stuff");
            
            loadingLabel.TextAlignment = UITextAlignment.Center;
            loadingLabel.AutoresizingMask = UIViewAutoresizing.All;
            AddSubview(loadingLabel);
            

        }

        /// <summary>
        /// Fades out the control and then removes it from the super view
        /// </summary>
        public void Hide()
        {
            UIView.Animate(
                0.5, // duration
                () => { Alpha = 0; },
                () => { RemoveFromSuperview(); }
            );
        }
    }

}
