using lngCollector.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace lngCollector.Services.UserAuth
{
    public interface IUserAuthRepo
    {
        User Get(string login, string passowrd);
        User GetById(int id);
        bool ConfirmEmail(string email, string code);
        User Create(User usr);
        bool EmailExists(string email);
        void UpdateFields(User u, string[] flds);
        void UpdateField<T>(User u, Expression<Func<T>> p);
    }
}
