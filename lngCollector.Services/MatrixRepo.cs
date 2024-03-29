﻿using lngCollector.Model;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace lngCollector.Services
{
    public class MatrixRepo : IMatrixRepo
    {
        private readonly IAppDataDbFactory _dbFactory;

        public MatrixRepo(IAppDataDbFactory dbFactory)
        {
            _dbFactory = dbFactory;
        }

        public Bunch? Get(int id)
        {
            using (var db = _dbFactory.Create())
            {
                // Only text, weight and matrix_id properties are needed in an entity EWord type.
                // In the EWord object type, only the text, weight, and matrix_id properties are required.

                var matr = (from m in db.Bunches
                            where m.id == id
                            select new
                            {
                                _m = m,
                                _words = (from wrd in db.EWords
                                         where wrd.bunch_id == m.id
                                         select new EWord
                                         {
                                             id = wrd.id,
                                             bunch_id = wrd.bunch_id,
                                             text = wrd.text,
                                             weight = wrd.weight
                                         }).ToList()
                            }
                    ).FirstOrDefault();

                Bunch res = matr._m;
                res.EWords = matr._words.ToList();

                return res;
            }
        }

        public IEnumerable<Bunch> GetAll(int lngid)
        {
            using (var db = _dbFactory.Create())
            {
                var matrxs = db.Bunches
                    .Where(m => !m.isdeleted && m.user_id == db.UserInfo.UID && m.lng_id == lngid)
                    .Select(m => new Bunch
                    {
                        id = m.id,
                        content_short = m.content_short,
                        name = m.name,
                        word_count = db.EWords.Where(x => x.bunch_id == m.id).Count(),
                        snt_count_actually = db.EWords.Where(x => x.bunch_id == m.id).Sum(x => x.weight),
                        snt_count_target = db.EWords.Where(x => x.bunch_id == m.id).Count() * 60
                    }).ToList();

                return matrxs;

                //return db.Bunches.Where(x => !x.isdeleted).ToList();
            }
        }

        public void Create(Bunch m)
        {
            using (var db = _dbFactory.Create())
            {
                m.isdeleted = false;
                db.Bunches.Add(m);
                db.SaveChanges();
            }
        }

        public void Save(Bunch m)
        {
            using (var db = _dbFactory.Create())
            {
                db.Entry(m).State = EntityState.Modified;
                db.SaveChanges();
            }
        }

        public void Delete(Bunch m)
        {
            using (var db = _dbFactory.Create())
            {
                db.Entry(m).State = EntityState.Deleted;
                db.SaveChanges();
            }
        }

        public void SendToBinRecycle(Bunch m)
        {
            using (var db = _dbFactory.Create())
            {
                m.isdeleted = true;
                db.Entry(m).Property(x => x.isdeleted).IsModified = true;
                db.SaveChanges();
            }
        }

        public IEnumerable<Bunch> AllDeleted()
        {
            using (var db = _dbFactory.Create())
            {
                return db.Bunches.Where(x => x.isdeleted).ToList();
            }
        }

        public void RestoreFromBinRecycle(int id)
        {
            using (var db = _dbFactory.Create())
            {
                var matr = db.Bunches.Select(m => new Bunch { id = m.id, isdeleted = m.isdeleted}).FirstOrDefault(x => x.id == id);

                if (matr != null)
                {
                    matr.isdeleted = false;
                    db.Entry(matr).Property(x => x.isdeleted).IsModified = true;
                    db.SaveChanges();
                }
            }
        }

        public void Delete(int id)
        {
            using (var db = _dbFactory.Create())
            {
                var m = new Bunch() { id = id };
                db.Bunches.Attach(m);
                db.Bunches.Remove(m);
                db.SaveChanges();
            }
        }

        public IEnumerable<Bunch> GetAll()
        {
            using (var db = _dbFactory.Create())
            {
                var matrxs = db.Bunches
                    .Where(m => !m.isdeleted && m.user_id == db.UserInfo.UID)
                    .Select(m => new Bunch
                    {
                        id = m.id,
                        content_short = m.content_short,
                        name = m.name,
                        word_count = db.EWords.Where(x => x.bunch_id == m.id).Count(),
                        snt_count_actually = db.EWords.Where(x => x.bunch_id == m.id).Sum(x => x.weight),
                        snt_count_target = db.EWords.Where(x => x.bunch_id == m.id).Count() * 60
                    }).ToList();

                return matrxs;

                //return db.Bunches.Where(x => !x.isdeleted).ToList();
            }
        }
    }
}
