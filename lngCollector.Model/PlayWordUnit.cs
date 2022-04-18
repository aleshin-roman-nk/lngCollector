using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace lngCollector.Model
{
    /// <summary>
    /// Репозиторий одного экземпляра. Ассемблер игрового объекта, единственной физ-модели для страницы игры.
    /// </summary>
    public class PlayWordUnit
    {


        public List<Sentence> Sentences { get; }

        public void AddSentence(string txt)
        {

        }

        public void DeleteSentence(int id)
        {

        }

        public int GetLevel()
        {
            return 0;
        }

        public void Save()
        {

        }
    }
}
