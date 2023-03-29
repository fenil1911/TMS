using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TMS.Model
{
    public class LoginModel
    {
        public int UserId { get; set; }
        [Required(ErrorMessage = "Email Id is required.")] 
        public string UserName { get; set; }
        public string RoleCode { get; set; }

        [DataType(DataType.Password)]
        [Required(AllowEmptyStrings = false, ErrorMessage = "Password is required")]
        public string Password { get; set; }

        [Display(Name = "Remember me")]
        public bool RememberMe { get; set; } 
        public string EmailId { get; set; }
    }
}    