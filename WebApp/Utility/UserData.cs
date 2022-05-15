using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebApp.Models;

namespace WebApp.Utility
{
    public class UserData
    {
        public static List<User> GetUsers()
        {
            List<User> lstSUM = new List<User>();
            User sm = new User();
            sm.FirstName = "Srikanth";
            sm.LastName = "Chinni";
            sm.EmployeeNumber = 1234;
            sm.Email = "srikanth@test.com";
            sm.Password = "admin";
            sm.Type = EmployeeType.FullTime;
            lstSUM.Add(sm);

            sm = new User();
            sm.FirstName = "CCS";
            sm.LastName = "Admin";
            sm.EmployeeNumber = 12359;
            sm.Email = "admin@test.com";
            sm.Password = "admin";
            sm.Type = EmployeeType.Admin;
            lstSUM.Add(sm);

            sm = new User();
            sm.FirstName = "Naresh";
            sm.LastName = "Darga";
            sm.EmployeeNumber = 1235;
            sm.Email = "naresh@test.com";
            sm.Password = "admin";
            sm.Type = EmployeeType.FullTime;
            lstSUM.Add(sm);

            sm = new User();
            sm.FirstName = "Sailaja";
            sm.LastName = "Lakshmi";
            sm.EmployeeNumber = 1236;
            sm.Email = "sailaja@test.com";
            sm.Type = EmployeeType.FullTime;
            lstSUM.Add(sm);

            return lstSUM;
        }
    }
}
