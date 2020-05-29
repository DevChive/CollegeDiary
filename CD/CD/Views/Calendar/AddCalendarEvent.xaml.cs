using Xamarin.Forms.Xaml;
using Rg.Plugins.Popup.Services;
using System;
using CD.Helper;

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
            string date = TheDaySelected.Date.Day + "/" + TheDaySelected.Date.Month + "/" + TheDaySelected.Date.Year;
            //TimeSpan time = timePicker.Time;
            if (!string.IsNullOrEmpty(name))
            {
               // await fireBaseHelper.AddEvent(name, desc, DateTime.Parse(date), time);
                //await DisplayAlert("Success", "Event " + "'" + name +"'" + " added on \n" 
                //    + date + " at " + time.Hours.ToString() + ":" + time.Minutes.ToString(), "OK");
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