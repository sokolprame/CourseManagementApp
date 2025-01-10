using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
// Models/Teacher.cs
namespace CourseManagementApp.Models
{
    public class Teacher
    {
        public int TeacherId { get; set; } // Первичный ключ
        public string FullName { get; set; }
        public string Email { get; set; }
        public string Phone { get; set; }
        public string Specialization { get; set; }

        public override string ToString()
        {
            return FullName; // Возвращает имя преподавателя
        }
    }
}

