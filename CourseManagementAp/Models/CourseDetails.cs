using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CourseManagementAp
{
    public class CourseDetails
    {
        public int CourseID { get; set; }   // Добавляем CourseID
        public string Name { get; set; }
        public string Description { get; set; }
        public int Duration { get; set; }
        public decimal Price { get; set; }
    }
}
