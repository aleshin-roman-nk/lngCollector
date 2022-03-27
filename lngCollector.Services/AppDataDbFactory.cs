using lngCollector.Services.sqliteDb;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace lngCollector.Services
{
    public class AppDataDbFactory : IAppDataDbFactory
    {
        string _path;

        public AppDataDbFactory(IDbConfig dbc)
        {
            _path = dbc.path;
        }

        public AppDataDb Create()
        {
            return new AppDataDb(_path);
        }
    }
}
