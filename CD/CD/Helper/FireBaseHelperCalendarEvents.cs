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
    }
}
