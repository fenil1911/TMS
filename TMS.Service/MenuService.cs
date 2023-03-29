using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TMS.Data;
using TMS.Model;

namespace TMS.Service
{
    public class MenuService
    {
        public readonly MenuProvider _menuProvider;
        public MenuService()
        {
            _menuProvider = new MenuProvider();
        }
        public List<FormModel> BindMenyByRole(string roleName)
        {
            
            var BindMenyByRole = _menuProvider.BindMenyByRole( roleName);
            return BindMenyByRole;
        }
    }
}
