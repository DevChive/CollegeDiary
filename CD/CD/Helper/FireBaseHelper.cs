using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Firebase.Database;
using Firebase.Database.Query;
using CD.Models;

namespace CD.Helper

{
    class FireBaseHelper
    {
        private readonly string SubjectName = "Subject";
        readonly FirebaseClient firebase = new FirebaseClient("https://collegediary-fd88a.firebaseio.com/Subjects");

        public async Task<List<Subject>> GetAllSubjects()
        {
            return (await firebase.Child(SubjectName).OnceAsync<Subject>()).Select(item => new Subject
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
            await firebase.Child(subjectName).PostAsync(new Subject()
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
            await firebase.Child(SubjectName).OnceAsync<Subject>();
            return allSubjects.FirstOrDefault(a => a.SubjectID == subjectID);
        }

        public async Task<Subject> GetSubject(string subjectname)
        {
            var allSubjects = await GetAllSubjects();
            await firebase.Child(SubjectName).OnceAsync<Subject>();
            return allSubjects.FirstOrDefault(a => a.SubjectName == subjectname);
        }
    }
}
