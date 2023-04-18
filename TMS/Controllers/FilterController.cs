using Kendo.Mvc;
using Kendo.Mvc.Extensions;
using Kendo.Mvc.UI;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using TMS.Data.Database;
using TMS.Helper;
using TMS.Model;
using TMS.Service;

namespace TMS.Controllers
{

    public class FilterController : BaseController
    {
        private readonly FilterService filterService;

        public FilterController()
        {
            filterService = new FilterService();
        }
        public ActionResult Activity([DataSourceRequest] DataSourceRequest request)
        {
            if (!CheckPermission(AuthorizeFormAccess.FormAccessCode.ACTIVITYLOG.ToString(), AccessPermission.IsView))
            {
                return RedirectToAction("AccessDenied", "Base");
            }
            return View();
        }
        public ActionResult IndexActivityGrid([DataSourceRequest] DataSourceRequest request)
        {
            try
            {

                string filterCondition = "";

                foreach (IFilterDescriptor filter in request.Filters)
                {
                    FilterDescriptor filterDesc = (FilterDescriptor)filter;
                    filterCondition = filterDesc.Value.ToString();
                }

                IQueryable<FilterModel.ActivityLog> List = filterService.GetAllFilter(request.PageSize, request.Page, request.Sorts, filterCondition);
                DataSourceResult result = List.ToDataSourceResult(request);

                var sortedlist = ApplyOrdersSortingLog(List, request.Sorts, request.PageSize, request.Page);
                var returndata = new DataSourceResult()
                {
                    Data = sortedlist,
                    Total = List.ToList().Count > 0 ? List.FirstOrDefault().count : 0
                };

                return Json(returndata, JsonRequestBehavior.AllowGet);


            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public ActionResult ErrorLog([DataSourceRequest] DataSourceRequest request)
        {
            if (!CheckPermission(AuthorizeFormAccess.FormAccessCode.ERRORLOG.ToString(), AccessPermission.IsView))
            {
                return RedirectToAction("AccessDenied", "Base");
            }
            return View();
        }

        public ActionResult IndexErrorLogGrid([DataSourceRequest] DataSourceRequest request)

        {
            try
            {

                string filterCondition = "";

                foreach (IFilterDescriptor filter in request.Filters)
                {
                    FilterDescriptor filterDesc = (FilterDescriptor)filter;
                    filterCondition = filterDesc.Value.ToString();
                }

                IQueryable<FilterModel.ErrorLog> List = filterService.GetAllError(request.PageSize, request.Page, request.Sorts, filterCondition);
                DataSourceResult result = List.ToDataSourceResult(request);

                var sortedlist = ApplyOrdersSorting(List, request.Sorts, request.PageSize, request.Page);
                var returndata = new DataSourceResult()
                {
                    Data = sortedlist,
                    Total = List.ToList().Count > 0 ? List.FirstOrDefault().count : 0
                };

                return Json(returndata, JsonRequestBehavior.AllowGet);


            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public IQueryable<FilterModel.ActivityLog> ApplyOrdersSortingLog(IQueryable<FilterModel.ActivityLog> data, IList<SortDescriptor> sortDescriptors, int PageSize, int Page)
        {


            if (sortDescriptors != null && sortDescriptors.Any())
            {
                foreach (SortDescriptor sortDescriptor in sortDescriptors)
                {
                    data = (IQueryable<FilterModel.ActivityLog>)AddSortExpressionlog(data, sortDescriptor.SortDirection, sortDescriptor.Member, PageSize, Page);
                }
            }

            return data;
        }
        public IQueryable<FilterModel.ErrorLog> ApplyOrdersSorting(IQueryable<FilterModel.ErrorLog> data, IList<SortDescriptor> sortDescriptors, int PageSize, int Page)
        {


            if (sortDescriptors != null && sortDescriptors.Any())
            {
                foreach (SortDescriptor sortDescriptor in sortDescriptors)
                {
                    data = (IQueryable<FilterModel.ErrorLog>)AddSortExpression(data, sortDescriptor.SortDirection, sortDescriptor.Member, PageSize, Page);
                }
            }

            return data;
        }
        public IQueryable<FilterModel.ActivityLog> AddSortExpressionlog(IQueryable<FilterModel.ActivityLog> data, ListSortDirection
                    sortDirection, string memberName, int PageSize, int Page)
        {
            if (sortDirection == ListSortDirection.Ascending)
            {
                switch (memberName)
                {
                    case "ActionName":
                        data = data.OrderBy(order => order.ActionName).Skip((Page - 1) * PageSize).Take(PageSize);
                        break;
                    case "ControllerName":
                        data = data.OrderBy(order => order.ControllerName).Skip((Page - 1) * PageSize).Take(PageSize);
                        break;
                    case "BrowserName":
                        data = data.OrderBy(order => order.BrowserName).Skip((Page - 1) * PageSize).Take(PageSize);
                        break;
                    case "Id":
                        data = data.OrderBy(order => order.Id).Skip((Page - 1) * PageSize).Take(PageSize);
                        break;
                    case "PageUrl":
                        data = data.OrderBy(order => order.Id).Skip((Page - 1) * PageSize).Take(PageSize);
                        break;
                    case "IPAddress":
                        data = data.OrderBy(order => order.Id).Skip((Page - 1) * PageSize).Take(PageSize);
                        break;
                    case "UserName":
                        data = data.OrderBy(order => order.Id).Skip((Page - 1) * PageSize).Take(PageSize);
                        break; 
                        case "CreatedOn":
                        data = data.OrderBy(order => order.Id).Skip((Page - 1) * PageSize).Take(PageSize);
                        break;
                }
            }
            else
            {
                switch (memberName)
                {
                    case "ActionName":
                        data = data.OrderByDescending(order => order.ActionName).Skip((Page - 1) * PageSize).Take(PageSize);
                        break;
                    case "ControllerName":
                        data = data.OrderByDescending(order => order.ControllerName).Skip((Page - 1) * PageSize).Take(PageSize);
                        break;

                    case "BrowserName":
                        data = data.OrderByDescending(order => order.BrowserName).Skip((Page - 1) * PageSize).Take(PageSize);
                        break;
                     case "Id":
                        data = data.OrderByDescending(order => order.Id).Skip((Page - 1) * PageSize).Take(PageSize);
                        break;
                    case "PageUrl":
                        data = data.OrderByDescending(order => order.Id).Skip((Page - 1) * PageSize).Take(PageSize);
                        break;
                    case "IPAddress":
                        data = data.OrderByDescending(order => order.Id).Skip((Page - 1) * PageSize).Take(PageSize);
                        break;
                    case "UserName":
                        data = data.OrderByDescending(order => order.Id).Skip((Page - 1) * PageSize).Take(PageSize);
                        break;  
                    case "CreatedOn":
                        data = data.OrderByDescending(order => order.Id).Skip((Page - 1) * PageSize).Take(PageSize);
                        break;
                }
            }
            return data;
        }
      

        public IQueryable<FilterModel.ErrorLog> AddSortExpression(IQueryable<FilterModel.ErrorLog> data, ListSortDirection
                    sortDirection, string memberName, int PageSize, int Page)
        {
            if (sortDirection == ListSortDirection.Ascending)
            {
                switch (memberName)
                {
                    case "ActionName":
                        data = data.OrderBy(order => order.ActionName).Skip((Page - 1) * PageSize).Take(PageSize);
                        break;
                    case "ControllerName":
                        data = data.OrderBy(order => order.ControllerName).Skip((Page - 1) * PageSize).Take(PageSize);
                        break;

                    case "Id":
                        data = data.OrderBy(order => order.Id).Skip((Page - 1) * PageSize).Take(PageSize);
                        break;
                    case "UserName":
                        data = data.OrderBy(order => order.UserName).Skip((Page - 1) * PageSize).Take(PageSize);
                        break;
                    case "UpdatedOn":
                        data = data.OrderBy(order => order.UpdatedOn).Skip((Page - 1) * PageSize).Take(PageSize);
                        break;

                }
            }
            else
            {
                switch (memberName)
                {
                    case "ActionName":
                        data = data.OrderByDescending(order => order.ActionName).Skip((Page - 1) * PageSize).Take(PageSize);
                        break;
                    case "ControllerName":
                        data = data.OrderByDescending(order => order.ControllerName).Skip((Page - 1) * PageSize).Take(PageSize);
                        break;

                    case "Id":
                        data = data.OrderByDescending(order => order.Id).Skip((Page - 1) * PageSize).Take(PageSize);
                        break;
                    case "UserName":
                        data = data.OrderByDescending(order => order.UserName).Skip((Page - 1) * PageSize).Take(PageSize);
                        break;
                    case "UpdatedOn":
                        data = data.OrderByDescending(order => order.UpdatedOn).Skip((Page - 1) * PageSize).Take(PageSize);
                        break;
                }
            }
            return data;
        }

    }
}