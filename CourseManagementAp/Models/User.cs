using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CourseManagementApp.Models
{
    public class User
    {
        public int userid { get; set; }  // Первичный ключ
        public string FullName { get; set; }
        public string Email { get; set; }
        public string PasswordHash { get; set; }
        public string Role { get; set; } // Роль пользователя (например, студент, преподаватель, администратор)
    }
}
