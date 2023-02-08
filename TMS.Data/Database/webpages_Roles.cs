using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TMS.Data.Database
{
    [Table("webpages_Roles")]
    public class webpages_Roles
    {
        [Key]
        public int?  RoleId { get; set; }

        
        public string RoleName { get; set; }
        [Required]
        public bool  IsActive { get; set; }
        public string RoleCode { get; set; }
        public int? UpdatedBy { get; set; }
        public DateTime? UpdatedOn { get; set; }
        

    }
}