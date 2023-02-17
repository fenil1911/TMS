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
            var allCommonLookup = (from c in _db.CommonLookup
                                   where c.IsDeleted != 1
                                   select new CommonLookupModel
                                   {
                                       Code = c.Code,
                                       DisplayOrder = c.DisplayOrder,
                                       Id = c.Id,
                                       IsActive = c.IsActive,
                                       Name = c.Name,
                                       Type = c.Type

                                   }).ToList();

            return allCommonLookup;


        }


            /*return _db.CommonLookup.Select(x => new CommonLookupModel  
            
            {
                Id = x.Id,
                Type = x.Type,
                Code = x.Code,
                Name = x.Name,
                DisplayOrder = x.DisplayOrder,
                IsActive = x.IsActive
            }).ToList();*/


        
        public int CreateCommonLookup(CommonLookupModel commonLookup, int CreatedBy)
        {
            CommonLookup _commonLookup = new CommonLookup()
            {
                Id = commonLookup.Id,
                Name = commonLookup.Name,
                Code = commonLookup.Code,
                Type = commonLookup.Type,
                DisplayOrder = commonLookup.DisplayOrder,
                IsActive = commonLookup.IsActive,
                CreatedOn = DateTime.Now,
                CreatedBy = CreatedBy,


            };
            _db.CommonLookup.Add(_commonLookup);
            _db.SaveChanges();
            return _commonLookup.Id;
        }
        public CommonLookupModel UpdateCommonLookup(CommonLookupModel model, int UpdatedBy)
        {
            var objclup = GetCommonLookupById(model.Id);
            objclup.Type = model.Type;
            objclup.Name = model.Name;
            objclup.Code = model.Code;
            objclup.DisplayOrder = model.DisplayOrder;
            objclup.IsActive = model.IsActive;
            objclup.UpdatedOn = DateTime.Now;
            objclup.UpdatedBy = UpdatedBy;
            objclup.UpdatedOn = DateTime.Now;


            _db.SaveChanges();
            return model;
        }
        public void DeleteCommonLookup(int Id)
        {
            var data = GetCommonLookupById(Id);
            if (data != null)
            {

                CommonLookup model = _db.CommonLookup.Find(Id);
                model.IsDeleted = 1;

                _db.SaveChanges();
            }
        }
    }
}
