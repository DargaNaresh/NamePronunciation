
using System.ComponentModel.DataAnnotations;

namespace WebApp.Models
{
    public class User
    {
        public string LanId { get; set; }
        [Display(Name = "Employee Number")]
        public int EmployeeNumber { get; set; }
        public string LoginName { get; set; }
        public string Email { get; set; }

        [Display(Name = "First Name")]
        public string FirstName { get; set; }

        [Display(Name = "Middle Name")]
        public string MiddleName { get; set; }

        [Display(Name = "Last Name")]
        public string LastName { get; set; }

        public byte[] AudioContent { get; set; }
    }

}
