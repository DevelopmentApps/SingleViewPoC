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
           
            return cell;
        }

        public Venue GetItem(int id)
        {
            return tableItems[id];
        }
    }
}