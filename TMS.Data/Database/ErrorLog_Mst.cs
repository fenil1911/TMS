﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TMS.Data.Database
{
    public class ErrorLog_Mst
    {
        [Key]
        public int Id { get; set; }
        public string ControllerName { get; set; }
        public string Message { get; set; }
        public string ActionName { get; set; }
        public string PageURL { get; set; }
     
        public int UpdatedBy { get; set; }
        public DateTime UpdatedOn { get; set; }
    }
}
