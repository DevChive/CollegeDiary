using System;
using System.Collections.Generic;
using System.Text;

namespace CD.Models
{
	class Student
	{
		public Guid StudentID { get; set; }
		public String StudentName { get; set; }
		public String StudentAddress { get; set; }
		public int StudentPhone { get; set; }
		public string StudentEmail { get; set; }
	}
}
