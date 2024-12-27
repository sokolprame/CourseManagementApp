using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CourseManagementApp.Models
{
        public class Assignment
        {
            public int AssignmentID { get; set; }
            public int CourseID { get; set; }
            public string Title { get; set; }
            public string Description { get; set; }
            public DateTime DueDate { get; set; }

            public Course Course { get; set; }
        }
}
