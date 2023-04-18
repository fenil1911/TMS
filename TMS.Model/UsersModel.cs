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

        public int UserId{ get; set; }
        public int Id{ get; set; }

        public string UserName { get; set; }

        public string Name { get; set; }
        [Required]
        public string FirstName { get; set; }
        [Required]
        public string LastName { get; set; }
                  

       
        public string EmailId { get; set; }
        [Required]
        public string Role { get; set; }
        public bool IsActive { get; set; }
        public bool IsDeleted { get; set; }
        public int UpdatedBy { get; set; }
        //public DateTime UpdatedOn { get; set; }
        public int CreatedBy { get; set; }
        public DateTime CreatedOn { get; set; }

     
        public string Password { get; set; }

       
        public string ConfirmPassword { get; set; }
        
        public List<MyDropdown> RoleDropdown { get; set; }
    }
}