using CourseManagementApp.Models;
using PdfSharp.Drawing;
using PdfSharp.Pdf;
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
using CourseManagementApp;

namespace CourseManagementAp
{
    /// <summary>
    /// Логика взаимодействия для StudentProfileWindow.xaml
    /// </summary>
    public partial class StudentProfileWindow : Window
    {
        private ApplicationDbContext _context;
        private Users _currentUser;

        public StudentProfileWindow(Users user)
        {
            InitializeComponent();
            _context = new ApplicationDbContext();  // Инициализация контекста

            _currentUser = user;  // Убедитесь, что переданный user не равен null
            PopulateProfile();
            LoadStudentCourses();
        }

        private void PopulateProfile()
        {
            FullNameText.Text = _currentUser.FullName;
            EmailText.Text = _currentUser.Email;
        }

        private void LoadStudentCourses()
        {
            // Получаем StudentId для текущего пользователя
            var student = _context.Students.SingleOrDefault(s => s.UserId == _currentUser.UserId);

            if (student != null)
            {
                // Получаем все курсы, на которые записан студент
                var courses = from enrollment in _context.Enrollments
                              join course in _context.Courses on enrollment.CourseId equals course.CourseId
                              where enrollment.StudentId == student.StudentId  // Фильтруем по StudentId
                              select new
                              {
                                  course.Name,
                                  course.Description,
                                  course.Duration,
                                  course.Price
                              };

                // Убедитесь, что курсы правильно подгружаются в DataGrid
                CoursesDataGrid.ItemsSource = courses.ToList();
            }
            else
            {
                MessageBox.Show("Студент не найден в базе данных.");
            }
        }

        private void ChangePasswordButton_Click(object sender, RoutedEventArgs e)
        {
            var changePasswordWindow = new ChangePasswordWindow(_currentUser);
            changePasswordWindow.Show();
        }

        private void ApplyForCourseButton_Click(object sender, RoutedEventArgs e)
        {
            var availableCourses = _context.Courses
                .Where(c => !_context.Enrollments.Any(e => e.StudentId == _currentUser.UserId && e.CourseId == c.CourseId))
                .ToList();

            // Передаем ссылку на окно профиля
            var applyCourseWindow = new ApplyForCourseWindow(availableCourses, _currentUser, this);
            applyCourseWindow.Show();
        }


        private void GeneratePDFButton_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                // Создаем новый PDF-документ
                PdfDocument document = new PdfDocument
                {
                    Info = { Title = "Заявка на курс" }
                };

                // Добавляем страницу в документ
                PdfPage page = document.AddPage();
                XGraphics gfx = XGraphics.FromPdfPage(page);
                XFont font = new XFont("Verdana", 12);

                int margin = 20;
                int lineSpacing = 20;
                int yPosition = margin;

                // Добавляем информацию о студенте
                gfx.DrawString($"Заявка на курс для студента: {_currentUser.FullName}", font, XBrushes.Black, new XPoint(margin, yPosition));
                yPosition += lineSpacing;
                gfx.DrawString($"Email: {_currentUser.Email}", font, XBrushes.Black, new XPoint(margin, yPosition));
                yPosition += lineSpacing;

                // Получаем список курсов, на которые записан студент
                var courses = _context.Enrollments
                    .Where(e => e.StudentId == _currentUser.UserId)
                    .Join(
                        _context.Courses,
                        enrollment => enrollment.CourseId,
                        course => course.CourseId,
                        (enrollment, course) => course
                    )
                    .ToList();

                if (courses.Any())
                {
                    // Если курсы найдены, добавляем их в PDF
                    foreach (var course in courses)
                    {
                        gfx.DrawString($"Курс: {course.Name}", font, XBrushes.Black, new XPoint(margin, yPosition));
                        yPosition += lineSpacing;
                        gfx.DrawString($"Описание: {course.Description}", font, XBrushes.Black, new XPoint(margin, yPosition));
                        yPosition += lineSpacing;
                        gfx.DrawString($"Продолжительность: {course.Duration} часов", font, XBrushes.Black, new XPoint(margin, yPosition));
                        yPosition += lineSpacing;
                        gfx.DrawString($"Стоимость: {course.Price} руб.", font, XBrushes.Black, new XPoint(margin, yPosition));
                        yPosition += lineSpacing * 2; // Отступ между курсами

                        // Проверка на выход за границы страницы
                        if (yPosition + lineSpacing > page.Height - margin)
                        {
                            page = document.AddPage();
                            gfx = XGraphics.FromPdfPage(page);
                            yPosition = margin;
                        }
                    }
                }
                else
                {
                    // Если курсы не найдены, выводим сообщение
                    gfx.DrawString("Студент не записан на курсы.", font, XBrushes.Black, new XPoint(margin, yPosition));
                }

                // Устанавливаем путь для сохранения PDF
                string filename = System.IO.Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments), "StudentApplication.pdf");
                document.Save(filename);

                // Сообщаем об успешном создании PDF
                MessageBox.Show($"PDF успешно создан: {filename}");
            }
            catch (Exception ex)
            {
                // Обрабатываем исключения
                MessageBox.Show($"Произошла ошибка при создании PDF: {ex.Message}");
            }
        }
    }
}
