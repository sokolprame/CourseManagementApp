using CourseManagementApp.Models;
using System.Windows;

namespace CourseManagementAp
{
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
            try
            {
                // Сохраняем изменения в объекте
                _teacher.FullName = TeacherNameTextBox.Text.Trim();
                _teacher.Email = TeacherEmailTextBox.Text.Trim();
                _teacher.Phone = TeacherPhoneTextBox.Text.Trim();

                // Отмечаем объект как изменённый в контексте
                _context.Teachers.Attach(_teacher);
                _context.Entry(_teacher).State = Microsoft.EntityFrameworkCore.EntityState.Modified;

                // Сохраняем изменения в базе данных
                _context.SaveChanges();

                MessageBox.Show("Информация о преподавателе успешно обновлена.");
                this.Close();
            }
            catch (System.Exception ex)
            {
                MessageBox.Show($"Произошла ошибка при сохранении изменений: {ex.Message}");
            }
        }
    }
}
