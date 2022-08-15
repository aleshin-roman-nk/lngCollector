using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace lngCollector.Services.UserDt
{
    public interface IUserValuesRepo
    {
        string? GetValue(string valName);
    }
}
