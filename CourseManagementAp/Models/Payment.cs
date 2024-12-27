using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CourseManagementApp.Models
{
    namespace CourseManagementApp.Models
    {
        public class Payment
        {
            public int PaymentID { get; set; }
            public int StudentID { get; set; }
            public int CourseID { get; set; }
            public DateTime PaymentDate { get; set; }
            public decimal Amount { get; set; }

            public Student Student { get; set; }
            public Course Course { get; set; }
        }
    }
}
