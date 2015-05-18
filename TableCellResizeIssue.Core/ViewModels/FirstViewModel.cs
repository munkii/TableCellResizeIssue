using Cirrious.MvvmCross.ViewModels;

namespace TableCellResizeIssue.Core.ViewModels
{
    using System.Windows.Input;

    /// <summary>
    /// First VM
    /// </summary>
    public class FirstViewModel : MvxViewModel
    {
        /// <summary>
        /// Gets the Search command
        /// </summary>
        public MvxCommand GoToSearchCommand
        {
            get
            {
                return new MvxCommand(() => this.ShowViewModel<SearchViewModel>());
            }
        }

        public void DoSomething()
        {
            this.ShowViewModel<SearchViewModel>();
        }
    }
}
