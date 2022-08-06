using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace lngCollector
{
    // Объект для доступа данных из coockie
    public interface IUserInfo
    {
        string Email { get; }
        string Name { get; }
        int UID { get; }
    }
}
