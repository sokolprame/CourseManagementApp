using CourseManagementApp.Models.CourseManagementApp.Models;
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
    public partial class ApplyForCourseWindow : Window
    {
        private ApplicationDbContext _context;
        private User _currentUser;

        public ApplyForCourseWindow(List<Course> availableCourses, User currentUser)
        {
            InitializeComponent();
            _context = new ApplicationDbContext();
            _currentUser = currentUser;

            // Заполняем ComboBox доступными курсами
            CoursesComboBox.ItemsSource = availableCourses;
        }

        private void ApplyButton_Click(object sender, RoutedEventArgs e)
        {
            var selectedCourse = CoursesComboBox.SelectedItem as Course;
            if (selectedCourse != null)
            {
                var enrollment = new Enrollment
                {
                    StudentID = _currentUser.userid,
                    CourseID = selectedCourse.CourseID
                };

                _context.Enrollments.Add(enrollment);
                _context.SaveChanges();

                MessageBox.Show($"Вы успешно подали заявку на курс: {selectedCourse.Name}");
                this.Close();
            }
        }
    }
}