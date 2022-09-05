using lngCollector.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace lngCollector.Services
{
    public class LngRepo : ILngRepo
    {
        private readonly IAppDataDbFactory dbFactory;

        public LngRepo(IAppDataDbFactory dbFactory)
        {
            this.dbFactory = dbFactory;
        }

        public IEnumerable<Lng> All()
        {
            using (var db = dbFactory.Create())
            {
                return db.Lngs.ToList();
            }
        }

        public void Create(string sh_name, string full_name)
        {
            using (var db = dbFactory.Create())
            {
                db.Lngs.Add(new Lng { name = full_name, short_name = sh_name });
                db.SaveChanges();
            }
        }

        public Lng Get(int id)
        {
            using (var db = dbFactory.Create())
            {
                return db.Lngs.FirstOrDefault(x => x.id == id);
            }
        }
    }
}
