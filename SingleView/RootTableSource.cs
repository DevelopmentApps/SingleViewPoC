using System;
using System.Drawing;

using CoreGraphics;
using Foundation;
using UIKit;

using PetWee.ThirdParty.Foursquare.Dto;

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
            // base.AccessoryButtonTapped(tableView, indexPath);
            new UIAlertView("DetailDisclosureButton Touched"
                 , tableItems[indexPath.Row].id, null, "OK", null).Show();
        }

        public Venue GetItem(int id)
        {
            return tableItems[id];
        }
        
    }
}