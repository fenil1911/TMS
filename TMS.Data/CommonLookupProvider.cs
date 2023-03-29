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

                                   }).OrderByDescending(X => X.Id).ToList();

            return allCommonLookup;


        }
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
            var obj = GetCommonLookupById(model.Id);
            obj.Type = model.Type;
            obj.Name = model.Name;
            obj.Code = model.Code;
            obj.DisplayOrder = model.DisplayOrder;
            obj.IsActive = model.IsActive;
            obj.UpdatedOn = DateTime.Now;
            obj.UpdatedBy = UpdatedBy;
            obj.UpdatedOn = DateTime.Now;


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
