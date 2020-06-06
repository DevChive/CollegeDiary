﻿using java.sql;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Text;
using Syncfusion.SfSchedule.XForms;

namespace CD.Models.Calendar
{
    public class EventModel
    {
        public Guid EventID { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public DateTime? StartEventDate { get; set; }
        public DateTime? EndEventDate { get; set; }
    }
}
