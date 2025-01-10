using System;
using System.Windows;
using CourseManagementApp.Models;

namespace CourseManagementAp
{
    public partial class AddTeacherWindow : Window
    {
        private readonly ApplicationDbContext _context;

        public AddTeacherWindow()
        {
            InitializeComponent();
            _context = new ApplicationDbContext();
        }

        private void AddTeacherButton_Click(object sender, RoutedEventArgs e)
        {
            // Получаем данные из текстовых полей
            var fullName = FullNameTextBox.Text;
            var email = EmailTextBox.Text;
            var phone = PhoneTextBox.Text;
            var specialization = SpecializationTextBox.Text;

            // Проверяем, что поля заполнены
            if (string.IsNullOrWhiteSpace(fullName) || string.IsNullOrWhiteSpace(email) || string.IsNullOrWhiteSpace(phone))
            {
                MessageBox.Show("Пожалуйста, заполните все поля.");
                return;
            }

            // Проверяем уникальность email
            var existingTeacher = _context.Teachers.FirstOrDefault(t => t.Email == email);
            if (existingTeacher != null)
            {
                MessageBox.Show("Преподаватель с таким Email уже существует.");
                return;
            }

            // Создаем объект Teacher и добавляем в базу данных
            var teacher = new Teacher
            {
                FullName = fullName,
                Email = email,
                Phone = phone,
                Specialization = specialization
            };

            _context.Teachers.Add(teacher);
            _context.SaveChanges();

            MessageBox.Show("Преподаватель успешно добавлен!");
            this.Close();
        }
    }
}
