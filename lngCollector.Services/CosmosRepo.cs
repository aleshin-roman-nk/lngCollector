using lngCollector.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace lngCollector.Services
{
    public class CosmosRepo : ICosmosRepo
    {
        private readonly IAppDataDbFactory _dbFactory;

        public CosmosRepo(IAppDataDbFactory dbFactory)
        {
            _dbFactory = dbFactory;
        }

        public IEnumerable<Sentence> GetSentences(int year, int month)
        {
            DateTime dt1 = new DateTime(year, month, 1);
            DateTime dt2 = dt1.AddMonths(1);

            using var db = _dbFactory.Create();
            return db.Sentences.Where(x => x.user_id == db.UserInfo.UID && x.date >= dt1 && x.date < dt2).ToList();
        }

        public int SentensesCount(int year, int month)
        {
            DateTime dt1 = new DateTime(year, month, 1);
            DateTime dt2 = dt1.AddMonths(1);

            using var db = _dbFactory.Create();
            return db.Sentences.Count(x => x.user_id == db.UserInfo.UID && x.date >= dt1 && x.date < dt2);
        }

        public int SentensesCount()
        {
            using var db = _dbFactory.Create();
            return db.Sentences.Count(x => x.user_id == db.UserInfo.UID);
        }
    }
}
