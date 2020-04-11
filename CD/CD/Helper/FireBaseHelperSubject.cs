using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Firebase.Database;
using Firebase.Database.Query;
using CD.Models;

namespace CD.Helper
{
    class FireBaseHelperSubject
    {
        private readonly string Subject_Name = "Subjects";
        readonly FirebaseClient firebase = new FirebaseClient("https://collegediary-fd88a.firebaseio.com/");

        public async Task<List<Subject>> GetAllSubjects()
        {
            return (await firebase.Child(Subject_Name).OnceAsync<Subject>()).Select(item => new Subject
            {
                SubjectName = item.Object.SubjectName,
                SubjectID = item.Object.SubjectID,
                LecturerName = item.Object.LecturerName,
                LecturerEmail = item.Object.LecturerEmail,
                CA = item.Object.CA,
                FinalExam = item.Object.FinalExam
            }).ToList();
        }

        public async Task AddSubject(string subjectName, string lecturerName, string lecturerEmail, int ca, int finalExam)
        {
            await firebase.Child(Subject_Name).PostAsync(new Subject()
            {
                SubjectID = Guid.NewGuid(),
                SubjectName = subjectName,
                LecturerName = lecturerName,
                LecturerEmail = lecturerEmail,
                CA = ca,
                FinalExam = finalExam
            }); 
        }
        public async Task<Subject> GetSubject(Guid subjectID)
        {
            var allSubjects = await GetAllSubjects();
            await firebase.Child(Subject_Name).OnceAsync<Subject>();
            return allSubjects.FirstOrDefault(a => a.SubjectID == subjectID);
        }

        public async Task<Subject> GetSubject(string subjectname)
        {
            var allSubjects = await GetAllSubjects();
            await firebase.Child(Subject_Name).OnceAsync<Subject>();
            return allSubjects.FirstOrDefault(a => a.SubjectName == subjectname);
        }

        public async Task DeleteSubject(Guid subjectID)
        {
            var toDeleteSubject = (await firebase.Child(Subject_Name).OnceAsync<Subject>()).FirstOrDefault
                (a => a.Object.SubjectID == subjectID);
            await firebase.Child(Subject_Name).Child(toDeleteSubject.Key).DeleteAsync();
        }
    }
}
