using Cirrious.MvvmCross.ViewModels;

namespace TableCellResizeIssue.Core.ViewModels
{
    using System;
    using System.Collections.Generic;
    using System.Diagnostics;
    using System.Windows.Input;

    using TableCellResize.Core.ViewModels;

    public class SearchViewModel : MvxViewModel
    {
        /// <summary>
        /// The search results
        /// </summary>
        private List<ItemViewModel<Customer>> searchResults;

        /// <summary>
        /// The current search criteria
        /// </summary>
        private string currentSearchCriteria;

        /// <summary>
        /// The search command
        /// </summary>
        private ICommand executeSearchCommand;

        /// <summary>
        /// Initializes a new instance of the <see cref="SearchViewModel"/> class.
        /// </summary>
        public SearchViewModel()
        {
            this.CurrentSearchCriteria = string.Empty;
            this.searchResults = new List<ItemViewModel<Customer>>();
        }

        /// <summary>
        /// Gets the Search command
        /// </summary>
        public new ICommand SearchCommand
        {
            get
            {
                return this.executeSearchCommand ?? (this.executeSearchCommand = new MvxCommand(this.DoSearch));
            }
        }

        /// <summary>
        /// Gets or sets a the current search criteria
        /// </summary>
        /// <value>The current search criteria.</value>
        public string CurrentSearchCriteria
        {
            get
            {
                return this.currentSearchCriteria;
            }

            set
            {
                this.currentSearchCriteria = value;
                this.RaisePropertyChanged(() => this.CurrentSearchCriteria);
            }
        }
        
        /// <summary>
        /// Gets or sets the list of resolved customers from the last search
        /// </summary>
        public List<ItemViewModel<Customer>> SearchResults
        {
            get
            {
                return this.searchResults;
            }

            set
            {
                this.searchResults = value;
                this.RaisePropertyChanged(() => this.SearchResults);
            }
        }

        /// <summary>
        /// The ViewModels initialization method
        /// </summary>
        /// <param name="searchCriteria">The search criteria</param>
        public void Init(string searchCriteria)
        {
            this.CurrentSearchCriteria = string.IsNullOrEmpty(searchCriteria) ? string.Empty : searchCriteria;
        }

        /// <summary>
        /// Give control to the Search view
        /// </summary>
        private void DoSearch()
        {
            this.RefreshSearchResults();
        }

        /// <summary>
        /// Builds the search results list for the current search criteria
        /// </summary>
        private async void RefreshSearchResults()
        {
            // Reset the current search results and results summary
            this.SearchResults = new List<ItemViewModel<Customer>>();

            try
            {
                if (this.CurrentSearchCriteria.ToLower() == "smi")
                {
                    this.SearchResults = this.SearchForSmi();
                }
                else if (this.CurrentSearchCriteria.ToLower() == "this")
                {
                    this.SearchResults = this.SearchForThis();
                }
                else if (this.CurrentSearchCriteria.ToLower() == "thi")
                {
                    this.SearchResults = this.SearchForThi();   
                }
                else
                {
                    this.SearchResults = new List<ItemViewModel<Customer>>();
                }
            }
            catch (Exception ex)
            {
                Debug.WriteLine("Error during search. Message: {0}", ex.Message);
            }
        }

        private List<ItemViewModel<Customer>> SearchForThi()
        {
            List<ItemViewModel<Customer>> results = new List<ItemViewModel<Customer>>();

            results.Add(new ItemViewModel<Customer>()
            {
                Model = new Customer() { DisplayDateOfBirth = "04-May-1954 (61y)", DisplayPersonName = "THI, Robert", TextList = new List<string>() { "Hello", "World" } }
            });

            results.Add(new ItemViewModel<Customer>()
            {
                Model = new Customer() { DisplayDateOfBirth = "04-May-1954 (61y)", DisplayPersonName = "THI, Sheila" }
            });

            results.Add(new ItemViewModel<Customer>()
            {
                Model = new Customer() { DisplayDateOfBirth = "04-May-1954 (61y)", DisplayPersonName = "THIBAUT, Alexander", TextList = new List<string>() { "Hello", "World", "Goodbye Cruel World" } }
            });

            results.Add(new ItemViewModel<Customer>()
            {
                Model = new Customer() { DisplayDateOfBirth = "04-May-1954 (61y)", DisplayPersonName = "THISISAcustomerWITHA-VERYLONGSUNRNAME, Thisisacustomerwitha-verylong firstname" }
            });

            return results;
        }

        private List<ItemViewModel<Customer>> SearchForThis()
        {
            List<ItemViewModel<Customer>> results = new List<ItemViewModel<Customer>>();

            results.Add(new ItemViewModel<Customer>()
            {
                Model = new Customer() { DisplayDateOfBirth = "04-May-1954 (61y)", DisplayPersonName = "THISISAcustomerWITHA-VERYLONGSUNRNAME, Thisisacustomerwitha-verylong firstname" }
            });

            return results;
        }

        private List<ItemViewModel<Customer>> SearchForSmi()
        {
            List<ItemViewModel<Customer>> results = new List<ItemViewModel<Customer>>();

            results.Add(new ItemViewModel<Customer>()
                            {
                                Model = new Customer() { DisplayDateOfBirth = "30-Oct-1974 (78y)", DisplayPersonName = "Smith Barbara" }
                            });

            results.Add(new ItemViewModel<Customer>()
            {
                Model = new Customer() { DisplayDateOfBirth = "30-Oct-1974 (78y)", DisplayPersonName = "Smith Chris" }
            });

            results.Add(new ItemViewModel<Customer>()
            {
                Model = new Customer() { DisplayDateOfBirth = "30-Oct-1974 (78y)", DisplayPersonName = "Smith Daniel" }
            });

            results.Add(new ItemViewModel<Customer>()
            {
                Model = new Customer() { DisplayDateOfBirth = "30-Oct-1974 (78y)", DisplayPersonName = "Smith Eugene" }
            });

            results.Add(new ItemViewModel<Customer>()
            {
                Model = new Customer() { DisplayDateOfBirth = "30-Oct-1974 (78y)", DisplayPersonName = "Smith Felix", TextList = new List<string>() { "Apple", "Orange", "Peach", "Plum", "All from the green grocer" } }
            });

            results.Add(new ItemViewModel<Customer>()
            {
                Model = new Customer() { DisplayDateOfBirth = "30-Oct-1974 (78y)", DisplayPersonName = "Smith Gregory" }
            });

            results.Add(new ItemViewModel<Customer>()
            {
                Model = new Customer() { DisplayDateOfBirth = "30-Oct-1974 (78y)", DisplayPersonName = "Smith Hazel" }
            });

            results.Add(new ItemViewModel<Customer>()
            {
                Model = new Customer() { DisplayDateOfBirth = "30-Oct-1974 (78y)", DisplayPersonName = "Smith Ian" }
            });

            results.Add(new ItemViewModel<Customer>()
            {
                Model = new Customer() { DisplayDateOfBirth = "30-Oct-1974 (78y)", DisplayPersonName = "Smith Jessica" }
            });

            results.Add(new ItemViewModel<Customer>()
            {
                Model = new Customer() { DisplayDateOfBirth = "30-Oct-1974 (78y)", DisplayPersonName = "Smith Kevin" }
            });

            results.Add(new ItemViewModel<Customer>()
            {
                Model = new Customer() { DisplayDateOfBirth = "30-Oct-1974 (78y)", DisplayPersonName = "Smith Liam" }
            });

            results.Add(new ItemViewModel<Customer>()
            {
                Model = new Customer() { DisplayDateOfBirth = "30-Oct-1974 (78y)", DisplayPersonName = "Smith Matheus" }
            });

            results.Add(new ItemViewModel<Customer>()
            {
                Model = new Customer() { DisplayDateOfBirth = "30-Oct-1974 (78y)", DisplayPersonName = "Smith Norah" }
            });

            results.Add(new ItemViewModel<Customer>()
            {
                Model = new Customer() { DisplayDateOfBirth = "30-Oct-1974 (78y)", DisplayPersonName = "Smith Oscar" }
            });

            results.Add(new ItemViewModel<Customer>()
            {
                Model = new Customer() { DisplayDateOfBirth = "30-Oct-1974 (78y)", DisplayPersonName = "Smith Penelope" }
            });

            results.Add(new ItemViewModel<Customer>()
            {
                Model = new Customer() { DisplayDateOfBirth = "30-Oct-1974 (78y)", DisplayPersonName = "Smith Quentin" }
            });

            results.Add(new ItemViewModel<Customer>()
            {
                Model = new Customer() { DisplayDateOfBirth = "30-Oct-1974 (78y)", DisplayPersonName = "Smith Robert" }
            });

            results.Add(new ItemViewModel<Customer>()
            {
                Model = new Customer() { DisplayDateOfBirth = "30-Oct-1974 (78y)", DisplayPersonName = "Smith Sam" }
            });

            results.Add(new ItemViewModel<Customer>()
            {
                Model = new Customer() { DisplayDateOfBirth = "30-Oct-1974 (78y)", DisplayPersonName = "Smith Tim" }
            });

            results.Add(new ItemViewModel<Customer>()
            {
                Model = new Customer() { DisplayDateOfBirth = "30-Oct-1974 (78y)", DisplayPersonName = "Smith Ursula" }
            });

            results.Add(new ItemViewModel<Customer>()
            {
                Model = new Customer() { DisplayDateOfBirth = "30-Oct-1974 (78y)", DisplayPersonName = "Smith Victoria" }
            });

            results.Add(new ItemViewModel<Customer>()
            {
                Model = new Customer() { DisplayDateOfBirth = "30-Oct-1974 (78y)", DisplayPersonName = "Smith William (Willy)" }
            });

            results.Add(new ItemViewModel<Customer>()
            {
                Model = new Customer() { DisplayDateOfBirth = "30-Oct-1974 (78y)", DisplayPersonName = "Smith Xavier" }
            });

            results.Add(new ItemViewModel<Customer>()
            {
                Model = new Customer() { DisplayDateOfBirth = "30-Oct-1974 (78y)", DisplayPersonName = "Smith Yanick" }
            });

            results.Add(new ItemViewModel<Customer>()
            {
                Model = new Customer() { DisplayDateOfBirth = "30-Oct-1974 (78y)", DisplayPersonName = "Smith Zach" }
            });

            return results;
        }

    }
}
