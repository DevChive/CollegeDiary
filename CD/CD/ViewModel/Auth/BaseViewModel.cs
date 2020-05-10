using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;

namespace CD.ViewModel.Auth
{
    public class BaseViewModel : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;
        public bool IsBusy { get; protected set; }
    }
}
