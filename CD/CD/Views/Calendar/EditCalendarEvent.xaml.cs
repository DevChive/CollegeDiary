using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CD.Helper;
using Rg.Plugins.Popup.Services;
using Syncfusion.SfSchedule.XForms;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace CD.Views.Calendar
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class EditCalendarEvent 
    {
        readonly FireBaseHelperCalendarEvents fireBaseHelper = new FireBaseHelperCalendarEvents();
        private int color = 0;
        public EditCalendarEvent(ScheduleAppointment args)
        {
            InitializeComponent();
            event_name.Text = args.Subject;
            event_description.Text = args.Notes;
            startDate.Date = args.StartTime.Date;
            endDate.Date = args.EndTime.Date;
            startTimePicker.Time = args.StartTime.TimeOfDay;
            endTimePicker.Time = args.EndTime.TimeOfDay;
            segmentedControl.SelectionIndicatorSettings.Color = args.Color;
            color = segmentedControl.SelectedIndex = colorSelected(args.Color);
        }

        private void OnTimePickerPropertyChanged(object sender, System.ComponentModel.PropertyChangedEventArgs e)
        {

        }

        private void Save_Event(object sender, EventArgs e)
        {

        }

        [Obsolete]
        private async void Cancel_Event(object sender, EventArgs e)
        {
            await PopupNavigation.RemovePageAsync(this);
        }

        private void Handle_SelectionChanged(object sender, Syncfusion.XForms.Buttons.SelectionChangedEventArgs e)
        {
            // tap a color on the selection line 
            segmentedControl.SelectionChanged += Handle_SelectionChanged;
            color = segmentedControl.SelectedIndex;
            if (color == 0)
            {
                segmentedControl.SelectionIndicatorSettings.Color = Color.OrangeRed;
            }
            if (color == 1)
            {
                segmentedControl.SelectionIndicatorSettings.Color = Color.Orange;
            }
            if (color == 2)
            {
                segmentedControl.SelectionIndicatorSettings.Color = Color.DeepPink;
            }
            if (color == 3)
            {
                segmentedControl.SelectionIndicatorSettings.Color = Color.DodgerBlue;
            }
            if (color == 4)
            {
                segmentedControl.SelectionIndicatorSettings.Color = Color.MediumSeaGreen;
            }
            if (color == 5)
            {
                segmentedControl.SelectionIndicatorSettings.Color = Color.BlueViolet;
            }

        }
        private int colorSelected(Color theColor)
        {
            if (theColor.ToString() == Color.OrangeRed.ToString())
                return 0;
            else if (theColor.ToString() == Color.Orange.ToString())
                return 1;
            else if (theColor.ToString() == Color.DeepPink.ToString())
                return 2;
            else if (theColor.ToString() == Color.DodgerBlue.ToString())
                return 3;
            else if (theColor.ToString() == Color.MediumSeaGreen.ToString())
                return 4;
            else if (theColor.ToString() == Color.BlueViolet.ToString())
                return 5;
            else
                return 0;
        }

        private Color changeColor(int theColor)
        {
            if (theColor == 0)
                return Color.OrangeRed;
            else if (theColor == 1)
                return Color.Orange;
            else if (theColor == 2)
                return Color.DeepPink;
            else if (theColor == 3)
                return Color.DodgerBlue;
            else if (theColor == 4)
                return Color.MediumSeaGreen;
            else if (theColor == 5)
                return Color.BlueViolet;
            else
                return Color.Blue;
        }
    }
}