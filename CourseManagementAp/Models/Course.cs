using CourseManagementApp.Models.CourseManagementApp.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CourseManagementApp.Models
{
    public class Course
    {
        public int courseid { get; set; }
        public string name { get; set; }
        public string description { get; set; }
        public int duration { get; set; }
        public decimal price { get; set; }
        public int teacherid { get; set; }
        public Teacher Teacher { get; set; }


        // Навигационное свойство для связи с Enrollment
        public virtual ICollection<Enrollment> Enrollments { get; set; }
    }
}
