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
    public class FormRoleMappingService
    {
        public readonly FormRoleMappingProvider _formrolemappingProvider;
        public FormRoleMappingService()
        {
            _formrolemappingProvider = new FormRoleMappingProvider();
        }
        public FormRoleMappingModel CheckFormAccess(string _formaccessCode)
        {
            return _formrolemappingProvider.CheckFormAccess(_formaccessCode);
        }

        public int InsertRoleRights(FormRoleMapping rolerights)
        {
            return _formrolemappingProvider.InsertRoleRights(rolerights);
        }

        public List<FormRoleMappingModel> GetAllRoleRightsById(int RoleId)
        {
            return _formrolemappingProvider.GetAllRoleRightsById(RoleId);
        }

        public bool UpdateRoleRights(IEnumerable<FormRoleMappingModel> rolerights, int CreatedBy, int UpdatedBy)
        {
            return _formrolemappingProvider.UpdateRoleRights(rolerights, CreatedBy, UpdatedBy);
        }

    }
}
