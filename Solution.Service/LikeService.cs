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
    public class LikeService : Service<Like>, ILikeService
    {
        static IDataBaseFactory Factory = new DataBaseFactory();//l'usine de fabrication du context
        static IUnitOfWork utk = new UnitOfWork(Factory);//unité de travail a besoin du factory pour communiquer avec la base
        public LikeService():base(utk)
        {

        }
        public int LikeNumber(int id)
        {
            return GetMany(x => x.idPub == id).Count();
        }
        public List<Like> getMandates()
        {
            IEnumerable<Like> m = (from likes in utk.GetRepositoryBase<Like>().GetAll()
                                   select likes);
            List<Like> list = m.ToList<Like>();
            return list;
        }
    }
}
