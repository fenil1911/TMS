using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using TMS.Filter;
using TMS.Helper;
using TMS.Model;
using TMS.Service;

namespace TMS.Controllers
{
    public class MenuController : Controller
    {
        private readonly MenuService _menuService;
        public MenuController()
        {
            _menuService = new MenuService();
        }

        public JsonResult GetFormByRole(string RoleName)
        {
            try
            {


                var getform = _menuService.BindMenyByRole(RoleName);
                string htmlstring = "";

                var MenuPath = ConfigurationManager.AppSettings["MenuPath"];
                Session["MenuStr"] = null;
                if (Session["MenuStr"] == null)
                {
                    htmlstring = htmlstring + "<ul>";
                    foreach (var item in getform)
                    {
                        int ParentFormId = Convert.ToInt32(item.ParentFormId);
                        if (ParentFormId == 0)
                        {
                            if (item.NavigateURL == "#")
                                htmlstring = htmlstring + "<li id=''left_menu'" + item.FormAccessCode + "_li'>  <i class=\"fa fa-angle-down arw\"></i> <a class=\"sub-menu-click\"  href='javascript://'><i class='" + item.Name + "'></i>" + item.Name + "</a>";
                            else
                            {
                                if (item.FormAccessCode == "POLICYDETAILS")
                                {
                                    htmlstring = htmlstring + "<li id=''left_menu'" + item.FormAccessCode + "_li'><a  href='" + MenuPath + "" + item.NavigateURL + "' ><i class='" + item.Name + "'></i>" + item.Name + "<span class='blink_me'>New</span></a>";
                                }
                                else if (item.FormAccessCode == "MYPROFILE")
                                {
                                    if (SessionHelper.RoleCode != Constants.RoleCode.ADMIN && SessionHelper.RoleCode != Constants.RoleCode.SADMIN)
                                    {
                                        htmlstring = htmlstring + "<li id='" + item.FormAccessCode + "_li'><a  href='" + MenuPath + "" + item.NavigateURL + "' ><i class='" + item.Name + "'></i>" + item.Name + "</a>";
                                    }
                                }
                                else
                                {
                                    htmlstring = htmlstring + "<li id='" + item.FormAccessCode + "_li'><a  href='" + MenuPath + "" + item.NavigateURL + "' ><i class='" + item.Name + "'></i>" + item.Name + "</a>";
                                }
                            }
                            var getchildform = getform.Where(a => a.ParentFormId == item.Id).ToList();
                            if (getchildform.Count > 0)
                            {
                                htmlstring = htmlstring + "<div class=\"sub_menu\">";
                                foreach (var childform in getchildform)
                                {
                                    htmlstring = htmlstring + "<a id='" + childform.FormAccessCode + "_a' href='" + MenuPath + "" + childform.NavigateURL + "'>" + childform.Name + "</a>";
                                }
                                htmlstring = htmlstring + "</div>";
                            }

                            htmlstring = htmlstring + "</li>";
                        }
                    }
                    htmlstring = htmlstring + "</ul>";
                    htmlstring = htmlstring + "<script>$(document).ready(function () {$('.sub-menu-click').click(function () {$('.sub-menu-click').removeClass('active');if ($(this).next().is(':visible')) {$(this).next().slideUp();} else {$(this).next().slideDown();$(this).addClass('active')}}); });</script>";

                    Session["MenuStr"] = htmlstring;
                }
                else
                {
                    htmlstring = Convert.ToString(Session["MenuStr"]);
                }
                return Json(htmlstring, JsonRequestBehavior.AllowGet);

            }
            catch (Exception ex)
            {

                throw ex;
            }
        }
    }
}