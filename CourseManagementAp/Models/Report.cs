using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

// Models/Report.cs
namespace CourseManagementApp.Models
{
    public class Report
    {
        public int ReportId { get; set; }  // Первичный ключ
        public int TeacherId { get; set; }  // Внешний ключ на таблицу Teachers
        public DateTime ReportDate { get; set; }  // Дата отчёта
        public string Content { get; set; }  // Содержимое отчёта

        public Teacher Teacher { get; set; }  // Навигационное свойство
    }
}
