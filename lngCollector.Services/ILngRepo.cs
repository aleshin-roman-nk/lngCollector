using lngCollector.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace lngCollector.Services
{
    public interface ILngRepo
    {
        IEnumerable<Lng> All();
        Lng Get(int id);
        void Create(string sh_name, string full_name);
    }
}
