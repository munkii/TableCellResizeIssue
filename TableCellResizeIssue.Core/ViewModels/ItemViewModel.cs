using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TableCellResizeIssue.Core.ViewModels
{
    using System.Windows.Input;

    using Cirrious.CrossCore;
    using Cirrious.MvvmCross.ViewModels;
    using Cirrious.MvvmCross.Views;

    /// <summary>
    /// Simple ViewModel used to wrap a Model that is used as an Item in a List
    /// </summary>
    /// <typeparam name="T">The type</typeparam>
    public class ItemViewModel<T> : MvxNotifyPropertyChanged where T : new()
    {
        /// <summary>
        /// Is the Item selected
        /// </summary>
        private bool isSelected;

        /// <summary>
        /// The click command
        /// </summary>
        private MvxCommand clickCommand;

        /// <summary>
        /// The model.
        /// </summary>
        private T model;

        /// <summary>
        /// Initializes a new instance of the <see cref="ItemViewModel&lt;T&gt;"/> class.
        /// </summary>
        public ItemViewModel()
        {
            this.ClickCommand = new MvxCommand(this.ClickCommandHandler, this.CanClick);
        }

        /// <summary>
        /// Gets or sets the model.
        /// </summary>
        /// <value>
        /// The model.
        /// </value>
        public T Model
        {
            get
            {
                return this.model;
            }

            set
            {
                this.model = value;
                this.RaisePropertyChanged(() => this.Model);
            }
        }

        /// <summary>
        /// Gets or sets a value indicating whether this instance is selected.
        /// </summary>
        /// <value>
        /// <c>true</c> if this instance is selected; otherwise, <c>false</c>.
        /// </value>
        public bool IsSelected
        {
            get
            {
                return this.isSelected;
            }

            set
            {
                this.isSelected = value;
                this.RaisePropertyChanged(() => this.IsSelected);
            }
        }

        /// <summary>
        /// Gets or sets the click command.
        /// </summary>
        /// <value>
        /// The click command.
        /// </value>
        public MvxCommand ClickCommand
        {
            get
            {
                return this.clickCommand;
            }

            set
            {
                this.clickCommand = value;
                this.RaisePropertyChanged(() => this.ClickCommand);
            }
        }

        /// <summary>
        /// Gets or sets a value indicating whether or not the click command can be executed
        /// </summary>
        /// <returns>True if the Click command can be executed</returns>
        public virtual bool CanClick()
        {
            return true;
        }

        /// <summary>
        /// Handle the Click command
        /// </summary>
        protected virtual void ClickCommandHandler()
        {
            // Do nothing in this repro
        }
    }
}
