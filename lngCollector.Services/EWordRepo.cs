using lngCollector.Model;
using lngCollector.Services.sqliteDb;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace lngCollector.Services
{
    public class EWordRepo : IEWordRepo
    {
        IAppDataDbFactory _dbFactory;

        public EWordRepo(IAppDataDbFactory dbFactory)
        {
            _dbFactory = dbFactory;
        }

        public void Delete(EWord ws)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<EWord> GetAll()
        {
            using (var db = _dbFactory.Create())
            {
                return db.EWords.ToList();
            }
        }

        public int Save(EWord ws)
        {
            using(var db = _dbFactory.Create())
            {
                if(ws.id == 0)// a new word - check if it already exists
                {
                    if (db.EWords.Any(x => EF.Functions.Like(x.text, ws.text)))
                        return 0;

                    db.Entry(ws).State = EntityState.Added;
                    return db.SaveChanges();
                }

                db.Entry(ws).State = EntityState.Modified;
                return db.SaveChanges();
            }
        }

    }
}
