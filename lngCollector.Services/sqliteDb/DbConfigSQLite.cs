using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace lngCollector.Services.sqliteDb
{
    public class DbConfigSQLite : IDbConfig
    {
        public string path => "lng.db";
    }
}
