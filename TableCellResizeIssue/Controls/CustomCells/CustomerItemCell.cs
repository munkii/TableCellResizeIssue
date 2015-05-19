using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Foundation;
using UIKit;
using TableCellResizeIssue.CustomCells;

namespace TableCellResizeIssue.Controls.CustomCells
{
    using System.Windows.Input;

    using Cirrious.FluentLayouts.Touch;
    using Cirrious.MvvmCross.Binding.BindingContext;
    using Cirrious.MvvmCross.Binding.Touch.Views;

    using CoreGraphics;

    using TableCellResize.Core.ViewModels;

    using TableCellResizeIssue.Core.ViewModels;

    /// <summary>
    /// Custom TableCell definition for CustomerItem
    /// </summary>
    [Register("CustomerItemCell")]
    public class CustomerItemCell : MvxTableViewCell
    {
        /// <summary>
        /// The horizontal space between a label and its data e.g. space between 'No.' and '111' 
        /// </summary>
        private const int LabelDataMargin = 4;

        /// <summary>
        /// The horizontal space between data items e.g. Gender and NHS number
        /// </summary>
        private const int DataMargin = 4;

        /// <summary>
        /// The Customer name label
        /// </summary>
        private UILabel customerNameLabel;

        /// <summary>
        /// The born label
        /// </summary>
        private UILabel bornLabel;

        /// <summary>
        /// The born data label
        /// </summary>
        private UILabel bornDataLabel;

        /// <summary>
        /// The gender label
        /// </summary>
        private UILabel genderLabel;

        /// <summary>
        /// The gender data label
        /// </summary>
        private UILabel genderDataLabel;

        /// <summary>
        /// The Customer number label
        /// </summary>
        private UILabel customerNumberLabel;

        /// <summary>
        /// The Customer number data label
        /// </summary>
        private UILabel customerNumberDataLabel;

        /// <summary>
        /// Flag that indicates whether or not the constraints have been ran
        /// </summary>
        private bool constraintsApplied;

        /// <summary>
        /// The constraints applied when device is reporting a Horizontal Size Class of Compact
        /// </summary>
        private LayoutConstraintsSet horizontalSizeClassCompactConstraints = null;

        /// <summary>
        /// The constraints applied when device is reporting a Horizontal Size Class of Regular
        /// </summary>
        private LayoutConstraintsSet horizontalSizeClassRegularConstraints = null;

        /// <summary>
        /// Initializes a new instance of the <see cref="CustomerItemCell"/> class.
        /// </summary>
        public CustomerItemCell()
        {
            this.CreateView();
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="CustomerItemCell"/> class.
        /// </summary>
        /// <param name="handle">The handle.</param>
        public CustomerItemCell(IntPtr handle)
            : base(handle)
        {
            this.CreateView();
        }

        /// <summary>
        /// Gets or sets the selected command.
        /// </summary>
        /// <value>
        /// The selected command.
        /// </value>
        public ICommand SelectedCommand { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether this Cell has been selected
        /// </summary>
        /// <value>
        /// <c>true</c> if this instance is selected; otherwise, <c>false</c>.
        /// </value>
        public bool IsSelected
        {
            get
            {
                return ((ItemViewModel<Customer>)this.DataContext).IsSelected;
            }

            set
            {
                ((ItemViewModel<Customer>)this.DataContext).IsSelected = value;
            }
        }

        /// <summary>
        /// Sets the selected state of the cell, optionally animating the transition between the states.
        /// </summary>
        /// <param name="selected"><see langword="true" /> to select the cell, <see langword="false" /> to deselect.</param>
        /// <param name="animated"><see langword="true" /> to animate the selected transition, <see langword="false" /> to make the transition immediate.</param>
        /// <remarks>
        /// Selection changes the the appearance of the cell's labels, image, and background.
        /// </remarks>
        public override void SetSelected(bool selected, bool animated)
        {
            base.SetSelected(selected, animated);

            if (this.DataContext == null)
            {
                // We cannot support the selecting of the Cell if we have no data context
                return;
            }

            if (this.IsSelected == selected)
            {
                return;
            }

            this.IsSelected = selected;
            if (this.IsSelected)
            {
                if (this.Editing == false)
                {
                    if (this.SelectedCommand != null)
                    {
                        this.SelectedCommand.Execute(null);
                        this.SetSelected(false, false);
                    }
                }
            }
        }

        /// <summary>
        /// Updates the Auto Layout constraints for the <see cref="T:UIKit.UIView" />.
        /// </summary>
        public override void UpdateConstraints()
        {
            base.UpdateConstraints();

            if (this.constraintsApplied)
            {
                return;
            }



            this.constraintsApplied = true;
        }

        /// <summary>
        /// Lays out the SubViews.
        /// </summary>
        public override void LayoutSubviews()
        {
            base.LayoutSubviews();

            // This weirdness is needed to push the label to wrap
//            this.customerNameLabel.PreferredMaxLayoutWidth = this.customerNameLabel.Frame.Size.Width;
        }

        //Not necessary anymore
//        public void UpdateCell(string artistName, string songTitle)
//        {
//            this.customerNameLabel.Text = artistName;
//            this.bornDataLabel.Text = songTitle;
//        }
//
        //Not necessary anymore
//        public nfloat CalculateHeight(CGRect tableViewFrame)
//        {
//            this.Bounds = new CGRect(0, 0, tableViewFrame.Width, Bounds.Height);
//            this.SetNeedsLayout();
//            this.LayoutIfNeeded();
//
//            //This is where the magic happens
//            var size = ContentView.SystemLayoutSizeFittingSize(UILayoutFittingCompressedSize);
//
//            return size.Height + 1; //Add 1 since we are using cell separators.
//        }

        /// <summary>
        /// Creates the layout.
        /// </summary>
        private void CreateView()
        {
            this.customerNameLabel = new AutoLayoutLabel();
            this.customerNameLabel.AccessibilityIdentifier = "CustomerItemCell_CustomerNameLabel";
            this.customerNameLabel.Font = UIFont.PreferredHeadline; //// CustomerBanner.GetBoldDerivative(UIFont.PreferredHeadline);
            this.customerNameLabel.Lines = 0;
            this.customerNameLabel.LineBreakMode = UILineBreakMode.WordWrap;
            this.customerNameLabel.TranslatesAutoresizingMaskIntoConstraints = false;
            
            this.bornLabel = new UILabel();
            this.bornLabel.Text = "Born";
            this.bornLabel.Font = UIFont.PreferredCaption1;
            this.bornLabel.TranslatesAutoresizingMaskIntoConstraints = false;

            this.bornDataLabel = new UILabel();
            this.bornDataLabel.Font = UIFont.PreferredCaption1; ////CustomerBanner.GetBoldDerivative(UIFont.PreferredCaption1);
            this.bornDataLabel.AccessibilityIdentifier = "CustomerItemCell_bornDataLabel";
            this.bornDataLabel.TranslatesAutoresizingMaskIntoConstraints = false;
            
            this.genderLabel = new UILabel();
            this.genderLabel.Text = "G";
            this.genderLabel.Hidden = false;
            this.genderLabel.Font = UIFont.PreferredCaption1;
            this.genderLabel.TranslatesAutoresizingMaskIntoConstraints = false;

            this.genderDataLabel = new UILabel();
            this.genderDataLabel.Font = UIFont.PreferredCaption1; ////CustomerBanner.GetBoldDerivative(UIFont.PreferredCaption1);
            this.genderDataLabel.AccessibilityIdentifier = "CustomerItemCell_genderDataLabel";
            this.genderDataLabel.TranslatesAutoresizingMaskIntoConstraints = false;
            this.genderDataLabel.Text = "Female";

            this.customerNumberLabel = new UILabel();
            this.customerNumberLabel.Hidden = false;
            this.customerNumberLabel.Font = UIFont.PreferredCaption1;
            this.customerNumberLabel.TranslatesAutoresizingMaskIntoConstraints = false;
            this.customerNumberLabel.Text = "CHI";

            this.customerNumberDataLabel = new UILabel();
            this.customerNumberDataLabel.AccessibilityIdentifier = "CustomerItemCell_CustomerNumberDataLabel";
            this.customerNumberDataLabel.Font = UIFont.PreferredCaption1; ////CustomerBanner.GetBoldDerivative(UIFont.PreferredCaption1);
            this.customerNumberDataLabel.TranslatesAutoresizingMaskIntoConstraints = false;
            this.customerNumberDataLabel.Text = "A123456789";

            this.TranslatesAutoresizingMaskIntoConstraints = false;

            this.DelayBind(
                () =>
                {
                    var set = this.CreateBindingSet<CustomerItemCell, ItemViewModel<Customer>>();
                    set.Bind(this.customerNameLabel).To(vm => vm.Model.DisplayPersonName);
                    set.Bind(this.bornDataLabel).To(vm => vm.Model.DisplayDateOfBirth);
                    ////set.Bind(this.genderDataLabel).To(vm => vm.Model.DisplayPersonGender);
                    ////set.Bind(this.customerNumberLabel).To(vm => vm.Model.DisplayCustomerNumberLabel);
                    ////set.Bind(this.customerNumberDataLabel).To(vm => vm.Model.DisplayCustomerNumber);
                    set.Bind().For(c => c.SelectedCommand).To(vm => vm.ClickCommand);
                    set.Apply();
                });

            this.ContentView.AddSubviews(this.customerNameLabel, this.bornLabel, this.bornDataLabel, this.genderLabel, this.genderDataLabel, this.customerNumberLabel, this.customerNumberDataLabel);

            this.ContentView.AddConstraints(
               this.customerNameLabel.AtTopOf(this.ContentView, 7),
               this.customerNameLabel.AtLeftOf(this.ContentView, 15),
               this.customerNameLabel.AtRightOf(this.ContentView, 15));

            this.ContentView.AddConstraints(
                this.bornLabel.Below(this.customerNameLabel),
                this.bornLabel.AtBottomOf(this.ContentView, 5),
                this.bornLabel.AtLeftOf(this.ContentView, 15),
                this.bornDataLabel.WithSameTop(this.bornLabel),
                this.bornDataLabel.ToRightOf(this.bornLabel, LabelDataMargin),
                this.bornDataLabel.AtBottomOf(this.ContentView, 5));

            this.ContentView.AddConstraints(
                this.genderLabel.WithSameTop(this.bornLabel),
                this.genderLabel.AtBottomOf(this.ContentView, 5),
                this.genderDataLabel.WithSameTop(this.bornLabel),
                this.genderDataLabel.AtBottomOf(this.ContentView, 5));

            this.ContentView.AddConstraints(
                this.customerNumberLabel.WithSameTop(this.bornLabel),
                this.customerNumberLabel.AtBottomOf(this.ContentView, 5),
                this.customerNumberDataLabel.WithSameTop(this.bornLabel),
                this.customerNumberDataLabel.AtBottomOf(this.bornLabel, 5));

            this.ContentView.AddConstraints(
                                            this.genderLabel.ToRightOf(this.bornDataLabel, DataMargin),
                                            this.genderDataLabel.ToRightOf(this.genderLabel, LabelDataMargin),
                                            this.customerNumberLabel.ToRightOf(this.genderDataLabel, DataMargin),
                                            this.customerNumberDataLabel.ToRightOf(this.customerNumberLabel, LabelDataMargin));


            
            ////this.ContentView.TranslatesAutoresizingMaskIntoConstraints = false;
            this.SetNeedsUpdateConstraints();
        }

        /////// <summary>
        /////// Creates the SizeClass constraints. These are Added and Removed depending on Size Class constraints
        /////// </summary>
        ////private void EnsureSizeClassConstraints()
        ////{
        ////    if (this.horizontalSizeClassCompactConstraints == null)
        ////    {
        ////        this.horizontalSizeClassCompactConstraints = new LayoutConstraintsSet() { Applied = false, Constraints = new List<NSLayoutConstraint>() };

        ////        this.horizontalSizeClassCompactConstraints.Constraints.Add(this.genderDataLabel.ToRightOf(this.bornDataLabel, DataMargin).ToLayoutConstraints().First());
        ////        this.horizontalSizeClassCompactConstraints.Constraints.Add(this.customerNumberDataLabel.ToRightOf(this.genderDataLabel, DataMargin).ToLayoutConstraints().First());
        ////    }

        ////    if (this.horizontalSizeClassRegularConstraints == null)
        ////    {
        ////        this.horizontalSizeClassRegularConstraints = new LayoutConstraintsSet { Applied = false, Constraints = new List<NSLayoutConstraint>() };

        ////        this.horizontalSizeClassRegularConstraints.Constraints.Add(this.genderLabel.ToRightOf(this.bornDataLabel, DataMargin).ToLayoutConstraints().First());
        ////        this.horizontalSizeClassRegularConstraints.Constraints.Add(this.genderDataLabel.ToRightOf(this.genderLabel, LabelDataMargin).ToLayoutConstraints().First());
        ////        this.horizontalSizeClassRegularConstraints.Constraints.Add(this.customerNumberLabel.ToRightOf(this.genderDataLabel, DataMargin).ToLayoutConstraints().First());
        ////        this.horizontalSizeClassRegularConstraints.Constraints.Add(this.customerNumberDataLabel.ToRightOf(this.customerNumberLabel, LabelDataMargin).ToLayoutConstraints().First());
        ////    }
        ////}

        /////// <summary>
        /////// Ensure the application of SizeClassConstraints and the removal of opposing ones
        /////// </summary>
        /////// <param name="constraintsToApply">The constraints to apply.</param>
        /////// <param name="constraintsToRemove">The constraints to remove.</param>
        ////private void EnsureApplicationSizeClassConstraints(LayoutConstraintsSet constraintsToApply, LayoutConstraintsSet constraintsToRemove)
        ////{
        ////    if (constraintsToApply.Applied == false)
        ////    {
        ////        // Apply them
        ////        foreach (var constraint in constraintsToApply.Constraints)
        ////        {
        ////            this.AddConstraint(constraint);
        ////        }

        ////        constraintsToApply.Applied = true;
        ////    }

        ////    if (constraintsToRemove.Applied == true)
        ////    {
        ////        // Unapply them
        ////        foreach (var constraint in constraintsToRemove.Constraints)
        ////        {
        ////            this.RemoveConstraint(constraint);
        ////        }

        ////        constraintsToRemove.Applied = false;
        ////    }
        ////}
    }
}