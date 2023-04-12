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
        public Users GetEmailById(string EmailId)
        {
            
            

             var email= _db.Users.Where(e => e.EmailId == EmailId).FirstOrDefault();
            return email;
        }
        public Users GetroleById(string id)
        {



            var email = _db.Users.Where(e => e.ResetPasswordCode == id).FirstOrDefault();
            return email;
        }
        public List<UsersModel> GetAllUser1()
        {
           
            var GetAllUserview = (from t1 in _db.Users
                                  join t2 in _db.webpages_UsersInRoles on t1.UserId equals t2.UserId
                                  join t3 in _db.webpages_Roles on t2.RoleId equals t3.RoleId
                                  where t1.IsDeleted != true && t1.UserName != "admin"  
                                  select new UsersModel
                                  {
                                      UserName = t1.UserName,
                                      Name = t1.FirstName + " " + t1.LastName,
                                      EmailId = t1.EmailId,
                                      Id = t1.UserId,
                                      IsActive = t1.IsActive,
                                      IsDeleted = t1.IsDeleted,
                                      Role = t3.RoleName,
                                      CreatedOn = t1.CreatedOn
                                  }).ToList();


            return GetAllUserview;


        }
        public void DeleteUser(int Id)
        {
            var data = GetUserById(Id);
            if (data != null)
            {

                Users model = _db.Users.Find(Id);
                model.IsDeleted = true;

                _db.SaveChanges();
            }
        }


        public UsersModel UpdateCommonLookup(UsersModel model, int UpdatedBy)
        {
            var obj = GetUserById(model.Id);
            obj.FirstName = model.Name;
            obj.UserName = model.UserName;
                   

            _db.SaveChanges();
            return model;
        }


    }
}
