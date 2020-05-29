using CD.Helper;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;

namespace CD.ViewModel
{
    public class ZZZ : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;

        private static ZZZ _instance;

        public static ZZZ Instance => _instance ?? (_instance = new ZZZ());

        public ObservableRangeCollection<Models.Calendar.EventModel> Events { get; set; } = new ObservableRangeCollection<Models.Calendar.EventModel>();
    }
}
