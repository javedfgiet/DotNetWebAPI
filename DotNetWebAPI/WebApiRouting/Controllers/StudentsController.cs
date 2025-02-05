﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using WebApiRouting.Models;

namespace WebApiRouting.Controllers
{
    [RoutePrefix("api/students")]
    public class StudentsController : ApiController
    {
        static List<Student> students = new List<Student>()
        {
            new Student(){Id=1,Name="Tom"},
            new Student(){Id=2,Name="Sam" },
            new Student(){Id=3,Name="John" }
        };

        [Route("")]
        public IEnumerable<Student> Get()
        {
            return students;
        }

        [Route("{id:int:min(1)}")]
        public Student Get(int id)
        {
            return students.FirstOrDefault(x => x.Id == id);
        }

        [Route("{name:alpha}")]
        public Student Get(string name)
        {
            return students.FirstOrDefault(x => x.Name == name);
        }

        [Route("{id}/courses")]
        public IEnumerable<string> GetStudentCourses(int id)
        {
            if (id == 1)
                return new List<string>() { "C#", "Asp.Net", "SQL Server" };
            else if (id == 2)
                return new List<string>() { "Asp.net Web API", "C#", "SQL Server" };
            else
                return new List<string>() { "Bootstrap", "jQuery", "AngularJS" };
        }

        [Route("~/api/teachers")]
        public IEnumerable<Teacher> GetTeachers()
        {
            List<Teacher> teachers = new List<Teacher>()
            {
                new Teacher() { Id = 1, Name = "Rob" },
                new Teacher() { Id = 2, Name = "Mike" },
                new Teacher() { Id = 3, Name = "Mary" }
            };
            return teachers;
        }
    }
}
