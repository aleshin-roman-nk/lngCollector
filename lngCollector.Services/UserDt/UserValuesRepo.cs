using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace lngCollector.Services.UserDt
{
    public class UserValuesRepo : IUserValuesRepo
    {
        private readonly IAppDataDbFactory dbFactory;

        public UserValuesRepo(IAppDataDbFactory dbFactory)
        {
            this.dbFactory = dbFactory;
        }
        public string? GetValue(string valName)
        {
            using(var db = dbFactory.Create())
            {
                return db.Variables.Where(x => x.UserId == db.UserInfo.UID && x.Name.Equals(valName)).Select(x => x.Value).SingleOrDefault();
            }
        }
    }
}
