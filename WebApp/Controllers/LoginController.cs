using Microsoft.AspNetCore.Mvc;
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
            return View();
        }

        [HttpPost]
        public ActionResult Index(UserModel model)
        {
            Users oUser = new Users();
            var item = oUser.CheckUser(model.UserName, model.Password);
            
            if (item != null && item.Count<UserModel>() > 0)
            {
                //return View("PronounceName");
                //RedirectToAction("Index", "PronounceName",);
                return RedirectToActionPermanent("Index", "PronounceName");
            }
            else if (item == null)
            {
                ViewBag.NotValidUser = item;
            }
            else
            {
                ViewBag.Failedcount = item;
            }

            return View("Index");
        }
    }
}
