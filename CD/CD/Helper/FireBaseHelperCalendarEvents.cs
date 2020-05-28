using System;
using System.Collections.Generic;
using System.Text;
using Firebase.Database;
using Firebase.Database.Query;
using System.Linq;
using System.Threading.Tasks;
using CD.Models.Calendar;

namespace CD.Helper
{
    class FireBaseHelperCalendarEvents
    {
        private readonly string Calendar_Name = "Calendar";
        private readonly string UserUID = App.UserUID;
        readonly FirebaseClient firebase = new FirebaseClient("https://collegediary-fd88a.firebaseio.com/");

        public async Task AddEvent(string name, string description, string date, TimeSpan time)
        {
            await firebase.Child(UserUID).Child(Calendar_Name).PostAsync(new EventModel()
            {
                Name = name,
                Description = description,
                EventDate = date,
                Time = time
            });
        }
        public async Task<List<EventModel>> GetAllEvents()
        {
            return (await firebase.Child(UserUID).Child(Calendar_Name).OnceAsync<EventModel>()).Select(item => new EventModel
            {
                Name = item.Object.Name,
                Description = item.Object.Description,
                EventDate = item.Object.EventDate,
                Time = item.Object.Time
            }).ToList();
        }

        public async Task<List<EventModel>> GetEventsForThisDay(DateTime thisDate)
        {
            var allEvents = await GetAllEvents();
            var allEventsOfThisDay = new List<EventModel>();
            string daySelected = thisDate.Date.Day + "/" + thisDate.Date.Month + "/" + thisDate.Date.Year;
            foreach (EventModel e in allEvents)
            {
                if (daySelected == e.EventDate)
                {
                    allEventsOfThisDay.Add(e);
                }
            }
            return allEventsOfThisDay;
        }


    }
}
