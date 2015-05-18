using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Foundation;
using UIKit;

namespace TableCellResizeIssue.Controls.CustomCells
{
    using System.Diagnostics;

    using Cirrious.FluentLayouts.Touch;
    using Cirrious.MvvmCross.Binding.Touch.Views;

    using TableCellResize.Core.ViewModels;

    using TableCellResizeIssue.Core.ViewModels;

    public class CustomerItemTableViewSource  :MvxSimpleTableViewSource
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="CustomerItemTableViewSource"/> class.
        /// </summary>
        /// <param name="tableView">The table view.</param>
        public CustomerItemTableViewSource(UITableView tableView)
            : base(tableView, typeof(CustomerItemCell), "CustomerItemCell")
        {
        }

        public override nfloat GetHeightForRow(UITableView tableView, NSIndexPath indexPath)
        {
            ////var cell = tableView.DequeueReusableCell(new NSString("CustomerItemCell")) as CustomerItemCell;
            ////cell.TranslatesAutoresizingMaskIntoConstraints = false;
            ////cell.SubviewsDoNotTranslateAutoresizingMaskIntoConstraints();

            ////var customer = GetItemAt(indexPath) as ItemViewModel<Customer>;
            ////cell.UpdateCell(customer.Model.DisplayPersonName, customer.Model.DisplayDateOfBirth);

            ////var height = cell.CalculateHeight(tableView.Frame);

            ////Debug.Write("GetHeightForRow.Height: " + height);

            ////return height;

            var cell = tableView.DequeueReusableCell(new NSString("CustomerItemCell")) as CustomerItemCell;

            var customer = GetItemAt(indexPath) as ItemViewModel<Customer>;
            cell.UpdateCell(customer.Model.DisplayPersonName, customer.Model.DisplayDateOfBirth);

            var height = cell.CalculateHeight(tableView.Frame);

            ////Debug.Write("GetHeightForRow.Height: " + height);

            return height;
        }
    }
}