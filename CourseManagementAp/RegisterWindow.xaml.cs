using CourseManagementApp.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace CourseManagementAp
{
    /// <summary>
    /// Логика взаимодействия для RegisterWindow.xaml
    /// </summary>
    public partial class RegisterWindow : Window
    {
        private ApplicationDbContext _context;

        public RegisterWindow()
        {
            InitializeComponent();
            _context = new ApplicationDbContext();
        }

        private void RegisterButton_Click(object sender, RoutedEventArgs e)
        {
            var fullName = FullNameTextBox.Text;
            var email = EmailTextBox.Text;
            var password = PasswordBox.Password;

            if (string.IsNullOrEmpty(fullName) || string.IsNullOrEmpty(email) || string.IsNullOrEmpty(password))
            {
                MessageBox.Show("Пожалуйста, заполните все поля.");
                return;
            }

            var existingUser = _context.users.SingleOrDefault(u => u.Email == email);
            if (existingUser != null)
            {
                MessageBox.Show("Пользователь с таким email уже существует.");
                return;
            }

            var user = new User
            {
                FullName = fullName,
                Email = email,
                PasswordHash = password,  // В реальном приложении необходимо шифровать пароль
                Role = "Студент"
            };

            _context.users.Add(user);
            _context.SaveChanges();

            MessageBox.Show("Пользователь успешно зарегистрирован!");
            this.Close(); // Закрыть окно регистрации
        }
    }

}
