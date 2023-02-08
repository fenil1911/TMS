using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TMS.Data.Database
{
    [Table("FormRoleMapping")]
    public class FormRoleMapping
    {
        [Key]
        public int Id { get; set; }
        public int RoleId { get; set; }
        public int MenuId { get; set; }
        
        public bool FullRights { get; set; }
        
        public bool AllowInsert { get; set; }
        public bool AllowUpdate { get; set; }
        public bool AllowDelete { get; set; }
        public bool AllowView { get; set; }
        public bool AllowMenu { get; set; }
        
        public bool? IsActive { get; set; }
        public int CreatedBy { get; set; }
        public DateTime CreatedOn { get; set; }
        public int UpdatedBy { get; set; }
        public DateTime UpdatedOn { get; set; }
    }
}
