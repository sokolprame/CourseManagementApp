using CourseManagementApp.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows;

namespace CourseManagementAp
{
    public partial class TeacherProfileWindow : Window
    {
        private ApplicationDbContext _context;
        private Users _currentUser;

        public TeacherProfileWindow(Users user)
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
            var courses = _context.Courses
                .Where(c => c.TeacherId == _currentUser.UserId)
                .Select(c => new CourseDetails
                {
                    CourseID = c.CourseId,
                    Name = c.Name,
                    Description = c.Description,
                    Duration = c.Duration,
                    Price = c.Price
                })
                .ToList();

            CoursesDataGrid.ItemsSource = courses;
        }

        // Обработчик для добавления курса
        private void AddCourseButton_Click(object sender, RoutedEventArgs e)
        {
            var addCourseWindow = new AddCourseWindow(_currentUser);
            if (addCourseWindow.ShowDialog() == true) // Окно возвращает true при успешном добавлении
            {
                LoadCourses(); // Перезагружаем список курсов
            }
        }

        // Обработчик для удаления курса
        private void DeleteCourseButton_Click(object sender, RoutedEventArgs e)
        {
            var selectedCourse = CoursesDataGrid.SelectedItem as CourseDetails;
            if (selectedCourse != null)
            {
                var course = _context.Courses.FirstOrDefault(c => c.CourseId == selectedCourse.CourseID);
                if (course != null)
                {
                    var result = MessageBox.Show($"Вы уверены, что хотите удалить курс \"{course.Name}\"?",
                                                 "Подтверждение", MessageBoxButton.YesNo);
                    if (result == MessageBoxResult.Yes)
                    {
                        _context.Courses.Remove(course);
                        _context.SaveChanges();
                        MessageBox.Show("Курс успешно удален.");
                        LoadCourses(); // Перезагружаем список курсов
                    }
                }
            }
            else
            {
                MessageBox.Show("Выберите курс для удаления.");
            }
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
            var selectedCourse = CoursesDataGrid.SelectedItem as CourseDetails;
            if (selectedCourse != null)
            {
                var course = _context.Courses.FirstOrDefault(c => c.CourseId == selectedCourse.CourseID);
                if (course != null)
                {
                    var editCourseWindow = new EditCourseWindow(course);
                    editCourseWindow.ShowDialog(); // Дожидаемся закрытия окна
                    LoadCourses(); // Перезагружаем данные после закрытия окна
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
            var selectedCourse = CoursesDataGrid.SelectedItem as CourseDetails;
            if (selectedCourse != null)
            {
                var students = _context.Enrollments
                    .Where(e => e.CourseId == selectedCourse.CourseID)
                    .Select(e => e.StudentId)
                    .ToList();

                MessageBox.Show($"Количество студентов на курсе: {students.Count}");
            }
            else
            {
                MessageBox.Show("Выберите курс для отчета.");
            }
        }
    }
}
