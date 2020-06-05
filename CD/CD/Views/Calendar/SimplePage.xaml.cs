using System;
using CD.ViewModel;
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
        private DateTime date;
        public static SimplePage Instance;

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
                    
                    var result = await DisplayAlert(appointment.Subject, appointment.Notes + "\n"+ 
                        "\nDate: " + appointment.StartTime.Date.ToLongDateString() + 
                        "\nTime: " + appointment.StartTime.TimeOfDay.ToString(@"hh\:mm")
                        ,"Delete", "OK");
                    if (result) // if it's equal to OK
                    {
                        DeleteEvent(appointment);
                    }
                    else // if it's equal to DELETE
                    {
                        return; // just return to the page and do nothing.
                    }

                }
            }
            // taping a day
            schedule.CellTapped += CellTappedEventHandler;
            void CellTappedEventHandler(object sender, CellTappedEventArgs e)
            {
                date = Convert.ToDateTime(e.Datetime.ToString());
                schedule.ShowAppointmentsInline = true;
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
            foreach (EventModel ev in listEvents)
            {
                DateTime startDate = Convert.ToDateTime(ev.StartEventDate.ToString());
                DateTime endDate = Convert.ToDateTime(ev.EndEventDate.ToString());

                //Console.WriteLine("---------------------- ev->" + ev.EventDate.ToString() + "  -------------------- start_Date->" + start_Date.ToString());
                scheduleAppointmentCollection.Add(new ScheduleAppointment()
                {
                    BindingContext = this,
                    StartTime =  startDate,
                    EndTime = endDate,
                    Subject = ev.Name,
                    Notes = ev.Description,
                    ReminderTime = ReminderTimeType.TenHours
                });
                schedule.DataSource = scheduleAppointmentCollection;
            }
        }

        private void AddEvent(object sender, EventArgs e)
        {

            DateTime dateSelected = Convert.ToDateTime(string.IsNullOrEmpty( schedule.SelectedDate.ToString()) ? DateTime.Now.ToString(): schedule.SelectedDate.ToString());
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

        private async void DeleteEvent(ScheduleAppointment appointment)
        {
            listEvents = await fireBaseHelperEvents.GetAllEvents();
            EventModel theEvent = new EventModel();
            foreach (EventModel ev in listEvents)
            {
                DateTime startDate = Convert.ToDateTime(ev.StartEventDate.ToString());
                DateTime endDate = Convert.ToDateTime(ev.EndEventDate.ToString());

                if (ev.Name == appointment.Subject && ev.Description == appointment.Notes && startDate.Date.ToLongDateString() == appointment.StartTime.ToLongDateString())
                {
                    theEvent = ev;
                }
            }
            try
            {
                await fireBaseHelperEvents.DeleteEvent(theEvent.EventID);
                await Instance.refreshCalendar();
                await DisplayAlert("Event Deleted", "Event " + theEvent.Name, "OK");
            }
            catch (Exception) 
            {
            }
            Instance.refreshCalendar();
        }
    }
}