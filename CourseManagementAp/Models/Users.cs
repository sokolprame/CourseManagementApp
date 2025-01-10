using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CourseManagementApp.Models
{
    public class Users
    {
        public int UserId { get; set; }  // Первичный ключ
        public string FullName { get; set; }
        public string Email { get; set; }  // Электронная почта
        public string PasswordHash { get; set; }  // Хэш пароля
        public string Role { get; set; }  // Роль пользователя (admin, teacher, student)
    }
}
