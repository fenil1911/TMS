using TMS.Data;
using TMS.Data.Database;
using TMS.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TMS.Service
{
    public class FormsService
    {
        public readonly FormsProvider _formsProvider;
        public FormsService()
        {
            _formsProvider = new FormsProvider();
        }

        public List<FormModel> GetAllForms()
        {
            var forms = _formsProvider.GetAllForms();
            return forms;
        }
        public FormModel SaveUpdateForm(FormModel forms)
        {
            return _formsProvider.SaveUpdateForm(forms);
        }
        public FormModel GetFormsById(int id, int CreatedBy)
        {
            var data = _formsProvider.GetFormsById(id);
            FormModel form = new FormModel()
            {
                Id = data.Id,
                Name = data.Name,
                NavigateURL = data.NavigateURL,
                DisplayOrder = data.DisplayOrder,
                FormAccessCode = data.FormAccessCode,
                IsActive = (bool)data.IsActive,
                IsDisplayMenu = data.IsDisplayMenu,
                ParentFormId = data.ParentFormId,
                CreatedOn = DateTime.Now,
                CreatedBy =CreatedBy
            };
            return form;
        }
        public FormModel GetFormsByCode(string formcode)
        {
            return _formsProvider.GetFormsByCode(formcode);
        }
        public List<FormMst> CheckDuplicateFormAccessCode(string formAccessCode)
        {
            return _formsProvider.CheckDuplicateFormAccessCode(formAccessCode);
        }
        public void DeleteForm(int Id)
        {
            _formsProvider.DeleteForm(Id);
        }
    }
}
