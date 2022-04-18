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
        int SaveTextDescriptionOnly(EWord ws);
        IEnumerable<Sentence> GetSentences(int wordId);
        IEnumerable<Sentence> AddSentence(string txt, int wordId);
        IEnumerable<Sentence> DelSentence(Sentence s);
        void Delete(EWord ws);
        EWord Create(EWord ws);
        EWord Get(int id);
        IEnumerable<EWord> GetAll();
    }
}
