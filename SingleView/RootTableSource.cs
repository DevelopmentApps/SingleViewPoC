using System;
using System.Drawing;

using CoreGraphics;
using Foundation;
using UIKit;

using PetWee.ThirdParty.Foursquare.Dto;
using System.Text;

namespace SingleView
{
    

    public class RootTableSource : UITableViewSource
    {
        PlaceDirectionsController _directionsController;

        Venue[] tableItems;
        UIViewController _parentController;

        string cellIdentifier = "venuecell";


        public RootTableSource(Venue[] items, UIViewController parentController)
        {
            tableItems = items;
            _parentController = parentController;

            _directionsController = _parentController.Storyboard.InstantiateViewController("PlaceDirectionsController") as PlaceDirectionsController;

        }

        public override nint RowsInSection(UITableView tableview, nint section)
        {
            return tableItems.Length;
        }

        public override UITableViewCell GetCell(UITableView tableView, NSIndexPath indexPath)
        {
            // in a Storyboard, Dequeue will ALWAYS return a cell, 
            var cell = tableView.DequeueReusableCell(cellIdentifier);
            // now set the properties as normal
            cell.TextLabel.Text = tableItems[indexPath.Row].name;
            cell.Accessory = UITableViewCellAccessory.DetailButton;
          
           
            return cell;
        }

        public override void AccessoryButtonTapped(UITableView tableView, NSIndexPath indexPath)

        {
            string currentLanguage = string.Empty;

            if (NSLocale.PreferredLanguages.Length > 0)
            {
                currentLanguage = NSLocale.PreferredLanguages[0];
            }


            //string messageAlertCaption = NSBundle.MainBundle.LocalizedString("QuickInfo", "This message is the caption of the alert");

            SingleVenueInfo venueInfo = new SingleVenueInfo();

            var singleVenueResponse = venueInfo.GetIndividualVenueInformation(tableItems[indexPath.Row].id,currentLanguage);

            // base.AccessoryButtonTapped(tableView, indexPath);
            StringBuilder quickDetails = new StringBuilder();


            if (singleVenueResponse != null)
            {
                
                if (singleVenueResponse.venue.hours != null)
                {
                    if (!string.IsNullOrEmpty(singleVenueResponse.venue.hours.status))
                    {
                        quickDetails.Append(singleVenueResponse.venue.hours.status).Append("\n\n");
                    }
                    if (singleVenueResponse.venue.hours.timeframes != null)
                    {
                        
                        foreach (var timeFrame in singleVenueResponse.venue.hours.timeframes)
                        {
                            quickDetails.Append(timeFrame.days).Append("\n");

                            for (int i = 0; i < timeFrame.open.Count; i++)
                            {

                                foreach (var schedule in timeFrame.open)
                                {


                                    quickDetails.Append(schedule.renderedTime).Append("\n");
                                    if (i == timeFrame.open.Count - 1)
                                    {
                                        quickDetails.Append("\n");
                                    }
                                }
                            }
                           
                        }
                    }
                }

                if (singleVenueResponse.venue.location != null)
                {
                    
                    if (singleVenueResponse.venue.location.formattedAddress != null)
                    {
                        foreach (var addressLine in singleVenueResponse.venue.location.formattedAddress)
                        {
                            quickDetails.Append(addressLine).Append("\n");
                        }
                    }
                }
            }

            ////deprecated uiAlertView
            //new UIAlertView(tableItems[indexPath.Row].name, quickDetails.ToString(), null, "OK", null).Show();

            var quickInfoAlertController = UIAlertController.Create(tableItems[indexPath.Row].name, quickDetails.ToString(), UIAlertControllerStyle.Alert);

            quickInfoAlertController.AddAction(UIAlertAction.Create("Ok", UIAlertActionStyle.Default, null));
            quickInfoAlertController.AddAction(UIAlertAction.Create("Get Directions", UIAlertActionStyle.Default, action =>
            {
                _directionsController.Latitude = tableItems[indexPath.Row].location.lat;
                _directionsController.Longitude = tableItems[indexPath.Row].location.lng;

                _parentController.NavigationController.PushViewController(_directionsController, true);

            }));

            _parentController.PresentViewController(quickInfoAlertController, true, null);
            
            
        }

       

        public Venue GetItem(int id)
        {
            return tableItems[id];
        }
        
    }
}