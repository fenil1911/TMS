using TMS.Model;
using TMS.Service;
using PagedList;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Kendo.Mvc.UI;
using Kendo.Mvc.Extensions;

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

        public ActionResult Index([DataSourceRequest] DataSourceRequest request)
        {
            try
            {

              /*  if (!CheckPermission(AuthorizeFormAccess.FormAccessCode.ROLE.ToString(), AccessPermission.IsView))
                {
                    return RedirectToAction("AccessDenied", "Base");
                }*/
                return View();
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }
        public ActionResult GetGridData([DataSourceRequest] DataSourceRequest request)
        {
            try
            {

               

                    List<RolesModel> RoleList = _rolesService.GetAllRoles();
                    DataSourceResult result = RoleList.ToDataSourceResult(request);
                    return Json(result, JsonRequestBehavior.AllowGet);
               
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }

    }
}