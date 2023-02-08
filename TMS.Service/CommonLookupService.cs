using TMS.Data;
using TMS.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TMS.Service
{
    public class CommonLookupService
    {
        private readonly CommonLookupProvider commonLookupProvider;
        public CommonLookupService()
        {
            commonLookupProvider = new CommonLookupProvider();
        }
        public CommonLookupModel GetCommonLookupById(int id)
        {
            var data = commonLookupProvider.GetCommonLookupById(id);
            CommonLookupModel category = new CommonLookupModel
            {
                Id = data.Id,
           
                Type = data.Type,
                Code = data.Code,
                Name = data.Name,
                DisplayOrder = data.DisplayOrder,
                IsActive = data.IsActive
            };
            return category;

        }
        public List<CommonLookupModel> GetAllCommonLookup()
        {
            var cammonlookup = commonLookupProvider.GetAllCommonLookup();
            return cammonlookup;
        }
        public int CreateCommonLookup(CommonLookupModel objcommonLookup)
        {
            return commonLookupProvider.CreateCommonLookup(objcommonLookup);
        }
        public CommonLookupModel UpdateCommonLookup(CommonLookupModel model)
        {
            return commonLookupProvider.UpdateCommonLookup(model);
        }
    }
}
