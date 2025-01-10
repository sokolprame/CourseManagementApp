using CourseManagementApp.Models;
using System;
using System.Linq;
using System.Windows;

namespace CourseManagementAp
{
    public partial class ApplyForCourseWindow : Window
    {
        private readonly ApplicationDbContext _context;
        private readonly Users _currentUser;
        private readonly StudentProfileWindow _studentProfileWindow;

        public ApplyForCourseWindow(List<Course> availableCourses, Users currentUser, StudentProfileWindow studentProfileWindow)
        {
            InitializeComponent();
            _context = new ApplicationDbContext();
            _currentUser = currentUser;
            _studentProfileWindow = studentProfileWindow;

            // Заполняем ComboBox доступными курсами
            CoursesComboBox.ItemsSource = availableCourses;
        }

        private void ApplyButton_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                var selectedCourse = CoursesComboBox.SelectedItem as Course;

                if (selectedCourse == null)
                {
                    MessageBox.Show("Пожалуйста, выберите курс.");
                    return;
                }

                // Находим студента, основываясь на UserId
                var student = _context.Students.SingleOrDefault(s => s.UserId == _currentUser.UserId);
                if (student == null)
                {
                    MessageBox.Show("Студент не найден в базе данных.");
                    return;
                }

                // Проверка, не записан ли студент уже на этот курс
                var existingEnrollment = _context.Enrollments
                    .SingleOrDefault(e => e.StudentId == student.StudentId && e.CourseId == selectedCourse.CourseId);

                if (existingEnrollment != null)
                {
                    MessageBox.Show("Вы уже записаны на этот курс.");
                    return;
                }

                // Создаем запись о записи на курс
                var enrollment = new Enrollment
                {
                    StudentId = student.StudentId,
                    CourseId = selectedCourse.CourseId,
                    UserId = _currentUser.UserId  // Присваиваем UserId текущего пользователя
                };

                _context.Enrollments.Add(enrollment);
                _context.SaveChanges();  // Сохраняем изменения в базе данных

                // Уведомляем студента о том, что заявка подана
                MessageBox.Show($"Вы успешно подали заявку на курс: {selectedCourse.Name}");

                // Закрываем окно подачи заявки
                this.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Произошла ошибка при подаче заявки: {ex.Message}");
            }
        }


    }

}
