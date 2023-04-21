using Kendo.Mvc;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TMS.Model;

namespace TMS.Data
{

    public class FilterProvider : BaseProvider
    {
        public FilterProvider()
        {

        }

        public IQueryable<FilterModel.ActivityLog> GetAllFilter(int pagesize, int page, IList<SortDescriptor> Sorts, string filters)
        {
            var GetCount = _db.activityLogs.Count();
            if (Sorts.Any())
            {
                var GetFilter = (from c in _db.activityLogs
                                 join b in _db.Users on
                                 c.UserId equals b.UserId into aemp
                                 from ae in aemp.DefaultIfEmpty()
                                 where (filters != null && c.ControllerName.Contains(filters) || c.ActionName.Contains(filters) 
                                                          || c.Id.ToString().Contains(filters) || ae.UserName.Contains(filters)
                                                          || c.PageUrl.Contains(filters)       || c.IPAddress.Contains(filters) 
                                                          || c.BrowserName.Contains(filters) || c.CreatedOn.ToString().Contains(filters)

                                 )
                                 select new FilterModel.ActivityLog
                                 {
                                         ActionName = c.ActionName,
                                     BrowserName = c.BrowserName,
                                        ControllerName = c.ControllerName,
                                     CreatedOn = c.CreatedOn,
                                         Id = c.Id,
                                     IPAddress = c.IPAddress,
                                     PageUrl = c.PageUrl,
                                        UserName = ae.UserName,
                                     count = GetCount
                                 }).OrderBy(x => x.Id) 
                                         
                                         
                                 .AsQueryable();
                return GetFilter;
            }
            else
            {
                var GetFilter = (from c in _db.activityLogs
                                 join b in _db.Users on
                                 c.UserId equals b.UserId into aemp
                                 from ae in aemp.DefaultIfEmpty()
                                 where (filters != null && c.ControllerName.Contains(filters) || c.ActionName.Contains(filters) || c.Id.ToString().Contains(filters) || ae.UserName.Contains(filters)
                                                          || c.PageUrl.Contains(filters) || c.IPAddress.Contains(filters)
                                                          || c.BrowserName.Contains(filters) || c.CreatedOn.ToString().Contains(filters)
                                 )
                                 select new FilterModel.ActivityLog
                                 {
                                     ActionName = c.ActionName,
                                     BrowserName = c.BrowserName,
                                     ControllerName = c.ControllerName,
                                     CreatedOn = c.CreatedOn,
                                     Id = c.Id,
                                     IPAddress = c.IPAddress,
                                     PageUrl = c.PageUrl,
                                     UserName = ae.UserName,
                                     count = GetCount
                                 }).OrderBy(x => x.Id)
                                         .Skip((page - 1) * pagesize)
                                         .Take(pagesize)
                                         .AsQueryable();
                return GetFilter;
            }
        }

        public IQueryable<FilterModel.ErrorLog> GetAllError(int pagesize, int page, IList<SortDescriptor> Sorts, string filters)
        {
            var GetCount = _db.errorLogs.Count();
            if (Sorts.Any())
            {
                var GetFilter = (from c in _db.errorLogs
                                 join b in _db.Users on
                                 c.UpdatedBy equals b.UserId into aemp

                                 from ae in aemp.DefaultIfEmpty()

                                 where (filters != null && c.ControllerName.Contains(filters) || c.ActionName.Contains(filters) || c.Id.ToString().Contains(filters)
                                 )
                                 select new FilterModel.ErrorLog
                                 {
                                     ActionName = c.ActionName,
                                     ControllerName = c.ControllerName,
                                     Message = c.Message,
                                     Id = c.Id,
                                     PageURL = c.PageURL,
                                     UserName = ae.UserName,
                                     UpdatedOn = c.UpdatedOn,
                                     count = GetCount
                                 })
                                         
                                      

                                         .AsQueryable();

                return GetFilter;
            }
            else
            {
                var GetFilter = (from c in _db.errorLogs
                                 join b in _db.Users on
                                  c.UpdatedBy equals b.UserId into aemp

                                 from ae in aemp.DefaultIfEmpty()

                                 where (filters != null && c.ControllerName.Contains(filters) || c.ActionName.Contains(filters) || c.Id.ToString().Contains(filters)
                                 )
                                // where (filters != null && (c.ControllerName.Contains(filters) || c.ActionName.Contains(filters)))
                                 select new FilterModel.ErrorLog
                                 {
                                     ActionName = c.ActionName,
                                     ControllerName = c.ControllerName,
                                     Message = c.Message,
                                     Id = c.Id,
                                     PageURL = c.PageURL,
                                     UserName = ae.UserName,
                                     UpdatedOn = c.UpdatedOn,
                                     count = GetCount
                                 }).OrderBy(x => x.Id)
                                         .Skip((page - 1) * pagesize)
                                         .Take(pagesize)
                                         .AsQueryable();
                return GetFilter;
            }


        }

    }
}
