using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace WebApp.Models
{
    public class LoginUserModel
    {
        [Required(ErrorMessage = "UserName is required")]
        public string UserName { get; set; }
        [Required(ErrorMessage = "Password is required")]
        [DataType(DataType.Password)]
        public string Password { get; set; }

        public string EmpType { get; set; }

        public int EmpID { get; set; }
    }

    public class LoginUsers
    {
        List<LoginUserModel> lstUsers = new List<LoginUserModel>();

        public List<LoginUserModel> GetUsersList()
        {
            lstUsers.Add(new LoginUserModel() { UserName = "admin", Password = "admin", EmpType="admin",EmpID=1235 });
            lstUsers.Add(new LoginUserModel() { UserName = "srikanth", Password = "srikanth", EmpType="emp", EmpID = 1234 });
            lstUsers.Add(new LoginUserModel() { UserName = "sailaja", Password = "sailaja", EmpType = "emp", EmpID = 1236 });
            return lstUsers;
        }

        public LoginUserModel CheckUser(string Name, string pwd)
        {
           List<LoginUserModel> lst =  GetUsersList();
           return lst.FirstOrDefault(a => a.UserName == Name && a.Password == pwd);
        }
    }
}
