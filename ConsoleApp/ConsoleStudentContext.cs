using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using ClassLibrary;

namespace ConsoleApp
{
    public class ConsoleStudentContext : DbContext
    {
        //set db context to use ClassLibrary models
        public DbSet<Student> Students { get; set; }
        public DbSet<Course> Courses { get; set; }
        public DbSet<Lecturer> Lecturers { get; set; }

        //Constructor for MVC (accepts options)
        public ConsoleStudentContext(DbContextOptions<ConsoleStudentContext> options)
            : base(options)
        {
        }

        //Parameterless constructor for Console app
        public ConsoleStudentContext()
        {
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            // Only configure if no options were provided (preserve DI configuration)
            if (!optionsBuilder.IsConfigured)
            {
                optionsBuilder.UseSqlServer(@"Server=(localdb)\mssqllocaldb;Database=StudentDB2024;Trusted_Connection=True;");
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            //configure many to many relationship between Student and Course
            modelBuilder.Entity<Student>()
                .HasMany(s => s.Courses)
                .WithMany(c => c.Students);

            //seed data
            //seed lecturers
            modelBuilder.Entity<Lecturer>().HasData(
                new Lecturer { ID = 1, FirstName = "Otto", Surname = "Octavius", Title = "Dr.", Email = "otto@atu.ie" },
                new Lecturer { ID = 2, FirstName = "Dominic", Surname = "Carr", Title = "Dr.", Email = "dominic.carr@atu.ie" }
            );

            //seed students
            modelBuilder.Entity<Student>().HasData(
                new Student { ID = 1, FirstName = "James", Surname = "Johnson", Age = 22, Email = "jammyboi@yahoo.com", Eircode = "A94 FP71" },
                new Student { ID = 2, FirstName = "Peter", Surname = "Parker", Age = 45, Email = "spider-man@marvel.com", Eircode = "D14 X3H0" },
                new Student { ID = 3, FirstName = "Donald", Surname = "Trump", Age = 78, Email = "thedonald45@usa.com", Eircode = "D07 Y3B0" },
                new Student { ID = 4, FirstName = "Jack", Surname = "O'Hagan", Age = 22, Email = "jacky@yahoo.com", Eircode = "A84 FP91" }
            );

            //seed courses
            modelBuilder.Entity<Course>().HasData(
                new Course { ID = 1, CourseName = "Introduction to Programming", QQILevel = 6, Description = "blah blah for loop", LecturerID = 2 },
                new Course { ID = 2, CourseName = "Advanced Databases", QQILevel = 7, Description = "Relational DBs are superior", LecturerID = 1 },
                new Course { ID = 3, CourseName = "Functional Programming", QQILevel = 6, Description = "blah blah for loop", LecturerID = 2 }
            );

            // Seed the many-to-many join table
            modelBuilder.Entity<Student>()
                .HasMany(s => s.Courses)
                .WithMany(c => c.Students)
                .UsingEntity(j => j.HasData(
                    new { StudentsID = 1, CoursesID = 1 },  // James in Intro to Programming
                    new { StudentsID = 1, CoursesID = 2 },  // James in Advanced Databases
                    new { StudentsID = 2, CoursesID = 1 },  // Peter in Intro to Programming
                    new { StudentsID = 3, CoursesID = 2 },  // Donald in Advanced Databases
                    new { StudentsID = 4, CoursesID = 3 }   // Jack in Functional Programming ← NEW!
                ));
        }
    }
}
