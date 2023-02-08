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
    public class CommonLookupController : BaseController
    {
        private readonly CommonLookupService commonLookupService;

        public CommonLookupController()
        {
            commonLookupService = new CommonLookupService();
        }
        public ActionResult Index(int? page)
        {
            try
            {
                if (!CheckPermission(AuthorizeFormAccess.FormAccessCode.COMMONLOOKUP.ToString(), AccessPermission.IsView))
                {

                }
                List<CommonLookupModel> List = commonLookupService.GetAllCommonLookup();
                return View(List.ToPagedList(page ?? 1, 9));
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public ActionResult Create()
        {
            CommonLookupModel model = new CommonLookupModel();
            return PartialView("Create", model);
        }
        [HttpPost]
        public ActionResult Create(CommonLookupModel model)
        {
          
            commonLookupService.CreateCommonLookup(model);
            return View();
          

        }


    }
}