using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace WebApp.Models
{
    public class UserModel
    {
        [Required(ErrorMessage = "UserName is required")]
        public string UserName { get; set; }
        [Required(ErrorMessage = "Password is required")]
        [DataType(DataType.Password)]
        public string Password { get; set; }

        public string EmpType { get; set; }
    }

    public class Users
    {
        List<UserModel> lstUsers = new List<UserModel>();

        public List<UserModel> GetUsersList()
        {
            lstUsers.Add(new UserModel() { UserName = "admin", Password = "admin", EmpType="admin" });
            lstUsers.Add(new UserModel() { UserName = "srikanth", Password = "srikanth", EmpType="emp" });
            lstUsers.Add(new UserModel() { UserName = "sailaja", Password = "sailaja", EmpType = "emp" });
            return lstUsers;
        }

        public IEnumerable<UserModel> CheckUser(string Name, string pwd)
        {
           List<UserModel> lst =  GetUsersList();
           return lst.Where(a => a.UserName == Name && a.Password == pwd);
        }
    }
}
