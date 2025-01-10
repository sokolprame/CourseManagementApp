using CourseManagementApp.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

// Models/StudentAssignment.cs
namespace CourseManagementApp.Models
{
    public class StudentAssignment
    {
        public int StudentAssignmentId { get; set; }  // Первичный ключ
        public int AssignmentId { get; set; }  // Внешний ключ на таблицу Assignments
        public int StudentId { get; set; }  // Внешний ключ на таблицу Students
        public DateTime SubmissionDate { get; set; }  // Дата сдачи
        public string Grade { get; set; }  // Оценка

        public Assignment Assignment { get; set; }  // Навигационное свойство
        public Student Student { get; set; }  // Навигационное свойство
    }
}

