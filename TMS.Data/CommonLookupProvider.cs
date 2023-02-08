using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TMS.Data.Database;
using TMS.Model;

namespace TMS.Data
{

    public class CommonLookupProvider : BaseProvider
    {
        public CommonLookupProvider()
        {

        }
        public CommonLookup GetCommonLookupById(int Id)
        {
            return _db.CommonLookup.Find(Id);
        }
        public List<CommonLookupModel> GetAllCommonLookup()
        {
            //var ram = _db.CategoryMst.ToList();
            return _db.CommonLookup.Select(x => new CommonLookupModel
            {
                Id = x.Id,
                Type = x.Type,
                Code = x.Code,
                Name = x.Name,
                DisplayOrder = x.DisplayOrder,
                IsActive = x.IsActive
            }).ToList();
        }
        public int CreateCommonLookup(CommonLookupModel commonLookup)
        {
            CommonLookup _commonLookup = new CommonLookup()
            {
                Id = commonLookup.Id,
                Name = commonLookup.Name,
                Code = commonLookup.Code,
                Type = commonLookup.Type,
                DisplayOrder = commonLookup.DisplayOrder,
                IsActive = commonLookup.IsActive,
                CreatedOn = DateTime.Now
            };
            _db.CommonLookup.Add(_commonLookup);
            _db.SaveChanges();
            return _commonLookup.Id;
        }
        public CommonLookupModel UpdateCommonLookup(CommonLookupModel model)
        {
            var objclup = GetCommonLookupById(model.Id);
            objclup.Type = model.Type;
            objclup.Name = model.Name;
            objclup.Code = model.Code;
            objclup.DisplayOrder = model.DisplayOrder;
            objclup.IsActive = model.IsActive;
            objclup.UpdatedOn = DateTime.Now;
            _db.SaveChanges();
            return model;
        }
    }
}
