using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebApp.Models
{
    public class SearchUserModel
    {
        public string LanId { get; set; }
        public string EmpNum { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }
    }

    public class SearchUserModelList
    {
        public List<SearchUserModel> SearchUserData { get; set; }      
     
    }
}
