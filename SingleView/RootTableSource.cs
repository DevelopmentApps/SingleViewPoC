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
        Venue[] tableItems;
        string cellIdentifier = "venuecell";

        public RootTableSource(Venue[] items)
        {
            tableItems = items;
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
                        quickDetails.Append(singleVenueResponse.venue.hours.status).Append("\n");
                    }
                    if (singleVenueResponse.venue.hours.timeframes != null)
                    {
                        foreach (var timeFrame in singleVenueResponse.venue.hours.timeframes)
                        {
                            quickDetails.Append(timeFrame.days).Append("\n");
                            foreach (var schedule in timeFrame.open)
                            {
                                quickDetails.Append(schedule.renderedTime).Append("\n");
                            }

                        }
                    }
                }
            }
           

            new UIAlertView(tableItems[indexPath.Row].name
                 , quickDetails.ToString(), null, "OK", null).Show();
        }

        public Venue GetItem(int id)
        {
            return tableItems[id];
        }
        
    }
}