using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace TMS.Model
{
   public class FormModel
    {
        public FormModel()
        {
            _ParentFormList = new List<SelectListItem>();
        }
       
        public int Id { get; set; }

        [Required]
        public string Name { get; set; }

        [Required]
        [Display(Name = "Navigate URL")]
        public string NavigateURL { get; set; }

        [Required]
        [Display(Name = "Parent form")]
        public int? ParentFormId { get; set; }

        [Required]
        [Display(Name = "Code")]
        //[Remote("CheckDuplicateFormAccessCode", "Form", HttpMethod = "Post", AdditionalFields = "Id")]
        public string FormAccessCode { get; set; }

        [Required]
        [Display(Name = "Display Order")]
        public int? DisplayOrder { get; set; }
        [Display(Name = "Active")]
        public bool IsActive { get; set; }

        [Display(Name = "Display Menu")]
        public bool IsDisplayMenu { get; set; }
        public int? CreatedBy { get; set; }
        public DateTime? CreatedOn { get; set; }
        public int? UpdatedBy { get; set; }
       
        public DateTime? UpdatedOn { get; set; }

        public List<SelectListItem> _ParentFormList { get; set; }
    }
}
