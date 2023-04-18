using TMS.Data;
using TMS.Data.Database;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TMS.Model;

namespace TMS.Service
{
    public class UsersService
    {
        public readonly UsersProvider usersProvider;
        public UsersService()
        {
            usersProvider = new UsersProvider();
        }

        public UsersModel GetUserById(int Id)
        {
            var data = usersProvider.GetUserById(Id);
            UsersModel category = new UsersModel
            {
                FirstName = data.FirstName,
                LastName = data.LastName,
                IsActive = data.IsActive  ,
                UserName = data.UserName ,
                UserId = data.UserId   ,
                EmailId = data.EmailId
            };
            return category;
        }
        public List<webpages_RolesModel> GetDropdownBykey3(string key)
        {
            return usersProvider.GetAllUser().Where(a => a.Type.ToLower() == key.ToLower()).ToList(); 

        }
        public List<UsersModel> GetAlluser()
        {
            var GetAlluser = usersProvider.GetAllUser1();
            return GetAlluser;
        }
        public Users GetEmailById(string EmailId)
        {
          
        var email = usersProvider.GetEmailById(EmailId);
            return email;
        }  
        public Users GetroleById(string id)
        {
          
        var email = usersProvider.GetroleById(id);
            return email;
        }
        public void DeleteUser(int Id)
        {
            usersProvider.DeleteUser(Id);
        }
       

        public Users GetUserProfileById()
        {
            var userid = SessionHelper.UserId;
            var data = usersProvider.GetUserById(userid);

            var username = SessionHelper.UserName;
            Users userProfileModel = new Users()
            {

                FirstName = data.FirstName,
                LastName = data.LastName,
                IsActive = data.IsActive
                
            };
            return userProfileModel;

        }
        public UsersModel UpdateUserProfile(UsersModel userProfileModel)
        {
            return usersProvider.UpdateUserProfile(userProfileModel);
        }
      
    }
}
