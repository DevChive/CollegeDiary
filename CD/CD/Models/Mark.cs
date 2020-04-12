using System;

namespace CD.Models
{
    public class Mark
    {
        public Guid SubjectID { get; set; }
        public string MarkName { get; set; }
        public int Result { get; set; }
        public string Category { get; set; }
        public int Weight { get; set; }
    }
}
