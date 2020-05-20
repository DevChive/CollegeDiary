using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Firebase.Database;
using Firebase.Database.Query;
using CD.Models;
using CD.Helper;

namespace CD.Helper
{
    class FireBaseHelperSubject
    {
        private readonly string UserUID = App.UserUID;
        private readonly string Subject_Name = "Subjects";
        readonly FirebaseClient firebase = new FirebaseClient("https://collegediary-fd88a.firebaseio.com/");

        readonly FireBaseHelperMark fireBaseHelperMark = new FireBaseHelperMark();

        public async Task<List<Subject>> GetAllSubjects()
        {
            return (await firebase.Child(UserUID).Child(Subject_Name).OnceAsync<Subject>()).Select(item => new Subject
            {
                SubjectName = item.Object.SubjectName,
                SubjectID = item.Object.SubjectID,
                LecturerName = item.Object.LecturerName,
                LecturerEmail = item.Object.LecturerEmail,
                CA = item.Object.CA,
                FinalExam = item.Object.FinalExam,
                TotalCA = item.Object.TotalCA,
                TotalFinalExam = item.Object.TotalFinalExam
            }).ToList();
        }
        public async Task AddSubject(string subjectName, string lecturerName, string lecturerEmail, int ca, int finalExam)
        {
            await firebase.Child(UserUID).Child(Subject_Name).PostAsync(new Subject()
            {
                SubjectID = Guid.NewGuid(),
                SubjectName = subjectName,
                LecturerName = lecturerName,
                LecturerEmail = lecturerEmail,
                CA = ca,
                FinalExam = finalExam,
                TotalCA = 0,
                TotalFinalExam = 0,
            }) ; 
        }
        public async Task<Subject> GetSubject(Guid subjectID)
        {
            var allSubjects = await GetAllSubjects();
            await firebase.Child(UserUID).Child(Subject_Name).OnceAsync<Subject>();
            return allSubjects.FirstOrDefault(a => a.SubjectID == subjectID);
        }

        public async Task<Subject> GetSubject(string subjectname)
        {
            var allSubjects = await GetAllSubjects();
            await firebase.Child(UserUID).Child(Subject_Name).OnceAsync<Subject>();
            return allSubjects.FirstOrDefault(a => a.SubjectName == subjectname);
        }

        public async Task DeleteSubject(Guid subjectID)
        {
            var toDeleteSubject = (await firebase.Child(UserUID).Child(Subject_Name).OnceAsync<Subject>()).FirstOrDefault
                (a => a.Object.SubjectID == subjectID);
            await firebase.Child(UserUID).Child(Subject_Name).Child(toDeleteSubject.Key).DeleteAsync();
        }

        public async Task<Decimal> getTotalCA(Guid SubjectID)
        {
            var marks_belonging_to_subject = await fireBaseHelperMark.GetMarksForSubject(SubjectID);
            decimal total_CA_all_Marks = 0;
            foreach (Mark m in marks_belonging_to_subject)
            {
                if (m.Category.Equals("Continuous Assessment"))
                {
                    decimal result = m.Result;
                    total_CA_all_Marks += ((result / 100) * m.Weight);
                }
            }

            Subject this_subject = await GetSubject(SubjectID);
            decimal totalCA = total_CA_all_Marks / this_subject.CA;

            var subjectToUpdate = (await firebase.Child(UserUID).Child(Subject_Name).OnceAsync<Subject>()).FirstOrDefault(a => a.Object.SubjectID == SubjectID);
            await firebase.Child(UserUID).Child(Subject_Name).Child(subjectToUpdate.Key).Child("TotalCA").PutAsync(totalCA * 100);

            return totalCA;
        }

        public async Task<decimal> Final_Exam_Progress(Guid SubjectID)
        {
            var marks_belonging_to_subject = await fireBaseHelperMark.GetMarksForSubject(SubjectID);
            decimal finalExam = 0;
            foreach (Mark m in marks_belonging_to_subject)
            {
                if (m.Category.Equals("Final Exam"))
                {
                    decimal result = m.Result;
                    finalExam = result / 100;
                }
            }

            var subjectToUpdate = (await firebase.Child(UserUID).Child(Subject_Name).OnceAsync<Subject>()).FirstOrDefault(a => a.Object.SubjectID == SubjectID);
            await firebase.Child(UserUID).Child(Subject_Name).Child(subjectToUpdate.Key).Child("TotalFinalExam").PutAsync(finalExam * 100);

            return finalExam;
        }
    }
}
