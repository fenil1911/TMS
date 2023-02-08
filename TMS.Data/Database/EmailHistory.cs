using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TMS.Data.Database
{
    [Table("EmailHistory")]
    public class EmailHistory
    {
        [Key]


        public int Id {get;set;}
        public string  Name {get;set;}
         public string ToEmailAddress { get;set;}
        public string CCEmailAddress { get;set;}
        public string BCCEmailAddress { get;set;}
        public string Body { get;set;}
        public DateTime SentOn { get;set;}
                    
    }
}
                    
                    
                    
                    
                    