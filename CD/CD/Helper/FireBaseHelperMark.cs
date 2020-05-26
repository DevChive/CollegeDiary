using System;
using System.Collections.Generic;
using System.Text;
using CD.Models;
using Firebase.Database;
using Firebase.Database.Query;
using System.Linq;
using System.Threading.Tasks;

namespace CD.Helper
{
    class FireBaseHelperMark
    {
        private readonly string UserUID = App.UserUID;
        private readonly string Marks_Name = "Marks";
        readonly FirebaseClient firebase = new FirebaseClient("https://collegediary-fd88a.firebaseio.com/");

        public async Task AddMark(Guid subjectID, string mark_name, decimal result, int weight, string category)
        {
            await firebase.Child(UserUID).Child(Marks_Name).PostAsync(new Mark()
            {
                SubjectID = subjectID,
                MarkName = mark_name,
                Result = result,
                Weight = weight,
                Category = category
            }) ;
        }

        public async Task<List<Mark>> GetAllMarks()
        {
            return (await firebase.Child(UserUID).Child(Marks_Name).OnceAsync<Mark>()).Select(item => new Mark
            {
                SubjectID = item.Object.SubjectID,
                MarkName = item.Object.MarkName,
                Result = item.Object.Result,
                Category = item.Object.Category,
                Weight = item.Object.Weight,
            }).ToList();
        }

        public async Task<Mark> GetMark(string markname)
        {
            var allMarks = await GetAllMarks();
            await firebase.Child(UserUID).Child(Marks_Name).OnceAsync<Mark>();
            return allMarks.FirstOrDefault(a => a.MarkName == markname);
        }

        public async Task<List<Mark>> GetMarksForSubject(Guid s_ID)
        {
            var allMarks = await GetAllMarks();
            var allSubjectMarks = new List<Mark>(); 

            foreach (Mark m in allMarks)
            {
                if (s_ID == m.SubjectID)
                {
                    allSubjectMarks.Add(m);
                }
            }
            return allSubjectMarks;
        }

        public async Task DeleteMarks(Guid s_ID)
        {
            var allMarks = await GetMarksForSubject(s_ID);
            foreach (Mark m in allMarks)
            {
                var toDeleteMark = (await firebase.Child(UserUID).Child(Marks_Name).OnceAsync<Mark>()).FirstOrDefault
                    (a => a.Object.SubjectID == s_ID);
                await firebase.Child(UserUID).Child(Marks_Name).Child(toDeleteMark.Key).DeleteAsync();
            }
        }
    }
}
