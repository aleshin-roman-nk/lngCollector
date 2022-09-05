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
        IEnumerable<Matrix> GetAll();
        IEnumerable<Matrix> GetAll(int lngid);
        Matrix? Get(int id);
        void Save(Matrix matrix);
        void Create(Matrix m);
        void Delete(Matrix m);
        void Delete(int id);
        void SendToBinRecycle(Matrix m);
        IEnumerable<Matrix> AllDeleted();
        void RestoreFromBinRecycle(int id);
    }
}
