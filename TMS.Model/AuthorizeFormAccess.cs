using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TMS.Model
{
    public class AuthorizeFormAccess
    {
        public enum FormAccessCode
        {
            CATEGORY = 1,
            EMPLOYEE = 2,
            DASHBOARD = 3,
            COMMONLOOKUP = 4,
            FORMMASTER = 5,
            ROLE = 6,
            USER = 7,
        }
    }
}