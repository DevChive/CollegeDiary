using java.sql;
using System;
using System.Collections.Generic;
using System.Text;

namespace CD.Models.Calendar
{
    public class EventModel
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public DateTime? StartEventDate { get; set; }
        public DateTime? EndEventDate { get; set; }
    }
}
