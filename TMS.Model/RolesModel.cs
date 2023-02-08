using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace TMS.Model
{
    public class RolesModel
    {
        public int Id { get; set; }
        [Required]
        public string Name { get; set; }

        [DisplayName("Active")]
        public bool IsActive { get; set; }

        public string _RoleCode { get; set; }


        [Required]
        [Display(Name = "Role Code")]
        [Remote("CheckDuplicateRoleCode", "Roles", HttpMethod = "Post", AdditionalFields = "Id")]
        public string RoleCode
        {
            get
            {
                if (string.IsNullOrEmpty(_RoleCode))
                {
                    return _RoleCode;
                }
                return _RoleCode.ToUpper();
            }
            set
            {
                _RoleCode = value;
            }
        }

        public int UpdatedBy { get; set; }
        public DateTime UpdatedOn { get; set; }
    }
}
