using ElementarySchoolApp.Shared.Models;
using ElementarySchoolApp.Shared.Models.ViewModels;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace BusinessLogic.Data
{
    public class ApplicationDbContext : IdentityDbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        {

        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<ClassRoomGroup>().HasKey(x =>
                new { x.ClassRoomGroupId, x.GroupId });
            modelBuilder.Entity<ClassRoomLessons>().HasKey(x =>
                new { x.ClassRoomLessonsId, x.LessonId });
            modelBuilder.Entity<ClassRoomTeachers>().HasKey(x =>
                new { x.ClassRoomTeachersId, x.EmployeeId });

            base.OnModelCreating(modelBuilder);
        }

        public DbSet<Room> Rooms { get; set; }
        public DbSet<Lesson> Lessons { get; set; }
        public DbSet<Group> Groups { get; set; }
        public DbSet<Employee> Employees { get; set; }

        public DbSet<ClassRoomGroup> ClassRoomGroups { get; set; }
        public DbSet<ClassRoomLessons> ClassRoomLessons { get; set; }
        public DbSet<ClassRoomTeachers> ClassRoomTeachers { get; set; }
    }
}
