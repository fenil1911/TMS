using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TMS.Model;

namespace TMS.Data
{
    public class MenuProvider : BaseProvider
    {
        public MenuProvider()
        {
        }



        public List<FormModel> BindMenyByRole(string role)
        {
            
            {
                var roleId = _db.webpages_Roles.Where(r => r.RoleName == role)
                                                   .Select(r => r.RoleId)
                                                   .FirstOrDefault();

                var forms = (from fm in _db.FormMst
                             join frm in _db.FormRoleMapping on fm.Id equals frm.MenuId
                             where frm.RoleId == roleId && frm.AllowView == true  && fm.IsDisplayMenu == true
                             select new FormModel
                             {
                                 Id = fm.Id,
                                 Name = fm.Name,
                                 NavigateURL = fm.NavigateURL,
                                 ParentFormId = fm.ParentFormId ?? 0,
                                 FormAccessCode = fm.FormAccessCode,
                                 DisplayOrder = fm.DisplayOrder,
                                 
                             }).Distinct().OrderBy(f => f.Id).ToList();

                return forms;
            }
        }


    }
}
