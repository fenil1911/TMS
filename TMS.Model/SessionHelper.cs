using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;

namespace TMS.Model
{
    public class SessionHelper : BaseSessionHelper
    {
        public class Constants
        {
            public const string UserId = "CurrentUserId";
            public const string UserName = "CurrentUserName";
            public const string Name = "Name";
            public const string RoleId = "RoleId";
            public const string RoleName = "RoleName";
            public const string RoleCode = "RoleCode";
            public const string DefaultTimeZone = "DefaultTimeZone";
            public const string EmailId = "EmailId";
            public const string IsAdmin = "IsAdmin";
        }
        public static bool IsAdmin
        {
            get { return GetSessionValue<bool>(Constants.IsAdmin); }
            set { SetSessionValue(Constants.IsAdmin, value); }
        }
        public static string UserName
        {
            get { return GetSessionValue<string>(Constants.UserName); }
            set { SetSessionValue(Constants.UserName, value); }
        }
        public static int UserId
        {
            get { return GetSessionValue<int>(Constants.UserId); }
            set { SetSessionValue(Constants.UserId, value); }
        }

        public static int RoleId
        {
            get { return GetSessionValue<int>(Constants.RoleId); }
            set { SetSessionValue(Constants.RoleId, value); }
        }

        public static string RoleName
        {
            get { return GetSessionValue<string>(Constants.RoleName); }
            set { SetSessionValue(Constants.RoleName, value); }
        }
        public static string RoleCode
        {
            get { return GetSessionValue<string>(Constants.RoleCode); }
            set { SetSessionValue(Constants.RoleCode, value); }
        }

        public static string DefaultTimeZone
        {
            get { return GetSessionValue<string>(Constants.DefaultTimeZone); }
            set { SetSessionValue(Constants.DefaultTimeZone, value); }
        }
        public static string EmailId
        {
            get { return GetSessionValue<string>(Constants.EmailId); }
            set { SetSessionValue(Constants.EmailId, value); }
        }
    }

    public class BaseSessionHelper
    {
        public static T GetSessionValue<T>(string item, bool throwExceptionIfMissing = false)
        {
            if (HttpContext.Current == null)
                throw new ApplicationException("Invalid HTTP Context.");

            if (HttpContext.Current.Session == null)
                throw new ApplicationException("Session Expired.");

            if (HttpContext.Current.Session[item] == null)
            {
                if (throwExceptionIfMissing)
                {
                    throw new ApplicationException(String.Format("{0} is missing", item));
                }
                else
                {
                    return default(T);
                }
            }

            return (T)HttpContext.Current.Session[item];
        }

        public static bool HasSessionValue(string item)
        {
            if (HttpContext.Current == null)
                throw new ApplicationException("Invalid HTTP Context.");

            if (HttpContext.Current.Session == null)
                throw new ApplicationException("Session Expired.");

            return (HttpContext.Current.Session[item] != null);
        }

        public static void SetSessionValue<T>(string item, T value)
        {
            if (!object.Equals(value, default(T)))
                HttpContext.Current.Session[item] = value;
            else
                NullSessionVar(item);
        }

        public static void NullSessionVar(string item)
        {
            HttpContext.Current.Session[item] = null;
        }
    }
}

