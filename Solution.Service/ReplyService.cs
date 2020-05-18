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
    public class ReplyService : Service<Reply>, IReplyService
    {
        static IDataBaseFactory Factory = new DataBaseFactory();//l'usine de fabrication du context
        static IUnitOfWork utk = new UnitOfWork(Factory);//unité de travail a besoin du factory pour communiquer avec la base
        public ReplyService() : base(utk)
        {

        }
        public List<Reply> getMandates()
        {
            IEnumerable<Reply> m = (from replies in utk.GetRepositoryBase<Reply>().GetAll()
                                    select replies);
            List<Reply> list = m.ToList<Reply>();
            return list;
        }
    }
}
