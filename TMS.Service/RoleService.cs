using TMS.Data;
using TMS.Data.Database;
using TMS.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TMS.Service
{
    public class RoleService
    {
        public readonly RolesProvider _roleProvider;
        public RoleService()
        {
            _roleProvider = new RolesProvider();
        }

        public List<RolesModel> GetAllRoles()
        {
            var roles = _roleProvider.GetAllRoles();
            return roles;
        }

        public RolesModel GetRolesById()
        {
            var data = _roleProvider.GetRolesById();
            RolesModel role = new RolesModel()
            {
                Id = (int)data.RoleId,
                Name = data.RoleName,
                RoleCode = data.RoleCode,

            };
            return role;
        }
        public webpages_Roles GetRolesByName(string GetRolesByName)
        {
            return _roleProvider.GetRolesByName(GetRolesByName);
        }


    }
}

