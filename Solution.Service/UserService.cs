using Service.Pattern;
using Solution.Data.Infrastructure;
using Solution.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Solution.Service
{
    public class UserService : Service<User>, IUserService
    {

        public UserService() : base(utk)
        {

        }
        static IDataBaseFactory Factory = new DataBaseFactory();
        static IUnitOfWork utk = new UnitOfWork(Factory);

        public List<User> getUsers()
        {
            List<User> list = new List<User>();
            return list;
        }

        public List<User> getMandates()
        {
            IEnumerable<User> m = (from users in utk.GetRepositoryBase<User>().GetAll()
                                   select users);
            List<User> list = m.ToList<User>();
            return list;
        }

    }
}
