using TMS.Data.Database;
using TMS.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static TMS.Model.AccountModel;

namespace TMS.Data
{
    public class UsersProvider : BaseProvider
    {
        public UsersProvider()
        {

        }
        public Users GetUserById(int Id)
        {
            return _db.Users.Where(a => a.UserId == Id).FirstOrDefault();
        }
        public List<webpages_RolesModel> GetAllCommonLookup()
        {
            var allCommonLookup = (from c in _db.webpages_Roles
                                   select new webpages_RolesModel
                                   {
                                        RoleCode = c.RoleCode,
                                        RoleName = c.RoleName,
                                        RoleId = c.RoleId,
                                        Type = c.Type
                                       

                                   }).ToList();

            return allCommonLookup;


        }
    }
}
