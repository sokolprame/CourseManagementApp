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
    /// Логика взаимодействия для LoginWindow.xaml
    /// </summary>
    public partial class LoginWindow : Window
    {
        private ApplicationDbContext _context;

        public LoginWindow()
        {
            InitializeComponent();
            _context = new ApplicationDbContext();
        }

        private void LoginButton_Click(object sender, RoutedEventArgs e)
        {
            var email = EmailTextBox.Text;
            var password = PasswordTextBox.Password;

            var user = _context.users.SingleOrDefault(u => u.Email == email && u.PasswordHash == password);

            if (user != null)
            {
                MessageBox.Show("Вход выполнен успешно!");
                this.Close(); // Закрыть окно входа
            }
            else
            {
                MessageBox.Show("Неверный логин или пароль.");
            }
        }
    }
}
