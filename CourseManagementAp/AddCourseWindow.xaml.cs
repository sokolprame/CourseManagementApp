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
    /// Логика взаимодействия для AddCourseWindow.xaml
    /// </summary>
    public partial class AddCourseWindow : Window
    {
        private ApplicationDbContext _context;
        private Users _currentUser;

        public AddCourseWindow(Users user)
        {
            InitializeComponent();
            _context = new ApplicationDbContext();
            _currentUser = user;
        }

        private void AddButton_Click(object sender, RoutedEventArgs e)
        {
            var name = CourseNameTextBox.Text;
            var description = CourseDescriptionTextBox.Text;
            var duration = int.Parse(CourseDurationTextBox.Text);
            var price = decimal.Parse(CoursePriceTextBox.Text);

            if (string.IsNullOrWhiteSpace(name) || string.IsNullOrWhiteSpace(description))
            {
                MessageBox.Show("Заполните все поля.");
                return;
            }

            var course = new Course
            {
                Name = name,
                Description = description,
                Duration = duration,
                Price = price,
                TeacherId = _currentUser.UserId
            };

            _context.Courses.Add(course);
            _context.SaveChanges();
            MessageBox.Show("Курс успешно добавлен.");
            DialogResult = true; // Возвращаем true при успешном добавлении
            Close();
        }
    }

}
