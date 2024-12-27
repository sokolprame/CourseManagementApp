using CourseManagementApp.Models.CourseManagementApp.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CourseManagementApp.Models
{
    public class StudentAssignment
    {
        public int StudentAssignmentID { get; set; }
        public int AssignmentID { get; set; }
        public int StudentID { get; set; }
        public DateTime SubmissionDate { get; set; }
        public string Grade { get; set; }

        public Assignment Assignment { get; set; }
        public Student Student { get; set; }
    }
}
