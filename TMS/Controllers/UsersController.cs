﻿using TMS.Model;
using TMS.Service;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using TMS.Data.Database;
using Kendo.Mvc.Extensions;
using Kendo.Mvc.UI;

namespace TMS.Controllers
{
    public class UsersController : BaseController
    {

        private readonly RoleService _rolesService;
        private readonly FormRoleMappingService _formRoleMapping;
        private readonly FormsService _formsService;
        private readonly UsersService _usersService;
        public UsersController()
        {
            try
            {
                _rolesService = new RoleService();
                _formRoleMapping = new FormRoleMappingService();
                _formsService = new FormsService();
                _usersService = new UsersService();
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

                if (!CheckPermission(AuthorizeFormAccess.FormAccessCode.USER.ToString(), AccessPermission.IsView))
                {
                    return RedirectToAction("AccessDenied", "Base");
                }
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
                List<UsersModel> List = _usersService.GetAlluser();
                DataSourceResult result = List.ToDataSourceResult(request);
                return Json(result, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public ActionResult Delete(int Id)
        {
            try
            {
                if (!CheckPermission(AuthorizeFormAccess.FormAccessCode.USER.ToString(), AccessPermission.IsDelete))
                {
                    return RedirectToAction("AccessDenied", "Base");
                }
                _usersService.DeleteUser(Id);
                return RedirectToAction("Index");

            }
            catch (Exception ex)
            {

                throw ex;
            }
        }


    }
}