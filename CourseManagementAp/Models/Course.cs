using CourseManagementApp.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

// Models/Course.cs
namespace CourseManagementApp.Models
{
    public class Course
    {
        public int CourseId { get; set; }  // Первичный ключ
        public string Name { get; set; }
        public string Description { get; set; }
        public int Duration { get; set; }  // Продолжительность в часах
        public decimal Price { get; set; }

        public int TeacherId { get; set; }  // Внешний ключ на таблицу Teachers
        public Teacher Teacher { get; set; }  // Навигационное свойство

        public ICollection<Enrollment> Enrollments { get; set; }
    }
}
