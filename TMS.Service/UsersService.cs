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

        public Users GetUserById(int Id)
        {
            return usersProvider.GetUserById(Id);
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
        public UsersModel UpdateCommonLookup(UsersModel model, int UpdatedBy)
        {
            return usersProvider.UpdateCommonLookup(model, UpdatedBy);
        }
    }
}
