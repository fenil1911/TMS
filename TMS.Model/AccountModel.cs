using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TMS.Model
{
    public class AccountModel
    {
        public class LoginModel
        {
            //[Required(AllowEmptyStrings = false, ErrorMessage = "username is required")]
            //public string UserName { get; set; }

            [Required(ErrorMessage = "Email Id is required.")]
            public string EmailId { get; set; }

            [DataType(DataType.Password)]
            [Required(AllowEmptyStrings = false, ErrorMessage = "Password is required")]
            public string Password { get; set; }

            [Display(Name = "Remember me")]
            public bool RememberMe { get; set; }
        }
        public class ChangePasswordModel
        {
            public int UserId { get; set; }
            [Display(Name = "Old Password:")]
            [Required(ErrorMessage = "Old Password is required.")]
            [DataType(DataType.Password)]
            public string OldPassword { get; set; }

            [Display(Name = "New Password:")]
            [Required(ErrorMessage = "New Password is required.")]
            [DataType(DataType.Password)]
            public string NewPassword { get; set; }

            [Display(Name = "Confirm New Password:")]
            [Required(ErrorMessage = "Confirm New Password is required.")]
            [Compare(otherProperty: "NewPassword", ErrorMessage = "New Password doesn't match.")]
            [DataType(DataType.Password)]
            public string ConfirmNewPassword { get; set; }
        }

        public class ResetPasswordModel
        {
            [Required(ErrorMessage = "Please enter new password")]
            [DataType(DataType.Password)]
            public string NewPassword { get; set; }

            [DataType(DataType.Password)]
            [Required(ErrorMessage = "please enter confirm password")]
            [Compare("NewPassword", ErrorMessage = "New password and confirm password does not match")]
            public string ConfirmPassword { get; set; }

            [Required]
            public string ResetCode { get; set; }
        }
        public class ForgotPasswordModel
        {
            [Required(ErrorMessage = "Email Id is required.")]
            [RegularExpression(@"^[A-Za-z0-9._%+-]+@[A-Za-z0-9.-]+\.[A-Za-z]{2,4}$", ErrorMessage = "Invalid Email ID")]
            public string EmailId { get; set; }
        }
    }
}
