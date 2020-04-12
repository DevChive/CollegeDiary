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
    class FireBaseHelperMarks
    {
        private readonly string Marks_Name = "Marks";
        readonly FirebaseClient firebase = new FirebaseClient("https://collegediary-fd88a.firebaseio.com/");

        public async Task AddMark(Guid subjectID, string mark_name, int result, int weight, string category)
        {
            await firebase.Child(Marks_Name).PostAsync(new Mark()
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
            return (await firebase.Child(Marks_Name).OnceAsync<Mark>()).Select(item => new Mark
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
            await firebase.Child(Marks_Name).OnceAsync<Mark>();
            return allMarks.FirstOrDefault(a => a.MarkName == markname);
        }
    }
}
