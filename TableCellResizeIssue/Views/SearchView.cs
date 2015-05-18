using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Foundation;
using UIKit;

namespace TableCellResizeIssue.Views
{
    using Cirrious.FluentLayouts.Touch;
    using Cirrious.MvvmCross.Binding.BindingContext;
    using Cirrious.MvvmCross.Binding.Touch.Views;
    using Cirrious.MvvmCross.Touch.Views;

    using TableCellResize.Core.ViewModels;

    using TableCellResizeIssue.Controls.CustomCells;
    using TableCellResizeIssue.Core.ViewModels;

    public class SearchView : MvxViewController
    {
        /// <summary>
        /// The search bar
        /// </summary>
        private UISearchBar searchBar;

        /// <summary>
        /// TableView for Search Results
        /// </summary>
        private UITableView searchResultsTable;

        /// <summary>
        /// DataSource for the UITableView searchResultsTable, bound to the ViewModel SearchResults list
        /// </summary>
        private CustomerItemTableViewSource searchResultsTableDataSource;
        ////private MvxSimpleTableViewSource searchResultsTableDataSource;

        /// <summary>
        /// Initializes a new instance of the <see cref="SearchView"/> class.
        /// </summary>
        /// <param name="handle">The handle.</param>
        public SearchView(IntPtr handle)
            : base(handle)
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="SearchView"/> class.
        /// </summary>
        public SearchView()
            : base()
        {
        }
        
        /// <summary>
        /// Gets the SearchViewModel.
        /// </summary>
        /// <value>
        /// The appointments view model.
        /// </value>
        public SearchViewModel SearchViewModel
        {
            get
            {
                return this.ViewModel as SearchViewModel;
            }
        }

        #region View lifecycle

        /// <summary>
        /// The View has loaded
        /// </summary>
        public override void ViewDidLoad()
        {
            base.ViewDidLoad();

            this.Title = "Search";
            this.View.BackgroundColor = UIColor.White;
        }

        /// <summary>
        /// Initializes the <see cref="P:UIKit.UIViewController.View" /> property.
        /// </summary>
        public override void LoadView()
        {
            base.LoadView();

            this.CreateEnterSelectModeToolbar();
            this.CreateSearchBar();
            this.CreateSearchResultsTable();

            this.View.SubviewsDoNotTranslateAutoresizingMaskIntoConstraints();

            var set = this.CreateBindingSet<SearchView, SearchViewModel>();
            set.Bind(this.searchResultsTableDataSource).To(vm => vm.SearchResults);
            set.Bind(this.searchBar).For(x => x.Text).To(vm => vm.CurrentSearchCriteria);
            set.Apply();

            this.NavigationController.ToolbarHidden = false;
        }

        /// <summary>
        /// Views the will appear.
        /// </summary>
        /// <param name="animated">if set to <c>true</c> [animated].</param>
        public override void ViewWillAppear(bool animated)
        {
            base.ViewWillAppear(animated);

            this.NavigationController.ToolbarHidden = false;
        }

        #endregion

        /// <summary>
        /// Called when the <see cref="T:UIKit.UIViewController" /> needs to recalculate its layout constraints.
        /// </summary>
        /// <remarks>
        /// This method allows application developers to add or remove layout constraints dynamically. Application developers who override this method must call <c>base.UpdateViewConstraints()</c>.
        /// </remarks>
        public override void UpdateViewConstraints()
        {
            base.UpdateViewConstraints();
        }

        /// <summary>
        /// Creates the search results table.
        /// </summary>
        private void CreateSearchResultsTable()
        {
            this.searchResultsTable = new UITableView();
            this.searchResultsTable.AccessibilityIdentifier = "SearchView_SearchResultsTable";
            this.searchResultsTable.TranslatesAutoresizingMaskIntoConstraints = false;
            this.searchResultsTable.RowHeight = UITableView.AutomaticDimension;
            this.searchResultsTable.EstimatedRowHeight = 44.0f;
            this.searchResultsTable.AllowsMultipleSelectionDuringEditing = true;
            this.searchResultsTable.TableFooterView = new UIView();

            this.searchResultsTableDataSource = new CustomerItemTableViewSource(this.searchResultsTable);

            this.searchResultsTable.Source = this.searchResultsTableDataSource;

            this.View.AddSubview(this.searchResultsTable);

            this.View.AddConstraints(
                                this.searchResultsTable.Below(this.searchBar),
                                this.searchResultsTable.WithSameLeft(this.View),
                                this.searchResultsTable.WithSameWidth(this.View));

            this.View.AddConstraint(NSLayoutConstraint.Create(this.searchResultsTable, NSLayoutAttribute.Bottom, NSLayoutRelation.Equal, this.BottomLayoutGuide, NSLayoutAttribute.Top, 1.0f, 0.0f));
        }

        /// <summary>
        /// Creates the search bar.
        /// </summary>
        private void CreateSearchBar()
        {
            this.searchBar = new UISearchBar()
            {
                AutocorrectionType = UITextAutocorrectionType.No,
                EnablesReturnKeyAutomatically = true,
                TranslatesAutoresizingMaskIntoConstraints = false,
                AccessibilityIdentifier = "SearchView_SearchBar"
            };

            this.searchBar.SearchButtonClicked += this.SearchBarSearchButtonClicked;
            this.searchBar.CancelButtonClicked += this.SearchBarOnCancelButtonClicked;

            this.View.AddSubview(this.searchBar);
            this.View.AddConstraint(NSLayoutConstraint.Create(this.searchBar, NSLayoutAttribute.Top, NSLayoutRelation.Equal, this.TopLayoutGuide, NSLayoutAttribute.Bottom, 1.0f, 0.0f));

            this.View.AddConstraints(
                                this.searchBar.WithSameWidth(this.View),
                                this.searchBar.WithSameCenterX(this.View));
        }

        /// <summary>
        /// Creates the "Enter Select Mode" toolbar.
        /// </summary>
        private void CreateEnterSelectModeToolbar()
        {
            this.SetToolbarItems(
                           new UIBarButtonItem[]
                            { 
                                new UIBarButtonItem(UIBarButtonSystemItem.FlexibleSpace) { Width = 50 },
                                new UIBarButtonItem(
                                                    "Select", 
                                                    UIBarButtonItemStyle.Plain, 
                                                    this.EnterSelectModeClick)
                            },
                           true);
        }

        /// <summary>
        /// Puts the view in "Selectable Mode". i.e. A toolbar with a Download and Cancel option
        /// </summary>
        private void CreateSelectableMode()
        {
            this.searchResultsTable.SetEditing(true, true);
            UIBarButtonItem downloadButtonItem = new UIBarButtonItem(
                                                                    "Download",
                                                                    UIBarButtonItemStyle.Plain,
                                                                    this.DownloadClick);

            UIBarButtonItem cancelSelectModeButtonItem = new UIBarButtonItem("Cancel", UIBarButtonItemStyle.Plain, this.CancelSelectModeClick);

            this.SetToolbarItems(
                           new UIBarButtonItem[]
                            { 
                                downloadButtonItem,
                                new UIBarButtonItem(UIBarButtonSystemItem.FlexibleSpace) { Width = 50 },
                                cancelSelectModeButtonItem
                            },
                           true);
        }

        /// <summary>
        /// Searches the bar on cancel button clicked.
        /// </summary>
        /// <param name="sender">The sender.</param>
        /// <param name="eventArgs">The <see cref="EventArgs"/> instance containing the event data.</param>
        private void SearchBarOnCancelButtonClicked(object sender, EventArgs eventArgs)
        {
            this.SearchViewModel.CurrentSearchCriteria = string.Empty;
            this.BeginInvokeOnMainThread(() => this.searchBar.ResignFirstResponder());
        }

        /// <summary>
        /// Handles the SearchButtonClicked event of the SearchBar control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="EventArgs"/> instance containing the event data.</param>
        private void SearchBarSearchButtonClicked(object sender, EventArgs e)
        {
            this.SearchViewModel.SearchCommand.Execute(null);
            this.BeginInvokeOnMainThread(() => this.searchBar.ResignFirstResponder());
            this.searchResultsTable.ReloadData();
        }

        /// <summary>
        /// Handles user clicking Download
        /// </summary>
        /// <param name="sender">The sender.</param>
        /// <param name="eventArgs">The <see cref="EventArgs"/> instance containing the event data.</param>
        private void DownloadClick(object sender, EventArgs eventArgs)
        {
            this.CancelSelectModeClick(null, null);
        }

        /// <summary>
        /// Handles user clicking Select in Toolbar
        /// </summary>
        /// <param name="sender">The sender.</param>
        /// <param name="eventArgs">The <see cref="EventArgs"/> instance containing the event data.</param>
        private void EnterSelectModeClick(object sender, EventArgs eventArgs)
        {
            this.CreateSelectableMode();
        }

        /// <summary>
        /// Handles user clicking Cancel in SelectMode Toolbar
        /// </summary>
        /// <param name="sender">The sender.</param>
        /// <param name="eventArgs">The <see cref="EventArgs"/> instance containing the event data.</param>
        private void CancelSelectModeClick(object sender, EventArgs eventArgs)
        {
            this.searchResultsTable.SetEditing(false, true);
            this.CreateEnterSelectModeToolbar();
        }
    }
}