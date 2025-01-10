using CourseManagementApp.Models;
using System;
using System.Windows;

namespace CourseManagementAp
{
    public partial class EditCourseWindow : Window
    {
        private Course _selectedCourse;
        private ApplicationDbContext _context;

        public EditCourseWindow(Course selectedCourse)
        {
            InitializeComponent();
            _context = new ApplicationDbContext();
            _selectedCourse = selectedCourse;

            // Заполняем поля данными из выбранного курса
            CourseNameTextBox.Text = _selectedCourse.Name;
            CourseDescriptionTextBox.Text = _selectedCourse.Description;
            CourseDurationTextBox.Text = _selectedCourse.Duration.ToString();
            CoursePriceTextBox.Text = _selectedCourse.Price.ToString();
        }

        private void SaveCourseButton_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                // Проверяем, что поля не пустые
                if (string.IsNullOrWhiteSpace(CourseNameTextBox.Text) ||
                    string.IsNullOrWhiteSpace(CourseDescriptionTextBox.Text) ||
                    string.IsNullOrWhiteSpace(CourseDurationTextBox.Text) ||
                    string.IsNullOrWhiteSpace(CoursePriceTextBox.Text))
                {
                    MessageBox.Show("Пожалуйста, заполните все поля.");
                    return;
                }

                // Проверяем, что длительность и цена корректны
                if (!int.TryParse(CourseDurationTextBox.Text, out int duration) || duration <= 0)
                {
                    MessageBox.Show("Длительность должна быть положительным числом.");
                    return;
                }

                if (!decimal.TryParse(CoursePriceTextBox.Text, out decimal price) || price <= 0)
                {
                    MessageBox.Show("Стоимость должна быть положительным числом.");
                    return;
                }

                // Убедимся, что объект отслеживается контекстом
                var courseEntry = _context.Entry(_selectedCourse);
                if (courseEntry.State == Microsoft.EntityFrameworkCore.EntityState.Detached)
                {
                    _context.Courses.Attach(_selectedCourse);
                }

                // Сохраняем изменения
                _selectedCourse.Name = CourseNameTextBox.Text;
                _selectedCourse.Description = CourseDescriptionTextBox.Text;
                _selectedCourse.Duration = duration;
                _selectedCourse.Price = price;

                _context.SaveChanges();
                MessageBox.Show("Курс успешно обновлен.");
                this.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Произошла ошибка при сохранении изменений: {ex.Message}");
            }
        }

    }
}
