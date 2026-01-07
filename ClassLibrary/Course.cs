using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClassLibrary
{
    public class Course
    {
        public int ID { get; set; }
        public string CourseName { get; set; }
        public int QQILevel { get; set; }
        public string Description { get; set; }

        //navigation property to represent relationship between Course and Student
        public ICollection<Student> Students { get; set; }

        //navigation property for lecturer
        public Lecturer Lecturer { get; set; }

        //foreign key for lecturer
        public int LecturerID { get; set; }
    }
}
