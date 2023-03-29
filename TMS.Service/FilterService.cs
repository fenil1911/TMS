using Kendo.Mvc;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TMS.Data;
using TMS.Model;

namespace TMS.Service
{
    public class FilterService
    {

        private readonly FilterProvider filterProvider;
        public FilterService()
        {
            filterProvider = new FilterProvider();
        }
        public IQueryable<FilterModel.ActivityLog> GetAllFilter(int pagesize, int page, IList<SortDescriptor> Sorts, string filters)
        {
            var filter = filterProvider.GetAllFilter(pagesize, page, Sorts, filters);
            return filter;
        }
        public IQueryable<FilterModel.ErrorLog> GetAllError(int pagesize, int page, IList<SortDescriptor> Sorts, string filters)
        {

            var filter = filterProvider.GetAllError(pagesize, page, Sorts, filters);
             return filter;
        }
    }
}

