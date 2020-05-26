using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CD.ViewModel.Calendar;
using java.time;
using CD.ViewModel.Calendar;
using CD.Models.Calendar;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using com.sun.xml.@internal.fastinfoset.util;

namespace CD.Views.Calendar
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class SimplePage : ContentPage
    {
        public SimplePage()
        {
            InitializeComponent();
        }

        private void AddEvent(object sender, EventArgs e)
        {
            string date = SimplePageViewModel.theSelectedDate().ToString();
            String[] theDate = parseDate(date);
            Console.WriteLine("--------------------------   Add Event Pressed: " + theDate[0] + "/" + theDate[1] + "/" + theDate[2]);
        }

        private String[] parseDate(string date)
        {
            DateTime theDate = (Convert.ToDateTime(date));
            String day = theDate.Day.ToString();
            String month = theDate.Month.ToString();
            String year = theDate.Year.ToString();
            String[] parsedDate = { day, month, year };
            return parsedDate;
        }
    }
}