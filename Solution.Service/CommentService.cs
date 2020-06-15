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
    public class CommentService : Service<Comment>, ICommentService
    {
        static IDataBaseFactory Factory = new DataBaseFactory();//l'usine de fabrication du context
        static IUnitOfWork utk = new UnitOfWork(Factory);//unité de travail a besoin du factory pour communiquer avec la base

        public CommentService() : base(utk)
        {

        }

        public int LikeNumber(int id)
        {
            return GetMany(x => x.PublicationId == id).Count();
        }
        public List<Comment> getMandates()
        {
            IEnumerable<Comment> m = (from comments in utk.GetRepositoryBase<Comment>().GetAll()
                                      select comments);
            List<Comment> list = m.ToList<Comment>();
            return list;
        }
    }


}
