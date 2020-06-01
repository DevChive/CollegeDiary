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
        public static SimplePage Instance;

        public SimplePage()
        {
            Instance = this;
            InitializeComponent();
            //////////////////////////////////////////////////////////////////////////////////////////////////////
            schedule.MonthInlineAppointmentTapped += Schedule_MonthInlineAppointmentTapped;
            void Schedule_MonthInlineAppointmentTapped(object sender, MonthInlineAppointmentTappedEventArgs args)
            {
                if (args.Appointment != null)
                {
                    var appointment = (args.Appointment as ScheduleAppointment);
                    DisplayAlert(appointment.Subject, appointment.StartTime.ToString(), "ok");
                    //TODO: add a pop up,, with a form filled in with appointment details and a delete button
                }
                else
                {
                    DisplayAlert("", "No Events", "ok");
                }
            }
            //////////////////////////////////////////////////////////////////////////////////////////////////////
            schedule.CellTapped += CellTappedEventHandler;
            void CellTappedEventHandler(object sender, CellTappedEventArgs e)
            {
                date = Convert.ToDateTime(e.Datetime.ToString());
                schedule.ShowAppointmentsInline = true;
            }
            addingAnAppointment();
        }
        protected override async void OnAppearing()
        {
            base.OnAppearing();
            var theListOfEvents = await fireBaseHelperEvents.GetAllEvents();
            listEvents = theListOfEvents;
        }
        public async void addingAnAppointment()
        {
            listEvents = await fireBaseHelperEvents.GetAllEvents();
            ScheduleAppointmentCollection scheduleAppointmentCollection = new ScheduleAppointmentCollection();
            foreach (EventModel ev in listEvents)
            {
                // TODO: here, match the times and bring the events to the app -  check firefox last TAB!!!
                DateTime startDate = Convert.ToDateTime(ev.StartEventDate.ToString());
                DateTime endDate = Convert.ToDateTime(ev.EndEventDate.ToString());

                //Console.WriteLine("---------------------- ev->" + ev.EventDate.ToString() + "  -------------------- start_Date->" + start_Date.ToString());
                scheduleAppointmentCollection.Add(new ScheduleAppointment()
                {
                    StartTime =  startDate,
                    EndTime = endDate,
                    Subject = ev.Name,
                    Location = ev.Description,
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
    }
}