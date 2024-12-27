using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CourseManagementApp.Models
{
    public class Report
    {
        public int ReportID { get; set; }
        public int TeacherID { get; set; }
        public DateTime ReportDate { get; set; }
        public string Content { get; set; }

        public Teacher Teacher { get; set; }
    }
}

