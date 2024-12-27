using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CourseManagementApp.Models
{
    public class Teacher
    {
        public int teacherid{ get; set; }
        public string FullName { get; set; }
        public string Email { get; set; }
        public string Phone { get; set; }
        public string Specialization { get; set; }

        public ICollection<Course> Courses { get; set; }
    }

}
