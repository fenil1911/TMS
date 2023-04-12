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
        public static class MessageCode
        {
            public const string EMAILSUCCESS = "EMAILSUCCESS";
            public const string EMAILEERROR = "EMAILEERROR";
            public const string OTPERROR = "OTPERROR";
            public const string OTPEXPIRE = "OTPEXPIRE";
            public const string OTPSENTSUCCESS = "OTPSENTSUCCESS";
            public const string OTPSENTERROR = "OTPSENTERROR";
            public const string OTPWRONG = "OTPWRONG";
            public const string PASSWORDRESETLINKEXPIRED = "PASSWORDRESETLINKEXPIRED";
            public const string PASSWORDCHANGESUCCESS = "PASSWORDCHANGESUCCESS";
            public const string PASSWORDERRORMESSAGE = "PASSWORDERRORMESSAGE";
            public const string SAMEPASSWORDMESSAGE = "SAMEPASSWORDMESSAGE";
            public const string OLDPASSWORDINCORRECTMESSAGE = "OLDPASSWORDINCORRECTMESSAGE";
            public const string PASSWORDRESETEMAILSENT = "PASSWORDRESETEMAILSENT";
            public const string CODEEXIST = "CODEEXIST";
            public const string EMAILEXIST = "EMAILEXIST";
            public const string USERNAMEEXIST = "USERNAMEEXIST";
            public const string USERLOGGEDOUT = "USERLOGGEDOUT";
            public const string POSITIONEXIST = "POSITIONEXIST";
        }
    }
}



