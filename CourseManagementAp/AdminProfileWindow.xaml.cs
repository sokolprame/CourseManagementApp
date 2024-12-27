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
    /// Логика взаимодействия для AdminProfileWindow.xaml
    /// </summary>
    public partial class AdminProfileWindow : Window
    {
        private ApplicationDbContext _context;
        private User _currentUser;

        public AdminProfileWindow(User user)
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
                    t.FullName,
                    t.Email,
                    t.Phone
                })
                .ToList();

            TeachersDataGrid.ItemsSource = teachers;
        }

        // Обработчик для смены пароля
        private void ChangePasswordButton_Click(object sender, RoutedEventArgs e)
        {
            var changePasswordWindow = new ChangePasswordWindow(user: _currentUser);
            changePasswordWindow.Show();
        }

        // Обработчик для редактирования преподавателя
        private void EditTeacherButton_Click(object sender, RoutedEventArgs e)
        {
            var selectedTeacher = TeachersDataGrid.SelectedItem as Teacher; // Используем Teacher
            if (selectedTeacher != null)
            {
                var teacherEmail = selectedTeacher.Email;
                var teacher = _context.Teachers.FirstOrDefault(t => t.Email == teacherEmail);
                if (teacher != null)
                {
                    var editTeacherWindow = new EditTeacherWindow(teacher);
                    editTeacherWindow.Show();
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
            // Открыть окно для добавления нового преподавателя
            var addTeacherWindow = new AddTeacherWindow();  // Убедитесь, что это окно существует
            addTeacherWindow.Show();
        }
    }
}
