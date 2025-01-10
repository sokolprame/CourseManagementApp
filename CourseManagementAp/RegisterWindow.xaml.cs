using CourseManagementApp.Models;
using System;
using System.Linq;
using System.Windows;
using System.Windows.Controls;

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
            try
            {
                var fullName = FullNameTextBox.Text.Trim();
                var email = EmailTextBox.Text.Trim();
                var password = PasswordBox.Password.Trim();

                // Проверка, что поля не пустые
                if (string.IsNullOrEmpty(fullName) || string.IsNullOrEmpty(email) || string.IsNullOrEmpty(password))
                {
                    MessageBox.Show("Пожалуйста, заполните все поля.");
                    return;
                }

                // Проверка существования пользователя с таким же email
                var existingUser = _context.Users.SingleOrDefault(u => u.Email.ToLower() == email.ToLower());
                if (existingUser != null)
                {
                    MessageBox.Show("Пользователь с таким email уже существует.");
                    return;
                }

                // Создание нового пользователя без хэширования пароля
                var user = new Users
                {
                    FullName = fullName,
                    Email = email,
                    PasswordHash = password,  // Сохраняем пароль без хэширования
                    Role = "Студент"
                };

                // Добавление пользователя в контекст
                _context.Users.Add(user);
                _context.SaveChanges(); // Сохранение изменений в базе данных

                // Создание студента с привязкой к пользователю
                var student = new Student
                {
                    FullName = fullName,
                    Email = email,
                    Phone = "", // Можно задать номер телефона, если нужно
                    UserId = user.UserId  // Привязываем студента к учетной записи пользователя
                };

                // Добавление студента
                _context.Students.Add(student);
                _context.SaveChanges(); // Сохранение изменений в базе данных

                MessageBox.Show("Пользователь и студент успешно зарегистрированы!");
                this.Close(); // Закрытие окна регистрации
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Произошла ошибка: {ex.Message}");
            }
        }

    }
}
