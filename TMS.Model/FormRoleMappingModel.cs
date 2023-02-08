using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TMS.Model
{
    
    public class FormRoleMappingModel
    {
        public int Id { get; set; }
        public int RoleId { get; set; }

        [DisplayName("All")]
        public bool FullRights { get; set; }
        [DisplayName("Is Menu")]
        public bool AllowMenu { get; set; }
        [DisplayName("View")]
        public bool AllowView { get; set; }
        [DisplayName("Insert")]
        public bool AllowInsert { get; set; }
        [DisplayName("Edit")]
        public bool AllowUpdate { get; set; }
        [DisplayName("Delete")]
        public bool AllowDelete { get; set; }
        public int CreatedBy { get; set; }
        public DateTime CreateOn { get; set; }
        public int UpdatedBy { get; set; }
        public DateTime UpdatedOn { get; set; }
        public bool IsActive { get; set; }
        public int MenuId { get; set; }
        [DisplayName("Form Name")]
        public string FormName { get; set; }
        public string RoleName { get; set; }
    }
}
