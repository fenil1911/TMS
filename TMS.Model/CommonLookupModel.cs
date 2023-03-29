using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TMS.Model
{
    public class CommonLookupModel
    {
        [ScaffoldColumn(false)]
        public int Id { get; set; }
        [Required]
        [Display(Name="Type")]
        public string Type { get; set; }

      
        [RegularExpression(@"[A-Z]{1,50}$", ErrorMessage = "Only uppercase Characters are allowed.")]
        [Display(Name = "Code")]
        [Required]
        public string Code { get; set; }


        [Required(ErrorMessage ="Name is Required")]
        [Display(Name = "Name")]
        public string Name { get; set; }


        [Required]
        
        
        [Display(Name = "DisplayOrder")]
        public int DisplayOrder { get; set; }


        [Required]
        [Display(Name = "Active")]
        public bool IsActive { get; set; }
        
    }
}
