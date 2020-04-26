using System;
using System.Collections.Generic;
using System.Text;

namespace CD.Models.Calendar
{
    public class CalendarPickerResult
    {
        public bool IsSuccess { get; set; }

        public DateTime SelectedDate { get; set; }
    }
}
