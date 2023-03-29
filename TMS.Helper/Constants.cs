using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TMS.Helper
{
    public class Constants
    {

        public static class EmailCodes
        {


            public const string PASSWORDRESETEMAILSENT = "PasswordReset link sent succesfull";
        }
        public enum Codes
        {
            FORGOTPASSWORD

        }

        public static class RoleCode
        {
            public const string SADMIN = "SADMIN";
            public const string ADMIN = "ADMIN";
        }
    }
}



