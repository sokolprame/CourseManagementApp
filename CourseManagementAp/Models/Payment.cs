using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

// Models/Payment.cs
namespace CourseManagementApp.Models
{
    public class Payment
    {
        public int PaymentId { get; set; }  // Первичный ключ
        public int StudentId { get; set; }  // Внешний ключ на таблицу Students
        public int CourseId { get; set; }  // Внешний ключ на таблицу Courses
        public DateTime PaymentDate { get; set; }  // Дата платежа
        public decimal Amount { get; set; }  // Сумма платежа

        public Student Student { get; set; }  // Навигационное свойство
        public Course Course { get; set; }  // Навигационное свойство
    }
}
