using System;
using System.Collections.Generic;
using System.Text;
using Firebase.Database;
using Firebase.Database.Query;
using System.Linq;
using System.Threading.Tasks;

namespace CD.Helper
{
    class FireBaseHelperCalendarEvents
    {
        private readonly string Calendar_Name = "Calendar";
        private readonly string UserUID = App.UserUID;
        readonly FirebaseClient firebase = new FirebaseClient("https://collegediary-fd88a.firebaseio.com/");
    }
}
