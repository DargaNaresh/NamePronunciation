using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebApp.Models;

namespace WebApp.Controllers
{
    public class LoginController : Controller
    {
        public IActionResult Index()
        {
            if (HttpContext.Session.GetString("EmpID") != null)
            {
                return RedirectToAction("Index", "Home");
            }
            else
            {
                return View();
            }
        }

        [HttpPost]
        public ActionResult Index(LoginUserModel model)
        {
            LoginUsers oUser = new LoginUsers();
            var item = oUser.CheckUser(model.UserName, model.Password);
            
            if (item != null)
            {
                HttpContext.Session.SetString("UserName", item.UserName);
                HttpContext.Session.SetString("EmpID", item.EmpID.ToString());
                return RedirectToAction("Index", "Home");
            }
            else if (item == null)
            {
                ViewBag.NotValidUser = "User Name and Password mismatch";
            }
            else
            {
                ViewBag.Failedcount = item;
            }

            return View("Index");
        }
        public  IActionResult Logout()
        {
            HttpContext.Session.Remove("UserName");
            HttpContext.Session.Remove("EmpID");
            return RedirectToAction("Index", "Home");
        }
        public IActionResult Denied()
        {
            return Content("Access Denied");
        }


    }
}
