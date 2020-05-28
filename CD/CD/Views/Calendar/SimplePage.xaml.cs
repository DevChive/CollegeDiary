using System;
using CD.ViewModel.Calendar;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using Rg.Plugins.Popup.Services;

namespace CD.Views.Calendar
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class SimplePage : ContentPage
    {
        public string DateSelected = "";
        public SimplePage()
        {
            InitializeComponent();
        }

        private void AddEvent(object sender, EventArgs e)
        {
            PopupNavigation.PushAsync(new AddCalendarEvent(SimplePageViewModel.theSelectedDate()));
        }

        public static String[] parseDate(DateTime date)
        {
            String day = date.Day.ToString();
            String month = date.Month.ToString();
            String year = date.Year.ToString();
            String[] parsedDate = { day, month, year };
            return parsedDate;
        }
    }
}