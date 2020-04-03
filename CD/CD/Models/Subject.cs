using System;
using System.Collections.Generic;
using System.Text;

namespace CD.Models
{
    class Subject
    {
        public Guid SubjectID { get; set; }
        public String SubjectName { get; set; }
        public String LecturerName { get; set; }
        public String LecturerEmail { get; set; }
        public int CA { get; set; }
        public int FinalExam { get; set; }
    }
}
