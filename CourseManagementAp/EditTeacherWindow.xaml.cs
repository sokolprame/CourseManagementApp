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
    /// Логика взаимодействия для EditTeacherWindow.xaml
    /// </summary>
    public partial class EditTeacherWindow : Window
    {
        private Teacher _teacher;
        private ApplicationDbContext _context;

        public EditTeacherWindow(Teacher teacher)
        {
            InitializeComponent();
            _context = new ApplicationDbContext();
            _teacher = teacher;

            // Заполняем поля данными из выбранного преподавателя
            TeacherNameTextBox.Text = _teacher.FullName;
            TeacherEmailTextBox.Text = _teacher.Email;
            TeacherPhoneTextBox.Text = _teacher.Phone;
        }

        private void SaveTeacherButton_Click(object sender, RoutedEventArgs e)
        {
            // Сохраняем изменения в преподавателе
            _teacher.FullName = TeacherNameTextBox.Text;
            _teacher.Email = TeacherEmailTextBox.Text;
            _teacher.Phone = TeacherPhoneTextBox.Text;

            _context.SaveChanges();
            MessageBox.Show("Информация о преподавателе успешно обновлена.");
            this.Close();
        }
    }
}
