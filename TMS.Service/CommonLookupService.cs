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
        private static bool UpdateDatabase = false;
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
        public int CreateCommonLookup(CommonLookupModel objcommonLookup, int CreatedBy)
        {
            return commonLookupProvider.CreateCommonLookup(objcommonLookup, CreatedBy);
        }
        public CommonLookupModel UpdateCommonLookup(CommonLookupModel model ,int UpdatedBy)
        {
            return commonLookupProvider.UpdateCommonLookup(model, UpdatedBy);
        }
        public void DeleteCommonLookup(int Id)
        {
            commonLookupProvider.DeleteCommonLookup(Id);
        }


     



    }
}
