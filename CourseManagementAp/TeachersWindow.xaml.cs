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
    /// Логика взаимодействия для TeachersWindow.xaml
    /// </summary>
    public partial class TeachersWindow : Window
    {
        private ApplicationDbContext _context;

        public TeachersWindow()
        {
            InitializeComponent();
            _context = new ApplicationDbContext();
            LoadTeachers();
        }

        private void LoadTeachers()
        {
            var teachers = _context.Teachers.ToList();
            TeachersDataGrid.ItemsSource = teachers;
        }
    }

}
