using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

// Models/Assignment.cs
namespace CourseManagementApp.Models
{
    public class Assignment
    {
        public int AssignmentId { get; set; }  // Первичный ключ
        public int CourseId { get; set; }  // Внешний ключ на таблицу Courses
        public string Title { get; set; }
        public string Description { get; set; }
        public DateTime DueDate { get; set; }  // Дата сдачи

        public Course Course { get; set; }  // Навигационное свойство
    }
}
