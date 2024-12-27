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
    /// Логика взаимодействия для TeacherProfileWindow.xaml
    /// </summary>
    public partial class TeacherProfileWindow : Window
    {
        private ApplicationDbContext _context;
        private User _currentUser;

        public TeacherProfileWindow(User user)
        {
            InitializeComponent();
            _context = new ApplicationDbContext();
            _currentUser = user;
            PopulateProfile();
            LoadCourses();
        }

        // Метод для заполнения данных преподавателя
        private void PopulateProfile()
        {
            FullNameText.Text = _currentUser.FullName;
            EmailText.Text = _currentUser.Email;
        }

        // Метод для загрузки курсов, которые ведет преподаватель
        private void LoadCourses()
        {
            // Загружаем курсы, которые ведет преподаватель
            var courses = _context.Courses
                .Where(c => c.teacherid == _currentUser.userid)
                .Select(c => new CourseDetails
                {
                    CourseID = c.CourseID, // Добавляем CourseID
                    Name = c.Name,
                    Description = c.Description,
                    Duration = c.Duration,
                    Price = c.Price
                })
                .ToList();

            // Заполняем DataGrid данными о курсах
            CoursesDataGrid.ItemsSource = courses;
        }

        // Обработчик для смены пароля
        private void ChangePasswordButton_Click(object sender, RoutedEventArgs e)
        {
            var changePasswordWindow = new ChangePasswordWindow(_currentUser);
            changePasswordWindow.Show();
        }

        // Обработчик для редактирования курса
        private void EditCourseButton_Click(object sender, RoutedEventArgs e)
        {
            var selectedCourse = CoursesDataGrid.SelectedItem as CourseDetails;  // Используем CourseDetails
            if (selectedCourse != null)
            {
                var courseId = selectedCourse.CourseID;  // Используем CourseID
                var selectedCourseFromDb = _context.Courses.FirstOrDefault(c => c.CourseID == courseId);
                if (selectedCourseFromDb != null)
                {
                    var editCourseWindow = new EditCourseWindow(selectedCourseFromDb);
                    editCourseWindow.Show();
                }
            }
            else
            {
                MessageBox.Show("Выберите курс для редактирования.");
            }
        }

        // Обработчик для составления отчета по курсу
        private void GenerateReportButton_Click(object sender, RoutedEventArgs e)
        {
            var selectedCourse = CoursesDataGrid.SelectedItem as CourseDetails;  // Используем CourseDetails
            if (selectedCourse != null)
            {
                var courseId = selectedCourse.CourseID;  // Используем CourseID
                var students = _context.Enrollments
                    .Where(e => e.CourseID == courseId)
                    .Select(e => e.Student)
                    .ToList();

                MessageBox.Show($"Количество студентов на курсе: {students.Count}");
                // Для генерации более сложного отчета можно добавить дополнительные данные
            }
            else
            {
                MessageBox.Show("Выберите курс для отчета.");
            }
        }
    }
}
