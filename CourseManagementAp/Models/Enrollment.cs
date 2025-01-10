using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

// Models/Enrollment.cs
namespace CourseManagementApp.Models
{
    public class Enrollment
    {
        public int EnrollmentId { get; set; }  // Первичный ключ

        // Внешний ключ на таблицу Students
        public int StudentId { get; set; }
        public Student Student { get; set; }  // Навигационное свойство

        // Внешний ключ на таблицу Courses
        public int CourseId { get; set; }
        public Course Course { get; set; }  // Навигационное свойство

        // Внешний ключ на Users (для связывания с пользователем)
        public int UserId { get; set; }
        public Users User { get; set; }  // Навигационное свойство для User
    }
}


