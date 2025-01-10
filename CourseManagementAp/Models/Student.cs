using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CourseManagementApp.Models
{
    public class Student
    {
        public int StudentId { get; set; }  // Первичный ключ
        public int UserId { get; set; } // Внешний ключ на таблицу Users
        public string FullName { get; set; }
        public string Email { get; set; }
        public string Phone { get; set; }

        public Users User { get; set; }  // Навигационное свойство для User
    }
}

