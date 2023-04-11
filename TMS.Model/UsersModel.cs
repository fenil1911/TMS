using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;
namespace TMS.Model
{
    public class UsersModel
    {
        [Required]
        [Display(Name = "User Name")]
        [Remote("CheckDuplicateUserName", "Users", HttpMethod = "Post", AdditionalFields = "UserId")]
        public string UserName { get; set; }

        [Required]
        [RegularExpression(@"^[a-zA-Z ]+$", ErrorMessage = "Only Letters and Spaces are allowed")]
        public string Name { get; set; }


        [Required(AllowEmptyStrings = false, ErrorMessage = "EmailId Required.")]
        [DataType(DataType.EmailAddress)]
        //[Remote("CheckDuplicateUserEmail", "Users", HttpMethod = "Post", AdditionalFields = "UserId")]
        public string EmailId { get; set; }

        public string Role { get; set; }
        public bool IsActive { get; set; }
        public bool IsDeleted { get; set; }
        public int UpdatedBy { get; set; }
        //public DateTime UpdatedOn { get; set; }
        public int CreatedBy { get; set; }
        public DateTime CreatedOn { get; set; }

        [Required(AllowEmptyStrings = false, ErrorMessage = "Password is required")]
        [DataType(DataType.Password)]
        public string Password { get; set; }

        [Display(Name = "Confirm Password")]
        [DataType(DataType.Password)]
        [System.ComponentModel.DataAnnotations.Compare("Password", ErrorMessage = "Confirm password and password do not match")]
        public string ConfirmPassword { get; set; }
        public int Id { get; set; }
    }
}