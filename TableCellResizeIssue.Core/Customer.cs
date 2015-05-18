using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace TableCellResize.Core.ViewModels
{
    using System.ComponentModel;

    public class Customer : INotifyPropertyChanged
    {
        public string DisplayPersonName { get; set; }

        public string DisplayDateOfBirth { get; set; }

        public event PropertyChangedEventHandler PropertyChanged;
    }
}
