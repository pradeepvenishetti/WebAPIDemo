using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using WebAPIDemo.Models;

namespace WebAPIDemo.Controllers
{
    public class StudentController : ApiController
    {

        private StudentRepository studentRepository;

        public StudentController()
        {
            this.studentRepository = new StudentRepository();
        }
        public Student[] GetStudents()
        {
            return studentRepository.GetAllStudents();
        }

        public HttpResponseMessage Post(Student student)
        {
            this.studentRepository.SaveStudent(student);

            var response = Request.CreateResponse<Student>(System.Net.HttpStatusCode.Created, student);

            return response;
        }

        public Student GetStudentById(int id)
        {
            var student = studentRepository.GetAllStudents().FirstOrDefault((s) => s.Id == id);
            if (student == null)
            {
                throw new HttpResponseException(HttpStatusCode.NotFound);
            }
            return student;
        }
    }
}
