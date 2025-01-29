using EmployeeDataAccess;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace EmployeeService.Models
{
    public class EmployeeSecurity
    {
        public static bool Login(string username, string password)
        {
            using (EmployeeDBContext dbContext = new EmployeeDBContext())
            {
                return dbContext.Users.Any(user => user.Username.Equals
                (username, StringComparison.OrdinalIgnoreCase) && user.Password == password);
            }
        }
    }
}