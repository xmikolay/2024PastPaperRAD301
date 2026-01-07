using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClassLibrary
{
    public class Lecturer
    {
        public int ID { get; set; }
        public string FirstName { get; set; }
        public string Surname { get; set; }
        public string Email { get; set; }
        public string Title { get; set; }

        //navigation property to represent relationship between Lecturer and Course
        public ICollection<Course> Courses { get; set; }
    }
}
