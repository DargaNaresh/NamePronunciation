using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace WebApp.Models
{
    public class LoginUserModel
    {
        [Required(ErrorMessage = "Email is required")]
        [Display(Name ="Email")]
        public string UserName { get; set; }
        [Required(ErrorMessage = "Password is required")]
        [DataType(DataType.Password)]
        public string Password { get; set; }

    }

    public class LoginUsers
    {          
        public static User CheckUser(string Name, string pwd)
        {
            List<User> lst = Utility.UserData.GetUsers();
           return lst.FirstOrDefault(a => a.Email == Name && a.Password == pwd);
        }
    }
}
