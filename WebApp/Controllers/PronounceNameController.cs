using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using WebApp.ActionFilters;
using WebApp.Models;

namespace WebApp.Controllers
{
    public class PronounceNameController : Controller
    {
        [ServiceFilter(typeof(ValidateLoginFilterAttribute))]
        public IActionResult Index()
        {
            List<User> lstSUM = GetUsers();
            ViewBag.Users = lstSUM;
            return View();
        }

        [HttpGet]
        public ActionResult SearchUser(string searchParam)
        {
            List<User> lstSUM= GetUsers();
            if (string.IsNullOrEmpty(searchParam))
                return PartialView(lstSUM);
            else
            return PartialView(lstSUM.Where(a => a.LoginName.Contains(searchParam) || a.LanId.Contains(searchParam) || a.Email.Contains(searchParam) || a.EmployeeNumber.ToString().Contains(searchParam)).ToList());
            
        }     


        private List<User> GetUsers()
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
