using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using WebApp.Models;

namespace WebApp.Controllers
{
    public class PronounceNameController : Controller
    {
        public IActionResult Index()
        {
            List<SearchUserModel> lstSUM = GetUsers();
            ViewBag.Users = lstSUM;
            return View();
        }

        [HttpGet]
        public ActionResult SearchUser(string searchParam)
        {
            List<SearchUserModel> lstSUM= GetUsers();
            if (string.IsNullOrEmpty(searchParam))
                return PartialView(lstSUM);
            else
            return PartialView(lstSUM.Where(a => a.Name.Contains(searchParam) || a.LanId.Contains(searchParam) || a.Email.Contains(searchParam) || a.EmpNum.Contains(searchParam)).ToList());
            
        }     


        private List<SearchUserModel> GetUsers()
        {
            List<SearchUserModel> lstSUM = new List<SearchUserModel>();
            SearchUserModel sm = new SearchUserModel();
            sm.Name = "Srikanth";
            sm.EmpNum = "1234";
            sm.Email = "srikanth@test.com";
            sm.LanId = "u567157";
            lstSUM.Add(sm);

            sm = new SearchUserModel();
            sm.Name = "Naresh";
            sm.EmpNum = "1235";
            sm.Email = "naresh@test.com";
            sm.LanId = "u123459";
            lstSUM.Add(sm);

            sm = new SearchUserModel();
            sm.Name = "Sailaja";
            sm.EmpNum = "1236";
            sm.Email = "sailaja@test.com";
            sm.LanId = "u123456";
            lstSUM.Add(sm);

            return lstSUM;
        }
    }
}
