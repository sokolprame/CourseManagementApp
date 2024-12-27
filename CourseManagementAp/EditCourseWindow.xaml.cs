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
    /// Логика взаимодействия для EditCourseWindow.xaml
    /// </summary>
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
        }

        private void SaveCourseButton_Click(object sender, RoutedEventArgs e)
        {
            // Сохраняем изменения в курсе
            _selectedCourse.Name = CourseNameTextBox.Text;
            _selectedCourse.Description = CourseDescriptionTextBox.Text;

            _context.SaveChanges();
            MessageBox.Show("Курс успешно обновлен.");
            this.Close();
        }
    }
}
