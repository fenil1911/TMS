using TMS.Data.Database;
using TMS.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
namespace TMS.Data
{
    public class BaseProvider : IDisposable
    {
        public TMSEntities _db;
        public BaseProvider()
        {
            _db = new TMSEntities();
        }

        public void Dispose()
        {
            _db.Dispose();
        }
    }
}
