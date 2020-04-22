using CD.Models;
using Firebase.Database;
using Firebase.Database.Query;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CD.Helper
{
	class FireBaseHelperStudent
	{
		private readonly string Student_Name = "Students";
		readonly FirebaseClient firebase = new FirebaseClient("https://collegediary-fd88a.firebaseio.com/");

		public async Task<List<Student>> GetAllStudents()
		{
			return (await firebase.Child(Student_Name).OnceAsync<Student>()).Select(item => new Student
			{
				StudentName = item.Object.StudentName,
				StudentID = item.Object.StudentID,
				StudentEmail = item.Object.StudentEmail,
				StudentAddress = item.Object.StudentAddress,
				StudentPhone = item.Object.StudentPhone
			}).ToList();
		}

		public async Task AddStudent(string studentName, string studentEmail, string studentAddress, int studentPhone)
		{
			await firebase.Child(Student_Name).PostAsync(new Student()
			{
				StudentID = Guid.NewGuid(),
				StudentName = studentName,
				StudentEmail = studentEmail,
				StudentAddress = studentAddress,
				StudentPhone = studentPhone
			}) ;
		}

		public async Task<Student> GetStudent(Guid studentID)
		{
			var allStudents = await GetAllStudents();
			await firebase.Child(Student_Name).OnceAsync<Student>();
			return allStudents.FirstOrDefault(a => a.StudentID == studentID);
		}

		public async Task<Student> GetStudent(string studentName)
		{
			var allStudents = await GetAllStudents();
			await firebase.Child(Student_Name).OnceAsync<Student>();
			return allStudents.FirstOrDefault(a => a.StudentName == studentName);
		}

		public async Task DeleteStudent(Guid StudentID)
		{
			var toDeleteStudent = (await firebase.Child(Student_Name).OnceAsync<Subject>()).FirstOrDefault
				(a => a.Object.SubjectID == StudentID);
			await firebase.Child(Student_Name).Child(toDeleteStudent.Key).DeleteAsync();
		}
	}
}
