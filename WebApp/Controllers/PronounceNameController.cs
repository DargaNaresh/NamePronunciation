using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using WebApp.ActionFilters;
using WebApp.Models;
using WebApp.Utility;

namespace WebApp.Controllers
{
    public class PronounceNameController : Controller
    {
        [ServiceFilter(typeof(ValidateLoginFilterAttribute))]
        public IActionResult Index()
        {
            List<User> lstSUM = UserData.GetUsers();
            ViewBag.Users = lstSUM;
            return View();
        }

        [HttpGet]
        public ActionResult SearchUser(string searchParam)
        {
            List<User> lstSUM= UserData.GetUsers();
            if (string.IsNullOrEmpty(searchParam))
                return PartialView(lstSUM);
            else
            return PartialView(lstSUM.Where(a => a.FirstName.Contains(searchParam)|| a.LastName.Contains(searchParam) || a.Email.Contains(searchParam) || a.EmployeeNumber.ToString().Contains(searchParam)).ToList());
            
        }     


       
    }
}
