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
            return _db.Users.Where(x => x.UserId == Id).FirstOrDefault();
        }
        public List<webpages_RolesModel> GetAllUser()
        {
            var GetAllUser = (from c in _db.webpages_Roles
                              select new webpages_RolesModel
                              {
                                  RoleCode = c.RoleCode,
                                  RoleName = c.RoleName,
                                  RoleId = c.RoleId,
                                 

                              }).ToList();

            return GetAllUser;


        }
        public List<UsersModel> GetAllUser1()
        {
            var GetAllUserview = (from c in _db.Users
                                  select new UsersModel
                                  {
                                      UserName = c.UserName,
                                      Name = c.UserName,
                                      EmailId = c.EmailId,
                                      Id = c.UserId



                                  }).ToList();

            return GetAllUserview;


        }
    }
}
