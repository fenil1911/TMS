using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;


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
        
        public string LastName { get; set; }
        
        [Required]
        [Display(Name = "UserName")]
        [Remote("IsUsernameExist", "Account", ErrorMessage = "UserName already exists!")]
        public string UserName { get; set; }
        [Display(Name = "Password ")]
        [Required(ErrorMessage = "Password is required")]
        [DataType(DataType.Password)]
        [RegularExpression(@"^(?=.*[a-z])(?=.*[A-Z])(?=.*\d)(?=.*[^\da-zA-Z]).{8,5000}$", ErrorMessage = "Use 8 or more characters with a mix of letters,numbers & symbols")]
        public string Password { get; set; }
        [Display(Name = "ConfirmPassword ")]
        [Required(ErrorMessage = "ConfirmPassword  is required")]
        [DataType(DataType.Password)]
        [System.ComponentModel.DataAnnotations.Compare(otherProperty: "Password", ErrorMessage = "Password doesn't match.")]
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
