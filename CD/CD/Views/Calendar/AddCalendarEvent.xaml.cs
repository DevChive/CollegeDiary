using Xamarin.Forms.Xaml;
using Rg.Plugins.Popup.Services;
using System;
using CD.Helper;
using System.Drawing;

namespace CD.Views.Calendar
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class AddCalendarEvent
    {
        private DateTime start_Date;
        private DateTime end_Date;
        readonly FireBaseHelperCalendarEvents fireBaseHelper = new FireBaseHelperCalendarEvents();
        private Random rnd = new Random();

        public AddCalendarEvent(DateTime selectedDate)
        {
            InitializeComponent();
            string[] theDate = SimplePage.parseDate(selectedDate);

            // date selected displayed in the pop-up form
            startDate.Date = selectedDate;
            endDate.Date = selectedDate;
            start_Date = selectedDate;
        }
        protected override void OnAppearing()
        {
            base.OnAppearing();
        }

        private async void Save_Event(object sender, System.EventArgs e)
        {
            string name = event_name.Text;
            string desc = event_description.Text;
            DateTime start_Date = new DateTime(startDate.Date.Year, startDate.Date.Month, startDate.Date.Day, startTimePicker.Time.Hours, startTimePicker.Time.Minutes, startTimePicker.Time.Seconds);
            DateTime end_Date = new DateTime(endDate.Date.Year, endDate.Date.Month, endDate.Date.Day, endTimePicker.Time.Hours, endTimePicker.Time.Minutes, endTimePicker.Time.Seconds);
            if (!string.IsNullOrEmpty(name))
            {
                await fireBaseHelper.AddEvent(name, desc, start_Date, end_Date);
                //await DisplayAlert("Success", "Event " + "'" + name +"'" + " added on " 
                //    +  date.Date.Day.ToString() + "/" + date.Date.Month.ToString() + "/" + date.Date.Year.ToString() + 
                //    " at " + date.Hour.ToString() + ":" + date.Minute.ToString(), "OK");
                // TODO: change the message, is very bad
                await PopupNavigation.RemovePageAsync(this);
            }
            else
                await DisplayAlert("Failed", "Please add a name to the event", "OK");

            // repopulating the calendar
            SimplePage.Instance.addingAnAppointment();
        }

        private async void Cancel_Event(object sender, System.EventArgs e)
        {
           await PopupNavigation.RemovePageAsync(this);
        }

        private void OnTimePickerPropertyChanged(object sender, System.ComponentModel.PropertyChangedEventArgs e)
        {
            TimeSpan addingTime = new TimeSpan(0, 1, 0, 0);
            endTimePicker.Time = startTimePicker.Time.Add(addingTime);
        }
    }
}