using TMS.Data.Database;
using TMS.Model;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TMS.Data
{
    public class FormRoleMappingProvider : BaseProvider
    {
        public FormRoleMappingProvider()
        {

        }

        public List<FormRoleMapping> GetAllRoleRights()
        {
            var getallrolerights = (from rights in _db.FormRoleMapping where rights.IsActive == true select rights).ToList();
            return getallrolerights;
        }

        public FormRoleMapping GetRoleRightsById(int Id)
        {
            return _db.FormRoleMapping.Find(Id);
        }
        public int InsertRoleRights(FormRoleMapping rolerights)
        {
            rolerights.CreatedBy = 1;
            _db.FormRoleMapping.Add(rolerights);
            try
            {
                _db.SaveChanges();
            }
            catch (Exception ex)
            {

                throw;
            }
            
            return rolerights.Id;
        }

  


        public List<FormRoleMappingModel> GetAllRoleRightsById(int RoleId)
        {
            var getformdata = (from f in _db.FormMst
                               select new
                               {
                                   Id = f.Id,
                                   Name = ((f.ParentFormId == null ? 0 : f.ParentFormId) == 0 ? f.Name : ((from fc in _db.FormMst where fc.Id == (f.ParentFormId == null ? 0 : f.ParentFormId) select fc).FirstOrDefault().Name) + " " + ">>" + " " + f.Name),
                               }).AsEnumerable().Select(x => new FormMst()
                               {
                                   Id = x.Id,
                                   Name = x.Name
                               }).ToList();

            List<FormRoleMappingModel> roleRights = new List<FormRoleMappingModel>();

            foreach (var formdata in getformdata)
            {
                FormRoleMappingModel roleRightsdata = new FormRoleMappingModel();

                var permission = GetAllRoleRights().Where(x => x.RoleId == RoleId && x.MenuId == formdata.Id).FirstOrDefault();

                if (permission != null)
                {
                    roleRightsdata.RoleId = permission.RoleId;
                    roleRightsdata.MenuId = formdata.Id;
                    roleRightsdata.FormName = formdata.Name;
                    roleRightsdata.AllowMenu = permission.AllowMenu;
                    roleRightsdata.FullRights = permission.FullRights;
                    roleRightsdata.AllowView = permission.AllowView;
                    roleRightsdata.AllowInsert = permission.AllowInsert;
                   
                    roleRightsdata.AllowDelete = permission.AllowDelete;
                }
                else
                {
                    roleRightsdata.RoleId = RoleId;
                    roleRightsdata.MenuId = formdata.Id;
                    roleRightsdata.FormName = formdata.Name;
                    roleRightsdata.AllowMenu = false;
                    roleRightsdata.FullRights = false;
                    roleRightsdata.AllowView = false;
                    roleRightsdata.AllowInsert = false;
                    roleRightsdata.AllowUpdate = false;
                    roleRightsdata.AllowDelete = false;
                }
                roleRights.Add(roleRightsdata);
            }
            return roleRights;
        }
        public void UpdateRoleRights(FormRoleMapping rolerights)
        {
            var getrolerights = GetRoleRightsById(rolerights.Id);

            if (getrolerights != null)
            {
                getrolerights.MenuId = rolerights.MenuId;
                getrolerights.FullRights = rolerights.FullRights;
                getrolerights.AllowMenu = rolerights.AllowMenu;
                getrolerights.AllowView = rolerights.AllowView;
                getrolerights.AllowInsert = rolerights.AllowInsert;
               
                getrolerights.AllowDelete = rolerights.AllowDelete;
                getrolerights.UpdatedBy = rolerights.UpdatedBy;
                getrolerights.UpdatedOn = rolerights.UpdatedOn;
                getrolerights.IsActive = rolerights.IsActive;
                _db.SaveChanges();
            }
        }
        public bool UpdateRoleRights(IEnumerable<FormRoleMappingModel> rolerights, int CreatedBy, int UpdatedBy)
        {

            FormRoleMapping frm = new FormRoleMapping();
            int roleID = Convert.ToInt16(rolerights.Select(p => p.RoleId).First());

            foreach (FormRoleMappingModel RoleRights in rolerights)
            {
                int MenuID = Convert.ToInt16(RoleRights.MenuId);
                string formCode = _db.FormMst.Where(a => a.Id == RoleRights.MenuId).FirstOrDefault().FormAccessCode;
                frm = GetAllRoleRights().Where(x => x.RoleId == roleID && x.MenuId == MenuID).FirstOrDefault();
                if (frm == null)
                {
                    if (RoleRights.AllowView == true || RoleRights.AllowInsert == true || RoleRights.AllowUpdate == true || RoleRights.AllowDelete == true)
                    {
                        frm = new FormRoleMapping();
                        frm.RoleId = roleID;
                        frm.MenuId = RoleRights.MenuId;
                        frm.FullRights = RoleRights.FullRights;
                        frm.AllowMenu = RoleRights.AllowMenu;
                        frm.AllowView = RoleRights.AllowView;
                        frm.AllowInsert = RoleRights.AllowInsert;
                        frm.AllowUpdate = RoleRights.AllowUpdate;
                        frm.AllowDelete = RoleRights.AllowDelete;
                        frm.CreatedBy = CreatedBy;
                        frm.CreatedOn = DateTime.UtcNow;
                        frm.UpdatedBy = UpdatedBy;
                        frm.UpdatedOn = DateTime.UtcNow;
                        frm.IsActive = true;
                        InsertRoleRights(frm);
                    }
                }
                else
                {
                    frm.RoleId = roleID;
                    frm.MenuId = RoleRights.MenuId;
                    frm.FullRights = RoleRights.FullRights;
                    frm.AllowMenu = RoleRights.AllowMenu;
                    frm.AllowView = RoleRights.AllowView;
                    frm.AllowInsert = RoleRights.AllowInsert;
                   
                    frm.AllowDelete = RoleRights.AllowDelete;
                    frm.UpdatedBy = UpdatedBy;
                    frm.UpdatedOn = DateTime.UtcNow;
                    frm.IsActive = true;
                    UpdateRoleRights(frm);
                }
            }
            return true;
        }

        public FormRoleMappingModel CheckFormAccess(string _formaccessCode)
        {
            int _roleID = (from user in _db.Users
                           join roles in _db.webpages_UsersInRoles on user.UserId equals roles.UserId
                           where user.EmailId == SessionHelper.EmailId
                           select roles.RoleId).FirstOrDefault();


            SqlParameter param1 = new SqlParameter("@formaccessCode", _formaccessCode);
            SqlParameter param2 = new SqlParameter("@roleID", _roleID);

            var _roleRights = _db.Database.SqlQuery<FormRoleMappingModel>("CheckFormAccess_sp  @formaccessCode, @roleID", param1, param2).FirstOrDefault();
            if (_roleRights != null)
            {

                AccessPermission.AllowMenu = _roleRights.AllowMenu;
                AccessPermission.View = _roleRights.AllowView;
                AccessPermission.Add = _roleRights.AllowInsert;
                AccessPermission.Edit = _roleRights.AllowUpdate;
                AccessPermission.Delete = _roleRights.AllowDelete;
            }
            else
            {
                AccessPermission.AllowMenu = true;
                AccessPermission.View = true;
                AccessPermission.Add = true;
                AccessPermission.Edit = true;
                AccessPermission.Delete = true;

            }
            return _roleRights;
        }

    }
}
