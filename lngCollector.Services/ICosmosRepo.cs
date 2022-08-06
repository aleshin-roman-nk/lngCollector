using lngCollector.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace lngCollector.Services
{
    public interface ICosmosRepo
    {
        IEnumerable<Sentence> GetSentences(int year, int month);
        int SentensesCount();
        int SentensesCount(int year, int month);
    }
}
