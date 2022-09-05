using lngCollector.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace lngCollector.Services
{
    public class StoryRepo
    {
        private readonly IAppDataDbFactory dbFactory;

        public StoryRepo(IAppDataDbFactory dbFactory)
        {
            this.dbFactory = dbFactory;
        }

        public IEnumerable<Story> Stories(int year, int month)
        {
            using (var db = dbFactory.Create()) 
                return db.Stories.Where(x => x.userId == db.UserInfo.UID).TakeLast(30).ToArray();
        }
    }
}
