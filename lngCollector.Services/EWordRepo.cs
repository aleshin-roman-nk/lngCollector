using lngCollector.Model;
using lngCollector.Services.sqliteDb;
using lngCollector.Tools;
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

        public IEnumerable<Sentence> AddSentence(string txt, int wordId)
        {
            IEnumerable<Sentence> res;

            using (var db = _dbFactory.Create())
            {
                db.Sentences.Add(new Sentence { Text = txt, WordId = wordId });
                db.SaveChanges();
                res = db.Sentences.Where(s => s.WordId == wordId).OrderByDescending(x => x.Id).ToArray();

                updateWeight(new EWord { id = wordId }, res, db);
            }

            
            return res;
        }

        public EWord Create(EWord ws)
        {
            using (var db = _dbFactory.Create())
            {
                if (ws.id == 0)// a new word - check if it already exists
                {
                    ws.weight = 1;
                    if (db.EWords.Any(x => EF.Functions.Like(x.text, ws.text)))
                        throw new InvalidOperationException($"Word '{ws.text}' already exists");

                    db.Entry(ws).State = EntityState.Added;
                    db.SaveChanges();
                    
                    return ws;
                }

                throw new InvalidOperationException($"ID must not be 0");
            }
        }

        public void Delete(EWord ws)
        {
            using (var db = _dbFactory.Create())
            {
                db.Entry(ws).State = EntityState.Deleted;
                db.SaveChanges();
            }
        }

        public IEnumerable<Sentence> DelSentence(Sentence s)
        {
            if(s == null) return Enumerable.Empty<Sentence>();

            IEnumerable<Sentence> res;

            using (var db = _dbFactory.Create())
            {
                //var s = new Sentence { Id = id };
                //db.Sentences.Attach(s);
                //db.Sentences.Remove(s);

                Sentence s_db = db.Sentences.FirstOrDefault(x => x.Id == s.Id);

                //LoggerObj.Write(s);

                if (s_db != null)
                {
                    db.Sentences.Remove(s_db);
                    db.SaveChanges();
                }

                res = db.Sentences.Where(x => x.WordId == s.WordId).OrderByDescending(x => x.Id).ToArray();

                updateWeight(new EWord { id = s.WordId }, res, db);
            }

            return res;
        }

        public EWord Get(int id)
        {
            using (var db = _dbFactory.Create())
            {
                return db.EWords.FirstOrDefault(x => x.id == id);
            }
        }

        public IEnumerable<EWord> GetAll()
        {
            using (var db = _dbFactory.Create())
            {
                return db.EWords.ToList();
            }
        }

        private void updateWeight(EWord w, IEnumerable<Sentence> ws, AppDataDb db)
        {
           /*
            * 0 - 9 : 1
            * 10 - 19 : 2
            * 20 - 29 : 3
            * 30 - 39 : 4
            * 40 - 49 : 5
            * 50+ : 6
            */

            int lvl = ws.Count() / 10 + 1;
            if (lvl > 6) lvl = 6;
            if (lvl <= 0) lvl = 1;

            w.weight = lvl;

            db.EWords.Attach(w);
            db.Entry(w).Property(x => x.weight).IsModified = true;
            db.SaveChanges();
        }

        public IEnumerable<Sentence> GetSentences(int wordId)
        {
            using (var db = _dbFactory.Create())
            {
                return db.Sentences.Where(x => x.WordId == wordId).OrderByDescending(x => x.Id).ToArray();
            }
        }

        public int Save(EWord ws)
        {
            using(var db = _dbFactory.Create())
            {
                if (db.EWords.Any(x => EF.Functions.Like(x.text, ws.text) && x.id != ws.id ))
                    throw new InvalidOperationException($"Word '{ws.text}' already exists");

                if (ws.id == 0)// a new word - check if it already exists
                {
                    ws.weight = 1;
                    db.Entry(ws).State = EntityState.Added;
                    return db.SaveChanges();
                }

                db.Entry(ws).State = EntityState.Modified;
                return db.SaveChanges();
            }
        }

        public int SaveTextDescriptionOnly(EWord ws)
        {
            using (var db = _dbFactory.Create())
            {
                db.EWords.Attach(ws);
                db.Entry(ws).Property(x => x.description).IsModified = true;
                db.Entry(ws).Property(x => x.text).IsModified = true;
                return db.SaveChanges();
            }
        }
    }
}
