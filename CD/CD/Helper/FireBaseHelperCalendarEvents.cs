using System;
using System.Collections.Generic;
using System.Text;
using Firebase.Database;
using Firebase.Database.Query;
using System.Linq;
using System.Threading.Tasks;
using CD.Models.Calendar;
using System.Drawing;
using ikvm.lang;
using com.sun.tools.@internal.jxc.gen.config;

namespace CD.Helper
{
    class FireBaseHelperCalendarEvents
    {
        private readonly string Calendar_Name = "Calendar";
        private readonly string UserUID = App.UserUID;
        readonly FirebaseClient firebase = new FirebaseClient(App.conf.firebase);

        public async Task AddEvent(string name, string description, DateTime start_date_time, DateTime end_date_time, Color eventColor)
        {
            await firebase.Child(UserUID).Child(Calendar_Name).PostAsync(new EventModel()
            {
                EventID = Guid.NewGuid(),
                Name = name,
                Description = description,
                StartEventDate = start_date_time,
                EndEventDate = end_date_time,
                Color = eventColor,
                StartDateString = start_date_time.Date.ToLongDateString(),
                StartTimeString = start_date_time.ToShortTimeString(),
                EndDateString = end_date_time.Date.ToLongDateString(),
                EndTimeString = end_date_time.ToShortTimeString(),
            }) ;
        }
        public async Task<List<EventModel>> GetAllEvents()
        {
            return (await firebase.Child(UserUID).Child(Calendar_Name).OnceAsync<EventModel>()).Select(item => new EventModel
            {
                EventID = item.Object.EventID,
                Name = item.Object.Name,
                Description = item.Object.Description,
                StartEventDate = item.Object.StartEventDate,
                EndEventDate = item.Object.EndEventDate,
                Color = item.Object.Color,
                StartDateString = item.Object.StartDateString,
                StartTimeString = item.Object.StartTimeString,
                EndDateString = item.Object.EndDateString,
                EndTimeString = item.Object.EndTimeString,
            }).ToList();
        }

        public async Task DeleteEvent(Guid eventID)
        {
            var toDeleteEvent = (await firebase.Child(UserUID).Child(Calendar_Name).OnceAsync<EventModel>()).FirstOrDefault
                (a => a.Object.EventID == eventID);
            await firebase.Child(UserUID).Child(Calendar_Name).Child(toDeleteEvent.Key).DeleteAsync();
        }
    }
}
