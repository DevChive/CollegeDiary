using System;
using CD.ViewModel;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using Rg.Plugins.Popup.Services;
using CD.Helper;
using Syncfusion.SfSchedule.XForms;
using CD.Models.Calendar;
using System.Collections.Generic;

namespace CD.Views.Calendar
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class SimplePage : ContentPage
    {
        readonly FireBaseHelperCalendarEvents fireBaseHelperEvents = new FireBaseHelperCalendarEvents();
        private List<EventModel> listEvents;
        private DateTime date;

        public SimplePage()
        {
            InitializeComponent();

            schedule.CellTapped += CellTappedEventHandler;
            void CellTappedEventHandler(object sender, CellTappedEventArgs e)
            {
                date = Convert.ToDateTime(e.Datetime.ToString());
                schedule.ShowAppointmentsInline = true;
                trying();
            }
        }
        protected override async void OnAppearing()
        {
            base.OnAppearing();
            var theListOfEvents = await fireBaseHelperEvents.GetAllEvents();
            listEvents = theListOfEvents;
        }
        private async void trying()
        {
            listEvents = await fireBaseHelperEvents.GetAllEvents();
            foreach(EventModel ev in listEvents)
            {
                // TODO: here, match the times and bring the events to the app -  check firefox last TAB!!!
                Console.WriteLine("---------------------- ev->" + ev.EventDate.ToString() + "  -------------------- date->" + date.ToString());
            }
        }

        private void AddEvent(object sender, EventArgs e)
        {
            DateTime dateSelected = Convert.ToDateTime(schedule.SelectedDate.ToString());
            PopupNavigation.PushAsync(new AddCalendarEvent(dateSelected));
        }

        public static string[] parseDate(DateTime date)
        {
            var day = date.Day.ToString();
            var month = date.Month.ToString();
            var year = date.Year.ToString();
            string[] parsedDate = { day, month, year };
            return parsedDate;
        }
    }
}