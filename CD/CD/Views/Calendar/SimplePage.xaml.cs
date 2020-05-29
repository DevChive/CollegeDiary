using System;
using CD.ViewModel;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using Rg.Plugins.Popup.Services;
using CD.Helper;
using Syncfusion.SfSchedule.XForms;

namespace CD.Views.Calendar
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class SimplePage : ContentPage
    {
        readonly FireBaseHelperCalendarEvents fireBaseHelper = new FireBaseHelperCalendarEvents();
        public SimplePage()
        {
            InitializeComponent();

            schedule.CellTapped += CellTappedEventHandler;
            void CellTappedEventHandler(object sender, CellTappedEventArgs e)
            {
                PopupNavigation.PushAsync(new DayView());
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