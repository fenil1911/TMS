using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TMS.Model
{
    public class RegisterModel
    {
        [Display(Name = "FirstName")]
        [Required(ErrorMessage = "FirstName is required")]
        public string FirstName { get; set; }
        [Display(Name = "LastName")]
        [Required(ErrorMessage = "LastName is required")]
        public string LastName { get; set; }
        [Display(Name = "UserName")]
        [Required(ErrorMessage = "UserName is required")]
        public string UserName { get; set; }
        [Display(Name = "Password ")]
        [Required(ErrorMessage = "Password is required")]
        [DataType(DataType.Password)]
        public string Password { get; set; }
        [Display(Name = "ConfirmPassword ")]
        [Required(ErrorMessage = "ConfirmPassword  is required")]
        [Compare(otherProperty: "Password", ErrorMessage = "Password doesn't match.")]
        public string ConfirmPassword { get; set; }
        [Display(Name = "Role")]
        [Required(ErrorMessage = "Role is required")]
        public string Role { get; set; }
        [EmailAddress]
        public string EmailId { get; set; }
    }
    
}
