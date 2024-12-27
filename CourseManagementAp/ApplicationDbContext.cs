using Microsoft.EntityFrameworkCore;
using CourseManagementApp.Models;
using CourseManagementApp.Models.CourseManagementApp.Models;  // Добавлено пространство имен

public class ApplicationDbContext : DbContext
{
    public DbSet<User> users { get; set; }
    public DbSet<Student> Students { get; set; }
    public DbSet<Teacher> Teachers { get; set; }
    public DbSet<Course> Courses { get; set; }
    public DbSet<Enrollment> Enrollments { get; set; }
    public DbSet<Payment> Payments { get; set; }
    public DbSet<Assignment> Assignments { get; set; }
    public DbSet<StudentAssignment> StudentAssignments { get; set; }
    public DbSet<Report> Reports { get; set; }

    // Переопределяем метод OnConfiguring
    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        if (!optionsBuilder.IsConfigured)
        {
            optionsBuilder.UseNpgsql("Host=127.0.0.1;Port=5433;Username=admin;Password=8952;Database=coursemanagement");
        }
    }
}
