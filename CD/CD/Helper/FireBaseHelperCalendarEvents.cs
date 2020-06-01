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

        public async Task AddEvent(string name, string description, DateTime start_date_time, DateTime end_date_time)
        {
            await firebase.Child(UserUID).Child(Calendar_Name).PostAsync(new EventModel()
            {
                Name = name,
                Description = description,
                StartEventDate = start_date_time,
                EndEventDate = end_date_time,
            });
        }
        public async Task<List<EventModel>> GetAllEvents()
        {
            return (await firebase.Child(UserUID).Child(Calendar_Name).OnceAsync<EventModel>()).Select(item => new EventModel
            {
                Name = item.Object.Name,
                Description = item.Object.Description,
                StartEventDate = item.Object.StartEventDate,
                EndEventDate = item.Object.EndEventDate,
            }).ToList();
        }

        public async Task<List<EventModel>> GetEventsForThisDay(DateTime thisDate)
        {
            var allEvents = await GetAllEvents();
            var allEventsOfThisDay = new List<EventModel>();
            string daySelected = thisDate.Date.Day + "/" + thisDate.Date.Month + "/" + thisDate.Date.Year;

            try
            {
                foreach (EventModel e in allEvents)
                {
                    if (DateTime.Parse(daySelected) == e.StartEventDate)
                    {
                        allEventsOfThisDay.Add(e);
                    }
                }
            }
            catch (Exception)
            {
                await App.Current.MainPage.DisplayAlert("Error", "Something went wrong!", "OK");
            }
          
            return allEventsOfThisDay;
        }


    }
}
