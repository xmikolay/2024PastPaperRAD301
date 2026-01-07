using ClassLibrary;
using ConsoleApp;
using System;
using Microsoft.EntityFrameworkCore;

static void list_students(int CourseID)
{
    //create an instance of db context and print out a list of all students enrolled in the indicated course
    using (var context = new ConsoleStudentContext())
    {
        var course = context.Courses
            .Where(c => c.ID == CourseID)
            .Select(c => new
            {
                c.CourseName,
                Students = c.Students.Select(s => new { s.FirstName, s.Surname })
            })
            .FirstOrDefault();
        if (course != null)
        {
            Console.WriteLine($"Students enrolled in {course.CourseName}:");
            foreach (var student in course.Students)
            {
                Console.WriteLine($"{student.FirstName} {student.Surname}");
            }
        }
        else
        {
            Console.WriteLine("Course not found.");
        }
    }
}

static void connected(int StudentID, int LecturerID)
{
    //create an instance of db context and check if the indicated student is enrolled in any course taught by the indicated lecturer, print student name and lecturer name to console
    using (var context = new ConsoleStudentContext())
    {
        var student = context.Students
            .Where(s => s.ID == StudentID)
            .Select(s => new
            {
                s.FirstName,
                s.Surname,
                Courses = s.Courses.Where(c => c.LecturerID == LecturerID)
            })
            .FirstOrDefault();

        var lecturer = context.Lecturers
            .Where(l => l.ID == LecturerID)
            .Select(l => new { l.FirstName, l.Surname })
            .FirstOrDefault();
        if (student != null && lecturer != null)
        {
            if (student.Courses.Any())
            {
                Console.WriteLine($"Student {student.FirstName} {student.Surname} is enrolled in a course taught by Lecturer {lecturer.FirstName} {lecturer.Surname}.");
            }
            else
            {
                Console.WriteLine($"Student {student.FirstName} {student.Surname} is NOT enrolled in any course taught by Lecturer {lecturer.FirstName} {lecturer.Surname}.");
            }
        }
        else
        {
            Console.WriteLine("Student or Lecturer not found.");
        }
    }

    //using (var context = new ConsoleStudentContext())
    //{
    //    var isConnected = context.Students
    //        .Where(s => s.ID == StudentID)
    //        .SelectMany(s => s.Courses)
    //        .Any(c => c.LecturerID == LecturerID);
    //    if (isConnected)
    //    {
    //        Console.WriteLine($"Student {StudentID} is enrolled in a course taught by Lecturer {LecturerID}.");
    //    }
    //    else
    //    {
    //        Console.WriteLine($"Student {StudentID} is NOT enrolled in any course taught by Lecturer {LecturerID}.");
    //    }
    //}
}

list_students(1);
Console.WriteLine();
connected(2, 2);