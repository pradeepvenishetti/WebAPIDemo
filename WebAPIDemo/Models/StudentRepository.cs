using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WebAPIDemo.Models
{
    public class StudentRepository
    {
        private const string CacheKey = "StudentStore";


        public StudentRepository()
        {
            var ctx = HttpContext.Current;

            if (ctx != null)
            {
                if (ctx.Cache[CacheKey] == null)
                {
                    var contacts = new Student[]
                    {
                new Student
                {
                    Id = 1, Name = "Glenn Block"
                },
                new Student
                {
                    Id = 2, Name = "Dan Roth"
                }
                    };

                    ctx.Cache[CacheKey] = contacts;
                }
            }
        }

        public Student[] GetAllStudents()
        {

            var ctx = HttpContext.Current;

            if (ctx != null)
            {
                return (Student[])ctx.Cache[CacheKey];
            }

            return new Student[]
                {
            new Student
            {
                Id = 0,
                Name = "Placeholder"
            }
                };

        }


        public bool SaveStudent(Student student)
        {
            var ctx = HttpContext.Current;

            if (ctx != null)
            {
                try
                {
                    var currentData = ((Student[])ctx.Cache[CacheKey]).ToList();
                    currentData.Add(student);
                    ctx.Cache[CacheKey] = currentData.ToArray();

                    return true;
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.ToString());
                    return false;
                }
            }

            return false;
        }

    }
}