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
            save_button.IsEnabled = false;
            string name = event_name.Text;
            string desc = event_description.Text;
            DateTime start_Date = new DateTime(startDate.Date.Year, startDate.Date.Month, startDate.Date.Day, startTimePicker.Time.Hours, startTimePicker.Time.Minutes, startTimePicker.Time.Seconds);
            DateTime end_Date = new DateTime(endDate.Date.Year, endDate.Date.Month, endDate.Date.Day, endTimePicker.Time.Hours, endTimePicker.Time.Minutes, endTimePicker.Time.Seconds);
            if (!string.IsNullOrEmpty(name))
            {
                await fireBaseHelper.AddEvent(name, desc, start_Date, end_Date);
                await PopupNavigation.RemovePageAsync(this);
            }
            else
                await DisplayAlert("Failed", "Please add a name to the event", "OK");
            save_button.IsEnabled = true;

            // repopulating the calendar
            await SimplePage.Instance.refreshCalendar();
        }

        private async void Cancel_Event(object sender, System.EventArgs e)
        {
            cancel_button.IsEnabled = false;
            await PopupNavigation.RemovePageAsync(this);
            cancel_button.IsEnabled = true;
        }

        private void OnTimePickerPropertyChanged(object sender, System.ComponentModel.PropertyChangedEventArgs e)
        {
        }
    }
}