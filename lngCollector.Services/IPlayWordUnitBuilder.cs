using lngCollector.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace lngCollector.Services
{

    public interface IPlayWordUnitBuilder
    {
        void Save(PlayWordUnit w);
        PlayWordUnit Create(EWord w);
    }
}
