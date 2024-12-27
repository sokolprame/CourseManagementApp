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
        private User _currentUser;

        public StudentProfileWindow(User user)
        {
            InitializeComponent();
            _context = new ApplicationDbContext();
            _currentUser = user;
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
            var courses = _context.Enrollments
                .Where(e => e.StudentID == _currentUser.userid)
                .Join(_context.Courses,
                    enrollment => enrollment.CourseID,
                    course => course.CourseID,
                    (enrollment, course) => new
                    {
                        course.Name,
                        course.Description,
                        course.Duration,
                        course.Price
                    }).ToList();

            CoursesDataGrid.ItemsSource = courses;
        }

        private void ChangePasswordButton_Click(object sender, RoutedEventArgs e)
        {
            var changePasswordWindow = new ChangePasswordWindow(_currentUser);
            changePasswordWindow.Show();
        }

        private void ApplyForCourseButton_Click(object sender, RoutedEventArgs e)
        {
            var availableCourses = _context.Courses
                .Where(c => !c.Enrollments.Any(e => e.StudentID == _currentUser.userid)).ToList();

            var applyCourseWindow = new ApplyForCourseWindow(availableCourses, _currentUser);
            applyCourseWindow.Show();
        }

        private void GeneratePDFButton_Click(object sender, RoutedEventArgs e)
        {
            // Создание нового PDF-документа с использованием PDFsharp
            PdfDocument document = new PdfDocument();
            document.Info.Title = "Заявка на курс";

            // Добавление страницы
            PdfPage page = document.AddPage();

            // Получаем XGraphics для рисования
            XGraphics gfx = XGraphics.FromPdfPage(page);

            // Шрифт для текста
            XFont font = new XFont("Verdana", 12);

            // Рисуем текст в PDF
            gfx.DrawString($"Заявка на курс для студента: {_currentUser.FullName}", font, XBrushes.Black, new XPoint(20, 40));
            gfx.DrawString($"Email: {_currentUser.Email}", font, XBrushes.Black, new XPoint(20, 60));

            // Получаем все курсы студента
            var courses = _context.Enrollments
                .Where(e => e.StudentID == _currentUser.userid)
                .Join(_context.Courses, enrollment => enrollment.CourseID, course => course.CourseID, (enrollment, course) => course)
                .ToList();

            if (courses.Any())  // Проверка, есть ли курсы
            {
                int yPos = 80;
                foreach (var course in courses)
                {
                    gfx.DrawString($"Курс: {course.Name} | Описание: {course.Description}", font, XBrushes.Black, new XPoint(20, yPos));
                    yPos += 20;
                }
            }
            else
            {
                gfx.DrawString("Студент не записан на курсы.", font, XBrushes.Black, new XPoint(20, 80));
            }

            // Сохранение документа в файл на рабочем столе
            string filename = System.IO.Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.Desktop), "StudentApplication.pdf");
            document.Save(filename);

            // Сообщение о завершении
            MessageBox.Show($"PDF успешно создан: {filename}");
        }
    }
}
