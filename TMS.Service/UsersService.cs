using TMS.Data;
using TMS.Data.Database;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TMS.Service
{
    public class UsersService
    {
        public readonly UsersProvider usersProvider;
        public UsersService()
        {
            usersProvider = new UsersProvider();
        }

        public Users GetUserById(int UserId)
        {
            return usersProvider.GetUserById(UserId);
        }
       
    }
}
