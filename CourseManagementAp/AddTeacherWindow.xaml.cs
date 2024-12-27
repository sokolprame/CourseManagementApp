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
    /// Логика взаимодействия для AddTeacherWindow.xaml
    /// </summary>
    public partial class AddTeacherWindow : Window
    {
        private ApplicationDbContext _context;

        public AddTeacherWindow()
        {
            InitializeComponent();
            _context = new ApplicationDbContext();
        }

        private void AddTeacherButton_Click(object sender, RoutedEventArgs e)
        {
            var name = TeacherNameTextBox.Text;
            var email = TeacherEmailTextBox.Text;
            var phone = TeacherPhoneTextBox.Text;

            var teacher = new Teacher
            {
                FullName = name,
                Email = email,
                Phone = phone
            };

            _context.Teachers.Add(teacher);
            _context.SaveChanges();
            MessageBox.Show("Преподаватель успешно добавлен!");
            this.Close();
        }
    }
}
