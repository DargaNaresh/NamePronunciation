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
            sm.LoginName = "Srikanth";
            sm.EmployeeNumber = 1234;
            sm.Email = "srikanth@test.com";
            sm.LanId = "u567157";
            lstSUM.Add(sm);

            sm = new User();
            sm.LoginName = "Naresh";
            sm.EmployeeNumber = 1235;
            sm.Email = "naresh@test.com";
            sm.LanId = "u123459";
            lstSUM.Add(sm);

            sm = new User();
            sm.LoginName = "Sailaja";
            sm.EmployeeNumber = 1236;
            sm.Email = "sailaja@test.com";
            sm.LanId = "u123456";
            lstSUM.Add(sm);

            return lstSUM;
        }
    }
}
