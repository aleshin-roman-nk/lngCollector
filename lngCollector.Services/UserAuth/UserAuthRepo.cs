using lngCollector.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace lngCollector.Services.UserAuth
{
    public class UserAuthRepo : IUserAuthRepo
    {
        private readonly IAppDataDbFactory _dbFactory;

        public UserAuthRepo(IAppDataDbFactory dbFactory)
        {
            _dbFactory = dbFactory;
        }

        public bool ConfirmEmail(string email, string code)
        {
            throw new NotImplementedException();
        }

        public User Create(User usr)
        {
            using (var db = _dbFactory.Create())
            {
                db.Users.Add(usr);
                db.SaveChanges();
                return usr;
            }
        }

        public bool EmailExists(string email)
        {
            using (var db = _dbFactory.Create())
            {
                return db.Users.Any(x => x.email.Equals(email));
            }
        }

        public User Get(string login, string passowrd)
        {
            using(var db = _dbFactory.Create())

            return db.Users.FirstOrDefault(x => x.email == login && x.password == passowrd);
        }

        public User GetById(int id)
        {
            using (var db = _dbFactory.Create())
                return db.Users.FirstOrDefault(x => x.id == id);
        }

        public void UpdateField<T>(User u, Expression<Func<T>> p)
        {
            using (var db = _dbFactory.Create())
            {
                db.Users.Attach(u);
                db.Entry(u).Property((p.Body as MemberExpression).Member.Name).IsModified = true;
                db.SaveChanges();
            }
        }

        public void UpdateFields(User u, string[] flds)
        {
            using (var db = _dbFactory.Create())
            {
                //User u = new User { id = userId };

                db.Users.Attach(u);

                foreach (var item in flds)
                {
                    db.Entry(u).Property(item).IsModified = true;
                    //Console.WriteLine($"{item} = {}");// Ну да... А где значения?
                }

                db.SaveChanges();
            }
        }
    }
}
