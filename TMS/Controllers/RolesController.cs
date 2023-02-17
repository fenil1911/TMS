using TMS.Model;
using TMS.Service;
using PagedList;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace TMS.Controllers
{
    public class RolesController : BaseController
    {

        private readonly RoleService _rolesService;
        public RolesController()
        {
            try
            {
                _rolesService = new RoleService();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public ActionResult Index(int? page)
        {
            if (!CheckPermission(AuthorizeFormAccess.FormAccessCode.ROLE.ToString(), AccessPermission.IsView))
            {

            }
            List<RolesModel> RoleList = _rolesService.GetAllRoles();
            return View(RoleList.ToPagedList(page ?? 1, 6));


        }
    }
}