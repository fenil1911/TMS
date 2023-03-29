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

        [Required]
        public int UserId { get; set; }
        [Required]
        public string FirstName { get; set; }
        [Required]
        [Display(Name = "LastName")]
        //[Required(ErrorMessage = "LastName is required")]
        public string LastName { get; set; }
        
        [Required]
        [Display(Name = "UserName")]
        
        public string UserName { get; set; }
        [Display(Name = "Password ")]
        [Required(ErrorMessage = "Password is required")]
        [DataType(DataType.Password)]
        public string Password { get; set; }
        [Display(Name = "ConfirmPassword ")]
        [Required(ErrorMessage = "ConfirmPassword  is required")]
        [Compare(otherProperty: "Password", ErrorMessage = "Password doesn't match.")]
        public string ConfirmPassword { get; set; }
        
        public List<MyDropdown> RoleDropdown { get; set; }
        [Required]
        public string Role { get; set; }
        [Required]
        public int RoleId { get; set; }

        [Required]
        [EmailAddress]
        public string EmailId { get; set; }

        public bool IsActive { get; set; }
        public bool IsDeleted { get; set; }

    }
    
}
