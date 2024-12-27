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
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        // Открыть окно с курсами
        private void CoursesButton_Click(object sender, RoutedEventArgs e)
        {
            var coursesWindow = new CoursesWindow();
            coursesWindow.Show();
        }

        // Открыть окно с преподавателями
        private void TeachersButton_Click(object sender, RoutedEventArgs e)
        {
            var teachersWindow = new TeachersWindow();
            teachersWindow.Show();
        }

        // Открыть окно входа
        private void LoginButton_Click(object sender, RoutedEventArgs e)
        {
            var loginWindow = new LoginWindow();
            loginWindow.Show();
        }

        // Открыть окно регистрации
        private void RegisterButton_Click(object sender, RoutedEventArgs e)
        {
            var registerWindow = new RegisterWindow();
            registerWindow.Show();
        }
    }

}

