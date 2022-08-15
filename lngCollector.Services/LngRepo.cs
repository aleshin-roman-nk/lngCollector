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
    }
}
