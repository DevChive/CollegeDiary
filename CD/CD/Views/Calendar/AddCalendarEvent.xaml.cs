using Xamarin.Forms.Xaml;
using Rg.Plugins.Popup.Services;
using CD.ViewModel.Calendar;
using System;
using CD.Helper;
using System;

namespace CD.Views.Calendar
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class AddCalendarEvent
    {
        readonly FireBaseHelperCalendarEvents fireBaseHelper = new FireBaseHelperCalendarEvents();
        public AddCalendarEvent(DateTime selectedDate)
        {
            InitializeComponent();
            string[] theDate = SimplePage.parseDate(selectedDate);
            TheDaySelected.Date = selectedDate;
            //Console.WriteLine("Date Selected -----------> " + date123.ToString());
        }
        protected override void OnAppearing()
        {
            base.OnAppearing();
        }

        private async void Save_Event(object sender, System.EventArgs e)
        {
            string name = event_name.Text;
            string desc = event_description.Text;
            DateTime date = TheDaySelected.Date;
            TimeSpan time = timePicker.Time;
            await fireBaseHelper.AddEvent(name, desc, date.Date.ToString(), time);
            PopupNavigation.RemovePageAsync(this);
        }

        private void Cancel_Event(object sender, System.EventArgs e)
        {
            PopupNavigation.RemovePageAsync(this);
        }

        private void OnTimePickerPropertyChanged(object sender, System.ComponentModel.PropertyChangedEventArgs e)
        {

        }
    }
}