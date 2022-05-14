using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebApp.Models;

namespace WebApp.Controllers
{
    public class PronounceNameController : Controller
    {
        public IActionResult Index()
        {

            //SearchUserModelList lst = new SearchUserModelList();
            //lst.SearchUserData = GetUsers();
            return View();
        }

        [HttpPost]
        public IActionResult SearchUser([FromBody] string qry)
        {
            SearchUserModelList lst = new SearchUserModelList();         
            List<SearchUserModel> lstSUM= GetUsers();

            if (string.IsNullOrEmpty(qry))
                lst.SearchUserData = lstSUM;
            else
                lst.SearchUserData = lstSUM.Where(a => a.Name.Contains(qry) || a.LanId.Contains(qry) || a.Email.Contains(qry) || a.EmpNum.Contains(qry)).ToList();
            
            return PartialView(@"~/Views/Shared/_SearchUsers.cshtml", lst);
        }

        public List<SearchUserModel> GetUsers()
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
            sm.LanId = "u123456";
            lstSUM.Add(sm);

            sm = new SearchUserModel();
            sm.Name = "Sailaja";
            sm.EmpNum = "1235";
            sm.Email = "sailaja@test.com";
            sm.LanId = "u123456";
            lstSUM.Add(sm);

            return lstSUM;
        }
    }
}
