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
        private readonly IUserInfo userInfo;
        string _path;

        public AppDataDbFactory(IDbConfig dbc, IUserInfo userInfo)
        {
            _path = dbc.path_db_file;
            this.userInfo = userInfo;
        }

        public AppDataDb Create()
        {
            return new AppDataDb(_path, userInfo);
        }
    }
}
