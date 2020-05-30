using Xamarin.Forms.Xaml;
using Rg.Plugins.Popup.Services;
using System;
using CD.Helper;

namespace CD.Views.Calendar
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class AddCalendarEvent
    {
        private DateTime date;
        readonly FireBaseHelperCalendarEvents fireBaseHelper = new FireBaseHelperCalendarEvents();
        public AddCalendarEvent(DateTime selectedDate)
        {
            InitializeComponent();
            string[] theDate = SimplePage.parseDate(selectedDate);
            TheDaySelected.Date = selectedDate;
            date = selectedDate;
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
            DateTime date = new DateTime(TheDaySelected.Date.Year, TheDaySelected.Date.Month, TheDaySelected.Date.Day, timePicker.Time.Hours, timePicker.Time.Minutes, timePicker.Time.Seconds);

            if (!string.IsNullOrEmpty(name))
            {
                await fireBaseHelper.AddEvent(name, desc, date);
                await DisplayAlert("Success", "Event " + "'" + name +"'" + " added on " 
                    +  date.Date.Day.ToString() + "/" + date.Date.Month.ToString() + "/" + date.Date.Year.ToString() + 
                    " at " + date.Hour.ToString() + ":" + date.Minute.ToString(), "OK");
                await PopupNavigation.RemovePageAsync(this);
            }
            else
                await DisplayAlert("Failed", "Please add a name to the event", "OK");
        }

        private async void Cancel_Event(object sender, System.EventArgs e)
        {
           await PopupNavigation.RemovePageAsync(this);
        }

        private void OnTimePickerPropertyChanged(object sender, System.ComponentModel.PropertyChangedEventArgs e)
        {

        }
    }
}