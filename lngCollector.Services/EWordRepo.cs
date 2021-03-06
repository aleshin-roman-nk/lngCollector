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
            IQueryable<Sentence> res;

            using (var db = _dbFactory.Create())
            {
                res = db.Sentences.Where(s => s.WordId == wordId);
                db.Sentences.Add(new Sentence { Text = txt, WordId = wordId, date = DateTime.Now });

                increaseWordWeight(wordId, 1, db);
                db.SaveChanges();

                return res.OrderByDescending(x => x.Id).ToArray();
            }
        }

        public EWord Create(EWord ws)
        {
            using (var db = _dbFactory.Create())
            {
                if (ws.id == 0)// a new word - check if it already exists
                {
                    ws.weight = 0;
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

                Sentence? s_db = db.Sentences.FirstOrDefault(x => x.Id == s.Id);

                if (s_db != null)
                {
                    db.Sentences.Remove(s_db);
                    increaseWordWeight(s_db.WordId, -1, db);
                    db.SaveChanges();
                }

                res = db.Sentences.Where(x => x.WordId == s.WordId).OrderByDescending(x => x.Id).ToArray();


                //updateWeight(new EWord { id = s.WordId }, res, db);
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

        private void increaseWordWeight(int wordId, int vec1, AppDataDb db)
        {
            EWord? w = db.EWords.Where(wrd => wrd.id == wordId).Select(w => new EWord { id = w.id, weight = w.weight }).FirstOrDefault();
            w.weight += vec1;
            db.Entry(w).Property(x => x.weight).IsModified = true;
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
                    ws.weight = 0;
                    db.Entry(ws).State = EntityState.Added;
                    return db.SaveChanges();
                }

                db.Entry(ws).State = EntityState.Modified;
                return db.SaveChanges();
            }
        }

        public int SaveTextpartOnly(EWord ws)
        {
            using (var db = _dbFactory.Create())
            {
                db.EWords.Attach(ws);
                db.Entry(ws).Property(x => x.description).IsModified = true;
                db.Entry(ws).Property(x => x.text).IsModified = true;
                db.Entry(ws).Property(x => x.expressions).IsModified = true;
                db.Entry(ws).Property(x => x.examples).IsModified = true;
                db.Entry(ws).Property(x => x.meaning).IsModified = true;
                db.Entry(ws).Property(x => x.meaning_verb).IsModified = true;
                db.Entry(ws).Property(x => x.meaning_noun).IsModified = true;
                return db.SaveChanges();
            }
        }

        public Sentence? GetSentence(int id)
        {
            using (var db = _dbFactory.Create())
            {
                return db.Sentences.FirstOrDefault(x => x.Id == id);
            }
        }

        public int SaveSentence(Sentence s)
        {
            using (var db = _dbFactory.Create())
            {
                db.Sentences.Attach(s);
                db.Entry(s).Property(x => x.Text).IsModified = true;
                return db.SaveChanges();
            }
        }

        public int UpdateWeights()
        {
            using (var db = _dbFactory.Create())
            {
                var r = db.EWords.Select(wrd => new EWord { id = wrd.id, weight = db.Sentences.Count(x => x.WordId == wrd.id) });
                foreach (var item in r)
                {
                    db.Entry(item).Property(x => x.weight).IsModified = true;
                }
                return db.SaveChanges();
            }
        }
    }
}
