using lngCollector.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace lngCollector.WinForm
{
    public interface IMainView
    {
        void Display(IEnumerable<EWord> c);
        event EventHandler<EWord> SaveWord;
        event EventHandler<EWord> DeleteWord;
    }
}
