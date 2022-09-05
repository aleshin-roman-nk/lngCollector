using lngCollector.Model.UserDt;
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

        public int? LoadInt(string valName)
        {
            using (var db = dbFactory.Create())
            {
                var res = db.Variables.Where(x => x.user_id == db.UserInfo.UID && x.name.Equals(valName)).Select(x => x.value).FirstOrDefault();

                return res == null ? null : int.Parse(res);
            }
        }

        public string? LoadValue(string valName)
        {
            using(var db = dbFactory.Create())
            {
                return db.Variables.Where(x => x.user_id == db.UserInfo.UID && x.name.Equals(valName)).Select(x => x.value).FirstOrDefault();
            }
        }

        public void SaveValue(string name, string value)
        {
            using (var db = dbFactory.Create())
            {
                var val = db.Variables.Where(x => x.user_id == db.UserInfo.UID && x.name.Equals(name)).FirstOrDefault();

                if(val != null)
                {
                    val.value = value;
                    db.SaveChanges();
                }
                else
                {
                    val = new UserValue { name = name, value = value, user_id = db.UserInfo.UID };
                    db.Variables.Add(val);
                    db.SaveChanges();
                }
            }
        }
    }
}
