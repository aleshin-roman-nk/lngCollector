using lngCollector.Services.sqliteDb;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace lngCollector.Services
{
    public interface IAppDataDbFactory
    {
        AppDataDb Create();
    }
}
