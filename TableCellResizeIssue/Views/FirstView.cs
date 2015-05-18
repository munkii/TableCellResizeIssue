using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Foundation;
using UIKit;

namespace TableCellResizeIssue.Views
{
    using Cirrious.FluentLayouts.Touch;
    using Cirrious.MvvmCross.Touch.Views;

    using TableCellResizeIssue.Core.ViewModels;

    public class FirstView : MvxViewController<FirstViewModel>
    {
        public override void ViewDidLoad()
        {
            base.ViewDidLoad();

            UIButton searchButton = UIButton.FromType(UIButtonType.System);
            searchButton.SetTitle("Search", UIControlState.Normal);
            searchButton.TranslatesAutoresizingMaskIntoConstraints = false;
            searchButton.TouchUpInside += searchButton_TouchUpInside;

            this.View.AddSubviews(searchButton);

            this.View.AddConstraints(
                searchButton.WithSameCenterX(this.View), 
                searchButton.WithSameCenterY(this.View));


            this.View.SubviewsDoNotTranslateAutoresizingMaskIntoConstraints();
        }

        void searchButton_TouchUpInside(object sender, EventArgs e)
        {
            FirstViewModel vm = this.ViewModel as FirstViewModel;

            if (vm != null)
            {
                vm.GoToSearchCommand.Execute(null);
            }
        }
    }
}