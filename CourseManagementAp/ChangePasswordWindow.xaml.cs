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
    /// Логика взаимодействия для ChangePasswordWindow.xaml
    /// </summary>
    public partial class ChangePasswordWindow : Window
    {
        private Users _currentUser;
        private ApplicationDbContext _context;

        public ChangePasswordWindow(Users user)
        {
            InitializeComponent();
            _currentUser = user;
            _context = new ApplicationDbContext();
        }

        private void ChangePasswordButton_Click(object sender, RoutedEventArgs e)
        {
            var currentPassword = CurrentPasswordBox.Password;
            var newPassword = NewPasswordBox.Password;

            if (_currentUser.PasswordHash == currentPassword)
            {
                _currentUser.PasswordHash = newPassword;
                _context.SaveChanges();
                MessageBox.Show("Пароль успешно изменен!");
                this.Close();
            }
            else
            {
                MessageBox.Show("Неверный текущий пароль.");
            }
        }
    }
}
