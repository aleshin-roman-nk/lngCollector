using lngCollector.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace lngCollector.Services
{
    public interface IEWordRepo
    {
        int Save(EWord ws);
        void Delete(EWord ws);
        IEnumerable<EWord> GetAll();
    }
}
