using Microsoft.EntityFrameworkCore;
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
    /// Логика взаимодействия для CoursesWindow.xaml
    /// </summary>
    public partial class CoursesWindow : Window
    {
        private ApplicationDbContext _context;

        public CoursesWindow()
        {
            InitializeComponent();
            _context = new ApplicationDbContext();
            LoadCourses();
        }

        private void LoadCourses()
        {
            var courses = _context.Courses
                .Include(c => c.Teacher)  // Загружаем преподавателя
                .ToList();

            CoursesDataGrid.ItemsSource = courses;
        }
    }
}
