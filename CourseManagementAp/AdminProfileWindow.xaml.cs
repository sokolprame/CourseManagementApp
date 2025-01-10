using CourseManagementApp.Models;
using System;
using System.Linq;
using System.Windows;
using Microsoft.EntityFrameworkCore;

namespace CourseManagementAp
{
    public partial class AdminProfileWindow : Window
    {
        private readonly ApplicationDbContext _context;
        private readonly Users _currentUser;

        public AdminProfileWindow(Users user)
        {
            InitializeComponent();
            _context = new ApplicationDbContext();
            _currentUser = user;
            PopulateProfile();
            LoadTeachers();
        }

        // Метод для заполнения данных администратора
        private void PopulateProfile()
        {
            FullNameText.Text = _currentUser.FullName;
            EmailText.Text = _currentUser.Email;
        }

        // Метод для загрузки преподавателей
        private void LoadTeachers()
        {
            var teachers = _context.Teachers
                .Select(t => new
                {
                    t.TeacherId,        // Идентификатор преподавателя
                    t.FullName,         // ФИО
                    t.Email,            // Email
                    t.Phone             // Телефон
                })
                .ToList();

            TeachersDataGrid.ItemsSource = teachers;
        }

        // Обработчик для смены пароля
        private void ChangePasswordButton_Click(object sender, RoutedEventArgs e)
        {
            var changePasswordWindow = new ChangePasswordWindow(user: _currentUser);
            changePasswordWindow.ShowDialog();
        }

        // Обработчик для редактирования преподавателя
        private void EditTeacherButton_Click(object sender, RoutedEventArgs e)
        {
            // Получаем выбранного преподавателя из DataGrid
            var selectedTeacher = TeachersDataGrid.SelectedItem;

            if (selectedTeacher != null)
            {
                // Получаем ID преподавателя из выбранной строки
                var teacherId = (int)selectedTeacher.GetType().GetProperty("TeacherId")?.GetValue(selectedTeacher);

                // Ищем преподавателя в базе данных по ID
                var teacher = _context.Teachers.FirstOrDefault(t => t.TeacherId == teacherId);

                if (teacher != null)
                {
                    // Открываем окно для редактирования преподавателя
                    var editTeacherWindow = new EditTeacherWindow(teacher);
                    editTeacherWindow.ShowDialog();

                    // После редактирования обновляем данные
                    LoadTeachers();
                }
            }
            else
            {
                MessageBox.Show("Выберите преподавателя для редактирования.");
            }
        }

        // Обработчик для добавления нового преподавателя
        private void AddTeacherButton_Click(object sender, RoutedEventArgs e)
        {
            var addTeacherWindow = new AddTeacherWindow();
            if (addTeacherWindow.ShowDialog() == true) // Если добавление завершилось успешно
            {
                LoadTeachers(); // Обновляем список преподавателей
            }
        }

        // Обработчик для удаления преподавателя
        private void DeleteTeacherButton_Click(object sender, RoutedEventArgs e)
        {
            // Получаем выбранного преподавателя из DataGrid
            var selectedTeacher = TeachersDataGrid.SelectedItem;

            if (selectedTeacher != null)
            {
                // Получаем ID преподавателя из выбранной строки
                var teacherId = (int)selectedTeacher.GetType().GetProperty("TeacherId")?.GetValue(selectedTeacher);

                // Ищем преподавателя в базе данных по ID
                var teacher = _context.Teachers.FirstOrDefault(t => t.TeacherId == teacherId);

                if (teacher != null)
                {
                    // Удаляем преподавателя из базы данных
                    _context.Teachers.Remove(teacher);
                    _context.SaveChanges();

                    MessageBox.Show("Преподаватель успешно удалён.");
                    LoadTeachers(); // Обновляем список преподавателей
                }
            }
            else
            {
                MessageBox.Show("Выберите преподавателя для удаления.");
            }
        }
    }
}
