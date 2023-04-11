using TMS.Data.Database;
using TMS.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TMS.Data
{
    public class FormsProvider : BaseProvider
    {
        public FormsProvider()
        {

        }

        public int CreateForms(FormMst forms)
        {
            _db.FormMst.Add(forms);
            _db.SaveChanges();
            return forms.Id;
        }

        public int UpdateForms(FormMst forms)
        {
            _db.Entry(forms).State = System.Data.Entity.EntityState.Modified;
            _db.SaveChanges();
            return forms.Id;

        }
        public FormMst GetFormsById(int id)
        {
            return _db.FormMst.Find(id);
        }

        public FormModel GetFormsByCode(string formcode)
        {
            var FormCode = _db.FormMst.Where(a => a.FormAccessCode == formcode).FirstOrDefault();
            FormModel formmodel = new FormModel()
            {
                Id = FormCode.Id,
                Name = FormCode.Name,
                NavigateURL = FormCode.NavigateURL,
                DisplayOrder = FormCode.DisplayOrder,
                FormAccessCode = FormCode.FormAccessCode,
                IsActive = (bool)FormCode.IsActive,
                IsDisplayMenu = FormCode.IsDisplayMenu,
                ParentFormId = FormCode.ParentFormId
            };
            return formmodel;
        }
        public FormModel SaveUpdateForm(FormModel model)
        {
            FormMst obj = new FormMst();
            if (model.Id > 0)
            {
                obj = GetFormsById(model.Id);
            }
            {
                obj.Name = model.Name;
                obj.NavigateURL = model.NavigateURL;
                obj.DisplayOrder = model.DisplayOrder;
                obj.IsActive = model.IsActive;
                obj.IsDisplayMenu = model.IsDisplayMenu;
                obj.ParentFormId = model.ParentFormId;
                obj.CreatedOn = DateTime.Now;
                obj.Id = model.Id;
                if (obj.Id == 0)
                {
                    obj.FormAccessCode = model.FormAccessCode.ToUpper();
                    obj.CreatedBy = SessionHelper.UserId;
                    obj.CreatedOn = DateTime.UtcNow;
                    model.Id = CreateForms(obj);
                }
                else
                {
                    obj.UpdatedBy = SessionHelper.UserId;
                    obj.UpdatedOn = DateTime.UtcNow;
                    UpdateForms(obj);
                }
                return model;
            }
        }

        public List<FormModel> GetAllForms()
        {
            var allForms = (from f in _db.FormMst 
                           
                            
                            where f.IsDeleted == 0
                            
                            select new FormModel
                            {
                                Id = f.Id,
                                Name = (f.ParentFormId == null || f.ParentFormId == 0 ? f.Name :
                                        (from fc in _db.FormMst
                                         where fc.Id == f.ParentFormId
                                         select fc.Name).FirstOrDefault()
                                          + " >>" + " "
                                          + f.Name
                                        ),
                                NavigateURL = f.NavigateURL,
                                DisplayOrder = f.DisplayOrder,
                                FormAccessCode = f.FormAccessCode,
                                IsActive = (bool)f.IsActive,
                                IsDisplayMenu = f.IsDisplayMenu,
                                ParentFormId =  f.ParentFormId,
                                
                                
                             }).OrderByDescending(x => x.Id).ToList();

            return allForms;
        }
        public List<FormMst> CheckDuplicateFormAccessCode(string formAccessCode)
        {
            var getForm = (from form in _db.FormMst
                           where form.FormAccessCode.ToUpper().Trim() == formAccessCode.ToUpper().Trim()
                           select form).ToList();
            return getForm;
        }
        public void DeleteForm(int Id)
        {
            var data = GetFormsById(Id);
            if (data != null)
            {

                FormMst model = _db.FormMst.Find(Id);
                _db.FormMst.Remove(model);
               

                _db.SaveChanges();
            }
        }
    }
}
