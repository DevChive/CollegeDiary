using System;
using System.Collections.Generic;
using Rg.Plugins.Popup.Services;
using CD.Models.Calendar;
using CD.Helper;
using Xamarin.Forms.Xaml;
using Syncfusion.SfSchedule.XForms;

namespace CD.Views.Calendar
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class EventSelected 
    {
        readonly FireBaseHelperCalendarEvents fireBaseHelperEvents = new FireBaseHelperCalendarEvents();
        private List<EventModel> listEvents;
        private ScheduleAppointment appointment;
        string sourcePage = "";
        public EventSelected(ScheduleAppointment args, string motiv)
        {
            InitializeComponent();
            AppointmentSubject.Text = args.Subject;
            SubjectFrame.BackgroundColor = args.Color;
            AppointmentDescription.Text = args.Notes;
            StartingDate.Text = args.StartTime.Date.ToLongDateString();
            StartingDate.TextColor = args.Color;
            StartingTime.Text = args.StartTime.ToShortTimeString();
            EndDate.Text = args.EndTime.Date.ToLongDateString();
            EndDate.TextColor = args.Color;
            EndTime.Text = args.EndTime.ToShortTimeString();

            appointment = args;
            sourcePage = motiv;
        }
        protected override void OnAppearing()
        {
            base.OnAppearing();
            BackgroundColor = System.Drawing.Color.FromArgb(200, 0, 0, 0);
        }

        [Obsolete]
        private async void delete_event(object sender, EventArgs e)
        {
            delete_event_button.IsEnabled = false;
            listEvents = await fireBaseHelperEvents.GetAllEvents();
            EventModel theEvent = new EventModel();
            foreach (EventModel ev in listEvents)
            {
                DateTime startDate = Convert.ToDateTime(ev.StartEventDate.ToString());
                DateTime endDate = Convert.ToDateTime(ev.EndEventDate.ToString());

                if (ev.Name == appointment.Subject && ev.Description == appointment.Notes
                    && startDate.Date.ToLongDateString() == appointment.StartTime.ToLongDateString()
                    && endDate.Date.ToLongDateString() == appointment.EndTime.ToLongDateString())
                {
                    theEvent = ev;
                }
            }
            try
            {
                await fireBaseHelperEvents.DeleteEvent(theEvent.EventID);
                if (sourcePage == "SimplePage")
                {
                    await PopupNavigation.RemovePageAsync(this);
                    await SimplePage.Instance.refreshCalendar();
                }
                else if (sourcePage == "MyAccount")
                {
                    await Navigation.PushAsync(new MyAccount(), false);
                    Navigation.RemovePage(Navigation.NavigationStack[Navigation.NavigationStack.Count - 1]);
                    await PopupNavigation.RemovePageAsync(this);
                }
            }
            catch (Exception)
            {
            }
            await SimplePage.Instance.refreshCalendar();
            delete_event_button.IsEnabled = true;
        }

        [Obsolete]
        private async void cancel_event(object sender, EventArgs e)
        {
            cancel_event_button.IsEnabled = false;
            await PopupNavigation.RemovePageAsync(this);
            cancel_event_button.IsEnabled = false;
        }

        [Obsolete]
        private async void EditEvent(object sender, EventArgs e)
        {
            edit_event_button.IsEnabled = false;
            await PopupNavigation.RemovePageAsync(this);
            await PopupNavigation.PushAsync(new EditCalendarEvent(appointment, sourcePage));
            edit_event_button.IsEnabled = true;
        }
    }
}