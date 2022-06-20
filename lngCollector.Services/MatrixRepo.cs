using lngCollector.Model;
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

        public Matrix? Get(int id)
        {
            using (var db = _dbFactory.Create())
            {
                // Only text, weight and matrix_id properties are needed in an entity EWord type.
                // In the EWord object type, only the text, weight, and matrix_id properties are required.

                var matr = (from m in db.Matrixs
                            where m.id == id
                            select new
                            {
                                _m = m,
                                _words = (from wrd in db.EWords
                                         where wrd.MatrixId == m.id
                                         select new EWord
                                         {
                                             id = wrd.id,
                                             MatrixId = wrd.MatrixId,
                                             text = wrd.text,
                                             weight = wrd.weight
                                         }).ToList()
                            }
                    ).FirstOrDefault();

                Matrix res = matr._m;
                res.EWords = matr._words.ToList();

                return res;
            }
        }

        public IEnumerable<Matrix> GetAll()
        {
            using (var db = _dbFactory.Create())
            {
                var matrxs = db.Matrixs
                    .Where(m => !m.isdeleted)
                    .Select(m => new Matrix
                    {
                        id = m.id,
                        content_short = m.content_short,
                        name = m.name,
                        word_count = db.EWords.Where(x => x.MatrixId == m.id).Count(),
                        snt_count_actually = db.EWords.Where(x => x.MatrixId == m.id).Sum(x => x.weight),
                        snt_count_target = db.EWords.Where(x => x.MatrixId == m.id).Count() * 60
                    }).ToList();

                return matrxs;

                //return db.Matrixs.Where(x => !x.isdeleted).ToList();
            }
        }

        public void Create(Matrix m)
        {
            using (var db = _dbFactory.Create())
            {
                m.isdeleted = false;
                db.Matrixs.Add(m);
                db.SaveChanges();
            }
        }

        public void Save(Matrix m)
        {
            using (var db = _dbFactory.Create())
            {
                db.Entry(m).State = EntityState.Modified;
                db.SaveChanges();
            }
        }

        public void Delete(Matrix m)
        {
            using (var db = _dbFactory.Create())
            {
                db.Entry(m).State = EntityState.Deleted;
                db.SaveChanges();
            }
        }

        public void SendToBinRecycle(Matrix m)
        {
            using (var db = _dbFactory.Create())
            {
                m.isdeleted = true;
                db.Entry(m).Property(x => x.isdeleted).IsModified = true;
                db.SaveChanges();
            }
        }

        public IEnumerable<Matrix> AllDeleted()
        {
            using (var db = _dbFactory.Create())
            {
                return db.Matrixs.Where(x => x.isdeleted).ToList();
            }
        }

        public void RestoreFromBinRecycle(int id)
        {
            using (var db = _dbFactory.Create())
            {
                var matr = db.Matrixs.Select(m => new Matrix { id = m.id, isdeleted = m.isdeleted}).FirstOrDefault(x => x.id == id);

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
                var m = new Matrix() { id = id };
                db.Matrixs.Attach(m);
                db.Matrixs.Remove(m);
                db.SaveChanges();
            }
        }
    }
}
