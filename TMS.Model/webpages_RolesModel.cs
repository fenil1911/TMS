using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TMS.Model
{
   public class webpages_RolesModel
    {
        public int? RoleId { get; set; }


        public string RoleName { get; set; }
       
        public bool IsActive { get; set; }
        public string RoleCode { get; set; }
        public string Type { get; set; }
        public int? UpdatedBy { get; set; }
        public DateTime? UpdatedOn { get; set; }
    }
}
