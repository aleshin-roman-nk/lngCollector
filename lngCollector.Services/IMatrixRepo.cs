using lngCollector.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace lngCollector.Services
{
    public interface IMatrixRepo
    {
        IEnumerable<Bunch> GetAll();
        IEnumerable<Bunch> GetAll(int lngid);
        Bunch? Get(int id);
        void Save(Bunch matrix);
        void Create(Bunch m);
        void Delete(Bunch m);
        void Delete(int id);
        void SendToBinRecycle(Bunch m);
        IEnumerable<Bunch> AllDeleted();
        void RestoreFromBinRecycle(int id);
    }
}
