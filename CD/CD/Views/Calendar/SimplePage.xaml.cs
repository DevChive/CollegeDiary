using System;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using Rg.Plugins.Popup.Services;
using CD.Helper;
using Syncfusion.SfSchedule.XForms;
using CD.Models.Calendar;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace CD.Views.Calendar
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class SimplePage : ContentPage
    {
        readonly FireBaseHelperCalendarEvents fireBaseHelperEvents = new FireBaseHelperCalendarEvents();
        private List<EventModel> listEvents;
        public static SimplePage Instance;

        [Obsolete]
        public SimplePage()
        {
            Instance = this;
            InitializeComponent();

            // tpping an appointment
            schedule.MonthInlineAppointmentTapped += Schedule_MonthInlineAppointmentTapped;
            async void Schedule_MonthInlineAppointmentTapped(object sender, MonthInlineAppointmentTappedEventArgs args)
            {
                if (args.Appointment != null)
                {
                    var appointment = (args.Appointment as ScheduleAppointment);
                    await PopupNavigation.PushAsync(new EventSelected(appointment));
                }
            }
            refreshCalendar();
        }
        protected override async void OnAppearing()
        {
            base.OnAppearing();
            var theListOfEvents = await fireBaseHelperEvents.GetAllEvents();
            listEvents = theListOfEvents;
        }
        public async Task refreshCalendar()
        {
            listEvents = await fireBaseHelperEvents.GetAllEvents();
            ScheduleAppointmentCollection scheduleAppointmentCollection = new ScheduleAppointmentCollection();
            if (listEvents.Count == 0)
            {
                schedule.DataSource = scheduleAppointmentCollection;
            }
            else
            {
                foreach (EventModel ev in listEvents)
                {
                    DateTime startDate = Convert.ToDateTime(ev.StartEventDate.ToString());
                    DateTime endDate = Convert.ToDateTime(ev.EndEventDate.ToString());

                    //Console.WriteLine("---------------------- ev->" + ev.EventDate.ToString() + "  -------------------- start_Date->" + start_Date.ToString());
                    scheduleAppointmentCollection.Add(new ScheduleAppointment()
                    {
                        BindingContext = this,
                        StartTime = startDate,
                        EndTime = endDate,
                        Subject = ev.Name,
                        Notes = ev.Description,
                        Color = ev.Color,
                    });
                    schedule.DataSource = scheduleAppointmentCollection;
                }
            }
        }

        [Obsolete]
        private async void AddEvent(object sender, EventArgs e)
        {
            add_calendar_event_button.IsEnabled = false;
            DateTime dateSelected = Convert.ToDateTime(string.IsNullOrEmpty( schedule.SelectedDate.ToString()) ? DateTime.Now.ToString(): schedule.SelectedDate.ToString());
            await PopupNavigation.PushAsync(new AddCalendarEvent(dateSelected));
            add_calendar_event_button.IsEnabled = true;
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