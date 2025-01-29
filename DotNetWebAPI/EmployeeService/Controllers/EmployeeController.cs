using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using EmployeeDataAccess;
using EmployeeService.Models;
namespace EmployeeService.Controllers
{
    public class EmployeeController : ApiController
    {

        [BasicAuthentication]
        public HttpResponseMessage Get()
        {
            using (EmployeeDBContext db = new EmployeeDBContext())
            {
                var result = db.Employees.ToList();
                return Request.CreateResponse(HttpStatusCode.OK, result);
            }
        }


        public HttpResponseMessage GetById(int id)
        {
            using (EmployeeDBContext db = new EmployeeDBContext())
            {
                var employee = db.Employees.FirstOrDefault(x => x.ID == id);
                if (employee != null)
                {
                    return Request.CreateResponse(HttpStatusCode.OK, employee);
                }
                else
                {
                    return Request.CreateErrorResponse(HttpStatusCode.NotFound,
                        "Employee with id : " + id.ToString() + " Not Found");
                }
            }

        }

        public HttpResponseMessage GetByGender(string gender)
        {
            using (EmployeeDBContext db = new EmployeeDBContext())
            {
                switch(gender.ToLower())
                {
                    case "all":
                        return Request.CreateResponse(HttpStatusCode.OK,db.Employees.ToList());
                    case "male":
                        return Request.CreateResponse(HttpStatusCode.OK,
                            db.Employees.Where(x => x.Gender == "male").ToList());
                    case "female":
                        return Request.CreateResponse(HttpStatusCode.OK,
                            db.Employees.Where(x => x.Gender == "female").ToList());
                    default:
                        return Request.CreateErrorResponse(HttpStatusCode.BadRequest,
                            "Value for gender must be Male, Female or All. " + gender + " is invalid.");
                }
            }
        }




        public HttpResponseMessage Post([FromBody] Employee employee)
        {
            try
            {
                using (EmployeeDBContext db = new EmployeeDBContext())
                {
                    db.Employees.Add(employee);
                    db.SaveChangesAsync();

                    var messgae = Request.CreateResponse(HttpStatusCode.Created, employee);
                    messgae.Headers.Location = new Uri(Request.RequestUri + employee.ID.ToString());
                    return messgae;
                }

            }
            catch (Exception ex)
            {
                return Request.CreateErrorResponse(HttpStatusCode.BadRequest, ex);

            }
        }


        public HttpResponseMessage Delete(int id)
        {
            try
            {
                using (EmployeeDBContext db = new EmployeeDBContext())
                {
                    var employee = db.Employees.FirstOrDefault(x => x.ID == id);
                    if (employee == null)
                    {
                        return Request.CreateErrorResponse(HttpStatusCode.NotFound,
                            "Employee with Id :" + id.ToString() + " not found to delete");
                    }
                    else
                    {
                        db.Employees.Remove(employee);
                        db.SaveChanges();

                        return Request.CreateResponse(HttpStatusCode.OK);
                    }
                }
            }
            catch (Exception ex)
            {
                return Request.CreateErrorResponse(HttpStatusCode.BadRequest, ex);
            }
        }

        public HttpResponseMessage Put(int id, [FromBody] Employee employee)
        {
            try
            {
                using (EmployeeDBContext db = new EmployeeDBContext())
                {
                    var entity = db.Employees.FirstOrDefault(x => x.ID == id);
                    if (entity == null)
                    {
                        return Request.CreateErrorResponse(HttpStatusCode.NotFound,
                            "Employee with id : " + id.ToString() + " Not Found");
                    }
                    else
                    {
                        entity.FirstName = employee.FirstName;
                        entity.LastName = employee.LastName;
                        entity.Gender = employee.Gender;
                        entity.Salary = employee.Salary;
                        db.SaveChanges();
                        return Request.CreateResponse(HttpStatusCode.OK, entity);
                    }
                }
            }
            catch (Exception ex)
            {
                return Request.CreateErrorResponse(HttpStatusCode.BadRequest, ex);
            }
        }

    }
}
