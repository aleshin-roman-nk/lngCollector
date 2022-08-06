using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace lngCollector.Services
{
    public interface IDbConfig
    {
        string path_db_file { get; }
        string path { get; }
    }
}
