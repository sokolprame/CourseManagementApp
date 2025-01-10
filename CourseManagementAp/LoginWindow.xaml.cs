using System;
using System.Linq;
using System.Security.Cryptography;
using System.Windows;
using Microsoft.EntityFrameworkCore;

namespace CourseManagementAp
{
    public partial class LoginWindow : Window
    {
        private readonly ApplicationDbContext _context;

        public LoginWindow()
        {
            InitializeComponent();
            _context = new ApplicationDbContext(); // Ваш контекст базы данных
        }

        private async void LoginButton_Click(object sender, RoutedEventArgs e)
        {
            // Получаем данные из полей
            var email = EmailTextBox.Text.Trim();
            var password = PasswordTextBox.Password.Trim();

            // Проверяем, что поля не пустые
            if (string.IsNullOrWhiteSpace(email) || string.IsNullOrWhiteSpace(password))
            {
                ErrorTextBlock.Text = "Пожалуйста, заполните все поля.";
                return;
            }

            try
            {
                // Ищем пользователя по email
                var user = await _context.Users
                    .SingleOrDefaultAsync(u => u.Email.ToLower() == email.ToLower());

                // Проверяем, найден ли пользователь и совпадает ли пароль
                if (user != null && user.PasswordHash == password)  // Сравниваем без хэширования
                {
                    ErrorTextBlock.Text = ""; // Очищаем сообщение об ошибке
                    MessageBox.Show("Вход выполнен успешно!", "Успех", MessageBoxButton.OK, MessageBoxImage.Information);

                    // Закрываем текущее окно
                    this.Close();

                    // Открываем окно в зависимости от роли пользователя
                    switch (user.Role)
                    {
                        case "Администратор":
                            var adminProfile = new AdminProfileWindow(user);
                            adminProfile.Show();
                            break;
                        case "Преподаватель":
                            var teacherProfile = new TeacherProfileWindow(user);
                            teacherProfile.Show();
                            break;
                        case "Студент":
                            var studentProfile = new StudentProfileWindow(user);
                            studentProfile.Show();
                            break;
                        default:
                            MessageBox.Show("Неизвестная роль пользователя.", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
                            break;
                    }
                }
                else
                {
                    ErrorTextBlock.Text = "Неверный email или пароль.";
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Произошла ошибка: {ex.Message}", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

    }
}
